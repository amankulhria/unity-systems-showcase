using UnityEngine;

public class SnapshotSerializer
{
    public const float CELL_SIZE = 2000f;

    private const float MIN_POS = -1000f;
    private const float MAX_POS = 1000f;
    private const float INV_RANGE = 1f / (MAX_POS - MIN_POS);

    private const int POSITION_BITS = 17;
    private const int POSITION_MASK = (1 << POSITION_BITS) - 1;

    private const int CELL_BITS = 8;
    private const int CELL_OFFSET = 1 << (CELL_BITS - 1);

    private const float POSITION_EPSILON = 0.001f;

    private const int MAX_PACKET_BYTES = 32;

    private readonly byte[] buffer;
    private readonly BitWriter writer;

    private byte sequenceId;

    public SnapshotSerializer()
    {
        buffer = new byte[MAX_PACKET_BYTES];
        writer = new BitWriter(buffer);
        sequenceId = 0;
    }

    public byte[] BuildSnapshot(
        Vector3Int cell,
        Vector3 localPos,
        Vector3Int lastCell,
        Vector3 lastLocalPos)
    {
        SnapshotChangeMask mask = SnapshotChangeMask.None;

        if (cell != lastCell)
            mask |= SnapshotChangeMask.CellUpdate;

        if (Mathf.Abs(localPos.x - lastLocalPos.x) > POSITION_EPSILON)
            mask |= SnapshotChangeMask.PositionX;

        if (Mathf.Abs(localPos.y - lastLocalPos.y) > POSITION_EPSILON)
            mask |= SnapshotChangeMask.PositionY;

        if (Mathf.Abs(localPos.z - lastLocalPos.z) > POSITION_EPSILON)
            mask |= SnapshotChangeMask.PositionZ;

        if (mask == SnapshotChangeMask.None)
            return null;

        writer.Reset();

        // Sequence ID (8 bits)
        writer.WriteBits(sequenceId++, 8);

        // Change mask (4 bits)
        writer.WriteBits((byte)mask, 4);

        if ((mask & SnapshotChangeMask.CellUpdate) != 0)
        {
            WriteCell(cell);
        }

        if ((mask & SnapshotChangeMask.PositionX) != 0) WriteFloat(localPos.x);
        if ((mask & SnapshotChangeMask.PositionY) != 0) WriteFloat(localPos.y);
        if ((mask & SnapshotChangeMask.PositionZ) != 0) WriteFloat(localPos.z);

        int byteCount = writer.GetByteCount();
        byte[] packet = new byte[byteCount];
        System.Buffer.BlockCopy(buffer, 0, packet, 0, byteCount);

        #if UNITY_EDITOR
        Debug.Log($"<color=green>[BUILD]</color> Position:{localPos} | Bits:{writer.BitsWritten}");
        #endif

        return packet;
    }

    public SnapshotState ReadSnapshot(byte[] data, SnapshotState previous)
    {
        BitReader reader = new BitReader(data);

        byte seq = (byte)reader.ReadBits(8);
        previous.SequenceId = seq;

        SnapshotChangeMask mask = (SnapshotChangeMask)reader.ReadBits(4);

        SnapshotState state = previous;

        if ((mask & SnapshotChangeMask.CellUpdate) != 0)
        {
            state.Cell = ReadCell(reader);
        }

        Vector3 local = state.LocalPosition;

        if ((mask & SnapshotChangeMask.PositionX) != 0) local.x = ReadFloat(reader);
        if ((mask & SnapshotChangeMask.PositionY) != 0) local.y = ReadFloat(reader);
        if ((mask & SnapshotChangeMask.PositionZ) != 0) local.z = ReadFloat(reader);

        state.LocalPosition = local;

        #if UNITY_EDITOR
        Debug.Log($"<color=cyan>[READ]</color> Position:{state.LocalPosition}");
        #endif

        return state;
    }

    private void WriteFloat(float value)
    {
        float normalized = Mathf.Clamp01((value - MIN_POS) * INV_RANGE);
        writer.WriteBits((uint)(normalized * POSITION_MASK), POSITION_BITS);
    }

    private float ReadFloat(BitReader reader)
    {
        float normalized = reader.ReadBits(POSITION_BITS) / (float)POSITION_MASK;
        return MIN_POS + normalized * (MAX_POS - MIN_POS);
    }

    private void WriteCell(Vector3Int cell)
    {
        writer.WriteBits((uint)(cell.x + CELL_OFFSET), CELL_BITS);
        writer.WriteBits((uint)(cell.y + CELL_OFFSET), CELL_BITS);
        writer.WriteBits((uint)(cell.z + CELL_OFFSET), CELL_BITS);
    }

    private Vector3Int ReadCell(BitReader reader)
    {
        int x = (int)reader.ReadBits(CELL_BITS) - CELL_OFFSET;
        int y = (int)reader.ReadBits(CELL_BITS) - CELL_OFFSET;
        int z = (int)reader.ReadBits(CELL_BITS) - CELL_OFFSET;
        return new Vector3Int(x, y, z);
    }
}
