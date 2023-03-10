using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ServerModels;
using PlayFab.ClientModels; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Chest_UI : MonoBehaviour
{

    public GameObject Panel_Chest;
    public GameObject Panel_ChestUse;
    public GameObject Panel_ChestUseList;
    public GameObject Panel_Loading;
    public GameObject Panel_ChestRstConfirm;

    public GameObject Panel_InvMenu;
    public GameObject Panel_Ranking;
    public GameObject Panel_Character;
    
    public Text txt_Loading;
    public Text txt_ChestRstConfirm;
    bool  Chest_state = false;

    public string User_ID;

    
    public List<string> chest_item_id = new List<string>();
    public List<string> chest_item_info = new List<string>();
    public List<string> chest_item_name = new List<string>();
    public List<uint> chest_item_value = new List<uint>();
    public List<int> chest_item_count = new List<int>();
    public List<string> chest_item_instance_id = new List<string>();

    public Text txt_money;
    public Text txt_ruby;
    public Text txt_ItemName;
    public Text txt_ItemInfo;

    public string select_chest_item_id;
    public string select_chest_item_name;
    public uint select_chest_item_value;
    public string select_chest_item_info;
    public int select_chest_item_count;
    public string select_chest_item_instance_id;

    public int user_money;
    public int user_ruby;

    public Chest_Slot[] chest_slot = new Chest_Slot[100];
    public Transform slotHolder;

    public InventoryUI iui;
    public Store_UI sui;

    public LobbyManager LM;

    public Image Img_GetItem;
    public Image Img_GetItemList1;
    public Image Img_GetItemList2;
    public Image Img_GetItemList3;
    public Image Img_GetItemList4;
    public Image Img_GetItemList5;
    public Image Img_GetItemList6;
    public Image Img_GetItemList7;
    public Image Img_GetItemList8;
    public Image Img_GetItemList9;
    public Image Img_GetItemList10;

    // Start is called before the first frame update
    void Start()
    {
        Chest_state = false;
        Panel_Chest.SetActive(Chest_state);
        Panel_ChestUse.SetActive(false);
        Panel_ChestUseList.SetActive(false);
        Panel_Loading.SetActive(false);
        Panel_ChestRstConfirm.SetActive(false);
        Panel_Ranking.SetActive(false);
        Panel_InvMenu.SetActive(false);
        Panel_Character.SetActive(false);

        User_ID = LoginMenu.User_ID;
        chest_slot = slotHolder.GetComponentsInChildren<Chest_Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickChest()
    {
        SoundManager.instance.PlaySE("button");
        Chest_state = !Chest_state;
        Panel_Chest.SetActive(Chest_state);


        if(Chest_state == true)
        {
            getChestInventory();
        } 

        iui.Inventory_Close();
        sui.StoreClose();
        Panel_Ranking.SetActive(false);

    }

    public void ChestClose()
    {
        Panel_Chest.SetActive(false);
        Chest_state = false;
    }

public void getChestInventory()
    {


        txt_Loading.text = "???????????? ??? ..";
        Panel_Loading.SetActive(true);

        chest_item_id.Clear();
        chest_item_name.Clear();
        chest_item_info.Clear();
        chest_item_value.Clear();
        chest_item_count.Clear();
        chest_item_instance_id.Clear();

        for(int i = 0; i < chest_slot.Length; i++)
        {
            chest_slot[i].BackImg.color = UnityEngine.Color.white;
        }

        deleteSlot();

        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(),
        (result) => 
        {
            
            user_money = result.VirtualCurrency["GD"];
            user_ruby = result.VirtualCurrency["RB"];
            txt_money.text = user_money.ToString();
            txt_ruby.text = user_ruby.ToString();
            LM.user_money = user_money;
            LM.user_ruby = user_ruby;

            foreach(var item in result.Inventory)
            {
                if (item.CatalogVersion == "CHEST" || item.CatalogVersion == "MONEY")
                {
                    chest_item_id.Add(item.ItemId);
                    chest_item_name.Add(item.DisplayName);
                    chest_item_value.Add(item.UnitPrice);
                    chest_item_count.Add(item.RemainingUses.Value);
                    chest_item_instance_id.Add(item.ItemInstanceId);
                }

            }

            for(int i = 0; i < chest_item_name.Count; i++)
            {
                chest_slot[i].item_id = chest_item_id[i];
                chest_slot[i].item_name = chest_item_name[i];
                chest_slot[i].item_value = chest_item_value[i];
                chest_slot[i].item_count = chest_item_count[i];
                chest_slot[i].item_instance_id = chest_item_instance_id[i];
                chest_slot[i].itemImage.sprite = Resources.Load<Sprite>("items/" + chest_item_name[i]);
                chest_slot[i].itemImage.preserveAspect = true;

                for(int j = 0; j < LM.chest_id.Count; j++)
                {
                    if(chest_slot[i].item_id == LM.chest_id[j])
                    {
                        chest_slot[i].item_info = LM.chest_info[j];
                    }
                }

                if(chest_slot[i].item_id == "money.chest.ruby100" ||chest_slot[i].item_id == "money.chest.ruby50")
                {
                    chest_slot[i].item_info = "?????? ??? ?????? ??????";
                }

            }

            Panel_ChestUseList.SetActive(false);
            Panel_ChestUse.SetActive(false);
            Panel_Loading.SetActive(false);
            Panel_ChestRstConfirm.SetActive(false);



        }, 
        (error) => {
            txt_ChestRstConfirm.text = "???????????? ??????!";
            Panel_ChestRstConfirm.SetActive(true);
        });

    }

    public void onClinkChestSlotItem(Chest_Slot input_slot)
    {

        SoundManager.instance.PlaySE("button");

        for(int i = 0; i < chest_slot.Length; i++)
        {
            chest_slot[i].BackImg.color = UnityEngine.Color.white;
        }
        
        input_slot.BackImg.color = UnityEngine.Color.yellow;

        select_chest_item_id = input_slot.item_id;
        select_chest_item_name = input_slot.item_name;
        select_chest_item_value = input_slot.item_value;
        select_chest_item_info = input_slot.item_info;
        select_chest_item_count = input_slot.item_count;
        select_chest_item_instance_id = input_slot.item_instance_id;
        txt_ItemName.text = select_chest_item_name;
        txt_ItemInfo.text = select_chest_item_info + "\n?????? : " + select_chest_item_count;

    }

    public void deleteSlot()
    {
        for(int i = 0; i < chest_slot.Length; i++)
        {
            chest_slot[i].item_id = null;
            chest_slot[i].item_info = null;
            chest_slot[i].item_name = null;
            chest_slot[i].item_value = 0;
            chest_slot[i].item_count = 0;
            chest_slot[i].item_instance_id = null;
            chest_slot[i].itemImage.sprite = null;
        }

    }

    public void onClickSellBtn()
    {
        SoundManager.instance.PlaySE("button");

        if(select_chest_item_id == "money.chest.ruby100" || select_chest_item_id == "money.chest.ruby50")
        {
            txt_ChestRstConfirm.text = "????????? ??? ????????????!";
            Panel_ChestRstConfirm.SetActive(true);
            return;
        }

        if(txt_ItemName.text == "")
        {
            txt_ChestRstConfirm.text = "????????? ???????????????!";
            Panel_ChestRstConfirm.SetActive(true);
            return;
        }

        txt_ItemName.text = "";
        txt_ItemInfo.text = "";

        txt_Loading.text = "?????? ?????????..";
        Panel_Loading.SetActive(true);

        sellChestItem(select_chest_item_instance_id);

    }

    public void onClickUseBtn()
    {
        System.Random randomObj = new System.Random(); // ???????????? obj

        if(txt_ItemName.text == "")
        {
            txt_ChestRstConfirm.text = "????????? ???????????????!";
            Panel_ChestRstConfirm.SetActive(true);
            return;
        }

        txt_Loading.text = "?????? ?????????..";
        Panel_Loading.SetActive(true);

        if(select_chest_item_id == "money.chest.ruby100" || select_chest_item_id == "money.chest.ruby50")
        {
            txt_Loading.text = "?????? ?????????..";
            Panel_Loading.SetActive(true);
            if(select_chest_item_id == "money.chest.ruby100")
            {
                grantUserRuby(select_chest_item_instance_id, 110);
            } else if(select_chest_item_id == "money.chest.ruby50")
            {
                grantUserRuby(select_chest_item_instance_id, 50);
            }
            return;
        }

        if(select_chest_item_id == "01") //????????????
        {
            int rand_chest = randomObj.Next(1,100);
            int rand_item = randomObj.Next();

            string getItemID;
            string getItemName;

            if(rand_chest <= 1) // S??? 1
            {
                if(rand_item % 7 == 0) 
                {
                    getItemID = "102";
                    getItemName = "????????????";
                }
                else if(rand_item % 7 == 1) 
                {
                    getItemID = "114";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 7 == 2) 
                {
                    getItemID = "121";
                    getItemName = "?????????????????????";
                }
                else if(rand_item % 7 == 3) 
                {
                    getItemID = "128";
                    getItemName = "???????????????";
                }
                else if(rand_item % 7 == 4)
                {
                    getItemID = "135";
                    getItemName = "?????????????????????";
                }
                else if(rand_item % 7 == 5)
                {
                    getItemID = "136";
                    getItemName = "?????????";
                }
                else 
                {
                    getItemID = "141";
                    getItemName = "????????????";
                }
            } 
            else if (rand_chest > 1 && rand_chest <= 6) // A??? 2,3,4,5,6
            {
                if(rand_item % 8 == 0) 
                {
                    getItemID = "103";
                    getItemName = "????????????";
                }
                else if(rand_item % 8 == 1) 
                {
                    getItemID = "113";
                    getItemName = "???????????????";
                }
                else if(rand_item % 8 == 2) 
                {
                    getItemID = "120";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 8 == 3) 
                {
                    getItemID = "127";
                    getItemName = "????????????";
                }
                else if(rand_item % 8 == 4)
                {
                    getItemID = "134";
                    getItemName = "??????????????????";
                } else if(rand_item % 8 == 5)
                {
                    getItemID = "105";
                    getItemName = "??????";
                } else if(rand_item % 8 == 6)
                {
                    getItemID = "137";
                    getItemName = "?????????";
                } else
                {
                    getItemID = "139";
                    getItemName = "????????????";
                }

            }
            else if (rand_chest > 6 && rand_chest <= 25 ) // B??? 7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25
            {
                if(rand_item % 18 == 0) 
                {
                    getItemID = "104";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 18 == 1) 
                {
                    getItemID = "106";
                    getItemName = "???????????????";
                }
                else if(rand_item % 18 == 2) 
                {
                    getItemID = "101";
                    getItemName = "????????????";
                }
                else if(rand_item % 18 == 3) 
                {
                    getItemID = "107";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 18 == 4) 
                {
                    getItemID = "110";
                    getItemName = "?????????";
                }
                else if(rand_item % 18 == 5) 
                {
                    getItemID = "111";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 18 == 6) 
                {
                    getItemID = "112";
                    getItemName = "???????????????";
                }
                else if(rand_item % 18 == 7) 
                {
                    getItemID = "117";
                    getItemName = "?????????";
                }
                else if(rand_item % 18 == 8) 
                {
                    getItemID = "118";
                    getItemName = "?????????????????????";
                }
                else if(rand_item % 18 == 9) 
                {
                    getItemID = "119";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 18 == 10) 
                {
                    getItemID = "124";
                    getItemName = "?????????";
                }
                else if(rand_item % 18 == 11) 
                {
                    getItemID = "125";
                    getItemName = "???????????????";
                }
                else if(rand_item % 18 == 12) 
                {
                    getItemID = "126";
                    getItemName = "????????????";
                }
                else if(rand_item % 18 == 13) 
                {
                    getItemID = "131";
                    getItemName = "????????????";
                }
                else if(rand_item % 18 == 14) 
                {
                    getItemID = "132";
                    getItemName = "?????????????????????";
                }
                else if(rand_item % 18 == 15)
                {
                    getItemID = "133";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 18 == 16)
                {
                    getItemID = "138";
                    getItemName = "?????????";
                }
                else 
                {
                    getItemID = "140";
                    getItemName = "????????????????????????";
                }
                
                
            }
            else if (rand_chest > 25 && rand_chest <= 65 ) // C??? 26,27,28
            {
                if(rand_item % 8 == 0) 
                {
                    getItemID = "108";
                    getItemName = "???????????????";
                }
                else if(rand_item % 8 == 1) 
                {
                    getItemID = "109";
                    getItemName = "???????????????";
                }
                else if(rand_item % 8 == 2) 
                {
                    getItemID = "115";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 8 == 3) 
                {
                    getItemID = "116";
                    getItemName = "??????????????????";
                }
                else if(rand_item % 8 == 4) 
                {
                    getItemID = "122";
                    getItemName = "????????????";
                }
                else if(rand_item % 8 == 5) 
                {
                    getItemID = "123";
                    getItemName = "????????????";
                }
                else if(rand_item % 8 == 6) 
                {
                    getItemID = "129";
                    getItemName = "??????????????????";
                }
                else 
                {
                    getItemID = "130";
                    getItemName = "??????????????????";
                }

            } else
            {
                getItemID = "899";
                getItemName = "???????????????";
            }

            grantNewItem(getItemID, getItemName);

        }
        else if (select_chest_item_id == "02") // ????????????x10
        {

            List<string> getItemID = new List<string>();
            List<string> getItemName = new List<string>();
            for(int i = 0; i < 10; i++)
            {
                
                int rand_chest = randomObj.Next(1,100);
                int rand_item = randomObj.Next();

                if(rand_chest <= 1) // S???
                {
                    if(rand_item % 7 == 0) 
                    {
                        getItemID.Add("102");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 7 == 1) 
                    {
                        getItemID.Add("114");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 7 == 2) 
                    {
                        getItemID.Add("121");
                        getItemName.Add("?????????????????????");
                    }
                    else if(rand_item % 7 == 3) 
                    {
                        getItemID.Add("128");
                        getItemName.Add("???????????????");
                    }
                    else if(rand_item % 7 == 4)
                    {
                        getItemID.Add("135");
                        getItemName.Add("?????????????????????");
                    }
                    else if(rand_item % 7 == 5)
                    {
                        getItemID.Add("136");
                        getItemName.Add("?????????");
                    }
                    else 
                    {
                        getItemID.Add("141");
                        getItemName.Add("????????????");
                    }
                } 
                else if (rand_chest > 1 && rand_chest <= 6) // A???
                {
                    if(rand_item % 8 == 0) 
                    {
                        getItemID.Add("103");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 8 == 1) 
                    {
                        getItemID.Add("113");
                        getItemName.Add("???????????????");
                    }
                    else if(rand_item % 8 == 2) 
                    {
                        getItemID.Add("120");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 8 == 3) 
                    {
                        getItemID.Add("127");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 8 == 4) 
                    {
                        getItemID.Add("134");
                        getItemName.Add("??????????????????");
                    } else if(rand_item % 8 == 5)
                    {
                        getItemID.Add("105");
                        getItemName.Add("??????");
                    } else if(rand_item % 8 == 6)
                    {
                        getItemID.Add("137");
                        getItemName.Add("?????????");
                    } else
                    {
                        getItemID.Add("139");
                        getItemName.Add("????????????");
                    }

                }
                else if (rand_chest > 6 && rand_chest <= 25 ) // B???
                {
                    if(rand_item % 18 == 0) 
                    {
                        getItemID.Add("104");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 18 == 1) 
                    {
                        getItemID.Add("106");
                        getItemName.Add("???????????????");
                    }
                    else if(rand_item % 18 == 2) 
                    {
                        getItemID.Add("101");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 18 == 3) 
                    {
                        getItemID.Add("107");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 18 == 4) 
                    {
                        getItemID.Add("110");
                        getItemName.Add("?????????");
                    }
                    else if(rand_item % 18 == 5) 
                    {
                        getItemID.Add("111");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 18 == 6) 
                    {
                        getItemID.Add("112");
                        getItemName.Add("???????????????");
                    }
                    else if(rand_item % 18 == 7) 
                    {
                        getItemID.Add("117");
                        getItemName.Add("?????????");
                    }
                    else if(rand_item % 18 == 8) 
                    {
                        getItemID.Add("118");
                        getItemName.Add("?????????????????????");
                    }
                    else if(rand_item % 18 == 9) 
                    {
                        getItemID.Add("119");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 18 == 10) 
                    {
                        getItemID.Add("124");
                        getItemName.Add("?????????");
                    }
                    else if(rand_item % 18 == 11) 
                    {
                        getItemID.Add("125");
                        getItemName.Add("???????????????");
                    }
                    else if(rand_item % 18 == 12) 
                    {
                        getItemID.Add("126");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 18 == 13) 
                    {
                        getItemID.Add("131");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 18 == 14) 
                    {
                        getItemID.Add("132");
                        getItemName.Add("?????????????????????");
                    }
                    else if(rand_item % 18 == 15)
                    {
                        getItemID.Add("133");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 18 == 16)
                    {
                        getItemID.Add("138");
                        getItemName.Add("?????????");
                    }
                    else 
                    {
                        getItemID.Add("140");
                        getItemName.Add("????????????????????????");
                    }
                    
                }
                else if (rand_chest > 25 && rand_chest <= 65 ) // c
                {
                    if(rand_item % 8 == 0) 
                    {
                        getItemID.Add("108");
                        getItemName.Add("???????????????");
                    }
                    else if(rand_item % 8 == 1) 
                    {
                        getItemID.Add("109");
                        getItemName.Add("???????????????");
                    }
                    else if(rand_item % 8 == 2) 
                    {
                        getItemID.Add("115");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 8 == 3) 
                    {
                        getItemID.Add("116");
                        getItemName.Add("??????????????????");
                    }
                    else if(rand_item % 8 == 4) 
                    {
                        getItemID.Add("122");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 8 == 5) 
                    {
                        getItemID.Add("123");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 8 == 6) 
                    {
                        getItemID.Add("129");
                        getItemName.Add("??????????????????");
                    }
                    else 
                    {
                        getItemID.Add("130");
                        getItemName.Add("??????????????????");
                    }

                }else
                {
                    getItemID.Add("899");
                    getItemName.Add("???????????????");
                }
                

            }
            
            grantNewItem_List(getItemID,getItemName);

        }
        else if (select_chest_item_id == "03") //?????????
        {
            
            int rand_chest = randomObj.Next(1,100);
            int rand_item = randomObj.Next();

            string getItemID;
            string getItemName;

            if(rand_chest <= 1) // S???
            {
                getItemID = "114";
                getItemName = "??????????????????";
            } 
            else if (rand_chest > 1 && rand_chest <= 6) // A???
            {
                getItemID = "113";
                getItemName = "???????????????";
            }
            else if (rand_chest > 6 && rand_chest <= 25 ) // B???
            {
                if(rand_item % 3 == 0) 
                {
                    getItemID = "110";
                    getItemName = "?????????";
                }
                else if(rand_item % 3 == 1) 
                {
                    getItemID = "111";
                    getItemName = "??????????????????";
                }
                else 
                {
                    getItemID = "112";
                    getItemName = "???????????????";
                }
            }
            else if (rand_chest > 25 && rand_chest <= 65 ) // c???
            {
                if(rand_item % 2 == 0) 
                {
                    getItemID = "108";
                    getItemName = "???????????????";
                }
                else 
                {
                    getItemID = "109";
                    getItemName = "???????????????";
                }
            }
            else
            {
                getItemID = "899";
                getItemName = "???????????????";
            }

            grantNewItem(getItemID, getItemName);
            
        }
        else if (select_chest_item_id == "04") // ????????????
        {
            
            int rand_chest = randomObj.Next(1,100);
            int rand_item = randomObj.Next();

            string getItemID;
            string getItemName;

            if(rand_chest <= 1) // S???
            {
                getItemID = "121";
                getItemName = "?????????????????????";
            } 
            else if (rand_chest > 1 && rand_chest <= 6) // A???
            {
                getItemID = "120";
                getItemName = "??????????????????";
            }
            else if (rand_chest > 6 && rand_chest <= 25 ) // B???
            {
                if(rand_item % 3 == 0) 
                {
                    getItemID = "117";
                    getItemName = "?????????";
                }
                else if(rand_item % 3 == 1) 
                {
                    getItemID = "118";
                    getItemName = "?????????????????????";
                }
                else 
                {
                    getItemID = "119";
                    getItemName = "??????????????????";
                }
            }
            else if (rand_chest > 25 && rand_chest <= 65 ) // B???
            {
                if(rand_item % 2 == 0) 
                {
                    getItemID = "115";
                    getItemName = "??????????????????";
                }
                else 
                {
                    getItemID = "116";
                    getItemName = "??????????????????";
                }
            } 
            else
            {
                getItemID = "899";
                getItemName = "???????????????";
            }
            
            grantNewItem(getItemID, getItemName);
            
        }
        else if (select_chest_item_id == "05") // ??????
        {
            int rand_chest = randomObj.Next(1,100);
            int rand_item = randomObj.Next();

            string getItemID;
            string getItemName;

            if(rand_chest <= 1) // S???
            {
                getItemID = "128";
                getItemName = "???????????????";
            } 
            else if (rand_chest > 1 && rand_chest <= 6) // A???
            {
                getItemID = "127";
                getItemName = "????????????";
            }
            else if (rand_chest > 6 && rand_chest <= 25 ) // B???
            {
                if(rand_item % 3 == 0) 
                {
                    getItemID = "124";
                    getItemName = "?????????";
                }
                else if(rand_item % 3 == 1) 
                {
                    getItemID = "125";
                    getItemName = "???????????????";
                }
                else 
                {
                    getItemID = "126";
                    getItemName = "????????????";
                }
            }
            else if (rand_chest > 25 && rand_chest <= 65 ) // c???
            {
                if(rand_item % 2 == 0) 
                {
                    getItemID = "122";
                    getItemName = "????????????";
                }
                else 
                {
                    getItemID = "123";
                    getItemName = "????????????";
                }
            }
            else
            {
                getItemID = "899";
                getItemName = "???????????????";
            }

            grantNewItem(getItemID, getItemName);
            
        }
        else if (select_chest_item_id == "06") //????????????
        {
            int rand_chest = randomObj.Next(1,100);
            int rand_item = randomObj.Next();

            string getItemID;
            string getItemName;

            if(rand_chest <= 1) // S???
            {
                getItemID = "135";
                getItemName = "?????????????????????";
            } 
            else if (rand_chest > 1 && rand_chest <= 6) // A???
            {
                getItemID = "134";
                getItemName = "??????????????????";
            }
            else if (rand_chest > 6 && rand_chest <= 25 ) // B???
            {
                if(rand_item % 3 == 0) 
                {
                    getItemID = "131";
                    getItemName = "????????????";
                }
                else if(rand_item % 3 == 1) 
                {
                    getItemID = "132";
                    getItemName = "?????????????????????";
                }
                else 
                {
                    getItemID = "133";
                    getItemName = "??????????????????";
                }
            }
            else if (rand_chest > 25 && rand_chest <= 65 ) // c???
            {
                if(rand_item % 2 == 0) 
                {
                    getItemID = "129";
                    getItemName = "??????????????????";
                }
                else 
                {
                    getItemID = "130";
                    getItemName = "??????????????????";
                }
            }
            else
            {
                getItemID = "899";
                getItemName = "???????????????";
            }

            grantNewItem(getItemID, getItemName);
            
            
        }
        else if (select_chest_item_id == "08") //????????? 10
        {

            List<string> getItemID = new List<string>();
            List<string> getItemName = new List<string>();

            for(int i = 0; i < 10; i++)
            {
                
                int rand_chest = randomObj.Next(1,100);
                int rand_item = randomObj.Next();

                if(rand_chest <= 1) // S???
                {
                    getItemID.Add("114");
                    getItemName.Add("??????????????????");
                } 
                else if (rand_chest > 1 && rand_chest <= 6) // A???
                {
                    getItemID.Add("113");
                    getItemName.Add("???????????????");
                }
                else if (rand_chest > 6 && rand_chest <= 25 ) // B???
                {
                    if(rand_item % 3 == 0) 
                    {
                        getItemID.Add("110");
                        getItemName.Add("?????????");
                    }
                    else if(rand_item % 16 == 1) 
                    {
                        getItemID.Add("111");
                        getItemName.Add("??????????????????");
                    }
                    else
                    {
                        getItemID.Add("112");
                        getItemName.Add("???????????????");
                    }
                    
                }
                else if (rand_chest > 25 && rand_chest <= 65 ) // B???
                {
                    if(rand_item % 2 == 0) 
                    {
                        getItemID.Add("108");
                        getItemName.Add("???????????????");
                    }
                    else 
                    {
                        getItemID.Add("109");
                        getItemName.Add("???????????????");
                    }
                }
                else
                {
                    getItemID.Add("899");
                    getItemName.Add("???????????????");
                }

            }
            grantNewItem_List(getItemID, getItemName);
            
        }
        else if (select_chest_item_id == "09") // ???????????? 10
        {
            List<string> getItemID = new List<string>();
            List<string> getItemName = new List<string>();

            for(int i = 0; i < 10; i++)
            {
                
                int rand_chest = randomObj.Next(1,100);
                int rand_item = randomObj.Next();

                if(rand_chest <= 1) // S???
                {
                    getItemID.Add("121");
                    getItemName.Add("?????????????????????");
                } 
                else if (rand_chest > 1 && rand_chest <= 6) // A???
                {
                    getItemID.Add("120");
                    getItemName.Add("??????????????????");
                }
                else if (rand_chest > 6 && rand_chest <= 25 ) // B???
                {
                    if(rand_item % 3 == 0) 
                    {
                        getItemID.Add("117");
                        getItemName.Add("?????????");
                    }
                    else if(rand_item % 16 == 1) 
                    {
                        getItemID.Add("118");
                        getItemName.Add("?????????????????????");
                    }
                    else
                    {
                        getItemID.Add("119");
                        getItemName.Add("??????????????????");
                    }
                    
                }
                else if (rand_chest > 25 && rand_chest <= 65 ) // c???
                {
                    if(rand_item % 2 == 0) 
                    {
                        getItemID.Add("115");
                        getItemName.Add("??????????????????");
                    }
                    else 
                    {
                        getItemID.Add("116");
                        getItemName.Add("??????????????????");
                    }
                }
                else
                {
                    getItemID.Add("899");
                    getItemName.Add("???????????????");   
                }

            }
            grantNewItem_List(getItemID, getItemName);
        }
        else if (select_chest_item_id == "10") // ?????? 10
        {
            
            List<string> getItemID = new List<string>();
            List<string> getItemName = new List<string>();

            for(int i = 0; i < 10; i++)
            {
                
                int rand_chest = randomObj.Next(1,100);
                int rand_item = randomObj.Next();

                if(rand_chest <= 1) // S???
                {
                    getItemID.Add("128");
                    getItemName.Add("???????????????");
                } 
                else if (rand_chest > 1 && rand_chest <= 6) // A???
                {
                    getItemID.Add("127");
                    getItemName.Add("????????????");
                }
                else if (rand_chest > 6 && rand_chest <= 25 ) // B???
                {
                    if(rand_item % 3 == 0) 
                    {
                        getItemID.Add("124");
                        getItemName.Add("?????????");
                    }
                    else if(rand_item % 16 == 1) 
                    {
                        getItemID.Add("125");
                        getItemName.Add("???????????????");
                    }
                    else
                    {
                        getItemID.Add("126");
                        getItemName.Add("????????????");
                    }
                    
                }
                else if (rand_chest > 25 && rand_chest <= 65 ) // c???
                {
                    if(rand_item % 2 == 0) 
                    {
                        getItemID.Add("122");
                        getItemName.Add("????????????");
                    }
                    else 
                    {
                        getItemID.Add("123");
                        getItemName.Add("????????????");
                    }
                }
                else
                {
                    getItemID.Add("899");
                    getItemName.Add("???????????????");      
                }

            }
            grantNewItem_List(getItemID, getItemName);

        }
        else if (select_chest_item_id == "11") //???????????? 10
        {
            List<string> getItemID = new List<string>();
            List<string> getItemName = new List<string>();

            for(int i = 0; i < 10; i++)
            {
                
                int rand_chest = randomObj.Next(1,100);
                int rand_item = randomObj.Next();

                if(rand_chest <= 1) // S???
                {
                    getItemID.Add("135");
                    getItemName.Add("?????????????????????");
                } 
                else if (rand_chest > 1 && rand_chest <= 6) // A???
                {
                    getItemID.Add("134");
                    getItemName.Add("??????????????????");
                }
                else if (rand_chest > 6 && rand_chest <= 25 ) // B???
                {
                    if(rand_item % 3 == 0) 
                    {
                        getItemID.Add("131");
                        getItemName.Add("????????????");
                    }
                    else if(rand_item % 16 == 1) 
                    {
                        getItemID.Add("132");
                        getItemName.Add("?????????????????????");
                    }
                    else
                    {
                        getItemID.Add("133");
                        getItemName.Add("??????????????????");
                    }
                    
                }
                else if (rand_chest > 25 && rand_chest <= 65 ) // c???
                {
                    if(rand_item % 2 == 0) 
                    {
                        getItemID.Add("129");
                        getItemName.Add("??????????????????");
                    }
                    else 
                    {
                        getItemID.Add("130");
                        getItemName.Add("??????????????????");
                    }
                }
                else
                {
                    getItemID.Add("899");
                    getItemName.Add("???????????????");      

                }

            }
            grantNewItem_List(getItemID, getItemName);

        }

        txt_ItemName.text = "";
        txt_ItemInfo.text = "";
    }

    public void grantNewItem(string getItemID, string GetItemName)
    {

        PlayFabServerAPI.GrantItemsToUser(new GrantItemsToUserRequest {
                CatalogVersion = "ITEM",PlayFabId = User_ID, ItemIds = new List<string> {getItemID}}
                                    , (result) => {
                                            Img_GetItem.sprite = null;
                                            Img_GetItem.sprite = Resources.Load<Sprite>("items/" + GetItemName);
                                            useChestItem(select_chest_item_instance_id);
                                    }
                                    , (error) => {
                                            txt_ChestRstConfirm.text = "?????? ?????? ??????!";
                                            Panel_ChestRstConfirm.SetActive(true);
                                    });



    }

    public void grantNewItem_List(List<string> getItemID, List<string> GetItemName)
    {
        
        PlayFabServerAPI.GrantItemsToUser(new GrantItemsToUserRequest {
                CatalogVersion = "ITEM",PlayFabId = User_ID, ItemIds = getItemID}
                                    , (result) => {
                                        Img_GetItemList1.sprite = null;    
                                        Img_GetItemList2.sprite = null;    
                                        Img_GetItemList3.sprite = null;    
                                        Img_GetItemList4.sprite = null;    
                                        Img_GetItemList5.sprite = null;    
                                        Img_GetItemList6.sprite = null;    
                                        Img_GetItemList7.sprite = null;    
                                        Img_GetItemList8.sprite = null;    
                                        Img_GetItemList9.sprite = null;    
                                        Img_GetItemList10.sprite = null;    

                                        Img_GetItemList1.sprite = Resources.Load<Sprite>("items/" + GetItemName[0]);
                                        Img_GetItemList2.sprite = Resources.Load<Sprite>("items/" + GetItemName[1]);
                                        Img_GetItemList3.sprite = Resources.Load<Sprite>("items/" + GetItemName[2]);
                                        Img_GetItemList4.sprite = Resources.Load<Sprite>("items/" + GetItemName[3]);
                                        Img_GetItemList5.sprite = Resources.Load<Sprite>("items/" + GetItemName[4]);
                                        Img_GetItemList6.sprite = Resources.Load<Sprite>("items/" + GetItemName[5]);
                                        Img_GetItemList7.sprite = Resources.Load<Sprite>("items/" + GetItemName[6]);
                                        Img_GetItemList8.sprite = Resources.Load<Sprite>("items/" + GetItemName[7]);
                                        Img_GetItemList9.sprite = Resources.Load<Sprite>("items/" + GetItemName[8]);
                                        Img_GetItemList10.sprite = Resources.Load<Sprite>("items/" + GetItemName[9]);

                                        useChestItem(select_chest_item_instance_id);

                                    }
                                   , (error) => {
                                            txt_ChestRstConfirm.text = "?????? ?????? ??????!";
                                            Panel_ChestRstConfirm.SetActive(true);
                                    });



    }

    public void grantUserRuby(string select_chest_item_instance_id, int amount)
    {

        PlayFabClientAPI.AddUserVirtualCurrency(new PlayFab.ClientModels.AddUserVirtualCurrencyRequest() {
                VirtualCurrency = "RB", Amount = amount}
                            , (result) => {
                                useChestRuby(select_chest_item_instance_id);
                            }
                            , (error) => {
                                Panel_Loading.SetActive(false);
                                txt_ChestRstConfirm.text = "?????? ?????? ??????!";
                                Panel_ChestRstConfirm.SetActive(true);
                            });


    }

    public void useChestRuby(string select_chest_item_instance_id)
    {
        PlayFabClientAPI.ConsumeItem( new PlayFab.ClientModels.ConsumeItemRequest {
                ConsumeCount = 1, ItemInstanceId = select_chest_item_instance_id}
                                    , (result) =>         
                                    {
                                            Panel_Loading.SetActive(false);
                                            txt_ChestRstConfirm.text = "?????? ?????? ??????!";
                                            Panel_ChestRstConfirm.SetActive(true);
                                    }
                                    , (error) => {
                                        Panel_Loading.SetActive(false);
                                        txt_ChestRstConfirm.text = "?????? ?????? ??????!";
                                        Panel_ChestRstConfirm.SetActive(true);

                                    });
    }
    public void useChestItem(string select_chest_item_instance_id)
    {
        PlayFabClientAPI.ConsumeItem( new PlayFab.ClientModels.ConsumeItemRequest {
                ConsumeCount = 1, ItemInstanceId = select_chest_item_instance_id}
                                    , (result) =>         
                                    {
                                        if(select_chest_item_id == "01" ||  select_chest_item_id == "03" || select_chest_item_id == "04" || select_chest_item_id == "05" || select_chest_item_id == "06" || select_chest_item_id == "07")
                                        {
                                            Panel_Loading.SetActive(false);
                                            Panel_ChestUse.SetActive(true);
                                        } 
                                        else 
                                        {
                                            Panel_Loading.SetActive(false);
                                            Panel_ChestUseList.SetActive(true);
                                        }
                                    }
                                    , (error) => {});
    }

    public void sellChestItem(string select_chest_item_instance_id)
    {

        PlayFabClientAPI.ConsumeItem( new PlayFab.ClientModels.ConsumeItemRequest {
                ConsumeCount = 1, ItemInstanceId = select_chest_item_instance_id}
                                    , (result) =>         
                                    {
                                        getChestSellMoney(select_chest_item_value);
                                    }
                                    , (error) => {
                                        Panel_Loading.SetActive(false);
                                        txt_ChestRstConfirm.text = "???????????? ??????!";
                                        Panel_ChestRstConfirm.SetActive(true);
                                    });
    }


    public void getChestSellMoney(uint select_chest_item_value)
    {

        PlayFabClientAPI.AddUserVirtualCurrency(new PlayFab.ClientModels.AddUserVirtualCurrencyRequest() {
                VirtualCurrency = "GD", Amount = (int)select_chest_item_value}
                            , (result) => {
                                       Panel_Loading.SetActive(false);
                                        txt_ChestRstConfirm.text = "???????????? ??????!";
                                        Panel_ChestRstConfirm.SetActive(true);
                                        LM.user_money = LM.user_money + (int)select_chest_item_value;
                                        user_money = user_money + (int)select_chest_item_value;
                                        txt_money.text = user_money.ToString();
                            }
                            , (error) => {});
                            

    }

    public void onClickChestUseChk()
    {
        SoundManager.instance.PlaySE("button");
        if(select_chest_item_id == null)
        {
            txt_ChestRstConfirm.text = "????????? ???????????????!";
            Panel_ChestRstConfirm.SetActive(true);
            return;
        }
        getChestInventory();
    }

    public void onClickChestUseListChk()
    {
        SoundManager.instance.PlaySE("button");
        getChestInventory();
    }

    public void onClickChestRstConfirmBtn()
    {
        SoundManager.instance.PlaySE("button");
        getChestInventory();
    }

    public void onClickChestCloseBtn()
    {
        SoundManager.instance.PlaySE("button");
        Panel_Chest.SetActive(false);
        Chest_state = false;
    }


}





