namespace Ping.Server {

    public class LoginPlayerEntity {

        // Base Info
        int playerID;

        // Net
        public string ethernetIP;
        public string publicIP;
        public string token;

        // State
        public bool isReady;

        public LoginPlayerEntity() {
            isReady = false;
        }

        public void SetPlayerID(int id) {
            playerID = id;
        }

        public int GetPlayerID() {
            return playerID;
        }

    }

}