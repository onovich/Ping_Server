using MortiseFrame.Abacus;

namespace Ping.Server.Business.Game {

    public static class GameGameDomain {
        public static void NewGame(GameBusinessContext ctx) {

            var config = ctx.templateInfraContext.Config_Get();

            // Game
            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            fsm.Gaming_Enter();
            game.random = new RandomService(101052099, 0);

            // Field
            GameFieldDomain.Spawn(ctx);

            // Ball
            GameBallDomain.SpawnAtOriginPos(ctx, new Vector2(0, 0));

            // Paddle 1
            GamePaddleDomain.Spawn(ctx, 0, config.player1PaddleSpawnPos);
            GamePaddleDomain.Spawn(ctx, 1, config.player2PaddleSpawnPos);

            // Send Res

        }

        public static void ExitGame(GameBusinessContext ctx) {

            // Game
            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            if (fsm.status == GameFSMStatus.NotInGame) return;

            fsm.NotInGame_Enter();

            // Field
            var field = ctx.Field_Get();
            GameFieldDomain.UnSpawn(ctx, field);

            // Ball
            var ball = ctx.Ball_Get();
            GameBallDomain.UnSpawn(ctx, ball);

            // Paddle
            var paddle1 = ctx.Paddle_Get(1);
            GamePaddleDomain.UnSpawn(ctx, paddle1);

            var paddle2 = ctx.Paddle_Get(2);
            GamePaddleDomain.UnSpawn(ctx, paddle2);

            // Map
            // Send Res

        }

        public static void Win(GameBusinessContext ctx, int playerIndex) {
            var game = ctx.gameEntity;
            game.IncTurn();
            var player = ctx.Player_Get(playerIndex);
            player.Score_Inc();
            // Send Res
        }

    }

}