using MortiseFrame.Abacus;

namespace Ping.Server {

    public class InputEntity {

        Vector2 moveAxis;
        bool isUI_Setting;
        InputKeybindingComponent keybindingCom;

        public void Ctor() {
            keybindingCom.Ctor();
        }

        public Vector2 Move_GetAxis() {
            return moveAxis;
        }

        public bool IsUI_Setting() {
            return isUI_Setting;
        }

        public void ProcessInput(float dt) {

            // Move Axis
            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveLeft)) {
                moveAxis.x = -1;
            } else if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveRight)) {
                moveAxis.x = 1;
            }

            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveDown)) {
                moveAxis.y = -1;
            } else if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveUp)) {
                moveAxis.y = 1;
            }

            // UI
            if (keybindingCom.IsKeyDown(InputKeyEnum.UI_Setting)) {
                isUI_Setting = true;
            }

        }

        public void Keybinding_Set(InputKeyEnum key, KeyCode[] keyCodes) {
            keybindingCom.Bind(key, keyCodes);
        }

        public void Reset() {
            moveAxis = Vector2.zero;
            isUI_Setting = false;
        }

    }

}