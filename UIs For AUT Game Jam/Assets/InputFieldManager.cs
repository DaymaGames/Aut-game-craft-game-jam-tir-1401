using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Sirenix.OdinInspector;

public class InputFieldManager : MonoBehaviour
{
    public List<CMDAction> actions = new List<CMDAction>();

    public void OnTextChanged(string text)
    {
        foreach (CMDAction action in actions)
        {
            if(action.text == text)
            {
                action.onTextInput.Invoke();
                return;
            }
        }
    }
}
[Serializable]
public class CMDAction
{
    public string text;
    public UnityEvent onTextInput;
}