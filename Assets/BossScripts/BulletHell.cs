using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);  // activate Destroy() method in 3 seconds.
    }
    void Start()
    {
        moveSpeed = 5f;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);        
    }

    public void SetMoveDirection(Vector2 dir)   // dir allows to set bullet moveDirection from another script, in can use this in the projectilescripts if i want to change from how i've done it so far.
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);  // set to inactive state and waits for another chanse to fly.
    }

    private void OnDisable()
    {
        CancelInvoke();  // cancel invokation  of Destroy() method to avoid possible issues with invokation of method when object is in an active state. 
    }
}
