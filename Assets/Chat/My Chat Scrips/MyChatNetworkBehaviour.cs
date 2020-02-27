using MLAPI;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class MyChatNetworkBehaviour : NetworkedBehaviour
{
    public int maxMessagesCount; 
    [HideInInspector]
    public RectTransform contentTransform;
    [HideInInspector]
    public Text contentText;
    [HideInInspector]
    public ScrollRect chatView;
    [HideInInspector]
    public float emptyContentSize;
    [HideInInspector]
    public float contentWidth;
    [HideInInspector]
    public float oneLineHeigth;
    [HideInInspector] // the hight of one line in the conent text, we need it for initial spacings
    public int chanContentLinesCount;
    [HideInInspector]
    public TextGenerator contentTextGenerator;
    [HideInInspector]
    public TextGenerationSettings contentTextGeneratorSettings;
    public List<string> chatItems;

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

    public string BuildContentString()
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

    public void AddNewMessage(string message)
    {
        chatItems.Add(message);
        if (chatItems.Count > maxMessagesCount)
        {
            chatItems.RemoveAt(0);
        }
    }
    public void AddMessageToChat(string message)
    {
        float scrollPosition = chatView.verticalNormalizedPosition;
        AddNewMessage(message);
        contentText.text = BuildContentString();
        contentTransform.sizeDelta = new Vector2(contentWidth, Mathf.Max(contentText.preferredHeight, emptyContentSize));
        chatView.verticalNormalizedPosition = scrollPosition;
    }
}
