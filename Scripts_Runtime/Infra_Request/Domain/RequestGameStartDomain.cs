using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestGameStartDomain {

        // On
        public static void On_GameStartReq(RequestInfraContext ctx, ClientStateEntity clientState, byte[] data) {

            var msgID = data[0];
            if (msgID != ProtocolIDConst.REQID_STARTGAME) {
                return;
            }

            int offset = 0;
            var msg = new GameStartReqMessage();

            ushort count = ByteReader.Read<ushort>(data, ref offset);
            if (count <= 0) {
                return;
            }

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.StartGame_OnHandle(msg, clientState);

        }

        // Send
        public static void Send_GameStartBroadRes(RequestInfraContext ctx) {
            ctx.CliendState_ForEach((clientState) => {
                ctx.CliendState_ForEach((clientState) => {
                    Send_GameStartRes(ctx, clientState);
                });
            });
        }

        static async void Send_GameStartRes(RequestInfraContext ctx, ClientStateEntity state) {

            var msg = new GameStartBroadMessage();
            byte msgID = ProtocolIDConst.BROADID_STARTGAME;

            byte[] data = msg.ToBytes();
            if (data.Length >= 4096 - 2) {
                throw new Exception("Message is too long");
            }

            byte[] dst = new byte[data.Length + 2];
            int offset = 0;
            dst[offset] = msgID;
            offset += 1;
            Buffer.BlockCopy(data, 0, dst, offset, data.Length);
            var client = state.clientfd;
            await client.SendAsync(dst);

        }

    }

}