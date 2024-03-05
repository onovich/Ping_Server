namespace Ping.Server {

    public class LoginFSMComponent {

        public LoginFSMStatus Status { get; private set; }

        public bool WaitForJoin_isEntering { get; set; }
        public bool WaitForStart_isEntering { get; set; }
        public bool LoginDone_isEntering { get; set; }

        public void WaitForJoin_Enter() {
            Status = LoginFSMStatus.WaitForJoin;
            WaitForJoin_isEntering = true;
            PLog.Log("LoginBusiness Enter WaitForJoin");
        }

        public void WaitForStart_Enter() {
            Status = LoginFSMStatus.WaitForStart;
            WaitForStart_isEntering = true;
            PLog.Log("LoginBusiness Enter WaitForStart");
        }

        public void LoginDone_Enter() {
            Status = LoginFSMStatus.LoginDone;
            LoginDone_isEntering = true;
            PLog.Log("LoginBusiness Enter LoginDone");
        }

    }

}