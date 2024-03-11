namespace MortiseFrame.Pulse {

    public static class TriggerEnterPhase {

        public static void Tick(PhysicalContext context, float dt) {
            while (context.TryDequeueTriggerEnter(out var a, out var b)) {
                ApplyTriggerEnter(context, a, b);
            }
        }

        static void ApplyTriggerEnter(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            var evt = context.EventCenter;

            if (a.IsTrigger || b.IsTrigger) {
                evt.OnTriggerEnter(a, b);
            }

            if (a.IsTrigger && b.IsTrigger) {
                return;
            }

            if (!context.CollisionContact_Contains(a, b)) {
                context.CollisionContact_Add(a, b);
            }
        }

    }

}