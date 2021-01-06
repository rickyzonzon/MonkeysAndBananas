using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CreateMonkeyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    private GameController game;
    private GameObject tempMonkey;
    private bool placeMonkey = false;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (placeMonkey)
        {
            CreateMonkey();
        }
    }

    void TaskOnClick()
    {
        if (GameObject.Find("Name").GetComponent<InputField>().text != "" && GameObject.Find("Energy").GetComponent<InputField>().text != "")
        {
            transform.parent.GetComponent<Canvas>().enabled = false;
            Camera.main.transform.GetComponent<CameraFollow>().toggleFollow = true;
            Camera.main.transform.GetComponent<CameraFollow>().toggleToggleFollow = true;
            Camera.main.transform.GetComponent<CameraZoom>().toggleZoom = true;
            Camera.main.transform.GetComponent<CameraZoom>().toggleToggleZoom = true;

            tempMonkey = new GameObject("Monkey", typeof(SpriteRenderer));
            tempMonkey.GetComponent<SpriteRenderer>().sprite = game.monkeyTemplate.GetComponent<SpriteRenderer>().sprite;
            tempMonkey.GetComponent<SpriteRenderer>().sortingLayerName = "Monkeys";
            tempMonkey.GetComponent<SpriteRenderer>().sortingOrder = 3;

            placeMonkey = true;

            PauseButton pause = GameObject.Find("Pause").GetComponent<PauseButton>();
            pause.Pause();
        }
    }

    public void CreateMonkey()
    {
        if (Input.GetKeyDown("escape"))
        {
            Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
            Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;

            foreach (Transform child in GameObject.Find("Buttons").transform)
            {
                child.gameObject.GetComponent<Image>().raycastTarget = true;
            }

            placeMonkey = false;
            Destroy(tempMonkey);

            PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
            play.Resume();
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempMonkey.transform.position = new Vector3(mousePos.x, mousePos.y, -8f);
            tempMonkey.transform.localScale = new Vector3(GameObject.Find("Size").GetComponent<Slider>().value, GameObject.Find("Size").GetComponent<Slider>().value, 1);

            if (game.SafeSpawn(tempMonkey.transform.position, "monkey"))
            {
                tempMonkey.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.85f);
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject monkey = game.SpawnMonkey(tempMonkey.transform.position);

                    string[] name = GameObject.Find("Name").GetComponent<InputField>().text.Split(' ');
                    monkey.GetComponent<MonkeyGenes>().firstName = name[0];
                    if (name.Length != 1)
                    {
                        monkey.GetComponent<MonkeyGenes>().lastName = name[1];
                    }

                    monkey.GetComponent<MonkeyEnergy>().energy = System.Convert.ToInt32(GameObject.Find("Energy").GetComponent<InputField>().text);
                    monkey.GetComponent<MonkeyStates>()._state = "Confused";
                    monkey.GetComponent<MonkeyStates>().natural = false;
                    monkey.GetComponent<MonkeyStates>().generation = 0;

                    monkey.GetComponent<MonkeyGenes>().intelligence = GameObject.Find("Intelligence").GetComponent<Slider>().value;
                    monkey.GetComponent<MonkeyGenes>().size = GameObject.Find("Size").GetComponent<Slider>().value;
                    monkey.GetComponent<MonkeyGenes>().targetingSpeed = GameObject.Find("Targeting Speed").GetComponent<Slider>().value;
                    monkey.GetComponent<MonkeyGenes>().wanderingSpeed = GameObject.Find("Wandering Speed").GetComponent<Slider>().value;
                    monkey.GetComponent<MonkeyGenes>().targetingStamina = (int)GameObject.Find("Targeting Stamina").GetComponent<Slider>().value;
                    monkey.GetComponent<MonkeyGenes>().wanderingStamina = (int)GameObject.Find("Wandering Stamina").GetComponent<Slider>().value;
                    monkey.GetComponent<MonkeyGenes>().maxClimb = (int)GameObject.Find("Max Climb").GetComponent<Slider>().value;
                    monkey.GetComponent<MonkeyGenes>().breedingThreshold = (int)GameObject.Find("Breeding Threshold").GetComponent<Slider>().value;
                    monkey.GetComponent<MonkeyGenes>().babyEnergy = (int)GameObject.Find("Energy Passover").GetComponent<Slider>().value;

                    foreach (Transform child in GameObject.Find("Buttons").transform)
                    {
                        child.gameObject.GetComponent<Image>().raycastTarget = true;
                    }

                    Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
                    Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = false;

                    placeMonkey = false;
                    Destroy(tempMonkey);

                    PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
                    play.Resume();
                }
            }
            else
            {
                tempMonkey.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.85f);
            }
        }
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
