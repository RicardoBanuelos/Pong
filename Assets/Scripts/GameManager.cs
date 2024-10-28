using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject canvas;
    private GameObject mBall; //Server

    private int mPlayerOneScore = 0;
    private int mPlayerTwoScore = 0;

    private readonly int MAX_SCORE = 1;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        }
    }

    private void OnClientConnectedCallback(ulong obj)
    {
        IReadOnlyDictionary<ulong, NetworkClient> players = NetworkManager.Singleton.ConnectedClients;

        if (players.Count == 2)
        {
            players[obj].PlayerObject.GetComponent<PlayerController>().SetPositionClientRpc(new Vector3(8, 0, 0));
            players[obj].PlayerObject.GetComponent<PlayerController>().PlayerId = 2;
            LaunchBall();
        }
    }

    public void AddPlayerOneScore()
    {
        ++mPlayerOneScore;
        canvas.GetComponent<CanvasController>().UpdatePlayerOneCountClientRpc(mPlayerOneScore, mPlayerTwoScore);
        ResetBall();

        if (mPlayerOneScore >= MAX_SCORE)
        {
            SetPlayerWinner(1);
        }
    }

    public void AddPlayerTwoScore()
    {
        ++mPlayerTwoScore;
        canvas.GetComponent<CanvasController>().UpdatePlayerTwoCountClientRpc(mPlayerTwoScore, mPlayerOneScore);
        ResetBall();

        if (mPlayerTwoScore >= MAX_SCORE)
        {
            SetPlayerWinner(2);
        }
    }

   

    private void SetPlayerWinner(int winner)
    {

        NetworkClient winnerClient = GetPlayer(winner);

        ClientRpcParams clientWinnerRpcParams = new()
        {
            Send = new (){
                TargetClientIds = new ulong[]{winnerClient.ClientId}
            }
        };

        SetMiddleTextClientRpc("You Win", clientWinnerRpcParams);

        NetworkClient looserClient = GetPlayer(winner == 1 ? 2 : 1);
        ClientRpcParams clientLooserRpcParams = new()
        {
            Send = new (){
                TargetClientIds = new ulong[]{looserClient.ClientId}
            }
        };

        SetMiddleTextClientRpc("You lost", clientLooserRpcParams);   
        mBall.GetComponent<NetworkObject>().Despawn();

        canvas.GetComponent<CanvasController>().ShowAgainButtonClientRpc();
    }


    private NetworkClient GetPlayer(int playerId)
    {
        IReadOnlyList<NetworkClient> players = NetworkManager.Singleton.ConnectedClientsList;
        foreach (NetworkClient client in players)
        {
            if(client.PlayerObject.GetComponent<PlayerController>().PlayerId == playerId)
            {
                return client;
            }
        }

        return null;
    }

    [ClientRpc]
    void SetMiddleTextClientRpc(string text, ClientRpcParams clientRpcParams)
    {
        canvas.GetComponent<CanvasController>().SetMiddleText(text, clientRpcParams);
    }

    [ContextMenu("Launch Ball")]
    void LaunchBall()
    {
        mBall = Instantiate(ballPrefab);
        mBall.GetComponent<NetworkObject>().Spawn();
    }

    void ResetBall()
    {
        mBall.GetComponent<Ball>().Reset();
    }

    void Reset()
    {
        LaunchBall();
        ResetBall();
    }

    [ServerRpc]
    public void ResetServerRpc()
    {
        Reset();
    }

}
