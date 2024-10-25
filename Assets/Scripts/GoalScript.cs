using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GoalScript : NetworkBehaviour
{
    public bool isPlayerOneGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsServer)
        {
            return;
        }
        
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
