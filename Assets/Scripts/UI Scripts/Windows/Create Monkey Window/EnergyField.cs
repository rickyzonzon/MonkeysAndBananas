using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnergyField : MonoBehaviour
{
    private InputField inputField;

    void Start()
    {
        inputField = this.GetComponent<InputField>();
        inputField.onEndEdit.AddListener(delegate { TaskOnEnd(); });
    }

    public void TaskOnEnd()
    {
        if (System.Convert.ToInt32(inputField.text) <= 0)
        {
            inputField.text = "1";
        }
    }
}
