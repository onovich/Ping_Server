using MortiseFrame.Abacus;

namespace Ping.Server.Business.Game {

    public static class GamePaddleDomain {

        public static PaddleEntity Spawn(GameBusinessContext ctx, int playerIndex, FVector2 pos) {
            var Paddle = GameFactory.Paddle_Spawn(ctx.templateInfraContext, ctx.physicalCore, playerIndex, pos);
            ctx.Paddle_Add(Paddle);
            PLog.Log("GamePaddleDomain.Spawn: PlayerIndex " + playerIndex + " pos " + pos);
            return Paddle;
        }

        public static void UnSpawn(GameBusinessContext ctx, PaddleEntity paddle) {
            ctx.Paddle_Remove(paddle);
            paddle.TearDown();
        }

        public static void ApplyMove(GameBusinessContext ctx, PaddleEntity paddle, float fixdt) {
            var field = ctx.fieldEntity;
            paddle.Move_MoveByInput(fixdt);
        }

    }

}