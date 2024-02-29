using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GameBusiness {

        static float restTime;

        public static void Init(GameBusinessContext ctx) {

        }

        public static void StartGame(GameBusinessContext ctx) {
            GameGameDomain.NewGame(ctx);
        }

        public static void ExitGame(GameBusinessContext ctx) {
            GameGameDomain.ExitGame(ctx);
        }

        public static void Tick(GameBusinessContext ctx, float dt) {

            ResetInput(ctx);
            OnNetEvent(ctx, dt);
            LogicTick(ctx, dt);

            restTime += dt;
            const float intervalTime = 0.01f;
            if (restTime <= intervalTime) {
                FixedTick(ctx, restTime);
                restTime = 0;
            } else {
                while (restTime >= intervalTime) {
                    FixedTick(ctx, intervalTime);
                    restTime -= intervalTime;
                }
            }

            LateTick(ctx, dt);
            SendNetRes(ctx);

        }

        static void ResetInput(GameBusinessContext ctx) {
            var paddle1 = ctx.Paddle_Get(1);
            if (paddle1 != null) {
                GameInputDomain.Paddle_ResetInput(ctx, paddle1);
            }
            var paddle2 = ctx.Paddle_Get(2);
            if (paddle2 != null) {
                GameInputDomain.Paddle_ResetInput(ctx, paddle2);
            }
        }

        static void OnNetEvent(GameBusinessContext ctx, float dt) {
            // Receive

            // On

        }

        static void SendNetRes(GameBusinessContext ctx) {
            // Send
        }

        static void LogicTick(GameBusinessContext ctx, float dt) {

        }

        public static void FixedTick(GameBusinessContext ctx, float dt) {

            var game = ctx.gameEntity;
            var status = game.GetStatus();
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

        static void LateTick(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var status = game.GetStatus();
            if (status != GameFSMStatus.Gaming) { return; }

            // Time
            GameTimeDomain.ApplyGameTime(ctx, dt);

        }

        public static void TearDown(GameBusinessContext ctx) {
            ExitGame(ctx);
        }

    }

}