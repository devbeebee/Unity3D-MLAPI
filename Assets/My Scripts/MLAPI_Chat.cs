using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MLAPI;
using MLAPI.NetworkedVar.Collections;
using MLAPI.Messaging;
using MLAPI.Connection;
using System;
[System.Serializable]
class ChatSettings
{
    public bool addNewMessagesToTop = false;
    [Range(1, 10)]
    public int MinimumMessageCharactersLength = 3;
    [Range(10, 150)]
    public int MaximumMessageCharactersLength = 100;
    [Range(1, 100)]
    public int MaxChatMessages = 5;
}
[System.Serializable]
class ChatUI_Setup
{
    public TextMeshProUGUI connectedPlayers;
    public TextMeshProUGUI playerConnectedName;
    public TextMeshProUGUI playersInChat;
    public TMP_InputField messageInput;
    public Button sendMessageBtn;
    public Button clearChatBtn;
    public ScrollRect scrollRect = null;
    public RectTransform container = null;
    public GameObject msgFab;
}
public class MLAPI_Chat : NetworkedBehaviour
{
    public static MLAPI_Chat Instance { get { return _instance; } }
    static MLAPI_Chat _instance;
    [Space, Header("MLAPI_Chat.Instance.SendOutMessage(string message);")]
    [Header("TO SEND A MESSAGE FROM ALTERNATE SCRIPT USE")]
    [Space]

    [SerializeField]
    ChatSettings chatSetup = new ChatSettings();
    [SerializeField]
    ChatUI_Setup chatUI_Setup = new ChatUI_Setup();

    Queue<GameObject> messages = new Queue<GameObject>();
    NetworkedList<string> ChatMessages = new NetworkedList<string>(new MLAPI.NetworkedVar.NetworkedVarSettings()
    {
        ReadPermission = MLAPI.NetworkedVar.NetworkedVarPermission.Everyone,
        WritePermission = MLAPI.NetworkedVar.NetworkedVarPermission.Everyone,
        SendTickrate = 5
    }, new List<string>());


     public void PopulatePlayers(List<string> Players)
    {
        chatUI_Setup.connectedPlayers.text = "";
        for (int i = 0; i < Players.Count; i++)
        {
            chatUI_Setup.connectedPlayers.text += $"{Players[i]}";
        }
        chatUI_Setup.playersInChat.SetText($"Total Players in chat : {Players.Count}");
    }
    #region Methods
    #region  Awake()
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
    private void Start()
    {
        StartOnChangeListeners();

        chatUI_Setup.sendMessageBtn.onClick.AddListener(MessageFromInput);
        chatUI_Setup.clearChatBtn.onClick.AddListener(ClearAllMesssages);
    }

    private void StartOnChangeListeners()
    {
        ChatMessages.OnListChanged += ((message) =>
        {
            AddMessage($"{ChatMessages[ChatMessages.Count - 1]}");
        });
    }


    #endregion  
    #region Public Methods
    public void SendOutMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            ChatMessages.Add(message);
        }
    }
    #endregion
    #region Private Methods

    void AddMessage(string msg)
    {
        string[] tempArr = MyStringExtenstions.SplitStringWithString(msg, "||GAMERTAG||");
        string gamerTag = tempArr[0];      
        string message = tempArr[1];
        GameObject go = Instantiate(chatUI_Setup.msgFab, chatUI_Setup.container);
        go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = $"Gamertag = {gamerTag}";
        go.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
        go.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = message;
        go.gameObject.SetActive(true);
        messages.Enqueue(go);

        if (chatSetup.addNewMessagesToTop)
        {
            go.transform.SetAsFirstSibling();
        }
        // remove older messages if there are too many
        if (messages.Count > chatSetup.MaxChatMessages)
        {
            go = messages.Dequeue();
            Destroy(go);
        }
        // auto-scroll
        if (gameObject.activeSelf)
        {
            StartCoroutine(AutoScroll());
        }
    }
    private IEnumerator AutoScroll()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatUI_Setup.container);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        chatUI_Setup.scrollRect.verticalNormalizedPosition = chatSetup.addNewMessagesToTop ? 1 : 0;
    }
 
    void MessageFromInput()
    {
        if (!string.IsNullOrEmpty(chatUI_Setup.messageInput.text))
        {
            string toSend = $"{PlayerProfile.Instance.Gamer_Profile.GamerTag}||GAMERTAG||{ chatUI_Setup.messageInput.text}";
            SendOutMessage(toSend);
            chatUI_Setup.messageInput.text = "";
        }
    }
    void ClearAllMesssages()
    {
        GameObject go;
        while (messages.Count > 0)
        {
            go = messages.Dequeue();
            Destroy(go);
        }
    }

    #endregion
    #endregion
}
