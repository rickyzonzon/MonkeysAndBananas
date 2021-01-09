using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite btnUp;
    public Sprite btnDown;
    public bool delete = false;
    private GameObject prevObj;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        if (delete)
        {
            if (Input.GetKeyDown("escape"))
            {
                Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
                Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;

                foreach (Transform child in GameObject.Find("Buttons").transform)
                {
                    child.gameObject.GetComponent<Image>().raycastTarget = true;
                }

                prevObj = null;
                delete = false;

                PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
                play.Resume();
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.transform != null)
            {
                if (hit.transform.gameObject.tag == "collidable" || hit.transform.gameObject.tag == "tree")
                {
                    prevObj = hit.transform.gameObject;
                    prevObj.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.8f);

                    if (Input.GetMouseButtonDown(0))
                    {
                        Destroy(prevObj);
                        Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
                        Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;

                        foreach (Transform child in GameObject.Find("Buttons").transform)
                        {
                            child.gameObject.GetComponent<Image>().raycastTarget = true;
                        }

                        prevObj = null;
                        delete = false;

                        PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
                        play.Resume();
                    }
                }
            }
            else
            {
                try
                {
                    prevObj.GetComponent<SpriteRenderer>().color = Color.white;
                }
                catch (Exception e) { }

                if (Input.GetMouseButtonDown(0))
                {
                    Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = true;
                    Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = true;

                    foreach (Transform child in GameObject.Find("Buttons").transform)
                    {
                        child.gameObject.GetComponent<Image>().raycastTarget = true;
                    }

                    prevObj = null;
                    delete = false;

                    PlayButton play = GameObject.Find("Play").GetComponent<PlayButton>();
                    play.Resume();
                }
            }
        }
    }

    void TaskOnClick()
    {
        Camera.main.transform.GetComponent<CameraFollow>().target = null;
        Camera.main.transform.GetComponent<MonkeyInfoWindow>().toggleInfo = false;
        Camera.main.transform.GetComponent<TreeInfoWindow>().toggleInfo = false;

        foreach (Transform child in GameObject.Find("Buttons").transform)
        {
            child.gameObject.GetComponent<Image>().raycastTarget = false;
        }

        delete = true;

        PauseButton pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        pause.Pause();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.GetComponent<Image>().sprite = btnDown;
        GameObject.Find("Button Menu").GetComponent<UISFX>().PlayButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.parent.GetComponent<Image>().sprite = btnUp;
    }
}
