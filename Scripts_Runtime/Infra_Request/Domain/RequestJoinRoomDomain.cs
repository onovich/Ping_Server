using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestJoinRoomDomain {

        // On
        public static void On_JoinRoomReq(RequestInfraContext ctx, ClientStateEntity clientState, byte[] data) {

            int offset = 0;
            var msgID = ByteReader.Read<byte>(data, ref offset);
            if (msgID != ProtocolIDConst.GetID<JoinRoomReqMessage>()) {
                return;
            }

            var msg = new JoinRoomReqMessage();

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.JoinRoom_On(msg, clientState);

        }

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

        }

    }

}