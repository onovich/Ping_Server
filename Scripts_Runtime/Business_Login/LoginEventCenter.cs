using System;

namespace Ping.Server.Business.Login {

    public class LoginEventCenter {

        public LoginEventCenter() { }

        public Action<string> OnLoginHandle;
        public void Login(string userName) {
            OnLoginHandle.Invoke(userName);
        }

        public Action OnCancleWaitingHandle;
        public void CancleWaiting() {
            OnCancleWaitingHandle.Invoke();
        }

        public void Clear() {
            OnLoginHandle = null;
            OnCancleWaitingHandle = null;
        }

    }

}