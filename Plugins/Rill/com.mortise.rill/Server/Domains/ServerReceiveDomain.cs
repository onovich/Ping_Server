using System;
using MortiseFrame.LitIO;

namespace MortiseFrame.Rill {

    internal static class ServerReceiveDomain {

        // Receive
        internal static void ThreadTick_Receive(ServerContext ctx, ConnectionEntity connection) {

            try {

                while (true) {

                    byte[] buff = connection.Buffer_Get();
                    int count = connection.clientfd.Receive(buff);
                    if (count <= 0) {
                        return;
                    }

                    var data = new byte[count];
                    Buffer.BlockCopy(buff, 0, data, 0, count);
                    Enqueue(ctx, connection, data);

                    connection.Buffer_Clear();

                }

            } catch (Exception exception) {
                RLog.Log(" ReceiveLoop: finished receive function for:" + exception);

            } finally {
                connection.clientfd.Close();
            }

        }

        // Enqueue
        static void Enqueue(ServerContext ctx, ConnectionEntity connection, byte[] data) {
            connection.ReceiveData_Enqueue(data);
        }

        // Read
        internal static void Tick_DeserializeAll(ServerContext ctx) {

            ctx.Connection_ForEachOrderly((connection) => {

                while (connection.ReceiveDate_TryDequeue(out byte[] data)) {

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
                        evt.Emit(msgID, msg, connection);
                    }

                }

            });

        }

    }

}