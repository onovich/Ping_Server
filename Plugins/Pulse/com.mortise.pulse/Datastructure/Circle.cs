using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class Circle  : ICollider{

        Vector2 center;
        public Vector2 Center => center;

        float radius;
        public float Radius => radius;

        public Circle(Vector2 center, float radius) {
            this.center = center;
            this.radius = radius;
        }

        public bool Contains(Vector2 point) {
            return Vector2.Distance(point, center) <= radius;
        }

    }

}