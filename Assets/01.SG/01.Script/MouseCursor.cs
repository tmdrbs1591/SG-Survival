using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{

    [SerializeField] Texture2D cursorImage;

    void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }


    void Update()
    {

    }
}
