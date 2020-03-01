using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class PHP_GetHighScores : MonoBehaviour
{
    public static PHP_GetHighScores Instance { get { return _instance; } }
    static PHP_GetHighScores _instance;
    [SerializeField]
    TextMeshProUGUI displayText;
    public string highscoreURL = "http://localhost/mydb/display.php";

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
        StartCoroutine(GetScores());
    }
    public IEnumerator GetScores()
    {
        CoroutineWithData cd = new CoroutineWithData(this, WebRequestExtension.Request(this, highscoreURL,"HighScores"));
        yield return cd.coroutine;
        displayText.SetText("" + cd.result);
    }
}