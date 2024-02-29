using MortiseFrame.Abacus;

namespace Ping.Server {

    public class TranformComponent {

        public Vector2 position;
        public Vector2 scale;
        public float rotation;

        IEntity ie;
        public void Inject(IEntity ie) {
            this.ie = ie;
        }

        public T GetComponent<T>() {
            if (ie is T component) {
                return component;
            }
            return default(T);
        }

        public TranformComponent() {
            position = Vector2.zero;
            scale = Vector2.one;
            rotation = 0;
        }

    }

}