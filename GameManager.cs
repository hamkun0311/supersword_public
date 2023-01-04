
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour 
{
    public string User_ID;
    public PlayerMove PM;
    public Transform Player;

    public Transform SpawnPoint;
    public Transform WolfSpawn1;
    public Transform WolfSpawn2;
    public Transform WolfSpawn3;
    public Transform WolfSpawn4;
    public Transform WolfSpawn5;
    public Transform WolfSpawn6;
    public Transform WolfSpawn7;
    public Transform WolfSpawn8;

    public Slider hpbar;
    public Slider expbar;
    public Slider superswordbar;
    public int P_health_total = 0;
    public int P_health_now = 0;
    public int P_health_total_tmp = 0;
    public int P_health_now_tmp = 0;
    public float leftTime = 600;

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

    public List<string> useItemID = new List<string>();
    public List<string> useItemName = new List<string>();

    public int[] P_Nexp = new int[8];

    public GameObject Panel_ItemSelect;
    public GameObject Panel_SetItem;
    public GameObject Panel_EndInfo;
    public GameObject Panel_Pause;

    public Slot slot1;
    public Slot slot2;
    public Slot slot3;

    public Slot[] setslot = new Slot[8];

    public Text txt_leftTime;
    public Text txt_Gold;
    public Text txt_HPinfo;
    public Text txt_Point;
    public Text txt_Expinfo;
    public Text txt_Power;
    public Text txt_MPower;
    public Text txt_firecnt;
    public Text txt_icecnt;
    public Text txt_stonecnt;
    public Text txt_thundercnt;


    public Text txt_ItemInfo1;
    public Text txt_ItemInfo2;
    public Text txt_ItemInfo3;
    public Text txt_getMoney;
    public Text txt_getPoint;
    public Text txt_gameResult;

    public string select_item_id;
    public string select_item_name;


    public GameObject Goblin;
    public GameObject Slime;
    public GameObject Beholder;
    public GameObject Demon;
    public GameObject GoblinKing;
    public GameObject Ooze;
    public GameObject Wolf;

    public GameObject LightningBird;
    public GameObject FireBird;
    public GameObject IceRabbit;
    public GameObject StoneRabbit;

    public GameObject IceRabbit2;

    public GameObject FlameOrb;
    public GameObject IceOrb;
    public GameObject StoneOrb;
    public GameObject LightningOrb;
    public GameObject ChaosOrb;

    public GameObject FireArmor;
    public GameObject IceArmor;
    public GameObject StoneArmor;
    public GameObject LightningArmor;
    public GameObject ChaosArmor;

    public GameObject FireBookObj;
    public GameObject IceBookObj;
    public GameObject StoneBookObj;
    public GameObject LightningBookObj;
    public GameObject FireBook;
    public GameObject IceBook;
    public GameObject StoneBook;
    public GameObject LightningBook;
    public GameObject FireStaff;
    public GameObject IceStaff;
    public GameObject StoneStaff;
    public GameObject LightningStaff;    
    public GameObject FireStaffObj;
    public GameObject IceStaffObj;
    public GameObject StoneStaffObj;
    public GameObject LightningStaffObj;    
    public GameObject Leaf4;    

    public GameObject IceHat;

    public GameObject Supersword;

    public int monster_cnt = 0;

    public int kill_cnt = 0;

    public int fire_cnt = 0;
    public int ice_cnt = 0;
    public int stone_cnt = 0;
    public int lightning_cnt = 0;

    public int wave_sword_cnt = 0;
    public int sword_cnt;
    public int bow_cnt;
    public int minion_cnt;
    public int book_cnt;
    public int armor_cnt;
    public int orb_cnt;
    public int staff_cnt;

    public int c_sword_cnt = 0;

    public int steal_health_p = 0;
    public int steal_health_v = 0;

    

    public int sword_cnt_tmp;
    public int bow_cnt_tmp;
    public int minion_cnt_tmp;
    public int book_cnt_tmp;
    public int armor_cnt_tmp;
    public int orb_cnt_tmp;
    public int staff_cnt_tmp;

    public int leaf4_cnt;
    public float leaf4_P_cool = 10;
    public float leaf4_M_cool = 5;
    public float leaf4_P_fire;
    public float leaf4_M_fire;

    public int leaf4_flag = 0;


/*
    public int sword_dmg = 100;
    public int bow_dmg = 100;
    public int minion_dmg = 200;
    public int book_dmg = 200;
    public int armor_dmg = 50;
    public int orb_dmg = 500;
    public int staff_dmg = 1000;
*/
    public int fire_time = 0;
    public int ice_time = 0;
    public int stone_time = 0;
    public int lightning_time = 0;

    public int f_bow_cnt = 0;
    public int i_bow_cnt = 0;
    public int s_bow_cnt = 0;
    public int l_bow_cnt = 0;

    public int f_minion1_cnt = 0;
    public int i_minion1_cnt = 0;
    public int s_minion1_cnt = 0;
    public int l_minion1_cnt = 0;

    public int f_orb_cnt = 0;
    public int i_orb_cnt = 0;
    public int s_orb_cnt = 0;
    public int l_orb_cnt = 0;

    public int c_armor_cnt = 0;
    public int f_armor_cnt = 0;
    public int i_armor_cnt = 0;
    public int s_armor_cnt = 0;
    public int l_armor_cnt = 0;
    
    public int f_book_cnt = 0;
    public int i_book_cnt = 0;
    public int s_book_cnt = 0;
    public int l_book_cnt = 0;

    public int f_staff_cnt = 0;
    public int i_staff_cnt = 0;
    public int s_staff_cnt = 0;
    public int l_staff_cnt = 0;

    public int ice_hat_cnt = 0;
    
    public int health_cnt = 0;

    public int health_cnt_tmp = 0;

    public int meat_cnt = 0;
    public int whisky_cnt = 0;

    public int meat_cnt_tmp = 0;
    public int whisky_cnt_tmp = 0;

    public int hat_cnt = 0;
    public int hat_cnt_tmp = 0;

    public int yy_sword_cnt = 0;
    
    public int P_lvl = 0;
    public int P_exp = 1;
    public int P_power = 0;
    public int P_Mpower = 0;
    public int P_power_tmp = 0;
    public int P_Mpower_tmp = 0;
    public int get_money = 1;
    public int get_point = 1;

    public float health_cool = 5.0f;
    public float health_fire = 0.0f;

    public float fire_armor_cool = 1.0f;
    public float fire_armor_fire = 0.0f;
    public float ice_armor_cool = 1.0f;
    public float ice_armor_fire = 0.0f;
    public float stone_armor_cool = 1.0f;
    public float stone_armor_fire = 0.0f;
    public float light_armor_cool = 1.0f;
    public float light_armor_fire = 0.0f;
    public float fire_book_cool = 3.0f;
    public float fire_book_fire = 0.0f;
    public float ice_book_cool = 3.0f;
    public float ice_book_fire = 0.0f;
    public float stone_book_cool = 3.0f;
    public float stone_book_fire = 0.0f;
    public float light_book_cool = 3.0f;
    public float light_book_fire = 0.0f;
    public float fire_staff_cool = 5.0f;
    public float fire_staff_fire = 0.0f;
    public float ice_staff_cool = 5.0f;
    public float ice_staff_fire = 0.0f;
    public float stone_staff_cool = 5.0f;
    public float stone_staff_fire = 0.0f;
    public float light_staff_cool = 5.0f;
    public float light_staff_fire = 0.0f;

    public float ice_hat_cool = 5.0f;
    public float ice_hat_fire = 0.0f; 

    public int quiver_cnt = 0;
    public int windforce_cnt = 0;
    public int babyforce_cnt = 0;

    public float slime_cool = 3.0f;
    public float slime_fire = 0;


    public float goblin_cool = 3.0f;
    public float goblin_fire = 0;

    public float beholder_cool = 3f;
    public float beholder_fire = 0;

    
    public float goblinKing_cool = 5f;
    public float goblinking_fire = 0;

    
    public float ooze_cool = 5f;

    public float ooze_fire = 0;

    public float wolf_cool = 3f;
    public float wolf_fire = 0;
    public int utilCoin_cnt = 0;

    public int monster_exp = 1;

    public bool getEquipedItem_chk = false;
    public bool getAllChest_chk = false;
    public bool getAllItem_chk = false;
    public bool getPlayerStats_chk = false;

    //로딩
    public GameObject Panel_Loading;
    public Slider Loading_Bar;
    public Text txt_Loading;

    public bool boss_death_chk = false;
    public int boss_spawn_cnt = 0;

    public bool supersword_flag = false;

    //Panel_Option
    public SoundManager SM;
    public GameObject Panel_Option;
    public Slider slider_BGM;
    public Slider slider_Effect;

    

//  virtual joystick
//    public VirtualJoystick virtualJoystick;
//    public VirtualAttackBtn virtualAttackBtn;

    


    void Awake()
    {
        User_ID = LoginMenu.User_ID;
        Panel_Loading.SetActive(true);
        txt_Loading.text = "0%";
        Loading_Bar.value = 0.0f;
        getVolumn();
    }

    // Start is called before the first frame update
    void Start()
    {
        Panel_ItemSelect.SetActive(false);
        Panel_EndInfo.SetActive(false);
        Panel_Pause.SetActive(false);
        Panel_Option.SetActive(false);
        
        txt_Loading.text = "100%";
        Loading_Bar.value = 1f;
        
        superswordbar.maxValue = 600f;
        
        getEquipedItemList();
        getAllChest();
        setNextExp();

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

        

        if(getAllChest_chk == true && getAllItem_chk == true && getEquipedItem_chk == true && getPlayerStats_chk == true && P_lvl < 8)
        {

            PlayerLvlUp();
        }

        if(leftTime > 1)
        {
            leftTime -= Time.deltaTime;
            txt_leftTime.text = "남은시간\n" + Mathf.Round(leftTime);
            superswordbar.value = 600f - leftTime;
        } else {
            leftTime = 1;
            txt_leftTime.text = "보스출현!";
            
            if(boss_death_chk == true)
            {
                superswordbar.value = superswordbar.value - 2f;
                PM.SuperSword1.Stop();
                PM.SuperSword2.Stop();

            }else
            {
                superswordbar.value = 600f;
                SuperSword();
            }
            
        }

        txt_Gold.text = get_money.ToString();

        if(P_health_now>P_health_total)
        {
            P_health_now = P_health_total;
        }
        txt_HPinfo.text = P_health_now.ToString() + " / " + P_health_total.ToString();

        SpawnGoblin();
        SpawnSlime();
        SpawnBeholder();
        SpawnOoze();
        SpawnGoblinKing();
        SpawnDemon();
        SpawnWolf();

        FireArmorSpawn();
        IceArmorSpawn();
        StoneArmorSpawn();
        LightArmorSpawn();
        ChaosArmorSpawn();

        FireBookSpawn();
        IceBookSpawn();
        StoneBookSpawn();
        LightBookSpawn();

        FireStaffSpawn();
        IceStaffSpawn();
        StoneStaffSpawn();
        LightStaffSpawn();

        left4Spawn();
        IceHatSpawn();

        getHpBar();
        getExpBar();

        RestoreHealth();

        steal_health_v = Mathf.CeilToInt(P_power * (float)steal_health_p / 1000 );
        get_point = kill_cnt + get_money;
        txt_Point.text = get_point.ToString();
        txt_Power.text = P_power.ToString();
        txt_MPower.text = P_Mpower.ToString();
        txt_firecnt.text = fire_cnt.ToString();
        txt_icecnt.text = ice_cnt.ToString();
        txt_stonecnt.text = stone_cnt.ToString();
        txt_thundercnt.text = lightning_cnt.ToString();

        EndGameChk();
    }

    public void SpawnGoblin()
    {
        if(Time.time > goblin_fire && P_lvl >= 2 && boss_spawn_cnt == 0 && monster_cnt < 100)
        {
            for(int i = 0; i < 30; i++)
            {
                Instantiate(Goblin, getCirclePosition("Goblin"), Quaternion.identity);
            }
            monster_cnt = monster_cnt + 30;
            goblin_fire = Time.time + goblin_cool;
        }
    }

    public void SpawnGoblinKing()
    {
        if((Time.time > goblinking_fire) && P_lvl >= 5 && boss_spawn_cnt == 0 && monster_cnt < 100)
        {
            for(int i = 0; i < 20; i++)
            {
                Instantiate(GoblinKing, getCirclePosition("GoblinKing"), Quaternion.identity);
            }
            monster_cnt = monster_cnt + 20;
            goblinking_fire = Time.time + goblinKing_cool;
        }
    }


    public void SpawnSlime()
    {



        if(Time.time > slime_fire && boss_spawn_cnt == 0 && monster_cnt < 100 )
        {
            Vector3 spawn_point1 = new Vector3(PM.transform.position.x , PM.transform.position.y - 3f, 0);
            Vector3 spawn_point2 = new Vector3(PM.transform.position.x , PM.transform.position.y + 3f, 0);
            Vector3 spawn_point3 = new Vector3(PM.transform.position.x - 3f, PM.transform.position.y , 0);
            Vector3 spawn_point4 = new Vector3(PM.transform.position.x + 3f, PM.transform.position.y , 0);
            Vector3 spawn_point5 = new Vector3(PM.transform.position.x - 3f, PM.transform.position.y - 3f, 0);
            Vector3 spawn_point6 = new Vector3(PM.transform.position.x - 3f, PM.transform.position.y + 3f, 0);
            Vector3 spawn_point7 = new Vector3(PM.transform.position.x + 3f, PM.transform.position.y - 3f, 0);
            Vector3 spawn_point8 = new Vector3(PM.transform.position.x + 3f, PM.transform.position.y + 3f, 0);

            Instantiate(Slime, spawn_point1, Quaternion.identity);
            Instantiate(Slime, spawn_point2, Quaternion.identity);
            Instantiate(Slime, spawn_point3, Quaternion.identity);
            Instantiate(Slime, spawn_point4, Quaternion.identity);
            Instantiate(Slime, spawn_point5, Quaternion.identity);
            Instantiate(Slime, spawn_point6, Quaternion.identity);
            Instantiate(Slime, spawn_point7, Quaternion.identity);
            Instantiate(Slime, spawn_point8, Quaternion.identity);

            monster_cnt = monster_cnt + 8;

            slime_fire = Time.time + slime_cool;
        }
    }

    public void SpawnBeholder()
    {
        if((Time.time > beholder_fire) && P_lvl >= 3 && boss_spawn_cnt == 0 && monster_cnt < 100)
        {
            
            for(int i = 0; i < 8; i++)
            {
                Instantiate(Beholder, getCirclePosition("Beholder"), Quaternion.identity);
            }
            monster_cnt = monster_cnt + 8;
            beholder_fire = Time.time + beholder_cool;
        }
    }
    
    public void SpawnWolf()
    {
        if(Time.time > wolf_fire && P_lvl >= 4 && boss_spawn_cnt == 0 && monster_cnt < 100 )
        {

            Instantiate(Wolf, WolfSpawn1.position, Quaternion.identity);
            Instantiate(Wolf, WolfSpawn2.position, Quaternion.identity);
            Instantiate(Wolf, WolfSpawn3.position, Quaternion.identity);
            Instantiate(Wolf, WolfSpawn4.position, Quaternion.identity);
            Instantiate(Wolf, WolfSpawn5.position, Quaternion.identity);
            Instantiate(Wolf, WolfSpawn6.position, Quaternion.identity);
            Instantiate(Wolf, WolfSpawn7.position, Quaternion.identity);
            Instantiate(Wolf, WolfSpawn8.position, Quaternion.identity);

            monster_cnt = monster_cnt + 8;

            wolf_fire = Time.time + wolf_cool;
        }
    }

   public void SpawnOoze()
    {
        if((Time.time > ooze_fire) && P_lvl >= 6 && boss_spawn_cnt == 0 && monster_cnt < 100 )
        {
            for(int i = 0; i < 8; i++)
            {
                Instantiate(Ooze, getCirclePosition("Ooze"), Quaternion.identity);
            }
            monster_cnt = monster_cnt + 8;
            ooze_fire = Time.time + ooze_cool;
        }
    }

    public void SpawnDemon()
    {
        if(leftTime < 1 && boss_spawn_cnt == 0)
        {
            boss_spawn_cnt++;
            Vector3 spawn_point = new Vector3(SpawnPoint.position.x, SpawnPoint.position.y,0);
            Instantiate(Demon, spawn_point, Quaternion.identity);
            monster_cnt = monster_cnt + 1;

            PM.Demon = Demon.transform;

        }
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

            getAllChest_chk = true;

            getAllItem();

           }
         
       },
       (error) => print("fail"));

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

            getPlayerStats();

            getAllItem_chk = true;
          
        },
        (error) => print("fail"));

    }

    public void getEquipedItemList()
    {
        PlayFabClientAPI.GetUserData( new GetUserDataRequest() {PlayFabId = User_ID}
                        , (result) => {
                            getItemIDList = result.Data["ITEMIDLIST"].Value;
                            getItemNameList = result.Data["ITEMNAMELIST"].Value;

                            getEquipedItem();

                        }
                        , (error) => print("error"));
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

        getEquipedItem_chk = true;

    }

    public void setNextExp()
    {
        P_Nexp[0] = 1;
        P_Nexp[1] = 100;
        P_Nexp[2] = 1000;
        P_Nexp[3] = 2500;
        P_Nexp[4] = 7000;
        P_Nexp[5] = 20000;
        P_Nexp[6] = 50000;
        P_Nexp[7] = 100000;

    }

    public void PlayerLvlUp()
    {
        if(P_exp >= P_Nexp[P_lvl])
        {
            P_exp = 0;
            P_lvl++;
            P_health_now = P_health_total;
            Panel_Loading.SetActive(false);
            OpenItemSelect();
        }
    }

    public void OpenItemSelect()
    {
        Time.timeScale = 0;
        setSelectItemSlot();
        Panel_ItemSelect.SetActive(true);

    }
    public void setSelectItemSlot()
    {
        slot1.item_id = null;
        slot1.item_name = null;
        slot1.item_info = null;
        slot2.item_id = null;
        slot2.item_name = null;
        slot2.item_info = null;
        slot3.item_id = null;
        slot3.item_name = null;
        slot3.item_info = null;

        select_item_id = null;
        select_item_name = null;

        slot1.BackImg.color = UnityEngine.Color.white;
        slot2.BackImg.color = UnityEngine.Color.white;
        slot3.BackImg.color = UnityEngine.Color.white;

        int rand_no1 = UnityEngine.Random.Range(1,100);
        int rand_no2 = UnityEngine.Random.Range(1,100);
        int rand_no3 = UnityEngine.Random.Range(1,100);



        int first_item_no = rand_no1 % 8;

        int second_item_no = rand_no2 % item_id.Count;
        
        int third_item_no = rand_no3 % item_id.Count;


        slot1.item_id = setItemID[first_item_no];
        slot1.item_name = setItemName[first_item_no];
        slot1.itemImage.sprite = Resources.Load<Sprite>("items/" + setItemName[first_item_no]);
        slot1.itemImage.preserveAspect = true;

        for(int i = 0; i < item_id.Count; i++)
        {
            if(setItemID[first_item_no] == item_id[i])
            {
                slot1.item_info = item_info[i];    
            }
            
        }

        txt_ItemInfo1.text = slot1.item_name + "\n" + slot1.item_info;

        if(item_name[second_item_no] == "긴급지원금")
        {
            second_item_no--;
        }

        if(item_name[third_item_no] == "긴급지원금")
        {
            third_item_no--;
        }

        slot2.item_id = item_id[second_item_no];
        slot2.item_name = item_name[second_item_no];
        slot2.item_info = item_info[second_item_no];
        slot2.itemImage.sprite = Resources.Load<Sprite>("items/" + item_name[second_item_no]);
        slot2.itemImage.preserveAspect = true;

        txt_ItemInfo2.text = slot2.item_name + "\n" + slot2.item_info;

        slot3.item_id = item_id[third_item_no];
        slot3.item_name = item_name[third_item_no];
        slot3.item_info = item_info[third_item_no];
        slot3.itemImage.sprite = Resources.Load<Sprite>("items/" + item_name[third_item_no]);
        slot3.itemImage.preserveAspect = true;

        txt_ItemInfo3.text = slot3.item_name + "\n" + slot3.item_info;


    }

    public void onClickSelectItemSlot(Slot slot)
    {

        slot1.BackImg.color = UnityEngine.Color.white; 
        slot2.BackImg.color = UnityEngine.Color.white; 
        slot3.BackImg.color = UnityEngine.Color.white; 

        slot.BackImg.color = UnityEngine.Color.yellow;
        select_item_id = slot.item_id;
        select_item_name = slot.item_name;
    }

    public void onClickBtn_Confirm()
    {

        if(select_item_id == null)
        {
            return;
        }

        if(select_item_id == "108" || select_item_id == "115" || select_item_id == "122" || select_item_id == "129" || select_item_id == "900" || select_item_id == "139"|| select_item_id == "140"|| select_item_id == "141" || select_item_id == "896")
        {
            P_power = P_power + (P_power * 20 / 100);
            sword_cnt++;
        }

        if(select_item_id == "109")
        {
            P_power = P_power + (P_power * 20 / 100);
            f_bow_cnt++;
            bow_cnt++;
        }

        if(select_item_id == "116")
        {
            i_bow_cnt++;
            P_power = P_power + (P_power * 20 / 100);
            bow_cnt++;
        }

        if(select_item_id == "123")
        {
            s_bow_cnt++;
            bow_cnt++;
            P_power = P_power + (P_power * 20 / 100);
        }

        if(select_item_id == "130")
        {
            l_bow_cnt++;
            bow_cnt++;
            P_power = P_power + (P_power * 20 / 100);
        }

        if(select_item_id == "139")
        {
            yy_sword_cnt++;
        }

        if(select_item_id == "140")
        {
            steal_health_p = steal_health_p + 5;
        }

        if(select_item_id == "141")
        {
            wave_sword_cnt++;
        }

        if(select_item_id == "110")
        {
            Instantiate(FireBird, Player.position, Quaternion.identity);
            f_minion1_cnt++;
            minion_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "117")
        {
            Instantiate(IceRabbit, Player.position, Quaternion.identity);
            i_minion1_cnt++;
            minion_cnt++;
            P_Mpower = P_Mpower + 200;
        }
/*
        if(select_item_id == "897")
        {
            Instantiate(IceRabbit2, Player.position, Quaternion.identity);
            i_minion1_cnt++;
            minion_cnt++;
            P_Mpower = P_Mpower + 500;
        }
*/
        if(select_item_id == "124")
        {
            Instantiate(StoneRabbit, Player.position, Quaternion.identity);
            s_minion1_cnt++;
            minion_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "131")
        {
            Instantiate(LightningBird, Player.position, Quaternion.identity);
            l_minion1_cnt++;
            minion_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "113")
        {
            Instantiate(FlameOrb, Player.position, Quaternion.identity);
            f_orb_cnt++;
            orb_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "120")
        {
            Instantiate(IceOrb, Player.position, Quaternion.identity);
            i_orb_cnt++;
            orb_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "127")
        {
            Instantiate(StoneOrb, Player.position, Quaternion.identity);
            s_orb_cnt++;
            orb_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "134")
        {
            Instantiate(LightningOrb, Player.position, Quaternion.identity);
            l_orb_cnt++;
            orb_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "898")
        {
            Instantiate(ChaosOrb, Player.position, Quaternion.identity);
            f_orb_cnt++;
            i_orb_cnt++;
            s_orb_cnt++;
            l_orb_cnt++;
            orb_cnt++;
            P_Mpower = P_Mpower + 500;
        }
        if(select_item_id == "896")
        {
            c_sword_cnt++;
        }
        if(select_item_id == "897")
        {
            P_health_total = P_health_total + 100;
            P_health_now = P_health_now + 100;
            c_armor_cnt++;
            armor_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "112")
        {
            P_health_total = P_health_total + 100;
            P_health_now = P_health_now + 100;
            f_armor_cnt++;
            armor_cnt++;
            P_Mpower = P_Mpower + 200;

        }

        if(select_item_id == "119")
        {
            P_health_total = P_health_total + 100;
            P_health_now = P_health_now + 100;
            i_armor_cnt++;
            armor_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "126")
        {
            P_health_total = P_health_total + 100;
            P_health_now = P_health_now + 100;
            s_armor_cnt++;
            armor_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "133")
        {
            P_health_total = P_health_total + 100;
            P_health_now = P_health_now + 100;
            l_armor_cnt++;
            armor_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "111")
        {
            f_book_cnt++;
            book_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "118")
        {
            i_book_cnt++;
            book_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "125")
        {
            s_book_cnt++;
            book_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "132")
        {
            l_book_cnt++;
            book_cnt++;
            P_Mpower = P_Mpower + 200;
        }

        if(select_item_id == "114")
        {
            f_staff_cnt++;
            staff_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "121")
        {
            i_staff_cnt++;
            staff_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "128")
        {
            s_staff_cnt++;
            staff_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "135")
        {
            l_staff_cnt++;
            staff_cnt++;
            P_Mpower = P_Mpower + 500;
        }

        if(select_item_id == "101")
        {
            utilCoin_cnt++;
        }



        if(select_item_id == "103")
        {
            P_health_now = P_health_now + 500;
            P_health_total = P_health_total + 500;
            health_cnt++;
        }

        if(select_item_id == "102")
        {
            leaf4_cnt++;
        }

        if(select_item_id == "104")
        {
            fire_armor_cool = fire_armor_cool * 0.85f;
            fire_book_cool = fire_book_cool * 0.85f;
            fire_staff_cool = fire_staff_cool * 0.85f;
            ice_armor_cool = ice_armor_cool * 0.85f;
            ice_book_cool = ice_book_cool * 0.85f;
            ice_staff_cool = ice_staff_cool * 0.85f;
            stone_armor_cool = stone_armor_cool * 0.85f;
            stone_book_cool = stone_book_cool * 0.85f;
            stone_staff_cool = stone_staff_cool * 0.85f;
            light_armor_cool = light_armor_cool * 0.85f;
            light_book_cool = light_book_cool * 0.85f;
            light_staff_cool = light_staff_cool * 0.85f;
            health_cool = health_cool * 0.85f;
            ice_hat_cool = ice_hat_cool * 0.85f;
            
        }

        if(select_item_id == "106")
        {
            PM.moveSpeed = PM.moveSpeed * 1.15f;
        }
        
        if(select_item_id == "107")
        {
            monster_exp = monster_exp * 2;
        }

        if(select_item_id == "136")
        {
            quiver_cnt = quiver_cnt + 2;
        }

        if(select_item_id == "137")
        {
            windforce_cnt++;
        }

        if(select_item_id == "138")
        {
            babyforce_cnt++;
        }


        /*
        if(select_item_id == "898")
        {
            P_Mpower = P_Mpower + 1000;
            fire_armor_cool = fire_armor_cool * 0.85f;
            fire_book_cool = fire_book_cool * 0.85f;
            fire_staff_cool = fire_staff_cool * 0.85f;
            ice_armor_cool = ice_armor_cool * 0.85f;
            ice_book_cool = ice_book_cool * 0.85f;
            ice_staff_cool = ice_staff_cool * 0.85f;
            stone_armor_cool = stone_armor_cool * 0.85f;
            stone_book_cool = stone_book_cool * 0.85f;
            stone_staff_cool = stone_staff_cool * 0.85f;
            light_armor_cool = light_armor_cool * 0.85f;
            light_book_cool = light_book_cool * 0.85f;
            light_staff_cool = light_staff_cool * 0.85f;
            health_cool = health_cool  * 0.85f;
            P_health_now = P_health_now + 200;
            P_health_total = P_health_total + 200;
            ice_hat_cnt++;
            hat_cnt++;
            ice_hat_cool = ice_hat_cool * 0.85f;
        }
        */

        useItemID.Add(select_item_id);
        useItemName.Add(select_item_name);

        deleteSetSlot();
        setNewSetSlot();

        Panel_ItemSelect.SetActive(false);
        Time.timeScale = 1;


    }

    public void deleteSetSlot()
    {
        for(int i = 0; i < 8; i++)
        {
            setslot[i].item_id = null;
            setslot[i].item_name = null;
            setslot[i].itemImage.sprite = null;
        }
    }

    public void setNewSetSlot()
    {

        fire_cnt = 0;
        ice_cnt = 0;
        stone_cnt = 0;
        lightning_cnt = 0;

        for(int i = 0; i < useItemID.Count; i++)
        {
            setslot[i].item_id = useItemID[i];
            setslot[i].item_name = useItemName[i];
            setslot[i].itemImage.sprite = Resources.Load<Sprite>("items/" + useItemName[i]);
            setslot[i].itemImage.preserveAspect = true;

            if(useItemID[i] == "108" || useItemID[i] == "109" || useItemID[i] == "110" || useItemID[i] == "111" || useItemID[i] == "112" || useItemID[i] == "113" || useItemID[i] == "114" )
            {
                fire_cnt++;
                ice_cnt--;
            } else if(useItemID[i] == "115" || useItemID[i] == "116" || useItemID[i] == "117" || useItemID[i] == "118" || useItemID[i] == "119" || useItemID[i] == "120" || useItemID[i] == "121" )
            {
                fire_cnt--;
                ice_cnt++;
            } else if(useItemID[i] == "122" || useItemID[i] == "123" || useItemID[i] == "124" || useItemID[i] == "125" || useItemID[i] == "126" || useItemID[i] == "127" || useItemID[i] == "128" )
            {
                stone_cnt++;
                lightning_cnt--;
            } else if(useItemID[i] == "129" || useItemID[i] == "130" || useItemID[i] == "131" || useItemID[i] == "132" || useItemID[i] == "133" || useItemID[i] == "134" || useItemID[i] == "135" )
            {
                stone_cnt--;
                lightning_cnt++;
            } else if (useItemID[i] == "898"|| useItemID[i]=="897"|| useItemID[i]=="896")
            {
                fire_cnt++;
                ice_cnt++;
                stone_cnt++;
                lightning_cnt++;
            } 
            
        }

            fire_time = fire_cnt + stone_cnt + lightning_cnt;
            ice_time = ice_cnt + stone_cnt + lightning_cnt;
            stone_time = fire_cnt + stone_cnt + ice_cnt;
            lightning_time = fire_cnt + ice_cnt + lightning_cnt;


    }

    public Vector3 getCirclePosition(string monster)
    {
        float radius = 0;

        if(monster == "Goblin")
        {
            radius = 3f;
        } else if(monster == "GoblinKing")
        {
            radius = 5f;
        } else if(monster == "Beholder")
        {
            radius = 5f;
        } else if (monster == "Ooze")
        {
            radius = 6f;
        } 

        Vector3 PlayerPosition = Player.position;

        float a = PlayerPosition.x;
        float b = PlayerPosition.y;

        float x = UnityEngine.Random.Range(-radius + a, radius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - a, 2));
        y_b *= UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;

        Vector3 randomPosition = new Vector3(x, y, 0);
 
        return randomPosition;

    }

    public void getPlayerStats()
    {
        PlayFabClientAPI.GetUserData( new GetUserDataRequest() {PlayFabId = User_ID}
                        , (result) => {
                            P_health_total = int.Parse(result.Data["HEALTH"].Value);
                            P_health_now = int.Parse(result.Data["HEALTH"].Value);
                            P_power = int.Parse(result.Data["POWER"].Value);

                            getPlayerStats_chk = true;

                        }
                        , (error) => print("error"));

    }
    
    public void ChaosArmorSpawn()
    {
            if(c_armor_cnt >= 1 && Time.time > fire_armor_fire)
            {
                Instantiate(ChaosArmor, Player.position, Quaternion.identity);
                c_armor_cnt++;
                fire_armor_fire = Time.time + fire_armor_cool;
                SM.PlaySE("armor");
            }
    }
    public void FireArmorSpawn()
    {
            if(f_armor_cnt >= 1 && Time.time > fire_armor_fire)
            {
                Instantiate(FireArmor, Player.position, Quaternion.identity);
                f_armor_cnt++;
                fire_armor_fire = Time.time + fire_armor_cool;
                SM.PlaySE("armor");
            }
    }

    public void IceArmorSpawn()
    {
            if(i_armor_cnt >= 1 && Time.time > ice_armor_fire)
            {
                Instantiate(IceArmor, Player.position, Quaternion.identity);
                i_armor_cnt++;
                ice_armor_fire = Time.time + ice_armor_cool;
                SM.PlaySE("armor");
            }
            

    }
    public void StoneArmorSpawn()
    {
            if(s_armor_cnt >= 1 && Time.time > stone_armor_fire)
            {
                Instantiate(StoneArmor, Player.position, Quaternion.identity);
                s_armor_cnt++;
                stone_armor_fire = Time.time + stone_armor_cool;
                SM.PlaySE("armor");
            }
            

    }
    public void LightArmorSpawn()
    {
            if(l_armor_cnt >= 1 && Time.time > light_armor_fire)
            {
                Instantiate(LightningArmor, Player.position, Quaternion.identity);
                l_armor_cnt++;
                light_armor_fire = Time.time + light_armor_cool;
                SM.PlaySE("armor");
            }
            
    }    

    public void FireBookSpawn()
    {
            if(f_book_cnt >= 1 && Time.time > fire_book_fire)
            {
                Instantiate(FireBook, Player.position, Quaternion.identity);
                Instantiate(FireBookObj, Player.position, Quaternion.identity);
                f_book_cnt++;
                fire_book_fire = Time.time + fire_book_cool;
                SM.PlaySE("book");
            }
            

    }

    public void IceBookSpawn()
    {
            if(i_book_cnt >= 1 && Time.time > ice_book_fire)
            {
                Instantiate(IceBook, Player.position, Quaternion.identity);
                Instantiate(IceBookObj, Player.position, Quaternion.identity);
                i_book_cnt++;
                ice_book_fire = Time.time + ice_book_cool;
                SM.PlaySE("book");
            }
            

    }

    public void StoneBookSpawn()
    {
            if(s_book_cnt >= 1 && Time.time > stone_book_fire)
            {
                Instantiate(StoneBook, Player.position, Quaternion.identity);
                Instantiate(StoneBookObj, Player.position, Quaternion.identity);
                s_book_cnt++;
                stone_book_fire = Time.time + stone_book_cool;
                SM.PlaySE("book");
            }
            
            
    }

    public void LightBookSpawn()
    {
            if(l_book_cnt >= 1 && Time.time > light_book_fire)
            {
                Instantiate(LightningBook, Player.position, Quaternion.identity);
                Instantiate(LightningBookObj, Player.position, Quaternion.identity);
                l_book_cnt++;
                light_book_fire = Time.time + light_book_cool;
                SM.PlaySE("book");
            }
            
    }

    public void FireStaffSpawn()
    {
        if(f_staff_cnt >= 1 && Time.time > fire_staff_fire)
        {
            Instantiate(FireStaff, Player.position, Quaternion.identity);
            Instantiate(FireStaffObj, Player.position, Quaternion.identity);
            f_staff_cnt++;
            fire_staff_fire = Time.time + fire_staff_cool;
            SM.PlaySE("staff");
        }
        

    }

    public void IceStaffSpawn()
    {

        if(i_staff_cnt >= 1 && Time.time > ice_staff_fire)
        {
            Instantiate(IceStaff, Player.position, Quaternion.identity);
            Instantiate(IceStaffObj, Player.position, Quaternion.identity);
            i_staff_cnt++;
            ice_staff_fire = Time.time + ice_staff_cool;
            SM.PlaySE("staff");
        }
        

    }

    public void IceHatSpawn()
    {

        if(ice_hat_cnt >= 1 && Time.time > ice_hat_fire)
        {
            Instantiate(IceHat, Player.position, Quaternion.identity);
            Instantiate(IceStaffObj, Player.position, Quaternion.identity);
            ice_hat_fire = Time.time + ice_hat_cool;
            SM.PlaySE("staff");
        }
        

    }

    public void StoneStaffSpawn()
    {
        if(s_staff_cnt >= 1 && Time.time > stone_staff_fire)
        {
            Instantiate(StoneStaff, Player.position, Quaternion.identity);
            Instantiate(StoneStaffObj, Player.position, Quaternion.identity);
            s_staff_cnt++;
            stone_staff_fire = Time.time + stone_staff_cool;
            SM.PlaySE("staff");
        }
        
    }

    public void LightStaffSpawn()
    {
            if(l_staff_cnt >= 1 && Time.time > light_staff_fire)
            {
                Instantiate(LightningStaff, Player.position, Quaternion.identity);
                Instantiate(LightningStaffObj, Player.position, Quaternion.identity);
                l_staff_cnt++;
                light_staff_fire = Time.time + light_staff_cool;
                SM.PlaySE("staff");
            }
            
    }

    public void left4Spawn()
    {

        if(leaf4_cnt > 0 && Time.time > leaf4_P_fire)
        {
            P_power_tmp = P_power;
            P_Mpower_tmp = P_Mpower;

            meat_cnt_tmp = meat_cnt;
            whisky_cnt_tmp = whisky_cnt;

            P_health_now_tmp = P_health_now;
            P_health_total_tmp = P_health_total;

            sword_cnt_tmp = sword_cnt;
            bow_cnt_tmp = bow_cnt;
            minion_cnt_tmp = minion_cnt;
            book_cnt_tmp = book_cnt;
            armor_cnt_tmp = armor_cnt;
            orb_cnt_tmp = orb_cnt;
            staff_cnt_tmp = staff_cnt;
            hat_cnt_tmp = hat_cnt;
            health_cnt_tmp = health_cnt;

            int rand_no = UnityEngine.Random.Range(1,100);
            leaf4_flag = rand_no % 3;
            if(leaf4_flag == 0)
            {
                P_power = P_power * 2 * leaf4_cnt;
                PM.PowerUp_Leaf.Play();
            } else if (leaf4_flag == 1)
            {
                P_Mpower = P_Mpower * 2 * leaf4_cnt;
                PM.MPowerUp_Leaf.Play();
            } else
            {
                P_health_now = P_health_now * 2 * leaf4_cnt;
                P_health_total = P_health_total * 2 * leaf4_cnt;
                PM.HPUp_Leaf.Play();
            }
            leaf4_P_fire = Time.time + leaf4_P_cool;
            leaf4_M_fire = Time.time + leaf4_M_cool; 
            Instantiate(Leaf4, Player.position, Quaternion.identity);
            
        }

        if(leaf4_cnt > 0 && Time.time > leaf4_M_fire)
        {
            PM.PowerUp_Leaf.Stop();
            PM.MPowerUp_Leaf.Stop();
            PM.HPUp_Leaf.Stop();
            if(leaf4_flag == 0)
            {
                P_power = P_power_tmp + ((P_power_tmp * (sword_cnt - sword_cnt_tmp + bow_cnt - bow_cnt_tmp) * 20) / 100)
                                      + 10 * (meat_cnt - meat_cnt_tmp);
            } else if (leaf4_flag == 1)
            {
                P_Mpower = P_Mpower_tmp + ((book_cnt + minion_cnt + armor_cnt - book_cnt_tmp - minion_cnt_tmp - armor_cnt_tmp) * 200) 
                                        + ((staff_cnt + orb_cnt - orb_cnt_tmp- staff_cnt_tmp) * 500) 
                                        + ((hat_cnt - hat_cnt_tmp ) * 1000) 
                                        + (whisky_cnt - whisky_cnt_tmp) * 10;
            } else
            {
                P_health_now = P_health_now_tmp + ((health_cnt - health_cnt_tmp) * 500) 
                                                + ((armor_cnt - armor_cnt_tmp) * 100) 
                                                + ((hat_cnt - hat_cnt_tmp) * 200);
                P_health_total = P_health_total_tmp + ((health_cnt - health_cnt_tmp) * 500) 
                                                    + ((armor_cnt - armor_cnt_tmp) * 100) 
                                                    + ((hat_cnt - hat_cnt_tmp) * 200);
            }
            leaf4_M_fire = Time.time + 10; 
        }
    }

    public void RestoreHealth()
    {
        if(health_cnt >= 1 && Time.time > health_fire && P_health_now < P_health_total)
        {
            P_health_now = P_health_now + (100 * health_cnt);
            health_fire = Time.time + health_cool;
        }
    }    

    public void getHpBar()
    {
        hpbar.value = (float)P_health_now / (float)P_health_total;
    }

    public void getExpBar()
    {
        if(P_lvl >= 8)
        {
            expbar.value = 1f;
            txt_Expinfo.text = "MAX / MAX";
            return;
        }
        expbar.value = (float)P_exp / (float)P_Nexp[P_lvl];
        txt_Expinfo.text = P_exp + " / " + P_Nexp[P_lvl];
    }

    public void EndGameChk()
    {
        if(P_health_now < 1 || superswordbar.value == 0)
        {
            Time.timeScale = 0;

            int item_chk = 0;
            
            if(useItemID.Count == 8)
            {
                for(int i = 0 ; i < 8; i++)
                {
                    if(useItemID[i] == setItemID[i])
                    {
                        item_chk++;
                    }
                }

                if(item_chk == 8)
                {
                    get_point = get_point + 50000;
                }

            }

            if(boss_death_chk == true)
            {
                txt_gameResult.text = "승리!\n하지만 슈퍼소드는\n힘을 잃었습니다.";
            } else
            {
                txt_gameResult.text = "패배..\n슈퍼소드의 힘을\n다시 모아보세요.";
            }
            
            txt_getMoney.text = get_money.ToString();
            txt_getPoint.text = get_point.ToString();
            Panel_EndInfo.SetActive(true);
        }

    }

    public void onClickEndGameConfirm()
    {

        txt_Loading.text = "0%";
        Panel_Loading.SetActive(true);

        if(get_money > 0)
        {
            PlayFabClientAPI.AddUserVirtualCurrency(new PlayFab.ClientModels.AddUserVirtualCurrencyRequest() {
                    VirtualCurrency = "GD", Amount = (int)get_money}
                                , (result) => {
                                    
                                    setUserRank();
                                }
                                
                                , (error) => print("fail"));
        }

        
        
    }

    public void setUserRank()
    {

        var request = new UpdatePlayerStatisticsRequest {Statistics = new List<StatisticUpdate> {new StatisticUpdate {StatisticName = "HighScore", Value = get_point}}};

        PlayFabClientAPI.UpdatePlayerStatistics(request
                                , (result) => {
                                    
                                    Time.timeScale = 1;
                                    Panel_Loading.SetActive(false);
                                    SceneManager.LoadScene("LobbyScene");
                                }
                                , (error) => print("fail"));

    }

    public void onClickPauseBtn()
    {
        Time.timeScale = 0;
        Panel_Pause.SetActive(true);
    }

    public void onClickConnectGame()
    {
        Time.timeScale = 1;
        Panel_Pause.SetActive(false);
    }

    public void onClickGameQuit()
    {
        Time.timeScale = 1;
        Panel_Pause.SetActive(false);
        SceneManager.LoadScene("LobbyScene");
    }

    public void onClickRestart()
    {
        Time.timeScale = 1;
        Panel_Pause.SetActive(false);
        SceneManager.LoadScene("GamePlayScene");

    }

    public void CloseLoading()
    {
        Panel_Loading.SetActive(false);
    }

    //Panel_Option
    public void onClickOptionBtn()
    {
        InitializeVolumn();
        Panel_Option.SetActive(true);
    }

    public void onClickOptionConfirmBtn()
    {
        setVolumn();
        getVolumn();
        Panel_Option.SetActive(false);
    }

    public void getVolumn()
    {
        SM.background.volume = PlayerPrefs.GetFloat("BGM");
        for(int i = 0; i<SM.sfxPlayer.Length; i++)
        {
            SM.sfxPlayer[i].volume = PlayerPrefs.GetFloat("Effect");
        }

    }
    public void InitializeVolumn()
    {
        slider_BGM.value = PlayerPrefs.GetFloat("BGM"); 
        slider_Effect.value = PlayerPrefs.GetFloat("Effect");
    }
    public void setVolumn()
    {
        PlayerPrefs.SetFloat("BGM", slider_BGM.value); 
        PlayerPrefs.SetFloat("Effect", slider_Effect.value);
    }

    public void SuperSword()
    {
        if(supersword_flag == false)
        {
            supersword_flag = true;
            P_power = P_power * 150 / 100;
            P_Mpower = P_Mpower * 150 / 100;
            P_health_now = P_health_now * 150 / 100;
            P_health_total = P_health_total * 150 / 100;
            Instantiate(Supersword,Player.position, Quaternion.identity);
            PM.SuperSword1.Play();
            PM.SuperSword2.Play();
        }
    }





}

