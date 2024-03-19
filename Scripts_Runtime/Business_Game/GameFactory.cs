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
            rb_wall0.SetIsStatic(true);
            rb_wall0.SetHolder((int)EntityType.Wall, 0);
            rb_wall0.SetLayer(LayerConst.WALL);
            wall0.RB_Set(rb_wall0);
            field.Wall_Add(wall0);

            var wall1 = new WallEntity();
            var rb_wall1 = physicalCore.Rigidbody_CreateBox(config.wall1Pos, config.wall1Size);
            rb_wall1.SetIsStatic(true);
            rb_wall1.SetHolder((int)EntityType.Wall, 1);
            rb_wall1.SetLayer(LayerConst.WALL);
            wall1.RB_Set(rb_wall1);
            field.Wall_Add(wall1);

            // Set Gate
            var gate1 = new GateEntity(1);
            var rb_gate1 = physicalCore.Rigidbody_CreateBox(config.gate1Pos, config.gate1Size);
            rb_gate1.SetIsTrigger(true);
            rb_gate1.SetIsStatic(true);
            rb_gate1.SetHolder((int)EntityType.Gate, 0);
            rb_gate1.SetLayer(LayerConst.GATE);
            gate1.RB_Set(rb_gate1);
            field.Gate_Add(gate1);

            var gate2 = new GateEntity(2);
            var rb_gate2 = physicalCore.Rigidbody_CreateBox(config.gate2Pos, config.gate2Size);
            rb_gate2.SetIsTrigger(true);
            rb_gate2.SetIsStatic(true);
            rb_gate2.SetHolder((int)EntityType.Gate, 1);
            rb_gate2.SetLayer(LayerConst.GATE);
            gate2.RB_Set(rb_gate2);
            field.Gate_Add(gate2);

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
            rb.SetHolder((int)EntityType.Ball, 0);
            rb.SetLayer(LayerConst.BALL);
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
            rb.SetHolder((int)EntityType.Paddle, playerIndex);
            rb.SetLayer(LayerConst.PADDLE);
            paddle.RB_Set(rb);

            // Set Constraint
            var constraintPos = playerIndex == 0 ? config.constraint1Pos : config.constraint2Pos;
            var constraintSize = playerIndex == 0 ? config.constraint1Size : config.constraint2Size;
            paddle.Constrain_Set(constraintPos, constraintSize);

            return paddle;
        }

    }

}