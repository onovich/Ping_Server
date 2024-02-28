namespace Ping.Protocol {

    public interface IMessage<T> {

        void WriteTo(byte[] dst, ref int offset);
        void FromBytes(byte[] src, ref int offset);
        byte[] ToBytes();
        int GetEvaluatedSize(out bool isCertain);

    }

}