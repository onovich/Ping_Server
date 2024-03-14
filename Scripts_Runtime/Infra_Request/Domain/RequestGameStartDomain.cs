using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestGameStartDomain {

        // Send
        public static void Send_GameStartBroadRes(RequestInfraContext ctx) {
            ctx.ClientState_ForEachOrderly((clientState) => {
                Send_GameStartRes(ctx, clientState);
            });
        }

        static void Send_GameStartRes(RequestInfraContext ctx, ClientStateEntity state) {

            var msg = new GameStartBroadMessage();
            ctx.Message_Enqueue(msg, state.clientfd);
            PLog.Log("Send Message: " + msg.GetType().Name + " ID: " + ProtocolIDConst.GetID(msg));

        }

    }

}