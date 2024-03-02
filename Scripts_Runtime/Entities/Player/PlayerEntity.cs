namespace Ping.Server {

    public class PlayerEntity {

        // Base Info
        int playerIndex;
        string userName;

        // Score
        int score;

        // Index
        public void SetPlayerIndex(int id) {
            playerIndex = id;
        }

        public int GetPlayerIndex() {
            return playerIndex;
        }

        // Name
        public void SetUserName(string name) {
            userName = name;
        }

        public string GetUserName() {
            return userName;
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