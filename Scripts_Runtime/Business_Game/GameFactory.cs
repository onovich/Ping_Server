using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GameFactory {

        // Field
        public static FieldEntity Field_Spawn(TemplateInfraContext templateInfraContext,
                                              PhysicalCore physicalCore) {

            var config = templateInfraContext.Config_Get();

            var field = new FieldEntity();
            field.Ctor();

            // Set Wall
            var wall0 = new WallEntity();
            var rb_wall0 = physicalCore.Rigidbody_CreateBox(config.wall0Pos, config.wall0Size);
            wall0.RB_Set(rb_wall0);

            var wall1 = new WallEntity();
            var rb_wall1 = physicalCore.Rigidbody_CreateBox(config.wall1Pos, config.wall1Size);
            wall1.RB_Set(rb_wall1);

            // Set Gate
            var gate0 = new GateEntity();
            var rb_gate0 = physicalCore.Rigidbody_CreateBox(config.gate0Pos, config.gate0Size);
            rb_gate0.SetIsTrigger(true);
            gate0.RB_Set(rb_gate0);

            var gate1 = new GateEntity();
            var rb_gate1 = physicalCore.Rigidbody_CreateBox(config.gate1Pos, config.gate1Size);
            rb_gate0.SetIsTrigger(true);
            gate1.RB_Set(rb_gate1);

            return field;
        }

        // Ball
        public static BallEntity Ball_Spawn(TemplateInfraContext templateInfraContext,
                                            PhysicalCore physicalCore,
                                            FVector2 pos) {

            var config = templateInfraContext.Config_Get();

            var ball = new BallEntity();
            ball.Ctor();

            // Set Attr
            ball.Attr_SetMoveSpeed(config.ballMoveSpeed);
            ball.Attr_SetMoveSpeedMax(config.ballMoveSpeedMax);
            ball.Attr_SetRadius(config.ballRadius);

            // Set FSM
            var fsmCom = ball.FSM_GetComponent();
            fsmCom.EnterIdle();

            // Set Physical
            var rb = physicalCore.Rigidbody_CreateCircle(pos, config.ballRadius);
            rb.SetMass(1);
            rb.SetRestitution(1);
            ball.RB_Set(rb);

            return ball;

        }

        // Paddle
        public static PaddleEntity Paddle_Spawn(TemplateInfraContext templateInfraContext,
                                                PhysicalCore physicalCore,
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

            // Set FSM
            var fsmCom = paddle.FsmCom;
            fsmCom.Moving_Enter();

            // Set Physical
            var rb = physicalCore.Rigidbody_CreateBox(pos, config.paddleSize);
            rb.SetMass(10);
            paddle.RB_Set(rb);

            return paddle;
        }

    }

}