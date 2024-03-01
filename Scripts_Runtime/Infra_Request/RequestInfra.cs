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

        public static async void Tick_OnLogin(RequestInfraContext ctx, float dt) {
            var listenfd = ctx.Listenfd;
            if (listenfd == null) {
                return;
            }
            if (!listenfd.Poll(0, SelectMode.SelectRead)) {
                await RequestConnectDomain.AcceptConnectReqAsync(ctx);
            }

            ctx.CliendState_ForEachOrderly((clientState) => {
                var data = new byte[4096];
                int count = clientState.clientfd.Receive(data);
                RequestJoinRoomDomain.On_JoinRoomReq(ctx, clientState, data);
                RequestGameStartDomain.On_GameStartReq(ctx, clientState, data);
            });

        }

        public static void Tick_Game(RequestInfraContext ctx, float dt) {
            return;
        }

        // Connect
        public static async Task AcceptNewConnectAsync(RequestInfraContext ctx, ClientStateEntity state) {
            await RequestConnectDomain.AcceptConnectReqAsync(ctx);
        }

    }

}