using MortiseFrame.Abacus;

namespace Ping.Server {

    public class Rigidbody2DComponent {

        public Vector2 velocity;

        public Vector2 acceleration;

        public float mass;

        public Rigidbody2DComponent() {
            velocity = Vector2.zero;
            acceleration = Vector2.zero;
            mass = 1;
        }

    }

}