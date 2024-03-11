using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public static class IntersectPF {

        public static bool IsIntersectRB_RB(RigidbodyEntity a, RigidbodyEntity b, float epsilon) {
            IShape aShape = a.Shape;
            IShape bShape = b.Shape;

            // Circle & Circle
            CircleShape aCircle = aShape as CircleShape;
            CircleShape bCircleB = bShape as CircleShape;
            if (aCircle != null && bCircleB != null) {
                return IsIntersectCircle_Circle(a.Transform, aCircle, b.Transform, bCircleB, epsilon);
            }

            // Box & Box
            BoxShape aBox = aShape as BoxShape;
            BoxShape bBox = bShape as BoxShape;
            if (aBox != null && bBox != null) {
                return IsIntersectBox_Box(a.Transform, aBox, b.Transform, bBox, epsilon);
            }

            // Circle & Box
            if (aBox != null && bCircleB != null) {
                return IsIntersectCircle_Box(b.Transform, bCircleB, a.Transform, aBox, epsilon);
            }
            if (aCircle != null && bBox != null) {
                return IsIntersectCircle_Box(a.Transform, aCircle, b.Transform, bBox, epsilon);
            }

            return false;
        }

        static bool IsIntersectBox_Box(TFComponent aTF, BoxShape aBox, TFComponent bTF, BoxShape bBox, float epsilon) {
            // AABB & AABB
            if (aTF.RadAngle == 0 && bTF.RadAngle == 0) {
                return IsIntersectAABB_AABB(aBox.GetAABB(aTF), bBox.GetAABB(bTF), epsilon);
            }
            // OBB & OBB
            return IsIntersectOBB_OBB(aBox.GetOBB(aTF), bBox.GetOBB(bTF), epsilon);
        }

        static bool IsIntersectCircle_Circle(TFComponent aTF, CircleShape a, TFComponent bTF, CircleShape b, float epsilon) {
            var diff = aTF.Pos - bTF.Pos;
            var distanceSquared = diff.sqrMagnitude;
            var radiusSum = a.Radius + b.Radius;
            return radiusSum * radiusSum - distanceSquared > epsilon;
        }

        static bool IsIntersectCircle_Box(TFComponent circleTF, CircleShape circle, TFComponent boxTF, BoxShape box, float epsilon) {
            // Circle & AABB
            if (boxTF.RadAngle == 0) {
                return IsIntersectAABB_Sphere(box.GetAABB(boxTF), circle.GetSphere(circleTF), epsilon);
            }
            // Circle & OBB
            return IsIntersectOBB_Sphere(circle.GetSphere(circleTF), box.GetOBB(boxTF), epsilon);
        }

        static bool IsIntersectAABB_AABB(AABB a, AABB b, float epsilon) {
            return (b.Max.y - a.Min.y > epsilon) && (a.Max.y - b.Min.y > epsilon) &&
                   (b.Max.x - a.Min.x > epsilon) && (a.Max.x - b.Min.x > epsilon);
        }

        static bool IsIntersectAABB_Sphere(AABB aabb, Sphere sphere, float epsilon) {
            FVector2 clampedCenter = FVector2.Max(aabb.Min, FVector2.Min(aabb.Max, sphere.Center));
            float distanceSquared = (clampedCenter - sphere.Center).sqrMagnitude;
            return distanceSquared <= (sphere.Radius * sphere.Radius + epsilon);
        }

        static bool IsIntersectOBB_OBB(OBB obb1, OBB obb2, float epsilon) {
            bool notInAX = NotIntersectInAxis_OBB(obb1.Vertices, obb2.Vertices, obb1.AxisX, epsilon);
            bool notInAY = NotIntersectInAxis_OBB(obb1.Vertices, obb2.Vertices, obb1.AxisY, epsilon);
            bool notInBX = NotIntersectInAxis_OBB(obb1.Vertices, obb2.Vertices, obb2.AxisX, epsilon);
            bool notInBY = NotIntersectInAxis_OBB(obb1.Vertices, obb2.Vertices, obb2.AxisY, epsilon);
            return !(notInAX || notInAY || notInBX || notInBY);
        }

        static bool NotIntersectInAxis_OBB(FVector2[] verticesA, FVector2[] verticesB, FVector2 axis, float epsilon) {
            FVector2 rangeA = Project_OBB(verticesA, axis);
            FVector2 rangeB = Project_OBB(verticesB, axis);
            return (rangeA.x - rangeB.y > epsilon) || (rangeB.x - rangeA.y > epsilon);
        }

        static FVector2 Project_OBB(FVector2[] vertices, FVector2 axis) {
            var range = new FVector2(float.MaxValue, float.MinValue);
            foreach (var vertex in vertices) {
                var projection = FVector2.Dot(vertex, axis);
                range.x = FMath.Min(range.x, projection);
                range.y = FMath.Max(range.y, projection);
            }
            return range;
        }

        static bool IsIntersectOBB_Sphere(Sphere sphere, OBB obb, float epsilon) {
            FVector2 localSphereCenter = sphere.Center - obb.Center;
            FVector2 closestPointOnOBB = obb.Center;

            foreach (var axis in new FVector2[] { obb.AxisX, obb.AxisY }) {
                float distanceOnAxis = FVector2.Dot(localSphereCenter, axis);
                float halfSizeOnAxis = axis == obb.AxisX ? obb.Size.x * 0.5f : obb.Size.y * 0.5f;

                float clampedDistance = FMath.Clamp(distanceOnAxis, -halfSizeOnAxis, halfSizeOnAxis);
                closestPointOnOBB += axis * clampedDistance;
            }

            return FVector2.Distance(sphere.Center, closestPointOnOBB) <= sphere.Radius + epsilon;
        }

    }

}