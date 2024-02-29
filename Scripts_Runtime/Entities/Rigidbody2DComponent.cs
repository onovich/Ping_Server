using MortiseFrame.Abacus;

namespace Ping.Server {

    public class Rigidbody2DComponent {

        public Vector2 velocity;
        public TranformComponent transform;

        public Rigidbody2DComponent() {
            velocity = Vector2.zero;
        }

        public void Inject(TranformComponent trans) {
            this.transform = trans;
        }

        public void ApplyPhysics(float deltaTime) {
            transform.position += velocity * deltaTime;
        }

        public void Reflect(Vector2 normal) {
            velocity = Vector2.Reflect(velocity, normal);
        }



    }

}