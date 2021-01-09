using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CloseStatsButton : MonoBehaviour, IPointerDownHandler
{
    private Transform window;
    private Vector3 oldPos;
    public bool close;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        if (close)
        {
            window = transform.parent;
            window.position = Vector3.MoveTowards(window.position, new Vector3(oldPos.x, oldPos.y, oldPos.z), 500 * Time.deltaTime);
            if (window.position == oldPos)
            {
                close = false;
                window.GetComponent<Canvas>().enabled = false;
                GameObject.Find("Open Flap").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void TaskOnClick()
    {
        this.GetComponent<RectTransform>().localScale = Vector3.zero;

        oldPos = GameObject.Find("Open Flap").GetComponent<OpenStatsButton>().oldPos;
        close = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayButton();
    }
}
