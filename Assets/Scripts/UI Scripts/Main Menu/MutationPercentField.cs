using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MutationPercentField : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        this.GetComponent<InputField>().text = "27.0";
        this.GetComponent<InputField>().onEndEdit.AddListener(delegate { TaskOnEnd(); });
    }

    public void TaskOnEnd()
    {
        if (System.Convert.ToSingle(this.GetComponent<InputField>().text) <= 0f)
        {
            this.GetComponent<InputField>().text = "0.01";
        }
        else if (System.Convert.ToSingle(this.GetComponent<InputField>().text) > 100f)
        {
            this.GetComponent<InputField>().text = "100.0";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.GetComponent<UISFX>().PlayField();
    }
}
