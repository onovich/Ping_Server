using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestJoinRoomDomain {

        // On
        public static void On_JoinRoomReq(RequestInfraContext ctx, ClientStateEntity clientState, byte[] data) {

            var msgID = data[0];
            if (msgID != ProtocolIDConst.REQID_JOINROOM) {
                return;
            }

            int offset = 0;
            var msg = new JoinRoomReqMessage();

            ushort count = ByteReader.Read<ushort>(data, ref offset);
            if (count <= 0) {
                return;
            }

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.JoinRoom_On(msg, clientState);

        }

        // Send
        public static void Send_JoinRoomBroadRes(RequestInfraContext ctx) {
            ctx.CliendState_ForEach((clientState) => {
                Send_JoinRoomRes(ctx, clientState);
            });
        }

        static async void Send_JoinRoomRes(RequestInfraContext ctx, ClientStateEntity clientState) {

            var msg = new JoinRoomBroadMessage();
            msg.status = 1;
            msg.ownerIndex = clientState.playerIndex;
            msg.playerIndexs = new byte[2];
            msg.userNames = new string[2];
            ctx.CliendState_ForEach((clientState) => {
                var playerIndex = clientState.playerIndex;
                msg.playerIndexs[playerIndex] = playerIndex;
                msg.userNames[playerIndex] = clientState.userName;
            });

            byte msgID = ProtocolIDConst.BROADID_JOINROOM;

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