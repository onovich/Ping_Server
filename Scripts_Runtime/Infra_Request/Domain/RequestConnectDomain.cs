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
            clientfd.NoDelay = true;

            var id = ctx.ID_PickPlayerIndex();
            clientState.playerIndex = id;

            ctx.ClientState_Add(clientState);

            var evt = ctx.EventCenter;
            evt.ConnectRes_On(clientState);

        }

        public static void SendConnectResAsync(RequestInfraContext ctx, ClientStateEntity clientState) {

            var msg = new ConnectResMessage();
            msg.status = 1;
            msg.playerIndex = clientState.playerIndex;
           
            ctx.Message_Enqueue(msg, clientState.clientfd);
            PLog.Log("SendConnectResAsync: " + clientState.playerIndex);

        }

    }

}