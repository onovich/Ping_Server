using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct GameResultBroadMessage : IMessage<GameResultBroadMessage> {

        int winnerId;
        int gameTurn;
        int[] scores;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWritter.Write<int>(dst, winnerId, ref offset);
            ByteWritter.Write<int>(dst, gameTurn, ref offset);
            ByteWritter.WriteArray<int>(dst, scores, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            winnerId = ByteReader.Read<int>(src, ref offset);
            gameTurn = ByteReader.Read<int>(src, ref offset);
            scores = ByteReader.ReadArray<int>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            int count = 12;
            isCertain = false;
            if (scores != null) {
                count += scores.Length * 4;
            }
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