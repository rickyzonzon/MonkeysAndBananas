using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MaxTreesField : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        this.GetComponent<InputField>().text = "40";
        this.GetComponent<InputField>().onEndEdit.AddListener(delegate { TaskOnEnd(); });
    }

    public void TaskOnEnd()
    {
        if (System.Convert.ToInt32(this.GetComponent<InputField>().text) < System.Convert.ToInt32(GameObject.Find("Starting Trees").GetComponent<InputField>().text))
        {
            this.GetComponent<InputField>().text = GameObject.Find("Starting Trees").GetComponent<InputField>().text;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.GetComponent<UISFX>().PlayField();
    }
}
