using MortiseFrame.Pulse;
using MortiseFrame.Abacus;

namespace Ping.Server {

    public class BallEntity : IEntity {

        // Base Info
        EntityType IEntity.EntityType => EntityType.Ball;

        // Attr
        float moveSpeed;
        float moveSpeedMax;
        float radius;

        // FSM
        BallFSMComponent fsmCom;

        // Physics
        Rigidbody2DComponent rb;

        // Collider
        Circle collider;

        // Transform
        TranformComponent transform;

        public void Ctor() {
            fsmCom = new BallFSMComponent();
            transform = new TranformComponent();
            rb = new Rigidbody2DComponent();
        }

        public void Inject() {
            transform.Inject(this);
            rb.Inject(transform, collider);
        }

        // Pos
        public void Pos_SetPos(Vector2 pos) {
            transform.position = pos;
        }

        public Vector2 Pos_GetPos() {
            return transform.position;
        }

        public Vector2 Pos_GetDirection() {
            return fsmCom.movingDir;
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

        public void Attr_SetRadius(float radius) {
            this.radius = radius;
        }

        public float Attr_GetRadius() {
            return radius;
        }

        // Move
        public Vector2 Move_GetVelocity() {
            return rb.velocity;
        }

        public void Move_ByDir(Vector2 dir, float dt) {
            PLog.LogAssert(dir != Vector2.zero, "BallEntity.Move_ByDir: dir is zero");
            PLog.LogAssert(Attr_GetMoveSpeed() > 0, "BallEntity.Move_ByDir: moveSpeed is zero");
            Move_Apply(dir, Attr_GetMoveSpeed(), dt);
        }

        public void Move_Stop() {
            Move_Apply(Vector2.zero, 0, 0);
        }

        void Move_Apply(Vector2 dir, float moveSpeed, float fixdt) {
            rb.velocity = dir.Normalize() * moveSpeed;
        }

        // FSM
        public BallFSMStatus FSM_GetStatus() {
            return fsmCom.status;
        }

        public BallFSMComponent FSM_GetComponent() {
            return fsmCom;
        }

        public void FSM_SetMovingDir(Vector2 dir) {
            fsmCom.movingDir = dir;
        }

        // Physics
        public Rigidbody2DComponent Rigidbody2D_Get() {
            return rb;
        }

        public void TearDown() {
        }

    }

}