using System;
using MortiseFrame.LitIO;
using MortiseFrame.Rill;

namespace Ping.Protocol {

    public struct JoinRoomReqMessage : IMessage {

        public string userName;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.WriteUTF8String(dst, userName, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            userName = ByteReader.ReadUTF8String(src, ref offset);
        }

    }
}