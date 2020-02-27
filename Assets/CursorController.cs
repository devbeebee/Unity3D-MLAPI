using System;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    bool drawOnGUI = false;
    private void Update()
    {
        switch (Cursor.lockState)
        {
            case CursorLockMode.None:
                if (Input.GetMouseButton(1))
                    HideCursor();
                if (Input.GetKeyDown(KeyCode.Escape))
                    ConfineCursor();
                break;
            case CursorLockMode.Confined:
                if (Input.GetKeyDown(KeyCode.Escape))
                    HideCursor();
                break;
            case CursorLockMode.Locked:
                if (Input.GetKeyDown(KeyCode.Escape))
                    ShowCursor();
                break;
        }
    }

    private void OnGUI()
    {
        if (Application.isEditor && drawOnGUI)
        {
            GUI.Label(new Rect(10, 10, 300, 60), $"Time : is <color=#0000ffff>{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}</color>");
            switch (Cursor.lockState)
            {
                case CursorLockMode.None:
                    GUI.Label(new Rect(10, 25, 300, 60), "Cursor: Is Free\nLock Cursor: Right Click \nConfine Cursor: Esc");
                    break;
                case CursorLockMode.Confined:
                    GUI.Label(new Rect(10, 25, 300, 60), "Cursor: Is Confined\nShow Cursor: Esc");
                    break;
                case CursorLockMode.Locked:
                    GUI.Label(new Rect(10, 25, 300, 60), "Cursor: is Locked\nShow Cursor: Esc");
                    break;
            }
        }
    }

    public static void DisplayCursor(bool _visible)
    {
        if (_visible)
        {
            ShowCursor();
        }
        else
        {
            HideCursor();
        }
    }
    public static void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public static void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public static void ConfineCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
