using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class AABB : ICollider {

        Vector2 min;
        Vector2 max;

        public Vector2 Min => min;
        public Vector2 Max => max;
        public Vector2 Center => GetCenter();
        public Vector2 Size => GetSize();
        public float Height => GetHeight();
        public float Width => GetWidth();
        public Vector2[] Axis => GetAxis();

        public AABB(Vector2 min, Vector2 max) {
            this.min = min;
            this.max = max;
        }

        public bool Contains(Vector2 point) {
            return point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y;
        }

        Vector2 GetCenter() {
            return (min + max) / 2;
        }

        Vector2 GetSize() {
            return max - min;
        }

        float GetHeight() {
            return max.y - min.y;
        }

        float GetWidth() {
            return max.x - min.x;
        }

        public void SetCenter(Vector2 center) {
            var size = GetSize();
            min = center - size / 2;
            max = center + size / 2;
        }

        public (float Min, float Max) ProjectOntoAxis(Vector2 axis) {
            Vector2[] vertices = new Vector2[4];
            vertices[0] = this.min;
            vertices[1] = new Vector2(this.min.x, this.max.y);
            vertices[2] = this.max;
            vertices[3] = new Vector2(this.max.x, this.min.y);

            float min = Vector2.Dot(vertices[0], axis);
            float max = min;

            for (int i = 1; i < vertices.Length; i++) {
                float dotProduct = Vector2.Dot(vertices[i], axis);
                if (dotProduct < min) {
                    min = dotProduct;
                } else if (dotProduct > max) {
                    max = dotProduct;
                }
            }

            return (min, max);
        }

        Vector2[] GetAxis() {
            Vector2[] axes = new Vector2[2];
            axes[0] = new Vector2(1, 0);
            axes[1] = new Vector2(0, 1);
            return axes;
        }

    }

}