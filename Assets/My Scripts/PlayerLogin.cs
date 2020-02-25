using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLogin : MonoBehaviour
{



    private const string SceneName = "MultiplayerTestScene";

    // Start is called before the first frame update
    [SerializeField]
    GameObject LoadingScene;  
    [SerializeField]
    GameObject NoProfile;
    [SerializeField]
    GameObject HasProfile;
    [SerializeField]
    TMP_InputField gamerTagField;
    [SerializeField]
    Button LoginBtn;  
    [SerializeField]
    Button CancelBtn;
    [SerializeField]
    TextMeshProUGUI gamerTagPopulateField;
    [SerializeField]
    TextMeshProUGUI errorMessage;  
    [SerializeField]
    TextMeshProUGUI cancelTimer;
    [SerializeField]
    Color Error = Color.red;
    [SerializeField]
    Color Sucsess = Color.green;
    [SerializeField]
    [Range(3, 60)]
    int gamertagMaxLength = 6;
    int gamertagMinLength = 3;
    void Start()
    {
       Login();
    }

    private void Login()
    {
        if(PlayerProfile.Instance.Gamer_Profile.GamerTag == "Needs New Name")
        {
            NoProfile.SetActive(true);
            HasProfile.SetActive(false);
        }
        else
        {        
            gamerTagPopulateField.SetText($"GamerTag : {PlayerProfile.Instance.Gamer_Profile.GamerTag}");
            StartCoroutine(LoadScene());
        }
    }
    public void CheckCharLength()
    {
        if (gamerTagField.text.Length < gamertagMinLength)
        {
            errorMessage.color = Error;
            LoginBtn.interactable = false;
            errorMessage.SetText($"Minimum Name Length {gamerTagField.text.Length} / {gamertagMinLength}");
        }
        else if (gamerTagField.text.Length > gamertagMaxLength)
        {
            errorMessage.color = Error;
            LoginBtn.interactable = false;
            errorMessage.SetText($"Maximum Name Length {gamerTagField.text.Length} / {gamertagMaxLength}");
        }
        else
        {
            errorMessage.color = Sucsess;
            errorMessage.SetText($"Profile Ready To be Made {gamerTagField.text.Length} / {gamertagMaxLength}");
            LoginBtn.interactable = true;
        }
    }
    // Update is called once per frame
    public void SaveProfile()
    {
        PlayerProfile.Instance.SaveProfile(gamerTagField.text);
        PlayerProfile.Instance.Loadprofile();
        Login();
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        NoProfile.SetActive(false);
        HasProfile.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            cancelTimer.SetText($"Cancel Loading {i} / 5");
               yield return new WaitForSeconds(1); 
        }
        CancelBtn.interactable = false;
        cancelTimer.SetText($"Loading Scene");
        Slider slider = LoadingScene.GetComponent<Slider>();
        slider.maxValue = 100;
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        while (!asyncOperation.isDone)
        {
            slider.value = (asyncOperation.progress * 100);
            if (asyncOperation.progress >= 0.9f)
            {
                slider.value = slider.maxValue;
                yield return new WaitForSeconds(1);
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
