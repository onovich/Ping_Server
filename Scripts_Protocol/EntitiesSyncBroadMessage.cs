using System;
using MortiseFrame.LitIO;
using MortiseFrame.Abacus;

namespace Ping.Protocol {

    public struct EntitiesSyncBroadMessage : IMessage<EntitiesSyncBroadMessage> {

        public FVector2 paddle0Pos;
        public FVector2 paddle1Pos;
        public FVector2 ballPos;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<FVector2>(dst, paddle0Pos, ref offset);
            ByteWriter.Write<FVector2>(dst, paddle1Pos, ref offset);
            ByteWriter.Write<FVector2>(dst, ballPos, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            paddle0Pos = ByteReader.Read<FVector2>(src, ref offset);
            paddle1Pos = ByteReader.Read<FVector2>(src, ref offset);
            ballPos = ByteReader.Read<FVector2>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = false;
            int count = ByteCounter.Count<FVector2>() + ByteCounter.Count<FVector2>() + ByteCounter.Count<FVector2>();
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