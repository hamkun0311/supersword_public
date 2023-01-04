using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class OptionMenu_UI : MonoBehaviour
{

    public GameObject Panel_OptionMenu;
    public GameObject Panel_InvMenu;
    public GameObject Panel_Character;
    public GameObject Panel_Chest;
    public GameObject Panel_Inventory;
    public GameObject Panel_StoreMenu;
    public GameObject Panel_Ranking;
    public GameObject Panel_Store;
    public GameObject Panel_StoreGold;
    public GameObject Panel_Help;
    public GameObject Panel_Tutorial1;
    public GameObject Panel_Tutorial2;
    public GameObject Panel_Tutorial3;
    

    public bool optionmenu_chk = false;

    // Panel_Option
    public GameObject Panel_Option;
    
    public SoundManager SM;

    public float bgm_value;
    public float effect_value;
    public Slider slider_bgm;
    public Slider slider_effect;

    //Panel_Creator
    public GameObject Panel_Creator;

    


    // Start is called before the first frame update
    void Start()
    {
        Panel_OptionMenu.SetActive(false);
        Panel_InvMenu.SetActive(false);
        Panel_Character.SetActive(false);
        Panel_Chest.SetActive(false);
        Panel_Inventory.SetActive(false);
        Panel_StoreMenu.SetActive(false);
        Panel_Store.SetActive(false);
        Panel_StoreGold.SetActive(false);
        Panel_Ranking.SetActive(false);
        Panel_Option.SetActive(false);
        Panel_Creator.SetActive(false);
        Panel_Help.SetActive(false);
        Panel_Tutorial1.SetActive(false);
        Panel_Tutorial2.SetActive(false);
        Panel_Tutorial3.SetActive(false);

        getVolumn();
        inistializeVolumn();

    }

    // Update is called once per frame
    void Update()
    {
        setVolumn();
    }

    public void onClickOptionMenuBtn()
    {
        SM.PlaySE("button");
        Panel_OptionMenu.SetActive(true);
        Panel_InvMenu.SetActive(false);
        Panel_Character.SetActive(false);
        Panel_Chest.SetActive(false);
        Panel_Inventory.SetActive(false);
        Panel_StoreMenu.SetActive(false);
        Panel_Store.SetActive(false);
        Panel_StoreGold.SetActive(false);
        Panel_Ranking.SetActive(false);
        Panel_Option.SetActive(false);
    }

    public void onClickOptionMenuCancel()
    {
        SM.PlaySE("button");
        Panel_OptionMenu.SetActive(false);
    }

    public void onClickGameEnd()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    //Panel_Option 
    public void onClickPanel_OptionBtn()
    {
        SM.PlaySE("button");
        Panel_Option.SetActive(true);
    }

    public void onClickPanel_OptionCloseBtn()
    {
        SM.PlaySE("button");
        
        PlayerPrefs.SetFloat("BGM", bgm_value);
        PlayerPrefs.SetFloat("Effect", effect_value);

        SM.background.volume = bgm_value;
        
        for(int i = 0; i < SM.sfxPlayer.Length; i++)
        {
            SM.sfxPlayer[i].volume = effect_value;
        }

        Panel_Option.SetActive(false);
    }

    public void inistializeVolumn()
    {
        slider_bgm.value = bgm_value;
        slider_effect.value = effect_value;
    }

    public void setVolumn()
    {
        bgm_value = slider_bgm.value;
        effect_value = slider_effect.value;
    }

    public void getVolumn()
    {
        bgm_value = PlayerPrefs.GetFloat("BGM");
        effect_value = PlayerPrefs.GetFloat("Effect");

        

    }

    //Panel_Creator
    public void onClickPanel_CreatorBtn()
    {
        SM.PlaySE("button");
        Panel_Creator.SetActive(true);
    }
    public void onClickPanel_CreatorCloseBtn()
    {
        SM.PlaySE("button");
        Panel_Creator.SetActive(false);
    }


    //Tutorial
    public void onClickHelpBtn()
    {
        SM.PlaySE("button");
        Panel_Help.SetActive(true);
        Panel_Tutorial1.SetActive(true);
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
        Panel_Help.SetActive(false);
        Panel_Tutorial1.SetActive(false);
        Panel_Tutorial2.SetActive(false);
        Panel_Tutorial3.SetActive(false);
    }

}
