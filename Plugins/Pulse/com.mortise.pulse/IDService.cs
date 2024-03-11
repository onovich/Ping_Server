namespace MortiseFrame.Pulse {

    public static class IDService {

        public static uint rigidbodyRecordID = 0;

        public static uint PickRigidbodyID() {
            return rigidbodyRecordID++;
        }

        public static ulong ContactKey(uint idA, uint idB) {
            if (idA > idB) {
                return (ulong)idA << 32 | (ulong)idB;
            }
            return (ulong)idB << 32 | (ulong)idA;
        }

    }

}