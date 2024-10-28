using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    public float speed;
    private Rigidbody2D mRigidBody;
    public Vector3 startPosition;


    public override void OnNetworkSpawn()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position; 
        Launch();
    }

    public void Reset()
    {
  
        mRigidBody.linearVelocity = Vector2.zero;
        transform.position = startPosition;
        Launch();
        
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        mRigidBody.linearVelocity = new Vector2(speed * x, speed * y);
        
    }

}
