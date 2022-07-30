using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Spawner script;
    public string keyName;

    private CanvasGroup group;
    private void Awake()
    {
        group = script.GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (group.interactable == false) return;
        Tooltip.ShowTooltip(keyName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (group.interactable == false) return;
        Tooltip.HideTooltip();
    }
}
