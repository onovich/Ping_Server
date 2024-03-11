using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class FieldEntity {

        AABB bound;
        WallEntity[] walls;
        GateEntity[] gates;

        public void Ctor() {

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