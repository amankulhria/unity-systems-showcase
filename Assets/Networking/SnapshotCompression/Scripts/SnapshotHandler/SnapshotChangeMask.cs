[System.Flags]
public enum SnapshotChangeMask : byte
{
    None = 0,
    PositionX = 1 << 0,
    PositionY = 1 << 1,
    PositionZ = 1 << 2,
    CellUpdate = 1 << 3
}