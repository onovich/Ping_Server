namespace Ping.Server {

    public class PlayerEntity {

        // Base Info
        public int PlayerIndex { get; private set; }
        public string UserName { get; private set; }

        // Score
        public int Score { get; private set; }

        // Index
        public void SetPlayerIndex(int id) {
            PlayerIndex = id;
        }

        // Name
        public void SetUserName(string name) {
            UserName = name;
        }

        // Score
        public void Score_Inc() {
            Score++;
        }

    }

}