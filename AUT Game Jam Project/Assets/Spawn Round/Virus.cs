using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Virus : MonoBehaviour
{
    public System.Action OnDieAction;
    private void Awake()
    {
        GetComponent<Health>().OnDie += () => OnDieAction?.Invoke();
    }
}
