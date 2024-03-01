using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct GameResultBroadMessage : IMessage<GameResultBroadMessage> {

        int winnerId;
        int gameTurn;
        int[] scores;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<int>(dst, winnerId, ref offset);
            ByteWriter.Write<int>(dst, gameTurn, ref offset);
            ByteWriter.WriteArray<int>(dst, scores, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            winnerId = ByteReader.Read<int>(src, ref offset);
            gameTurn = ByteReader.Read<int>(src, ref offset);
            scores = ByteReader.ReadArray<int>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = false;
            int count = ByteCounter.Count<int>()
            + ByteCounter.Count<int>()
            + ByteCounter.CountArray<int>(scores);
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