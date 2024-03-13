using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ping.Server.Business.Game;
using Ping.Server.Business.Login;
using Ping.Server.Requests;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class ServerMain {

        TemplateInfraContext templateInfraContext;
        RequestInfraContext requestInfraContext;

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
            requestInfraContext = new RequestInfraContext();

            mainContext = new MainContext();
            physicalCore = new PhysicalCore();

            // Inject
            gameBusinessContext.reqInfraContext = requestInfraContext;
            gameBusinessContext.templateInfraContext = templateInfraContext;
            gameBusinessContext.mainContext = mainContext;
            gameBusinessContext.physicalCore = physicalCore;

            loginBusinessContext.reqInfraContext = requestInfraContext;
            loginBusinessContext.mainContext = mainContext;

            Binding();

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

        public async void OnNetEvent(float dt) {
            if (!isLoadedAssets || isTearDown) {
                return;
            }
            await RequestInfra.Tick_On(requestInfraContext, dt);
        }

        public async void SendNetMessages(float dt) {
            if (!isLoadedAssets || isTearDown) {
                return;
            }
            await RequestInfra.Tick_Send(requestInfraContext, dt);
        }

        void Init() {
            LoginBusiness.Init(loginBusinessContext);
            GameBusiness.Init(gameBusinessContext);
        }

        void Binding() {
            Binding_Net();

            Binding_Request_Login();
            Binding_Login();

            Binding_Request_Game();
            Binding_Game();

            Binding_Physical();
        }

        void Binding_Net() {
            RequestInfra.Bind(requestInfraContext);
        }

        void Binding_Physical() {
            var evt = physicalCore.EventCenter;

            evt.OnTriggerEnterHandle += (a, b) => {
                GameBusiness.OnTriggerEnter(gameBusinessContext, a, b);
            };
        }

        void Binding_Login() {
            var evt = loginBusinessContext.evt;

            evt.OnLoginDoneHandle += () => {
                GameBusiness.OnLoginDone(gameBusinessContext);
            };
        }

        void Binding_Request_Login() {
            var evt = requestInfraContext.EventCenter;

            evt.ConnectRer_OnHandle += (clientState) => {
                LoginBusiness.On_ConnectReq(loginBusinessContext, clientState);
            };

            evt.ConnectRes_OnErrorHandle += (msg) => {
                LoginBusiness.On_ConnectResError(loginBusinessContext, msg);
            };

            evt.JoinRoom_OnHandle += (msg, clientState) => {
                LoginBusiness.On_JoinRoomReq(loginBusinessContext, msg, clientState);
            };

            evt.StartGame_OnHandle += (msg, clientState) => {
                LoginBusiness.On_GameStartReq(loginBusinessContext, msg, clientState);
            };
        }

        void Binding_Game() {
            var evt = requestInfraContext.EventCenter;

            evt.PaddleMove_OnHandle += (msg, clientState) => {
                GameBusiness.On_PaddleMoveReq(gameBusinessContext, msg, clientState);
            };

        }

        void Binding_Request_Game() {

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

            GameBusiness.TearDown(gameBusinessContext);
            LoginBusiness.TearDown(loginBusinessContext);
            TemplateInfra.Release(templateInfraContext);
        }

    }

}