using System;
using MortiseFrame.LitIO;
using MortiseFrame.Rill;

namespace Ping.Protocol {

    public struct GameResultBroadMessage : IMessage {

        public int winnerPlayerIndex;
        public int gameTurn;
        public int score1;
        public int score2;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<int>(dst, winnerPlayerIndex, ref offset);
            ByteWriter.Write<int>(dst, gameTurn, ref offset);
            ByteWriter.Write<int>(dst, score1, ref offset);
            ByteWriter.Write<int>(dst, score2, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            winnerPlayerIndex = ByteReader.Read<int>(src, ref offset);
            gameTurn = ByteReader.Read<int>(src, ref offset);
            score1 = ByteReader.Read<int>(src, ref offset);
            score2 = ByteReader.Read<int>(src, ref offset);
        }

    }

}