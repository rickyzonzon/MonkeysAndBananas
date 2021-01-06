using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RandomizeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    private GameController game;

    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameController>();
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameObject.Find("Name").GetComponent<InputField>().text = game.firstNames[UnityEngine.Random.Range(0, game.firstNames.Length)] + " " + game.lastNames[UnityEngine.Random.Range(0, game.lastNames.Length)];
        GameObject.Find("Energy").GetComponent<InputField>().text = game.startingEnergy.ToString();
        GameObject.Find("Intelligence").GetComponent<Slider>().value = UnityEngine.Random.Range(game.intelligenceBounds[0], game.intelligenceBounds[1]);
        GameObject.Find("Size").GetComponent<Slider>().value = UnityEngine.Random.Range(game.sizeBounds[0], game.sizeBounds[1]);
        GameObject.Find("Targeting Speed").GetComponent<Slider>().value = UnityEngine.Random.Range(game.targetingSpeedBounds[0], game.targetingSpeedBounds[1]);
        GameObject.Find("Wandering Speed").GetComponent<Slider>().value = UnityEngine.Random.Range(game.wanderingSpeedBounds[0], game.wanderingSpeedBounds[1]);
        GameObject.Find("Max Climb").GetComponent<Slider>().value = UnityEngine.Random.Range(game.maxClimbBounds[0], game.maxClimbBounds[1]);
        GameObject.Find("Breeding Threshold").GetComponent<Slider>().value = UnityEngine.Random.Range(game.breedingThresholdBounds[0], game.breedingThresholdBounds[1]);
        GameObject.Find("Energy Passover").GetComponent<Slider>().value = UnityEngine.Random.Range(game.babyEnergyBounds[0], game.babyEnergyBounds[1]);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnDown;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnUp;
    }
}