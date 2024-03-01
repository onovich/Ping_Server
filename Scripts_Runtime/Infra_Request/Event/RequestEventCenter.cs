using System;
using System.Net.Sockets;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public class RequestEventCenter {

        // Connect To Server Res
        public Action<Socket> ConnectRer_OnHandle;
        public void ConnectRes_On(Socket client) {
            ConnectRer_OnHandle.Invoke(client);
        }

        public Action<string> ConnectRes_OnErrorHandle;
        public void ConnectRes_OnError(string msg) {
            ConnectRes_OnErrorHandle.Invoke(msg);
        }

        // Join Room Req
        public Action<JoinRoomReqMessage, ClientStateEntity> JoinRoom_OnHandle;
        public void JoinRoom_On(JoinRoomReqMessage msg, ClientStateEntity clientState) {
            JoinRoom_OnHandle.Invoke(msg, clientState);
        }

        // Game Start Broad
        public Action<GameStartReqMessage, ClientStateEntity> StartGame_OnHandle;
        public void StartGame_On(GameStartReqMessage msg, ClientStateEntity clientState) {
            StartGame_OnHandle.Invoke(msg, clientState);
        }

        public void Clear() {
            ConnectRer_OnHandle = null;
            ConnectRes_OnErrorHandle = null;
            JoinRoom_OnHandle = null;
            StartGame_OnHandle = null;
        }

    }

}