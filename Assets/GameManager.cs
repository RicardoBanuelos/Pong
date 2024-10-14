using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("PlayerOne")]
    public GameObject playerOne;
    public GameObject playerOneGoal;

    [Header("PlayerTwo")]
    public GameObject playerTwo;
    public GameObject playerTwoGoal;

    [Header("ScoreUI")]
    public GameObject playerOneScoreText;
    public GameObject playerTwoScoreText;

    private int playerOneScore;
    private int playerTwoScore;

    public void playerOneScored()
    {
        ++playerOneScore;
        playerOneScoreText.GetComponent<TextMeshProUGUI>().text = playerOneScore.ToString();
        ResetPosition();
    }

    public void playerTwoScored()
    {
        ++playerTwoScore;
        playerTwoScoreText.GetComponent<TextMeshProUGUI>().text = playerTwoScore.ToString();
        ResetPosition();
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        playerOne.GetComponent<Paddle>().Reset();
        playerTwo.GetComponent<Paddle>().Reset();
    }
}
