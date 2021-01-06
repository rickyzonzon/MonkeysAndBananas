using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public interface IFields<T>
{
    void TaskOnChange();

    void TaskOnEnd();

    void InputFieldUpdate();
}
