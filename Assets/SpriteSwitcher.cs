using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public AIPath aidPath;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (aidPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aidPath.desiredVelocity.x <= -0.01f)
        {

            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aidPath.desiredVelocity.y <= -0.01f)
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        else if (aidPath.desiredVelocity.y >= 0.01f)
        {
            print("going up");
            spriteRenderer.sprite = spriteArray[3];
        }
    }
}
