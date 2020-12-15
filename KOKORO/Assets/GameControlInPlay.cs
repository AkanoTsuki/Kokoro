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
        UIManager.Instance.InitPanel(UIPanelType.PlayMain);
        PlayMainPanel.Instance.OnShow();


        InvokeRepeating("TimeFlow", 0.05f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimeFlow()
    {
        
        gc.timeS++;
        if (gc.timeS > 10) { gc.timeHour++; gc.timeS = 0; PlayMainPanel.Instance.UpdateDateInfo(); Debug.Log("年" + gc.timeYear + " 月" + gc.timeMonth + " 日" + gc.timeDay + " 时" + gc.timeHour); }
        if (gc.timeHour >= 24) { gc.timeDay++; gc.timeHour = 0;gc.timeWeek++;if (gc.timeWeek > 7) { gc.timeWeek = 1; } }
        if (gc.timeDay > 30) { gc.timeMonth++; gc.timeDay = 1; }
        if (gc.timeMonth > 12) { gc.timeYear++; gc.timeMonth = 1; }

        

        gc.standardTime++;
        if(gc.executeEventList.Count>0)
        {
            if (gc.standardTime == gc.executeEventList[0].endTime)
            {
                switch (gc.executeEventList[0].type)
                {
                    case ExecuteEventType.ProduceResource:
                        Debug.Log("  gc.standardTime=" + gc.standardTime + "   资源生产" + gc.standardTime);
                        int districtID = gc.executeEventList[0].value1;
                        int buildingID = gc.executeEventList[0].value2;
                        gc.ResourceAdd(districtID, (StuffType)gc.executeEventList[0].value3, gc.executeEventList[0].value4);
                        gc.executeEventList.RemoveAt(0);
                        gc.CreateProduceEvent(buildingID);
                        break;
                    case ExecuteEventType.ProduceItem:
                        Debug.Log("  gc.standardTime=" + gc.standardTime + "   制作" + gc.standardTime);
                        int itemId = gc.executeEventList[0].value3;
                        gc.GenerateItemByRandom(itemId);
                        gc.executeEventList.RemoveAt(0);
                        StartProduceItem(itemId);
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
        BuildPanel.Instance.OnShow( 688, -88);
    }

    public void StartProduceItem(int itemId)
    {
        gc.ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.ProduceItem, gc.standardTime, gc.standardTime + 50, -1, -1, itemId,-1,-1));

    }
   
}
