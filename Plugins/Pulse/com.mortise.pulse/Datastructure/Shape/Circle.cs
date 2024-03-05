using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class Circle {

        public Vector2 Center { get; private set; }
        public float Radius { get; private set; }

        public Circle(Vector2 center, float radius) {
            Center = center;
            Radius = radius;
        }

        public bool Contains(Vector2 point) {
            return Vector2.Distance(point, Center) <= Radius;
        }

    }

}