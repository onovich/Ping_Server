using System.Collections.Generic;
using MortiseFrame.Pulse;
using Ping.Server.Requests;

namespace Ping.Server.Business.Game {

    public class GameBusinessContext {

        // Entity
        public GameEntity gameEntity;

        public FieldEntity fieldEntity;
        public BallEntity ballEntity;

        SortedList<int, PaddleEntity> paddles;

        // TEMP
        public Hits raycastTemp;

        // Infra
        public TemplateInfraContext templateInfraContext;
        public RequestInfraContext reqInfraContext;

        // Core
        public PhysicalCore physicalCore;

        // Main
        public MainContext mainContext;

        public GameBusinessContext() {
            gameEntity = new GameEntity();
            paddles = new SortedList<int, PaddleEntity>(2);
        }

        public void Reset() {
            fieldEntity = null;
            ballEntity = null;
            paddles.Clear();
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
        public void Paddle_Add(PaddleEntity paddleEntity) {
            paddles[paddleEntity.PlayerIndex] = paddleEntity;
        }

        public void Paddle_Remove(PaddleEntity paddleEntity) {
            paddles.Remove(paddleEntity.PlayerIndex);
        }

        public PaddleEntity Paddle_Get(int playerIndex) {
            if (paddles.TryGetValue(playerIndex, out var paddle)) {
                return paddle;
            }
            PLog.LogError($"GameBusinessContext.Paddle_Get: paddle not found: {playerIndex}");
            return null;
        }

        // Field
        public void Field_Set(FieldEntity fieldEntity) {
            this.fieldEntity = fieldEntity;
        }

        public FieldEntity Field_Get() {
            return fieldEntity;
        }

        // Time
        public float Time_GetTimestamp() {
            return gameEntity.Time_Get();
        }

    }

}