using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct LeaveRoomBroadMessage : IMessage<LeaveRoomBroadMessage> {

        public void WriteTo(byte[] dst, ref int offset) {
        }

        public void FromBytes(byte[] src, ref int offset) {
        }

        public int GetEvaluatedSize(out bool isCertain) {
            int count = 0;
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