using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public RectTransform cursor;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!cursor) return;

        cursor.position = Input.mousePosition;

        if (Input.anyKeyDown)
        {
            Cursor.visible = false;
        }
    }
}