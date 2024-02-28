using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestJoinRoomDomain {

        // On
        public static void OnJoinRoomReq(RequestInfraContext ctx, byte[] data) {

            var msgID = data[0];
            if (msgID != ProtocolIDConst.RESID_JOINROOM) {
                return;
            }

            int offset = 0;
            var msg = new JoinRoomResMessage();

            ushort count = ByteReader.Read<ushort>(data, ref offset);
            if (count <= 0) {
                return;
            }

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.JoinRoom_On(msg);

        }

        // Send
        public static async void SendJoinRoomResAsync(RequestInfraContext ctx, ClientState state, string token) {

            var msg = new JoinRoomResMessage();
            msg.userToken = token;
            byte msgID = ProtocolIDConst.REQID_JOINROOM;

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