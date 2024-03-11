using MortiseFrame.Abacus;

namespace Ping.Server {

    public class BallFSMComponent {

        public BallFSMStatus Status { get; private set; }

        public bool Idle_isEntering { get; set; }

        public bool Moving_isEntering { get; set; }

        public bool Dead_isEntering { get; set; }
        public int Dead_gatePlayerIndex { get; set; }

        public FVector2 movingDir;

        public BallFSMComponent() { }

        public void EnterIdle() {
            Status = BallFSMStatus.Idle;
            Idle_isEntering = true;
        }

        public void EnterMoving(FVector2 movingDir) {
            Status = BallFSMStatus.Moving;
            Moving_isEntering = true;
            this.movingDir = movingDir;
        }

        public void EnterDead(int gatePlayerIndex) {
            Status = BallFSMStatus.Dead;
            Dead_isEntering = true;
            Dead_gatePlayerIndex = gatePlayerIndex;
        }

    }

}