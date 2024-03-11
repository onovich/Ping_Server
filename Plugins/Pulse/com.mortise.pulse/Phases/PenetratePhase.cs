using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public static class PenetratePhase {

        public static void Tick(PhysicalContext context, float dt) {

            context.CollisionContact_ForEach((kv) => {
                var a = kv.Item2;
                var b = kv.Item3;
                ApplyRestore(context, a, b);
                ApplyCollisionStay(context, a, b);
            });

        }

        static void ApplyRestore(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            if (a.IsStatic && !b.IsStatic) {
                RestoreDynamic_Static(context, b, a);
            }

            if (!a.IsStatic && b.IsStatic) {
                RestoreDynamic_Static(context, a, b);
            }

            if (!a.IsStatic && !b.IsStatic) {
                RestoreDynamic_Dynamic(context, a, b);
            }
        }

        static void RestoreDynamic_Static(PhysicalContext context, RigidbodyEntity d, RigidbodyEntity s) {

            var overlapDepth = PenetratePF.PenetrateDepthRB_RB(d, s);
            if (overlapDepth == FVector2.zero) {
                return;
            }
            d.Transform.SetPos(d.Transform.Pos + overlapDepth);

            ApplyBounce_Dynamic_Static(context, d, s, overlapDepth);

        }

        static void RestoreDynamic_Dynamic(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {

            var overlapDepth = PenetratePF.PenetrateDepthRB_RB(a, b);
            if (overlapDepth == FVector2.zero) {
                return;
            }

            if (a.Mass >= b.Mass) {
                b.Transform.SetPos(b.Transform.Pos - overlapDepth);
                ApplyBouce_Dynamic_Dynamic(context, b, a, -overlapDepth);
            }
            if (a.Mass <= b.Mass) {
                a.Transform.SetPos(a.Transform.Pos + overlapDepth);
                ApplyBouce_Dynamic_Dynamic(context, a, b, overlapDepth);
            }

        }

        static void ApplyBounce_Dynamic_Static(PhysicalContext context, RigidbodyEntity d, RigidbodyEntity s, FVector2 overlapDepth) {
            context.EnqueueCollisionEnter(d, s);

            if (d.Restitution > 0 && d.Velocity != FVector2.zero) {
                var normalDir = overlapDepth.normalized;
                var dBounceVelocity = FVector2.Reflect(d.Velocity, normalDir) * d.Restitution;
                d.SetVelocity(dBounceVelocity);
            }
        }

        static void ApplyBouce_Dynamic_Dynamic(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b, FVector2 overlapDepth) {
            context.EnqueueCollisionEnter(a, b);

            if (a.Restitution > 0 && a.Velocity != FVector2.zero) {
                var normalDir = overlapDepth.normalized;
                var aBounceVelocity = FVector2.Reflect(a.Velocity, normalDir) * a.Restitution;
                a.SetVelocity(aBounceVelocity);
            }

        }

        static void ApplyCollisionStay(PhysicalContext context, RigidbodyEntity a, RigidbodyEntity b) {
            if (context.CollisionContact_Contains(a, b)) {
                context.EnqueueCollisionStay(a, b);
            }
        }

    }

}