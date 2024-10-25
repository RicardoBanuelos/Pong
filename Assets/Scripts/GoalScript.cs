using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GoalScript : NetworkBehaviour
{
    public bool isPlayerOneGoal;
    private GameObject GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

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
                GameManager.GetComponent<NetworkController>().PlayerOneScoredServer();
            }
            else
            {
                Debug.Log("Player Two Scored!");
                GameManager.GetComponent<NetworkController>().PlayerTwoScoredServer();
            }
        }
    }
}
