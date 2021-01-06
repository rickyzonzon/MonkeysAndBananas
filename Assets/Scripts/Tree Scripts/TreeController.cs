using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public int numTree;
    public int height;
    public int energy;
    public bool natural = true;

    // Start is called before the first frame update
    void Start()
    {
        numTree = GameObject.Find("GameController").GetComponent<GameController>().totalTrees;
        if (gameObject.name.Contains("1"))
        {
            height = 1;
        }
        else if (gameObject.name.Contains("2"))
        {
            height = 2;
        }
        else if (gameObject.name.Contains("3"))
        {
            height = 3;
        }
        else if (gameObject.name.Contains("4"))
        {
            height = 4;
        }
        else
        {
            height = 5;
        }

        if (natural)
        {
            energy = Random.Range(6, 27);
        }
    }
}
