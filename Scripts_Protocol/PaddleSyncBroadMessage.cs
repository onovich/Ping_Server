using System;
using MortiseFrame.LitIO;
using MortiseFrame.Abacus;

namespace Ping.Protocol {

    public struct PaddleSyncBroadMessage : IMessage<PaddleSyncBroadMessage> {

        public Vector2[] paddlePos;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.WriteArray<Vector2>(dst, paddlePos, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            paddlePos = ByteReader.ReadArray<Vector2>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = false;
            int count = ByteCounter.CountArray<Vector2>(paddlePos);
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