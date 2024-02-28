using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class FieldEntity {

        AABB bound;
        WallEntity[] walls;
        GateEntity[] gates;

        public void Ctor() {

        }

        public void SetBound(Vector2 min, Vector2 max) {
            bound = new AABB(min, max);
        }

        public AABB GetBound() {
            return bound;
        }

        public void TearDown() {
            foreach (var wall in walls) {
                wall.TearDown();
            }
            foreach (var gate in gates) {
                gate.TearDown();
            }
        }

    }

}