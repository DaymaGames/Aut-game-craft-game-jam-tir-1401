using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using TMPro;
using Sirenix.OdinInspector;

public class RTLTextSetter : MonoBehaviour
{
    public string text;
    public RTLTextMeshPro textMeshPro;
    public float writeTime = 0.05f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetText();
        }

    }

    public void SetText()
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(writeTime));
    }
    IEnumerator TypeText(float writeTime)
    {
        foreach (var c in text.ToCharArray())
        {
            textMeshPro.text += c;

            yield return new WaitForSeconds(writeTime);
        }
    }
}
