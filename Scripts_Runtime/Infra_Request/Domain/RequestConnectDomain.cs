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

            ClientStateEntity clientState = new ClientStateEntity();
            clientState.clientfd = clientfd;

            var id = ctx.ID_PickPlayerIndex();
            clientState.playerIndex = id;

            ctx.ClientState_Add(clientState);

            var evt = ctx.EventCenter;
            evt.ConnectRes_On(clientState);

        }

        public static async Task SendConnectResAsync(RequestInfraContext ctx, ClientStateEntity clientState) {

            var msg = new ConnectResMessage();
            msg.status = 1;
            msg.playerIndex = clientState.playerIndex;
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
            await clientState.clientfd.SendAsync(dst);

            PLog.Log("SendConnectResAsync: " + clientState.playerIndex);

        }

    }

}