using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public GameObject Panel_InvMenu;
    public GameObject Panel_Character;
    public GameObject Panel_Chest;
    public GameObject Panel_Inventory;
    public GameObject Panel_Store;
    public GameObject Panel_Ranking;
    public GameObject Panel_Loading;
    public GameObject Panel_Confirm;

    public Text txt_status;
    public Text txt_NickNM;
    public Text txt_Confirm;
    public InputField input_NickName;

    public LobbyManager LM;

    public int getHP;
    public int getPower;
    public int setData;

    public string NickNM;


    public string User_ID;

    public bool charuiChk = false;
    
    // Start is called before the first frame update
    void Start()
    {

        Panel_Character.SetActive(false);
        Panel_Chest.SetActive(false);
        Panel_Inventory.SetActive(false);
        Panel_Store.SetActive(false);
        Panel_Ranking.SetActive(false);
        Panel_Confirm.SetActive(false);

        User_ID = LoginMenu.User_ID;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getPlayerStats()
    {

        Panel_Loading.SetActive(true);

        PlayFabClientAPI.GetUserData( new GetUserDataRequest() {PlayFabId = User_ID}
                        , (result) => {

                            getHP = int.Parse(result.Data["HEALTH"].Value);
                            getPower = int.Parse(result.Data["POWER"].Value);

                            txt_status.text = "체력 : " + result.Data["HEALTH"].Value 
                                     + "\n" + "파워 : " + result.Data["POWER"].Value;

                            getPlayerNickNM();

                        }
                        , (error) => print("error"));

    }

    public void getPlayerNickNM()
    {
        PlayFabClientAPI.GetPlayerProfile( new GetPlayerProfileRequest() {
            PlayFabId = User_ID,
            ProfileConstraints = new PlayerProfileViewConstraints() {ShowDisplayName = true}
        }, result => {
            txt_NickNM.text = result.PlayerProfile.DisplayName;
            NickNM = result.PlayerProfile.DisplayName;
            Panel_Loading.SetActive(false);
        },
        error => {
            print("failed status load");
            Debug.LogError(error.GenerateErrorReport());
        });

    }

    public void onClickChangeNickName()
    {

        SoundManager.instance.PlaySE("button");

        if(input_NickName.text.Length < 2)
        {
            charuiChk = false;
            txt_Confirm.text = "글자수를 확인하세요!";
            Panel_Confirm.SetActive(true);
            return;
        }

        charuiChk = true;

        
        Panel_Loading.SetActive(true);
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = input_NickName.text + "#" };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, 
        (result) => {
                        Panel_Loading.SetActive(false);
                        txt_Confirm.text = "닉네임 변경 완료!";
                        Panel_Confirm.SetActive(true);
                
        }, (error) => {
            print("failed nickname update");
        });
    }

    public void onClickPowerUP()
    {

        charuiChk = false;

        SoundManager.instance.PlaySE("button");
        if(LM.user_money < 1000)
        {
            txt_Confirm.text = "골드가 부족합니다!";
            Panel_Confirm.SetActive(true);
            return;
        }

        Panel_Loading.SetActive(true);

        System.Random randomObj = new System.Random();
        int rand_no = randomObj.Next();
        string PowerUP_data;

        if(rand_no % 2 == 1)
        {
            PowerUP_data = "HEALTH";
            setData = getHP + 100;

        } else {
            PowerUP_data = "POWER";
            setData = getPower + 100;
        }

        PlayFabClientAPI.UpdateUserData(new PlayFab.ClientModels.UpdateUserDataRequest() 
        {Data = new Dictionary<string, string>(){
                 {PowerUP_data, setData.ToString()}  
                }}
                            , (result) => {
                                    LM.user_money = LM.user_money - 1000;
                                    substracUserMoney(PowerUP_data);
                                    
                            }
                            , (error) => {
                                print("failed power up");

                            });

    }

    public void substracUserMoney(string PowerUP_data)
    {
        var request = new SubtractUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 1000 };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, 
                                (result) => {
                                    Panel_Loading.SetActive(false);
                                    if(PowerUP_data == "HEALTH")
                                    {
                                        txt_Confirm.text = "체력 강화 완료!";
                                        Panel_Confirm.SetActive(true);
                                    } else
                                    {
                                        txt_Confirm.text = "파워 강화 완료!";
                                        Panel_Confirm.SetActive(true);
                                    }
                                    
                                    getPlayerStats();      
                                }
                              , (error) => print("money subtrace fail"));

    }

    public void onClickCharacterBtn()
    {
        SoundManager.instance.PlaySE("button");
        getPlayerStats();
        Panel_Character.SetActive(true);
    }

    public void onClickConfirmBtn()
    {
        SoundManager.instance.PlaySE("button");

        if(charuiChk == true)
        {
            Panel_Confirm.SetActive(false);
            Panel_Character.SetActive(false);
        }else
        {
            Panel_Confirm.SetActive(false);
        }


    }


}
