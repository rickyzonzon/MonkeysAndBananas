using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIMonkeys : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource sfx;

    void Start()
    {
        sfx = this.GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        sfx.Play();
    }
}
