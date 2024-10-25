using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkController : NetworkBehaviour
{
    [SerializeField] private GameObject ball;
    
    private GameObject playerOneScoreText;
    private GameObject playerTwoScoreText;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        }

        playerOneScoreText = GameObject.Find("PlayerOneScore");
        playerTwoScoreText = GameObject.Find("PlayerTwoScore");
        
    }
    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        
    }

    private void OnClientConnectedCallback(ulong obj)
    {
        if(NetworkManager.Singleton.ConnectedClients.ContainsKey(obj))
        {
            var client = NetworkManager.Singleton.ConnectedClients.GetValueOrDefault(obj);
            if(client.PlayerObject.TryGetComponent(out ControlledPaddle player))
            {
                int clientCount = NetworkManager.Singleton.ConnectedClientsList.Count;
               
                if(clientCount == 1)
                {

                    player.SetStartPositionClientRpc(new Vector3(-8,0,0));
                }
                else 
                {
                    player.SetStartPositionClientRpc(new Vector3(8,0,0));
                }
            }
        }
    }

    public void PlayerOneScoredServer()
    {
        if(!IsServer)
        {
            return;
        }

        playerOneScore++;
        playerOneScoreText.GetComponent<TextMeshProUGUI>().text = playerOneScore.ToString();
        UpdateValuesClientRpc(playerOneScore, playerTwoScore);
        ResetPosition();
    }

    public void PlayerTwoScoredServer()
    {
        if(!IsServer)
        {
            return;
        }
        playerTwoScore++;
        playerTwoScoreText.GetComponent<TextMeshProUGUI>().text = playerTwoScore.ToString();
        UpdateValuesClientRpc(playerOneScore, playerTwoScore);
        ResetPosition();
    }


    [ClientRpc]
    void UpdateValuesClientRpc(int playerOne, int playerTwo)
    {
        Debug.Log("UpdateValuesClientRpc");
        playerOneScoreText.GetComponent<TextMeshProUGUI>().text = playerOne.ToString();
        playerTwoScoreText.GetComponent<TextMeshProUGUI>().text = playerTwo.ToString();
    }

    private void OnServerStarted()
    {
        GameObject spawned = Instantiate(ball);
        spawned.GetComponent<NetworkObject>().Spawn();
    }

    private void ResetPosition()
    {
        if(IsServer)
        {
            GameObject ball = GameObject.FindWithTag("Ball");
            ball.GetComponent<Ball>().Reset();
        }
    }
}
