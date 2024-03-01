using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct JoinRoomBroadMessage : IMessage<JoinRoomBroadMessage> {

        public sbyte status; // 1 为成功, -1 为失败
        public byte[] playerIDs;
        public string[] userNames;

        public void WriteTo(byte[] dst, ref int offset) {
            ByteWriter.Write<sbyte>(dst, status, ref offset);
            ByteWriter.WriteArray<byte>(dst, playerIDs, ref offset);
            ByteWriter.WriteUTF8StringArray(dst, userNames, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            status = ByteReader.Read<sbyte>(src, ref offset);
            playerIDs = ByteReader.ReadArray<byte>(src, ref offset);
            userNames = ByteReader.ReadUTF8StringArray(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            isCertain = false;
            int count = ByteCounter.Count<sbyte>()
            + ByteCounter.CountArray<byte>(playerIDs)
            + ByteCounter.CountUTF8StringArray(userNames);
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