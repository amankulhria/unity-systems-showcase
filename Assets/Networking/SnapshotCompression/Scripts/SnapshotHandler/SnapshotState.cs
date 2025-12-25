using UnityEngine;

public struct SnapshotState
{
    public byte SequenceId;
    public Vector3Int Cell;
    public Vector3 LocalPosition;

    public Vector3 GetWorldPosition(float cellSize)
    {
        return (Vector3)Cell * cellSize + LocalPosition;
    }
}
