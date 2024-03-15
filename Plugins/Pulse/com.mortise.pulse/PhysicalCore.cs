using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class PhysicalCore {

        public PhysicalEventCenter EventCenter => context.EventCenter;
        PhysicalContext context;

        public PhysicalCore() {
            context = new PhysicalContext();
        }

        // Gravity
        public void SetGravity(FVector2 gravity) {
            context.SetGravity(gravity);
        }

        // RB
        public RigidbodyEntity Rigidbody_CreateBox(FVector2 pos, FVector2 size, float degAngle = 0) {
            var rb = PhysicalFactory.CreateBoxRB(pos, size, degAngle);
            Rigidbody_Add(rb);
            return rb;
        }

        public RigidbodyEntity Rigidbody_CreateCircle(FVector2 pos, float radius) {
            var rb = PhysicalFactory.CreateCircleRB(pos, radius);
            Rigidbody_Add(rb);
            return rb;
        }

        public bool Rigidbody_TryGetByID(uint id, out RigidbodyEntity res) {
            return context.Rigidbody_TryGetByID(id, out res);
        }

        void Rigidbody_Add(RigidbodyEntity rb) {
            context.Rigidbody_Add(rb);
        }

        public void Rigidbody_Remove(RigidbodyEntity rb) {
            context.Rigidbody_Remove(rb);
        }

        public int Rigidbody_TakeAll(out RigidbodyEntity[] res) {
            return context.Rigidbody_TakeAll(out res);
        }

        // Ignore
        public void Ignore(uint layerA, uint layerB) {
            context.Ignore_Add(layerA, layerB);
        }

        public void Unignore(uint layerA, uint layerB) {
            context.Ignore_Remove(layerA, layerB);
        }

        public void Tick(float dt) {

            // 重力 阻力 速度
            ForcePhase.Tick(context, dt);

            // 粗筛
            PrunePhase.Tick(context, dt);

            // 触发 Trigger Exit
            TriggerExitPhase.Tick(context, dt);

            // 交叉检测
            IntersectPhase.Tick(context, dt);

            // 触发 Trigger Enter / Stay
            TriggerEnterPhase.Tick(context, dt);
            TriggerStayPhase.Tick(context, dt);

            // 触发 Collision Exit
            CollisionExitPhase.Tick(context, dt);

            // 穿透处理
            PenetratePhase.Tick(context, dt);

            // 触发 Collision Enter / Stay
            CollisionEnterPhase.Tick(context, dt);
            CollisionStayPhase.Tick(context, dt);

        }

        public void Clear() {
            context.Clear();
        }

    }

}