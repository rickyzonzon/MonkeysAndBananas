using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SoundFields : MonoBehaviour, IFields<float>, IPointerDownHandler
{
    private Slider slider;
    private InputField inputField;

    void Start()
    {
        inputField = this.GetComponent<InputField>();
        slider = transform.parent.GetComponent<Slider>();
        inputField.onValueChanged.AddListener(delegate { TaskOnChange(); });
        inputField.onEndEdit.AddListener(delegate { TaskOnEnd(); });
    }

    public void TaskOnChange()
    {
        InputFieldUpdate();
    }

    public void TaskOnEnd()
    {
        if (System.Convert.ToSingle(inputField.text) < slider.minValue)
        {
            inputField.text = "0";
        }
        else if (System.Convert.ToSingle(inputField.text) > slider.maxValue)
        {
            inputField.text = "1";
        }
    }

    public void InputFieldUpdate()
    {
        slider.value = System.Convert.ToSingle(inputField.text);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayField();
    }
}
