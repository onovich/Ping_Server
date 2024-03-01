namespace Ping.Server {

    public class LoginEntity {

        LoginFSMComponent fsmComponent;

        public LoginEntity() {
            fsmComponent = new LoginFSMComponent();
        }

        public LoginFSMComponent FSM_GetComponent() {
            return fsmComponent;
        }

    }

}