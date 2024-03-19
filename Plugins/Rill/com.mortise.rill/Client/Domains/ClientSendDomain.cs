using System;
using System.Threading;
using MortiseFrame.LitIO;

namespace MortiseFrame.Rill {

    internal static class ClientSendDomain {

        // Enqueue
        internal static void Enqueue(ClientContext ctx, IMessage msg) {
            ctx.Message_Enqueue(msg);
            RLog.Log("Client Enqueued: " + msg.GetType());
            ctx.SendPending_Set();
        }

        // Send
        internal static void ThreadTick_Send(ClientContext ctx) {

            if (ctx.Client == null) {
                return;
            }

            try {

                while (ctx.Client.Connected) {
                    ctx.SendPending_Reset();
                    SerializeAllAndSend(ctx);
                    ctx.SendPending_WaitOne();
                }

            } catch (ThreadAbortException) {
            } catch (ThreadInterruptedException) {
            } catch (Exception exception) {
                RLog.Log("SendLoop Exception: " + exception);
            } finally {
                ctx.Client.Close();
            }

        }

        // Serialize
        static void SerializeAllAndSend(ClientContext ctx) {
            while (ctx.Message_TryDequeue(out IMessage message)) {

                if (message == null) {
                    continue;
                }

                byte[] buff = ctx.Buffer_Get();
                int offset = 0;

                var src = message.ToBytes();
                if (src.Length >= 4096 - 5) {
                    RLog.Log("Message is too long");
                }

                int len = src.Length + 5;
                byte msgID = ctx.GetMessageID(message.GetType());

                ByteWriter.Write<int>(buff, len, ref offset);
                ByteWriter.Write<byte>(buff, msgID, ref offset);
                Buffer.BlockCopy(src, 0, buff, offset, src.Length);
                offset += src.Length;

                if (offset == 0) {
                    return;
                }
                ctx.Client.Send(buff, 0, offset, System.Net.Sockets.SocketFlags.None);
                RLog.Log("Client Sent: " + message.GetType());
                ctx.Buffer_Clear();
            }
        }

    }

}