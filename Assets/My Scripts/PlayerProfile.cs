using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GamerProfile
{
    public string GamerTag = "Needs New Name";
}

public class PlayerProfile : MonoBehaviour
{
    private static PlayerProfile _instance;
    public static PlayerProfile Instance { get { return _instance; } }
    public GamerProfile Gamer_Profile;

    [SerializeField]
    string fileLocation;
    private void OnValidate()
    {
        fileLocation = $"{Application.persistentDataPath}/PlayerProfile/";
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
        if (!Application.isEditor)
        {
            Gamer_Profile = Loadprofile();
        }
        else
        {
            Gamer_Profile = new GamerProfile();
            Gamer_Profile.GamerTag = "Unity Editor";
        }
    }

    GamerProfile SetData(string gamerTag)
    {
        GamerProfile myObject = new GamerProfile();
        myObject.GamerTag = gamerTag;
        return myObject;
    }
    public GamerProfile Loadprofile()
    {
        if (Directory.Exists($"{Application.persistentDataPath}/PlayerProfile/"))
        {
            if (File.Exists($"{Application.persistentDataPath}/PlayerProfile/playerprofile.json"))
            {
                string inputString = File.ReadAllText($"{Application.persistentDataPath}/PlayerProfile/playerprofile.json");
                return JsonUtility.FromJson<GamerProfile>(inputString);
            }
        }
        Directory.CreateDirectory($"{Application.persistentDataPath}/PlayerProfile/");
        return new GamerProfile();
    }
    public void SaveProfile(string gamerTag)
    {
        GamerProfile td = SetData(gamerTag);

        var outputString = JsonUtility.ToJson(td);
        if (!Directory.Exists($"{Application.persistentDataPath}/PlayerProfile/"))
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}/PlayerProfile/");
        }
        File.WriteAllText($"{Application.persistentDataPath}/PlayerProfile/playerprofile.json", outputString);
    }
}
