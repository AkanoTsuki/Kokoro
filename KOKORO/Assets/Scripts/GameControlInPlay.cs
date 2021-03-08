using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControlInPlay : MonoBehaviour
{
    GameControl gc;

    //进度条
    public GameObject progressBgGo;
    public Slider progressSlider;
    //进度条进度显示文字  
    public Text ProgressSliderText;

    //运行变量
    public List<LogObject> needShowMessageList = new List<LogObject> { };
    public byte tempTimeSpeed = 0;

    void Start()
    {
       // progressSlider.value = 0.81f;
       // ProgressSliderText.text = "81%";

        gc = GameObject.Find("GameManager").GetComponent<GameControl>();

        UIManager.Instance.SceneUIInit();
        UIManager.Instance.InitPanel(UIPanelType.AreaMap);
        AreaMapPanel.Instance.OnShow(-288, 818);

       // progressBgGo.transform.SetAsLastSibling();

        UIManager.Instance.InitPanel(UIPanelType.DistrictMain);
        DistrictMainPanel.Instance.SetAnchoredPosition(0, -436);
        DistrictMainPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.Building);
        BuildingPanel.Instance.SetAnchoredPosition(0, -436);
        BuildingPanel.Instance.OnHide();
  

        UIManager.Instance.InitPanel(UIPanelType.Build);
        BuildPanel.Instance.SetAnchoredPosition(0, -432);
        BuildPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.Message);
        MessagePanel.Instance.SetAnchoredPosition(-2, 2);

        UIManager.Instance.InitPanel(UIPanelType.DistrictMap);
        DistrictMapPanel.Instance.nowDistrict = -1;
        DistrictMapPanel.Instance.InitCustomer();
        DistrictMapPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.ItemListAndInfo);
        ItemListAndInfoPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.ConsumableListAndInfo);
        ConsumableListAndInfoPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.SkillListAndInfo);
        SkillListAndInfoPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.AdventureTeam);
        AdventureTeamPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.AdventureMain);
        AdventureMainPanel.Instance.OnShow(0);
        AdventureMainPanel.Instance.SetAnchoredPosition(76, -104);
        AdventureMainPanel.Instance.OnHide();
       
        UIManager.Instance.InitPanel(UIPanelType.AdventureSend);
        AdventureSendPanel.Instance.SetAnchoredPosition(76, -104);
        AdventureSendPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.Hero);
        HeroPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.HeroSelect);
        HeroSelectPanel.Instance.OnHide();



        UIManager.Instance.InitPanel(UIPanelType.Market);
        MarketPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.SupplyAndDemand);
        SupplyAndDemandPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.Technology);
        TechnologyPanel.Instance.SetAnchoredPosition(76, -104);
        TechnologyPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.Transfer);
        TransferPanel.Instance.SetAnchoredPosition(76, -104);
        TransferPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.Diplomacy);
        DiplomacyPanel.Instance.SetAnchoredPosition(76, -104);
        DiplomacyPanel.Instance.OnHide();

       
        UIManager.Instance.InitPanel(UIPanelType.Progress);
        ProgressPanel.Instance.SetAnchoredPosition(-8, -104);
        ProgressPanel.Instance.OnShow();

        UIManager.Instance.InitPanel(UIPanelType.PlayMain);
        PlayMainPanel.Instance.OnShow();

        UIManager.Instance.InitPanel(UIPanelType.SystemSet);
        SystemSetPanel.Instance.SetAnchoredPosition(0, 0);
        SystemSetPanel.Instance.OnHide();

       // progressSlider.value = 0.9f;
       // ProgressSliderText.text = "90%";

        for (byte i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (gc.adventureTeamList[i].state == AdventureState.Doing)
            {
                if (gc.adventureTeamList[i].action == AdventureAction.Fight)
                {

                    StartCoroutine(gc.AdventureFight(i));
                }
                else
                {
                    gc.adventureTeamList[i].action = AdventureAction.Walk;
                }
            }
               
        }
        Time.timeScale = gc.timeFlowSpeed;
        tempTimeSpeed = gc.timeFlowSpeed;
        PlayMainPanel.Instance.UpdateTimeButtonState();

        gc.SetVolumeMusic(gc.volumeMusic);
        gc.SetVolumeSound(gc.volumeSound);
        InvokeRepeating("TimeFlow", 0, 0.05f );
        InvokeRepeating("SupplyAndDemandChangeRegular", 10f, 10f );
        InvokeRepeating("CustomerCome", 3f, 3f);
        InvokeRepeating("TravellerCome", 3f, 3f);
        InvokeRepeating("AdventureTravellerCome", 10f, 10f);

        gc.CreateRecruiter(1); gc.CreateRecruiter(1); gc.CreateRecruiter(1);

        //progressSlider.value = 1f;
        // ProgressSliderText.text = "100%";
        // progressBgGo.SetActive(false);
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gc.skillDic.Add(gc.skillIndex, gc.GenerateSkillByRandom((short)Random.Range(0, DataManager.mSkillDict.Count), -1));
            gc.skillIndex++;
            gc.forceDic[0].rProductNow++;
            PlayMainPanel.Instance.UpdateButtonSkillNum();
            PlayMainPanel.Instance.UpdateInventoryNum();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("gc.executeEventList.Count=" + gc.executeEventList.Count);
            if (gc.executeEventList.Count > 0)
            {
                Debug.Log("gc.executeEventList[0]=" + gc.executeEventList[0].endTime+ " gc.standardTime=" + gc.standardTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //gc.CreateCustomer(gc.nowCheckingDistrictID);
            gc.districtDic[1].recruitList.Clear();
            gc.CreateRecruiter(1);
            gc.CreateRecruiter(1);
            gc.CreateRecruiter(1);
            gc.CreateRecruiter(1);
            gc.CreateRecruiter(1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
            {
                Debug.Log(kvp.Value.name + " inDistrict=" + kvp.Value.inDistrict);
                Debug.Log(" gc.districtDic[ kvp.Value.inDistrict].heroList.Count=" + gc.districtDic[ kvp.Value.inDistrict].heroList.Count);
            }

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            DEVELOPSaveAllBuilding();

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            gc.forceDic[0].gold += 50000;

            gc.ConsumableChange((short)Random.Range(0, DataManager.mConsumableDict.Count - 1), 5);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            gc.forceDic[0].rStuffWood += 10000;
            gc.forceDic[0].rStuffStone += 5000;
            gc.forceDic[0].rStuffMetal += 5000;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < gc.fightMenberObjectSS[0].Count; i++)
            {
                if (gc.fightMenberObjectSS[0][i].side == 0)
                {
                    gc.fightMenberObjectSS[0][i].haloStatus = true;
                    AdventureMainPanel.Instance.UpdateSceneRoleHaloSingle(0, gc.fightMenberObjectSS[0][i]);
                    break;
                }
            }
            
        }
    }
   
    public void TimeFlow()
    {
        
        gc.timeS++;
        PlayMainPanel.Instance.UpdateTimeBar();
        if (gc.timeS >= 10) {

            PlayMainPanel.Instance.UpdateMonthDayHour();

            if (BuildingPanel.Instance.isShow)
            {
                if (gc.buildingDic[BuildingPanel.Instance.nowCheckingBuildingID].isOpen)
                {
                    if (BuildingPanel.Instance.IsShowOutputInfoPart)
                    {
                        BuildingPanel.Instance.UpdateOutputInfoPart(gc.buildingDic[BuildingPanel.Instance.nowCheckingBuildingID]);
                    }
                }
            }
            if (DistrictMapPanel.Instance.isShow)
            {
                DistrictMapPanel.Instance.ChangeSkyColor();
            }

            gc.timeHour++; gc.timeS = 0; 
        }
        if (gc.timeHour >= 24) { 
            gc.timeDay++;
            if (gc.timeDay > 30) 
            { 
                gc.timeMonth++; 
                if (gc.timeMonth > 12) 
                {
                    gc.timeYear++;
                    gc.timeMonth = 1; 
                } 
                gc.timeDay = 1;
                PlayMainPanel.Instance.UpdateYearSeason(); 
                gc.CreateSalesRecord(gc.timeYear, gc.timeMonth);
                gc.CreateCustomerRecord(gc.timeYear, gc.timeMonth);
                gc.DistrictBuildingExpenseAll();
                gc.DistrictCreateFiscalAll();
                gc.PayHeroSalary();
            }

            gc.timeHour = 0;
            gc.timeWeek++;
            if (gc.timeWeek > 7) 
            { 
                gc.timeWeek = 1;

                gc.DistrictGetTaxPeopleAll();
                gc.DistrictPeopleFoodExpenseAll();
            } 
        }
       

        

        gc.standardTime++;
        if(gc.executeEventList.Count>0)
        {
            if (gc.standardTime >= gc.executeEventList[0].endTime)
            {
                short districtID;
                int buildingID;
                bool isSuccess;
                switch (gc.executeEventList[0].type)
                {
                    case ExecuteEventType.ProduceResource:
                        //Debug.Log("  gc.standardTime=" + gc.standardTime + "   资源生产" + (StuffType)gc.executeEventList[0].value[2]+"*"+ gc.executeEventList[0].value[3]);
                         districtID = (short)gc.executeEventList[0].value[0][0];
                         buildingID = gc.executeEventList[0].value[1][0];
                        List<StuffType> stuffTypes = new List<StuffType>();
                        List<int> values = new List<int>();
                        for (int i = 0; i < gc.executeEventList[0].value[2].Count; i++)
                        {
                            stuffTypes.Add((StuffType)gc.executeEventList[0].value[2][i]);
                            values.Add(gc.executeEventList[0].value[3][i]);
                        }

                         isSuccess = gc.DistrictResourceAdd(districtID, buildingID, stuffTypes, values);

                        gc.ExecuteEventDelete(0);
                        if (isSuccess)
                        {
                            gc.CreateProduceResourceEvent(buildingID);
                        }
                        else
                        {
                            gc.buildingDic[buildingID].isOpen = false;
                        }
                        break;
                    case ExecuteEventType.ProduceItem:
                       // Debug.Log("  gc.standardTime=" + gc.standardTime + "   制作模板" + gc.executeEventList[0].value[2]);
                         districtID = (short)gc.executeEventList[0].value[0][0];
                         buildingID = gc.executeEventList[0].value[1][0];

                        gc.DistrictItemOrSkillAdd(districtID, buildingID);
                        gc.ExecuteEventDelete(0);
                        gc.CreateProduceItemEvent(buildingID);

                        break;
                    case ExecuteEventType.Build:

                        buildingID = gc.executeEventList[0].value[1][0];
                        gc.BuildDone((short)buildingID);
                        gc.ExecuteEventDelete(0);

                        break;
                    case ExecuteEventType.BuildingUpgrade:
                        buildingID = gc.executeEventList[0].value[1][0];
                        gc.BuildingUpgradeDone((short)buildingID);
                        gc.ExecuteEventDelete(0);

                        break;
                    case ExecuteEventType.Adventure:
                        byte teamID = (byte)gc.executeEventList[0].value[0][0];

                        /*事件*/
                        gc.ExecuteEventDelete(0);
                        gc.AdventureEventHappen(teamID);
                     

                        break;
                    case ExecuteEventType.BuildingSale:

                        buildingID = gc.executeEventList[0].value[1][0];

                        gc.BuildingSale(buildingID);
                        gc.ExecuteEventDelete(0);
                        gc.CreateBuildingSaleEvent(buildingID);
                        break;
                    case ExecuteEventType.TechnologyResearch:

                        int technologyID = gc.executeEventList[0].value[1][0];

                        gc.TechnologyResearchDone(technologyID);
                        gc.ExecuteEventDelete(0);
                        break;
                    default: break;
                }
            }
        }
        

    }

    #region 【方法组】游戏速度控制
    public void TimePause()
    {
        gc.timeFlowSpeed = 0;
        Time.timeScale = gc.timeFlowSpeed;
        PlayMainPanel.Instance.UpdateTimeButtonState();
    }

    public void TimePlay()
    {
        gc.timeFlowSpeed = 1;
        Time.timeScale = gc.timeFlowSpeed;
        PlayMainPanel.Instance.UpdateTimeButtonState();
    }

    public void TimeFast()
    {
        gc.timeFlowSpeed = 2;
        Time.timeScale = gc.timeFlowSpeed;
        PlayMainPanel.Instance.UpdateTimeButtonState();
    }
    #endregion

    #region 【方法组】定时事件
    //供需关系变化
    void SupplyAndDemandChangeRegular()
    {
        for (short i = 0; i < DataManager.mDistrictDict.Count; i++)
        {
            if (gc.districtDic[i].isOpen)
            {
                gc.SupplyAndDemandChangeRegular(i);
            }
        
        }
        if (SupplyAndDemandPanel.Instance.isShow)
        {
            SupplyAndDemandPanel.Instance.UpdateAllInfo(gc.nowCheckingDistrictID);
        }
    }

    //访客顾客生成
    void CustomerCome()
    {
        for (short i = 0; i < DataManager.mDistrictDict.Count; i++)
        {
            if (gc.districtDic[i].force==0|| gc.districtDic[i].id==gc.nowCheckingDistrictID)
            {
                if (gc.timeHour >= 6 && gc.timeHour < 18)
                {
                    if (Random.Range(0, 5) > 0)
                    {
                        gc.CreateCustomer(i);
                    }
                 
                }
            }
        }      
    }

    //大地图旅人生成
    void TravellerCome()
    {
        if (Random.Range(0, 3) > 0)
        {
            gc.CreateTravellerByRandom();
        }  
    }

    //大地图冒险队生成
    void AdventureTravellerCome()
    {
        if (Random.Range(0, 3) > 0)
        {
            gc.CreateAdventureTravellerByRandom();
        }
    }
    #endregion

    #region 【方法组】面板操作
    public void OpenDistrictMain()
    {
        if (DistrictMainPanel.Instance.isShow)
        {
            DistrictMainPanel.Instance.OnHide();
        }
        else
        {
            DistrictMainPanel.Instance.OnShow(gc.districtDic[gc.nowCheckingDistrictID]);
        }     
    }

    public void OpenBuild()
    {
        if (BuildPanel.Instance.isShow)
        {
            BuildPanel.Instance.OnHide();
        }
        else
        {
            BuildPanel.Instance.OnShow();
        }  
    }

    public void OpenHeroSelect(short districtID)
    {
        if (HeroSelectPanel.Instance.isShow)
        {
            HeroSelectPanel.Instance.OnHide();
        }
        else
        {
            HeroSelectPanel.Instance.OnShow("", districtID, -1,1, 76, -104);
        }

    
    }

    public void OpenItemListAndInfo()
    {
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnHide();
        }
        else
        {
            ItemListAndInfoPanel.Instance.OnShow(gc.nowCheckingDistrictID, 76, -104, 1);
        }
    }



    public void OpenSkillListAndInfo()
    {
        if (SkillListAndInfoPanel.Instance.isShow)
        {
            SkillListAndInfoPanel.Instance.OnHide();
        }
        else
        {
            SkillListAndInfoPanel.Instance.OnShow(gc.nowCheckingDistrictID, null, 76, -104);
        }
    }

 


    public void OpenAdventureMain()
    {
        if (AdventureMainPanel.Instance.isShow)
        {
            AdventureMainPanel.Instance.OnHide();
        }
        else
        {
            AdventureMainPanel.Instance.OnShow(0);
        }
    }

    public void OpenMarket()
    {
        if (MarketPanel.Instance.isShow)
        {
            MarketPanel.Instance.OnHide();
        }
        else
        {
            MarketPanel.Instance.OnShow(gc.nowCheckingDistrictID, ItemTypeBig.None, ItemTypeSmall.None, 76, -104);
        }
    }

    public void OpenTransfer(string tpye)
    {
        if (TransferPanel.Instance.isShow)
        {
            TransferPanel.Instance.OnHide();
        }
        else
        {
            TransferPanel.Instance.OnShow(tpye,gc.nowCheckingDistrictID);
        }
    }

    public void OpenAdventureSend()
    {
        if (AdventureSendPanel.Instance.isShow)
        {
            AdventureSendPanel.Instance.OnHide();
        }
        else
        {
            AdventureSendPanel.Instance.OnShow("To",gc.nowCheckingDistrictID);
        }
    }

    public void OpenTechnology()
    {
        if (TechnologyPanel.Instance.isShow)
        {
            TechnologyPanel.Instance.OnHide();
        }
        else
        {
            TechnologyPanel.Instance.OnShow();
        }
    }

    public void OpenInventoryEquip()
    {
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnHide();
        }
        else
        {
            ItemListAndInfoPanel.Instance.OnShow(-1, 76, -104, 2);
        }
    }

    public void OpenInventorySkill()
    {
        if (SkillListAndInfoPanel.Instance.isShow)
        {
            SkillListAndInfoPanel.Instance.OnHide();
        }
        else
        {
            SkillListAndInfoPanel.Instance.OnShow(-1,null, 76, -104);
        }
    }

    public void OpenInventoryConsumable()
    {
        if (ConsumableListAndInfoPanel.Instance.isShow)
        {
            ConsumableListAndInfoPanel.Instance.OnHide();
        }
        else
        {
            ConsumableListAndInfoPanel.Instance.OnShow(76, -104);
        }
    }

    public void OpenDiplomacy()
    {
        if (DiplomacyPanel.Instance.isShow)
        {
            DiplomacyPanel.Instance.OnHide();
        }
        else
        {
            DiplomacyPanel.Instance.OnShow();
        }
    }
    #endregion

    //保存游戏
    public void GameSave()
    {
        gc.Save();
        MessagePanel.Instance.AddMessage("游戏已保存");
    }

    //开发阶段方法：全部城镇现有建筑存储，作为游戏开始初始化配置数据
    public void DEVELOPSaveAllBuilding()
    {
        int count1 = 0;
        int count2 = 0;

        string str1 = "\"DevelopGrid\": [";

        string str2 = "\"DevelopBuilding\": [";



        foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
        {
            string strgrid = "";
            string strnpc = "";
            for (int i = 0; i < kvp.Value.gridList.Count; i++)
            {
                strgrid += "\""+ kvp.Value.gridList[i] + "\",";

                str1 += "{" +
                "\"ID\":\"" + kvp.Value.gridList[i] + "\"," +
                    "\"DistrictID\":" + kvp.Value.districtID + "," +
                       "\"BuildingID\":" + kvp.Value.id + "" +
                "},";
                count1++;
            }
            strgrid = strgrid.Substring(0, strgrid.Length - 1);


            short buildingId = kvp.Value.prototypeID;
            List<string> npcPicList = new List<string>();
            Debug.Log("DataManager.mBuildingDict[buildingId].BgPic=" + DataManager.mBuildingDict[buildingId].BgPic);
            switch (DataManager.mBuildingDict[buildingId].BgPic)
            {
                case "DBG_HouseSmall_1":
                case "DBG_HouseSmall_2":
                case "DBG_HouseSmall_3":
                    for (int i = 0; i < 4; i++)
                    {
                        npcPicList.Add(DataManager.mNpcName[Random.Range(0, DataManager.mNpcName.Length - 1)]);
                    }
                    break;
                case "DBG_HouseMiddle_1":
                case "DBG_HouseMiddle_2":
                case "DBG_HouseMiddle_3":
                case "DBG_HouseBig_1":
                case "DBG_HouseBig_2":
                case "DBG_HouseBig_3":
                    for (int i = 0; i < 6; i++)
                    {
                        npcPicList.Add(DataManager.mNpcName[Random.Range(0, DataManager.mNpcName.Length - 1)]);
                    }
                    break;
                case "DBG_WheatField":
                case "DBG_VegetableField":
                case "DBG_Orchard":
                case "DBG_FlaxField":
                    for (int i = 0; i < 6; i++)
                    {
                        npcPicList.Add("npc_farmer1");
                    }
                    break;
                case "DBG_Lair":
                    npcPicList.Add("npc_animal_cow1");
                    npcPicList.Add("npc_animal_cow1");
                    npcPicList.Add("npc_animal_cow2");
                    for (int i = 3; i < 6; i++)
                    {
                        npcPicList.Add("npc_farmer1");
                    }
                    break;
                case "DBG_Fishpond":
                    for (int i = 0; i < 6; i++)
                    {
                        npcPicList.Add("npc_other1_17");
                    }
                    break;
                case "DBG_Outside":
                    for (int i = 0; i < 6; i++)
                    {
                        npcPicList.Add("npc_other1_01");
                    }
                    break;
                case "DBG_IronMine":
                case "DBG_Quarry":
                    for (int i = 0; i < 6; i++)
                    {
                        npcPicList.Add("npc_other2_04");
                    }
                    break;

                case "DBG_Base1":
                case "DBG_Base2":
                case "DBG_Base3":
                case "DBG_Base4":
                case "DBG_Base5":
                    npcPicList.Add("npc_knight" + gc.forceDic[gc.districtDic[kvp.Value.districtID].force].flagIndex);
                    npcPicList.Add("npc_knight" + gc.forceDic[gc.districtDic[kvp.Value.districtID].force].flagIndex);
                    break;
                case "DBG_WeaponShop":
                    npcPicList.Add("npc_other1_24");
                    for (int i = 0; i < 4; i++)
                    {
                        npcPicList.Add("npc_blacksmith");
                    }
                    break;
                case "DBG_ArmorShop":
                    npcPicList.Add("npc_other1_30");
                    break;
                case "DBG_JewelryShop":
                    npcPicList.Add("npc_other2_13");
                    break;
                case "DBG_Warehouse":
                    npcPicList.Add("npc_other2_10");
                    break;
                case "DBG_Arena":
                    npcPicList.Add("npc_other2_10");
                    npcPicList.Add("npc_other1_07");
                    npcPicList.Add("npc_other2_21");
                    npcPicList.Add("npc_other2_17");
                    npcPicList.Add("npc_other2_06");
                    npcPicList.Add("npc_other2_02");
                    npcPicList.Add("npc_other2_01");
                    break;
                case "DBG_Monastery":
                    npcPicList.Add("npc_other2_12");
                    npcPicList.Add("npc_other2_15");
                    for (int i = 0; i < 5; i++)
                    {
                        npcPicList.Add(DataManager.mNpcName[Random.Range(0, DataManager.mNpcName.Length - 1)]);
                    }
                    break;
                case "DBG_Inn":
                    npcPicList.Add("npc_other1_02");
                    npcPicList.Add("npc_other1_23");
                    break;
                case "DBG_ScrollShop":
                    npcPicList.Add("npc_other2_08");
                    break;
            }
            for (int i = 0; i < npcPicList.Count; i++)
            {
                strnpc += "\"" + npcPicList[i] + "\",";
            }
            Debug.Log("strnpc="+ strnpc);
            if (strnpc != "")
            {
                strnpc = strnpc.Substring(0, strnpc.Length - 1);
            }
          

            str2 += "{" +
                   "\"ID\":" + kvp.Value.id + "," +
                    "\"DistrictID\":" + kvp.Value.districtID + "," +
                    "\"PrototypeID\":" + kvp.Value.prototypeID + "," +
                     "\"PositionX\":" + kvp.Value.positionX + "," +
                      "\"PositionY\":" + kvp.Value.positionY + "," +
                       "\"Layer\":" + kvp.Value.layer + "," +
                        "\"NpcList\":[" + strnpc + "]" + "," +
                          "\"GridList\":[" + strgrid + "]" +
                   "},";
            count2++;
        }

        str1 = str1.Substring(0, str1.Length - 1);
        str1 += " ]";

        Debug.Log("涉及格子数："+count1);
        Debug.Log(str1);


        str2 = str2.Substring(0, str2.Length - 1);
        str2 += " ]";

        Debug.Log("涉及建筑数：" + count2);
        Debug.Log(str2);

        string str3 = "\"DevelopDistrict\": [";

        for (int i = 0; i < gc.districtDic.Length; i++)
        {
            string strbuilding = "";
            for (int j = 0; j < gc.districtDic[i].buildingList.Count; j++)
            {
                strbuilding += gc.districtDic[i].buildingList[j]+",";
            }

            if (strbuilding != "")
            {
                strbuilding = strbuilding.Substring(0, strbuilding.Length - 1);
            }
       



            str3 += "{" +
            "\"ID\":" + i + "," +
             "\"PeopleLimit\":" + gc.districtDic[i].peopleLimit + "," +
                        "\"BuildingList\":[" + strbuilding + "]," +
             "\"EWind\":" + gc.districtDic[i].eWind + "," +
              "\"EFire\":" + gc.districtDic[i].eFire + "," +
               "\"EWater\":" + gc.districtDic[i].eWater + "," +
                "\"EGround\":" + gc.districtDic[i].eGround + "," +
                   "\"ELight\":" + gc.districtDic[i].eLight + "," +
                       "\"EDark\":" + gc.districtDic[i].eDark + "," +
     "\"RProductLimit\":" + gc.districtDic[i].rProductLimit + "" +
            "},";
        }

        str3 = str3.Substring(0, str3.Length - 1);
        str3 += " ]";


        Debug.Log(str3);


        Debug.Log("{"+ str1+","+ str2+ "," + str3 +"}");
    }
}
