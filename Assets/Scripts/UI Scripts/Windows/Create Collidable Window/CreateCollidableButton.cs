using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CreateCollidableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    private GameController game;
    private GameObject tempObj;
    private bool placeObj = false;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (placeObj)
        {
            CreateCollidable();
        }
    }

    void TaskOnClick()
    {
        if (GameObject.Find("Boulder").GetComponent<BoulderButton>().selected || GameObject.Find("Water").GetComponent<WaterButton>().selected || GameObject.Find("Bamboo").GetComponent<BambooButton>().selected)
        {
            transform.parent.GetComponent<Canvas>().enabled = false;
            Camera.main.transform.GetComponent<CameraFollow>().toggleFollow = true;
            Camera.main.transform.GetComponent<CameraFollow>().toggleToggleFollow = true;
            Camera.main.transform.GetComponent<CameraZoom>().toggleZoom = true;
            Camera.main.transform.GetComponent<CameraZoom>().toggleToggleZoom = true;

            tempObj = new GameObject("Obstacle", typeof(SpriteRenderer));

            if (GameObject.Find("Boulder").GetComponent<BoulderButton>().selected)
            {
                tempObj.GetComponent<SpriteRenderer>().sprite = game.collidables[0].GetComponent<SpriteRenderer>().sprite;
            }
            else if (GameObject.Find("Water").GetComponent<WaterButton>().selected)
            {
                tempObj.GetComponent<SpriteRenderer>().sprite = game.collidables[1].GetComponent<SpriteRenderer>().sprite;
            }
            else if (GameObject.Find("Bamboo").GetComponent<BambooButton>().selected)
            {
                tempObj.GetComponent<SpriteRenderer>().sprite = game.collidables[2].GetComponent<SpriteRenderer>().sprite;
            }

            tempObj.GetComponent<SpriteRenderer>().sortingLayerName = "Monkeys";
            tempObj.GetComponent<SpriteRenderer>().sortingOrder = 3;

            placeObj = true;

            PauseButton pause = GameObject.Find("Pause").GetComponent<PauseButton>();
            pause.Pause();
        }
    }

    public void CreateCollidable()
    {
        if (Input.GetKeyDown("escape"))
        {
            Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
            Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;
            
            foreach (Transform child in GameObject.Find("Buttons").transform)
            {
                child.gameObject.GetComponent<Image>().raycastTarget = true;
            }

            placeObj = false;
            Destroy(tempObj);

            PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
            play.Resume();
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempObj.transform.position = new Vector3(mousePos.x, mousePos.y, -8f);

            if (game.SafeSpawn(tempObj.transform.position, "object"))
            {
                tempObj.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.85f);
                if (Input.GetMouseButtonDown(0))
                {
                    if (GameObject.Find("Boulder").GetComponent<BoulderButton>().selected)
                    {
                        GameObject obj = game.SpawnObject(tempObj.transform.position, 0);
                    }
                    else if (GameObject.Find("Water").GetComponent<WaterButton>().selected)
                    {
                        GameObject obj = game.SpawnObject(tempObj.transform.position, 1);
                    }
                    else if (GameObject.Find("Bamboo").GetComponent<BambooButton>().selected)
                    {
                        GameObject obj = game.SpawnObject(tempObj.transform.position, 2);
                    }

                    foreach (Transform child in GameObject.Find("Buttons").transform)
                    {
                        child.gameObject.GetComponent<Image>().raycastTarget = true;
                    }

                    Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
                    Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;
                    placeObj = false;
                    Destroy(tempObj);

                    PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
                    play.Resume();
                }
            }
            else
            {
                tempObj.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.85f);
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
