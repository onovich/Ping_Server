using System;
using System.Collections.Generic;

namespace MortiseFrame.Rill {

    internal class ServerEventCenter {

        readonly Dictionary<int, List<Action<IMessage, ConnectionEntity>>> eventsDict;
        readonly List<Action<string, ConnectionEntity>> errorEvent;
        readonly List<Action<ConnectionEntity>> connectEvent;

        internal ServerEventCenter() {
            eventsDict = new Dictionary<int, List<Action<IMessage, ConnectionEntity>>>();
            errorEvent = new List<Action<string, ConnectionEntity>>();
            connectEvent = new List<Action<ConnectionEntity>>();
        }

        internal void On<T>(ServerContext ctx, Action<IMessage, ConnectionEntity> listener) where T : IMessage {
            var msgId = ctx.GetMessageID<T>();
            if (!eventsDict.ContainsKey(msgId)) {
                eventsDict[msgId] = new List<Action<IMessage, ConnectionEntity>>();
            }

            eventsDict[msgId].Add(listener);
        }

        internal void OnError(ServerContext ctx, Action<string, ConnectionEntity> listener) {
            errorEvent.Add(listener);
        }

        internal void OnConnect(ServerContext ctx, Action<ConnectionEntity> listener) {
            connectEvent.Add(listener);
        }

        internal void Off<T>(ServerContext ctx, Action<IMessage, ConnectionEntity> listener) where T : IMessage {
            var msgId = ctx.GetMessageID<T>();
            if (eventsDict.ContainsKey(msgId)) {
                eventsDict[msgId].Remove(listener);
            }
        }

        internal void OffError(ServerContext ctx, Action<string, ConnectionEntity> listener) {
            errorEvent.Remove(listener);
        }

        internal void OffConnect(ServerContext ctx, Action<ConnectionEntity> listener) {
            connectEvent.Remove(listener);
        }

        internal void Emit(int msgId, IMessage msg, ConnectionEntity conn) {
            if (eventsDict.ContainsKey(msgId)) {
                foreach (var listener in eventsDict[msgId]) {
                    listener?.Invoke(msg, conn);
                }
            }
        }

        internal void EmitError(string error, ConnectionEntity conn) {
            foreach (var listener in errorEvent) {
                listener?.Invoke(error, conn);
            }
        }

        internal void EmitConnect(ConnectionEntity conn) {
            foreach (var listener in connectEvent) {
                listener?.Invoke(conn);
            }
        }

        internal void Clear() {
            eventsDict.Clear();
            errorEvent.Clear();
            connectEvent.Clear();
        }

    }

}