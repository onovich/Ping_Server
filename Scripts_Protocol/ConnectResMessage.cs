using System;
using MortiseFrame.LitIO;
using MortiseFrame.Rill;

namespace Ping.Protocol {

    public struct ConnectResMessage : IMessage {

        public sbyte status; // 1 为成功, -1 为失败
        public byte playerIndex;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<sbyte>(dst, status, ref offset);
            ByteWriter.Write<byte>(dst, playerIndex, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            status = ByteReader.Read<sbyte>(src, ref offset);
            playerIndex = ByteReader.Read<byte>(src, ref offset);
        }

    }

}