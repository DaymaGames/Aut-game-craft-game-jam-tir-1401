using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    AudioSource audioSource;
    [Header("KeyBoard Sound Effect")]
    [SerializeField] AudioClip audioClip;
    [Space]
    [Header("Delay before input activision")]
    [SerializeField] bool autoSelect = false;
    [SerializeField] float selectRepeatRate = .5f;
    [Space]
    [SerializeField] InputFieldWithSelected inputFiled;
    [SerializeField] string currentInput;
    [Space]
    [Header("Warning! Use small letters for Command Input")]
    public List<CMDActions> cMDAction;
    [Space]
    public bool isSelected = false;
    public void OnEndEdit(string input)
    {
        currentInput = input.ToLower(); 
        
        CheckInput(currentInput);
    }

    void CheckInput(string currentInput)
    {
        foreach (CMDActions cmdCommand in cMDAction)
        {
            if (cmdCommand.commandInput == currentInput)
            {
                cmdCommand.action.Invoke();
                return;
            }
        }

    }
    
    private void Awake()
    { 
        audioSource = GetComponent<AudioSource>();
        
    }

    private void OnEnable()
    {
        inputFiled.Select();
    }

    private void Start()
    {
        InvokeRepeating(nameof(SelectInputFiled),0, selectRepeatRate);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0) ||
                Input.GetMouseButtonDown(1) ||
                Input.GetMouseButtonDown(2))
                return;

            if (audioSource)
                audioSource.PlayOneShot(audioClip);
        }

        isSelected = inputFiled.isSelected;
    }

    void SelectInputFiled()
    {
        if (autoSelect && inputFiled.isSelected == false)
            inputFiled.Select();
    }
}


[Serializable]
public class CMDActions
{
    public string commandInput;
    public UnityEvent action;
}

