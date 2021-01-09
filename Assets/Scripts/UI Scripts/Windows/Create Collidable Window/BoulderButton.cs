using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BoulderButton : MonoBehaviour, IPointerDownHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    private GameObject water;
    private GameObject bamboo;
    public bool selected;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        water = GameObject.Find("Water");
        bamboo = GameObject.Find("Bamboo");
        selected = false;
    }

    void TaskOnClick()
    {
        water.GetComponent<Image>().sprite = water.GetComponent<WaterButton>().btnUp;
        bamboo.GetComponent<Image>().sprite = bamboo.GetComponent<BambooButton>().btnUp;

        selected = true;
        water.GetComponent<WaterButton>().selected = false;
        bamboo.GetComponent<BambooButton>().selected = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnDown;
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayToggle();
    }
}
