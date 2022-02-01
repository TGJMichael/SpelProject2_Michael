using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    // config params
    [SerializeField] Sprite[] hitSprites;
    
    // state variables
    [SerializeField] int timesHit;

    //SFX
    [SerializeField] AudioClip[] breakSounds;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //HandleHit();
        if (collision.gameObject.tag == "Projectile")
        {
            HandleHit();
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

            //SFX
            AudioClip clip = breakSounds[UnityEngine.Random.Range(0, breakSounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);   
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
   
}
