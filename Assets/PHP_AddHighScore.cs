using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class PHP_AddHighScore : MonoBehaviour
{
    public static PHP_AddHighScore Instance { get { return _instance; } }
    static PHP_AddHighScore _instance;

    public string addScoreURL = "http://localhost/mydb/addscore.php?"; //be sure to add a ? to your url
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
    public void AddRandomScore()
    {
        int s = Random.Range(1, 99999);
        string n = $"Unity";

        StartCoroutine(PostScores(n,s));
    }

    IEnumerator PostScores(string name, int score)
    {
        UnityWebRequest www = UnityWebRequest.Get(addScoreURL + $"name={name}&score={score}");
        yield return www.SendWebRequest();
        WWw_ErrorCheck(www);
        www.Dispose();
        yield return PHP_GetHighScores.Instance.GetScores();
    }

    private static void WWw_ErrorCheck(UnityWebRequest www)
    {
        if (www.error != null)
        {
            print(www.error);
        }
        else
        {
            print(www.downloadHandler.text);
        }
    }
}
