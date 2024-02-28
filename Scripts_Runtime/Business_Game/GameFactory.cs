using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GameFactory {

        // Field
        public static FieldEntity Field_Spawn(TemplateInfraContext templateInfraContext,
                                              AssetsInfraContext assetsInfraContext) {

            var config = templateInfraContext.Config_Get();

            var prefab = assetsInfraContext.Entity_GetField();
            var field = GameObject.Instantiate(prefab).GetComponent<FieldEntity>();
            field.Ctor();

            // Set Bound
            field.SetBound(config.fieldBoundMin, config.fieldBoundMax);

            return field;
        }

        // Ball
        public static BallEntity Ball_Spawn(TemplateInfraContext templateInfraContext,
                                            AssetsInfraContext assetsInfraContext,
                                            Vector2 pos) {

            var config = templateInfraContext.Config_Get();

            var prefab = assetsInfraContext.Entity_GetBall();
            var ball = GameObject.Instantiate(prefab).GetComponent<BallEntity>();
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
                                                AssetsInfraContext assetsInfraContext,
                                                int id,
                                                Vector2 pos) {

            var config = templateInfraContext.Config_Get();

            var prefab = assetsInfraContext.Entity_GetPaddle();
            var paddle = GameObject.Instantiate(prefab).GetComponent<PaddleEntity>();
            paddle.Ctor();

            // Base Info
            paddle.SetPlayerID(id);

            // Set Attr
            paddle.Attr_SetMoveSpeed(config.paddleMoveSpeed);
            paddle.Attr_SetMoveSpeedMax(config.paddleMoveSpeedMax);
            paddle.Attr_SetSize(config.paddleSize);

            // Set Pos
            paddle.Pos_SetPos(pos);

            // Set FSM
            var fsmCom = paddle.FSM_GetComponent();
            fsmCom.Moving_Enter();

            return paddle;
        }

    }

}