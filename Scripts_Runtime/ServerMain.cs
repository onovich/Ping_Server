using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ping.Server.Business.Game;
using Ping.Server.Business.Login;
using Ping.Server.Requests;

namespace Ping.Server {

    public class ServerMain {

        TemplateInfraContext templateInfraContext;
        Physics2DInfraContext physics2DInfraContext;
        RequestInfraContext requestInfraContext;

        LoginBusinessContext loginBusinessContext;
        GameBusinessContext gameBusinessContext;


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
            physics2DInfraContext = new Physics2DInfraContext();
            requestInfraContext = new RequestInfraContext();

            // Inject
            gameBusinessContext.templateInfraContext = templateInfraContext;
            gameBusinessContext.physics2DContext = physics2DInfraContext;
            loginBusinessContext.reqInfraContext = requestInfraContext;

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

        public void OnNetEvent(float dt) {
            if (!isLoadedAssets || isTearDown) {
                return;
            }
            GameBusiness.OnNetEvent(gameBusinessContext, dt);
        }

        void Init() {

            GameBusiness.Init(gameBusinessContext);

        }

        void Binding() {
            Binding_Request();
        }

        void Binding_Request() {

            var evt = loginBusinessContext.reqInfraContext.EventCenter;

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