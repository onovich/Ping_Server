using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ping.Server.Business.Game;
using Ping.Server.Business.Login;

namespace Ping.Server {

    public class ServerMain {

        InputEntity inputEntity;

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

            inputEntity = new InputEntity();

            loginBusinessContext = new LoginBusinessContext();
            gameBusinessContext = new GameBusinessContext();

            templateInfraContext = new TemplateInfraContext();

            // Inject
            loginBusinessContext.uiAppContext = uiAppContext;

            gameBusinessContext.inputEntity = inputEntity;
            gameBusinessContext.templateInfraContext = templateInfraContext;
            gameBusinessContext.uiAppContext = uiAppContext;

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

        public void PreTick(double dt) {
            if (!isLoadedAssets || isTearDown) {
                return;
            }
            var dt = Time.deltaTime;
            LoginBusiness.Tick(loginBusinessContext, dt);
            GameBusiness.Tick(gameBusinessContext, dt);
        }

        public void LateTick(double dt) {

        }

        public void FixedTick(double dt) {

        }

        void Init() {

            Application.targetFrameRate = 120;

            var inputEntity = this.inputEntity;
            inputEntity.Ctor();
            inputEntity.Keybinding_Set(InputKeyEnum.MoveLeft, new KeyCode[] { KeyCode.A });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveRight, new KeyCode[] { KeyCode.D });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveUp, new KeyCode[] { KeyCode.W });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveDown, new KeyCode[] { KeyCode.S });
            inputEntity.Keybinding_Set(InputKeyEnum.Cancel, new KeyCode[] { KeyCode.Escape, KeyCode.Mouse1 });
            inputEntity.Keybinding_Set(InputKeyEnum.UI_Setting, new KeyCode[] { KeyCode.Escape });

            GameBusiness.Init(gameBusinessContext);

            UIApp.Init(uiAppContext);

        }

        void Binding() {
            var uiEventCenter = uiAppContext.eventCenter;
            // UI
            // - Login
            uiEventCenter.Login_OnStartGameClickHandle += () => {
                LoginBusiness.Exit(loginBusinessContext);
                GameBusiness.StartGame(gameBusinessContext);
            };

            uiEventCenter.Login_OnExitGameClickHandle += () => {
                LoginBusiness.ExitLogin(loginBusinessContext);
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