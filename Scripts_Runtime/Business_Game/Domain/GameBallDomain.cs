using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GameBallDomain {

        public static BallEntity SpawnAtOriginPos(GameBusinessContext ctx, FVector2 pos) {
            var Ball = GameFactory.Ball_Spawn(ctx.templateInfraContext, ctx.physicalCore, pos);
            ctx.Ball_Set(Ball);
            return Ball;
        }

        public static void UnSpawn(GameBusinessContext ctx, BallEntity ball) {
            ctx.Ball_Set(null);
            ball.TearDown();
        }

        public static void SetMoveOnce(GameBusinessContext ctx, BallEntity ball, float fixdt) {
            BallFSMComponent fsm = ball.FSM_GetComponent();
            var dir = fsm.movingDir;
            ball.Move_SetVelocity_Once(dir);
        }

    }

}