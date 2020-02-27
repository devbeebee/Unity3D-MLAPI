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

    private void Awake()
    {
        Commands.Add(DefaultHelp());
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    ChatCommand DefaultHelp() 
    {
        ChatCommand h = new ChatCommand();
        h.CommandKey = "?Help";
        h.EventsToCall.AddListener(HELP_Chat);
        return h;
    }
    public bool CallCommand(string command)
    {
        foreach (var item in Commands)
        {
            if (command.StartsWith("!")|| command.StartsWith("?"))
            {
                if (item.CommandKey == command)
                {
                    ChatController.Instance.SendOutMessage($"Command||GAMERTAG||{Commands[0].EventsToCall.GetPersistentMethodName(0)}");
                    item.EventsToCall.Invoke();
                    return true;
                }
            }              
        }
        return false;
    }

    public void HELP_Chat()
    {
        string abc = "\nHello world ! \nCan't reall help with much there is one other command\ntype !stats";
        ChatController.Instance.SendOutMessage($"Command||GAMERTAG||<color =black>{abc}</color>");
    }
}
