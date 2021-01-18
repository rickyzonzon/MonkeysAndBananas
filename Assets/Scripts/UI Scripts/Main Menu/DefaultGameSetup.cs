using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DefaultGameSetup : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameObject.Find("Tree Spawn Rate").GetComponent<InputField>().text = "5";
        GameObject.Find("Trees Per Spawn Min").GetComponent<InputField>().text = "3";
        GameObject.Find("Trees Per Spawn Max").GetComponent<InputField>().text = "5";
        GameObject.Find("Max Trees").GetComponent<InputField>().text = "40";
        GameObject.Find("Starting Trees").GetComponent<InputField>().text = "15";
        GameObject.Find("Starting Monkeys").GetComponent<InputField>().text = "15";
        GameObject.Find("Starting Objects").GetComponent<InputField>().text = "30";
        GameObject.Find("Mutation %").GetComponent<InputField>().text = "27.0";
        GameObject.Find("Energy Loss Rate").GetComponent<InputField>().text = "4";
        GameObject.Find("Starting Energy").GetComponent<InputField>().text = "80";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.parent.GetComponent<UISFX>().PlayButton();
    }
}
