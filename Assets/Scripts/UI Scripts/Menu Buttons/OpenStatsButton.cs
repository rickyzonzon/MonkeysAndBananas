using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OpenStatsButton : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector] public Vector3 oldPos;
    private Vector3 targetPos;
    private Transform window;
    public bool open;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        if (open)
        {
            targetPos = new Vector3(oldPos.x + 422, oldPos.y, oldPos.z);

            window.position = Vector3.MoveTowards(window.position, targetPos, 500 * Time.deltaTime);
            if (window.position == targetPos)
            {
                open = false;
                window.Find("Close Flap").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void TaskOnClick()
    {
        this.GetComponent<RectTransform>().localScale = Vector3.zero;
        window = transform.parent.Find("Window");
        window.GetComponent<Canvas>().enabled = true;

        oldPos = new Vector3(window.position.x, window.position.y, window.position.z);
        open = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayButton();
    }
}
