using System.Collections;
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
        AreaMapPanel.Instance.OnShow(DataManager.mDistrictDict[gc.nowCheckingDistrictID].BigMapX, DataManager.mDistrictDict[gc.nowCheckingDistrictID].BigMapY);
        UIManager.Instance.InitPanel(UIPanelType.DistrictMain);
        UIManager.Instance.InitPanel(UIPanelType.Building);
        UIManager.Instance.InitPanel(UIPanelType.Hero);
        UIManager.Instance.InitPanel(UIPanelType.Build);
        UIManager.Instance.InitPanel(UIPanelType.PlayMain);
        PlayMainPanel.Instance.OnShow();
        UIManager.Instance.InitPanel(UIPanelType.DistrictMap);
        DistrictMapPanel.Instance.nowDistrict = -1;
        DistrictMapPanel.Instance.OnShow();
        DistrictMapPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.Message);
        MessagePanel.Instance.OnShow(0, 26);
        UIManager.Instance.InitPanel(UIPanelType.ItemListAndInfo);
        UIManager.Instance.InitPanel(UIPanelType.SkillListAndInfo);
        UIManager.Instance.InitPanel(UIPanelType.AdventureMain);
        AdventureMainPanel.Instance.OnShow(64, 5000);
        AdventureMainPanel.Instance.OnHide();
        UIManager.Instance.InitPanel(UIPanelType.AdventureTeam);
        UIManager.Instance.InitPanel(UIPanelType.BuildingSelect);
        UIManager.Instance.InitPanel(UIPanelType.HeroSelect);
        UIManager.Instance.InitPanel(UIPanelType.Market);
        UIManager.Instance.InitPanel(UIPanelType.SupplyAndDemand);
        UIManager.Instance.InitPanel(UIPanelType.Technology);
        UIManager.Instance.InitPanel(UIPanelType.Transfer);
        for (byte i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (gc.adventureTeamList[i].action == AdventureAction.Fight)
            {
                
                gc.AdventureEventHappen(i);
            }
        }
        Time.timeScale = gc.timeFlowSpeed;
        PlayMainPanel.Instance.UpdateTimeButtonState();
        InvokeRepeating("TimeFlow", 0, 0.05f );
        InvokeRepeating("SupplyAndDemandChangeRegular", 10f, 10f );
        // InvokeRepeating("CustomerCome", 3f, 3f);
        InvokeRepeating("TravellerCome", 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
           gc.skillDic.Add(gc.skillIndex , gc.GenerateSkillByRandom((short)Random.Range(0,DataManager.mSkillDict.Count),1));
            gc.skillIndex++;
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
                    BuildingPanel.Instance.UpdateOutputInfoPart(gc.buildingDic[BuildingPanel.Instance.nowCheckingBuildingID]);
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
            if (gc.standardTime == gc.executeEventList[0].endTime)
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
                         isSuccess = gc.DistrictItemOrSkillAdd(districtID, buildingID);
                        //Debug.Log(isSuccess);
                        gc.executeEventList.RemoveAt(0);
                        if (isSuccess)
                        {                         
                            gc.CreateProduceItemEvent(buildingID);
                        }
                        else//制作失败，停止继续生产
                        {
                            gc.buildingDic[buildingID].isOpen = false;
                        }
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

                        /*战斗事件*/
                        gc.AdventureEventHappen(teamID);
                        gc.executeEventList.RemoveAt(0);

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
            if (gc.districtDic[i].isOpen)
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

    public void OpenDistrictMain()
    {
        if (DistrictMainPanel.Instance.isShow)
        {
            DistrictMainPanel.Instance.OnHide();
        }
        else
        {
            DistrictMainPanel.Instance.OnShow(gc.districtDic[gc.nowCheckingDistrictID], 64, -88);
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
            BuildPanel.Instance.OnShow(0, -432);
        }  
    }

    

    public void OpenHeroSelect()
    {
        if (HeroSelectPanel.Instance.isShow)
        {
            HeroSelectPanel.Instance.OnHide();
        }
        else
        {
            HeroSelectPanel.Instance.OnShow("", gc.nowCheckingDistrictID,-1,1, 64, -88);
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
            ItemListAndInfoPanel.Instance.OnShow(gc.nowCheckingDistrictID, 64, -88,1);
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
            SkillListAndInfoPanel.Instance.OnShow(gc.nowCheckingDistrictID, null,64, -88);
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
            AdventureMainPanel.Instance.OnShow( 64, -88);
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
            MarketPanel.Instance.OnShow(gc.nowCheckingDistrictID, ItemTypeBig.None, ItemTypeSmall.None, 64, -88);
        }
    }

    public void OpenTransfer()
    {
        if (TransferPanel.Instance.isShow)
        {
            TransferPanel.Instance.OnHide();
        }
        else
        {
            TransferPanel.Instance.OnShow(gc.nowCheckingDistrictID);
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

    public void GameSave()
    {
        gc.Save();
        MessagePanel.Instance.AddMessage("游戏已保存");
    }
}
