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
        public RigidbodyEntity RB { get; private set; }

        public void Ctor() {
            FSMCom = new BallFSMComponent();
        }

        // Pos
        public void Pos_SetPos(FVector2 pos) {
            RB.SetPos(pos);
        }

        public FVector2 Pos_GetDirection() {
            return FSMCom.movingDir;
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

        public void Attr_SetRadius(float Radius) {
            this.Radius = Radius;
        }

        // Move
        public void Move_SetVelocity_Once(FVector2 dir) {
            if (RB.Velocity != FVector2.zero) {
                return;
            }
            var v = dir.Normalize() * MoveSpeed;
            RB.SetVelocity(v);
        }

        public void Move_ByDir(FVector2 dir, float dt) {
            Move_Apply(dir, Attr_GetMoveSpeed(), dt);
        }

        public void Move_Stop() {
            Move_Apply(FVector2.zero, 0, 0);
        }

        void Move_Apply(FVector2 dir, float MoveSpeed, float fixdt) {
            var v = dir.Normalize() * MoveSpeed;
            RB.SetVelocity(v);
        }

        // FSM
        public BallFSMStatus FSM_GetStatus() {
            return FSMCom.Status;
        }

        public BallFSMComponent FSM_GetComponent() {
            return FSMCom;
        }

        public void FSM_SetMovingDir(FVector2 dir) {
            FSMCom.movingDir = dir;
        }

        // Physics
        public void RB_Set(RigidbodyEntity RB) {
            this.RB = RB;
        }

        public void TearDown() {
        }

    }

}