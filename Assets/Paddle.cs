using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rigidBody;
    public Vector3 startPosition;
    private float movement;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movement = isPlayer1 ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("VerticalTwo");
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, movement * speed);
    }

    public void Reset()
    {
        rigidBody.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
