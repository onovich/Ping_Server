using System;
using System.Collections.Generic;

namespace MortiseFrame.Rill {

    internal class ClientEventCenter {

        readonly Dictionary<int, List<Action<IMessage>>> eventsDict;
        readonly List<Action<string>> errorEvent;
        readonly List<Action> connectEvent;
        readonly List<Action> disconnectEvent;

        internal ClientEventCenter() {
            eventsDict = new Dictionary<int, List<Action<IMessage>>>();
            errorEvent = new List<Action<string>>();
            connectEvent = new List<Action>();
            disconnectEvent = new List<Action>();
        }

        internal void On<T>(ClientContext ctx, Action<IMessage> listener) where T : IMessage {
            var msgId = ctx.GetMessageID<T>();
            if (!eventsDict.ContainsKey(msgId)) {
                eventsDict[msgId] = new List<Action<IMessage>>();
            }

            eventsDict[msgId].Add(listener);
        }

        internal void OnError(ClientContext ctx, Action<string> listener) {
            errorEvent.Add(listener);
        }

        internal void OnConnect(ClientContext ctx, Action listener) {
            connectEvent.Add(listener);
        }

        internal void OnDisconnect(ClientContext ctx, Action listener) {
            disconnectEvent.Add(listener);
        }

        internal void Off<T>(ClientContext ctx, Action<IMessage> listener) where T : IMessage {
            var msgId = ctx.GetMessageID<T>();
            if (eventsDict.ContainsKey(msgId)) {
                eventsDict[msgId].Remove(listener);
            }
        }

        internal void OffError(ClientContext ctx, Action<string> listener) {
            errorEvent.Remove(listener);
        }

        internal void OffConnect(ClientContext ctx, Action listener) {
            connectEvent.Remove(listener);
        }

        internal void OffDisconnect(ClientContext ctx, Action listener) {
            disconnectEvent.Remove(listener);
        }

        internal void Emit(int msgId, IMessage msg) {
            if (eventsDict.ContainsKey(msgId)) {
                foreach (var listener in eventsDict[msgId]) {
                    listener?.Invoke(msg);
                }
            }
        }

        internal void EmitError(string error) {
            foreach (var listener in errorEvent) {
                listener?.Invoke(error);
            }
        }

        internal void EmitConnect() {
            foreach (var listener in connectEvent) {
                listener?.Invoke();
            }
        }

        internal void EmitDisconnect() {
            foreach (var listener in disconnectEvent) {
                listener?.Invoke();
            }
        }

        internal void Clear() {
            eventsDict.Clear();
            errorEvent.Clear();
            connectEvent.Clear();
            disconnectEvent.Clear();
        }

    }

}