namespace Ping.Server {

    public class IDService {

        byte playerIdRecord;

        public IDService() {
            playerIdRecord = 0;
        }

        public byte PickPlayerID() {
            playerIdRecord += 1;
            if (playerIdRecord > 2 || playerIdRecord < 1) {
                PLog.LogError("PlayerID is out of range");
            }
            return playerIdRecord;
        }

    }

}