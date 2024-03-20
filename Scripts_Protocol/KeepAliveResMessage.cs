using System;
using MortiseFrame.LitIO;
using MortiseFrame.Rill;

namespace Ping.Protocol {

    public struct KeepAliveResMessage : IMessage {

        public float timestamp;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<float>(dst, timestamp, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            timestamp = ByteReader.Read<float>(src, ref offset);
        }

    }

}