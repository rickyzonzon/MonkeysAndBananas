using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CreateTreeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    private GameController game;
    private GameObject tempTree;
    private bool placeTree = false;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (placeTree)
        {
            CreateTree();
        }
    }

    void TaskOnClick()
    {
        transform.parent.GetComponent<Canvas>().enabled = false;
        Camera.main.transform.GetComponent<CameraFollow>().toggleFollow = true;
        Camera.main.transform.GetComponent<CameraFollow>().toggleToggleFollow = true;
        Camera.main.transform.GetComponent<CameraZoom>().toggleZoom = true;
        Camera.main.transform.GetComponent<CameraZoom>().toggleToggleZoom = true;

        tempTree = new GameObject("Tree", typeof(SpriteRenderer));
        tempTree.GetComponent<SpriteRenderer>().sprite = game.trees[(int)(transform.parent.Find("Height").GetComponent<Slider>().value) - 1].GetComponent<SpriteRenderer>().sprite;
        tempTree.GetComponent<SpriteRenderer>().sortingLayerName = "Monkeys";
        tempTree.GetComponent<SpriteRenderer>().sortingOrder = 3;

        placeTree = true;

        PauseButton pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        pause.Pause();
    }

    public void CreateTree()
    {
        if (Input.GetKeyDown("escape"))
        {
            Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
            Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;
            
            foreach (Transform child in GameObject.Find("Buttons").transform)
            {
                child.gameObject.GetComponent<Image>().raycastTarget = true;
            }

            placeTree = false;
            Destroy(tempTree);

            PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
            play.Resume();
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempTree.transform.position = new Vector3(mousePos.x, mousePos.y, -8f);

            if (game.SafeSpawn(tempTree.transform.position, "tree"))
            {
                tempTree.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.85f);
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject tree = game.SpawnTree(tempTree.transform.position, (int)transform.parent.Find("Height").GetComponent<Slider>().value);
                    tree.GetComponent<TreeController>().natural = false;
                    tree.GetComponent<TreeController>().energy = System.Convert.ToInt32(transform.parent.Find("Tree Energy").GetComponent<InputField>().text);
                    
                    foreach (Transform child in GameObject.Find("Buttons").transform)
                    {
                        child.gameObject.GetComponent<Image>().raycastTarget = true;
                    }

                    Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
                    Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;
                    placeTree = false;
                    Destroy(tempTree);

                    PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
                    play.Resume();
                }
            }
            else
            {
                tempTree.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.85f);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnDown;
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btnUp;
    }
}
