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
    public static bool ShowingDialogue = false;

    private Queue<string> sentences;
    private Queue<AudioClip> clips;
    private Queue<string> names;

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueParent.DOMoveY(0, animationDuration)
            .OnComplete(()=> DisplayNextSentence())
            .SetEase(Ease.InOutCubic);

        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (var clip in dialogue.sounds)
        {
            clips.Enqueue(clip);
        }
        foreach (var name in dialogue.names)
        {
            names.Enqueue(name);
        }
    }

    public void DisplayNextSentence()
    {
        ShowingDialogue = true;

        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string name = names.Dequeue();

        nameText.SetText(name);

        string sentence = sentences.Dequeue();

        source.Stop();
        if (clips.Count != 0)
        {
            AudioClip clipToPlay = clips.Dequeue();

            if (clipToPlay && source)
            {
                source.PlayOneShot(clipToPlay);
            }
        }

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

    public void EndDialogue()
    {
        dialogueParent.DOMoveY(-dialogueParent.sizeDelta.y - 10, animationDuration)
            .OnComplete(() => ShowingDialogue = false) ;
    }

    private void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
        clips = new Queue<AudioClip>();
        names = new Queue<string>();
    }
}