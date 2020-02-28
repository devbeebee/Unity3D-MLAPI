using UnityEngine;
using System;
using System.Data;
using System.Text;

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    private string url = "http://localhost/mydb";

    /*/ public Text loginNick, loginPass, loginInfo, registerPass, registerPass2, registerNick, registerInfo;
    /*/
    [SerializeField]
    private List<int> _pingTime = new List<int>();

    void Start()
    {
        this.StartCoroutine(PingUpdate());
    }

    IEnumerator PingUpdate()
    {
    RestartLoop:
        var ping = new Ping("https://www.google.com/");

        yield return new WaitForSeconds(1f);
        while (!ping.isDone) yield return null;

        Debug.Log(ping.time);
        _pingTime.Add(ping.time);

        goto RestartLoop;
    }
    /*/
    public void Login()
    {
        if (!string.IsNullOrEmpty(loginNick.text) && !string.IsNullOrEmpty(loginPass.text))
        {
            StartCoroutine(LoginSystem());
        }
        else
        {
            loginInfo.text = "Prosze podać wszystkie dane";
        }
    }

    IEnumerator LoginSystem()
    {
        yield return new WaitForEndOfFrame();
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("nick", loginNick.text);
        wwwForm.AddField("pass", loginPass.text);
        WWW www = new WWW(url + "/login.php", wwwForm);
        yield return www;

        loginInfo.text = www.text;
        if (loginInfo.text == "Zalogowano pomyślnie")
        {
            SceneManager.LoadScene("Game");
        }

    }

    public void Register()
    {
        if (!string.IsNullOrEmpty(registerNick.text) && !string.IsNullOrEmpty(registerPass.text) && !string.IsNullOrEmpty(registerPass2.text))
        {
            if (registerPass.text == registerPass2.text)
            {
                StartCoroutine(RegisterSystem());
            }
            else
                registerInfo.text = "Podane hasła są różne";

        }
        else
        {
            registerInfo.text = "Prosze podać wszystkie dane";
        }
    }
    IEnumerator RegisterSystem()
    {
        yield return new WaitForEndOfFrame();
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("nick", registerNick.text);
        wwwForm.AddField("pass", registerPass.text);
        WWW www = new WWW(url + "/register.php", wwwForm);
        yield return www;

        registerInfo.text = www.text;


    }
    /*/
}