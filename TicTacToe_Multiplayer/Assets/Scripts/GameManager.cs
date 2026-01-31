using System;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    // SINGLETON
    public static GameManager Instance {  get; private set; }

    public event EventHandler<OnClickedOnGridPositionEventArgs> OnClickedOnGridPosition;
    public class OnClickedOnGridPositionEventArgs : EventArgs
    {
        public int x;
        public int y;
        public PlayerType playerType;
    }

    public enum PlayerType
    {
        None,
        Cross,
        Circle
    }
    private PlayerType localPlayerType;

    private PlayerType currentPlayablePlayerType;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameManager instance! ");
        }

        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        Debug.Log("Client ID : " + NetworkManager.Singleton.LocalClientId);
        if (NetworkManager.Singleton.LocalClientId == 0)
        {
            localPlayerType = PlayerType.Circle;
        }
        else
        {
            localPlayerType = PlayerType.Cross;
        }

        if (IsServer)
        {
            currentPlayablePlayerType = PlayerType.Cross;
        }
    }

    [Rpc(SendTo.Server)]
    public void ClickedOnGridPositionRpc(int x, int y, PlayerType playerType)
    {
        if (playerType != currentPlayablePlayerType) 
        {
            return;
        }

        OnClickedOnGridPosition?.Invoke(this, new OnClickedOnGridPositionEventArgs
        {
            x = x,
            y = y,
            playerType = playerType
        });

        switch (currentPlayablePlayerType)
        {
            case PlayerType.Cross:
                currentPlayablePlayerType = PlayerType.Circle;
                break;
            case PlayerType.Circle:
                currentPlayablePlayerType = PlayerType.Cross;
                break;
            default:
                break;
        }
    }



    public PlayerType GetLocalPlayerType()
    {
        return localPlayerType;
    }
}
