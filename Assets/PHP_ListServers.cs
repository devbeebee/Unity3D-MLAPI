using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class serverphpclass
{
    public string serverID = "";
    public string serverName = "";
    public string serverIP = "";
    public string serverPassword = "";
    public string serverLastPing = "";
}
public class PHP_ListServers : MonoBehaviour
{
    [SerializeField]
    Slider refreshPage;
    [SerializeField]
    TextMeshProUGUI displayText;
    [SerializeField]
    private string URL = "http://localhost/mydb/servers.php";
    [SerializeField]
    string propertyDiv = "<div class=''>";
    [SerializeField]
    string propertyDivEnd = "</div>";
   
    List<string> propertyFields = new List<string>();
    [SerializeField]
    List<string> fields = new List<string>();
    [SerializeField]
    List<serverphpclass> phpfields = new List<serverphpclass>();
    [SerializeField]
    Transform UiParent;
    [SerializeField]
    GameObject UIelement;
    void Start()
    {
        StartCoroutine(LoginEnumerator());
        StartCoroutine(ServerListRepeat());
        refreshPage.maxValue = 30;
        refreshPage.value = 0;
    }
    IEnumerator LoginEnumerator()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                ListProperty(www.downloadHandler.text);
            }
        }
    }
    IEnumerator ServerListRepeat()
    {
        while (true)
        {
             for (int i = 0; i < 30; i++)
            {
                yield return new WaitForSeconds(1);
                refreshPage.value = i;
            }
            yield return LoginEnumerator();
        }
    }
        void ListProperty(string pageToEdit)
    {
        foreach (Transform item in UiParent)
        {
            Destroy(item.gameObject);
        }
        List<string> Extract = ExtractFromBody(pageToEdit, propertyDiv, propertyDivEnd);
        string st = "";
        foreach (var item in Extract)
        {
            st += "\n Nxt Item :" + item;
        }
        Extract = ExtractFromBody(st, "{", "}");
        st = "";
        int x = 0;
        Extract.Reverse();
        foreach (var item in Extract)
        {
            fields = ExtractFromBody(item, "[", "]");
            serverphpclass php = new serverphpclass();

            if (x == 0)
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    propertyFields.Add(fields[i]);

                    displayText.text += fields[i];
                }
               

            }
            else
            {
                // Recived data looks like this {[id][server_name][server_ip][server_password][server_lastping]} see http://localhost/mydb/gameserver.php
                php.serverID = fields[0];
                php.serverName = fields[1];
                php.serverIP = fields[2];
                php.serverPassword = fields[3];
                php.serverLastPing = fields[4];

            }
            if (x != 0)
            {
                phpfields.Add(php);
            }
            GameObject go = Instantiate(UIelement, UiParent);

            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(fields[0]);
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(fields[1]);
            go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(fields[2]);
            go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(fields[3]);
            go.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(fields[4]);
            x++;
        }

    }

    private static List<string> ExtractFromBody(string body, string start, string end)
    {
        List<string> matched = new List<string>();

        int indexStart = 0;
        int indexEnd = 0;

        bool exit = false;
        while (!exit)
        {
            indexStart = body.IndexOf(start);

            if (indexStart != -1)
            {
                indexEnd = indexStart + body.Substring(indexStart).IndexOf(end);

                matched.Add(body.Substring(indexStart + start.Length, indexEnd - indexStart - start.Length));

                body = body.Substring(indexEnd + end.Length);
            }
            else
            {
                exit = true;
            }
        }

        return matched;
    }
}
