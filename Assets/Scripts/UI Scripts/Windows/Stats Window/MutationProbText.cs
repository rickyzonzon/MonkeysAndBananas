﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutationProbText : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Text>().text = GameObject.Find("GameController").GetComponent<GameController>().mutationProbability.ToString();
    }
}
