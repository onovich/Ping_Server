using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class Rigidbody2DComponent {

        public Vector2 Velocity { get; set; }

        public Rigidbody2DComponent() {
            Velocity = Vector2.zero;
        }

        public void ApplyPhysics(TranformComponent transform, float deltaTime) {
            transform.Pos += Velocity * deltaTime;
        }

        public void Reflect(Vector2 normal) {
            Velocity = Vector2.Reflect(Velocity, normal);
        }



    }

}