using MLAPI;
using MLAPI.Transports.UNET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
public class PHP_AddServer : NetworkedBehaviour
{
    public static PHP_AddServer Instance { get { return _instance; } }
    static PHP_AddServer _instance;
    DateTime time = new DateTime();
    public string addServerURL = "http://localhost/mydb/newgameserver.php?"; //be sure to add a ? to your url
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
    public void AddRandomServer()
    {
        string n = $"Unity";
        string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }
        StartCoroutine(PostServer(n, localIP, ""));
    }
    IEnumerator PostServer(string s_name, string s_ip, string s_pass)
    {
   
        UnityWebRequest www = UnityWebRequest.Get(addServerURL + $" server_name={s_name}&server_ip={s_ip}&server_password={s_pass}&server_lastping={MyStringExtenstions.ReturnTimeAsString()}");
        yield return www.SendWebRequest();
        WWw_ErrorCheck(www);
        www.Dispose();
        yield return PHP_GetHighScores.Instance.GetScores();
    }

    private static void WWw_ErrorCheck(UnityWebRequest www)
    {
        if (www.error != null)
        {
            print(www.error);
        }
        else
        {
            print(www.downloadHandler.text);
        }
    }
}
