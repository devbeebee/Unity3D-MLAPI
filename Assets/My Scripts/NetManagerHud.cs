using MLAPI;
using System.Collections.Generic;
using UnityEngine;

public class NetManagerHud : NetworkedBehaviour
{
    public void ConnectAsClient()
    {
        NetworkingManager.Singleton.StartClient();
        MyGUI_Instance.Instance.MainMenu.SetActive(false);
    }
    public void ConnectAsHost()
    {
        NetworkingManager.Singleton.StartHost();
        MyGUI_Instance.Instance.MainMenu.SetActive(false);
    }
}