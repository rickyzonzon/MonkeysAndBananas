using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BambooButton : MonoBehaviour, IPointerDownHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    private GameObject water;
    private GameObject boulder;
    public bool selected;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        water = GameObject.Find("Water");
        boulder = GameObject.Find("Boulder");
        selected = false;
    }

    void TaskOnClick()
    {
        water.GetComponent<Image>().sprite = water.GetComponent<WaterButton>().btnUp;
        boulder.GetComponent<Image>().sprite = boulder.GetComponent<BoulderButton>().btnUp;

        selected = true;
        water.GetComponent<WaterButton>().selected = false;
        boulder.GetComponent<BoulderButton>().selected = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnDown;
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayToggle();
    }
}
