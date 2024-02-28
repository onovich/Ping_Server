using MortiseFrame.LitIO;
using MortiseFrame.Abacus;

namespace Ping.Protocol {

    public struct PaddleMoveReqMessage : IMessage<PaddleMoveReqMessage> {

        public Vector2 moveAxis;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWritter.Write<Vector2>(dst, moveAxis, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            moveAxis = ByteReader.Read<Vector2>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            int count = 8;
            isCertain = true;
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