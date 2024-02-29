using MortiseFrame.Abacus;

namespace Ping.Server {

    public static class Physics2DInfra {

        public static int CircleCastNonAlloc(Physics2DInfraContext ctx, Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask) {

            // TODO
            return 0;
        }

        public static bool Simulate(Physics2DInfraContext ctx, float step) {
            ctx.Rigidbodies_ForEach(rb => {
                rb.ApplyPhysics(step);
            });
            return true;
        }

    }

}