using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BossAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Multiline]
    public List<string> tooltipTextsOrderSensitive = new List<string>();
    public BossAbilityManager abilityManager;
    public AbilityType type;
    int currentOption = 0;

    public void OnDropDownValueChange(int value)
    {
        currentOption = value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.ShowTooltip(tooltipTextsOrderSensitive[currentOption]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip();
    }

    public void GiveTypeToManager()
    {
        switch (type)
        {
            case AbilityType.Size:

                Abilities.SizeMode mode1 = (Abilities.SizeMode)currentOption;
                abilityManager.SetMode(mode1);

                break;

            case AbilityType.MoveType:

                Abilities.MoveMode mode2 = (Abilities.MoveMode)currentOption;
                abilityManager.SetMode(mode2);

                break;

            case AbilityType.AttackRange:

                Abilities.AttackMode mode3 = (Abilities.AttackMode)currentOption;
                abilityManager.SetMode(mode3);

                break;
            case AbilityType.Speed:

                Abilities.SpeedMode mode4 = (Abilities.SpeedMode)currentOption;
                abilityManager.SetMode(mode4);

                break;
        }
    }

    public enum AbilityType { Size, MoveType, AttackRange, Speed }
}
