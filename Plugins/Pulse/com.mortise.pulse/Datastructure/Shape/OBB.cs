using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class OBB {

        public Vector2 AxisX { get; private set; }
        public Vector2 AxisY { get; private set; }

        public Vector2 Center { get; private set; }
        public Vector2 Size { get; private set; }

        public float RadAngle { get; private set; }
        public Vector2[] Vertices { get; private set; }

        public OBB(Vector2 center, Vector2 size, float radAngle) {

            Center = center;
            Size = size;
            RadAngle = radAngle;
            AxisX = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
            AxisY = new Vector2(-AxisX.y, AxisX.x);
            AxisX.Normalize();
            AxisY.Normalize();
            var vertices = new Vector2[4];
            vertices[0] = center + AxisX * size.x * 0.5f + AxisY * size.y * 0.5f;
            vertices[1] = center - AxisX * size.x * 0.5f + AxisY * size.y * 0.5f;
            vertices[2] = center - AxisX * size.x * 0.5f - AxisY * size.y * 0.5f;
            vertices[3] = center + AxisX * size.x * 0.5f - AxisY * size.y * 0.5f;
            this.Vertices = vertices;

        }

        public bool Contains(Vector2 point) {
            var localPoint = point - Center;
            var localX = Vector2.Dot(localPoint, AxisX);
            var localY = Vector2.Dot(localPoint, AxisY);
            return Mathf.Abs(localX) <= Size.x * 0.5f && Mathf.Abs(localY) <= Size.y * 0.5f;
        }

        public (float Min, float Max) ProjectOntoAxis(Vector2 axis) {
            float dotProduct = Vector2.Dot(this.Vertices[0], axis);
            float min = dotProduct;
            float max = dotProduct;

            for (int i = 1; i < this.Vertices.Length; i++) {
                dotProduct = Vector2.Dot(this.Vertices[i], axis);
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