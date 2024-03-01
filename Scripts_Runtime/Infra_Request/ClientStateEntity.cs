using System.Net.Sockets;

namespace Ping.Server.Requests {

    public class ClientStateEntity {
        public Socket clientfd;
        public string userName;
        public byte playerID;
        public bool isJoinReady;
        public bool isStartReady;
    }

}