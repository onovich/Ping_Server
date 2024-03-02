using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public class GameBusinessContext {

        // Entity
        public GameEntity gameEntity;

        public FieldEntity fieldEntity;
        public BallEntity ballEntity;

        public PaddleEntity player1PaddleEntity;
        public PaddleEntity player2PaddleEntity;

        // TEMP
        public RaycastHit2D[] raycastTemp;

        // Infra
        public TemplateInfraContext templateInfraContext;
        public Physics2DInfraContext physics2DContext;

        // Main
        public MainContext mainContext;

        public GameBusinessContext() {
            gameEntity = new GameEntity();
            raycastTemp = new RaycastHit2D[1000];
        }

        public void Reset() {
            fieldEntity = null;
            ballEntity = null;
            player1PaddleEntity = null;
            player2PaddleEntity = null;
        }

        // Player
        public PlayerEntity Player_Get(int playerIndex) {
            return mainContext.Player_Get(playerIndex);
        }

        // Ball
        public void Ball_Set(BallEntity ballEntity) {
            this.ballEntity = ballEntity;
        }

        public BallEntity Ball_Get() {
            return ballEntity;
        }

        // Paddle
        public void Paddle_Set(PaddleEntity paddleEntity) {
            if (paddleEntity.GetPlayerIndex() == 1) {
                player1PaddleEntity = paddleEntity;
            } else {
                player2PaddleEntity = paddleEntity;
            }
        }

        public void Paddle_Clear(PaddleEntity paddleEntity) {
            if (paddleEntity.GetPlayerIndex() == 1) {
                player1PaddleEntity = null;
            } else {
                player2PaddleEntity = null;
            }
        }

        public PaddleEntity Paddle_Get(int playerIndex) {
            if (playerIndex == 1) {
                return player1PaddleEntity;
            } else {
                return player2PaddleEntity;
            }
        }

        // Field
        public void Field_Set(FieldEntity fieldEntity) {
            this.fieldEntity = fieldEntity;
        }

        public FieldEntity Field_Get() {
            return fieldEntity;
        }

    }

}