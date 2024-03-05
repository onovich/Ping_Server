using MortiseFrame.Abacus;

namespace Ping.Server {

    public class Physics2DInfraContext {

        List<Rigidbody2DComponent> rigidbodies;
        Dictionary<Rigidbody2DComponent, TranformComponent> rb2Transform;

        public Physics2DInfraContext() {
            rigidbodies = new List<Rigidbody2DComponent>();
            rb2Transform = new Dictionary<Rigidbody2DComponent, TranformComponent>();
        }

        public void Rigidbodies_Add(Rigidbody2DComponent rb, TranformComponent transform) {
            rigidbodies.Add(rb);
            rb2Transform.Add(rb, transform);
        }

        public void Rigidbodies_Remove(Rigidbody2DComponent rb) {
            rigidbodies.Remove(rb);
            rb2Transform.Remove(rb);
        }

        public void Rigidbodies_ForEach(Action<Rigidbody2DComponent> action) {
            foreach (var rb in rigidbodies) {
                action(rb);
            }
        }

        public TranformComponent Rigidbodies_GetTransform(Rigidbody2DComponent rb) {
            return rb2Transform[rb];
        }

        public void Rigidbodies_Clear() {
            rigidbodies.Clear();
            rb2Transform.Clear();
        }

    }

}