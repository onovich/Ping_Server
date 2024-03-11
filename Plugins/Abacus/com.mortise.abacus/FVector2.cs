namespace MortiseFrame.Abacus {

    public struct FVector2 {

        public float x;
        public float y;

        public FVector2(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static FVector2 operator +(FVector2 a, FVector2 b) {
            return new FVector2(a.x + b.x, a.y + b.y);
        }

        public static FVector2 operator -(FVector2 a, FVector2 b) {
            return new FVector2(a.x - b.x, a.y - b.y);
        }

        public static FVector2 operator *(FVector2 a, float b) {
            return new FVector2(a.x * b, a.y * b);
        }

        public static FVector2 operator *(float a, FVector2 b) {
            return new FVector2(a * b.x, a * b.y);
        }

        public static FVector2 operator /(FVector2 a, float b) {
            return new FVector2(a.x / b, a.y / b);
        }

        public static FVector2 operator -(FVector2 a) {
            return new FVector2(-a.x, -a.y);
        }

        public static bool operator ==(FVector2 a, FVector2 b) {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(FVector2 a, FVector2 b) {
            return a.x != b.x || a.y != b.y;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }
            var v = (FVector2)obj;
            return x == v.x && y == v.y;
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString() {
            return $"({x}, {y})";
        }

        public static FVector2 zero = new FVector2(0, 0);

        public static FVector2 one = new FVector2(1, 1);

        public static FVector2 up = new FVector2(0, 1);

        public static FVector2 down = new FVector2(0, -1);

        public static FVector2 left = new FVector2(-1, 0);

        public static FVector2 right = new FVector2(1, 0);

        public static float Distance(FVector2 a, FVector2 b) {
            return (a - b).Magnitude();
        }

        public float Magnitude() {
            return (float)System.Math.Sqrt(x * x + y * y);
        }

        public float magnitude => Magnitude();

        public FVector2 Normalize() {
            float magnitude = Magnitude();
            if (magnitude > 1E-05f) {
                this = this / magnitude;
            } else {
                this = zero;
            }
            return this;
        }

        public FVector2 normalized {
            get {
                float magnitude = Magnitude();
                if (magnitude > 1E-05f) {
                    return this / magnitude;
                } else {
                    return zero;
                }
            }
        }

        public float SqrMagnitude() {
            return x * x + y * y;
        }

        public float sqrMagnitude => SqrMagnitude();

        public static float Dot(FVector2 a, FVector2 b) {
            return a.x * b.x + a.y * b.y;
        }

        public static FVector2 Lerp(FVector2 a, FVector2 b, float t) {
            return a + (b - a) * t;
        }

        public static FVector2 Max(FVector2 a, FVector2 b) {
            return new FVector2(System.Math.Max(a.x, b.x), System.Math.Max(a.y, b.y));
        }

        public static FVector2 Min(FVector2 a, FVector2 b) {
            return new FVector2(System.Math.Min(a.x, b.x), System.Math.Min(a.y, b.y));
        }

        public static FVector2 Scale(FVector2 a, FVector2 b) {
            return new FVector2(a.x * b.x, a.y * b.y);
        }

        public static FVector2 Reflect(FVector2 inDirection, FVector2 inNormal) {
            return inDirection - 2 * Dot(inDirection, inNormal) * inNormal;
        }

        public static FVector2 Perpendicular(FVector2 inDirection) {
            return new FVector2(-inDirection.y, inDirection.x);
        }

    }

}