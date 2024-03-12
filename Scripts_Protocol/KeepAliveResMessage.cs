using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct KeepAliveResMessage : IMessage<ConnectResMessage> {

        public float timestamp;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<float>(dst, timestamp, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            timestamp = ByteReader.Read<float>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            int count = ByteCounter.Count<float>();
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