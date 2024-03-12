namespace Ping.Server {

    public class GameFSMComponent {

        public GameFSMStatus Status { get; private set; }
        public bool Gaming_isEntering { get; set; }
        public bool GameResult_isEntering { get; set; }
        public int GameResult_winnierPlayerIndex { get; set; }

        public void NotInGame_Enter() {
            Status = GameFSMStatus.NotInGame;
        }

        public void Gaming_Enter() {
            Status = GameFSMStatus.Gaming;
            Gaming_isEntering = true;
        }

        public void GameResult_Enter(int winnierPlayerIndex) {
            Status = GameFSMStatus.GameResult;
            GameResult_isEntering = true;
            GameResult_winnierPlayerIndex = winnierPlayerIndex;
        }

    }

}