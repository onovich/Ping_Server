namespace MortiseFrame.Pulse {

    public static class TriggerExitPhase {

        public static void Tick(PhysicalContext context, float dt) {
            while (context.TryDequeueTriggerExit(out var a, out var b)) {
                ApplyTriggerExit(context, a, b);
            }
        }

        static void ApplyTriggerExit(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            var evt = context.EventCenter;

            if (a.IsTrigger || b.IsTrigger) {
                evt.OnTriggerExit(a, b);
            }

            if (a.IsTrigger && b.IsTrigger) {
                return;
            }

            if (context.CollisionContact_Remove(a, b)) {
                context.EnqueueCollisionExit(a, b);
            }
        }

    }

}