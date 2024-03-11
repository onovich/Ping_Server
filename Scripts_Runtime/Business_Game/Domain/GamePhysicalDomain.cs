using MortiseFrame.Abacus;
using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GamePhysicalDomain {

        public static void OnTriggerEnter(GameBusinessContext ctx, RigidbodyEntity a, RigidbodyEntity b) {

            var aType = (EntityType)a.HolderType;
            var bType = (EntityType)b.HolderType;

            var aIndex = a.HolderID;
            var bIndex = b.HolderID;

            if (aType == EntityType.Ball && bType == EntityType.Gate) {
                var ball = ctx.Ball_Get();
                OnTriggerEnterBall_Gate(ctx, ball, bIndex);
                return;
            }

            if (bType == EntityType.Ball && aType == EntityType.Gate) {
                var ball = ctx.Ball_Get();
                OnTriggerEnterBall_Gate(ctx, ball, aIndex);
                return;
            }

        }

        static void OnTriggerEnterBall_Gate(GameBusinessContext ctx, BallEntity ball, int gatePlayerIndex) {

            var fsm = ball.FSM_GetComponent();
            fsm.EnterDead(gatePlayerIndex);

        }

    }

}