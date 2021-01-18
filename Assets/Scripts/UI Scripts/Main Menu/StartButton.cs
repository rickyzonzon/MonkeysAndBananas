using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private UISFX sfx;

    void Start()
    {
        sfx = this.GetComponent<UISFX>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        sfx.PlayHover();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        sfx.PlayButton();
    }
}
