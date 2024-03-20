using System;
using MortiseFrame.LitIO;
using MortiseFrame.Rill;

namespace Ping.Protocol {

    public struct JoinRoomBroadMessage : IMessage {

        public sbyte status; // 1 为成功, -1 为失败
        public string userName1;
        public string userName2;
        public byte ownerIndex;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<sbyte>(dst, status, ref offset);
            ByteWriter.WriteUTF8String(dst, userName1, ref offset);
            ByteWriter.WriteUTF8String(dst, userName2, ref offset);
            ByteWriter.Write<byte>(dst, ownerIndex, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            status = ByteReader.Read<sbyte>(src, ref offset);
            userName1 = ByteReader.ReadUTF8String(src, ref offset);
            userName2 = ByteReader.ReadUTF8String(src, ref offset);
            ownerIndex = ByteReader.Read<byte>(src, ref offset);
        }

    }

}