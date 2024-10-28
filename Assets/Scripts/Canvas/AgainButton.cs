using Unity.Netcode;
using UnityEngine;

public class AgainButton : NetworkBehaviour
{
    [SerializeField] GameObject gameManager;

    public override void OnNetworkSpawn()
    {
        
    }

    public void OnClick()
    {
        gameManager.GetComponent<GameManager>().ResetServerRpc();
    }

}
