using System;

namespace Ping.Server.Business.Login {

    public class LoginEventCenter {

        public LoginEventCenter() { }

        public Action OnLoginDoneHandle;
        public void LoginDone() {
            OnLoginDoneHandle?.Invoke();
        }

        public void Clear() {
            OnLoginDoneHandle = null;
        }

    }

}