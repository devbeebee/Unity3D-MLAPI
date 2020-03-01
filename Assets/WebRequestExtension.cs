using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequestExtension
{
    public static IEnumerator Request(MonoBehaviour owner, string url, string errorString)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        CoroutineWithData cd = new CoroutineWithData(owner, WWw_ErrorCheck(www));
        yield return cd.coroutine;
        string result = "" + cd.result;
        if (result == "true")
        {
            yield return www.downloadHandler.text;
        }
        else
        {
            yield return $"Error Downloading {errorString}";
        }
    }
    private static IEnumerator WWw_ErrorCheck(UnityWebRequest www)
    {
        if (String.IsNullOrEmpty(www.error) )
        {
            yield return "true";
        }
        else
        {
            yield return "false";
        }
    }
}
public class CoroutineWithData
{
    //https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;
    public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
    }
}