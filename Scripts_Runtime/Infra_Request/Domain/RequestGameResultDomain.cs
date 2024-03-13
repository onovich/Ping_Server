using System;
using MortiseFrame.Abacus;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestGameResultDomain {

        // Send
        public static void Send_GameResultBroadRes(RequestInfraContext ctx, int winnerPlayerIndex, int gameTurn, int score0, int score1) {
            ctx.ClientState_ForEachOrderly((clientState) => {
                Send_GameResult(ctx, clientState, winnerPlayerIndex, gameTurn, score0, score1);
            });

        }

        static void Send_GameResult(RequestInfraContext ctx, ClientStateEntity clientState, int winnerPlayerIndex, int gameTurn, int score0, int score1) {
            var msg = new GameResultBroadMessage();
            msg.winnerPlayerIndex = winnerPlayerIndex;
            msg.gameTurn = gameTurn;
            msg.score0 = score0;
            msg.score1 = score1;

            ctx.Message_Enqueue(msg, clientState.clientfd);
        }

    }

}