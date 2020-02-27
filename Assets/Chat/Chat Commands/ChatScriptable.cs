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
public class ChatScriptable : MonoBehaviour
{
    public static ChatScriptable Instance { get { return _instance; } }
    static ChatScriptable _instance;
    [SerializeField]
    List<ChatCommand> Commands = new List<ChatCommand>();
    [SerializeField]
    [TextArea(5, 15)]
    string HelpMessage = "";
    private void Awake()
    {
        Commands.Add(DefaultHelp());
        Commands.Add(DefaultServer());
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
    }
    ChatCommand DefaultHelp()
    {
        ChatCommand cc = new ChatCommand();
        cc.CommandKey = "?help";
        cc.EventsToCall.AddListener(HELP_Chat);
        return cc;
    }
    ChatCommand DefaultServer()
    {
        ChatCommand cc = new ChatCommand();
        cc.CommandKey = "?server";
        cc.EventsToCall.AddListener(Server_Chat);
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

    public void Server_Chat()
    {
        ChatController.Instance.AddMessage($"System Message||GAMERTAG||<color=#000000FF>Server Owner TBA</color>");
    }
}
