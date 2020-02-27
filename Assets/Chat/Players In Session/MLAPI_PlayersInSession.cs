using MLAPI;
using MLAPI.Connection;
using MLAPI.NetworkedVar.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MLAPI_PlayersInSession : NetworkedBehaviour
{
    public static MLAPI_PlayersInSession Instance { get { return _instance; } }
    static MLAPI_PlayersInSession _instance;
    [SerializeField]

    private List<string> chatItems;
    NetworkedList<string> PlayersConnected = new NetworkedList<string>(new MLAPI.NetworkedVar.NetworkedVarSettings()
    {
        ReadPermission = MLAPI.NetworkedVar.NetworkedVarPermission.Everyone,
        WritePermission = MLAPI.NetworkedVar.NetworkedVarPermission.Everyone,
        SendTickrate = 5
    }, new List<string>());
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
        StartOnChangeListeners();
    }

    public override void NetworkStart()
    {
        if (IsClient)
        {
            AddPlayer($"{PlayerProfile.Instance.Gamer_Profile.GamerTag}   ID = {NetworkingManager.Singleton.ServerClientId}");
            ChatController.Instance.SendOutMessage($"SYSTEM||GAMERTAG||<color=#00ff00ff> {PlayerProfile.Instance.Gamer_Profile.GamerTag}</color><color=black> Has Connected </color>");
        }

    }
    private void StartOnChangeListeners()
    {
        PlayersConnected.OnListChanged += ((message) =>
        {
            PopulatePlayers(message.value);
        });
    }
    public void PopulatePlayers(string Players)
    {
        if (IsClient)
        {
         AddMessageToChat($"{Players}");
            
        }
    }
    void AddPlayer(string nameAndId)
    {
        if (!PlayersConnected.Contains(nameAndId))
        {
            PlayersConnected.Add(nameAndId);
        }
    
}

    public int maxMessagesCount;  // the max number of messages on the chat window
    public RectTransform contentTransform;
    private Text contentText;
    public ScrollRect chatView;
    private float emptyContentSize;
    private float contentWidth;
    private float oneLineHeigth;  // the hight of one line in the conent text, we need it for initial spacings
    private int chanContentLinesCount;
    private TextGenerator contentTextGenerator;
    private TextGenerationSettings contentTextGeneratorSettings;

    //strings inside chat window


    // Start is called before the first frame update
    void Start()
    { 
        contentText = contentTransform.GetComponent<Text>();
        contentTextGenerator = new TextGenerator();
        contentTextGeneratorSettings = contentText.GetGenerationSettings(contentText.rectTransform.rect.size);
        oneLineHeigth = contentTextGenerator.GetPreferredHeight(" ", contentTextGeneratorSettings);

        chatView.verticalNormalizedPosition = 0.0f;
        contentWidth = contentTransform.sizeDelta.x;
       // contentTransform.sizeDelta = new Vector2(contentWidth, GetComponent<RectTransform>().sizeDelta.y - inputField.GetComponent<RectTransform>().sizeDelta.y);
        emptyContentSize = contentTransform.sizeDelta.y;
        chatView.verticalScrollbar.size = 1.0f;
        chanContentLinesCount = (int)(emptyContentSize / oneLineHeigth);

        chatItems = new List<string>();
    }

    //--------------------------------------------------------------------
    //-----------------------Event callbacks------------------------------

    public void ScrollButtonUp()
    {
        if (chatItems.Count > chanContentLinesCount)
        {
            chatView.verticalNormalizedPosition += 1.0f / (chatItems.Count - chanContentLinesCount);
        }
    }

    public void ScrollButtonDown()
    {
        if (chatItems.Count > chanContentLinesCount)
        {
            chatView.verticalNormalizedPosition -= 1.0f / (chatItems.Count - chanContentLinesCount);
        }
    }

    //------------------------------------------------------------------
    //------------------Internal methods--------------------------------
    private string BuildContentString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < chatItems.Count; i++)
        {
            if (i < chatItems.Count - 1)
            {
                sb.AppendLine(chatItems[i]);
            }
            else
            {
                sb.Append(chatItems[i]);
            }
        }

        return sb.ToString();
    }

    private void AddNewMessage(string message)
    {
        chatItems.Add(message);
        if (chatItems.Count > maxMessagesCount)
        {
            chatItems.RemoveAt(0);
        }
    }

    private void AddMessageToChat(string message)
    {
        float scrollPosition = chatView.verticalNormalizedPosition;
        AddNewMessage(message);
        contentText.text = BuildContentString();
        contentTransform.sizeDelta = new Vector2(contentWidth, Mathf.Max(contentText.preferredHeight, emptyContentSize));
        chatView.verticalNormalizedPosition = scrollPosition;
    }
}


