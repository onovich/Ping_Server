namespace Ping.Server {

    public class LoginFSMComponent {

        LoginFSMStatus status;
        public LoginFSMStatus Status => status;

        public bool waitForJoin_isEntering;
        public bool waitForStart_isEntering;
        public bool loginDone_isEntering;

        public void WaitForJoin_Enter() {
            status = LoginFSMStatus.WaitForJoin;
            waitForJoin_isEntering = true;
        }

        public void WaitForStart_Enter() {
            status = LoginFSMStatus.WaitForStart;
            waitForStart_isEntering = true;
        }

        public void LoginDone_Enter() {
            status = LoginFSMStatus.LoginDone;
            loginDone_isEntering = true;
        }

    }

}