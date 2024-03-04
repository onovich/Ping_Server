using MortiseFrame.Pulse;
using Ping.Protocol;
using Ping.Server.Requests;

namespace Ping.Server.Business.Game {

    public static class GameBusiness {

        static float restTime;

        public static void Init(GameBusinessContext ctx) { }

        static void StartGame(GameBusinessContext ctx) {
            GameGameDomain.NewGame(ctx);
        }

        static void ExitGame(GameBusinessContext ctx) {
            GameGameDomain.ExitGame(ctx);
        }

        public static void ResetInput(GameBusinessContext ctx) {
            var paddle1 = ctx.Paddle_Get(1);
            if (paddle1 != null) {
                GameInputDomain.Paddle_ResetInput(ctx, paddle1);
            }
            var paddle2 = ctx.Paddle_Get(2);
            if (paddle2 != null) {
                GameInputDomain.Paddle_ResetInput(ctx, paddle2);
            }
        }

        public static void PreTick(GameBusinessContext ctx, float dt) { }

        public static void FixedTick(GameBusinessContext ctx, float dt) {

            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            var status = fsm.status;
            if (status != GameFSMStatus.Gaming) { return; }

            // Ball
            var ball = ctx.Ball_Get();
            if (ball == null) { return; }
            GameBallFSMController.FixedTickFSM(ctx, ball, dt);

            // Paddle
            var paddle1 = ctx.Paddle_Get(1);
            if (paddle1 == null) { return; }
            GamePaddleFSMController.FixedTickFSM(ctx, paddle1, dt);

            var paddle2 = ctx.Paddle_Get(2);
            if (paddle2 == null) { return; }
            GamePaddleFSMController.FixedTickFSM(ctx, paddle2, dt);

            Physics2DInfra.Simulate(ctx.physics2DContext, dt);

        }

        public static void LateTick(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            var status = fsm.status;
            if (status != GameFSMStatus.Gaming) { return; }

            // Time
            GameTimeDomain.ApplyGameTime(ctx, dt);

            // Send Net Res
            var ball = ctx.Ball_Get();
            var ballPos = ball.Pos_GetPos();

            var paddle1 = ctx.Paddle_Get(1);
            var paddle1Pos = paddle1.Pos_GetPos();

            var paddle2 = ctx.Paddle_Get(2);
            var paddle2Pos = paddle2.Pos_GetPos();

            RequestEntitiesSyncDomain.Send_EntitiesSyncBroadRes(ctx.reqInfraContext, paddle1Pos, paddle2Pos, ballPos);

        }

        public static void OnLoginDone(GameBusinessContext ctx) {
            StartGame(ctx);
        }

        public static void TearDown(GameBusinessContext ctx) {
            ExitGame(ctx);
        }

    }

}