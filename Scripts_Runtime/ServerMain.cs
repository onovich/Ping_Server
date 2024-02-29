using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ping.Server.Business.Game;
using Ping.Server.Business.Login;

namespace Ping.Server {

    public class ServerMain {

        TemplateInfraContext templateInfraContext;

        LoginBusinessContext loginBusinessContext;
        GameBusinessContext gameBusinessContext;

        bool isLoadedAssets;
        bool isTearDown;

        public void ResetInput() {

        }

        public void ProcessInput() {

            isLoadedAssets = false;
            isTearDown = false;

            loginBusinessContext = new LoginBusinessContext();
            gameBusinessContext = new GameBusinessContext();

            templateInfraContext = new TemplateInfraContext();

            // Inject
            gameBusinessContext.templateInfraContext = templateInfraContext;

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
            LoginBusiness.Tick(loginBusinessContext, dt);
            GameBusiness.Tick(gameBusinessContext, dt);
        }

        public void LateTick(float dt) {

        }

        public void FixedTick(float dt) {

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