using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class Sphere {

        FVector2 center;
        public FVector2 Center => center;

        float radius;
        public float Radius => radius;

        public Sphere(FVector2 center, float radius) {
            this.center = center;
            this.radius = radius;
        }

        public bool Contains(FVector2 point) {
            var diff = point - center;
            if (diff.SqrMagnitude() <= radius * radius) {
                return true;
            }
            return false;
        }

    }

}