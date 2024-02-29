using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class Rigidbody2DComponent {

        public Vector2 velocity;
        public TranformComponent transform;

        public ICollider collider;

        public Rigidbody2DComponent() {
            velocity = Vector2.zero;
        }

        public void Inject(TranformComponent trans, ICollider collider) {
            this.transform = trans;
            this.collider = collider;
        }

        public void ApplyPhysics(float deltaTime) {
            transform.position += velocity * deltaTime;
        }

        public void Reflect(Vector2 normal) {
            velocity = Vector2.Reflect(velocity, normal);
        }



    }

}