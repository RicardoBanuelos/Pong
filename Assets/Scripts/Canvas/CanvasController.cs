using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class CanvasController : NetworkBehaviour
{
    private GameObject mMiddleText;
    private GameObject mPlayerOneScore;
    private GameObject mPlayerTwoScore;
    private GameObject mAgainButton;
    
    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        }
        
        mMiddleText = GetComponentInChildren<MiddleText>().gameObject;
        mPlayerOneScore = GetComponentInChildren<PlayerOneScore>().gameObject;
        mPlayerTwoScore = GetComponentInChildren<PlayerTwoScore>().gameObject;
        mAgainButton = GetComponentInChildren<AgainButton>().gameObject;

        mAgainButton.SetActive(false);
    }

    private void OnClientConnectedCallback(ulong obj)
    {
        int clients = NetworkManager.Singleton.ConnectedClientsList.Count;

        if(clients == 1)
        {
            SetMiddleTextClientRpc("Waiting for player...");
        }
        else 
        {
            SetMiddleTextClientRpc("");
        }
        
    }

    public void SetMiddleText(string text, ClientRpcParams clientRpcParams)
    {
        mMiddleText.GetComponent<TextMeshProUGUI>().text = text;
    }

    [ClientRpc]
    public void ShowAgainButtonClientRpc()
    {
        mAgainButton.SetActive(true);
    }

    [ClientRpc]
    public void HideAgainButtonClientRpc()
    {
        mAgainButton.SetActive(false);
    }

    [ClientRpc]
    public void SetMiddleTextClientRpc(string text, ClientRpcParams clientRpcParams = default)
    {
        mMiddleText.GetComponent<TextMeshProUGUI>().text = text;
    }

    [ClientRpc]
    public void UpdatePlayerOneCountClientRpc(int count, int playerTwoScore)
    {
        mPlayerOneScore.GetComponent<TextMeshProUGUI>().text = count.ToString();
        mPlayerTwoScore.GetComponent<TextMeshProUGUI>().text = playerTwoScore.ToString();
    }

    [ClientRpc]
    public void UpdatePlayerTwoCountClientRpc(int count, int playerOneScore)
    {
        mPlayerTwoScore.GetComponent<TextMeshProUGUI>().text = count.ToString();
        mPlayerOneScore.GetComponent<TextMeshProUGUI>().text = playerOneScore.ToString();
    }


}
