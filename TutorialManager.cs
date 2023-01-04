using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public SoundManager SM;
    public GameObject Panel_Tutorial1;
    public GameObject Panel_Tutorial2;
    public GameObject Panel_Tutorial3;
    public GameObject Panel_RabbitLoading;
    
    //Panel_NickNM
    public GameObject Panel_NickNM;
    public InputField input_NickNM;


    //Panel_Loading
    public GameObject Panel_Loading;
    public Slider Loading_Bar;
    public Text txt_Loading;

    public Text txt_info;

    public string User_ID;


    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;

        User_ID = LoginMenu.User_ID;

        SM.background.volume = PlayerPrefs.GetFloat("BGM");

        for(int i = 0; i < SM.sfxPlayer.Length; i++)
        {
            SM.sfxPlayer[i].volume = PlayerPrefs.GetFloat("Effect");
        }

        Panel_Tutorial1.SetActive(false);
        Panel_Tutorial2.SetActive(false);
        Panel_Tutorial3.SetActive(false);
        Panel_RabbitLoading.SetActive(false);
        Panel_NickNM.SetActive(false);
        Panel_Loading.SetActive(true);
        
        txt_Loading.text = "0%";
        Loading_Bar.value = 0f;

        Invoke("Loading70",1f);
        Invoke("Loading100",2f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Loading70()
    {
        txt_Loading.text = "70%";
        Loading_Bar.value = 0.7f;
    }
    public void Loading100()
    {
        txt_Loading.text = "100%";
        Loading_Bar.value = 1f;
        Invoke("openPanel_NickNM",1);
    }
    public void openPanel_NickNM()
    {
        Panel_Loading.SetActive(false);
        Panel_NickNM.SetActive(true);
    }

    public void onClickNickNM_ConfirmBtn()
    {
        SM.PlaySE("button");
        Panel_RabbitLoading.SetActive(true);

        if(input_NickNM.text.Length < 2)
        {
            txt_info.text = "글자수를 확인하세요.";
            return;
        }

        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = input_NickNM.text + "#" };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, 
        (result) => {
                        Panel_RabbitLoading.SetActive(false);
                        Panel_NickNM.SetActive(false);
                        Panel_Tutorial1.SetActive(true);
        }, (error) => {
            print("failed nickname update");
        });
    }

    public void onClickTutorial1_nextBtn()
    {
        SM.PlaySE("button");
        Panel_Tutorial1.SetActive(false);
        Panel_Tutorial2.SetActive(true);
    }
    public void onClickTutorial2_prevBtn()
    {
        SM.PlaySE("button");
        Panel_Tutorial1.SetActive(true);
        Panel_Tutorial2.SetActive(false);
    }
    public void onClickTutorial2_nextBtn()
    {
        SM.PlaySE("button");
        Panel_Tutorial2.SetActive(false);
        Panel_Tutorial3.SetActive(true);
    }
    public void onClickTutorial3_prevBtn()
    {
        SM.PlaySE("button");
        Panel_Tutorial2.SetActive(true);
        Panel_Tutorial3.SetActive(false);
    }
    public void onClickTutorial3_nextBtn()
    {
        SM.PlaySE("button");
        SceneManager.LoadScene("LobbyScene");
    }
}
