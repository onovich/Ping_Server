using System;
using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GameTimeDomain {

        public static void ApplyGameTime(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            game.Time_Set(game.Time + dt);
            // Send Res
        }

    }

}