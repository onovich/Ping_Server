using System;
using MortiseFrame.LitIO;

namespace Ping.Protocol {

    public struct RoomStartGameBroadMessage : IMessage<JoinRoomResMessage> {
        public int[] roleTypeIDArray;
        public int[] idArray;
        public void WriteTo(byte[] dst, ref int offset) {
            ByteWritter.WriteArray<int>(dst, roleTypeIDArray, ref offset);
            ByteWritter.WriteArray<int>(dst, idArray, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            roleTypeIDArray = ByteReader.ReadArray<int>(src, ref offset);
            idArray = ByteReader.ReadArray<int>(src, ref offset);
        }

        public int GetEvaluatedSize(out bool isCertain) {
            int count = 4;
            isCertain = false;
            if (roleTypeIDArray != null) {
                count += roleTypeIDArray.Length * 4;
            }

            if (idArray != null) {
                count += idArray.Length * 4;
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