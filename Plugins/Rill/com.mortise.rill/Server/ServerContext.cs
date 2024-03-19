using System.Collections.Generic;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System;
using System.Threading;

namespace MortiseFrame.Rill {

    internal class ServerContext {

        // Listener
        Socket listener;
        internal Socket Listener => listener;

        // Thread
        Thread listenerThread;
        public Thread ListenerThread => listenerThread;
        public bool Active => listenerThread != null && listenerThread.IsAlive;

        // Clients
        Dictionary<Socket, ConnectionEntity> clients;
        SortedList<int, Socket> clientOrderList;

        // Event
        ServerEventCenter evt;
        internal ServerEventCenter Evt => evt;

        // Protocol Dict
        BiDictionary<byte, Type> protocolDict;

        // Service
        IDService idService;
        internal IDService IDService => idService;

        // Locker
        object locker;

        internal ServerContext() {
            clients = new Dictionary<Socket, ConnectionEntity>();
            clientOrderList = new SortedList<int, Socket>();
            evt = new ServerEventCenter();
            idService = new IDService();
            locker = new object();
            protocolDict = new BiDictionary<byte, Type>();
        }

        internal void Server_Set(Socket socket) {
            this.listener = socket;
        }

        // Client
        public void Connection_Add(ConnectionEntity connection) {
            clients.Add(connection.clientfd, connection);
            clientOrderList.Add(connection.clientIndex, connection.clientfd);
        }

        public void Connection_Remove(Socket client) {
            clientOrderList.Remove(clients[client].clientIndex);
            clients.Remove(client);
        }

        public void Connection_ForEachOrderly(Action<ConnectionEntity> action) {
            for (int i = 0; i < clientOrderList.Count; i++) {
                action(clients[clientOrderList.Values[i]]);
            }
        }

        // Protocol
        internal void RegisterMessage(Type msgType) {
            if (!protocolDict.ContainsValue(msgType)) {
                var msgId = IDService.PickMsgId();
                protocolDict.Add(msgId, msgType);
            }
        }

        internal object GetMessage(byte id) {
            var has = protocolDict.TryGetByKey(id, out Type type);
            if (!has) {
                throw new ArgumentException("No type found for the given ID.", id.ToString());
            }
            if (type == null) {
                throw new ArgumentException("No type found for the given ID.", id.ToString());
            }
            return Activator.CreateInstance(type);
        }

        internal byte GetMessageID(Type msgType) {
            var has = protocolDict.TryGetByValue(msgType, out byte id);
            if (!has) {
                throw new ArgumentException("ID Not Found");
            }
            return id;
        }

        internal byte GetMessageID<T>() {
            var has = protocolDict.TryGetByValue(typeof(T), out byte id);
            if (!has) {
                throw new ArgumentException("ID Not Found");
            }
            return id;
        }

        // Thread
        internal void ListenerThread_Clear() {
            listenerThread = null;
        }

        internal void ListnerThread_Set(Thread thread) {
            listenerThread = thread;
        }

        internal void Clear() {
            protocolDict.Clear();
            evt.Clear();
            idService.Reset();
        }

    }

}