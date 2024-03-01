using System.Collections.Generic;
using System.Net.Sockets;

namespace Ping.Server.Requests {

    public class RequestInfraContext {

        Socket listenfd;
        public Socket Listenfd => listenfd;

        Dictionary<Socket, ClientStateEntity> clients = new Dictionary<Socket, ClientStateEntity>();

        RequestEventCenter eventCenter;
        public RequestEventCenter EventCenter => eventCenter;

        public RequestInfraContext() {
            eventCenter = new RequestEventCenter();
        }

        public void ClientState_Add(ClientStateEntity clientState) {
            clients.Add(clientState.clientfd, clientState);
        }

        public void ClientState_Remove(Socket clientfd) {
            clients.Remove(clientfd);
        }

        public void CliendState_ForEach(Action<ClientStateEntity> action) {
            foreach (var client in clients.Values) {
                action.Invoke(client);
            }
        }

        public ClientStateEntity ClientState_GetByPlayerID(int playerID) {
            foreach (var client in clients.Values) {
                if (client.playerID == playerID) {
                    return client;
                }
            }
            return null;
        }

        public void Listenfd_Set(Socket listenfd) {
            this.listenfd = listenfd;
        }

        public void Clear() {
            listenfd = null;
            clients.Clear();
            eventCenter.Clear();
        }

    }

}