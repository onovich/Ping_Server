using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public static class InsersectPF {

        public static bool IsIntersectAABB_AABB(AABB a, AABB b, float epsilon) {

            var res = ((b.Max.y - a.Min.y > epsilon)
                && (a.Max.y - b.Min.y > epsilon)
                && (b.Max.x - a.Min.x > epsilon)
                && (a.Max.x - b.Min.x > epsilon));

            return res;

        }

        public static bool IsIntersectAABB_Circle(AABB aabb, Circle circle, float epsilon) {

            var i = aabb.Center - circle.Center;
            var v = Vector2.Max(i, -i);
            var diff = Vector2.Max(v - aabb.Size * 0.5f, Vector2.zero);
            var res = circle.Radius * circle.Radius - diff.sqrMagnitude > epsilon;

            return res;

        }

        public static bool IsIntersectAABB_OBB(AABB aabb, OBB obb, float epsilon) {

            Vector2[] obbAxis = obb.Axis;
            Vector2[] aabbAxis = aabb.Axis;

            for (int i = 0; i < obbAxis.Length; i++) {
                (float Min, float Max) projectionA = aabb.ProjectOntoAxis(obbAxis[i]);
                (float Min, float Max) projectionB = obb.ProjectOntoAxis(obbAxis[i]);

                if (!ProjectionsOverlap(projectionA, projectionB, epsilon)) {
                    return false;
                }
            }

            for (int i = 0; i < aabbAxis.Length; i++) {
                (float Min, float Max) projectionA = aabb.ProjectOntoAxis(aabbAxis[i]);
                (float Min, float Max) projectionB = obb.ProjectOntoAxis(aabbAxis[i]);

                if (!ProjectionsOverlap(projectionA, projectionB, epsilon)) {
                    return false;
                }
            }

            return true;

        }

        static bool ProjectionsOverlap((float Min, float Max) a, (float Min, float Max) b, float epsilon) {

            var res = b.Max - a.Min > epsilon && a.Max - b.Min > epsilon;
            return res;

        }

    }

}