using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ping.Protocol;
using Ping.Server.Business.Game;
using Ping.Server.Business.Login;
using Ping.Server.Requests;
using MortiseFrame.Pulse;
using MortiseFrame.Rill;

namespace Ping.Server {

    public class ServerMain {

        TemplateInfraContext templateInfraContext;
        RequestInfraContext reqInfraContext;

        LoginBusinessContext loginBusinessContext;
        GameBusinessContext gameBusinessContext;

        MainContext mainContext;
        PhysicalCore physicalCore;

        bool isLoadedAssets;
        bool isTearDown;

        public void ResetInput() {
            GameBusiness.ResetInput(gameBusinessContext);
        }

        public void Start() {

            isLoadedAssets = false;
            isTearDown = false;

            loginBusinessContext = new LoginBusinessContext();
            gameBusinessContext = new GameBusinessContext();

            templateInfraContext = new TemplateInfraContext();
            reqInfraContext = new RequestInfraContext();

            mainContext = new MainContext();
            physicalCore = new PhysicalCore();

            // Inject
            gameBusinessContext.reqInfraContext = reqInfraContext;
            gameBusinessContext.templateInfraContext = templateInfraContext;
            gameBusinessContext.mainContext = mainContext;
            gameBusinessContext.physicalCore = physicalCore;

            loginBusinessContext.reqInfraContext = reqInfraContext;
            loginBusinessContext.mainContext = mainContext;

            RegisterProtocols();
            BindingEvents();

            Action action = async () => {
                try {
                    await LoadAssets();
                    Init();
                    Enter();
                    isLoadedAssets = true;
                } catch (Exception e) {
                    PLog.LogError(e.ToString());
                }
            };
            action.Invoke();

        }

        void Enter() {
            LoginBusiness.Enter(loginBusinessContext);
        }

        public void PreTick(float dt) {
            if (!isLoadedAssets || isTearDown) {
                return;
            }
            LoginBusiness.PreTickFSM(loginBusinessContext, dt);
            GameBusiness.PreTick(gameBusinessContext, dt);
        }

        public void LateTick(float dt) {
            if (!isLoadedAssets || isTearDown) {
                return;
            }
            GameBusiness.LateTick(gameBusinessContext, dt);
        }

        public void FixedTick(float dt) {
            if (!isLoadedAssets || isTearDown) {
                return;
            }
            GameBusiness.FixedTick(gameBusinessContext, dt);
        }

        public void Tick(float dt) {
            if (!isLoadedAssets || isTearDown) {
                return;
            }
            RequestInfra.Tick(reqInfraContext, dt);
        }

        void Init() {
            LoginBusiness.Init(loginBusinessContext);
            GameBusiness.Init(gameBusinessContext);
        }

        void RegisterProtocols() {
            RequestInfra.RegisterAllProtocol(reqInfraContext);
        }

        void BindingEvents() {

            // Physical
            {
                var evt = physicalCore.EventCenter;
                evt.OnTriggerEnterHandle += (a, b) => {
                    GameBusiness.OnTriggerEnter(gameBusinessContext, a, b);
                };
            }

            // Login
            {
                var evt = loginBusinessContext.evt;
                evt.OnLoginDoneHandle += () => {
                    GameBusiness.OnLoginDone(gameBusinessContext);
                };
            }

            // Request Login
            {
                RequestInfra.OnConnected(reqInfraContext, (conn) => {
                    LoginBusiness.On_ConnectReq(loginBusinessContext, conn);
                });
                RequestInfra.OnError(reqInfraContext, (msg, conn) => {
                    LoginBusiness.On_ConnectResError(loginBusinessContext, msg);
                });
                RequestInfra.On<JoinRoomReqMessage>(reqInfraContext, (msg, conn) => {
                    LoginBusiness.On_JoinRoomReq(loginBusinessContext, (JoinRoomReqMessage)msg, conn);
                });
                RequestInfra.On<GameStartReqMessage>(reqInfraContext, (msg, conn) => {
                    LoginBusiness.On_GameStartReq(loginBusinessContext, (GameStartReqMessage)msg, conn);
                });
            }

            // Request Game
            {
                RequestInfra.On<PaddleMoveReqMessage>(reqInfraContext, (msg, clientState) => {
                    GameBusiness.On_PaddleMoveReq(gameBusinessContext, (PaddleMoveReqMessage)msg, clientState);
                });
            }

        }

        async Task LoadAssets() {
            await TemplateInfra.LoadAssets(templateInfraContext);
        }

        void TearDown() {
            if (isTearDown) {
                return;
            }
            isTearDown = true;

            loginBusinessContext.evt.Clear();
            reqInfraContext.Clear();

            GameBusiness.TearDown(gameBusinessContext);
            LoginBusiness.TearDown(loginBusinessContext);
            TemplateInfra.Release(templateInfraContext);
        }

    }

}