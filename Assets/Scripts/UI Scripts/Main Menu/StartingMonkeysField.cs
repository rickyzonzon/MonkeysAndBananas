using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartingMonkeysField : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        this.GetComponent<InputField>().text = "15";
        this.GetComponent<InputField>().onEndEdit.AddListener(delegate { TaskOnEnd(); });
    }

    public void TaskOnEnd()
    {
        if (System.Convert.ToInt32(this.GetComponent<InputField>().text) <= 0)
        {
            this.GetComponent<InputField>().text = "1";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.GetComponent<UISFX>().PlayField();
    }
}
