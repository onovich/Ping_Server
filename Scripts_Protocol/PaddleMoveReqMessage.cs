using System;
using MortiseFrame.LitIO;
using MortiseFrame.Abacus;

namespace Ping.Protocol {

    public struct PaddleMoveReqMessage : IMessage<PaddleMoveReqMessage> {

        public int paddleId;
        public Vector2 moveAxis;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWritter.Write<int>(dst, paddleId, ref offset);
            ByteWritter.Write<Vector2>(dst, moveAxis, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            paddleId = ByteReader.Read<int>(src, ref offset);
            moveAxis = ByteReader.Read<Vector2>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            int count = 12;
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