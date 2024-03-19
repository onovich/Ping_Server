using System;
using MortiseFrame.Abacus;
using MortiseFrame.Rill;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestInfra {

        //  Register
        public static void RegisterAllProtocol(RequestInfraContext ctx) {
            var server = ctx.ServerCore;
            server.Register(typeof(ConnectResMessage));
            server.Register(typeof(EntitiesSyncBroadMessage));
            server.Register(typeof(GameResultBroadMessage));
            server.Register(typeof(GameStartBroadMessage));
            server.Register(typeof(GameStartReqMessage));
            server.Register(typeof(JoinRoomBroadMessage));
            server.Register(typeof(JoinRoomReqMessage));
            server.Register(typeof(KeepAliveReqMessage));
            server.Register(typeof(KeepAliveResMessage));
            server.Register(typeof(LeaveRoomBroadMessage));
            server.Register(typeof(LeaveRoomReqMessage));
            server.Register(typeof(PaddleMoveReqMessage));
        }

        //  Send
        static void Send(RequestInfraContext ctx, IMessage msg, ConnectionEntity conn) {
            ctx.ServerCore.Send(msg, conn);
        }

        //  Tick
        public static void Tick(RequestInfraContext ctx, float dt) {
            ctx.ServerCore.Tick(dt);
        }

        //  Connect
        public static void Start(RequestInfraContext ctx) {
            var ip = RequestConst.LOCAL_IP;
            var port = RequestConst.LOCAL_PORT;
            ctx.ServerCore.Start(ip, port);
        }

        //  On  
        public static void On<T>(RequestInfraContext ctx, Action<IMessage, ConnectionEntity> listener) where T : IMessage {
            ctx.ServerCore.On<T>(listener);
        }

        public static void OnError(RequestInfraContext ctx, Action<string, ConnectionEntity> listener) {
            ctx.ServerCore.OnError(listener);
        }

        public static void OnConnected(RequestInfraContext ctx, Action<ConnectionEntity> listener) {
            ctx.ServerCore.OnConnect(listener);
        }

        //  Off
        public static void Off<T>(RequestInfraContext ctx, Action<IMessage, ConnectionEntity> listener) where T : IMessage {
            ctx.ServerCore.Off<T>(listener);
        }

        public static void OffError(RequestInfraContext ctx, Action<string, ConnectionEntity> listener) {
            ctx.ServerCore.OffError(listener);
        }

        public static void OffConnected(RequestInfraContext ctx, Action<ConnectionEntity> listener) {
            ctx.ServerCore.OffConnect(listener);
        }

        public static void Stop(RequestInfraContext ctx) {
            ctx.ServerCore.Stop();
        }

        // Send Res And Broad
        // - Login
        public static void SendConnectRes(RequestInfraContext ctx, ConnectionEntity conn) {
            var msg = new ConnectResMessage();
            msg.status = 1;
            msg.playerIndex = (byte)conn.ConnectionIndex;
            ctx.ServerCore.Send(msg, conn);
        }

        public static void SendJoinRoomBroad(RequestInfraContext ctx, string userName1, string userName2) {
            ctx.ServerCore.ForEachOrderly((conn) => {
                var msg = new JoinRoomBroadMessage();
                msg.status = 1;
                msg.ownerIndex = (byte)conn.ConnectionIndex;
                msg.userName1 = userName1;
                msg.userName2 = userName2;
                ctx.ServerCore.Send(msg, conn);
            });
        }

        public static void SendGameStartBroad(RequestInfraContext ctx) {
            ctx.ServerCore.ForEachOrderly((conn) => {
                var msg = new GameStartBroadMessage();
                ctx.ServerCore.Send(msg, conn);
            });
        }

        // - Game
        public static void SendGameEntitiesSyncBroad(RequestInfraContext ctx, FVector2 paddle1Pos, FVector2 paddle2Pos, FVector2 ballPos) {
            ctx.ServerCore.ForEachOrderly((conn) => {
                var msg = new EntitiesSyncBroadMessage();
                msg.paddle1Pos = paddle1Pos;
                msg.paddle2Pos = paddle2Pos;
                msg.ballPos = ballPos;
                ctx.ServerCore.Send(msg, conn);
            });
        }

        public static void SendGameResultBroad(RequestInfraContext ctx, int winnerIndex, int gameTurn, int score0, int score1) {
            ctx.ServerCore.ForEachOrderly((conn) => {
                var msg = new GameResultBroadMessage();
                msg.winnerPlayerIndex = winnerIndex;
                msg.gameTurn = gameTurn;
                msg.score1 = score0;
                msg.score2 = score1;
                ctx.ServerCore.Send(msg, conn);
            });
        }

        public static void SendKeepAliveRes(RequestInfraContext ctx, float timestamp) {
            ctx.ServerCore.ForEachOrderly((conn) => {
                var msg = new KeepAliveResMessage();
                msg.timestamp = timestamp;
                ctx.ServerCore.Send(msg, conn);
            });
        }

    }

}