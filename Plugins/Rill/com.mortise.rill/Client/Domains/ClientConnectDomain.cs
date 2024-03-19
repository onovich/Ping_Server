using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MortiseFrame.Rill {

    internal static class ClientConnectDomain {

        internal static void Connect(ClientContext ctx, string remoteIP, int port) {
            if (ctx.Connecting || ctx.Connected) {
                return;
            }

            ctx.Connecting_Set(true);

            var receiveThread = new Thread(() => {
                Listen(ctx, remoteIP, port);
            });

            receiveThread.IsBackground = true;
            ctx.ReceiveThread_Set(receiveThread);
            receiveThread.Start();
        }

        static void Listen(ClientContext ctx, string remoteIP, int port) {

            Thread sendThread = null;

            try {

                var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ctx.Client_Set(client);

                IPAddress ipAddress = IPAddress.Parse(remoteIP);
                IPEndPoint ep = new IPEndPoint(ipAddress, port);

                client.NoDelay = CommonConst.NoDelay;
                client.SendTimeout = CommonConst.SendTimeout;
                client.ReceiveTimeout = CommonConst.ReceiveTimeout;

                client.Connect(ep);
                ctx.Connecting_Set(false);
                ctx.Evt.EmitConnect();

                RLog.Log("Client Is Connected to IP = " + remoteIP + " PORT = " + port);

                sendThread = new Thread(() => {
                    ClientSendDomain.ThreadTick_Send(ctx);
                });
                sendThread.IsBackground = true;
                sendThread.Start();

                ClientReceiveDomain.ThreadTick_Receive(ctx);

            } catch (SocketException exception) {
                RLog.Log("Client Recv: failed to connect to ip=" + remoteIP + " port=" + port + " reason=" + exception);
                ctx.Evt.EmitDisconnect();
            } catch (ThreadInterruptedException) {
            } catch (ThreadAbortException) {
            } catch (ObjectDisposedException) {
            } catch (Exception exception) {
                RLog.Error("Client Recv Exception: " + exception);
            }

            sendThread?.Interrupt();
            ctx.Connecting_Set(false);
            ctx.Client?.Close();
        }

    }

}