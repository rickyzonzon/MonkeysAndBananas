using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ExitMainMenuButton : MonoBehaviour, IPointerDownHandler
{
    private UISFX sfx;

    void Start()
    {
        sfx = this.GetComponent<UISFX>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        sfx.PlayButton();
    }
}
