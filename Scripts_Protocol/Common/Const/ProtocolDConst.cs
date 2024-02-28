namespace Ping.Protocol {

    public static class ProtocolIDConst {

        // Req
        public const byte REQID_JOINROOM = 101;
        public const byte REQID_HEARTBEAT = 102;
        public const byte REQID_PADDLEMOVE = 104;
        public const byte REQID_RECONNECT = 108;

        // Res
        public const byte RESID_JOINROOM = 201;
        public const byte RESID_CONNECT = 207;
        public const byte RESID_RECONNECT = 208;

        // Broad
        public const byte BROADID_EXITGAME = 001;
        public const byte BROADID_STARTGAME = 002;
        public const byte BROADID_HEARTBEAT = 202;
        public const byte BROADID_PADDLEMOVE = 204;
        public const byte BROADID_BALLMOVE = 205;

    }

}