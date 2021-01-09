using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SettingsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    public float mSFX;
    public float uiSFX;
    public float bgSFX;
    public float bgMusic;

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

        SaveValues();
    }

    public void SaveValues()
    {
        mSFX = transform.Find("Window").Find("Monkey SFX").GetComponent<Slider>().value;
        uiSFX = transform.Find("Window").Find("UI SFX").GetComponent<Slider>().value;
        bgSFX = transform.Find("Window").Find("Background SFX").GetComponent<Slider>().value;
        bgMusic = transform.Find("Window").Find("Background Music").GetComponent<Slider>().value;
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
