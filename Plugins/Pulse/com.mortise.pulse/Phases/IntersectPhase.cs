namespace MortiseFrame.Pulse {

    public static class IntersectPhase {

        public static void Tick(PhysicalContext context, float dt) {

            var count = context.Rigidbody_TakeAll(out var entities);
            for (int i = 0; i < count; i++) {
                var a = entities[i];
                for (int j = i + 1; j < count; j++) {
                    var b = entities[j];
                    ApplyIntersection(context, a, b);
                }
            }

        }

        static void ApplyIntersection(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {

            if (context.Ignore_Contains(a.Layer, b.Layer)) {
                return;
            }

            var eventCenter = context.EventCenter;
            if (context.IntersectContact_TryGet(a, b, out var contact)) {
                // Trigger Stay
                context.EnqueueTriggerStay(a, b);
                // Trigger Exit
                TryExit(context, a, b);
            } else {
                // Trigger Enter
                TryEnter(context, a, b);
            }

        }

        static void TryEnter(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            bool isIntersect = IntersectPF.IsIntersectRB_RB(a, b, float.Epsilon);
            if (isIntersect) {
                TriggerEnter(context, a, b);
            }
        }

        static void TriggerEnter(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            context.EnqueueTriggerEnter(a, b);
            context.IntersectContact_Add(a, b);
        }

        static void TryExit(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            bool isIntersect = IntersectPF.IsIntersectRB_RB(a, b, -float.Epsilon);
            if (!isIntersect) {
                TriggerExit(context, a, b);
            }
        }

        static void TriggerExit(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            context.EnqueueTriggerExit(a, b);
            context.IntersectContact_Remove(a, b);
        }

    }

}