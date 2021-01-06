using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TargetingStaminaField : MonoBehaviour, IFields<int>
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
        if (System.Convert.ToInt32(inputField.text) < slider.minValue)
        {
            inputField.text = slider.minValue.ToString();
        }
        else if (System.Convert.ToInt32(inputField.text) > slider.maxValue)
        {
            inputField.text = slider.maxValue.ToString();
        }
    }

    public void InputFieldUpdate()
    {
        slider.value = System.Convert.ToInt32(inputField.text);
    }
}
