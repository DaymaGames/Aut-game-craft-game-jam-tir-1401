using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    [Space]
    public RectTransform dialogueParent;
    public float animationDuration = 1;
    [Space]
    public float dialogueTyperDuration = 0.1f;
    [Space]
    public AudioSource source;

    public static DialogueManager Instance { get; private set; }

    private Queue<string> sentences;

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueParent.DOMoveY(0, animationDuration)
            .OnComplete(()=> DisplayNextSentence(dialogue.sound))
            .SetEase(Ease.InOutCubic);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence(AudioClip clip)
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (source && clip)
            source.PlayOneShot(clip);

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, dialogueTyperDuration));
    }

    IEnumerator TypeSentence(string sentence, float typeDelay = 0)
    {
        dialogueText.SetText("");
        foreach (var letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (typeDelay <= 0)
                yield return null;
            else
                yield return new WaitForSeconds(typeDelay);
        }
    }

    private void EndDialogue()
    {
        dialogueParent.DOMoveY(-dialogueParent.sizeDelta.y - 10, animationDuration);
    }

    private void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
    }
}