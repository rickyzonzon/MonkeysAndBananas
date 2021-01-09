using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HeightSlider : MonoBehaviour, ISliders, IPointerDownHandler
{
    private Slider slider;
    private InputField inputField;

    void Start()
    {
        slider = this.GetComponent<Slider>();
        inputField = transform.Find("Value").GetComponent<InputField>();
        slider.minValue = 1;
        slider.maxValue = 5;
        slider.value = 3;
        slider.onValueChanged.AddListener(delegate { TaskOnChange(); });
        SliderUpdate();
    }

    public void TaskOnChange()
    {
        SliderUpdate();
    }

    public void SliderUpdate()
    {
        inputField.text = slider.value.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlaySlider();
    }
}
