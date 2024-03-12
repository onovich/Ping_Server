using System;
using MortiseFrame.Abacus;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestGameResultDomain {

        // Send
        public static void Send_GameResultBroadRes(RequestInfraContext ctx, int winnerPlayerIndex, int gameTurn, int score0, int score1) {
            ctx.CliendState_ForEachOrderly(async (clientState) => {
                await Send_GameResult(ctx, clientState, winnerPlayerIndex, gameTurn, score0, score1);
            });

        }

        static async Task Send_GameResult(RequestInfraContext ctx, ClientStateEntity clientState, int winnerPlayerIndex, int gameTurn, int score0, int score1) {
            try {
                var msg = new GameResultBroadMessage();
                msg.winnerPlayerIndex = winnerPlayerIndex;
                msg.gameTurn = gameTurn;
                msg.score0 = score0;
                msg.score1 = score1;

                byte msgID = ProtocolIDConst.BROADID_GAMERESULT;

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