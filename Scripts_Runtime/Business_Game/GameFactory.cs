using MortiseFrame.Abacus;

namespace Ping.Server.Business.Game {

    public static class GameFactory {

        // Field
        public static FieldEntity Field_Spawn(TemplateInfraContext templateInfraContext) {

            var config = templateInfraContext.Config_Get();

            var field = new FieldEntity();
            field.Ctor();

            // Set Bound
            field.SetBound(config.fieldBoundMin, config.fieldBoundMax);

            // Set Wall

            // Set Gate

            return field;
        }

        // Ball
        public static BallEntity Ball_Spawn(TemplateInfraContext templateInfraContext,
                                            FVector2 pos) {

            var config = templateInfraContext.Config_Get();

            var ball = new BallEntity();
            ball.Ctor();

            // Set Attr
            ball.Attr_SetMoveSpeed(config.ballMoveSpeed);
            ball.Attr_SetMoveSpeedMax(config.ballMoveSpeedMax);
            ball.Attr_SetRadius(config.ballRadius);

            // Set Pos
            ball.Pos_SetPos(pos);

            // Set FSM
            var fsmCom = ball.FSM_GetComponent();
            fsmCom.EnterIdle();

            return ball;

        }

        // Paddle
        public static PaddleEntity Paddle_Spawn(TemplateInfraContext templateInfraContext,
                                                int playerIndex,
                                                FVector2 pos) {

            var config = templateInfraContext.Config_Get();

            var paddle = new PaddleEntity();
            paddle.Ctor();

            // Base Info
            paddle.PlayerIndex_Set(playerIndex);

            // Set Attr
            paddle.Attr_SetMoveSpeed(config.paddleMoveSpeed);
            paddle.Attr_SetMoveSpeedMax(config.paddleMoveSpeedMax);
            paddle.Attr_SetSize(config.paddleSize);

            // Set Pos
            paddle.Pos_SetPos(pos);

            // Set FSM
            var fsmCom = paddle.FsmCom;
            fsmCom.Moving_Enter();

            return paddle;
        }

    }

}