using System;
using MortiseFrame.LitIO;
using MortiseFrame.Rill;

namespace Ping.Protocol {

    public struct KeepAliveReqMessage : IMessage {

        public void WriteTo(byte[] dst, ref int offset) {
        }

        public void FromBytes(byte[] src, ref int offset) {
        }

    }

}