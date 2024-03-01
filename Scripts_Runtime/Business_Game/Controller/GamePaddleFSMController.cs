using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GamePaddleFSMController {

        public static void FixedTickFSM(GameBusinessContext ctx, PaddleEntity paddle, float fixdt) {

            FixedTickFSM_Any(ctx, paddle, fixdt);

            PaddleFSMStatus status = paddle.FSM_GetStatus();
            if (status == PaddleFSMStatus.Moving) {
                FixedTickFSM_Moving(ctx, paddle, fixdt);
            } else {
                PLog.LogError($"GamePaddleFSMController.FixedTickFSM: unknown status: {status}");
            }

        }

        static void FixedTickFSM_Any(GameBusinessContext ctx, PaddleEntity paddle, float fixdt) {

        }

        static void FixedTickFSM_Moving(GameBusinessContext ctx, PaddleEntity paddle, float fixdt) {
            PaddleFSMComponent fsm = paddle.FSM_GetComponent();
            if (fsm.moving_isEntering) {
                // Anim
                fsm.moving_isEntering = false;
            }

            // Move
            var player = ctx.Player_Get(paddle.GetPlayerIndex());
            if (paddle.GetPlayerIndex() == player.GetPlayerIndex()) {
                GamePaddleDomain.ApplyMove(ctx, paddle, fixdt);
            }

        }

    }

}