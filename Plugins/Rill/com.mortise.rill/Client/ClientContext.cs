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
        byte[] readBuffer;
        byte[] writeBuffer;

        // Service
        IDService idService;
        internal IDService IDService => idService;

        // Locker
        object locker;
        ManualResetEvent sendPending;

        // Config
        internal bool NoDelay = true;
        internal int SendTimeout = 5000;
        internal int ReceiveTimeout = 0;
        internal int BufferLength = 4096;

        internal ClientContext() {
            messageQueue = new Queue<IMessage>();
            readBuffer = new byte[BufferLength];
            writeBuffer = new byte[BufferLength];
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
        internal void ReadBuffer_Clear() {
            Array.Clear(readBuffer, 0, readBuffer.Length);
        }

        internal void WriteBuffer_Clear() {
            Array.Clear(writeBuffer, 0, writeBuffer.Length);
        }

        internal byte[] ReadBuffer_Get() {
            return readBuffer;
        }

        internal byte[] WriteBuffer_Get() {
            return writeBuffer;
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