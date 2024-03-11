namespace MortiseFrame.Pulse {

    public static class CollisionStayPhase {

        public static void Tick(PhysicalContext context, float dt) {
            while (context.TryDequeueCollisionStay(out var a, out var b)) {
                ApplyCollisionStay(context, a, b);
            }
        }

        static void ApplyCollisionStay(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            if (a.IsTrigger || b.IsTrigger) {
                return;
            }
            var evt = context.EventCenter;
            evt.OnCollisionStay(a, b);
        }

    }

}