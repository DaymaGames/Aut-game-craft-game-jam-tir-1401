using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BootManager
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Boot()
    {
        Object obj = Object.Instantiate(Resources.Load("System"));
        obj.name = "SYSTEM";
        Object.DontDestroyOnLoad(obj);
    }
}