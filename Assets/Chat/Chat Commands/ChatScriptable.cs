using MLAPI;
using MLAPI.Transports.UNET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChatCommand
{
    public string CommandKey = "!Example";
    public UnityEvent EventsToCall = new UnityEvent();
}
public class ChatScriptable : NetworkedBehaviour
{
    public static ChatScriptable Instance { get { return _instance; } }
    static ChatScriptable _instance;
    [SerializeField]
    List<ChatCommand> Commands = new List<ChatCommand>();
    [SerializeField]
    [TextArea(5, 15)]
    string HelpMessage = "";
    UnetTransport unetTransport;
    private void Awake()
    {
        Commands.Add(DefaultHelp());
        Commands.Add(DefaultConnection());
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        ChatController.Instance.AddMessage($"System Message||GAMERTAG||<color=#000000FF>Type </color><color=#E05656FF>?Help</color><color=#000000FF> to view Commands</color>");
        unetTransport = NetworkingManager.Singleton.NetworkConfig.NetworkTransport.GetComponent<UnetTransport>();
    }
    ChatCommand DefaultHelp()
    {
        ChatCommand cc = new ChatCommand();
        cc.CommandKey = "?help";
        cc.EventsToCall.AddListener(HELP_Chat);
        return cc;
    }
    ChatCommand DefaultConnection()
    {
        ChatCommand cc = new ChatCommand();
        cc.CommandKey = "?conDetails";
        cc.EventsToCall.AddListener(Connection_Chat);
        return cc;
    }
    public bool CallCommand(string command)
    {
        foreach (var item in Commands)
        {
            if (item.CommandKey == command)
            {
                item.EventsToCall.Invoke();
                return true;
            }
        }
        return false;
    }

    public void HELP_Chat()
    {
        ChatController.Instance.AddMessage($"Command Call||GAMERTAG||<color=#000000FF>{HelpMessage}</color>");
    }

    public void Connection_Chat()
    {
        ChatController.Instance.AddMessage($"System Message||GAMERTAG||Connection Details<color=#000000FF>{unetTransport.ConnectAddress}:{unetTransport.ConnectPort}</color>");
    }
}
