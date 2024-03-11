using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class OBB {

        public FVector2 AxisX { get; private set; }
        public FVector2 AxisY { get; private set; }

        public FVector2 Center { get; private set; }
        public FVector2 Size { get; private set; }

        public float RadAngle { get; private set; }
        public FVector2[] Vertices { get; private set; }

        public OBB(FVector2 center, FVector2 size, float radAngle) {

            Center = center;
            Size = size;
            RadAngle = radAngle;
            AxisX = new FVector2(FMath.Cos(radAngle), FMath.Sin(radAngle));
            AxisY = new FVector2(-AxisX.y, AxisX.x);
            AxisX.Normalize();
            AxisY.Normalize();
            var vertices = new FVector2[4];
            vertices[0] = center + AxisX * size.x * 0.5f + AxisY * size.y * 0.5f;
            vertices[1] = center - AxisX * size.x * 0.5f + AxisY * size.y * 0.5f;
            vertices[2] = center - AxisX * size.x * 0.5f - AxisY * size.y * 0.5f;
            vertices[3] = center + AxisX * size.x * 0.5f - AxisY * size.y * 0.5f;
            this.Vertices = vertices;

        }

        public bool Contains(FVector2 point) {
            var localPoint = point - Center;
            var localX = FVector2.Dot(localPoint, AxisX);
            var localY = FVector2.Dot(localPoint, AxisY);
            return FMath.Abs(localX) <= Size.x * 0.5f && FMath.Abs(localY) <= Size.y * 0.5f;
        }

        public (float Min, float Max) ProjectOntoAxis(FVector2 axis) {
            float dotProduct = FVector2.Dot(this.Vertices[0], axis);
            float min = dotProduct;
            float max = dotProduct;

            for (int i = 1; i < this.Vertices.Length; i++) {
                dotProduct = FVector2.Dot(this.Vertices[i], axis);
                if (dotProduct < min) {
                    min = dotProduct;
                } else if (dotProduct > max) {
                    max = dotProduct;
                }
            }

            return (min, max);
        }

    }

}