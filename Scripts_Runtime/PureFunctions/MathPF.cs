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

    }

}