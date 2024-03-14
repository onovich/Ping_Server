using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestJoinRoomDomain {

        // Send
        public static void Send_JoinRoomBroadRes(RequestInfraContext ctx) {
            ctx.ClientState_ForEachOrderly((clientState) => {
                Send_JoinRoomRes(ctx, clientState);
            });
        }

        static void Send_JoinRoomRes(RequestInfraContext ctx, ClientStateEntity clientState) {

            var msg = new JoinRoomBroadMessage();
            msg.status = 1;
            msg.ownerIndex = clientState.playerIndex;
            msg.userNames = new string[2];
            ctx.ClientState_ForEachOrderly((clientState) => {
                var playerIndex = clientState.playerIndex;
                msg.userNames[playerIndex] = clientState.userName;
            });

            ctx.Message_Enqueue(msg, clientState.clientfd);
            PLog.Log("Send Message: " + msg.GetType().Name + " ID: " + ProtocolIDConst.GetID(msg) + " To: " + clientState.playerIndex + " " + clientState.userName);

        }

    }

}