using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPrototype : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
    }
}
