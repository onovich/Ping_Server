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
                    var offset = 0;
                    var msgCount = ByteReader.Read<int>(buff, ref offset);
                    for (int i = 0; i < msgCount; i++) {
                        var len = ByteReader.Read<int>(buff, ref offset);
                        if (len < 5) {
                            break;
                        }
                        PLog.Log("Receive Message: Len: " + len);
                        On(ctx, clientState, buff, ref offset);
                    }
                }
            }
        }

        public static void On(RequestInfraContext ctx, ClientStateEntity clientState, byte[] data, ref int offset) {

            var msgID = ByteReader.Read<byte>(data, ref offset);
            var msg = ProtocolIDConst.GetObject(msgID) as IMessage;

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.On(msg, clientState);

            PLog.Log("Receive Message: " + msg.GetType().Name + " ID: " + msgID);

        }

        public static async Task Tick_Send(RequestInfraContext ctx, float dt) {
            await ctx.ClientState_ForEachOrderlyAsync(async (clientState) => {
                if (clientState.clientfd == null) {
                    return;
                }
                if (!clientState.clientfd.Connected) {
                    return;
                }
                byte[] buff = new byte[4096];
                int offset = 0;
                int msgCount = ctx.Message_GetCount(clientState.clientfd);
                ByteWriter.Write<int>(buff, msgCount, ref offset);
                while (ctx.Message_TryDequeue(clientState.clientfd, out IMessage message)) {
                    if (message == null) {
                        continue;
                    }

                    var src = message.ToBytes();
                    if (src.Length >= 4096 - 5) {
                        throw new Exception("Message is too long");
                    }

                    int len = src.Length + 5;
                    byte msgID = ProtocolIDConst.GetID(message);

                    ByteWriter.Write<int>(buff, len, ref offset);
                    ByteWriter.Write<byte>(buff, msgID, ref offset);
                    Buffer.BlockCopy(src, 0, buff, offset, src.Length);
offset += src.Length;

                }

                byte[] dst = new byte[offset];
                Buffer.BlockCopy(buff, 0, dst, 0, offset);

                await clientState.clientfd.SendAsync(dst);
            });
        }

    }

}