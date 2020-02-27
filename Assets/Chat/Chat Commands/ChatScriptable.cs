using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ChatCommandSymbol { ExcalmationPoint ,QuestionMark};
[System.Serializable]
public class ChatCommand 
{
    public ChatCommandSymbol commandSymbol = ChatCommandSymbol.ExcalmationPoint;
    public string CommandKey = "!Example";
    public UnityEvent EventsToCall;
}
public class ChatScriptable : MonoBehaviour
{
    public static ChatScriptable Instance { get { return _instance; } }
    static ChatScriptable _instance;
    [SerializeField]
    List<ChatCommand> chatCommands = new List<ChatCommand>();
    // Update is called once per frame
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
    public bool CallCommand(string command)
    {
        foreach (var item in chatCommands)
        {
            if (command.StartsWith("!")|| command.StartsWith("?"))
            {
                if (item.CommandKey == command)
                {
                    item.EventsToCall.Invoke();
                    ChatController.Instance.SendOutMessage($"Command||GAMERTAG||{chatCommands[0].EventsToCall.GetPersistentMethodName(0)}");
                    return true;
                }
            }              
        }
        return false;
    }

    public void HELP_Chat()
    {
        ChatController.Instance.SendOutMessage($"Command||GAMERTAG||Hello world !");
        ChatController.Instance.SendOutMessage($"Command||GAMERTAG||Can't reall help with much there is one other command");
        ChatController.Instance.SendOutMessage($"Command||GAMERTAG||type !stats");
    }
}
