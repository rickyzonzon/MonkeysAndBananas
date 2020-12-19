using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationWindow : MonoBehaviour
{
    private Canvas window;
    public GameObject monkey;
    private GameObject prevMonkey;
    private MonkeyGenes genes;
    private MonkeyStates state;
    private MonkeyEnergy energy;
    private bool showInfo = false;
    private int count = 0;
    private bool firstTarget = true;

    void Start()
    {
        window = GameObject.Find("Info").GetComponent<Canvas>();

        foreach (Transform child in window.transform)
        {
            child.GetComponent<RectTransform>().localScale = Vector3.zero;
            foreach (Transform grandchild in child)
            {
                grandchild.GetComponent<RectTransform>().localScale = Vector3.zero;
            }
        }
    }

    void Update()
    {
        count++;
        Transform target = this.GetComponent<CameraFollow>().target;

        if (target == null)
        {
            count = 0;
            monkey = null;
            showInfo = false;
        }
        else
        {
            firstTarget = false;
            monkey = target.gameObject;
            prevMonkey = monkey;
            if (count == 1)
            {
                window.transform.GetChild(0).GetComponent<RectTransform>().transform.position = Camera.main.WorldToScreenPoint(monkey.transform.position);
            }
            genes = monkey.GetComponent<MonkeyGenes>();
            state = monkey.GetComponent<MonkeyStates>();
            energy = monkey.GetComponent<MonkeyEnergy>();
            showInfo = true;
        }
    }

    void OnGUI()
    {
        if (showInfo)
        {
            if (state.bored)
            {
                window.transform.Find("Panel").Find("Bored Badge").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                window.transform.Find("Panel").Find("Bored").GetComponent<Image>().color = Color.white;
            }
            else
            {
                window.transform.Find("Panel").Find("Bored Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                window.transform.Find("Panel").Find("Bored").GetComponent<Image>().color = Color.black;
            }

            if (state.breedable)
            {
                window.transform.Find("Panel").Find("Breedable Badge").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                window.transform.Find("Panel").Find("Breedable").GetComponent<Image>().color = Color.white;
            }
            else
            {
                window.transform.Find("Panel").Find("Breedable Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                window.transform.Find("Panel").Find("Breedable").GetComponent<Image>().color = Color.black;
            }

            if (genes.mutated)
            {
                window.transform.Find("Panel").Find("Mutation Badge").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                window.transform.Find("Panel").Find("Mutated").GetComponent<Image>().color = Color.white;
            }
            else
            {
                window.transform.Find("Panel").Find("Mutation Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                window.transform.Find("Panel").Find("Mutated").GetComponent<Image>().color = Color.black;
            }

            if (state.baby)
            {

            }
            else
            {

            }

            //window.transform.Find("Panel").GetComponent<Image>().color = new Color(genes.red, genes.green, genes.blue, 0.8f);
            window.transform.Find("Panel").Find("Name").GetComponent<Text>().text = "Name: " + genes.firstName + " " + genes.lastName;
            window.transform.Find("Panel").Find("Energy").GetComponent<Text>().text = "Energy: " + energy.energy;

            window.transform.Find("Panel").Find("Intelligence").GetComponent<Text>().text = "Intelligence: " + genes.intelligence;
            window.transform.Find("Panel").Find("Targetting Speed").GetComponent<Text>().text = "Targetting Speed: " + genes.targettingSpeed;
            window.transform.Find("Panel").Find("Wandering Speed").GetComponent<Text>().text = "Wandering Speed: " + genes.wanderingSpeed;
            window.transform.Find("Panel").Find("Targetting Stamina").GetComponent<Text>().text = "Targetting Stamina: " + genes.targettingStamina;
            window.transform.Find("Panel").Find("Wandering Stamina").GetComponent<Text>().text = "Wandering Stamina: " + genes.wanderingStamina;
            window.transform.Find("Panel").Find("Max Climb").GetComponent<Text>().text = "Max Climb: " + genes.maxClimb;
            window.transform.Find("Panel").Find("Breeding Threshold").GetComponent<Text>().text = "Breeding Threshold: " + genes.breedingThreshold;
            window.transform.Find("Panel").Find("Energy Passover").GetComponent<Text>().text = "Energy Passover: " + genes.babyEnergy;

            RectTransform rectTransform = window.transform.GetChild(0).GetComponent<RectTransform>();
            Vector3 monkeyPos = Camera.main.WorldToScreenPoint(monkey.transform.position);
            Vector3 targetPos = Vector3.zero;

            foreach (Transform child in window.transform)
            {
                child.GetComponent<RectTransform>().localScale = Vector3.Lerp(child.GetComponent<RectTransform>().localScale, new Vector3(1f, 1f, 1f), 5f * Time.deltaTime);
                foreach (Transform grandchild in child)
                {
                    grandchild.GetComponent<RectTransform>().localScale = Vector3.Lerp(grandchild.GetComponent<RectTransform>().localScale, new Vector3(1f, 1f, 1f), 5f * Time.deltaTime);
                }

                targetPos.x = Mathf.Clamp(monkeyPos.x + 400, 0f, Screen.width - 25 - rectTransform.rect.width / 2);
                targetPos.y = Mathf.Clamp(monkeyPos.y, 25 + rectTransform.rect.height / 2, Screen.height - 25 - rectTransform.rect.height / 2);
                rectTransform.transform.position = Vector3.MoveTowards(rectTransform.transform.position, targetPos, 600f * Time.deltaTime);
            }

        }
        else
        {
            if (prevMonkey != null)
            {
                RectTransform rectTransform = window.transform.GetChild(0).GetComponent<RectTransform>();
                Vector3 monkeyPos = Camera.main.WorldToScreenPoint(prevMonkey.transform.position);

                foreach (Transform child in window.transform)
                {
                    child.GetComponent<RectTransform>().localScale = Vector3.Lerp(child.GetComponent<RectTransform>().localScale, Vector3.zero, 5f * Time.deltaTime);
                    foreach (Transform grandchild in child)
                    {
                        grandchild.GetComponent<RectTransform>().localScale = Vector3.Lerp(grandchild.GetComponent<RectTransform>().localScale, Vector3.zero, 5f * Time.deltaTime);
                    }

                    if (!firstTarget)
                    {
                        rectTransform.transform.position = Vector3.MoveTowards(rectTransform.transform.position, monkeyPos, 600f * Time.deltaTime);
                    }
                }
            }
        }
    }
}
