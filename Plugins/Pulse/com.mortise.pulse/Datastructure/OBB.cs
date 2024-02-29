using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class OBB : ICollider {

        Vector2 axisX;
        Vector2 axisY;
        Vector2 center;
        Vector2 size;
        Vector2[] vertices;
        float radAngle;

        public float RadAngle => radAngle;
        public Vector2 Center => center;
        public Vector2 Size => size;
        public Vector2[] Axis => GetAxis();
        public Vector2[] Vertices => vertices;

        public OBB(Vector2 center, Vector2 size, float radAngle) {

            this.center = center;
            this.size = size;
            this.radAngle = radAngle;
            this.axisX = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
            this.axisY = new Vector2(-axisX.y, axisX.x);
            this.axisX.Normalize();
            this.axisY.Normalize();
            var vertices = new Vector2[4];
            vertices[0] = center + axisX * size.x * 0.5f + axisY * size.y * 0.5f;
            vertices[1] = center - axisX * size.x * 0.5f + axisY * size.y * 0.5f;
            vertices[2] = center - axisX * size.x * 0.5f - axisY * size.y * 0.5f;
            vertices[3] = center + axisX * size.x * 0.5f - axisY * size.y * 0.5f;
            this.vertices = vertices;

        }

        public bool Contains(Vector2 point) {
            var localPoint = point - center;
            var localX = Vector2.Dot(localPoint, axisX);
            var localY = Vector2.Dot(localPoint, axisY);
            return Mathf.Abs(localX) <= size.x * 0.5f && Mathf.Abs(localY) <= size.y * 0.5f;
        }

        Vector2[] GetAxis() {
            Vector2[] axes = new Vector2[2];
            axes[0] = this.axisX;
            axes[1] = this.axisY;
            return axes;
        }

        public (float Min, float Max) ProjectOntoAxis(Vector2 axis) {
            float dotProduct = Vector2.Dot(this.vertices[0], axis);
            float min = dotProduct;
            float max = dotProduct;

            for (int i = 1; i < this.vertices.Length; i++) {
                dotProduct = Vector2.Dot(this.vertices[i], axis);
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