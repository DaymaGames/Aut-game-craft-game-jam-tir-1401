using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityDropdown : MonoBehaviour
{
    public List<AbilityOption> options = new List<AbilityOption>();

    [HideInInspector] public AbilityOption selectedOption;

    [Space]
    
    public Color selectedColor = Color.white;
    public Color normalColor = Color.white;

    [Space]

    public UnityEvent<int> OnValueChange;

    private void Awake()
    {
        if(selectedOption == null)
        {
            OnSelectOption(options[0]);
        }
    }

    public void OnSelectOption(AbilityOption option)
    {
        foreach (var item in options)
        {
            OnDeselectOption(item);
        }

        selectedOption = option;
        
        option.GetComponent<Image>().color = selectedColor;

        int index = GetSelectedIndex();

        OnValueChange.Invoke(index);
    }

    public void OnDeselectOption(AbilityOption option)
    {
        option.GetComponent<Image>().color = normalColor;
    }

    public int GetSelectedIndex()
    {
        for (int i = 0; i < options.Count; i++)
        {
            if(options[i] == selectedOption)
            {
                return i;
            }
        }

        Debug.LogError("Not Found", this);
        return -1;
    }

    public void SetIndex(int index)
    {
        OnSelectOption(options[index]);
    }

}
