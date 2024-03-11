using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public static class ForcePhase {

        public static void Tick(PhysicalContext context, float dt) {

            context.Rigidbody_ForEach(rb => {
                ApplyGravity(context, rb, dt);
                ApplyVelocity(context, rb, dt);
            });

        }

        static void ApplyGravity(PhysicalContext context, RigidbodyEntity rb, float dt) {
            if (rb.IsStatic) {
                return;
            }
            if (rb.Mass == 0) {
                return;
            }
            var velocity = rb.Velocity + context.Gravity * rb.Mass * dt;
            rb.SetVelocity(velocity);
        }

        static void ApplyVelocity(PhysicalContext context, RigidbodyEntity rb, float dt) {
            if (rb.IsStatic) {
                return;
            }
            if (rb.Velocity == FVector2.zero) {
                return;
            }
            var pos = rb.Transform.Pos + rb.Velocity * dt;
            rb.SetPos(pos);
        }

    }

}