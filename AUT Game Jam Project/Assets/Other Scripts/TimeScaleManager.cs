using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public void AnimationEndOfTeleportFinish()
    {
        transform.root.GetComponent<CharacterMovement>().autoAnimation = true;
    }

    public void AnimationEndOfTeleportStart()
    {
        Time.timeScale = transform.root.GetComponent<TeleportManager>().slowMotionScale;
    }
}
