using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestGameStartDomain {

        // On
        public static void On_GameStartReq(RequestInfraContext ctx, ClientStateEntity clientState, byte[] data) {

            int offset = 0;
            var msgID = ByteReader.Read<byte>(data, ref offset);
            if (msgID != ProtocolIDConst.GetID<GameStartReqMessage>()) {
                return;
            }

            var msg = new GameStartReqMessage();

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.StartGame_On(msg, clientState);

        }

        // Send
        public static void Send_GameStartBroadRes(RequestInfraContext ctx) {
            ctx.ClientState_ForEachOrderly((clientState) => {
                Send_GameStartRes(ctx, clientState);
            });
        }

        static void Send_GameStartRes(RequestInfraContext ctx, ClientStateEntity state) {

            var msg = new GameStartBroadMessage();
            ctx.Message_Enqueue(msg, state.clientfd);

        }

    }

}