using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public bool isPlayerOneGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!isPlayerOneGoal)
            {
                Debug.Log("Player One Scored!");
                GameObject.Find("GameManager").GetComponent<GameManager>().playerOneScored();
            }
            else
            {
                Debug.Log("Player Two Scored!");
                GameObject.Find("GameManager").GetComponent<GameManager>().playerTwoScored();
            }
        }
    }
}
