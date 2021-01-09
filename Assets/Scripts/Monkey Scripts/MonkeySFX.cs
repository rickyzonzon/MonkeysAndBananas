using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeySFX : MonoBehaviour
{
    public AudioClip[] monkeyChirps;
    public AudioClip[] monkeyHehes;
    public AudioSource sfx;

    void Start()
    {
        sfx = this.GetComponent<AudioSource>();
    }

    public void PlayChirp()
    {
        int randSound = Random.Range(0, monkeyChirps.Length);

        sfx.clip = monkeyChirps[randSound];
        sfx.Play();
    }

    public void PlayHehe()
    {
        int randSound = Random.Range(0, monkeyHehes.Length);

        sfx.clip = monkeyHehes[randSound];
        sfx.Play();
    }
}
