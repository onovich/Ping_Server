using System.Collections.Generic;

namespace Ping.Server {

    public class MainContext {

        // Entity
        SortedList<int, PlayerEntity> players;
        PlayerEntity player1Entity;

        public MainContext() {
            players = new SortedList<int, PlayerEntity>(2);
        }

        // Player
        public void Player_Add(PlayerEntity playerEntity) {
            players[playerEntity.GetPlayerIndex()] = playerEntity;
        }

        public PlayerEntity Player_Get(int index) {
            var player = players[index];
            if (player == null) {
                PLog.LogError($"MainContext.Player_Get: player not found: {index}");
            }
            return player;
        }

        public void Player_Clear() {
            players.Clear();
        }

    }

}