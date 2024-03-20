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

    }

}