using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkManager : NetworkedBehaviour
{
    public void Disconnect()
    {
        if (IsHost)
        {
            NetworkingManager.Singleton.StopHost();
        }
        else if (IsClient)
        {
            NetworkingManager.Singleton.StopClient();
        }
        else if (IsServer)
        {
            NetworkingManager.Singleton.StopServer();
        }
        MyGUI_Instance.Instance.MainMenu.SetActive(true);
    }
}
