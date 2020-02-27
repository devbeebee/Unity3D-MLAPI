using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatExampleGUI : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUILayout.Button("Send Hello"))
        {
            ChatController.Instance.SendOutMessage($"{PlayerProfile.Instance.Gamer_Profile.GamerTag}||GAMERTAG||<color=black>Hello World</color>");
        }

        if (GUILayout.Button("Send Hello From Input"))
        {
            ChatController.Instance.SendOutMessage($"{PlayerProfile.Instance.Gamer_Profile.GamerTag}||GAMERTAG||<color=black> { ChatController.Instance.inputField.text}</color>");
        }
    }
}
