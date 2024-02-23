namespace Ping.Server {

    public struct Vector2 {

        public float x;
        public float y;

        public Vector2(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b) {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, float b) {
            return new Vector2(a.x * b, a.y * b);
        }

        public static Vector2 operator *(float a, Vector2 b) {
            return new Vector2(a * b.x, a * b.y);
        }

        public static Vector2 operator /(Vector2 a, float b) {
            return new Vector2(a.x / b, a.y / b);
        }

        public static bool operator ==(Vector2 a, Vector2 b) {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector2 a, Vector2 b) {
            return a.x != b.x || a.y != b.y;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }
            var v = (Vector2)obj;
            return x == v.x && y == v.y;
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString() {
            return $"({x}, {y})";
        }

        public static Vector2 zero = new Vector2(0, 0);

        public static Vector2 one = new Vector2(1, 1);

        public static Vector2 up = new Vector2(0, 1);

        public static Vector2 down = new Vector2(0, -1);

        public static Vector2 left = new Vector2(-1, 0);

        public static Vector2 right = new Vector2(1, 0);

        public static float Distance(Vector2 a, Vector2 b) {
            return (a - b).Magnitude();
        }

        public float Magnitude() {
            return (float)System.Math.Sqrt(x * x + y * y);
        }

        public Vector2 Normalize() {
            return this / Magnitude();
        }

        public float SqrMagnitude() {
            return x * x + y * y;
        }

        public static float Dot(Vector2 a, Vector2 b) {
            return a.x * b.x + a.y * b.y;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t) {
            return a + (b - a) * t;
        }

        public static Vector2 Max(Vector2 a, Vector2 b) {
            return new Vector2(System.Math.Max(a.x, b.x), System.Math.Max(a.y, b.y));
        }

        public static Vector2 Min(Vector2 a, Vector2 b) {
            return new Vector2(System.Math.Min(a.x, b.x), System.Math.Min(a.y, b.y));
        }

        public static Vector2 Scale(Vector2 a, Vector2 b) {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal) {
            return inDirection - 2 * Dot(inDirection, inNormal) * inNormal;
        }

        public static Vector2 Perpendicular(Vector2 inDirection) {
            return new Vector2(-inDirection.y, inDirection.x);
        }

    }

}