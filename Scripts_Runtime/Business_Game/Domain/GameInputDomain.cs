using MortiseFrame.Abacus;

namespace Ping.Server.Business.Game {

    public class GameInputDomain {

        public static void Paddle_BakeInput(GameBusinessContext ctx, PaddleEntity paddle, FVector2 axis) {
            paddle.Input_SetMoveAxis(axis);
        }

        public static void Paddle_ResetInput(GameBusinessContext ctx, PaddleEntity paddle) {
            paddle.Input_Reset();
        }

    }

}