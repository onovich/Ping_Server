using System;
using MortiseFrame.LitIO;
using MortiseFrame.Abacus;

namespace Ping.Protocol {

    public struct EntitiesSyncBroadMessage : IMessage<EntitiesSyncBroadMessage> {

        public Vector2 paddle1Pos;
        public Vector2 paddle2Pos;
        public Vector2 ballPos;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<Vector2>(dst, paddle1Pos, ref offset);
            ByteWriter.Write<Vector2>(dst, paddle2Pos, ref offset);
            ByteWriter.Write<Vector2>(dst, ballPos, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            paddle1Pos = ByteReader.Read<Vector2>(src, ref offset);
            paddle2Pos = ByteReader.Read<Vector2>(src, ref offset);
            ballPos = ByteReader.Read<Vector2>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = false;
            int count = ByteCounter.Count<Vector2>() + ByteCounter.Count<Vector2>() + ByteCounter.Count<Vector2>();
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