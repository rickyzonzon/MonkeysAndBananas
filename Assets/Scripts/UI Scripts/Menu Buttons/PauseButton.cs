using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerDownHandler
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
        foreach (Transform child in GameObject.Find("Buttons").transform)
        {
            child.gameObject.GetComponent<Image>().raycastTarget = false;
        }

        GameObject.Find("Play").GetComponent<Image>().raycastTarget = true;

        Pause();
    }

    public void Pause()
    {
        transform.parent.GetComponent<Image>().sprite = btnUp;
        Time.timeScale = 0f;
        game.paused = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.GetComponent<Image>().sprite = btnDown;
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayButton();
    }
}
