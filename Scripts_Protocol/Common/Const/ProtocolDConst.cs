namespace Ping.Protocol {

    public static class ProtocolIDConst {

        // Req
        public const byte REQID_JOINROOM = 101;
        public const byte REQID_LEAVEROOM = 102;
        public const byte REQID_HEARTBEAT = 103;
        public const byte REQID_PADDLEMOVE = 104;
        public const byte REQID_RECONNECT = 105;
        public const byte REQID_STARTGAME = 106;
        public const byte REQID_KEEPALIVE = 107;

        // Res
        public const byte RESID_CONNECT = 202;
        public const byte RESID_RECONNECT = 203;

        // Broad
        public const byte BROADID_JOINROOM = 001;
        public const byte BROADID_GAMERESULT = 002;
        public const byte BROADID_LEAVEROOM = 003;
        public const byte BROADID_STARTGAME = 004;
        public const byte BROADID_KEEPALIVE = 005;
        public const byte BROADID_ENTITIESSYNC = 006;
        public const byte BROADID_BALLMOVE = 007;

    }

}