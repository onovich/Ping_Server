using System;
using MortiseFrame.LitIO;
using MortiseFrame.Abacus;
using MortiseFrame.Rill;

namespace Ping.Protocol {

    public struct EntitiesSyncBroadMessage : IMessage {

        public FVector2 paddle1Pos;
        public FVector2 paddle2Pos;
        public FVector2 ballPos;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<FVector2>(dst, paddle1Pos, ref offset);
            ByteWriter.Write<FVector2>(dst, paddle2Pos, ref offset);
            ByteWriter.Write<FVector2>(dst, ballPos, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            paddle1Pos = ByteReader.Read<FVector2>(src, ref offset);
            paddle2Pos = ByteReader.Read<FVector2>(src, ref offset);
            ballPos = ByteReader.Read<FVector2>(src, ref offset);
        }

    }

}