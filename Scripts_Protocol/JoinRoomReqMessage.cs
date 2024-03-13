using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct JoinRoomReqMessage : IMessage {

        public string userName;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.WriteUTF8String(dst, userName, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            userName = ByteReader.ReadUTF8String(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = false;
            int count = ByteCounter.CountUTF8String(userName);
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