using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WaterButton : MonoBehaviour, IPointerDownHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    private GameObject bamboo;
    private GameObject boulder;
    public bool selected = false;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        bamboo = GameObject.Find("Bamboo");
        boulder = GameObject.Find("Boulder");
        selected = false;
    }

    void TaskOnClick()
    {
        boulder.GetComponent<Image>().sprite = boulder.GetComponent<BoulderButton>().btnUp;
        bamboo.GetComponent<Image>().sprite = bamboo.GetComponent<BambooButton>().btnUp;

        selected = true;
        bamboo.GetComponent<BambooButton>().selected = false;
        boulder.GetComponent<BoulderButton>().selected = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnDown;
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayToggle();
    }
}
