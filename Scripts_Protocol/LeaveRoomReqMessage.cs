using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct LeaveRoomReqMessage : IMessage<LeaveRoomReqMessage> {

        int playerIndex;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<int>(dst, playerIndex, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            playerIndex = ByteReader.Read<int>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = true;
            int count = ByteCounter.Count<int>();
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