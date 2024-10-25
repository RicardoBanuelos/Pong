using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody;
    public Vector3 startPosition;

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            enabled = false;
        }
    }

    private void OnClientConnectedCallback(ulong obj)
    {
        if(NetworkManager.Singleton.ConnectedClientsList.Count == 2)
        {
            enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        if(IsServer)
        {
            startPosition = transform.position; 
            Launch();
        }
    }

    public void Reset()
    {
        Debug.Log("Reset");
        if(IsServer)
        {
            rigidBody.velocity = Vector2.zero;
            transform.position = startPosition;
            Launch();
        }
    }

    private void Launch()
    {
        if(IsServer)
        {
            float x = Random.Range(0, 2) == 0 ? -1 : 1;
            float y = Random.Range(0, 2) == 0 ? -1 : 1;
            rigidBody.velocity = new Vector2(speed * x, speed * y);
        }
    }
}
