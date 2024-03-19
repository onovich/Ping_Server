namespace MortiseFrame.Rill {

    public class IDService {

        public byte msgIdRecord;
        public int clientIndexRecord;

        public IDService() {
            msgIdRecord = 0;
            clientIndexRecord = 0;
        }

        public byte PickMsgId() {
            return ++msgIdRecord;
        }

        public int PickClientIndex() {
            return ++clientIndexRecord;
        }

        public void Reset() {
            msgIdRecord = 0;
            clientIndexRecord = 0;
        }

    }

}