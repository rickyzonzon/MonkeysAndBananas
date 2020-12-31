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

        foreach (RectTransform child in window.transform)
        {
            child.localScale = Vector3.zero;
            foreach (RectTransform grandchild in child)
            {
                grandchild.localScale = Vector3.zero;
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
                foreach (RectTransform child in window.transform)
                {
                    child.transform.position = Camera.main.WorldToScreenPoint(monkey.transform.position);
                }
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
                window.transform.Find("Badges").Find("Bored Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
                window.transform.Find("Badges").Find("Bored Badge").Find("Bored").GetComponent<Image>().color = Color.white;
            }
            else
            {
                window.transform.Find("Badges").Find("Bored Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                window.transform.Find("Badges").Find("Bored Badge").Find("Bored").GetComponent<Image>().color = Color.black;
            }

            if (state.breedable)
            {
                window.transform.Find("Badges").Find("Breedable Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
                window.transform.Find("Badges").Find("Breedable Badge").Find("Breedable").GetComponent<Image>().color = Color.white;
            }
            else
            {
                window.transform.Find("Badges").Find("Breedable Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                window.transform.Find("Badges").Find("Breedable Badge").Find("Breedable").GetComponent<Image>().color = Color.black;
            }

            if (genes.mutated)
            {
                window.transform.Find("Badges").Find("Mutation Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
                window.transform.Find("Badges").Find("Mutation Badge").Find("Mutated").GetComponent<Image>().color = Color.white;
            }
            else
            {
                window.transform.Find("Badges").Find("Mutation Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                window.transform.Find("Badges").Find("Mutation Badge").Find("Mutated").GetComponent<Image>().color = Color.black;
            }

            if (state.baby)
            {
                window.transform.Find("Badges").Find("Baby Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
                window.transform.Find("Badges").Find("Baby Badge").Find("Baby").GetComponent<Image>().color = Color.white;
            }
            else
            {
                window.transform.Find("Badges").Find("Baby Badge").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                window.transform.Find("Badges").Find("Baby Badge").Find("Baby").GetComponent<Image>().color = Color.black;
            }

            window.transform.Find("Panel").Find("Name").GetComponent<Text>().text = genes.firstName + " " + genes.lastName;
            window.transform.Find("Panel").Find("Energy").GetComponent<Text>().text = "" + energy.energy;
            window.transform.Find("Panel").Find("Age").GetComponent<Text>().text = state.years + " Years " + state.months + " Months";
            window.transform.Find("Panel").Find("State").GetComponent<Text>().text = state._state;
            window.transform.Find("Panel").Find("Generation").GetComponent<Text>().text = "" + state.generation;

            window.transform.Find("Panel").Find("Intelligence").GetComponent<Text>().text = "" + System.Math.Round(genes.intelligence, 3);
            window.transform.Find("Panel").Find("Size").GetComponent<Text>().text = "" + System.Math.Round(genes.size, 3);
            window.transform.Find("Panel").Find("Targeting Speed").GetComponent<Text>().text = "" + System.Math.Round(genes.targettingSpeed, 3);
            window.transform.Find("Panel").Find("Wandering Speed").GetComponent<Text>().text = "" + System.Math.Round(genes.wanderingSpeed, 3);
            window.transform.Find("Panel").Find("Targeting Stamina").GetComponent<Text>().text = "" + genes.targettingStamina;
            window.transform.Find("Panel").Find("Wandering Stamina").GetComponent<Text>().text = "" + genes.wanderingStamina;
            window.transform.Find("Panel").Find("Max Climb").GetComponent<Text>().text = "" + genes.maxClimb;
            window.transform.Find("Panel").Find("Breeding Threshold").GetComponent<Text>().text = "" + genes.breedingThreshold;
            window.transform.Find("Panel").Find("Energy Passover").GetComponent<Text>().text = "" + genes.babyEnergy;

            Vector3 monkeyPos = Camera.main.WorldToScreenPoint(monkey.transform.position);
            Vector3 targetPos = Vector3.zero;

            foreach (RectTransform child in window.transform)
            {
                child.localScale = Vector3.Lerp(child.localScale, new Vector3(1f, 1f, 1f), 3.5f * Time.deltaTime);
                foreach (RectTransform grandchild in child)
                {
                    switch (grandchild.transform.name)
                    {
                        case "Name":
                        case "Energy":
                        case "Age":
                        case "State":
                        case "Generation":
                            grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(0.22f, 0.22f, 1f), 3.5f * Time.deltaTime);
                            break;
                        case "Intelligence":
                        case "Size":
                        case "Targeting Speed":
                        case "Wandering Speed":
                        case "Targeting Stamina":
                        case "Wandering Stamina":
                        case "Max Climb":
                        case "Breeding Threshold":
                        case "Energy Passover":
                            grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(0.18f, 0.18f, 0.18f), 3.5f * Time.deltaTime);
                            break;
                        default:
                            grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(1f, 1f, 1f), 3.5f * Time.deltaTime);
                            break;
                    }
                }

                switch (child.transform.name)
                {
                    case "Panel":
                        targetPos.x = Mathf.Clamp(monkeyPos.x + 450, 0f, Screen.width - 25 - child.rect.width / 2);
                        targetPos.y = Mathf.Clamp(monkeyPos.y, 25 + child.rect.height / 2, Screen.height - 25 - child.rect.height / 2);
                        break;
                    case "Badges":
                        targetPos.x = Mathf.Clamp(monkeyPos.x - 200, 0f, Screen.width - 100 - window.transform.Find("Panel").GetComponent<RectTransform>().rect.width);
                        targetPos.y = Mathf.Clamp(monkeyPos.y, 25 + child.rect.height / 2, Screen.height - 25 - child.rect.height / 2);
                        break;
                }
                    child.transform.position = Vector3.MoveTowards(child.transform.position, targetPos, 400f * Time.deltaTime);
            }
        }
        else
        {
            if (prevMonkey != null)
            {
                Vector3 monkeyPos = Camera.main.WorldToScreenPoint(prevMonkey.transform.position);

                foreach (RectTransform child in window.transform)
                {
                    child.localScale = Vector3.Lerp(child.localScale, Vector3.zero, 3.5f * Time.deltaTime);
                    if (!firstTarget)
                    {
                        child.transform.position = Vector3.MoveTowards(child.transform.position, monkeyPos, 400f * Time.deltaTime);
                    }
                    foreach (RectTransform grandchild in child)
                    {
                        grandchild.localScale = Vector3.Lerp(grandchild.localScale, Vector3.zero, 3.5f * Time.deltaTime);
                    }
                }
            }
        }
    }
}
