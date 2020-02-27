using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;

public class PlayerNetworkStart : NetworkedBehaviour
{
    public static PlayerNetworkStart Instance { get { return _instance; } }
    static PlayerNetworkStart _instance;
    [SerializeField]
    bool drawOnGUI = false;
    [SerializeField]
    bool network = false;
    public UnetTransport unetTransport;
    private void OnGUI()
    {
        if (Application.isEditor && drawOnGUI && network)
        {
            GUILayout.Label($"{unetTransport.ConnectAddress},{unetTransport.ConnectPort}");
        }
    }

    public override void NetworkStart()
    {
        unetTransport = NetworkingManager.Singleton.GetComponent<UnetTransport>();
        network = true;
    }
}
