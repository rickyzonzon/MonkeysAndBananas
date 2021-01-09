using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ExitSettingsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        SettingsButton settings = transform.parent.parent.GetComponent<SettingsButton>();

        GameObject.Find("Monkey SFX").GetComponent<Slider>().value = settings.mSFX;
        GameObject.Find("UI SFX").GetComponent<Slider>().value = settings.uiSFX;
        GameObject.Find("Background SFX").GetComponent<Slider>().value = settings.bgSFX;
        GameObject.Find("Background Music").GetComponent<Slider>().value = settings.bgMusic;

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
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnUp;
    }
}
