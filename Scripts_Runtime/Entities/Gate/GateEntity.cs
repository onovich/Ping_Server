using System.Collections;
using System.Collections.Generic;
using MortiseFrame.Pulse;

namespace Ping.Server {

    public class GateEntity : IEntity {

        // Base Info
        EntityType IEntity.EntityType => EntityType.Gate;

        public int playerIndex;

        // Physics
        RigidbodyEntity RB;

        public GateEntity(int playerIndex) {
            this.playerIndex = playerIndex;
        }

        public void RB_Set(RigidbodyEntity RB) {
            this.RB = RB;
        }

        public void TearDown() {
        }

    }

}