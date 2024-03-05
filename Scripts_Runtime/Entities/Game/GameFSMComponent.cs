namespace Ping.Server {

    public class GameFSMComponent {

        public GameFSMStatus Status { get; private set; }
        public bool Gaming_isEntering { get; set; }

        public void NotInGame_Enter() {
            Status = GameFSMStatus.NotInGame;
        }

        public void Gaming_Enter() {
            Status = GameFSMStatus.Gaming;
            Gaming_isEntering = true;
        }

    }

}