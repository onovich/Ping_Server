namespace MortiseFrame.Pulse {

    public static class CollisionExitPhase {

        public static void Tick(PhysicalContext context, float dt) {
            while (context.TryDequeueCollisionExit(out var a, out var b)) {
                ApplyCollisionExit(context, a, b);
            }
        }

        static void ApplyCollisionExit(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            if (a.IsTrigger || b.IsTrigger) {
                return;
            }
            var evt = context.EventCenter;
            evt.OnCollisionExit(a, b);
        }

    }

}