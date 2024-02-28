using MortiseFrame.Abacus;

namespace Ping.Server {

    public class TranformComponent {

        public Vector2 position;

        public Vector2 scale;

        public float rotation;

        public TranformComponent() {
            position = Vector2.zero;
            scale = Vector2.one;
            rotation = 0;
        }

    }

}