using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestConnectDomain {

        // Send
        public static async Task AcceptConnectReqAsync(RequestInfraContext ctx) {

            var listenfd = ctx.Listenfd;
            var clientfd = await listenfd.AcceptAsync();
            PLog.Log("New Client Connected");

            ClientStateEntity state = new ClientStateEntity();
            state.clientfd = clientfd;

            ctx.ClientState_Add(state);

            var evt = ctx.EventCenter;
            evt.ConnectRes_On(clientfd);

        }

        public static async Task SendConnectResAsync(RequestInfraContext ctx, Socket clientfd) {

            var msg = new ConnectResMessage();
            msg.status = 1;
            byte msgID = ProtocolIDConst.RESID_CONNECT;

            byte[] data = msg.ToBytes();
            if (data.Length >= 4096 - 2) {
                throw new Exception("Message is too long");
            }

            byte[] dst = new byte[data.Length + 2];
            int offset = 0;
            dst[offset] = msgID;
            offset += 1;
            Buffer.BlockCopy(data, 0, dst, offset, data.Length);
            await clientfd.SendAsync(dst);

        }

    }

}