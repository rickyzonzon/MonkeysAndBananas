using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindows : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Canvas>().enabled = false;
    }
}
