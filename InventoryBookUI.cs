using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class InventoryBookUI : MonoBehaviour
{

    public GameObject Panel_InvBook;
    public GameObject Panel_Loading;

    public Slot[] slot = new Slot[100];
    public LobbyManager LM;
    public List<string> itembook_item_id = new List<string>();
    public List<string> itembook_item_info = new List<string>();
    public List<string> itembook_item_name = new List<string>();
    public List<uint> itembook_item_value = new List<uint>();
    public int[] itembook_item_count = new int[100];
    public List<string> inventory_item_id = new List<string>();

    public string select_item_id;
    public string select_item_name;
    public uint select_item_value;
    public string select_item_info;
    public int select_item_count;

    public Text txt_ItemName;
    public Text txt_ItemInfo;
    public Transform slotHolder;

    // Start is called before the first frame update
    void Start()
    {
        Panel_InvBook.SetActive(false);
        Panel_Loading.SetActive(false);
        slot = slotHolder.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClinkSlotItem(Slot input_slot)
    {

        SoundManager.instance.PlaySE("button");

        select_item_id = input_slot.item_id;
        select_item_name = input_slot.item_name;
        select_item_value = input_slot.item_value;
        select_item_info = input_slot.item_info;
        txt_ItemName.text = select_item_name;
        txt_ItemInfo.text = select_item_info + "\n가격 : " + select_item_value + " 골드" ;

        if(input_slot.item_count > 0)
        {
            txt_ItemInfo.text = txt_ItemInfo.text + "\n보유중";
        } else
        {
            txt_ItemInfo.text = txt_ItemInfo.text + "\n미획득";
        }

    }

    public void onClickInvBookCloseBtn()
    {
        SoundManager.instance.PlaySE("button");
        Panel_InvBook.SetActive(false);
    }

    public void onClickInvBookBtn()
    {
        SoundManager.instance.PlaySE("button");
        getInvBook();
        Panel_InvBook.SetActive(true);
    }




    public void getInvBook()
    {

        Panel_Loading.SetActive(true);

        itembook_item_id.Clear();
        itembook_item_name.Clear();
        itembook_item_info.Clear();
        itembook_item_value.Clear();

        inventory_item_id.Clear();

        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
        (result) => 
        {
            Panel_Loading.SetActive(false);
            foreach(var item in result.Inventory)
            {
                if(item.CatalogVersion == "ITEM")
                {
                    inventory_item_id.Add(item.ItemId);
                }

            }

            for(int i=0; i<LM.item_id.Count; i++)
            {
                itembook_item_id.Add(LM.item_id[i]);
                itembook_item_name.Add(LM.item_name[i]);
                itembook_item_info.Add(LM.item_info[i]);
                itembook_item_value.Add(LM.item_value[i]);
            }

            for(int i = 0; i < itembook_item_id.Count; i++)
            {
                for(int j = 0; j < inventory_item_id.Count; j++)
                {
                    if(itembook_item_id[i] == inventory_item_id[j])
                    {
                        itembook_item_count[i]++;
                    }
                }

            }

            for(int i = 0; i < itembook_item_id.Count; i++)
            {
                    slot[i].item_id = itembook_item_id[i];
                    slot[i].item_name = itembook_item_name[i];
                    slot[i].item_info = itembook_item_info[i];
                    slot[i].item_value = itembook_item_value[i];
                    slot[i].item_count = itembook_item_count[i];
                    slot[i].itemImage.sprite = Resources.Load<Sprite>("items/" + itembook_item_name[i]);
                    slot[i].itemImage.preserveAspect = true;
                    slot[i].BackImg.color = UnityEngine.Color.white;
                    
                    if(itembook_item_count[i] > 0)
                    {
                        slot[i].BackImg.color = UnityEngine.Color.yellow;
                    }

            }
        
        },
        (error) => {
            Debug.Log("failed");
        });

    }

}
