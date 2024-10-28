using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private bool isPlayerOne = false;
    [SerializeField] private GameObject gameManager;
    private void OnCollisionEnter2D(Collision2D other) 
    {

        if(isPlayerOne)
        {
            gameManager.GetComponent<GameManager>().AddPlayerTwoScore();  
        }
        else 
        {
            gameManager.GetComponent<GameManager>().AddPlayerOneScore();  
        }
    }
}
