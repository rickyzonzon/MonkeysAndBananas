using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour
{
    public AudioClip hover;
    public AudioClip button;
    public AudioClip toggle;
    public AudioClip slider;
    public AudioClip field;
    public AudioSource sfx;

    void Start()
    {
        sfx = this.GetComponent<AudioSource>();
    }

    public void PlayHover()
    {
        sfx.clip = hover;
        sfx.Play();
    }

    public void PlayButton()
    {
        sfx.clip = button;
        sfx.Play();
    }

    public void PlayToggle()
    {
        sfx.clip = toggle;
        sfx.Play();
    }

    public void PlaySlider()
    {
        sfx.clip = slider;
        sfx.Play();
    }

    public void PlayField()
    {
        sfx.clip = field;
        sfx.Play();
    }
}
