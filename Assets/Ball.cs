using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody;
    public Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; 
        Launch();
    }

    public void Reset()
    {
        rigidBody.velocity = Vector2.zero;
        transform.position = startPosition;
        Launch();
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rigidBody.velocity = new Vector2(speed * x, speed * y);
    }
}
