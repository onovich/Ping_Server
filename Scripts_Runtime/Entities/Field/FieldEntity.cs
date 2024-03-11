using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class FieldEntity {

        List<WallEntity> walls;
        List<GateEntity> gates;

        public void Ctor() {
            walls = new List<WallEntity>(2);
            gates = new List<GateEntity>(2);
        }

        public void Wall_Add(WallEntity wall) {
            walls.Add(wall);
        }

        public void Gate_Add(GateEntity gate) {
            gates.Add(gate);
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