using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public static class MathFP {

        public static AABB SizeToAABB(Vector2 size) {
            return new AABB(
                new Vector2(-size.x / 2, -size.y / 2),
                new Vector2(size.x / 2, size.y / 2)
            );
        }

        public static Vector2 GetReflectDir(Vector2 srcDir, Vector2 normal) {
            // R = I - 2 * (I · N) * N
            // R: 反射向量; I: 入射向量; N: 法线单位向量
            var drtDir = srcDir - 2 * (Vector2.Dot(srcDir, normal)) * normal;
            return drtDir;
        }

    }

}