using System;
using System.Net.Sockets;
using System.Threading;
using MortiseFrame.LitIO;

namespace MortiseFrame.Rill {

    internal static class ServerSendDomain {

        // Enqueue
        internal static void Enqueue(ServerContext ctx, IMessage msg, ConnectionEntity connection) {
            connection.Message_Enqueue(msg);
            connection.SendPending_Set();
        }

        // Send
        internal static void ThreadTick_Send(ServerContext ctx, ConnectionEntity connection) {

            if (ctx.Listener == null) {
                return;
            }

            if (connection == null) {
                return;
            }

            try {

                while (connection.clientfd.Connected) {
                    connection.SendPending_Reset();
                    SerializeAll(ctx, connection);
                    connection.SendPending_WaitOne();
                }

            } catch (ThreadAbortException) {
            } catch (ThreadInterruptedException) {
            } catch (Exception exception) {
                RLog.Log("SendLoop Exception: " + exception);
            } finally {
                connection.clientfd.Close();
            }

        }

        // Serialize
        static void SerializeAll(ServerContext ctx, ConnectionEntity connection) {
            while (connection.Message_TryDequeue(out IMessage message)) {

                if (message == null) {
                    continue;
                }

                byte[] buff = connection.WriteBuffer_Get();

                int offset = 4;
                byte msgID = ctx.GetMessageID(message.GetType());
                ByteWriter.Write<byte>(buff, msgID, ref offset);
                message.WriteTo(buff, ref offset);

                int len = offset;
                offset = 0;
                ByteWriter.Write<int>(buff, len, ref offset);

                if (offset == 0) {
                    return;
                }
                connection.clientfd.Send(buff, 0, offset, System.Net.Sockets.SocketFlags.None);
                connection.WriteBuffer_Clear();
            }
        }

    }

}