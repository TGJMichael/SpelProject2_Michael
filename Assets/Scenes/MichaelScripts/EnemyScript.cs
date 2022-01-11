//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyScript : MonoBehaviour // from Unity Dungeon Crawler Tutorial 2D Game - Part 2 - Automated Enemies - Code and Assets Included
//{
//    private float range;
//    public Transform target;
//    private float minDistance = 5.0f;
//    private bool targetCollision = false;
//    private float speed = 2.0f;
//    private float thrust = 1.5f;
//    public float health = 5;
//    private int hitStrength = 10;

//    public Sprite deathSprite;  // this line are about sprites from the tutorial, changes should be done so that these are better named in our game.
//    public Sprite[] sprites;

//    private GameManager gameManager;  //I comment out these so get rid of error message

//    private bool isDead = false;
//    void Start()
//    {
//        gameManager = GameObject.Find("GameManager").GetComponent<GameManager> ();  //I comment out these so get rid of error message
//    }

//    void Update()
//    {
//        range = Vector2.Distance(transform.position, target.position);
//        if (range < minDistance)
//        {
//            if (!targetCollision)
//            {
//                 Get the position of the player
//                transform.LookAt(target.position);

//                 Correct the rotation
//                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
//                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
//            }
//        }

//    }
//}
