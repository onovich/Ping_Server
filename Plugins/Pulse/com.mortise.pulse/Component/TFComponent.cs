using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class TFComponent {

        // World
        FVector2 pos;
        public FVector2 Pos => pos;

        float radAngle;
        public float RadAngle => radAngle;

        public TFComponent(FVector2 pos) {
            this.pos = pos;
        }

        public void SetPos(FVector2 pos) {
            this.pos = pos;
        }

        public void SetRadAngle(float radAngle) {
            this.radAngle = radAngle;
        }

    }

}