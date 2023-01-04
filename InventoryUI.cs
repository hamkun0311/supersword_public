using System.Runtime.InteropServices;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InventoryUI : MonoBehaviour
{ 
    public GameObject Panel_Inventory;
    public GameObject Panel_Loading;
    public GameObject Panel_InventoryConfirm;
    public GameObject Panel_InvMenu;
    public GameObject Panel_ItemCnt;
    public GameObject Panel_OptionMenu;
    public GameObject Panel_Ranking;
    public GameObject Panel_Character;
    public GameObject Panel_StoreMenu;


    public Text txt_Loading;
    public Text txt_InventroyConfirm;

    public InputField input_itemCnt;

    bool Inventory_state = false;


    public string User_ID;
    public Slot[] slot = new Slot[100];
    public Slot[] setslot = new Slot[8];
    public LobbyManager LM;

    public List<string> inventory_item_id = new List<string>();
    public List<string> inventory_item_info = new List<string>();
    public List<string> inventory_item_name = new List<string>();
    public List<uint> inventory_item_value = new List<uint>();
    public List<int> inventory_item_count = new List<int>();
    public List<int> inventory_able_item_count = new List<int>();    
    public List<string> inventory_item_instance_id = new List<string>();

    public List<string> setItemID = new List<string>();
    public List<string> setItemName = new List<string>();

    public string getItemIDList;
    public string getItemNameList;

    public Text txt_money;
    public Text txt_ruby;
    public Text txt_ItemName;
    public Text txt_ItemInfo;

    public int inventoryinfo_state = 0; 

    public int user_money;
    public int user_ruby;

    public string select_item_id;
    public string select_item_name;
    public uint select_item_value;
    public string select_item_info;
    public int select_item_count;
    public string select_item_instance_id;

    public Transform slotHolder;

    public Store_UI sui;
    public Chest_UI cui;

    public bool invMenu_chk = false;
    private void Awake()
    {

    }

    private void Start() 
    {
        Panel_Inventory.SetActive(Inventory_state);  
        Panel_InventoryConfirm.SetActive(false);
        Panel_Loading.SetActive(false);
        Panel_ItemCnt.SetActive(false);
        Panel_Ranking.SetActive(false);
        Panel_Character.SetActive(false);
        Panel_OptionMenu.SetActive(false);
        Panel_StoreMenu.SetActive(false);
        User_ID = LoginMenu.User_ID;

        slot = slotHolder.GetComponentsInChildren<Slot>();

    }
    private void Update() 
    {
    }

    public void OnClickInventory()
    {

        SoundManager.instance.PlaySE("button");

        txt_ItemName.text = "";
        txt_ItemInfo.text = "";

        Panel_Inventory.SetActive(true);
        
        getSetItem();

        sui.StoreClose();
        cui.ChestClose();
        Panel_Ranking.SetActive(false);
        Panel_OptionMenu.SetActive(false);
        

    }

    public void getInventory()
    {

        txt_Loading.text = "불러오는 중..";
        Panel_Loading.SetActive(true);

        inventory_item_id.Clear();
        inventory_item_name.Clear();
        inventory_item_info.Clear();
        inventory_item_value.Clear();
        inventory_item_count.Clear();
        inventory_able_item_count.Clear();
        inventory_item_instance_id.Clear();

        deleteSlot();

        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
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
                if(item.CatalogVersion == "ITEM")
                {
                    inventory_item_id.Add(item.ItemId);
                    inventory_item_name.Add(item.DisplayName);
                    inventory_item_value.Add(item.UnitPrice);
                    inventory_item_count.Add(item.RemainingUses.Value);
                    inventory_item_instance_id.Add(item.ItemInstanceId);

                }

            }

            for(int i = 0; i < inventory_item_name.Count; i++)
            {
                slot[i].item_id = inventory_item_id[i];
                slot[i].item_name = inventory_item_name[i];
                slot[i].item_instance_id = inventory_item_instance_id[i];
                slot[i].item_count = inventory_item_count[i];
                slot[i].itemImage.sprite = Resources.Load<Sprite>("items/" + inventory_item_name[i]);
                slot[i].itemImage.preserveAspect = true;

                for(int j = 0; j < LM.item_id.Count; j++)
                {
                    if(slot[i].item_id == LM.item_id[j])
                    {
                        slot[i].item_info = LM.item_info[j];
                        slot[i].item_value = LM.item_value[j];
                    }
                }

            }

            string[] arrItemID = getItemIDList.Split(':');
            string[] arrItemName = getItemNameList.Split(':');

            for(int k = 0; k < 8; k++)
            {
                for(int l = 0; l < inventory_item_name.Count; l++)
                {
                    if(arrItemID[k] == inventory_item_id[l])
                    {
                        inventory_item_count[l]--;
                        slot[l].item_count = inventory_item_count[l];
                    }

                }
            }
            setItemSlot();
        }, 
        (error) => {
            txt_InventroyConfirm.text = "불러오기 실패";
            Panel_InventoryConfirm.SetActive(true);
        });

    }

    public void getSetItem()
    {
        txt_Loading.text = "불러오는 중..";
        Panel_Loading.SetActive(true);

        PlayFabClientAPI.GetUserData( new GetUserDataRequest() {PlayFabId = User_ID}
                        , (result) => {
                            getItemIDList = result.Data["ITEMIDLIST"].Value;
                            getItemNameList = result.Data["ITEMNAMELIST"].Value;
                            getInventory();

                        },
                        (error) => {
                            txt_InventroyConfirm.text = "불러오기 실패";
                            Panel_InventoryConfirm.SetActive(true);
                        });
        
    }



    public void Inventory_Close()
    {
        Panel_Inventory.SetActive(false);
        Inventory_state = false;

    }

    public void deleteSlot()
    {
        for(int i = 0; i < slot.Length; i++)
        {
            slot[i].item_id = null;
            slot[i].item_info = null;
            slot[i].item_name = null;
            slot[i].item_value = 0;
            slot[i].item_count = 0;
            slot[i].item_instance_id = null;
            slot[i].itemImage.sprite = null;
        }

    }

    public void deleteSetSlot()
    {
        for(int i = 0; i < 8; i++)
        {
            setslot[i].item_id = null;
            setslot[i].item_name = null;
            setslot[i].itemImage.sprite = null;
        }

        setItemID.Clear();
        setItemName.Clear();
    }

    public void setItemSlot()
    {
        deleteSetSlot();

        string[] arrItemID = getItemIDList.Split(':');
        string[] arrItemName = getItemNameList.Split(':');

        for(int i = 0; i < 8; i++)
        {
            setItemID.Add(arrItemID[i]);
            setItemName.Add(arrItemName[i]);
            setslot[i].item_id = arrItemID[i];
            setslot[i].item_name = arrItemName[i];
            setslot[i].itemImage.sprite = Resources.Load<Sprite>("items/" + arrItemName[i]);
        }

        Panel_Loading.SetActive(false);

    }

    public void onClinkSlotItem(Slot input_slot)
    {

        SoundManager.instance.PlaySE("button");

        renewItemCNT();

        for(int i = 0; i < slot.Length; i++)
        {
            slot[i].BackImg.color = UnityEngine.Color.white;
        }
        
        input_slot.BackImg.color = UnityEngine.Color.yellow;


        select_item_id = input_slot.item_id;
        select_item_name = input_slot.item_name;
        select_item_value = input_slot.item_value;
        select_item_info = input_slot.item_info;
        select_item_count = input_slot.item_count;
        select_item_instance_id = input_slot.item_instance_id;
        txt_ItemName.text = select_item_name;
        txt_ItemInfo.text = select_item_info + "\n갯수 : " + select_item_count + "\n가격 : " + select_item_value + "골드" ;

    }

    public void onClickSetItem()
    {

        SoundManager.instance.PlaySE("button");
        
        if(select_item_id == "899")
        {
            inventoryinfo_state = 0;
            txt_InventroyConfirm.text = "착용 불가 아이템 입니다.";
            Panel_InventoryConfirm.SetActive(true);
            return;
        }

        if(setItemName.Count > 7)
        {
            inventoryinfo_state = 0;
            txt_InventroyConfirm.text = "더이상 추가할 수 없습니다.";
            Panel_InventoryConfirm.SetActive(true);
            return;
        }

        if(select_item_count < 1)
        {
            inventoryinfo_state = 0;
            txt_InventroyConfirm.text = "아이템 수량이 부족합니다.";
            Panel_InventoryConfirm.SetActive(true);
            return;
        }

        setItemID.Add(select_item_id);
        setItemName.Add(select_item_name);

        for(int i = 0; i < setItemName.Count; i++)
        {
            setslot[i].item_id = setItemID[i];
            setslot[i].item_name = setItemName[i];
            setslot[i].itemImage.sprite = Resources.Load<Sprite>("items/" + setItemName[i]);
            setslot[i].itemImage.preserveAspect = true;
        }

        for(int i = 0; i < inventory_item_id.Count; i++)
        {
            if(setslot[setItemName.Count - 1].item_id == inventory_item_id[i])
            {
                inventory_item_count[i]--;
            }

            if(select_item_id == inventory_item_id[i])
            {
                select_item_count--;                
            }

        }

        renewItemCNT();

        txt_ItemInfo.text = select_item_info + "\n갯수 : " + select_item_count + "\n가격 : " + select_item_value ;

    }

    public void onClickReleaseItem()
    {

        SoundManager.instance.PlaySE("button");

        if(setItemName.Count <= 0)
        {
            inventoryinfo_state = 0;
            txt_InventroyConfirm.text = "더이상 해제할 수 없습니다.";
            Panel_InventoryConfirm.SetActive(true);
            return;
        }

        for(int i = 0; i < inventory_item_id.Count; i++)
        {
            if(setslot[setItemName.Count - 1].item_id == inventory_item_id[i])
            {
                inventory_item_count[i]++;
            }

        }

        if(select_item_id == setslot[setItemName.Count - 1].item_id)
        {
            select_item_count++;                
        }

        renewItemCNT();

        if(select_item_id == setItemID[setItemID.Count-1])
        {
            txt_ItemName.text = select_item_name;
            txt_ItemInfo.text = select_item_info + "\n갯수 : " + select_item_count + "\n가격 : " + select_item_value ;
        }

        setslot[setItemName.Count - 1].item_id = null;
        setslot[setItemName.Count - 1].item_name = null;
        setslot[setItemName.Count - 1].itemImage.sprite = null;
        setItemID.RemoveAt(setItemName.Count - 1);
        setItemName.RemoveAt(setItemName.Count - 1);


        

    }

    public void onClickConfirmBtn()
    {

        SoundManager.instance.PlaySE("button");


        if(setItemName.Count != 8)
        {
            inventoryinfo_state = 0;
            txt_InventroyConfirm.text = "아이템을 추가하세요.";
            Panel_InventoryConfirm.SetActive(true);
            return;
        }

        txt_Loading.text = "덱 구성중..";
        Panel_Loading.SetActive(true);

        PlayFabClientAPI.UpdateUserData(new PlayFab.ClientModels.UpdateUserDataRequest() 
        {Data = new Dictionary<string, string>(){
                 {"ITEMIDLIST", setItemID[0] + ":" + setItemID[1] + ":" + setItemID[2] 
                                             + ":" + setItemID[3] + ":" + setItemID[4] 
                                             + ":" + setItemID[5] + ":" + setItemID[6]
                                             + ":" + setItemID[7]}  
                ,{"ITEMNAMELIST", setItemName[0] + ":" + setItemName[1] + ":" + setItemName[2] 
                                                 + ":" + setItemName[3] + ":" + setItemName[4] 
                                                 + ":" + setItemName[5] + ":" + setItemName[6]
                                                 + ":" + setItemName[7]}

                
                }}
                            , (result) => {
                                inventoryinfo_state = 1;
                                for(int i = 0; i<setItemID.Count; i++)
                                {
                                    LM.setItemID[i] = setItemID[i];
                                    LM.setItemName[i] = setItemName[i];
                                }
                                
                                LM.ItemSetChk = true;

                                Panel_Loading.SetActive(false);
                                txt_InventroyConfirm.text = "덱 구성 성공!";


                                Panel_InventoryConfirm.SetActive(true);

                            }
                            , (error) => {
                                Panel_Loading.SetActive(false);
                                txt_InventroyConfirm.text = "덱 구성 실패!";
                                Panel_InventoryConfirm.SetActive(true);

                            });

    }


    public void renewItemCNT()
    {
        for(int i = 0; i < inventory_item_name.Count; i++)
        {
            slot[i].item_count = inventory_item_count[i];
        }
    }

    public void onClickItemSell()
    {

        SoundManager.instance.PlaySE("button");


        if(select_item_id == "900")
        {
            inventoryinfo_state = 0;
            txt_InventroyConfirm.text = "판매 불가 아이템 입니다.";
            Panel_InventoryConfirm.SetActive(true);
            return;
        }

        if(txt_ItemInfo.text == "")
        {
            inventoryinfo_state = 0;
            txt_InventroyConfirm.text = "아이템을 선택하세요.";
            Panel_InventoryConfirm.SetActive(true);
            return;
        }

        if(select_item_count < 1)
        {
            inventoryinfo_state = 0;
            txt_InventroyConfirm.text = "아이템 수량이 부족합니다.";
            Panel_InventoryConfirm.SetActive(true);
            return;
        }

        input_itemCnt.text = select_item_count.ToString();
        Panel_ItemCnt.SetActive(true);


    }

    public void onClickItemCntSell()
    {

        SoundManager.instance.PlaySE("button");


        int sellitemCnt = int.Parse(input_itemCnt.text);


        txt_ItemInfo.text = "";
        txt_ItemName.text = "";

        Panel_ItemCnt.SetActive(false);

        txt_Loading.text = "아이템 판매중..";
        Panel_Loading.SetActive(true);
        useInventoryItem(select_item_instance_id, sellitemCnt);

    }


    public void useInventoryItem(string select_item_instance_id, int item_cnt)
    {
        PlayFabClientAPI.ConsumeItem( new PlayFab.ClientModels.ConsumeItemRequest {
                ConsumeCount = item_cnt, ItemInstanceId = select_item_instance_id}
                                    , (result) => {
                                        getInventorySellMoney(select_item_value*(uint)item_cnt);
                                    }
                                    , (error) => {
                                        
                                        Panel_Loading.SetActive(false);
                                        txt_InventroyConfirm.text = "아이템 판매 실패!";
                                        Panel_InventoryConfirm.SetActive(true);

                                    });
    }

    public void getInventorySellMoney(uint select_item_value)
    {

        PlayFabClientAPI.AddUserVirtualCurrency(new PlayFab.ClientModels.AddUserVirtualCurrencyRequest() {
                VirtualCurrency = "GD", Amount = (int)select_item_value}
                            , (result) => {
                                        inventoryinfo_state = 7;
                                        Panel_Loading.SetActive(false);
                                        txt_InventroyConfirm.text = "아이템 판매 성공!";
                                        Panel_InventoryConfirm.SetActive(true);
                            }
                            
                            , (error) => print("fail"));

    }

    public void onClickInventoryConfirmBtn()
    {
        SoundManager.instance.PlaySE("button");

        for(int i = 0; i < slot.Length; i++)
        {
            slot[i].BackImg.color = UnityEngine.Color.white;
        }
        if(inventoryinfo_state != 0)
        {
            getSetItem();
        }

        txt_ItemName.text = "";
        txt_ItemInfo.text = "";
        
        Panel_InventoryConfirm.SetActive(false);
    }

    public void onClickTest()
    {
        Panel_InvMenu.SetActive(true);

    }

    public void onClickInvMenu()
    {
        SoundManager.instance.PlaySE("button");

        invMenu_chk = !invMenu_chk;
        Panel_InvMenu.SetActive(invMenu_chk);
        sui.StoreClose();
        cui.ChestClose();
        Panel_Ranking.SetActive(false);
        Panel_Character.SetActive(false);
        Panel_Inventory.SetActive(false);
        Panel_StoreMenu.SetActive(false);
    }

    public void onClickInvMenuCancel()
    {
        SoundManager.instance.PlaySE("button");
        Panel_InvMenu.SetActive(false);
    }

    public void onClickCharBtn()
    {
        SoundManager.instance.PlaySE("button");
        Panel_Character.SetActive(true);
    }

    public void onClickInventoryCancel()
    {
        SoundManager.instance.PlaySE("button");
        Panel_Inventory.SetActive(false);
        Panel_Character.SetActive(false);
    }

    public void onClickItemCntCancel()
    {
        SoundManager.instance.PlaySE("button");
        Panel_ItemCnt.SetActive(false);
    }

    public void onClickInventoryCloseBtn()
    {
        SoundManager.instance.PlaySE("button");
        Panel_Inventory.SetActive(false);
    }
    public void onClickCharacterCloseBtn()
    {
        SoundManager.instance.PlaySE("button");
        Panel_Character.SetActive(false);
    }


}
  


