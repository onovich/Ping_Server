using MortiseFrame.Pulse;

namespace Ping.Server.Business.Game {

    public class GameBusinessContext {

        // Entity
        public GameEntity gameEntity;
        public PlayerEntity playerEntity;
        public InputEntity inputEntity;

        public FieldEntity fieldEntity;
        public BallEntity ballEntity;
        public PaddleEntity player1PaddleEntity;
        public PaddleEntity player2PaddleEntity;

        // TEMP
        public Collider2D[] overlapTemp;
        public RaycastHit2D[] raycastTemp;

        // Infra
        public TemplateInfraContext templateInfraContext;

        public GameBusinessContext() {
            gameEntity = new GameEntity();
            overlapTemp = new Collider2D[1000];
            raycastTemp = new RaycastHit2D[1000];
        }

        public void Reset() {
            fieldEntity = null;
            ballEntity = null;
            player1PaddleEntity = null;
            player2PaddleEntity = null;
        }

        // Player
        public void Player_Set(PlayerEntity playerEntity) {
            this.playerEntity = playerEntity;
        }

        public PlayerEntity Player_Get() {
            return playerEntity;
        }

        public void Player_Clear() {
            playerEntity = null;
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
            if (paddleEntity.GetPlayerID() == 1) {
                player1PaddleEntity = paddleEntity;
            } else {
                player2PaddleEntity = paddleEntity;
            }
        }

        public void Paddle_Clear(PaddleEntity paddleEntity) {
            if (paddleEntity.GetPlayerID() == 1) {
                player1PaddleEntity = null;
            } else {
                player2PaddleEntity = null;
            }
        }

        public PaddleEntity Paddle_Get(int playerID) {
            if (playerID == 1) {
                return player1PaddleEntity;
            } else {
                return player2PaddleEntity;
            }
        }

        public PaddleEntity Paddle_GetLocalOwner() {
            var ownerID = gameEntity.GetLocalOwnerPlayerID();
            return Paddle_Get(ownerID);
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