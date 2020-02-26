using MLAPI.NetworkedVar.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MyChatNetworkBehaviour
{
    public static ChatController Instance { get { return _instance; } }
    static ChatController _instance;
    public InputField inputField;
    [SerializeField]
    int maxInputLength;

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            ActiveButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }

    public void ActiveButton()
    {
        SendOutMessage($"{PlayerProfile.Instance.Gamer_Profile.GamerTag}||GAMERTAG||{inputField.text}");
    }
    #region MLAPI FUNCTIONS
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
}

