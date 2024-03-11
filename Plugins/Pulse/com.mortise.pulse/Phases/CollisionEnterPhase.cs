namespace MortiseFrame.Pulse {

    public static class CollisionEnterPhase {

        public static void Tick(PhysicalContext context, float dt) {
            while (context.TryDequeueCollisionEnter(out var a, out var b)) {
                ApplyCollisionEnter(context, a, b);
            }
        }

        static void ApplyCollisionEnter(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            if (a.IsTrigger || b.IsTrigger) {
                return;
            }
            var evt = context.EventCenter;
            evt.OnCollisionEnter(a, b);
        }

    }

}