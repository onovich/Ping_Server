using MortiseFrame.Abacus;
using Ping.Server.Requests;

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
            GameBallDomain.SpawnAtOriginPos(ctx, new FVector2(0, 0));

            // Paddle 1
            GamePaddleDomain.Spawn(ctx, 0, config.player0PaddleSpawnPos);
            GamePaddleDomain.Spawn(ctx, 1, config.player1PaddleSpawnPos);

            // Send Res

        }

        public static void ExitGame(GameBusinessContext ctx) {

            // Game
            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            if (fsm.Status == GameFSMStatus.NotInGame) return;

            fsm.NotInGame_Enter();

            // Field
            var field = ctx.Field_Get();
            GameFieldDomain.UnSpawn(ctx, field);

            // Ball
            var ball = ctx.Ball_Get();
            GameBallDomain.UnSpawn(ctx, ball);

            // Paddle
            var paddle0 = ctx.Paddle_Get(0);
            GamePaddleDomain.UnSpawn(ctx, paddle0);

            var paddle1 = ctx.Paddle_Get(1);
            GamePaddleDomain.UnSpawn(ctx, paddle1);

            // Map
            // Send Res

        }

        public static void EnterGameResult(GameBusinessContext ctx, int playerIndex) {
            var game = ctx.gameEntity;
            var fsm = game.FSM_GetComponent();
            fsm.GameResult_Enter(playerIndex);
        }

        public static void Win(GameBusinessContext ctx, int playerIndex) {
            var game = ctx.gameEntity;
            game.Turn_Inc();
            var player = ctx.Player_Get(playerIndex);
            player.Score_Inc();

            var fsm = game.FSM_GetComponent();

            var winnierPlayerIndex = fsm.GameResult_winnierPlayerIndex;
            var socre0 = ctx.Player_Get(0).Score;
            var socre1 = ctx.Player_Get(1).Score;

            GameBallDomain.ResetBall(ctx, ctx.Ball_Get());
            RequestInfra.SendGameResultBroad(ctx.reqInfraContext, winnierPlayerIndex, game.Turn, socre0, socre1);
        }

    }

}