using System;
using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class PaddleEntity : IEntity {

        // Base Info
        public int PlayerIndex { get; private set; }
        public EntityType EntityType => EntityType.Paddle;

        // Attr
        float MoveSpeed { get; set; }
        public float MoveSpeedMax { get; private set; }
        public Vector2 Size { get; private set; }

        // FSM
        public PaddleFSMComponent FsmCom { get; private set; }

        // Input
        PaddleInputComponent inputCom;

        // Physics
        public Rigidbody2DComponent RB { get; private set; }
        public AABB ColliderBox { get; private set; }

        // Transform
        public TranformComponent Transform { get; private set; }

        public void Ctor() {
            inputCom = new PaddleInputComponent();
            FsmCom = new PaddleFSMComponent();
            RB = new Rigidbody2DComponent();
            Transform = new TranformComponent();
        }

        public void Inject() {
            Transform.Inject(this);
        }

        // Base Info
        public void PlayerIndex_Set(int id) {
            PlayerIndex = id;
        }

        // Pos
        public void Pos_SetPos(Vector2 pos) {
            Transform.Pos = pos;
        }

        // Attr
        public float Attr_GetMoveSpeed() {
            return Mathf.Clamp(MoveSpeed, 0, MoveSpeedMax);
        }

        public void Attr_SetMoveSpeed(float speed) {
            MoveSpeed = speed;
        }

        public void Attr_SetMoveSpeedMax(float speed) {
            MoveSpeedMax = speed;
        }

        public void Attr_SetSize(Vector2 size) {
            this.Size = size;
        }

        // Move
        public void Move_MoveByInput(float dt, AABB constrain) {
            Move_Apply(inputCom.MoveAxis.normalized, Attr_GetMoveSpeed(), dt);
            var pos = Transform.Pos;
            var constrainMin = constrain.Min;
            var constrainMax = constrain.Max;
            var constrainCenter = constrain.Center;

            if (PlayerIndex == 0) {
                pos.x = Mathf.Clamp(pos.x, constrainMin.x + Size.x / 2, constrainMax.x - Size.x / 2);
                pos.y = Mathf.Clamp(pos.y, constrainMin.y + Size.y / 2, constrainCenter.y - Size.y / 2);
            }

            if (PlayerIndex == 1) {
                pos.x = Mathf.Clamp(pos.x, constrainMin.x + Size.x / 2, constrainMax.x - Size.x / 2);
                pos.y = Mathf.Clamp(pos.y, constrainCenter.y + Size.y / 2, constrainMax.y - Size.y / 2);
            }

            Pos_SetPos(pos);
        }

        public void Move_Stop() {
            Move_Apply(Vector2.zero, 0, 0);
        }

        void Move_Apply(Vector2 dir, float moveSpeed, float fixdt) {
            RB.Velocity = dir.normalized * moveSpeed;
        }

        public void TearDown() {
        }

        // Input
        public void Input_SetMoveAxis(Vector2 axis) {
            inputCom.MoveAxis = axis;
        }

        public void Input_Reset() {
            inputCom.MoveAxis = Vector2.zero;
        }

    }

}