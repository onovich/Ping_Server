using MortiseFrame.Abacus;

namespace Ping.Server.Business.Game {

    public static class GamePaddleDomain {

        public static PaddleEntity Spawn(GameBusinessContext ctx, int id, Vector2 pos) {
            var Paddle = GameFactory.Paddle_Spawn(ctx.templateInfraContext, ctx.physics2DContext, id, pos);
            ctx.Paddle_Set(Paddle);
            return Paddle;
        }

        public static void UnSpawn(GameBusinessContext ctx, PaddleEntity paddle) {
            ctx.Paddle_Clear(paddle);
            paddle.TearDown();
        }

        public static void ApplyMove(GameBusinessContext ctx, PaddleEntity paddle, float fixdt) {
            var field = ctx.fieldEntity;
            var constrain = field.GetBound();
            paddle.Move_Move(fixdt, constrain);
        }

    }

}