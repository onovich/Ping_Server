using MortiseFrame.Abacus;

namespace Ping.Server.Business.Game {

    public static class GameBallFSMController {

        public static void FixedTickFSM(GameBusinessContext ctx, BallEntity ball, float fixdt) {

            FixedTickFSM_Any(ctx, ball, fixdt);

            BallFSMStatus status = ball.FSM_GetStatus();
            if (status == BallFSMStatus.Idle) {
                FixedTickFSM_Idle(ctx, ball, fixdt);
            } else if (status == BallFSMStatus.Moving) {
                FixedTickFSM_Moving(ctx, ball, fixdt);
            } else if (status == BallFSMStatus.Dead) {
                FixedTickFSM_Dead(ctx, ball, fixdt);
            } else {
                PLog.LogError($"GameBallFSMController.FixedTickFSM: unknown status: {status}");
            }

        }

        static void FixedTickFSM_Any(GameBusinessContext ctx, BallEntity ball, float fixdt) {

        }

        static void FixedTickFSM_Idle(GameBusinessContext ctx, BallEntity ball, float fixdt) {
            BallFSMComponent fsm = ball.FSM_GetComponent();
            if (fsm.Idle_isEntering) {
                fsm.Idle_isEntering = false;
            }

            var turn = ctx.gameEntity.Turn;
            var side = turn % 2;
            var dir = side == 0 ? FVector2.down : FVector2.up;
            var game = ctx.gameEntity;
            var config = ctx.templateInfraContext.Config_Get();
            dir = game.random.GetRandomDirection(dir, config.ballSpawnAngleRange);

            fsm.EnterMoving(dir);
        }

        static void FixedTickFSM_Moving(GameBusinessContext ctx, BallEntity ball, float fixdt) {
            BallFSMComponent fsm = ball.FSM_GetComponent();
            if (fsm.Moving_isEntering) {
                fsm.Moving_isEntering = false;
            }

            GameBallDomain.SetMoveOnce(ctx, ball, fixdt);

        }

        static void FixedTickFSM_Dead(GameBusinessContext ctx, BallEntity ball, float fixdt) {
            BallFSMComponent fsm = ball.FSM_GetComponent();
            if (fsm.Dead_isEntering) {
                fsm.Dead_isEntering = false;
                ball.Move_Stop();
                var gateIndex = fsm.Dead_gatePlayerIndex;
                var winnerPlayerIndex = 3 - gateIndex; // 偷懒做法
                GameGameDomain.EnterGameResult(ctx, winnerPlayerIndex);
            }
        }

    }

}