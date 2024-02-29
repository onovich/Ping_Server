using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ping.Server.Business.Game;
using Ping.Server.Business.Login;

namespace Ping.Server {

    public class ServerMain {

        TemplateInfraContext templateInfraContext;
        Physics2DInfraContext physics2DInfraContext;

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

            // Inject
            gameBusinessContext.templateInfraContext = templateInfraContext;
            gameBusinessContext.physics2DContext = physics2DInfraContext;

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
            LoginBusiness.PreTick(loginBusinessContext, dt);
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