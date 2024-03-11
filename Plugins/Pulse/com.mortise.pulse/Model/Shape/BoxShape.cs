using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class BoxShape : IShape {

        FVector2 size;
        public FVector2 Size => size;

        public BoxShape(FVector2 size) {
            this.size = size;
        }

        public AABB GetAABB(TFComponent transform) {
            return new AABB(transform.Pos, size);
        }

        public OBB GetOBB(TFComponent transform) {
            return new OBB(transform.Pos, size, transform.RadAngle);
        }

        public void SetSize(FVector2 size) {
            this.size = size;
        }

        AABB IShape.GetPruneBounding(TFComponent transform) {
            if (transform.RadAngle == 0) {
                return new AABB(transform.Pos, size);
            } else {
                float maxSide = FMath.Max(size.x, size.y);
                FVector2 targetSize = new FVector2(maxSide, maxSide);
                return new AABB(transform.Pos, targetSize);
            }
        }

    }

}