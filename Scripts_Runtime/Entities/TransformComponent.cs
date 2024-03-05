using MortiseFrame.Abacus;

namespace Ping.Server {

    public class TranformComponent {

        public Vector2 Pos { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }

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
            Pos = Vector2.zero;
            Scale = Vector2.one;
            Rotation = 0;
        }

    }

}