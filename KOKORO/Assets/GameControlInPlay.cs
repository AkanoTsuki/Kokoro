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


        //InvokeRepeating("TimeFlow", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimeFlow()
    {
        gc.standardTime++;
        gc.timeS++;
        if (gc.timeS > 10) { gc.timeHour++; gc.timeS = 0; Debug.Log("年" + gc.timeYear + " 月" + gc.timeMonth + " 日" + gc.timeDay + " 时" + gc.timeHour); }
        if (gc.timeHour >= 24) { gc.timeDay++; gc.timeHour = 0; }
        if (gc.timeDay > 30) { gc.timeMonth++; gc.timeDay = 1; }
        if (gc.timeMonth > 12) { gc.timeYear++; gc.timeMonth = 1; }

        


    }


    public void OpenDistrictMain()
    {

        DistrictMainPanel.Instance.OnShow(gc.districtDic[gc.nowCheckingDistrictID],84,-88);
    }

    public void OpenBuild()
    {

        BuildPanel.Instance.OnShow( 688, -88);
    }
}
