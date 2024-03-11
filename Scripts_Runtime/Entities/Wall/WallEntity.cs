using MortiseFrame.Pulse;

namespace Ping.Server {

    public class WallEntity : IEntity {

        // Base Info
        EntityType IEntity.EntityType => EntityType.Wall;

        // Physical
        RigidbodyEntity RB;

        public void RB_Set(RigidbodyEntity RB) {
            this.RB = RB;
        }

        public void TearDown() {
        }

    }

}