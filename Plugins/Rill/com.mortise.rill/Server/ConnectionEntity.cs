using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace MortiseFrame.Rill {

    public class ConnectionEntity {

        internal Socket clientfd;
        internal int connectionIndex;
        public int ConnectionIndex => connectionIndex;

        // Buffer
        byte[] readBuffer;
        byte[] writeBuffer;

        // Locker
        object locker;
        ManualResetEvent sendPending;

        Queue<IMessage> messageQueue;
        Queue<byte[]> receiveDataQueue;

        internal ConnectionEntity(Socket conn, int connIndex) {
            this.clientfd = conn;
            this.connectionIndex = connIndex;
            this.readBuffer = new byte[CommonConst.BufferLength];
            this.writeBuffer = new byte[CommonConst.BufferLength];
            locker = new object();
            messageQueue = new Queue<IMessage>();
            receiveDataQueue = new Queue<byte[]>();
            sendPending = new ManualResetEvent(false);
        }

        // Buffer
        internal byte[] ReadBuffer_Get() {
            return readBuffer;
        }

        internal byte[] WriteBuffer_Get() {
            return writeBuffer;
        }

        internal void ReadBuffer_Clear() {
            Array.Clear(readBuffer, 0, readBuffer.Length);
        }

        internal void WriteBuffer_Clear() {
            Array.Clear(writeBuffer, 0, writeBuffer.Length);
        }

        // Message Queue
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

    }

}