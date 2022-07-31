using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RTLTMPro;
using DG.Tweening;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public TextMeshProUGUI nameText;
    public RTLTextMeshPro nameText;
    //public TextMeshProUGUI dialogueText;
    public RTLTextMeshPro dialogueText;
    [Space]
    public RectTransform dialogueParent;
    public Image backgroundImage;
    public float backgroundAlpha = 0.7f;
    public float animationDuration = 1;
    [Space]
    public float dialogueTyperDuration = 0.1f;
    [Space]
    public AudioSource source;
    [Space]
    public bool typeSentences = false;
    [Space]
    public Image speakerImage;
    public List<SpriteAndNameMatch> matches = new List<SpriteAndNameMatch>();

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

        backgroundImage.DOComplete();
        backgroundImage.gameObject.SetActive(true);
        backgroundImage.DOFade(backgroundAlpha, animationDuration);

        sentences.Clear();

        dialogueText.text = "";
        nameText.text = "";

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

        nameText.text = name;

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
        if (typeSentences)
            StartCoroutine(TypeSentence(sentence, dialogueTyperDuration));
        else
            dialogueText.text = sentence;

        UpdateSpeakerImage();
    }

    IEnumerator TypeSentence(string sentence, float typeDelay = 0)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text = dialogueText.OriginalText + letter;

            if (typeDelay <= 0)
                yield return null;
            else
                yield return new WaitForSeconds(typeDelay);
        }
    }

    public void EndDialogue()
    {
        dialogueParent.DOMoveY(-dialogueParent.sizeDelta.y * 2, animationDuration)
            .OnComplete(() => ShowingDialogue = false) ;

        backgroundImage.DOComplete();
        backgroundImage.DOFade(0, animationDuration).OnComplete(()=> { backgroundImage.gameObject.SetActive(false); });
    }

    private void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
        clips = new Queue<AudioClip>();
        names = new Queue<string>();

        Vector2 pos = dialogueParent.position;
        pos.y = -dialogueParent.sizeDelta.y * 2;
        dialogueParent.position = pos;

        speakerImage.gameObject.SetActive(false);

        Color c = backgroundImage.color;
        c.a = 0;
        backgroundImage.color = c;
        backgroundImage.gameObject.SetActive(false);
    }

    private void UpdateSpeakerImage()
    {
        foreach (var match in matches)
        {
            if (match.IsMatch(nameText.OriginalText))
            {
                speakerImage.sprite = match.sprite;
                speakerImage.gameObject.SetActive(true);
                return;
            }
        }
        speakerImage.gameObject.SetActive(false);
    }
}
[System.Serializable]
public class SpriteAndNameMatch
{
    public string text;
    public Sprite sprite;
    public bool IsMatch(string current)
    {
        return text == current;
    }
}