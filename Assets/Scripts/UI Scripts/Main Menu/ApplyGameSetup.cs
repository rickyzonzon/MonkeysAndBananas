using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ApplyGameSetup : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SetupInfo setup = GameObject.Find("Setup Info").GetComponent<SetupInfo>();

        setup.treeSpawnFreq = System.Convert.ToInt32(GameObject.Find("Tree Spawn Rate").GetComponent<InputField>().text);
        setup.treeSpawnBounds[0] = System.Convert.ToInt32(GameObject.Find("Trees Per Spawn Min").GetComponent<InputField>().text);
        setup.treeSpawnBounds[1] = System.Convert.ToInt32(GameObject.Find("Trees Per Spawn Max").GetComponent<InputField>().text) + 1;
        setup.maxTrees = System.Convert.ToInt32(GameObject.Find("Max Trees").GetComponent<InputField>().text);
        setup.startingTrees = System.Convert.ToInt32(GameObject.Find("Starting Trees").GetComponent<InputField>().text);
        setup.startingMonkeys = System.Convert.ToInt32(GameObject.Find("Starting Monkeys").GetComponent<InputField>().text);
        setup.startingObjects = System.Convert.ToInt32(GameObject.Find("Starting Objects").GetComponent<InputField>().text);
        setup.mutationProbability = System.Convert.ToSingle(GameObject.Find("Mutation %").GetComponent<InputField>().text) / 100;
        setup.energyLossRate = System.Convert.ToInt32(GameObject.Find("Energy Loss Rate").GetComponent<InputField>().text);
        setup.startingEnergy = System.Convert.ToInt32(GameObject.Find("Starting Energy").GetComponent<InputField>().text);

        SceneManager.LoadScene("Simulation");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.parent.GetComponent<UISFX>().PlayButton();
    }
}
