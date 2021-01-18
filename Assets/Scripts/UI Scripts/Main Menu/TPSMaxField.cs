using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TPSMaxField : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        this.GetComponent<InputField>().text = "5";
        this.GetComponent<InputField>().onEndEdit.AddListener(delegate { TaskOnEnd(); });
    }

    public void TaskOnEnd()
    {
        if (System.Convert.ToInt32(this.GetComponent<InputField>().text) < System.Convert.ToInt32(GameObject.Find("Trees Per Spawn Min").GetComponent<InputField>().text))
        {
            this.GetComponent<InputField>().text = GameObject.Find("Trees Per Spawn Min").GetComponent<InputField>().text;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.GetComponent<UISFX>().PlayField();
    }
}
