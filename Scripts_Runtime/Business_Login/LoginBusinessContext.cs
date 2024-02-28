using Ping.Server.Requests;

namespace Ping.Server.Business.Login {

    public class LoginBusinessContext {

        // Entity
        public PlayerEntity playerEntity;

        // Event
        public LoginEventCenter evt;

        // Infra
        public RequestInfraContext reqContext;

        public LoginBusinessContext() {
            evt = new LoginEventCenter();
        }

        // Player
        public void Player_Set(PlayerEntity playerEntity) {
            this.playerEntity = playerEntity;
        }

        public PlayerEntity Player_Get() {
            return playerEntity;
        }

        public void Player_Clear() {
            playerEntity = null;
        }

    }

}