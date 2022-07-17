using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityOption : MonoBehaviour, IPointerDownHandler
{
    public AbilityDropdown dropdown;
    public void OnPointerDown(PointerEventData eventData)
    {
        dropdown.OnSelectOption(this);
    }
}
