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
                                            Physics2DInfraContext physics2DInfraContext,
                                            Vector2 pos) {

            var config = templateInfraContext.Config_Get();

            var ball = new BallEntity();
            ball.Ctor();
            ball.Inject();

            // Set Attr
            ball.Attr_SetMoveSpeed(config.ballMoveSpeed);
            ball.Attr_SetMoveSpeedMax(config.ballMoveSpeedMax);
            ball.Attr_SetRadius(config.ballRadius);

            // Set Pos
            ball.Pos_SetPos(pos);

            // Set FSM
            var fsmCom = ball.FSM_GetComponent();
            fsmCom.EnterIdle();

            // Set Physics
            var rigidbody2DCom = ball.Rigidbody2D_Get();
            physics2DInfraContext.Rigidbodies_Add(rigidbody2DCom, ball.Transform);

            return ball;

        }

        // Paddle
        public static PaddleEntity Paddle_Spawn(TemplateInfraContext templateInfraContext,
                                                Physics2DInfraContext physics2DInfraContext,
                                                int playerIndex,
                                                Vector2 pos) {

            var config = templateInfraContext.Config_Get();

            var paddle = new PaddleEntity();
            paddle.Ctor();
            paddle.Inject();

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

            // Set Physics
            var rigidbody2DCom = paddle.RB;
            physics2DInfraContext.Rigidbodies_Add(rigidbody2DCom, paddle.Transform);

            return paddle;
        }

    }

}