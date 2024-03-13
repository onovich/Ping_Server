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
                listenfd.NoDelay = true;
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
            ctx.ClientState_ForEachOrderly((clientState) => {
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

                    On(ctx, clientState, buff);
                }
            }
        }

        public static void On(RequestInfraContext ctx, ClientStateEntity clientState, byte[] data) {

            int offset = 0;
            var msgID = ByteReader.Read<byte>(data, ref offset);
            var msg = ProtocolIDConst.GetObject(msgID) as IMessage;

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.On(msg, clientState);

        }

        public static async Task Tick_Send(RequestInfraContext ctx, float dt) {
            await ctx.ClientState_ForEachOrderlyAsync(async (clientState) => {
                if (clientState.clientfd == null) {
                    return;
                }
                if (!clientState.clientfd.Connected) {
                    return;
                }
                byte[] dst = new byte[4096];
                int offset = 0;
                while (ctx.Message_TryDequeue(clientState.clientfd, out IMessage message)) {
                    var src = message.ToBytes();
                    if (src.Length >= 4096 - 2) {
                        throw new Exception("Message is too long");
                    }

                    byte msgID = ProtocolIDConst.GetID(message);
                    dst[offset] = msgID;
                    offset += 1;

                    Buffer.BlockCopy(src, 0, dst, offset, src.Length);
                    offset += src.Length;
                }
                await clientState.clientfd.SendAsync(dst);
            });
        }

    }

}