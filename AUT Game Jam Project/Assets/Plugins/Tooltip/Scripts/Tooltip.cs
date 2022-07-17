using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance { get; private set; }
    public Vector2 padding;
    
    public RectTransform canvasTransform;
    public RectTransform backgroundTransform;
    public TextMeshProUGUI text;

    private RectTransform rectTransform;
    private System.Func<string> getTooltipTextFunc;
    private void Awake()
    {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();

        if(!canvasTransform || !backgroundTransform || !text)
        {
            Debug.LogError("Assign in the inspector", this);
            return;
        }

        Hide();
    }
    private void SetText(string tooltipText)
    {
        text.SetText(tooltipText);
        text.ForceMeshUpdate();

        Vector2 textSize = text.GetRenderedValues(false);
        backgroundTransform.sizeDelta = textSize + padding;
    }
    private void Update()
    {
        SetText(getTooltipTextFunc());

        Vector2 anchoredPos = Input.mousePosition / canvasTransform.localScale.x;
        
        if(anchoredPos.x + backgroundTransform.rect.width > canvasTransform.rect.width)
        {
            anchoredPos.x = canvasTransform.rect.width - backgroundTransform.rect.width;
        }
        else if(anchoredPos.x < 0)
        {
            anchoredPos.x = 0;
        }

        if (anchoredPos.y + backgroundTransform.rect.height > canvasTransform.rect.height)
        {
            anchoredPos.y = canvasTransform.rect.height - backgroundTransform.rect.height;
        }
        else if (anchoredPos.y < 0)
        {
            anchoredPos.y = 0;
        }
        rectTransform.anchoredPosition = anchoredPos;
    }

    public void Show(string tooltipText)
    {
        Show(() => tooltipText);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(System.Func<string> getTooltipTextFunc)
    {
        this.getTooltipTextFunc = getTooltipTextFunc;
        gameObject.SetActive(true);
        SetText(getTooltipTextFunc());
    }

    public static void ShowTooltip(string tooltipText)
    {
        Instance.Show(tooltipText);
    }
    
    public static void ShowTooltip(System.Func<string> getTooltipTextFunc)
    {
        Instance.Show(getTooltipTextFunc);
    }

    public static void HideTooltip()
    {
        Instance.Hide();
    }
}
