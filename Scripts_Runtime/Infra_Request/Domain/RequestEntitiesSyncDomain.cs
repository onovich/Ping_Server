using System;
using MortiseFrame.Abacus;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestEntitiesSyncDomain {

        // Send
        public static void Send_EntitiesSyncBroadRes(RequestInfraContext ctx, FVector2 paddle0Pos, FVector2 paddle1Pos, FVector2 ballPos) {
            ctx.ClientState_ForEachOrderly((clientState) => {
                Send_EntitiesSyncRes(ctx, clientState, paddle0Pos, paddle1Pos, ballPos);
            });

        }

        static void Send_EntitiesSyncRes(RequestInfraContext ctx, ClientStateEntity clientState, FVector2 paddle0Pos, FVector2 paddle1Pos, FVector2 ballPos) {
            var msg = new EntitiesSyncBroadMessage();
            msg.paddle0Pos = paddle0Pos;
            msg.paddle1Pos = paddle1Pos;
            msg.ballPos = ballPos;

            ctx.Message_Enqueue(msg, clientState.clientfd);
        }

    }

}