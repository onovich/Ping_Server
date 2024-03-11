using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GameBallDomain {

        public static BallEntity SpawnAtOriginPos(GameBusinessContext ctx, FVector2 pos) {
            var Ball = GameFactory.Ball_Spawn(ctx.templateInfraContext, pos);
            ctx.Ball_Set(Ball);
            return Ball;
        }

        public static void UnSpawn(GameBusinessContext ctx, BallEntity ball) {
            ctx.Ball_Set(null);
            ball.TearDown();
        }

        public static void MoveAndApplyHit(GameBusinessContext ctx, BallEntity ball, float fixdt, Action<int> hitGate) {
            BallFSMComponent fsm = ball.FSM_GetComponent();
            var dir = fsm.movingDir;
            // if (PredictHit(ctx, ball, ball.Radius, hitGate)) {
            //     return;
            // }
            ball.Move_ByDir(dir, fixdt);
            CheckHit(ctx, ball, 0.02f, hitGate);
        }

        static bool PredictHit(GameBusinessContext ctx, BallEntity ball, float dis, Action<int> hitGate) {
            var succ = CheckHit(ctx, ball, dis, hitGate);
            return succ;
        }

        static bool CheckHit(GameBusinessContext ctx, BallEntity ball, float dis, Action<int> hitGate) {
            return true;
        }

        static bool TryHitPaddle(GameBusinessContext ctx, BallEntity ball, Hits hit) {
            return true;
        }

        static bool TryHitWall(GameBusinessContext ctx, BallEntity ball, Hits hit) {
            // var wall = hit.transform.GetComponent<WallEntity>();
            // if (wall == null) {
            //     return false;
            // }
            // var normal = hit.normal;
            // Reflect(ctx, ball, normal);
            return false;
        }

        static bool TryHitGate(GameBusinessContext ctx, BallEntity ball, Hits hit, Action<int> hitGate) {
            // var gate = hit.transform.GetComponent<GateEntity>();
            // if (gate == null) {
            //     return false;
            // }
            // hitGate.Invoke(gate.playerIndex);
            return false;
        }

        static void Reflect(GameBusinessContext ctx, BallEntity ball, FVector2 normal) {
            var srcDir = ball.Pos_GetDirection();
            var drtDir = FVector2.Reflect(srcDir, normal);
            ball.FSM_SetMovingDir(drtDir);
        }

    }

}