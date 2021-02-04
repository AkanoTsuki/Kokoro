﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlInPlay : MonoBehaviour
{
    GameControl gc;

    public List<LogObject> needShowMessageList = new List<LogObject> { };
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();

        UIManager.Instance.SceneUIInit();
        UIManager.Instance.InitPanel(UIPanelType.AreaMap);
        AreaMapPanel.Instance.OnShow(-288, 818);
        UIManager.Instance.InitPanel(UIPanelType.DistrictMain);
        UIManager.Instance.InitPanel(UIPanelType.Building);
        BuildingPanel.Instance.SetAnchoredPosition(0, -436);
        BuildingPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.Hero);
        HeroPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.Build);
        BuildPanel.Instance.SetAnchoredPosition(0, -432);
        BuildPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.Message);
        MessagePanel.Instance.OnShow(-2, 2);
        UIManager.Instance.InitPanel(UIPanelType.PlayMain);
        PlayMainPanel.Instance.OnShow();
        UIManager.Instance.InitPanel(UIPanelType.DistrictMap);
        DistrictMapPanel.Instance.nowDistrict = -1;
        DistrictMapPanel.Instance.InitCustomer();
        //DistrictMapPanel.Instance.OnHide();
        //DistrictMapPanel.Instance.OnHide();

        UIManager.Instance.InitPanel(UIPanelType.ItemListAndInfo);
        ItemListAndInfoPanel.Instance.OnHide();
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

        //UIManager.Instance.InitPanel(UIPanelType.BuildingSelect);
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

        for (byte i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (gc.adventureTeamList[i].state == AdventureState.Doing)
            {
                if (gc.adventureTeamList[i].action == AdventureAction.Fight)
                {
                    //gc.executeEventList.RemoveAt(0);
                    //gc.AdventureEventHappen(i);
                    StartCoroutine(gc.AdventureFight(i));
                }
                else
                {
                    gc.adventureTeamList[i].action = AdventureAction.Walk;
                }
            }
               
        }
        Time.timeScale = gc.timeFlowSpeed;
        PlayMainPanel.Instance.UpdateTimeButtonState();
        InvokeRepeating("TimeFlow", 0, 0.05f );
        InvokeRepeating("SupplyAndDemandChangeRegular", 10f, 10f );
        InvokeRepeating("CustomerCome", 3f, 3f);
        InvokeRepeating("TravellerCome", 3f, 3f);
        InvokeRepeating("AdventureTravellerCome", 10f, 10f);
    }

    // Update is called once per frame
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
            gc.CustomerCome(gc.nowCheckingDistrictID);
        
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
            {
                Debug.Log(kvp.Value.name + " inDistrict=" + kvp.Value.inDistrict);
                Debug.Log(" gc.districtDic[ kvp.Value.inDistrict].heroList.Count=" + gc.districtDic[ kvp.Value.inDistrict].heroList.Count);
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
            }

            gc.timeHour = 0;
            gc.timeWeek++;
            if (gc.timeWeek > 7) 
            { 
                gc.timeWeek = 1;
            } 
        }
       

        

        gc.standardTime++;
        if(gc.executeEventList.Count>0)
        {
            if (gc.standardTime >= gc.executeEventList[0].endTime)
            {
                short districtID;
                 int buildingID, itemId;
                bool isSuccess;
                switch (gc.executeEventList[0].type)
                {
                    case ExecuteEventType.ProduceResource:
                        //Debug.Log("  gc.standardTime=" + gc.standardTime + "   资源生产" + (StuffType)gc.executeEventList[0].value[2]+"*"+ gc.executeEventList[0].value[3]);
                         districtID = (short)gc.executeEventList[0].value[0][0];
                         buildingID = gc.executeEventList[0].value[1][0];
                        //if (gc.buildingDic[buildingID].produceEquipNow == -1)
                        //{
                        //    MessagePanel.Instance.AddMessage("接到停工命令，生产停止");
                        //    break;
                        //}
                        List<StuffType> stuffTypes = new List<StuffType>();
                        List<int> values = new List<int>();
                        for (int i = 0; i < gc.executeEventList[0].value[2].Count; i++)
                        {
                            stuffTypes.Add((StuffType)gc.executeEventList[0].value[2][i]);
                            values.Add(gc.executeEventList[0].value[3][i]);
                        }

                         isSuccess = gc.DistrictResourceAdd(districtID, buildingID, stuffTypes, values);
                        gc.executeEventList.RemoveAt(0);
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
                         itemId = gc.executeEventList[0].value[2][0];
                        // isSuccess = gc.DistrictItemOrSkillAdd(districtID, buildingID);
                        //Debug.Log(isSuccess);
                     
                        gc.DistrictItemOrSkillAdd(districtID, buildingID);
                        gc.executeEventList.RemoveAt(0);
                        gc.CreateProduceItemEvent(buildingID);
                        //if (isSuccess)
                        //{                         
                        //    gc.CreateProduceItemEvent(buildingID);
                        //}
                        //else//制作失败，停止继续生产
                        //{
                        //    gc.buildingDic[buildingID].isOpen = false;
                        //}
                        break;
                    case ExecuteEventType.Build:
                        districtID = (short)gc.executeEventList[0].value[0][0];
                        buildingID = gc.executeEventList[0].value[1][0];
                        gc.BuildDone((short)buildingID);
                        gc.executeEventList.RemoveAt(0);

                        break;
                    case ExecuteEventType.BuildingUpgrade:
                        districtID = (short)gc.executeEventList[0].value[0][0];
                        buildingID = gc.executeEventList[0].value[1][0];
                        gc.BuildingUpgradeDone((short)buildingID);
                        gc.executeEventList.RemoveAt(0);

                        break;
                    case ExecuteEventType.Adventure:
                        byte teamID = (byte)gc.executeEventList[0].value[0][0];

                        /*事件*/
                        gc.executeEventList.RemoveAt(0);
                        gc.AdventureEventHappen(teamID);
                     

                        break;
                    case ExecuteEventType.BuildingSale:

                        buildingID = gc.executeEventList[0].value[1][0];

                        gc.BuildingSale(buildingID);
                        gc.executeEventList.RemoveAt(0);
                        gc.CreateBuildingSaleEvent(buildingID);
                        break;
                    case ExecuteEventType.TechnologyResearch:

                        int technologyID = gc.executeEventList[0].value[1][0];

                        gc.TechnologyResearchDone(technologyID);
                        gc.executeEventList.RemoveAt(0);
                        break;
                    default: break;
                }
            }
        }
        

    }

    public void TimePause()
    {
        gc.timeFlowSpeed = 0;
        Time.timeScale = gc.timeFlowSpeed;
        //CancelInvoke("TimeFlow");
        PlayMainPanel.Instance.UpdateTimeButtonState();
    }
    public void TimePlay()
    {
        gc.timeFlowSpeed = 1;
        Time.timeScale = gc.timeFlowSpeed;
        //CancelInvoke("TimeFlow");
        //InvokeRepeating("TimeFlow", 0, 0.05f /gc.timeFlowSpeed);
        PlayMainPanel.Instance.UpdateTimeButtonState();
    }
    public void TimeFast()
    {
        gc.timeFlowSpeed = 2;
        Time.timeScale = gc.timeFlowSpeed;
        //CancelInvoke("TimeFlow");
        //InvokeRepeating("TimeFlow", 0, 0.05f / gc.timeFlowSpeed);
        PlayMainPanel.Instance.UpdateTimeButtonState();
    }
    //定时事件
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
                        gc.CustomerCome(i);
                    }
                 
                }
            }
        }      
    }
    void TravellerCome()
    {
        if (Random.Range(0, 3) > 0)
        {
            gc.CreateTravellerByRandom();
        }  
    }

    void AdventureTravellerCome()
    {
        if (Random.Range(0, 3) > 0)
        {
            gc.CreateAdventureTravellerByRandom();
        }
    }

    public void OpenDistrictMain()
    {
        if (DistrictMainPanel.Instance.isShow)
        {
            DistrictMainPanel.Instance.OnHide();
        }
        else
        {
            DistrictMainPanel.Instance.OnShow(gc.districtDic[gc.nowCheckingDistrictID], 76, -104);
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
        Debug.Log("gc.nowCheckingDistrictID=" + gc.nowCheckingDistrictID + " gc.itemDic.Coun=" + gc.itemDic.Count);
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
        Debug.Log("gc.nowCheckingDistrictID=" + gc.nowCheckingDistrictID + " gc.itemDic.Coun=" + gc.itemDic.Count);
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

    public void GameSave()
    {
        gc.Save();
        MessagePanel.Instance.AddMessage("游戏已保存");
    }
}
