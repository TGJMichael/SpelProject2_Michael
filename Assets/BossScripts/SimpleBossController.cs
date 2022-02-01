using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBossController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private bool moveRight;
    void Start()
    {
        moveRight = true;   // is there a difference in assigning value here instead of 
        
    }

    void Update()
    {
        if (transform.position.x > 7f)  // 7f is how far to the right the boss moves before
        {
            moveRight = false;          // bool sets to false wich will result in boss start moving to the left
        }
        else if (transform.position.x < -7f)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);  // moving right on x position, left position stays constant
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);  // moving left "-   -   -   -   -   -   -   -   -   -   -"
        }
    }
}
