using MLAPI;
using MLAPI.Connection;
using MLAPI.NetworkedVar.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public class ChatController : NetworkedBehaviour
{
    public static ChatController Instance { get { return _instance; } }
    static ChatController _instance;
    private void OnGUI()
    {
        if (GUILayout.Button("Send Hello"))
        {
            SendOutMessage($"{PlayerProfile.Instance.Gamer_Profile.GamerTag}||GAMERTAG||<color=black>Hello World</color>");
        }   
        
        if (GUILayout.Button("Send Hello From Input"))
        {
            SendOutMessage($"{PlayerProfile.Instance.Gamer_Profile.GamerTag}||GAMERTAG||<color=black> {inputField.text}</color>");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            PressActiveButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        { 
        }
    }

    [SerializeField]
    InputField inputField;
    [SerializeField]
    int maxInputLength;

    #region MLAPI FUNCTIONS
    private List<string> chatItems;
    NetworkedList<string> ChatMessages = new NetworkedList<string>(new MLAPI.NetworkedVar.NetworkedVarSettings()
    {
        ReadPermission = MLAPI.NetworkedVar.NetworkedVarPermission.Everyone,
        WritePermission = MLAPI.NetworkedVar.NetworkedVarPermission.Everyone,
        SendTickrate = 5
    }, new List<string>());
    private void StartOnChangeListeners()
    {
        ChatMessages.OnListChanged += ((message) =>
        {
            AddMessage($"{ChatMessages[ChatMessages.Count - 1]}");
        });
    }
    void AddMessage(string msg)
    {
        string[] tempArr = MyStringExtenstions.SplitStringWithString(msg, "||GAMERTAG||");
        string sender = tempArr[0];
        string message = tempArr[1];
        AddMessageToChat($" <color=#0000ffff>{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}</color> <color=#ff0000ff>[{sender}]</color> = {message}");
    }
    public void SendOutMessage(string message)
    {
        if (!string.IsNullOrEmpty(message) && message.Length < maxInputLength)
        {
            ChatMessages.Add(message);
        }
    } 
    public void SendOutMessageUI(string message)
    {
        if (!string.IsNullOrEmpty(message) && message.Length < maxInputLength)
        {
            ChatMessages.Add(message);
        }
    }

  
    #endregion


    //display chat parameters
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

    // Start is called before the first frame update
    void Start()
    {
        StartOnChangeListeners();
        inputField.characterLimit = maxInputLength;
        contentText = contentTransform.GetComponent<Text>();
        contentTextGenerator = new TextGenerator();
        contentTextGeneratorSettings = contentText.GetGenerationSettings(contentText.rectTransform.rect.size);
        oneLineHeigth = contentTextGenerator.GetPreferredHeight(" ", contentTextGeneratorSettings);

        chatView.verticalNormalizedPosition = 0.0f;
        contentWidth = contentTransform.sizeDelta.x;
        contentTransform.sizeDelta = new Vector2(contentWidth, GetComponent<RectTransform>().sizeDelta.y - inputField.GetComponent<RectTransform>().sizeDelta.y);
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

    public void PressActiveButton()
    {
        SendOutMessage($"{PlayerProfile.Instance.Gamer_Profile.GamerTag}||GAMERTAG||{inputField.text}");
    }

  

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

