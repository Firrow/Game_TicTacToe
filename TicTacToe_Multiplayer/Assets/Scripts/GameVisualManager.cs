using Unity.Netcode;
using UnityEngine;

public class GameVisualManager : NetworkBehaviour
{
    private const float GRID_SIZE = 3.1f;

    [SerializeField] private Transform crossPrefab;
    [SerializeField] private Transform circlePrefab;
    [SerializeField] private Transform lineCompletePrefab;


    private void Start()
    {
        GameManager.Instance.OnClickedOnGridPosition += GameManager_OnClickedOnGridPosition;
        GameManager.Instance.OnGameWin += GameManager_OnGameWin;
    }

    private void GameManager_OnClickedOnGridPosition(object sender, GameManager.OnClickedOnGridPositionEventArgs e)
    {
        Debug.Log("GameManager_OnClickedOnGridPosition");
        SpawnObjectRpc(e.x, e.y, e.playerType);
    }

    private void GameManager_OnGameWin(object sender, GameManager.OnGameWinEventArgs e)
    {
        float eulerZ = 0.0f;
        switch (e.line.orientation)
        {
            case GameManager.Orientation.Horizontal:
                eulerZ = 0.0f;
                break;
            case GameManager.Orientation.Vertical:
                eulerZ = 90.0f;
                break;
            case GameManager.Orientation.DiagonalA:
                eulerZ = 45.0f;
                break;
            case GameManager.Orientation.DiagonalB:
                eulerZ = -45.0f;
                break;
            default:
                break;
        }
        Transform lineCompleteTransform = Instantiate(
            lineCompletePrefab, 
            GetGridWorldPosition(e.line.centerGridPosition.x, e.line.centerGridPosition.y), 
            Quaternion.Euler(0, 0, eulerZ)
        );
        lineCompleteTransform.GetComponent<NetworkObject>().Spawn(true);
    }

    [Rpc(SendTo.Server)] // cette fonction sera appelée par le serveur lorsque le client va appeler GameManager_OnClickedOnGridPosition 
    private void SpawnObjectRpc(int x, int y, GameManager.PlayerType playerType)
    {
        Debug.Log("SpawnObject");
        Transform prefab = null;
        switch (playerType)
        {
            case GameManager.PlayerType.Cross:
                prefab = crossPrefab;
                break;
            case GameManager.PlayerType.Circle:
                prefab = circlePrefab;
                break;
            default:
                return;
        }
        Transform spawnedCrossTransform = Instantiate(prefab, GetGridWorldPosition(x, y), Quaternion.identity);
        spawnedCrossTransform.GetComponent<NetworkObject>().Spawn(true); // sera spawn chez tous les clients
    }

    private Vector2 GetGridWorldPosition(int x, int y)
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE, -GRID_SIZE + y * GRID_SIZE);
    }
}