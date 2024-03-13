namespace Ping.Protocol {

    public static class ProtocolIDConst {

        public static object GetObject(byte id) {
            var type = Dict.GetByFirst(id);
            if (type == null) {
                throw new ArgumentException("No type found for the given ID.", nameof(id));
            }
            return Activator.CreateInstance(type);
        }

        public static byte GetID(IMessage msg) {
            var type = msg.GetType();
            return Dict.GetBySecond(type);
        }

        public static byte GetID<T>() {
            return Dict.GetBySecond(typeof(T));
        }

        public static readonly BiDictionary<byte, Type> Dict = new BiDictionary<byte, Type> {
            // Req
            {101,typeof(JoinRoomReqMessage)},
            {102, typeof(LeaveRoomReqMessage)},
            {104, typeof(PaddleMoveReqMessage)},
            {106, typeof(GameStartReqMessage)},
            {107, typeof(KeepAliveReqMessage)},

            // Res
            {202, typeof(ConnectResMessage)},

            // Broad
            {001, typeof(JoinRoomBroadMessage)},
            {002, typeof(GameResultBroadMessage)},
            {003, typeof(LeaveRoomBroadMessage)},
            {004, typeof(GameStartBroadMessage)},
            {005, typeof(KeepAliveResMessage)},
            {006, typeof(EntitiesSyncBroadMessage)},
        };

    }

}