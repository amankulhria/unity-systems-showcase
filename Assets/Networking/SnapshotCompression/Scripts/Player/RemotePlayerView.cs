using UnityEngine;

public class RemotePlayerView : MonoBehaviour
{
    [SerializeField] private Vector3 visualOffset = new Vector3(10f, 0f, -10f);
    [SerializeField] private float smoothSpeed = 10f;

    private SnapshotSerializer snapshotSerializer;

    private SnapshotState currentState;
    private byte lastSequenceId;

    private Vector3 targetWorldPosition;
    private bool hasReceivedFirstSnapshot;

    private void Start()
    {
        snapshotSerializer = new SnapshotSerializer();
        hasReceivedFirstSnapshot = false;
        lastSequenceId = 0;
    }

    public void ApplySnapshot(byte[] data)
    {
        if (data == null)
            return;

        SnapshotState newState = snapshotSerializer.ReadSnapshot(data, currentState);

        if (hasReceivedFirstSnapshot && !IsNewer(newState.SequenceId, lastSequenceId))
            return;

        lastSequenceId = newState.SequenceId;
        currentState = newState;

        targetWorldPosition =
            currentState.GetWorldPosition(SnapshotSerializer.CELL_SIZE) + visualOffset;

        if (!hasReceivedFirstSnapshot)
        {
            hasReceivedFirstSnapshot = true;
            transform.position = targetWorldPosition;
        }
    }

    private void Update()
    {
        if (!hasReceivedFirstSnapshot)
            return;

        transform.position = Vector3.Lerp(
            transform.position,
            targetWorldPosition,
            smoothSpeed * Time.deltaTime
        );
    }

    private bool IsNewer(byte newSeq, byte lastSeq)
    {
        return (byte)(newSeq - lastSeq) < 128;
    }

}
