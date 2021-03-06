﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        transform.Find("Window").GetComponent<Canvas>().enabled = true;
        Camera.main.transform.GetComponent<CameraFollow>().toggleFollow = false;
        Camera.main.transform.GetComponent<CameraFollow>().toggleToggleFollow = false;
        Camera.main.transform.GetComponent<CameraFollow>().target = null;
        Camera.main.transform.GetComponent<CameraZoom>().toggleZoom = false;
        Camera.main.transform.GetComponent<CameraZoom>().toggleToggleZoom = false;
        Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = false;
        Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = false;

        foreach (Transform child in GameObject.Find("Buttons").transform)
        {
            child.gameObject.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.GetComponent<Image>().sprite = btnDown;
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.parent.GetComponent<Image>().sprite = btnUp;
    }
}
