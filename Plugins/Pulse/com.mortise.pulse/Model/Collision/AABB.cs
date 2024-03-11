using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public struct AABB {

        FVector2 min;
        public FVector2 Min => min;

        FVector2 max;
        public FVector2 Max => max;

        public AABB(FVector2 center, FVector2 size) {
            min = center - size / 2;
            max = center + size / 2;
        }

        public FVector2 Center => GetCenter();
        public FVector2 Size => GetSize();
        public FVector2[] Axis => GetAxis();

        public bool Contains(FVector2 point) {
            return point.x >= Min.x && point.x <= Max.x && point.y >= Min.y && point.y <= Max.y;
        }

        FVector2 GetCenter() {
            return (Min + Max) / 2;
        }

        FVector2 GetSize() {
            return Max - Min;
        }

        float GetHeight() {
            return Max.y - Min.y;
        }

        float GetWidth() {
            return Max.x - Min.x;
        }

        public void SetCenter(FVector2 center) {
            var size = GetSize();
            min = center - size / 2;
            max = center + size / 2;
        }

        public (float Min, float Max) ProjectOntoAxis(FVector2 axis) {
            FVector2[] vertices = new FVector2[4];
            vertices[0] = this.Min;
            vertices[1] = new FVector2(this.Min.x, this.Max.y);
            vertices[2] = this.Max;
            vertices[3] = new FVector2(this.Max.x, this.Min.y);

            float Min = FVector2.Dot(vertices[0], axis);
            float Max = Min;

            for (int i = 1; i < vertices.Length; i++) {
                float dotProduct = FVector2.Dot(vertices[i], axis);
                if (dotProduct < Min) {
                    Min = dotProduct;
                } else if (dotProduct > Max) {
                    Max = dotProduct;
                }
            }

            return (Min, Max);
        }

        FVector2[] GetAxis() {
            FVector2[] axes = new FVector2[2];
            axes[0] = new FVector2(1, 0);
            axes[1] = new FVector2(0, 1);
            return axes;
        }

    }

}