namespace Ping.Server {

    public struct InputKeybindingComponent {

        Dictionary<InputKeyEnum, KeyCode[]> all;

        public void Ctor() {
            all = new Dictionary<InputKeyEnum, KeyCode[]>();
        }

        public void Bind(InputKeyEnum key, KeyCode[] keyCodes) {
            bool succ = all.TryAdd(key, keyCodes);
            if (!succ) {
                all[key] = keyCodes;
            }
        }

        public bool IsKeyPressing(InputKeyEnum keyEnum) {
            bool has = all.TryGetValue(keyEnum, out KeyCode[] keyCodes);
            if (!has) {
                return false;
            }
            foreach (KeyCode keyCode in keyCodes) {
                if (Input.GetKey(keyCode)) {
                    return true;
                }
            }
            return false;
        }

        public bool IsKeyDown(InputKeyEnum keyEnum) {
            bool has = all.TryGetValue(keyEnum, out KeyCode[] keyCodes);
            if (!has) {
                return false;
            }
            foreach (KeyCode keyCode in keyCodes) {
                if (Input.GetKeyDown(keyCode)) {
                    return true;
                }
            }
            return false;
        }

        public bool IsKeyUp(InputKeyEnum keyEnum) {
            bool has = all.TryGetValue(keyEnum, out KeyCode[] keyCodes);
            if (!has) {
                return false;
            }
            foreach (KeyCode keyCode in keyCodes) {
                if (Input.GetKeyUp(keyCode)) {
                    return true;
                }
            }
            return false;
        }

    }

}