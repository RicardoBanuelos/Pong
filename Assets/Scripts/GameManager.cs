using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{


    [Header("PlayerOne")]
    public GameObject playerOneGoal;

    [Header("PlayerTwo")]
    public GameObject playerTwoGoal;

    [Header("ScoreUI")]
    public GameObject playerOneScoreText;
    public GameObject playerTwoScoreText;

    private NetworkVariable<int> playerOneScore = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<int> playerTwoScore = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public NetworkVariable<int> PlayerOneScore { get => playerOneScore; set => playerOneScore = value; }
    public NetworkVariable<int> PlayerTwoScore { get => playerTwoScore; set => playerTwoScore = value; }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsServer)
        {
            
        }
        
    }

    public void Start() 
    {

    }
    public void playerOneScored()
    {
        if(IsServer)
        {
            ++playerOneScore.Value;
        }

        //playerOneScoreText.GetComponent<TextMeshProUGUI>().text = playerOneScore.Value.ToString();
        ResetPosition();
    }

    public void playerTwoScored()
    {
        if(IsServer)
        {
            ++playerTwoScore.Value;
        }

        //playerTwoScoreText.GetComponent<TextMeshProUGUI>().text = playerTwoScore.Value.ToString();
        ResetPosition();
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
