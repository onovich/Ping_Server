using Ping.Server.Requests;

namespace Ping.Server.Business.Login {

    public class LoginBusinessContext {

        // Entity
        public LoginEntity loginEntity;

        // Event
        public LoginEventCenter evt;

        // Infra
        public RequestInfraContext reqInfraContext;

        public LoginBusinessContext() {
            loginEntity = new LoginEntity();
            evt = new LoginEventCenter();
        }

    }

}