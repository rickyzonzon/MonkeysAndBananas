using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Exit();
    }

    public void Exit()
    {
        transform.parent.GetComponent<Canvas>().enabled = false;
        Camera.main.transform.GetComponent<CameraFollow>().toggleFollow = true;
        Camera.main.transform.GetComponent<CameraFollow>().toggleToggleFollow = true;
        Camera.main.transform.GetComponent<CameraZoom>().toggleZoom = true;
        Camera.main.transform.GetComponent<CameraZoom>().toggleToggleZoom = true;
        Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
        Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;

        foreach (Transform child in GameObject.Find("Buttons").transform)
        {
            child.gameObject.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnDown;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnUp;
    }
}
