namespace Ping.Protocol {

    public static class ProtocolIDConst {

        // Req
        public const byte REQID_JOINROOM = 101;
        public const byte REQID_LEAVEROOM = 102;
        public const byte REQID_HEARTBEAT = 103;
        public const byte REQID_PADDLEMOVE = 104;
        public const byte REQID_RECONNECT = 105;

        // Res
        public const byte RESID_JOINROOM = 201;
        public const byte RESID_CONNECT = 202;
        public const byte RESID_RECONNECT = 203;
        public const byte RESID_PADDLESYNC = 204;

        // Broad
        public const byte BROADID_GAMERESULT = 001;
        public const byte BROADID_LEAVEROOM = 002;
        public const byte BROADID_STARTGAME = 003;
        public const byte BROADID_HEARTBEAT = 004;
        public const byte BROADID_PADDLEMOVE = 005;
        public const byte BROADID_BALLMOVE = 006;

    }

}