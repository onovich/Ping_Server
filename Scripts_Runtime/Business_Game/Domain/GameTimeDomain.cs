using System;
using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public static class GameTimeDomain {

        public static void ApplyGameTime(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            game.SetTime(game.GetTime() + dt);
            // Send Res
        }

    }

}