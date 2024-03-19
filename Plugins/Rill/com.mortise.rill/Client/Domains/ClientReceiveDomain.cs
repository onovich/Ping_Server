using System;
using MortiseFrame.LitIO;

namespace MortiseFrame.Rill {

    internal static class ClientReceiveDomain {

        internal static void ThreadTick_Receive(ClientContext ctx) {

            try {

                while (true) {
                    byte[] buff = ctx.Buffer_Get();
                    var client = ctx.Client;
                    int count = client.Receive(buff);
                    if (count <= 0) {
                        break;
                    }

                    var data = new byte[count];
                    Buffer.BlockCopy(buff, 0, data, 0, count);
                    Enqueue(ctx, data);

                    ctx.Buffer_Clear();
                }

            } catch (Exception exception) {
                RLog.Log(" ReceiveLoop: finished receive function for:" + exception);
            } finally {
                ctx.Client.Close();
                ctx.Evt.EmitDisconnect();
            }

        }

        // Enqueue
        static void Enqueue(ClientContext ctx, byte[] data) {
            ctx.ReceiveData_Enqueue(data);
        }

        // Read
        internal static void Tick_DeserializeAll(ClientContext ctx) {

            while (ctx.ReceiveDate_TryDequeue(out byte[] data)) {

                var count = data.Length;
                var offset = 0;
                while (offset < count) {
                    var len = ByteReader.Read<int>(data, ref offset);
                    if (len == 0) {
                        break;
                    }
                    var msgID = ByteReader.Read<byte>(data, ref offset);
                    var msg = ctx.GetMessage(msgID) as IMessage;

                    msg.FromBytes(data, ref offset);
                    var evt = ctx.Evt;
                    evt.Emit(msgID, msg);
                }

            }

        }

    }

}