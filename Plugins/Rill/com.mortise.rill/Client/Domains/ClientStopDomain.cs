using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MortiseFrame.Rill {

    internal static class ClientStopDomain {

        internal static void Stop(ClientContext ctx) {

            if ((!ctx.Connecting) && (!ctx.Connected)) {
                return;
            }

            var client = ctx.Client;
            client.Close();

            var receiveThread = ctx.ReceiveThread;
            receiveThread?.Interrupt();
            ctx.Connecting_Set(false);
            ctx.Clear();
            client = null;
        }

    }

}