using Ping.Server.Requests;

namespace Ping.Server.Business.Login {

    public class LoginBusinessContext {

        // Entity
        public LoginEntity loginEntity;

        // Event
        public LoginEventCenter evt;

        // Infra
        public RequestInfraContext reqInfraContext;

        // Main
        public MainContext mainContext;

        public LoginBusinessContext() {
            loginEntity = new LoginEntity();
            evt = new LoginEventCenter();
        }

        // Player
        public void Player_Add(PlayerEntity playerEntity) {
            mainContext.Player_Add(playerEntity);
        }

    }

}