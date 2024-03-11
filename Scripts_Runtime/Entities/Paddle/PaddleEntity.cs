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
        public FVector2 Size { get; private set; }

        // FSM
        public PaddleFSMComponent FsmCom { get; private set; }

        // Input
        PaddleInputComponent inputCom;

        // Physics
        public RigidbodyEntity RB { get; private set; }

        public void Ctor() {
            inputCom = new PaddleInputComponent();
            FsmCom = new PaddleFSMComponent();
        }

        // Base Info
        public void PlayerIndex_Set(int id) {
            PlayerIndex = id;
        }

        // Pos
        public void Pos_SetPos(FVector2 pos) {
            RB.SetPos(pos);
        }

        // Attr
        public float Attr_GetMoveSpeed() {
            return FMath.Clamp(MoveSpeed, 0, MoveSpeedMax);
        }

        public void Attr_SetMoveSpeed(float speed) {
            MoveSpeed = speed;
        }

        public void Attr_SetMoveSpeedMax(float speed) {
            MoveSpeedMax = speed;
        }

        public void Attr_SetSize(FVector2 size) {
            this.Size = size;
        }

        // Move
        public void Move_MoveByInput(float dt, AABB constrain) {
            Move_Apply(inputCom.MoveAxis.normalized, Attr_GetMoveSpeed(), dt);
            var pos = RB.Transform.Pos;
            var constrainMin = constrain.Min;
            var constrainMax = constrain.Max;
            var constrainCenter = constrain.Center;

            if (PlayerIndex == 0) {
                pos.x = FMath.Clamp(pos.x, constrainMin.x + Size.x / 2, constrainMax.x - Size.x / 2);
                pos.y = FMath.Clamp(pos.y, constrainMin.y + Size.y / 2, constrainCenter.y - Size.y / 2);
            }

            if (PlayerIndex == 1) {
                pos.x = FMath.Clamp(pos.x, constrainMin.x + Size.x / 2, constrainMax.x - Size.x / 2);
                pos.y = FMath.Clamp(pos.y, constrainCenter.y + Size.y / 2, constrainMax.y - Size.y / 2);
            }

            Pos_SetPos(pos);
        }

        public void Move_Stop() {
            Move_Apply(FVector2.zero, 0, 0);
        }

        void Move_Apply(FVector2 dir, float moveSpeed, float fixdt) {
            var v = dir.normalized * moveSpeed;
            RB.SetVelocity(v);
        }

        // Physics
        public void RB_Set(RigidbodyEntity RB) {
            this.RB = RB;
        }

        public void TearDown() {
        }

        // Input
        public void Input_SetMoveAxis(FVector2 axis) {
            inputCom.MoveAxis = axis;
        }

        public void Input_Reset() {
            inputCom.MoveAxis = FVector2.zero;
        }

    }

}