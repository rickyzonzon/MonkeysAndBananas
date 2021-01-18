using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class QuitButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private UISFX sfx;

    void Start()
    {
        sfx = this.GetComponent<UISFX>();
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Application.Quit();
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
