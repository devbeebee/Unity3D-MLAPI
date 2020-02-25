using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseMenu : MonoBehaviour
{
    [SerializeField]
    Transform pauseMenu;
    private void Awake()
    {
        Transform ui = GameObject.Find("Main Canvas Holder").transform;
        pauseMenu= ui.Find("Player GUI/Pause Menu");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        }
    }
}
