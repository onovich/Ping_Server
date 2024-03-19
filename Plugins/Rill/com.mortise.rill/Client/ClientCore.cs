using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MortiseFrame.LitIO;

namespace MortiseFrame.Rill {

    public class ClientCore {

        ClientContext ctx;

        public ClientCore() {
            ctx = new ClientContext();
        }

        // Register
        public void Register(Type msgType) {
            ctx.RegisterMessage(msgType);
        }

        // Tick 
        public void Tick(float dt) {
            ClientReceiveDomain.Tick_DeserializeAll(ctx);
        }

        // Send
        public void Send(IMessage msg) {
            ClientSendDomain.Enqueue(ctx, msg);
        }

        // Connect
        public void Connect(string remoteIP, int port) {
            ClientConnectDomain.Connect(ctx, remoteIP, port);
        }

        // On
        public void On<T>(Action<IMessage> listener) where T : IMessage {
            ctx.Evt.On<T>(ctx, listener);
        }

        public void OnError(Action<string> listener) {
            ctx.Evt.OnError(ctx, listener);
        }

        public void OnConnect(Action listener) {
            ctx.Evt.OnConnect(ctx, listener);
        }

        public void OnDisconnect(Action listener) {
            ctx.Evt.OnDisconnect(ctx, listener);
        }

        // Off
        public void Off<T>(Action<object> listener) where T : IMessage {
            ctx.Evt.Off<T>(ctx, listener);
        }

        public void OffError(Action<string> listener) {
            ctx.Evt.OffError(ctx, listener);
        }

        public void OffConnect(Action listener) {
            ctx.Evt.OffConnect(ctx, listener);
        }

        public void OffDisconnect(Action listener) {
            ctx.Evt.OffDisconnect(ctx, listener);
        }

        // Stop
        public void Stop() {
            ClientStopDomain.Stop(ctx);
        }

    }

}