using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public static class InsersectPF {

        public static bool IsIntersectAABB_AABB(AABB a, AABB b, float epsilon) {
            return (b.Max.y - a.Min.y > epsilon) && (a.Max.y - b.Min.y > epsilon) &&
                   (b.Max.x - a.Min.x > epsilon) && (a.Max.x - b.Min.x > epsilon);
        }

        public static bool IsIntersectAABB_Circle(AABB aabb, Circle circle, float epsilon) {
            Vector2 clampedCenter = Vector2.Max(aabb.Min, Vector2.Min(aabb.Max, circle.Center));
            float distanceSquared = (clampedCenter - circle.Center).sqrMagnitude;
            return distanceSquared <= (circle.Radius * circle.Radius + epsilon);
        }

        public static bool IsIntersectAABB_OBB(AABB aabb, OBB obb, float epsilon) {
            Vector2 aabbAxisX = new Vector2(1, 0); // 全局X轴
            Vector2 aabbAxisY = new Vector2(0, 1); // 全局Y轴

            Vector2[] axesToTest = { aabbAxisX, aabbAxisY, obb.AxisX, obb.AxisY };

            foreach (var axis in axesToTest) {
                var aabbProjection = aabb.ProjectOntoAxis(axis);
                var obbProjection = obb.ProjectOntoAxis(axis);

                if (!ProjectionsOverlap(aabbProjection, obbProjection, epsilon)) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsIntersectCircle_Circle(Circle c1, Circle c2, float epsilon) {

            float centerDistance = Vector2.Distance(c1.Center, c2.Center);
            float radiiSum = c1.Radius + c2.Radius;
            return centerDistance <= radiiSum + epsilon;

        }

        public static bool IsIntersectOBB_OBB(OBB obb1, OBB obb2, float epsilon) {
            Vector2[] axesToTest = { obb1.AxisX, obb1.AxisY, obb2.AxisX, obb2.AxisY };

            foreach (var axis in axesToTest) {
                var projection1 = obb1.ProjectOntoAxis(axis);
                var projection2 = obb2.ProjectOntoAxis(axis);

                if (!ProjectionsOverlap(projection1, projection2, epsilon)) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsIntersectCircle_OBB(Circle circle, OBB obb, float epsilon) {
            Vector2 localCircleCenter = circle.Center - obb.Center;
            Vector2 closestPointOnOBB = obb.Center;

            foreach (var axis in new Vector2[] { obb.AxisX, obb.AxisY }) {
                float distanceOnAxis = Vector2.Dot(localCircleCenter, axis);
                float halfSizeOnAxis = axis == obb.AxisX ? obb.Size.x * 0.5f : obb.Size.y * 0.5f;

                float clampedDistance = Mathf.Clamp(distanceOnAxis, -halfSizeOnAxis, halfSizeOnAxis);
                closestPointOnOBB += axis * clampedDistance;
            }

            return Vector2.Distance(circle.Center, closestPointOnOBB) <= circle.Radius + epsilon;
        }

        static bool ProjectionsOverlap((float Min, float Max) a, (float Min, float Max) b, float epsilon) {

            var res = b.Max - a.Min > epsilon && a.Max - b.Min > epsilon;
            return res;

        }

        public static bool OverlapCircle_AABB(AABB aabb, Circle circle, float epsilon, out Hits hits) {
            bool isCollided = IsIntersectAABB_Circle(aabb, circle, epsilon);

            if (isCollided) {
                Vector2 clampedCenter = Vector2.Max(aabb.Min, Vector2.Min(aabb.Max, circle.Center));
                Vector2 hitDirection = (circle.Center - clampedCenter).normalized;
                Vector2 hitPoint = clampedCenter;

                if (clampedCenter != circle.Center) {
                    hitPoint = circle.Center - hitDirection * circle.Radius;
                }

                hits = new Hits(hitPoint, hitDirection);
            } else {
                hits = default;
            }

            return isCollided;
        }

    }

}