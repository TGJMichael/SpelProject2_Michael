using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{

    // config params
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    
    // state variables
    [SerializeField] int timesHit;  // TODO only serialized for debug purposes

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //HandleHit();
        if (collision.gameObject.tag == "Projectile")
        {
            HandleHit();
        }
        else 
        { 
            print ("nope");
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            return;
            //DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }
    }

    public void MeleeHit(bool AttackHit)
    {
        if (AttackHit)
        {
            HandleHit();
            AttackHit = false;
        }
    }


    /*
    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
    */
}
