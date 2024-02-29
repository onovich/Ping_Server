using MortiseFrame.Abacus;

namespace Ping.Server.Business.Game {

    public static class GameBallDomain {

        public static BallEntity SpawnAtOriginPos(GameBusinessContext ctx, Vector2 pos) {
            var Ball = GameFactory.Ball_Spawn(ctx.templateInfraContext, ctx.physics2DContext, pos);
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
            if (PredictHit(ctx, ball, ball.Attr_GetRadius(), hitGate)) {
                return;
            }
            ball.Move_ByDir(dir, fixdt);
            CheckHit(ctx, ball, 0.02f, hitGate);
        }

        static bool PredictHit(GameBusinessContext ctx, BallEntity ball, float dis, Action<int> hitGate) {
            var succ = CheckHit(ctx, ball, dis, hitGate);
            return succ;
        }

        static bool CheckHit(GameBusinessContext ctx, BallEntity ball, float dis, Action<int> hitGate) {
            var succ = false;
            int targetLayerMask = 1 << LayerConst.WALL;
            targetLayerMask |= 1 << LayerConst.PADDLE;
            targetLayerMask |= 1 << LayerConst.GATE;
            var hits = ctx.raycastTemp;
            var dir = ball.Pos_GetDirection();
            int count = Physics2DInfra.CircleCastNonAlloc(ctx.physics2DContext, ball.Pos_GetPos(), ball.Attr_GetRadius(), dir, hits, dis, targetLayerMask);
            if (count <= 0) {
                return succ;
            }
            var hit = hits[0];
            succ |= TryHitWall(ctx, ball, hit);
            succ |= TryHitPaddle(ctx, ball, hit);
            succ |= TryHitGate(ctx, ball, hit, hitGate);
            return succ;
        }

        static bool TryHitPaddle(GameBusinessContext ctx, BallEntity ball, RaycastHit2D hit) {
            if (hit.collider == null) {
                return false;
            }
            var paddle = hit.transform.GetComponent<PaddleEntity>();
            if (paddle == null) {
                return false;
            }
            var normal = hit.normal;
            Reflect(ctx, ball, normal);
            return true;
        }

        static bool TryHitWall(GameBusinessContext ctx, BallEntity ball, RaycastHit2D hit) {
            if (hit.collider == null) {
                return false;
            }
            var wall = hit.transform.GetComponent<WallEntity>();
            if (wall == null) {
                return false;
            }
            var normal = hit.normal;
            Reflect(ctx, ball, normal);
            return true;
        }

        static bool TryHitGate(GameBusinessContext ctx, BallEntity ball, RaycastHit2D hit, Action<int> hitGate) {
            if (hit.collider == null) {
                return false;
            }
            var gate = hit.transform.GetComponent<GateEntity>();
            if (gate == null) {
                return false;
            }
            hitGate.Invoke(gate.playerID);
            return true;
        }

        static void Reflect(GameBusinessContext ctx, BallEntity ball, Vector2 normal) {
            var srcDir = ball.Pos_GetDirection();
            var drtDir = MathFP.GetReflectDir(srcDir, normal);
            ball.FSM_SetMovingDir(drtDir);
        }

    }

}