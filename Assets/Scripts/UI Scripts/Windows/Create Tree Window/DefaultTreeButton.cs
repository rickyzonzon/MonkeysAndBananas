using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DefaultTreeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        DefaultTree();
    }

    public void DefaultTree()
    {
        GameObject.Find("Height").GetComponent<Slider>().value = 3;
        GameObject.Find("Tree Energy").GetComponent<InputField>().text = "16";
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
