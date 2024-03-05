using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public struct Hits {

        public Vector2 point;
        public Vector2 normal;

        public Hits(Vector2 point, Vector2 normal) {
            this.point = point;
            this.normal = normal;
        }

    }

}