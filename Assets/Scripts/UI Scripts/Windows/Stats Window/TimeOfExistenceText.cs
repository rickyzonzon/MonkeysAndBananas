using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOfExistenceText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<Text>().text = GameObject.Find("GameController").GetComponent<GameController>().yearsOfExistence + " Years " 
            + GameObject.Find("GameController").GetComponent<GameController>().monthsOfExistence + " Months";
    }
}
