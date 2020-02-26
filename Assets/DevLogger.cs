using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DevLogger : MonoBehaviour
{
    public static DevLogger Instance { get { return _instance; } }
    static DevLogger _instance;
    private List<string> devLogs;
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
    void Start()
    {
        contentText = contentTransform.GetComponent<Text>();
        contentTextGenerator = new TextGenerator();
        contentTextGeneratorSettings = contentText.GetGenerationSettings(contentText.rectTransform.rect.size);
        oneLineHeigth = contentTextGenerator.GetPreferredHeight(" ", contentTextGeneratorSettings);

        chatView.verticalNormalizedPosition = 0.0f;
        contentWidth = contentTransform.sizeDelta.x;
        emptyContentSize = contentTransform.sizeDelta.y;
        chatView.verticalScrollbar.size = 1.0f;
        chanContentLinesCount = (int)(emptyContentSize / oneLineHeigth);

        devLogs = new List<string>();
    }
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
    public void ScrollButtonUp()
    {
        if (devLogs.Count > chanContentLinesCount)
        {
            chatView.verticalNormalizedPosition += 1.0f / (devLogs.Count - chanContentLinesCount);
        }
    }

    public void ScrollButtonDown()
    {
        if (devLogs.Count > chanContentLinesCount)
        {
            chatView.verticalNormalizedPosition -= 1.0f / (devLogs.Count - chanContentLinesCount);
        }
    }
    private string BuildContentString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < devLogs.Count; i++)
        {
            if (i < devLogs.Count - 1)
            {
                sb.AppendLine(devLogs[i]);
            }
            else
            {
                sb.Append(devLogs[i]);
            }
        }

        return sb.ToString();
    }

    private void AddNewMessage(string message)
    {
        devLogs.Add(message);
        if (devLogs.Count > maxMessagesCount)
        {
            devLogs.RemoveAt(0);
        }
    }
    public void LogMssage(string message)
    {
        AddMessageToLog(message);
    }
    private void AddMessageToLog(string message)
    {
        float scrollPosition = chatView.verticalNormalizedPosition;
        AddNewMessage($"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} <color=black>{message}</color>");
        contentText.text = BuildContentString();
        contentTransform.sizeDelta = new Vector2(contentWidth, Mathf.Max(contentText.preferredHeight, emptyContentSize));
        chatView.verticalNormalizedPosition = scrollPosition;
    }
}
