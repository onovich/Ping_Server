using System.Net;
using System.Net.Sockets;
using MortiseFrame.LitIO;

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

        public static void Tick_Login(RequestInfraContext ctx, float dt) {
            var listenfd = ctx.Listenfd;
            if (listenfd == null) {
                return;
            }
            if (!listenfd.Poll(0, SelectMode.SelectRead)) {
                AcceptAndBakeListenfd(ctx);
            }

            ctx.Cliendfds_ForEach((client) => {
                var data = new byte[4096];
                int count = client.clientfd.Receive(data);
                On_JoinRoomReq(ctx, client, data);
            });

        }

        static async void AcceptAndBakeListenfd(RequestInfraContext ctx) {
            var listenfd = ctx.Listenfd;
            var clientfd = listenfd.Accept();
            PLog.Log("New Client Connected");
            ClientState state = new ClientState();
            state.clientfd = clientfd;
            ctx.Clientfd_Add(clientfd, state);
            await AcceptNewConnectAsync(ctx, state);
            await Send_ConnectResAsync(ctx, state);
        }

        public static void Tick_Game(RequestInfraContext ctx, float dt) {
            return;
        }

        // Connect
        public static async Task AcceptNewConnectAsync(RequestInfraContext ctx, ClientState state) {
            await RequestConnectDomain.AcceptConnectReq(ctx);
        }

        // Send Res
        public static async Task Send_ConnectResAsync(RequestInfraContext ctx, ClientState state) {
            await RequestConnectDomain.SendConnectResAsync(ctx, state.clientfd);
        }

        public static void Send_JoinRoomRes(RequestInfraContext ctx, ClientState state, byte[] data) {
        }

        public static void Send_StartGameBroadRes(RequestInfraContext ctx, ClientState state, byte[] data) {
        }

        // On Req
        public static void On_JoinRoomReq(RequestInfraContext ctx, ClientState state, byte[] data) {
        }

    }

}