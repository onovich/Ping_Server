using System;
using MortiseFrame.LitIO;
using MortiseFrame.Abacus;

namespace Ping.Protocol {

    public struct PaddleSyncBroadMessage : IMessage<PaddleSyncBroadMessage> {

        public int[] paddleIds;
        public Vector2[] paddlePos;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWritter.Write<int>(dst, paddleIds.Length, ref offset);
            for (int i = 0; i < paddleIds.Length; i++) {
                ByteWritter.Write<int>(dst, paddleIds[i], ref offset);
                ByteWritter.Write<Vector2>(dst, paddlePos[i], ref offset);
            }
        }

        public void FromBytes(byte[] src, ref int offset) {
            int count = ByteReader.Read<int>(src, ref offset);
            paddleIds = new int[count];
            paddlePos = new Vector2[count];
            for (int i = 0; i < count; i++) {
                paddleIds[i] = ByteReader.Read<int>(src, ref offset);
                paddlePos[i] = ByteReader.Read<Vector2>(src, ref offset);
            }
        }

        public int GetEvaluatedSize(out bool isCertain) {
            int count = 8;
            isCertain = false;
            if (paddleIds != null) {
                count += paddleIds.Length * 4;
            }
            if (paddlePos != null) {
                count += paddlePos.Length * 8;
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