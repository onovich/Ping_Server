using MortiseFrame.Pulse;

namespace Ping.Server {

    public class WallEntity : IEntity {

        // Base Info
        EntityType IEntity.EntityType => EntityType.Wall;

        public AABB ColliderBox { get; private set; }

        public void ColliderBox_Set(AABB box) {
            ColliderBox = box;
        }

        public void TearDown() {
        }

    }

}