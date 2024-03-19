using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MortiseFrame.Rill {

    internal static class ServerStartDomain {

        internal static void Start(ServerContext ctx, IPAddress ip, int port) {
            if (ctx.Active) {
                return;
            }

            var listenerThread = new Thread(() => {
                Listen(ctx, ip, port);
            });

            listenerThread.IsBackground = true;
            listenerThread.Priority = ThreadPriority.BelowNormal;
            ctx.ListnerThread_Set(listenerThread);
            listenerThread.Start();
        }

        static void Listen(ServerContext ctx, IPAddress ip, int port) {
            try {

                IPEndPoint localEndPoint = new IPEndPoint(ip, port);
                var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.NoDelay = CommonConst.NoDelay;
                listener.Bind(localEndPoint);

                listener.Listen(0);
                ctx.Server_Set(listener);

                RLog.Log($"Server Has Started On {ip}:{port}.\nWaiting For A Connection...");

                while (true) {

                    Socket clientfd = listener.Accept();

                    clientfd.NoDelay = CommonConst.NoDelay;
                    clientfd.SendTimeout = CommonConst.SendTimeout;
                    clientfd.ReceiveTimeout = CommonConst.ReceiveTimeout;

                    int clientIndex = ctx.IDService.PickClientIndex();
                    var client = new ConnectionEntity(clientfd, clientIndex);
                    ctx.Connection_Add(client);
                    RLog.Log("Server Client Connected: " + clientfd.RemoteEndPoint);

                    ctx.Evt.EmitConnect(client);

                    Thread sendThread = new Thread(() => {
                        try {
                            ServerSendDomain.ThreadTick_Send(ctx, client);
                        } catch (ThreadAbortException) {
                        } catch (Exception exception) {
                            RLog.Error("Server send thread exception: " + exception);
                        }
                    });
                    sendThread.IsBackground = true;
                    sendThread.Start();

                    Thread receiveThread = new Thread(() => {
                        try {
                            ServerReceiveDomain.ThreadTick_Receive(ctx, client);
                            sendThread.Interrupt();
                        } catch (Exception exception) {
                            RLog.Error("Server client thread exception: " + exception);
                        }
                    });
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
            } catch (ThreadAbortException exception) {
                RLog.Log("Server thread aborted. That's okay. " + exception);
            } catch (SocketException exception) {
                RLog.Log("Server Thread stopped. That's okay. " + exception);
            } catch (Exception exception) {
                RLog.Error("Server Exception: " + exception);
            }
        }

    }

}