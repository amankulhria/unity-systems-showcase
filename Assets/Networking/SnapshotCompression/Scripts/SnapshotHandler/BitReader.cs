public class BitReader
{
    private byte[] buffer;
    private int bitPosition;
    private int totalBits;

    public int BitsRead => bitPosition;

    public BitReader(byte[] data)
    {
        Reset(data);
    }

    public void Reset(byte[] data)
    {
        buffer = data;
        bitPosition = 0;
        totalBits = data.Length << 3;
    }

    public uint ReadBits(int bitCount)
    {
        uint value = 0;

        for (int i = 0; i < bitCount; i++)
        {
            if (bitPosition >= totalBits)
                break; // defensive: stop reading if data ends

            int byteIndex = bitPosition >> 3;
            int bitIndex = bitPosition & 7;

            uint bit = (uint)((buffer[byteIndex] >> bitIndex) & 1);
            value |= (bit << i);

            bitPosition++;
        }

        return value;
    }
}
