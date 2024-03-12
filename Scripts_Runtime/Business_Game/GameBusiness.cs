using MortiseFrame.Pulse;
using Ping.Protocol;
using Ping.Server.Requests;

namespace Ping.Server.Business.Game {

    public static class GameBusiness {

        public static void Init(GameBusinessContext ctx) { }

        static void StartGame(GameBusinessContext ctx) {
            GameGameDomain.NewGame(ctx);
        }

        static void ExitGame(GameBusinessContext ctx) {
            GameGameDomain.ExitGame(ctx);
        }

        public static void ResetInput(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            var status = fsm.Status;
            if (status != GameFSMStatus.Gaming) { return; }

            var paddle0 = ctx.Paddle_Get(0);
            if (paddle0 != null) {
                GameInputDomain.Paddle_ResetInput(ctx, paddle0);
            }
            var paddle1 = ctx.Paddle_Get(1);
            if (paddle1 != null) {
                GameInputDomain.Paddle_ResetInput(ctx, paddle1);
            }
        }

        public static void PreTick(GameBusinessContext ctx, float dt) {

            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            var status = fsm.Status;
            if (status != GameFSMStatus.GameResult) { return; }

            if (fsm.GameResult_isEntering) {
                fsm.GameResult_isEntering = false;
                var winnierPlayerIndex = fsm.GameResult_winnierPlayerIndex;
                GameGameDomain.Win(ctx, winnierPlayerIndex);
                return;
            }
            fsm.Gaming_Enter();

        }

        public static void FixedTick(GameBusinessContext ctx, float dt) {

            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            var status = fsm.Status;
            if (status != GameFSMStatus.Gaming) { return; }

            if (fsm.Gaming_isEntering) {
                return;
            }

            // Ball
            var ball = ctx.Ball_Get();
            if (ball == null) { return; }
            GameBallFSMController.FixedTickFSM(ctx, ball, dt);

            // Paddle
            var paddle0 = ctx.Paddle_Get(0);
            if (paddle0 == null) { return; }
            GamePaddleFSMController.FixedTickFSM(ctx, paddle0, dt);

            var paddle1 = ctx.Paddle_Get(1);
            if (paddle1 == null) { return; }
            GamePaddleFSMController.FixedTickFSM(ctx, paddle1, dt);

            ctx.physicalCore.Tick(dt);

        }

        public static void LateTick(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            var status = fsm.Status;
            if (status != GameFSMStatus.Gaming) { return; }

            if (fsm.Gaming_isEntering) {
                fsm.Gaming_isEntering = false;
                return;
            }

            // Time
            GameTimeDomain.ApplyGameTime(ctx, dt);

            // Send Net Res
            var ball = ctx.Ball_Get();
            var ballPos = ball.RB.Transform.Pos;

            var paddle0 = ctx.Paddle_Get(0);
            var paddle0Pos = paddle0.RB.Transform.Pos;

            var paddle1 = ctx.Paddle_Get(1);
            var paddle1Pos = paddle1.RB.Transform.Pos;

            RequestEntitiesSyncDomain.Send_EntitiesSyncBroadRes(ctx.reqInfraContext, paddle0Pos, paddle1Pos, ballPos);
            RequestKeepAliveDomain.Send_KeepAliveBroadRes(ctx.reqInfraContext, ctx.Time_GetTimestamp());

        }

        public static void OnLoginDone(GameBusinessContext ctx) {
            StartGame(ctx);
        }

        public static void On_PaddleMoveReq(GameBusinessContext ctx, PaddleMoveReqMessage msg, ClientStateEntity clientState) {
            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            var status = fsm.Status;
            if (status != GameFSMStatus.Gaming) { return; }

            var playerIndex = clientState.playerIndex;
            var paddle = ctx.Paddle_Get(playerIndex);
            if (paddle == null) {
                PLog.LogError($"GameBusiness.On_PaddleMoveReq: paddle not found: {playerIndex}");
            }
            var axis = msg.moveAxis;
            GameInputDomain.Paddle_BakeInput(ctx, paddle, axis);
        }

        public static void OnTriggerEnter(GameBusinessContext ctx, RigidbodyEntity a, RigidbodyEntity b) {
            GamePhysicalDomain.OnTriggerEnter(ctx, a, b);
        }

        public static void TearDown(GameBusinessContext ctx) {
            ExitGame(ctx);
        }

    }

}