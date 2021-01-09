using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ApplySettingsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    private GameController game;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void TaskOnClick()
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

        ApplySettings();
    }

    public void ApplySettings()
    {
        GameObject[] monkeys = GameObject.FindGameObjectsWithTag("monkey");

        foreach (GameObject monkey in monkeys)
        {
            monkey.GetComponent<AudioSource>().volume = GameObject.Find("Monkey SFX").GetComponent<Slider>().value;
        }
        GameObject.Find("Button Menu").GetComponent<AudioSource>().volume = GameObject.Find("UI SFX").GetComponent<Slider>().value;
        GameObject.Find("Jungle BGFX").GetComponent<AudioSource>().volume = GameObject.Find("Background SFX").GetComponent<Slider>().value;
        GameObject.Find("Jungle BGM").GetComponent<AudioSource>().volume = GameObject.Find("Background Music").GetComponent<Slider>().value;

        transform.parent.parent.GetComponent<SettingsButton>().SaveValues();
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
