using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct GameResultBroadMessage : IMessage {

        public int winnerPlayerIndex;
        public int gameTurn;
        public int score0;
        public int score1;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<int>(dst, winnerPlayerIndex, ref offset);
            ByteWriter.Write<int>(dst, gameTurn, ref offset);
            ByteWriter.Write<int>(dst, score0, ref offset);
            ByteWriter.Write<int>(dst, score1, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            winnerPlayerIndex = ByteReader.Read<int>(src, ref offset);
            gameTurn = ByteReader.Read<int>(src, ref offset);
            score0 = ByteReader.Read<int>(src, ref offset);
            score1 = ByteReader.Read<int>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = false;
            int count = ByteCounter.Count<int>()
            + ByteCounter.Count<int>()
            + ByteCounter.Count<int>()
            + ByteCounter.Count<int>();
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