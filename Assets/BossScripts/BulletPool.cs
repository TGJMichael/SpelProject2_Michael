using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from https://youtu.be/Mq2zYk5tW_E?t=204
public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstance;

    [SerializeField]
    private GameObject pooledBullet;                // represents a particular bullet that is put in the pool.
    private bool notEnoughBulletsInPool = true;     // will help us add more bullets to the pool when we need more.

    private List<GameObject> bullets;               // list the actual pool of bullets.

    private void Awake()
    {
        bulletPoolInstance = this;                  // assign "bulletPoolInstance" to this gameobject so we can work with this pool from another script and take bullets out of it.
    }
    void Start()
    {
        bullets = new List<GameObject>();           // initialize* a new list to bullets and now it can be filled with bullets. It is strange he says assign but in screen it says initialize* ask jonte about this because i thought initialize was only used when declaring something. https://youtu.be/Mq2zYk5tW_E?t=233
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if (notEnoughBulletsInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }

        return null;
    }
}
