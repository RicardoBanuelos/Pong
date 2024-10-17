using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject gameEndedMessage;
    public GameObject playAgainButton;

    [Header("GameField")]
    public GameObject middleLine;


    private int playerOneScore;
    private int playerTwoScore;
    private const int FINAL_SCORE = 11;

    private void Start()
    {
        gameEndedMessage.GetComponent<CanvasElement>().Hide();
        playAgainButton.GetComponent<Button>().onClick.AddListener(playAgain);

        ball.GetComponent<SpriteRenderer>().enabled = false;
        middleLine.GetComponent<SpriteRenderer>().enabled = false;
        Time.timeScale = 0;
    }

    public void playerOneScored()
    {
        ++playerOneScore;
        playerOneScoreText.GetComponent<TextMeshProUGUI>().text = playerOneScore.ToString();

        if (gameEnded())
        {
            DisplayWinner(true);
        }

        ResetPosition();
    }

    public void playerTwoScored()
    {
        ++playerTwoScore;
        playerTwoScoreText.GetComponent<TextMeshProUGUI>().text = playerTwoScore.ToString();

        if (gameEnded())
        {
            DisplayWinner(false);
        }

        ResetPosition();
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        playerOne.GetComponent<Paddle>().Reset();
        playerTwo.GetComponent<Paddle>().Reset();
    }

    private void DisplayWinner(bool playerOneWon)
    {
        ball.GetComponent<SpriteRenderer>().enabled = false;
        middleLine.GetComponent<SpriteRenderer>().enabled = false;

        gameEndedMessage.GetComponent<CanvasElement>().Show();
        
        playAgainButton.GetComponent<CanvasElement>().Show();
        playAgainButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Again";

        gameEndedMessage.GetComponent<TextMeshProUGUI>().text = playerOneWon ? "Player One Won!!!" : "Player Two Won!!!";

        Time.timeScale = 0;
    }

    private void playAgain()
    {
        gameEndedMessage.GetComponent<CanvasElement>().Hide();
        playAgainButton.GetComponent<CanvasElement>().Hide();

        ball.GetComponent<SpriteRenderer>().enabled = true;
        middleLine.GetComponent<SpriteRenderer>().enabled = true;

        restartGame();
        Time.timeScale = 1;
    }

    private bool gameEnded()
    {
        return playerOneScore >= FINAL_SCORE || playerTwoScore >= FINAL_SCORE;
    }

    private void restartGame()
    {
        playerOneScore = 0;
        playerTwoScore = 0;

        playerOneScoreText.GetComponent<TextMeshProUGUI>().text = "0";
        playerTwoScoreText.GetComponent<TextMeshProUGUI>().text = "0";
    }
}
