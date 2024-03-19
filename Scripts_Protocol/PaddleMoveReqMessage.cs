using System;
using MortiseFrame.LitIO;
using MortiseFrame.Abacus;
using MortiseFrame.Rill;

namespace Ping.Protocol {

    public struct PaddleMoveReqMessage : IMessage {

        public FVector2 moveAxis;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<FVector2>(dst, moveAxis, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            moveAxis = ByteReader.Read<FVector2>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = true;
            int count = ByteCounter.Count<FVector2>();
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