using MortiseFrame.Pulse;
using MortiseFrame.Abacus;

namespace Ping.Server {

    public class BallEntity : IEntity {

        // Base Info
        public EntityType EntityType => EntityType.Ball;

        // Attr
        public float MoveSpeed { get; private set; }
        public float MoveSpeedMax { get; private set; }
        public float Radius { get; private set; }

        // FSM
        public BallFSMComponent FSMCom { get; private set; }

        // Physics
        public Rigidbody2DComponent RB { get; private set; }

        // Collider
        public Circle collider { get; private set; }

        // Transform
        public TranformComponent Transform { get; private set; }

        public void Ctor() {
            FSMCom = new BallFSMComponent();
            Transform = new TranformComponent();
            RB = new Rigidbody2DComponent();
        }

        public void Inject() {
            Transform.Inject(this);
        }

        // Pos
        public void Pos_SetPos(Vector2 pos) {
            Transform.Pos = pos;
        }

        public Vector2 Pos_GetDirection() {
            return FSMCom.movingDir;
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

        public void Attr_SetRadius(float Radius) {
            this.Radius = Radius;
        }

        // Move
        public void Move_ByDir(Vector2 dir, float dt) {
            PLog.LogAssert(dir != Vector2.zero, "BallEntity.Move_ByDir: dir is zero");
            PLog.LogAssert(Attr_GetMoveSpeed() > 0, "BallEntity.Move_ByDir: MoveSpeed is zero");
            Move_Apply(dir, Attr_GetMoveSpeed(), dt);
        }

        public void Move_Stop() {
            Move_Apply(Vector2.zero, 0, 0);
        }

        void Move_Apply(Vector2 dir, float MoveSpeed, float fixdt) {
            RB.Velocity = dir.Normalize() * MoveSpeed;
        }

        // FSM
        public BallFSMStatus FSM_GetStatus() {
            return FSMCom.Status;
        }

        public BallFSMComponent FSM_GetComponent() {
            return FSMCom;
        }

        public void FSM_SetMovingDir(Vector2 dir) {
            FSMCom.movingDir = dir;
        }

        // Physics
        public Rigidbody2DComponent Rigidbody2D_Get() {
            return RB;
        }

        public void TearDown() {
        }

    }

}