using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TargetingSpeedSlider : MonoBehaviour, ISliders, IPointerDownHandler
{
    private Slider slider;
    private InputField inputField;

    void Start()
    {
        slider = this.GetComponent<Slider>();
        inputField = transform.Find("Value").GetComponent<InputField>();
        slider.minValue = GameObject.Find("GameController").GetComponent<GameController>().targetingSpeedBounds[0];
        slider.maxValue = GameObject.Find("GameController").GetComponent<GameController>().targetingSpeedBounds[1];
        slider.value = (slider.minValue + slider.maxValue) / 2;
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
        GameObject.Find("Targeting Stamina").GetComponent<TargetingStaminaSlider>().SliderUpdate();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlaySlider();
    }
}
