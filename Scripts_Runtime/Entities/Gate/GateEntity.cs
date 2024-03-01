using System.Collections;
using System.Collections.Generic;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class GateEntity : IEntity {

        // Base Info
        EntityType IEntity.EntityType => EntityType.Gate;

        public int playerIndex;

        // Physics
        AABB colliderBox;

        public void TearDown() {
        }

    }

}