using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{


    [Header("PlayerOne")]
    // public GameObject playerOne;
    public GameObject playerOneGoal;

    [Header("PlayerTwo")]
    // public GameObject playerTwo;
    public GameObject playerTwoGoal;

    [Header("ScoreUI")]
    public GameObject playerOneScoreText;
    public GameObject playerTwoScoreText;

    private int playerOneScore;
    private int playerTwoScore;

    public void Start() 
    {

    }

    public void playerOneScored()
    {
        if(IsServer)
        {
            ++playerOneScore;
            playerOneScoreText.GetComponent<TextMeshProUGUI>().text = playerOneScore.ToString();
            ResetPosition();
        }

    }

    public void playerTwoScored()
    {
        if(IsServer)
        {
            ++playerTwoScore;
            playerTwoScoreText.GetComponent<TextMeshProUGUI>().text = playerTwoScore.ToString();
            ResetPosition();
        }
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
