using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeRateText : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Text>().text = GameObject.Find("GameController").GetComponent<GameController>().treeSpawnFreq.ToString();
    }
}
