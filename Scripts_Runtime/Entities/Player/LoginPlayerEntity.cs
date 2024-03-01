namespace Ping.Server {

    public class LoginPlayerEntity {

        // Base Info
        int playerIndex;

        // Net
        public string ethernetIP;
        public string publicIP;
        public string token;

        // State
        public bool isReady;

        public LoginPlayerEntity() {
            isReady = false;
        }

        public void SetPlayerIndex(int id) {
            playerIndex = id;
        }

        public int GetPlayerIndex() {
            return playerIndex;
        }

    }

}