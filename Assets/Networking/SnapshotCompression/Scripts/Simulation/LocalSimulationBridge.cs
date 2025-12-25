using UnityEngine;

public class LocalSimulationBridge : MonoBehaviour
{
    [SerializeField] private LocalPlayerController localPlayer;
    [SerializeField] private RemotePlayerView remotePlayer;

    private void Awake()
    {
        if (localPlayer == null || remotePlayer == null)
        {
            Debug.LogError("LocalSimulationBridge is missing references.");
            enabled = false;
            return;
        }

        localPlayer.OnSnapshotReady = remotePlayer.ApplySnapshot;
    }
}
