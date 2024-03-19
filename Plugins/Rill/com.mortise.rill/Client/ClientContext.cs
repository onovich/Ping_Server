using System.Collections.Generic;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System;
using System.Threading;

namespace MortiseFrame.Rill {

    internal class ClientContext {

        Socket client;
        internal Socket Client => client;

        // State
        volatile bool connecting;
        public bool Connecting => connecting;

        public bool Connected => client != null &&
                                 client.Connected;

        // Thread
        Thread receiveThread;
        public Thread ReceiveThread => receiveThread;

        // Message Queue
        Queue<IMessage> messageQueue;

        // Reveive Date Queue
        Queue<byte[]> receiveDataQueue;

        // Event
        ClientEventCenter evt;
        internal ClientEventCenter Evt => evt;

        // Protocol
        BiDictionary<byte, Type> protocolDicts;

        // Buffer
        byte[] buffer;

        // Service
        IDService idService;
        internal IDService IDService => idService;

        // Locker
        object locker;
        ManualResetEvent sendPending;

        internal ClientContext() {
            messageQueue = new Queue<IMessage>();
            buffer = new byte[4096];
            evt = new ClientEventCenter();
            idService = new IDService();
            receiveDataQueue = new Queue<byte[]>();
            locker = new object();
            protocolDicts = new BiDictionary<byte, Type>();
            sendPending = new ManualResetEvent(false);
        }

        internal void Client_Set(Socket socket) {
            this.client = socket;
        }

        // State
        internal void Connecting_Set(bool value) {
            connecting = value;
        }

        // Message
        internal void Message_Enqueue(IMessage message) {
            lock (locker) {
                messageQueue.Enqueue(message);
            }
        }

        internal bool Message_TryDequeue(out IMessage message) {
            lock (locker) {
                return messageQueue.TryDequeue(out message);
            }
        }

        // Buffer
        internal void Buffer_Clear() {
            lock (locker) {
                Array.Clear(buffer, 0, buffer.Length);
            }
        }

        internal byte[] Buffer_Get() {
            lock (locker) {
                return buffer;
            }
        }

        // Protocol
        internal void RegisterMessage(Type msgType) {
            if (!protocolDicts.ContainsValue(msgType)) {
                var msgId = IDService.PickMsgId();
                protocolDicts.Add(msgId, msgType);
            }
        }

        internal object GetMessage(byte id) {
            var has = protocolDicts.TryGetByKey(id, out Type type);
            if (!has) {
                throw new ArgumentException("No type found for the given ID.", id.ToString());
            }
            if (type == null) {
                throw new ArgumentException("No type found for the given ID.", id.ToString());
            }
            return Activator.CreateInstance(type);
        }

        internal byte GetMessageID(Type msgType) {
            var has = protocolDicts.TryGetByValue(msgType, out byte id);
            if (!has) {
                throw new ArgumentException("ID Not Found");
            }
            return id;
        }

        internal byte GetMessageID<T>() {
            var has = protocolDicts.TryGetByValue(typeof(T), out byte id);
            if (!has) {
                throw new ArgumentException("ID Not Found");
            }
            return id;
        }

        // Receive Data Queue
        internal void ReceiveData_Enqueue(byte[] data) {
            lock (locker) {
                receiveDataQueue.Enqueue(data);
            }
        }

        internal bool ReceiveDate_TryDequeue(out byte[] data) {
            lock (locker) {
                return receiveDataQueue.TryDequeue(out data);
            }
        }

        // Thread
        internal void ReceiveThread_Set(Thread thread) {
            receiveThread = thread;
        }

        // Pending
        internal void SendPending_Set() {
            sendPending.Set();
        }

        internal void SendPending_Reset() {
            sendPending.Reset();
        }

        internal void SendPending_WaitOne() {
            sendPending.WaitOne();
        }

        internal void Clear() {
            messageQueue.Clear();
            protocolDicts.Clear();
            evt.Clear();
            idService.Reset();
        }

    }

}