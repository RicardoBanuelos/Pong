using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rigidBody;
    public Vector3 startPosition;
    private bool mIsMousePressed;

    [SerializeField]
    private GameObject mLimitUp;

    [SerializeField]
    private GameObject mLimitDown;
    
    [SerializeField]
    private float mOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(mIsMousePressed)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 current = transform.position;
            current.y = worldPoint.y;

            if(current.y + mOffset < mLimitUp.transform.position.y && current.y - mOffset > mLimitDown.transform.position.y)
            {
                transform.position = current;
            }

        }
    }

    public void Reset()
    {
        rigidBody.velocity = Vector2.zero;
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
