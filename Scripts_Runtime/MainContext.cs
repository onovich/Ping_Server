namespace Ping.Server {

    public class MainContext {

        // Entity
        public PlayerEntity player1Entity;
        public PlayerEntity player2Entity;

        public MainContext() {
        }

        // Player
        public void Player_Add(PlayerEntity playerEntity) {
            if (playerEntity.GetPlayerIndex() == 0) {
                player1Entity = playerEntity;
            }
            if (playerEntity.GetPlayerIndex() == 1) {
                player2Entity = playerEntity;
            }
        }

        public PlayerEntity Player_Get(int index) {
            if (index == 0) {
                return player1Entity;
            }
            if (index == 1) {
                return player2Entity;
            }
            return null;
        }

        public void Player_Clear() {
            player1Entity = null;
            player2Entity = null;
        }

    }

}