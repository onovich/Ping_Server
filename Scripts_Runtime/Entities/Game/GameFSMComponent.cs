namespace Ping.Server {

    public class GameFSMComponent {

        public GameFSMStatus status;

        public bool gaming_isEntering;

        public void NotInGame_Enter() {
            status = GameFSMStatus.NotInGame;
        }

        public void Gaming_Enter() {
            status = GameFSMStatus.Gaming;
            gaming_isEntering = true;
        }

    }

}