using System.Collections.Generic;
using System.Net.Sockets;

namespace Ping.Server.Requests {

    public class RequestInfraContext {

        // Socket
        Socket listenfd;
        public Socket Listenfd => listenfd;

        Dictionary<Socket, ClientStateEntity> clients;
        SortedList<int, Socket> clientOrderList;

        // Event
        RequestEventCenter eventCenter;
        public RequestEventCenter EventCenter => eventCenter;

        // Select
        public List<Socket> checkReadList;

        // Buffer
        public byte[] readBuff;

        // Service
        public IDService idService;

        public RequestInfraContext() {
            eventCenter = new RequestEventCenter();
            clientOrderList = new SortedList<int, Socket>();
            clients = new Dictionary<Socket, ClientStateEntity>();
            idService = new IDService();
            checkReadList = new List<Socket>();
            readBuff = new byte[4096];
        }

        public void ClientState_Add(ClientStateEntity clientState) {
            clients.Add(clientState.clientfd, clientState);
            clientOrderList.Add(clientState.playerIndex, clientState.clientfd);
        }

        public void ClientState_Remove(Socket clientfd) {
            clientOrderList.Remove(clients[clientfd].playerIndex);
            clients.Remove(clientfd);
        }

        public void CliendState_ForEachOrderly(Action<ClientStateEntity> action) {
            for (int i = 0; i < clientOrderList.Count; i++) {
                action(clients[clientOrderList.Values[i]]);
            }
        }

        public ClientStateEntity ClientState_GetByPlayerIndex(int playerIndex) {
            foreach (var client in clients.Values) {
                if (client.playerIndex == playerIndex) {
                    return client;
                }
            }
            return null;
        }

        public ClientStateEntity ClientState_GetBySocket(Socket clientfd) {
            return clients[clientfd];
        }

        public void Listenfd_Set(Socket listenfd) {
            this.listenfd = listenfd;
        }

        // ID
        public byte ID_PickPlayerIndex() {
            return idService.PickPlayerIndex();
        }

        public void ID_Reset() {
            idService.Reset();
        }

        public void Clear() {
            listenfd = null;
            clients.Clear();
            eventCenter.Clear();
        }

    }

}