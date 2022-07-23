using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class RTLTextSetter : MonoBehaviour
{
    public string text;
    public RTLTextMeshPro textMeshPro;
    public void SetText()
    {
        print(text);
        textMeshPro.text += text;
    }
}
