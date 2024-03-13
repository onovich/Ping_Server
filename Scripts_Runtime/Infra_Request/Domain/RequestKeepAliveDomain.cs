using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestKeepAliveDomain {

        // On
        public static void On_KeepAliveBroadReq(RequestInfraContext ctx, byte[] data) {

            int offset = 0;
            var msgID = ByteReader.Read<byte>(data, ref offset);
            if (msgID != ProtocolIDConst.GetID<KeepAliveReqMessage>()) {
                return;
            }

            var msg = new KeepAliveReqMessage();

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.KeepAlive_On(msg);

        }

        // Send
        public static void Send_KeepAliveBroadRes(RequestInfraContext ctx, float timestamp) {
            ctx.ClientState_ForEachOrderly((clientState) => {
                Send_KeepAliveRes(ctx, clientState, timestamp);
                PLog.Log($"Send_KeepAliveRes to {clientState.clientfd.RemoteEndPoint}, timestamp: {timestamp}");
            });
        }

        static void Send_KeepAliveRes(RequestInfraContext ctx, ClientStateEntity clientState, float timestamp) {

            var msg = new KeepAliveResMessage();
            msg.timestamp = timestamp;

            ctx.Message_Enqueue(msg, clientState.clientfd);

        }

    }

}