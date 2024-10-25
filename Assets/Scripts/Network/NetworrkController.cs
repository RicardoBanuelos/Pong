using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkController : NetworkBehaviour
{
    [SerializeField] private GameObject ball;
    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
    }

    private void OnServerStarted()
    {
        GameObject spawned = Instantiate(ball);
        spawned.GetComponent<NetworkObject>().Spawn();
        Debug.Log(" Server Started");
    }
}
