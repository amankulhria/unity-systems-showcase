using UnityEngine;
using System;

public class LocalPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private SnapshotSerializer snapshotSerializer;

    private Vector3Int lastSentCell;
    private Vector3 lastSentLocalPosition;

    public Action<byte[]> OnSnapshotReady;

    private void Start()
    {
        snapshotSerializer = new SnapshotSerializer();

        // Force first snapshot to always send
        lastSentCell = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);
        lastSentLocalPosition = Vector3.positiveInfinity;
    }

    private void Update()
    {
        HandleMovement();
        TrySendSnapshot();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void TrySendSnapshot()
    {
        Vector3 worldPosition = transform.position;

        CalculateCellAndLocalPosition(
            worldPosition,
            out Vector3Int currentCell,
            out Vector3 currentLocalPosition
        );

        byte[] snapshot = snapshotSerializer.BuildSnapshot(
            currentCell,
            currentLocalPosition,
            lastSentCell,
            lastSentLocalPosition
        );

        if (snapshot == null)
            return;

        lastSentCell = currentCell;
        lastSentLocalPosition = currentLocalPosition;

        OnSnapshotReady?.Invoke(snapshot);
    }

    private void CalculateCellAndLocalPosition(
        Vector3 worldPosition,
        out Vector3Int cell,
        out Vector3 localPosition)
    {
        float cellSize = SnapshotSerializer.CELL_SIZE;

        cell = new Vector3Int(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.y / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );

        localPosition = worldPosition - (Vector3)cell * cellSize;
    }
}
