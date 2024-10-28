using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ServerButtonsController : MonoBehaviour
{
    private GameObject mHostButton;
    private GameObject mClientButton;
    
    private GameObject mServerButton;
    

    public void Start()
    {
        mHostButton = GetComponentInChildren<IsHostButon>().gameObject;
        mClientButton = GetComponentInChildren<IsClientButton>().gameObject;
        mServerButton = GetComponentInChildren<IsServerButton>().gameObject;

        mHostButton.GetComponent<Button>().onClick.AddListener(StartHost);
        mClientButton.GetComponent<Button>().onClick.AddListener(StartClient);
        mServerButton.GetComponent<Button>().onClick.AddListener(StartServer);
        
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        HideButtons();
    }

    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        HideButtons();
    }

    private void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        HideButtons();
    }

    private void HideButtons()
    {
        mHostButton.SetActive(false);
        mClientButton.SetActive(false);
        mServerButton.SetActive(false);
    }
}
