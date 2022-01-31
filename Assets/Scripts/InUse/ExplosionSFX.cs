using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSFX : MonoBehaviour
{
    //SFX
    [SerializeField] AudioClip[] explosionSounds;

    void Start()
    {
        //SFX
        AudioClip clip = explosionSounds[UnityEngine.Random.Range(0, explosionSounds.Length)];
        GetComponent<AudioSource>().PlayOneShot(clip);

    }
}
