using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MyGUI_Instance : MonoBehaviour
{
    public GameObject MainMenu;

    private static MyGUI_Instance _instance;
    public static MyGUI_Instance Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
