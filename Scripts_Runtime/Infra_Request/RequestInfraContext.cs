using System.Collections.Generic;
using System.Net.Sockets;

namespace Ping.Server.Requests {

    public class ClientState {
        public Socket clientfd;
        public string userToken;
    }

    public class RequestInfraContext {

        Socket listenfd;
        public Socket Listenfd => listenfd;

        Dictionary<Socket, ClientState> clients = new Dictionary<Socket, ClientState>();

        RequestEventCenter eventCenter;
        public RequestEventCenter EventCenter => eventCenter;

        public RequestInfraContext() {
            eventCenter = new RequestEventCenter();
        }

        public void Clientfd_Add(Socket clientfd, ClientState state) {
            clients.Add(clientfd, state);
        }

        public void Clientfd_Remove(Socket clientfd) {
            clients.Remove(clientfd);
        }

        public void Cliendfds_ForEach(Action<ClientState> action) {
            foreach (var client in clients.Values) {
                action.Invoke(client);
            }
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