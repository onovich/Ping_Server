using System;

namespace Ping.Protocol {

    public static class ProtocolIDConst {

        public static object GetObject(byte id) {
            var has = dict.TryGetByFirst(id, out Type type);
            if (!has) {
                throw new ArgumentException("No type found for the given ID.", id.ToString());
            }
            if (type == null) {
                throw new ArgumentException("No type found for the given ID.", id.ToString());
            }
            return Activator.CreateInstance(type);
        }

        public static byte GetID(IMessage msg) {
            var type = msg.GetType();
            var has = dict.TryGetBySecond(type, out byte id);
            if (!has) {
                throw new ArgumentException("ID Not Found");
            }
            return id;
        }

        public static byte GetID<T>() {
            var has = dict.TryGetBySecond(typeof(T), out byte id);
            if (!has) {
                throw new ArgumentException("ID Not Found");
            }
            return id;
        }

        static readonly BiDictionary<byte, Type> dict = new BiDictionary<byte, Type> {
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