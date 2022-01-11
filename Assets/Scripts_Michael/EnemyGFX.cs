using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
//Script for flipping the sprite, this might not be needed if we can change the direction and such in the animator.
public class EnemyGFX : MonoBehaviour
{
    public AIPath aidPath;

   
    void Update()
    {
        // this only works in brackeys sidescroller.
        if(aidPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aidPath.desiredVelocity.x <= -0.01f)
        {

            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
