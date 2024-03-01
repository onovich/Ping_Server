using System;
using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class PaddleEntity : IEntity {

        // Base Info
        int playerIndex;
        EntityType IEntity.EntityType => EntityType.Paddle;

        // Attr
        float moveSpeed;
        float moveSpeedMax;
        Vector2 size;

        // FSM
        PaddleFSMComponent fsmCom;

        // Input
        PaddleInputComponent inputCom;

        // Physics
        Rigidbody2DComponent rb;
        AABB colliderBox;

        // Transform
        TranformComponent transform;

        public void Ctor() {
            inputCom = new PaddleInputComponent();
            fsmCom = new PaddleFSMComponent();
            rb = new Rigidbody2DComponent();
            transform = new TranformComponent();
        }

        public void Inject() {
            transform.Inject(this);
            rb.Inject(transform, colliderBox);
        }

        // Base Info
        public void SetPlayerIndex(int id) {
            playerIndex = id;
        }

        public int GetPlayerIndex() {
            return playerIndex;
        }

        // Pos
        public void Pos_SetPos(Vector2 pos) {
            transform.position = pos;
        }

        public Vector2 Pos_GetPos() {
            return transform.position;
        }

        public Vector2 Pos_GetVolecity() {
            return rb.velocity;
        }

        // Attr
        public float Attr_GetMoveSpeed() {
            return Mathf.Clamp(moveSpeed, 0, moveSpeedMax);
        }

        public void Attr_SetMoveSpeed(float speed) {
            moveSpeed = speed;
        }

        public float Attr_GetMoveSpeedMax() {
            return moveSpeedMax;
        }

        public void Attr_SetMoveSpeedMax(float speed) {
            moveSpeedMax = speed;
        }

        public void Attr_SetSize(Vector2 size) {
            this.size = size;
        }

        // Move
        public void Move_Move(float dt, AABB constrain) {
            Move_Apply(inputCom.moveAxis.normalized, Attr_GetMoveSpeed(), dt);
            var pos = Pos_GetPos();
            var constrainMin = constrain.Min;
            var constrainMax = constrain.Max;
            var constrainCenter = constrain.Center;
            pos.x = Mathf.Clamp(pos.x, constrainMin.x + size.x / 2, constrainMax.x - size.x / 2);
            pos.y = Mathf.Clamp(pos.y, constrainMin.y + size.y / 2, constrainCenter.y - size.y / 2);
            Pos_SetPos(pos);
        }

        public Vector2 Move_GetVelocity() {
            return rb.velocity;
        }

        public void Move_Stop() {
            Move_Apply(Vector2.zero, 0, 0);
        }

        void Move_Apply(Vector2 dir, float moveSpeed, float fixdt) {
            rb.velocity = dir.normalized * moveSpeed;
        }

        // FSM
        public PaddleFSMStatus FSM_GetStatus() {
            return fsmCom.status;
        }

        public PaddleFSMComponent FSM_GetComponent() {
            return fsmCom;
        }

        public void TearDown() {
        }

        // Input
        public void Input_SetMoveAxis(Vector2 axis) {
            inputCom.moveAxis = axis;
        }

        public void Input_Reset() {
            inputCom.moveAxis = Vector2.zero;
        }

        // Physics
        public Rigidbody2DComponent Rigidbody2D_Get() {
            return rb;
        }

    }

}