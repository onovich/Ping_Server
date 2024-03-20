using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MortiseFrame.LitIO;

namespace MortiseFrame.Rill {

    public class ServerCore {

        ServerContext ctx;

        public ServerCore() {
            ctx = new ServerContext();
        }

        // Register
        public void Register(Type msgType) {
            ctx.RegisterMessage(msgType);
        }

        // Tick 
        public void Tick(float dt) {
            ServerReceiveDomain.Tick_DeserializeAll(ctx);
        }

        // Connects
        public void ForEachOrderly(Action<ConnectionEntity> action) {
            ctx.Connection_ForEachOrderly(action);
        }

        // Send
        public void Send(IMessage msg, ConnectionEntity client) {
            ServerSendDomain.Enqueue(ctx, msg, client);
        }

        // Connect
        public void Start(IPAddress ip, int port) {
            ServerStartDomain.Start(ctx, ip, port);
        }

        // On
        public void On<T>(Action<IMessage, ConnectionEntity> listener) where T : IMessage {
            ctx.Evt.On<T>(ctx, listener);
        }

        public void OnError(Action<string, ConnectionEntity> listener) {
            ctx.Evt.OnError(ctx, listener);
        }

        public void OnConnect(Action<ConnectionEntity> listener) {
            ctx.Evt.OnConnect(ctx, listener);
        }

        // Off
        public void Off<T>(Action<IMessage, ConnectionEntity> listener) where T : IMessage {
            ctx.Evt.Off<T>(ctx, listener);
        }

        public void OffError(Action<string, ConnectionEntity> listener) {
            ctx.Evt.OffError(ctx, listener);
        }

        public void OffConnect(Action<ConnectionEntity> listener) {
            ctx.Evt.OffConnect(ctx, listener);
        }

        // Stop
        public void Stop() {
            ServerStopDomain.Stop(ctx);
        }

        // Config
        public void SetNoDelay(bool noDelay) {
            ctx.NoDelay = noDelay;
        }

        public void SetSendTimeout(int sendTimeout) {
            ctx.SendTimeout = sendTimeout;
        }

        public void SetReceiveTimeout(int receiveTimeout) {
            ctx.ReceiveTimeout = receiveTimeout;
        }

        public void SetBufferLength(int bufferLength) {
            ctx.BufferLength = bufferLength;
        }

    }

}