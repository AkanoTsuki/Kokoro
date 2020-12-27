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
        UIManager.Instance.InitPanel(UIPanelType.Message);
        MessagePanel.Instance.OnShow(0, 26);
        UIManager.Instance.InitPanel(UIPanelType.ItemListAndInfo);
        UIManager.Instance.InitPanel(UIPanelType.SkillListAndInfo);
        UIManager.Instance.InitPanel(UIPanelType.AdventureMain);
        UIManager.Instance.InitPanel(UIPanelType.BuildingSelect);
        UIManager.Instance.InitPanel(UIPanelType.HeroSelect);
        UIManager.Instance.InitPanel(UIPanelType.PlayMain);
        PlayMainPanel.Instance.OnShow();


        InvokeRepeating("TimeFlow", 0, 0.05f / gc.timeFlowSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
           gc.skillDic.Add(gc.skillIndex , gc.GenerateSkillByRandom((short)Random.Range(0,DataManager.mSkillDict.Count)));
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
    }
    public void TimeFlow()
    {
        
        gc.timeS++;
        PlayMainPanel.Instance.UpdateTimeBar();
        if (gc.timeS >= 10) {

            PlayMainPanel.Instance.UpdateMonthDayHour();

            if (BuildingPanel.Instance.isShow)
            {
                if (gc.buildingDic[BuildingPanel.Instance.nowCheckingBuildingID].produceEquipNow != -1)
                {
                    BuildingPanel.Instance.UpdateOutputInfoPart(gc.buildingDic[BuildingPanel.Instance.nowCheckingBuildingID]);
                }
            }

            gc.timeHour++; gc.timeS = 0; 
        }
        if (gc.timeHour >= 24) { gc.timeDay++; gc.timeHour = 0;gc.timeWeek++;if (gc.timeWeek > 7) { gc.timeWeek = 1; } }
        if (gc.timeDay > 30) { gc.timeMonth++; gc.timeDay = 1; PlayMainPanel.Instance.UpdateYearSeason(); }
        if (gc.timeMonth > 12) { gc.timeYear++; gc.timeMonth = 1; }

        

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
                        Debug.Log("  gc.standardTime=" + gc.standardTime + "   资源生产" + (StuffType)gc.executeEventList[0].value[2]+"*"+ gc.executeEventList[0].value[3]);
                         districtID = (short)gc.executeEventList[0].value[0];
                         buildingID = gc.executeEventList[0].value[1];
                        //if (gc.buildingDic[buildingID].produceEquipNow == -1)
                        //{
                        //    MessagePanel.Instance.AddMessage("接到停工命令，生产停止");
                        //    break;
                        //}
                         isSuccess = gc.DistrictResourceAdd(districtID, buildingID,(StuffType)gc.executeEventList[0].value[2], gc.executeEventList[0].value[3]);
                        gc.executeEventList.RemoveAt(0);
                        if (isSuccess)
                        {
                            gc.CreateProduceResourceEvent(buildingID);
                        }
                        else
                        {
                            gc.buildingDic[buildingID].produceEquipNow = -1;
                        }
                        break;
                    case ExecuteEventType.ProduceItem:
                       // Debug.Log("  gc.standardTime=" + gc.standardTime + "   制作模板" + gc.executeEventList[0].value[2]);
                         districtID = (short)gc.executeEventList[0].value[0];
                         buildingID = gc.executeEventList[0].value[1];
                         itemId = gc.executeEventList[0].value[2];
                         isSuccess = gc.DistrictItemAdd(districtID, buildingID);
                        //Debug.Log(isSuccess);
                        gc.executeEventList.RemoveAt(0);
                        if (isSuccess)
                        {                         
                            gc.CreateProduceItemEvent(buildingID);
                        }
                        else//制作失败，停止继续生产
                        {
                            gc.buildingDic[buildingID].produceEquipNow = -1;
                        }
                        break;
                    case ExecuteEventType.Build:
                        districtID = (short)gc.executeEventList[0].value[0];
                        buildingID = gc.executeEventList[0].value[1];
                        gc.BuildDone((short)buildingID);
                        gc.executeEventList.RemoveAt(0);

                        break;
                    case ExecuteEventType.Adventure:
                        byte teamID = (byte)gc.executeEventList[0].value[0];

                        /*战斗事件*/
                        gc.AdventureFight(teamID);
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
        CancelInvoke("TimeFlow");
        PlayMainPanel.Instance.UpdateTimeButtonState();
    }
    public void TimePlay()
    {
        gc.timeFlowSpeed = 1;
        CancelInvoke("TimeFlow");
        InvokeRepeating("TimeFlow", 0, 0.05f /gc.timeFlowSpeed);
        PlayMainPanel.Instance.UpdateTimeButtonState();
    }
    public void TimeFast()
    {
        gc.timeFlowSpeed = 2;
        CancelInvoke("TimeFlow");
        InvokeRepeating("TimeFlow", 0, 0.05f / gc.timeFlowSpeed);
        PlayMainPanel.Instance.UpdateTimeButtonState();
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
            BuildPanel.Instance.OnShow(64, -88);
        }  
    }

    public void OpenBuildingSelect()
    {
        if (BuildingSelectPanel.Instance.isShow)
        {
            BuildingSelectPanel.Instance.OnHide();
        }
        else
        {
            BuildingSelectPanel.Instance.OnShow(64, -88);
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

    public void GameSave()
    {
        gc.Save();
        MessagePanel.Instance.AddMessage("游戏已保存");
    }
}
