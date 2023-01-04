using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RankUI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Panel_Chest;
    public GameObject Panel_Inventory;
    public GameObject Panel_InvMenu;
    public GameObject Panel_Store;
    public GameObject Panel_LeaderBoard;
    public GameObject Panel_Loading;
    public GameObject Panel_OptionMenu;
    public GameObject Panel_StoreMenu;

    public Transform txtHolder1;
    public Transform txtHolder2;
    public Text[] txt_TotalRank;
    public Text[] txt_MyRank;
    public Text txt_Loading;

    public string User_ID;

    public InventoryUI iui;
    public Store_UI sui;

    public bool rank_state = false;

    void Start()
    {
        Panel_Chest.SetActive(false);
        Panel_Inventory.SetActive(false);
        Panel_Store.SetActive(false);
        Panel_LeaderBoard.SetActive(false);
        Panel_Loading.SetActive(false);
        Panel_OptionMenu.SetActive(false);
        Panel_StoreMenu.SetActive(false);
        Panel_InvMenu.SetActive(false);


        User_ID = LoginMenu.User_ID;

        txt_TotalRank = txtHolder1.GetComponentsInChildren<Text>();
        txt_MyRank = txtHolder2.GetComponentsInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickRankBtn()
    {

        SoundManager.instance.PlaySE("button");

        rank_state = !rank_state;
        Panel_LeaderBoard.SetActive(rank_state);

        if(rank_state == true)
        {
            txt_Loading.text = "불러오는 중..";
            Panel_Loading.SetActive(true);
            getTopUserRank();
        } 

        Panel_Chest.SetActive(false);
        Panel_Inventory.SetActive(false);
        Panel_Store.SetActive(false);
        Panel_OptionMenu.SetActive(false);

        Panel_StoreMenu.SetActive(false);
        Panel_InvMenu.SetActive(false);
        iui.invMenu_chk = false;
        sui.Store_state = false;
    }

    public void getTopUserRank()
    {

        var request = new GetLeaderboardRequest{ StartPosition = 0, StatisticName = "HighScore", MaxResultsCount = 10};
        PlayFabClientAPI.GetLeaderboard(request,
            (result) => {
                    
                    List<string> rank_PlayFabId = new List<string>();
                    List<int> rank_postition = new List<int>();
                    List<int> rank_StatValue = new List<int>();
                    List<string> rank_DisplayName = new List<string>();

                    for(int i = 0; i < result.Leaderboard.Count; i++)
                    {
                        rank_postition.Add(result.Leaderboard[i].Position + 1);
                        rank_PlayFabId.Add(result.Leaderboard[i].PlayFabId);
                        rank_DisplayName.Add(result.Leaderboard[i].DisplayName);
                        rank_StatValue.Add(result.Leaderboard[i].StatValue);

                        if(rank_DisplayName[i] == null)
                        {
                            rank_DisplayName[i] = rank_PlayFabId[i];
                        }

                        txt_TotalRank[i].text = rank_postition[i].ToString() + ". " + rank_DisplayName[i] + " / " + rank_StatValue[i].ToString();
                        if(rank_PlayFabId[i] == User_ID)
                        {
                            txt_TotalRank[i].color = UnityEngine.Color.green;
                        } else
                        {
                            txt_TotalRank[i].color = UnityEngine.Color.black;
                        }

                    }

                    getAroundUserRank();
            },
            (error) => 
            {
                txt_Loading.text = "불러오기 실패!";
                Panel_Loading.SetActive(false);
            }

        
        );

    }

    public void getAroundUserRank()
    {
        var request = new GetLeaderboardAroundPlayerRequest(){PlayFabId = User_ID, StatisticName = "HighScore", MaxResultsCount = 10};

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request,
            (result) =>
            {
                    List<string> rank_PlayFabId = new List<string>();
                    List<int> rank_postition = new List<int>();
                    List<int> rank_StatValue = new List<int>();
                    List<string> rank_DisplayName = new List<string>();

                    for(int i = 0; i < result.Leaderboard.Count; i++)
                    {
                        rank_postition.Add(result.Leaderboard[i].Position + 1);
                        rank_PlayFabId.Add(result.Leaderboard[i].PlayFabId);
                        rank_DisplayName.Add(result.Leaderboard[i].DisplayName);
                        rank_StatValue.Add(result.Leaderboard[i].StatValue);

                        if(rank_DisplayName[i] == null)
                        {
                            rank_DisplayName[i] = rank_PlayFabId[i];
                        }

                        txt_MyRank[i].text = rank_postition[i].ToString() + ". " + rank_DisplayName[i] + " / " + rank_StatValue[i].ToString();

                        if(rank_PlayFabId[i] == User_ID)
                        {
                            txt_MyRank[i].color = UnityEngine.Color.green;
                        }
                        else
                        {
                            txt_MyRank[i].color = UnityEngine.Color.black;
                        }

                    }

                    Panel_Loading.SetActive(false);

            } ,
            (error) => {
                txt_Loading.text = "불러오기 실패!";
                Panel_Loading.SetActive(false);
            }
        );   

    }

    public void onClickBtn_Confirm()
    {
        SoundManager.instance.PlaySE("button");
        
        Panel_LeaderBoard.SetActive(false);
    }


}
