using Ping.Server.Requests;

namespace Ping.Server.Business.Login {

    public class LoginBusinessContext {

        // Entity
        public LoginEntity loginEntity;

        // Service
        public IDService idService;

        // Event
        public LoginEventCenter evt;

        // Infra
        public RequestInfraContext reqContext;

        public LoginBusinessContext() {
            loginEntity = new LoginEntity();
            idService = new IDService();
            evt = new LoginEventCenter();
        }

        // ID
        public byte PickPlayerID() {
            return idService.PickPlayerID();
        }

        public void Player_Clear() {
        }

    }

}