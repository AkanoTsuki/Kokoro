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
        UIManager.Instance.InitPanel(UIPanelType.BuildingSelect);
        UIManager.Instance.InitPanel(UIPanelType.HeroSelect);
        UIManager.Instance.InitPanel(UIPanelType.PlayMain);
        PlayMainPanel.Instance.OnShow();


        InvokeRepeating("TimeFlow", 0.05f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gc.Save();
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
        if (gc.timeS >= 10) { PlayMainPanel.Instance.UpdateTimeText();  gc.timeHour++; gc.timeS = 0;  }
        if (gc.timeHour >= 24) { gc.timeDay++; gc.timeHour = 0;gc.timeWeek++;if (gc.timeWeek > 7) { gc.timeWeek = 1; } }
        if (gc.timeDay > 30) { gc.timeMonth++; gc.timeDay = 1; }
        if (gc.timeMonth > 12) { gc.timeYear++; gc.timeMonth = 1; }

        

        gc.standardTime++;
        if(gc.executeEventList.Count>0)
        {
            if (gc.standardTime == gc.executeEventList[0].endTime)
            {
                int districtID, buildingID, itemId;
                switch (gc.executeEventList[0].type)
                {
                    case ExecuteEventType.ProduceResource:
                        Debug.Log("  gc.standardTime=" + gc.standardTime + "   资源生产" + (StuffType)gc.executeEventList[0].value[2]+"*"+ (StuffType)gc.executeEventList[0].value[3]);
                         districtID = gc.executeEventList[0].value[0];
                         buildingID = gc.executeEventList[0].value[1];
                        gc.DistrictResourceAdd(districtID, (StuffType)gc.executeEventList[0].value[2], gc.executeEventList[0].value[3]);
                        gc.executeEventList.RemoveAt(0);
                        gc.CreateProduceResourceEvent(buildingID);
                        break;
                    case ExecuteEventType.ProduceItem:
                       // Debug.Log("  gc.standardTime=" + gc.standardTime + "   制作模板" + gc.executeEventList[0].value[2]);
                         districtID = gc.executeEventList[0].value[0];
                         buildingID = gc.executeEventList[0].value[1];
                         itemId = gc.executeEventList[0].value[2];
                        bool isSuccess = gc.DistrictItemAdd(districtID, buildingID);
                        //Debug.Log(isSuccess);
                        gc.executeEventList.RemoveAt(0);
                        if (isSuccess)//制作失败，停止继续生产
                        {                         
                            gc.CreateProduceItemEvent(buildingID);
                        }
                        else
                        {
                            gc.buildingDic[buildingID].produceEquipNow = -1;
                        }
                        break;
                    case ExecuteEventType.Build:
                        districtID = gc.executeEventList[0].value[0];
                        buildingID = gc.executeEventList[0].value[1];
                        gc.BuildDone((short)buildingID);
                        gc.executeEventList.RemoveAt(0);
                        break;
                    default: break;
                }
            }
        }
        

    }


    

    public void OpenDistrictMain()
    {

        DistrictMainPanel.Instance.OnShow(gc.districtDic[gc.nowCheckingDistrictID],84,-88);
    }

    public void OpenBuild()
    {
        BuildPanel.Instance.OnShow(84, -88);
    }

    public void OpenBuildingSelect()
    {
        BuildingSelectPanel.Instance.OnShow(84, -88);
    }

    public void OpenHeroSelect()
    {
        HeroSelectPanel.Instance.OnShow("",84, -88);
    }

    public void OpenItemListAndInfo()
    {
        List<ItemObject> itemObjects = new List<ItemObject>();

        foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
        {
            if (kvp.Value.districtID == gc.nowCheckingDistrictID)
            {
                itemObjects.Add(kvp.Value);
            }
        }

        ItemListAndInfoPanel.Instance.OnShow(itemObjects, 84, -88);
    }
}
