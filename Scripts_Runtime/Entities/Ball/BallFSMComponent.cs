using MortiseFrame.Abacus;

namespace Ping.Server {

    public class BallFSMComponent {

        public BallFSMStatus status;

        public bool idle_isEntering;

        public bool moving_isEntering;

        public bool dead_isEntering;
        public int dead_gatePlayerIndex;

        public Vector2 movingDir;

        public BallFSMComponent() { }

        public void EnterIdle() {
            status = BallFSMStatus.Idle;
            idle_isEntering = true;
        }

        public void EnterMoving(Vector2 movingDir) {
            status = BallFSMStatus.Moving;
            moving_isEntering = true;
            this.movingDir = movingDir;
        }

        public void EnterDead(int gatePlayerIndex) {
            status = BallFSMStatus.Dead;
            dead_isEntering = true;
            dead_gatePlayerIndex = gatePlayerIndex;
        }

    }

}