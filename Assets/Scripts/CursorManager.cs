using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D CursorTexture; 
    public Texture2D EnemyCursorTexture; 

    void Start()
    {
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Cursor.SetCursor(EnemyCursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}