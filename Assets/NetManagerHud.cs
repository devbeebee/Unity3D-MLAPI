using MLAPI;
using System.Collections.Generic;
using UnityEngine;

public class NetManagerHud : NetworkedBehaviour
{
    public void ConnectAsClient()
    {
        DevLogger.Instance.LogMssage("Connect As Client");
        NetworkingManager.Singleton.StartClient();
        MyGUI_Instance.Instance.MainMenu.SetActive(false);
    }
    public void ConnectAsHost()
    {
        DevLogger.Instance.LogMssage("Connect As Host");
        NetworkingManager.Singleton.StartHost();
        MyGUI_Instance.Instance.MainMenu.SetActive(false);
    }
}