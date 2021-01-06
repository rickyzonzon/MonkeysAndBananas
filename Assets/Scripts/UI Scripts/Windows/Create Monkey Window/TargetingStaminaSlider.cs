using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TargetingStaminaSlider : MonoBehaviour, ISliders
{
    private Slider slider;
    private InputField inputField;
    private GameController game;

    void Start()
    {
        slider = this.GetComponent<Slider>();
        inputField = transform.Find("Value").GetComponent<InputField>();
        game = GameObject.Find("GameController").GetComponent<GameController>();

        slider.minValue = game.targetingStaminaBounds[0];
        slider.maxValue = game.targetingStaminaBounds[1] - 1;
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
        try {
            slider.value = (int)System.Math.Round(((((GameObject.Find("Intelligence").GetComponent<Slider>().value / 2 + GameObject.Find("Size").GetComponent<Slider>().value
                + 2 * GameObject.Find("Targeting Speed").GetComponent<Slider>().value) - (game.intelligenceBounds[0] / 2 + game.sizeBounds[0] + 2 * game.targetingSpeedBounds[0]))
                * (game.targetingStaminaBounds[1] - 1 - game.targetingStaminaBounds[0])) / (game.intelligenceBounds[1] / 2 + game.sizeBounds[1] + 2 * game.targetingSpeedBounds[1]
                - (game.intelligenceBounds[0] / 2 + game.sizeBounds[0] + 2 * game.targetingSpeedBounds[0]))) + game.targetingStaminaBounds[0]);

            slider.value = Mathf.Round(slider.value);
            inputField.text = ((int)slider.value).ToString();
        }
        catch (Exception e) { print(e); }
    }
}
