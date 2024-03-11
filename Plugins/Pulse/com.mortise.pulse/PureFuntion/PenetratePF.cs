using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public static class PenetratePF {

        public static FVector2 PenetrateDepthRB_RB(RigidbodyEntity a, RigidbodyEntity b) {
            IShape aShape = a.Shape;
            IShape bShape = b.Shape;

            // Circle & Circle
            CircleShape aCircle = aShape as CircleShape;
            CircleShape bCircle = bShape as CircleShape;
            if (aCircle != null && bCircle != null) {
                return PenetrateDepthCircle_Circle(a.Transform, aCircle, b.Transform, bCircle);
            }

            // Box & Box
            BoxShape aBox = aShape as BoxShape;
            BoxShape bBox = bShape as BoxShape;
            if (aBox != null && bBox != null) {
                return PenetrateDepthBox_Box(a.Transform, aBox, b.Transform, bBox);
            }

            // Circle & Box
            if (aBox != null && bCircle != null) {
                return -PenetrateDepthCircle_Box(b.Transform, bCircle, a.Transform, aBox);
            }
            if (aCircle != null && bBox != null) {
                return PenetrateDepthCircle_Box(a.Transform, aCircle, b.Transform, bBox);
            }

            return FVector2.zero;
        }

        static FVector2 PenetrateDepthBox_Box(TFComponent aTF, BoxShape aBox, TFComponent bTF, BoxShape bBox) {
            // AABB & AABB
            if (aTF.RadAngle == 0 && bTF.RadAngle == 0) {
                return PenetrateDepthAABB_AABB(aBox.GetAABB(aTF), bBox.GetAABB(bTF));
            }
            // OBB & OBB
            return PenetrateDepthOBB_OBB(aBox.GetOBB(aTF), bBox.GetOBB(bTF));
        }

        static FVector2 PenetrateDepthCircle_Circle(TFComponent aTF, CircleShape a, TFComponent bTF, CircleShape b) {
            FVector2 diff = aTF.Pos - bTF.Pos;
            float distance = diff.magnitude;
            float penetrationDepth = a.Radius + b.Radius - distance;

            if (penetrationDepth > 0) {
                return diff.normalized * penetrationDepth;
            } else {
                return FVector2.zero;
            }
        }

        static FVector2 PenetrateDepthCircle_Box(TFComponent circleTF, CircleShape circle, TFComponent boxTF, BoxShape box) {
            // Circle & AABB
            if (boxTF.RadAngle == 0) {
                return PenetrateDepthAABB_Sphere(box.GetAABB(boxTF), circle.GetSphere(circleTF));
            }
            // Circle & OBB
            return PenetrateDepthOBB_Sphere(box.GetOBB(boxTF), circle.GetSphere(circleTF));
        }

        static FVector2 PenetrateDepthAABB_AABB(AABB a, AABB b) {
            float overlapX = FMath.Min(a.Max.x - b.Min.x, b.Max.x - a.Min.x);
            float overlapY = FMath.Min(a.Max.y - b.Min.y, b.Max.y - a.Min.y);

            if (overlapX <= 0f || overlapY <= 0f) {
                return FVector2.zero;
            }

            float directionX = ((a.Min.x + a.Max.x) / 2) < ((b.Min.x + b.Max.x) / 2) ? -1 : 1;
            float directionY = ((a.Min.y + a.Max.y) / 2) < ((b.Min.y + b.Max.y) / 2) ? -1 : 1;

            if (overlapX < overlapY) {
                return new FVector2(overlapX * directionX, 0);
            } else {
                return new FVector2(0, overlapY * directionY);
            }
        }

        static FVector2 PenetrateDepthOBB_OBB(OBB obb1, OBB obb2) {
            throw new System.Exception($"未实现 OBB x OBB 的碰撞深度计算");
        }

        static FVector2 PenetrateDepthOBB_Sphere(OBB obb, Sphere sphere) {
            throw new System.Exception($"未实现 Circle x OBB 的碰撞深度计算");
        }

        static FVector2 PenetrateDepthAABB_Sphere(AABB aabb, Sphere sphere) {
            FVector2 closestPoint = new FVector2(
                FMath.Max(aabb.Min.x, FMath.Min(sphere.Center.x, aabb.Max.x)),
                FMath.Max(aabb.Min.y, FMath.Min(sphere.Center.y, aabb.Max.y))
            );

            FVector2 directionToSphere = sphere.Center - closestPoint;
            float distanceToSphere = directionToSphere.magnitude;
            float penetrationDepth = sphere.Radius - distanceToSphere;

            if (penetrationDepth > 0) {
                return directionToSphere.normalized * penetrationDepth;
            } else {
                return FVector2.zero;
            }
        }

    }

}