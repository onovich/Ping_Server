namespace Ping.Server {

    public class GamePlayerEntity {

        // Base Info
        int playerID;

        // Score
        int score;

        // Net
        public string ethernetIP;
        public string publicIP;
        public string token;

        public void SetPlayerID(int id) {
            playerID = id;
        }

        public int GetPlayerID() {
            return playerID;
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