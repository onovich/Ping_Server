namespace MortiseFrame.Pulse {

    public static class TriggerStayPhase {

        public static void Tick(PhysicalContext context, float dt) {
            while (context.TryDequeueTriggerStay(out var a, out var b)) {
                ApplyTriggerStay(context, a, b);
            }
        }

        static void ApplyTriggerStay(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            var evt = context.EventCenter;

            if (a.IsTrigger || b.IsTrigger) {
                evt.OnTriggerStay(a, b);
            }
        }

    }

}