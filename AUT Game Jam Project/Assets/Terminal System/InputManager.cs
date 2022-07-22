using System;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.Events;



public class InputManager : MonoBehaviour
{
    AudioSource audioSource;
    [Header("KeyBoard Sound Effect")]
    [SerializeField] AudioClip audioClip;
    [Space]
    [Header("Delay before input activision")]
    [SerializeField] float typeTimeStartDelay = 1;
    [Space]
    [SerializeField] TMP_InputField inputFiled;
    [SerializeField] string currentInput;
    [Space]
    [Header("Warning! Use small letters for Command Input")]
    public List<CMDActions> cMDAction;
    
    public void OnEndEdit()
    {
        
        currentInput = inputFiled.text.ToLower(); 
        
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
    private void Start()
    {
        Invoke(nameof(SelectInputFiled), typeTimeStartDelay);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0) ||
                Input.GetMouseButtonDown(1) ||
                Input.GetMouseButtonDown(2))
                return;

            audioSource.PlayOneShot(audioClip);
        }
    }


    void SelectInputFiled()
    {
        inputFiled.Select();


    }

}


[Serializable]
public class CMDActions
{
    public string commandInput;
    public UnityEvent action;
}

