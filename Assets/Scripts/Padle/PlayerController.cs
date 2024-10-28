using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : NetworkBehaviour
{

    private Vector3 startPosition;
    private GameObject mLimitUp;
    private GameObject mLimitDown;
    private bool mIsMousePressed;
    private int mPlayerId = 1;



    public override void OnNetworkSpawn()
    {
        mLimitUp = GameObject.FindWithTag("TopWall");
        mLimitDown = GameObject.FindWithTag("DownWall");
        startPosition = transform.position;
    }


    void Update()
    {
        if(mIsMousePressed)
        {
            Move();
        }
    }


    void Move()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 current = transform.position;
        current.y = worldPoint.y;

        if(current.y < mLimitUp.transform.position.y && current.y > mLimitDown.transform.position.y)
        {
            transform.position = current;
        }
    }

    [ClientRpc]
    public void SetPositionClientRpc(Vector3 position)
    {
        transform.position = position;
    }

    public void Reset()
    {
        transform.position = startPosition;
    }

    private void OnMouseDown() 
    {
        mIsMousePressed = true;  
    }

    private void OnMouseUp() {
        mIsMousePressed = false;
        
    }

    public int PlayerId { get => mPlayerId; set => mPlayerId = value; }
}
