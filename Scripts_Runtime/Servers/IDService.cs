namespace Ping.Server {

    public class IDService {

        byte playerIndexRecord; // Range: [0, 1]

        public IDService() {
            playerIndexRecord = 0;
        }

        public byte PickPlayerIndex() {
            if (playerIndexRecord > 1) {
                PLog.LogError("PlayerIndex is out of range");
                return 255;
            }

            return playerIndexRecord++;
        }

        public void Reset() {
            playerIndexRecord = 0;
        }

    }

}