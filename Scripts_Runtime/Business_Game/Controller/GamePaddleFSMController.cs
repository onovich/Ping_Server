using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GamePaddleFSMController {

        public static void FixedTickFSM(GameBusinessContext ctx, PaddleEntity paddle, float fixdt) {

            FixedTickFSM_Any(ctx, paddle, fixdt);

            PaddleFSMStatus status = paddle.FsmCom.Status;
            if (status == PaddleFSMStatus.Moving) {
                FixedTickFSM_Moving(ctx, paddle, fixdt);
            } else {
                PLog.LogError($"GamePaddleFSMController.FixedTickFSM: unknown status: {status}");
            }

        }

        static void FixedTickFSM_Any(GameBusinessContext ctx, PaddleEntity paddle, float fixdt) {

        }

        static void FixedTickFSM_Moving(GameBusinessContext ctx, PaddleEntity paddle, float fixdt) {
            PaddleFSMComponent fsm = paddle.FsmCom;
            if (fsm.Moving_isEntering) {
                // Anim
                fsm.Moving_isEntering = false;
            }

            // Move
            var player = ctx.Player_Get(paddle.PlayerIndex);
            if (paddle.PlayerIndex == player.PlayerIndex) {
                GamePaddleDomain.ApplyMove(ctx, paddle, fixdt);
            }

        }

    }

}