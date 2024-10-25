using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class ControlledPaddle : NetworkBehaviour
{

    private Vector3 startPosition;
    private bool mIsMousePressed;
    private GameObject mLimitUp;
    private GameObject mLimitDown;

    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            mLimitUp = GameObject.FindWithTag("TopWall");
            mLimitDown = GameObject.FindWithTag("DownWall");
            startPosition = transform.position;
        }
    }


    void Update()
    {
        if(!IsOwner)
        {
            return;
        }

        if(mIsMousePressed && IsClient)
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
            //request servere to update
            transform.position = current;

        }
    }

    
    [ClientRpc]
    public void SetStartPositionClientRpc(Vector3 startPosition)
    {
        transform.position = startPosition;
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
}
