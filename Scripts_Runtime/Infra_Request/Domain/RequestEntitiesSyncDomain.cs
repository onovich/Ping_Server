using System;
using MortiseFrame.Abacus;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestEntitiesSyncDomain {

        // Send
        public static void Send_EntitiesSyncBroadRes(RequestInfraContext ctx, Vector2 paddle0Pos, Vector2 paddle1Pos, Vector2 ballPos) {
            ctx.CliendState_ForEachOrderly(async (clientState) => {
                await Send_EntitiesSyncRes(ctx, clientState, paddle0Pos, paddle1Pos, ballPos);
            });

        }

        static async Task Send_EntitiesSyncRes(RequestInfraContext ctx, ClientStateEntity clientState, Vector2 paddle0Pos, Vector2 paddle1Pos, Vector2 ballPos) {
            try {
                var msg = new EntitiesSyncBroadMessage();
                msg.paddle0Pos = paddle0Pos;
                msg.paddle1Pos = paddle1Pos;
                msg.ballPos = ballPos;

                byte msgID = ProtocolIDConst.BROADID_ENTITIESSYNC;

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

            } catch (Exception e) {
                PLog.Log("Send Entities Sync Broad Error: " + e.Message);
            }
        }

    }

}