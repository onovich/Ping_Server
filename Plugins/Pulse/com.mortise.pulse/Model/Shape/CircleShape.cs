using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class CircleShape : IShape {

        float radius;
        public float Radius => radius;

        public CircleShape(float radius) {
            this.radius = radius;
        }

        public Sphere GetSphere(TFComponent transform) {
            return new Sphere(transform.Pos, radius);
        }

        public bool Contains(FVector2 point) {
            var diff = point;
            if (diff.SqrMagnitude() <= radius * radius) {
                return true;
            }
            return false;
        }

        AABB IShape.GetPruneBounding(TFComponent transform) {
            return new AABB(transform.Pos, new FVector2(radius * 2, radius * 2));
        }

    }

}