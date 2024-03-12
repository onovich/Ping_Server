using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestKeepAliveDomain {

        // On
        public static void On_KeepAliveBroadReq(RequestInfraContext ctx, byte[] data) {

            int offset = 0;
            var msgID = ByteReader.Read<byte>(data, ref offset);
            if (msgID != ProtocolIDConst.REQID_KEEPALIVE) {
                return;
            }

            var msg = new KeepAliveReqMessage();

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.KeepAlive_On(msg);

        }

        // Send
        public static void Send_KeepAliveBroadRes(RequestInfraContext ctx, float timestamp) {
            ctx.CliendState_ForEachOrderly((clientState) => {
                Send_KeepAliveRes(ctx, clientState, timestamp);
            });
        }

        static async void Send_KeepAliveRes(RequestInfraContext ctx, ClientStateEntity clientState, float timestamp) {

            var msg = new KeepAliveResMessage();
            msg.timestamp = timestamp;

            byte msgID = ProtocolIDConst.BROADID_KEEPALIVE;

            byte[] data = msg.ToBytes();
            if (data.Length >= 4096 - 2) {
                throw new Exception("Message is too long");
            }

            byte[] dst = new byte[data.Length + 2];
            int offset = 0;
            dst[offset] = msgID;
            offset += 1;
            Buffer.BlockCopy(data, 0, dst, offset, data.Length);
            var client = clientState.clientfd;
            await client.SendAsync(dst);

        }

    }

}