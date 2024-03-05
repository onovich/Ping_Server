using System;

namespace MortiseFrame.Abacus {

    public static class Mathf {
        public const float PI = 3.1415927F;
        public const float Infinity = float.PositiveInfinity;
        public const float NegativeInfinity = float.NegativeInfinity;
        public const float Deg2Rad = 0.017453292519943295769236907684886F;
        public const float Rad2Deg = 57.295779513082320876798154814105F;
        public static readonly float Epsilon = float.Epsilon;

        public static float Abs(float f) => Math.Abs(f);
        public static int Abs(int value) => Math.Abs(value);
        public static float Acos(float f) => (float)Math.Acos(f);
        public static bool Approximately(float a, float b) => Math.Abs(b - a) < Math.Max(1E-06f * Math.Max(Math.Abs(a), Math.Abs(b)), Epsilon * 8f);
        public static float Asin(float f) => (float)Math.Asin(f);
        public static float Atan(float f) => (float)Math.Atan(f);
        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);
        public static float Ceil(float f) => (float)Math.Ceiling(f);
        public static int CeilToInt(float f) => (int)Math.Ceiling(f);
        public static int Clamp(int value, int min, int max) => Math.Max(min, Math.Min(max, value));
        public static float Clamp(float value, float min, float max) => Math.Max(min, Math.Min(max, value));
        public static float Clamp01(float value) => Clamp(value, 0f, 1f);
        public static int ClosestPowerOfTwo(int value) => (int)Math.Pow(2, Math.Ceiling(Math.Log(value, 2)));
        public static float Cos(float f) => (float)Math.Cos(f);
        public static float DeltaAngle(float current, float target) {
            var delta = Repeat((target - current), 360);
            if (delta > 180)
                delta -= 360;
            return delta;
        }
        public static float Exp(float power) => (float)Math.Exp(power);
        public static float Floor(float f) => (float)Math.Floor(f);
        public static int FloorToInt(float f) => (int)Math.Floor(f);
        public static float InverseLerp(float a, float b, float value) => (a != b) ? Clamp01((value - a) / (b - a)) : 0f;
        public static bool IsPowerOfTwo(int value) => value != 0 && (value & (value - 1)) == 0;
        public static float Lerp(float a, float b, float t) => a + (b - a) * Clamp01(t);
        public static float LerpAngle(float a, float b, float t) {
            float delta = Repeat((b - a), 360);
            if (delta > 180)
                delta -= 360;
            return a + delta * Clamp01(t);
        }
        public static float LerpUnclamped(float a, float b, float t) => a + (b - a) * t;
        public static float Log(float f) => (float)Math.Log(f);
        public static float Log(float f, float p) => (float)Math.Log(f, p);
        public static float Log10(float f) => (float)Math.Log10(f);
        public static float Max(float a, float b) => Math.Max(a, b);
        public static int Max(int a, int b) => Math.Max(a, b);
        public static float Min(float a, float b) => Math.Min(a, b);
        public static int Min(int a, int b) => Math.Min(a, b);
        public static float MoveTowards(float current, float target, float maxDelta) {
            if (Math.Abs(target - current) <= maxDelta)
                return target;
            return current + Math.Sign(target - current) * maxDelta;
        }
        public static float MoveTowardsAngle(float current, float target, float maxDelta) {
            float deltaAngle = DeltaAngle(current, target);
            if (-maxDelta < deltaAngle && deltaAngle < maxDelta)
                return target;
            target = current + deltaAngle;
            return MoveTowards(current, target, maxDelta);
        }
        public static int NextPowerOfTwo(int value) => (int)Math.Pow(2, Math.Ceiling(Math.Log(value, 2)));
        public static float PingPong(float t, float length) => length - Math.Abs(Repeat(t, length * 2) - length);
        public static float Pow(float f, float p) => (float)Math.Pow(f, p);
        public static float Repeat(float t, float length) => Clamp(t - (float)Math.Floor(t / length) * length, 0.0f, length);
        public static float Round(float f) => (float)Math.Round(f);
        public static int RoundToInt(float f) => (int)Math.Round(f);
        public static float Sign(float f) => (f < 0) ? -1f : 1f;
        public static float Sin(float f) => (float)Math.Sin(f);
        public static float Sqrt(float f) => (float)Math.Sqrt(f);
        public static float Tan(float f) => (float)Math.Tan(f);

    }

}