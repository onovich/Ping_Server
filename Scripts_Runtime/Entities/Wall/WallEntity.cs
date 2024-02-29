using MortiseFrame.Pulse;

namespace Ping.Server {

    public class WallEntity : IEntity {

        // Base Info
        EntityType IEntity.EntityType => EntityType.Wall;

        AABB colliderBox;

        public void TearDown() {
        }

    }

}