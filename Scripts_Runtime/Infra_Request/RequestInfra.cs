using System.Net;
using System.Net.Sockets;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestInfra {

        public static void Bind(RequestInfraContext ctx) {
            try {

                IPEndPoint localEndPoint = new IPEndPoint(RequestConst.LOCAL_IP, RequestConst.LOCAL_PORT);
                var listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenfd.Bind(localEndPoint);

                listenfd.Listen(0);
                ctx.Listenfd_Set(listenfd);

                PLog.Log($"Server Has Started On {RequestConst.LOCAL_IP}:{RequestConst.LOCAL_PORT}.\nWaiting For A Connection...");

            } catch (Exception e) {
                PLog.Log(e.ToString());
            }
        }

        public static async Task Tick_On(RequestInfraContext ctx, float dt) {
            ctx.checkReadList.Clear();
            ctx.checkReadList.Add(ctx.Listenfd);
            ctx.CliendState_ForEachOrderly((clientState) => {
                ctx.checkReadList.Add(clientState.clientfd);
            });
            Socket.Select(ctx.checkReadList, null, null, 1000);
            foreach (Socket s in ctx.checkReadList) {
                if (s == ctx.Listenfd) {
                    await RequestConnectDomain.AcceptConnectReqAsync(ctx);
                } else {
                    byte[] buff = ctx.readBuff;
                    int count = await s.ReceiveAsync(buff);
                    var clientState = ctx.ClientState_GetBySocket(s);

                    // Login
                    RequestJoinRoomDomain.On_JoinRoomReq(ctx, clientState, buff);
                    RequestGameStartDomain.On_GameStartReq(ctx, clientState, buff);

                    // Game
                }
            }
        }

    }

}