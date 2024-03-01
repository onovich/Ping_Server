using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct ConnectResMessage : IMessage<ConnectResMessage> {

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

        public int GetEvaluatedSize(out bool isCertain) {
            int count = ByteCounter.Count<sbyte>() + ByteCounter.Count<byte>();
            isCertain = true;
            return count;
        }

        public byte[] ToBytes() {
            int count = GetEvaluatedSize(out bool isCertain);
            int offset = 0;
            byte[] src = new byte[count];
            WriteTo(src, ref offset);
            if (isCertain) {
                return src;
            } else {
                byte[] dst = new byte[offset];
                Buffer.BlockCopy(src, 0, dst, 0, offset);
                return dst;
            }
        }

    }

}