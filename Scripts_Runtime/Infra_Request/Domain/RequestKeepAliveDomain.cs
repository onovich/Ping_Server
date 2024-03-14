using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestKeepAliveDomain {

        // Send
        public static void Send_KeepAliveBroadRes(RequestInfraContext ctx, float timestamp) {
            ctx.ClientState_ForEachOrderly((clientState) => {
                Send_KeepAliveRes(ctx, clientState, timestamp);
            });
        }

        static void Send_KeepAliveRes(RequestInfraContext ctx, ClientStateEntity clientState, float timestamp) {

            var msg = new KeepAliveResMessage();
            msg.timestamp = timestamp;

            ctx.Message_Enqueue(msg, clientState.clientfd);

        }

    }

}