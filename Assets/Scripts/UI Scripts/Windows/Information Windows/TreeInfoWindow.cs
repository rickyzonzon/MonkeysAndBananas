using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeInfoWindow : MonoBehaviour
{
    private Canvas window;
    public bool toggleInfo = true;
    public GameObject tree;
    private int height;
    private int energy;
    private GameController game;
    private bool showInfo = false;
    private int count = 0;

    void Start()
    {
        window = GameObject.Find("Tree Info").GetComponent<Canvas>();
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

            if (target == null || target.gameObject.tag == "monkey")
            {
                count = 0;
                tree = null;
                showInfo = false;
            }
            else
            {
                tree = target.gameObject;
                if (count == 1)
                {
                    foreach (RectTransform child in window.transform)
                    {
                        child.transform.position = Camera.main.WorldToScreenPoint(tree.transform.position);
                    }
                }
                height = tree.GetComponent<TreeController>().height;
                energy = tree.GetComponent<TreeController>().energy;
                showInfo = true;
            }   
        }
        else
        {
            count = 0;
            tree = null;
            showInfo = false;
        }
    }

    void OnGUI()
    {
        if (showInfo)
        {
            window.transform.Find("Tree Energy").Find("Text").GetComponent<Text>().text = "" + energy;
            window.transform.Find("Height").Find("Text").GetComponent<Text>().text = "" + height;

            Vector3 treePos = Camera.main.WorldToScreenPoint(tree.transform.position);
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
                    if (game.paused)
                    {
                        grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(0.15f, 0.15f, 1f), 3.5f / 100);
                    }
                    else
                    {
                        grandchild.localScale = Vector3.Lerp(grandchild.localScale, new Vector3(0.15f, 0.15f, 1f), 3.5f * Time.deltaTime);
                    }
                }

                if (child.transform.name == "Height")
                {
                    targetPos.x = treePos.x;
                    targetPos.y = treePos.y + 15;
                }
                else 
                {
                    targetPos.x = treePos.x;
                    targetPos.y = treePos.y;
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
