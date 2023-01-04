using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour, IPointerDownHandler
{
    public string User_ID;
    public List<string> chest_id = new List<string>();
    public List<string> chest_info = new List<string>();
    public List<string> chest_name = new List<string>();
    public List<uint> chest_value = new List<uint>();

    public List<string> item_id = new List<string>();
    public List<string> item_info = new List<string>();
    public List<string> item_name = new List<string>();
    public List<uint> item_value = new List<uint>();
    public string getItemIDList;
    public string getItemNameList;
    public List<string> setItemID = new List<string>();
    public List<string> setItemName = new List<string>();

    public GameObject Panel_Loading;
    public Slider Loading_Bar;
    public Text txt_Loading;


    public bool Chest_Chk = false;
    public bool Item_Chk = false;
    public bool Equip_Chk = false;
    public bool Money_Chk = false;
    public int user_money;
    public int user_ruby;

    public Text txt_money;
    public Text txt_ruby;

    public bool ItemSetChk = false;

    public SoundManager SM;

    void Awake()
    {
        User_ID = LoginMenu.User_ID;
    }

    // Start is called before the first frame update
    void Start()
    {
        txt_Loading.text = "0%";
        Loading_Bar.value = 0.0f;
        Panel_Loading.SetActive(true);
        getAllChest();
        getAllItem();
        getEquipedItemList();
        getUserMoney();
        Invoke("Loading_70",1);
        Time.timeScale = 0;

        SM.background.volume = PlayerPrefs.GetFloat("BGM");

        for(int i = 0; i < SM.sfxPlayer.Length; i++)
        {
            SM.sfxPlayer[i].volume = PlayerPrefs.GetFloat("Effect");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if( Chest_Chk == true && Item_Chk == true && Equip_Chk == true && Money_Chk ==true )
        {
            Time.timeScale = 1 ;   
            Invoke("End_Loading",2);
        } 

        txt_money.text = user_money.ToString();
        txt_ruby.text = user_ruby.ToString();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene("GamePlayScene");
    }

   public void getAllChest()
   {
       PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(){CatalogVersion = "CHEST"}
       , (result) =>
       {
           
           for(int i = 0; i < result.Catalog.Count; i++)
           {
               var chest_db = result.Catalog[i];
               chest_id.Add(chest_db.ItemId);
               chest_name.Add(chest_db.DisplayName);
               chest_info.Add(chest_db.Description);
               chest_value.Add(chest_db.VirtualCurrencyPrices["GD"]);

           }

           sortChest();

           Chest_Chk = true;
         
       },
       (error) => Chest_Chk = false );

  }

  public void sortChest()
  {
      for(int i = 0; i < chest_id.Count; i++)
      {
          for(int j = i+1; j<chest_id.Count; j++)
          {
              if(chest_value[i]<chest_value[j])
              {
                  string temp_id;
                  string temp_name;
                  string temp_info;
                  uint temp_value;

                  temp_id = chest_id[i];
                  chest_id[i] = chest_id[j];
                  chest_id[j] = temp_id;

                  temp_name = chest_name[i];
                  chest_name[i] = chest_name[j];
                  chest_name[j] = temp_name;

                  temp_info = chest_info[i];
                  chest_info[i] = chest_info[j];
                  chest_info[j] = temp_info;

                  temp_value = chest_value[i];
                  chest_value[i] = chest_value[j];
                  chest_value[j] = temp_value;

              }
          }
      }
  }

  public void sortAllItem()
  {
      for(int i = 0; i < item_id.Count; i++)
      {
          for(int j = i+1; j<item_id.Count; j++)
          {
              if(int.Parse(item_id[i])<int.Parse(item_id[j]))
              {
                  string temp_id;
                  string temp_name;
                  string temp_info;
                  uint temp_value;

                  temp_id = item_id[i];
                  item_id[i] = item_id[j];
                  item_id[j] = temp_id;

                  temp_name = item_name[i];
                  item_name[i] = item_name[j];
                  item_name[j] = temp_name;

                  temp_info = item_info[i];
                  item_info[i] = item_info[j];
                  item_info[j] = temp_info;

                  temp_value = item_value[i];
                  item_value[i] = item_value[j];
                  item_value[j] = temp_value;

              }
          }
      }
  }

    public void getAllItem()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(){CatalogVersion = "ITEM"}
        , (result) =>
        {
            for(int i = 0; i < result.Catalog.Count; i++)
            {
                var item_db = result.Catalog[i];
                item_id.Add(item_db.ItemId);
                item_name.Add(item_db.DisplayName);
                item_info.Add(item_db.Description);
                item_value.Add(item_db.VirtualCurrencyPrices["GD"]);
            }

            sortAllItem();

            Item_Chk = true;
          
        },
        (error) => Item_Chk = false );

    }




    public void getEquipedItemList()
    {
        PlayFabClientAPI.GetUserData( new GetUserDataRequest() {PlayFabId = User_ID}
                        , (result) => {
                            getItemIDList = result.Data["ITEMIDLIST"].Value;
                            getItemNameList = result.Data["ITEMNAMELIST"].Value;

                            getEquipedItem();

                        }
                        , (error) => Equip_Chk = false);
    }

    public void getEquipedItem()
    {
        setItemID.Clear();
        setItemName.Clear();

        string[] arrItemID = getItemIDList.Split(':');
        string[] arrItemName = getItemNameList.Split(':');

        for(int i = 0; i < 8; i++)
        {
            setItemID.Add(arrItemID[i]);
            setItemName.Add(arrItemName[i]);
        }
        Equip_Chk = true;
        ItemSetChk = true;
    }

    public void getUserMoney()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(),
        (result) => 
        {
            user_money = result.VirtualCurrency["GD"];
            user_ruby = result.VirtualCurrency["RB"];
            Money_Chk = true;
        }, 
        (error) => {
        });

    }

    public void End_Loading()
    {
        Loading_Bar.value = 1;
        txt_Loading.text = "100%";
        Invoke("Close_Loading", 1);
    }

    public void Loading_70()
    {
        txt_Loading.text = "70%";
        Loading_Bar.value = 0.7f;
    }

    public void Close_Loading()
    {
        Panel_Loading.SetActive(false);
    }

    public void OnClickGameStart()
    {
        SoundManager.instance.PlaySE("button");
        SceneManager.LoadScene("GamePlayScene");
    }


}
