using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BreedingThresholdSlider : MonoBehaviour, ISliders
{
    private Slider slider;
    private InputField inputField;

    void Start()
    {
        slider = this.GetComponent<Slider>();
        inputField = transform.Find("Value").GetComponent<InputField>();
        slider.minValue = GameObject.Find("GameController").GetComponent<GameController>().breedingThresholdBounds[0];
        slider.maxValue = GameObject.Find("GameController").GetComponent<GameController>().breedingThresholdBounds[1] - 1;
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
        slider.value = Mathf.Round(slider.value);
        inputField.text = ((int)slider.value).ToString();
    }
}
