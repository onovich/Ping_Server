using System;
using System.Net.Sockets;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public class RequestEventCenter {

        // Connect To Server Res
        public Action<ConnectResMessage> ConnectRes_OnHandle;
        public void ConnectRes_On(ConnectResMessage msg) {
            ConnectRes_OnHandle.Invoke(msg);
        }

        public Action<string> ConnectRes_OnErrorHandle;
        public void ConnectRes_OnError(string msg) {
            ConnectRes_OnErrorHandle.Invoke(msg);
        }

        // Join Room Res
        public Action<JoinRoomResMessage> JoinRoom_OnHandle;
        public void JoinRoom_On(JoinRoomResMessage msg) {
            JoinRoom_OnHandle.Invoke(msg);
        }

        // Room Start Game Broad
        public Action<StartGameBroadMessage> RoomStartGame_OnBroadHandle;
        public void RoomStartGame_OnBroad(StartGameBroadMessage msg) {
            RoomStartGame_OnBroadHandle.Invoke(msg);
        }

        public void Clear() {
            ConnectRes_OnHandle = null;
            ConnectRes_OnErrorHandle = null;
            JoinRoom_OnHandle = null;
            RoomStartGame_OnBroadHandle = null;
        }

    }

}