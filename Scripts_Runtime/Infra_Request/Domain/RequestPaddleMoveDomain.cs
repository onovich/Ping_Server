using System;
using MortiseFrame.LitIO;
using Ping.Protocol;

namespace Ping.Server.Requests {

    public static class RequestPaddleMoveDomain {

        // On
        public static void On_RequestPaddleMoveReq(RequestInfraContext ctx, ClientStateEntity clientState, byte[] data) {

            int offset = 0;
            var msgID = ByteReader.Read<byte>(data, ref offset);
            if (msgID != ProtocolIDConst.GetID<PaddleMoveReqMessage>()) {
                return;
            }

            var msg = new PaddleMoveReqMessage();

            msg.FromBytes(data, ref offset);
            var evt = ctx.EventCenter;
            evt.PaddleMove_On(msg, clientState);

        }

    }

}