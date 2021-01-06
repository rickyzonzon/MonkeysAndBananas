using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonkeyInfoWindow : MonoBehaviour
{
    private Canvas window;
    public bool toggleInfo = true;
    public GameObject monkey;
    private MonkeyGenes genes;
    private MonkeyStates state;
    private MonkeyEnergy energy;
    private GameController game;
    private bool showInfo = false;
    private int count = 0;

    void Start()
    {
        window = GameObject.Find("Monkey Info").GetComponent<Canvas>();
        game = GameObject.Find("GameController").GetComponent<GameController>();

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
        if (toggleInfo)
        {
            count++;
            Transform target = this.GetComponent<CameraFollow>().target;

            if (target == null || target.gameObject.tag == "tree")
            {
                count = 0;
                monkey = null;
                showInfo = false;
            }
            else
            {
                monkey = target.gameObject;
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
        else
        {
            count = 0;
            monkey = null;
            showInfo = false;
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

            if (state.natural)
            {
                window.transform.Find("Panel").Find("Natural").GetComponent<Image>().color = new Color(1, 1, 1, 0f);
            }
            else
            {
                window.transform.Find("Panel").Find("Natural").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
            }

            window.transform.Find("Panel").Find("Name").GetComponent<Text>().text = genes.firstName + " " + genes.lastName;
            window.transform.Find("Panel").Find("Energy").GetComponent<Text>().text = "" + energy.energy;
            window.transform.Find("Panel").Find("Age").GetComponent<Text>().text = state.years + " Years " + state.months + " Months";
            window.transform.Find("Panel").Find("State").GetComponent<Text>().text = state._state;
            window.transform.Find("Panel").Find("Generation").GetComponent<Text>().text = "" + state.generation;

            window.transform.Find("Panel").Find("Intelligence").GetComponent<Text>().text = "" + System.Math.Round(genes.intelligence, 3);
            window.transform.Find("Panel").Find("Size").GetComponent<Text>().text = "" + System.Math.Round(genes.size, 3);
            window.transform.Find("Panel").Find("Targeting Speed").GetComponent<Text>().text = "" + System.Math.Round(genes.targetingSpeed, 3);
            window.transform.Find("Panel").Find("Wandering Speed").GetComponent<Text>().text = "" + System.Math.Round(genes.wanderingSpeed, 3);
            window.transform.Find("Panel").Find("Targeting Stamina").GetComponent<Text>().text = "" + genes.targetingStamina;
            window.transform.Find("Panel").Find("Wandering Stamina").GetComponent<Text>().text = "" + genes.wanderingStamina;
            window.transform.Find("Panel").Find("Max Climb").GetComponent<Text>().text = "" + genes.maxClimb;
            window.transform.Find("Panel").Find("Breeding Threshold").GetComponent<Text>().text = "" + genes.breedingThreshold;
            window.transform.Find("Panel").Find("Energy Passover").GetComponent<Text>().text = "" + genes.babyEnergy;

            Vector3 monkeyPos = Camera.main.WorldToScreenPoint(monkey.transform.position);
            Vector3 targetPos = Vector3.zero;

            foreach (RectTransform child in window.transform)
            {
                if (game.paused)
                {
                    child.localScale = Vector3.Lerp(child.localScale, new Vector3(1f, 1f, 1f), 3.5f / 100);
                }
                else
                {
                    child.localScale = Vector3.Lerp(child.localScale, new Vector3(1f, 1f, 1f), 3.5f * Time.deltaTime);
                }

                foreach (RectTransform grandchild in child)
                {
                    switch (grandchild.transform.name)
                    {
                        case "Name":
                        case "Energy":
                        case "Age":
                        case "State":
                        case "Generation":
                            if (game.paused)
                            {
                                grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(0.22f, 0.22f, 1f), 3.5f / 100);
                            }
                            else
                            {
                                grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(0.22f, 0.22f, 1f), 3.5f * Time.deltaTime);
                            }
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
                            if (game.paused)
                            {
                                grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(0.18f, 0.18f, 1f), 3.5f / 100);
                            }
                            else
                            {
                                grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(0.18f, 0.18f, 1f), 3.5f * Time.deltaTime);
                            }
                            break;
                        default:
                            if (game.paused)
                            {
                                grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(1f, 1f, 1f), 3.5f / 100);
                            }
                            else
                            {
                                grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(1f, 1f, 1f), 3.5f * Time.deltaTime);
                            }
                            
                            break;
                    }
                }

                switch (child.transform.name)
                {
                    case "Panel":
                        targetPos.x = Mathf.Clamp(monkeyPos.x + 450, 25 + child.rect.width, Screen.width - 25 - child.rect.width / 2);
                        targetPos.y = Mathf.Clamp(monkeyPos.y, 25 + child.rect.height / 2, Screen.height - GameObject.Find("Button Menu").transform.Find("Buttons").GetComponent<RectTransform>().rect.height - 25 - child.rect.height / 2);
                        break;
                    case "Badges":
                        targetPos.x = Mathf.Clamp(monkeyPos.x - 200, 25 + child.rect.width / 2, Screen.width - 100 - window.transform.Find("Panel").GetComponent<RectTransform>().rect.width);
                        targetPos.y = Mathf.Clamp(monkeyPos.y, 25 + child.rect.height / 2, Screen.height - GameObject.Find("Button Menu").transform.Find("Buttons").GetComponent<RectTransform>().rect.height - 25 - child.rect.height / 2);
                        break;
                }

                if (game.paused)
                {
                    child.transform.position = Vector3.MoveTowards(child.transform.position, targetPos, 400f / 100f);
                }
                else
                {
                    child.transform.position = Vector3.MoveTowards(child.transform.position, targetPos, 400f * Time.deltaTime);
                }
            }
        }
        else
        {
            foreach (RectTransform child in window.transform)
            {
                child.localScale = Vector3.zero;

                foreach (RectTransform grandchild in child)
                {
                    grandchild.localScale = Vector3.zero;
                }
            }
        }
    }
}
