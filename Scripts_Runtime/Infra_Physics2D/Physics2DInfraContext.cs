using MortiseFrame.Abacus;

namespace Ping.Server {

    public class Physics2DInfraContext {

        public List<Rigidbody2DComponent> rigidbodies;

        public Physics2DInfraContext() {
            rigidbodies = new List<Rigidbody2DComponent>();
        }

        public void Rigidbodies_Add(Rigidbody2DComponent rb) {
            rigidbodies.Add(rb);
        }

        public void Rigidbodies_Remove(Rigidbody2DComponent rb) {
            rigidbodies.Remove(rb);
        }

        public void Rigidbodies_ForEach(Action<Rigidbody2DComponent> action) {
            foreach (var rb in rigidbodies) {
                action(rb);
            }
        }

        public void Rigidbodies_Clear() {
            rigidbodies.Clear();
        }

    }

}