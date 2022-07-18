using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    [Button("Trigger Dialogue")]
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
