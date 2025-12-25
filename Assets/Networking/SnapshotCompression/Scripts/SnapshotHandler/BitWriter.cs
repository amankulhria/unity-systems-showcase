public class BitWriter
{
    private readonly byte[] buffer;
    private int bitPosition;
    private readonly int totalBits;

    public int BitsWritten => bitPosition;

    public BitWriter(byte[] targetBuffer)
    {
        buffer = targetBuffer;
        totalBits = targetBuffer.Length << 3;
        Reset();
    }

    public void Reset()
    {
        bitPosition = 0;
        System.Array.Clear(buffer, 0, buffer.Length);
    }

    public void WriteBits(uint value, int bitCount)
    {
        for (int i = 0; i < bitCount; i++)
        {
            if (bitPosition >= totalBits)
                break;

            int byteIndex = bitPosition >> 3;
            int bitIndex = bitPosition & 7;

            uint bit = (value >> i) & 1u;

            if (bit == 1u)
                buffer[byteIndex] |= (byte)(1 << bitIndex);

            bitPosition++;
        }
    }

    public int GetByteCount()
    {
        return (bitPosition + 7) >> 3;
    }

    public byte[] GetBuffer()
    {
        return buffer;
    }
}
