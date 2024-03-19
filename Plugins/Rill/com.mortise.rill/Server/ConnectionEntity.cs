using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace MortiseFrame.Rill {

    public class ConnectionEntity {

        internal Socket clientfd;
        internal int clientIndex;

        // Buffer
        byte[] buffer;

        // Locker
        object locker;
        ManualResetEvent sendPending;

        Queue<IMessage> messageQueue;
        Queue<byte[]> receiveDataQueue;

        internal ConnectionEntity(Socket client, int clientIndex) {
            this.clientfd = client;
            this.clientIndex = clientIndex;
            this.buffer = new byte[CommonConst.BufferLength];
            locker = new object();
            messageQueue = new Queue<IMessage>();
            receiveDataQueue = new Queue<byte[]>();
            sendPending = new ManualResetEvent(false);
        }

        // Buffer
        internal byte[] Buffer_Get() {
            lock (locker) {
                return buffer;
            }
        }

        internal void Buffer_Clear() {
            lock (locker) {
                Array.Clear(buffer, 0, buffer.Length);
            }
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