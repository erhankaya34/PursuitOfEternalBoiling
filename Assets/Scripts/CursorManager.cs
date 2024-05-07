using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D CursorTexture; 
    public Texture2D EnemyCursorTexture; 

    void Start()
    {
        // İlk başta varsayılan imleci ayarlayın
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        // Mouse pozisyonunu ekran koordinatlarından dünya koordinatlarına çevir
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Yeni imleç için raycast yap
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        
        // Eğer bir collider ile çarpışma varsa
        if (hit.collider != null)
        {
            // Eğer çarpışan obje "Enemy" etiketine sahipse düşman imleci kullan
            if (hit.collider.CompareTag("Enemy"))
            {
                Cursor.SetCursor(EnemyCursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                // Eğer çarpışan obje düşman değilse varsayılan imleci kullan
                Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            // Hiçbir şeyle çarpışma yoksa varsayılan imleci kullan
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