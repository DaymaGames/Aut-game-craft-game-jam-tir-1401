using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ColliderIn : MonoBehaviour
{
    public Collider2D thisColl;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (thisColl.bounds.Contains(other.bounds.min) &&
            thisColl.bounds.Contains(other.bounds.max))
        {
            print(other.name);
        }
    }
}
