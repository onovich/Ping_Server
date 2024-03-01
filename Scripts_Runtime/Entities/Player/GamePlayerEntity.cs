namespace Ping.Server {

    public class GamePlayerEntity {

        // Base Info
        int playerIndex;

        // Score
        int score;

        // Net
        public string ethernetIP;
        public string publicIP;
        public string token;

        public void SetPlayerIndex(int id) {
            playerIndex = id;
        }

        public int GetPlayerIndex() {
            return playerIndex;
        }

        // Score
        public void Score_Inc() {
            score++;
        }

        public int Score_Get() {
            return score;
        }

    }

}