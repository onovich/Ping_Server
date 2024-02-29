using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct LeaveRoomReqMessage : IMessage<LeaveRoomReqMessage> {

        int playerId;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWritter.Write<int>(dst, playerId, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            playerId = ByteReader.Read<int>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            int count = 4;
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