using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumMutationsText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<Text>().text = GameObject.Find("GameController").GetComponent<GameController>().numMutations.ToString();
    }
}
