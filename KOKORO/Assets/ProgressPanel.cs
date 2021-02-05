using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressPanel : BasePanel
{
    public static ProgressPanel Instance;
    GameControl gc;

    //UI组件


    //对象池
    List<GameObject> progressGoPool = new List<GameObject>();
    Dictionary<int, GameObject> progressGoDic = new Dictionary<int, GameObject>();//在用

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isShow)
        {

        }
    }

    void Init()
    {
        for (int i = 0; i < gc.executeEventList.Count; i++)
        {
            if (gc.executeEventList[i].type == ExecuteEventType.Build || gc.executeEventList[i].type == ExecuteEventType.BuildingUpgrade || gc.executeEventList[i].type == ExecuteEventType.TechnologyResearch)
            {
                GameObject go;
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Progress")) as GameObject;
                go.transform.SetParent(transform);
                progressGoDic.Add(gc.executeEventList[i].id, go);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i* -44f);

                UpdateBarSingle(gc.executeEventList[i].id);
            }
        }
    }

    public override void OnShow()
    {
        gameObject.SetActive(true);
        isShow = true;
    }

    public void UpdateBar()
    {
        foreach (KeyValuePair<int, GameObject> kvp in progressGoDic)         
        {
            UpdateBarSingle(kvp.Key);
        }
    }



    public void UpdateBarSingle(int eventID)
    {
        int needTime = gc.executeEventDic[eventID].endTime - gc.executeEventDic[eventID].startTime;
        int nowTime = gc.standardTime - gc.executeEventDic[eventID].startTime;
        progressGoDic[eventID].transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2((float)nowTime / needTime * 160f, 16f);

        switch (gc.executeEventDic[eventID].type)
        {
            case ExecuteEventType.Build:
                progressGoDic[eventID].transform.GetChild(3).GetComponent<Text>().text = "<color=#F3DE60>" + gc.districtDic[gc.executeEventDic[eventID].value[0][0]].name + "</color>的<color=#F89629>" + gc.buildingDic[gc.executeEventDic[eventID].value[1][0]].name + "</color>建筑中";
                break;
            case ExecuteEventType.BuildingUpgrade:
                progressGoDic[eventID].transform.GetChild(3).GetComponent<Text>().text = "<color=#F3DE60>" + gc.districtDic[gc.executeEventDic[eventID].value[0][0]].name + "</color>的<color=#F89629>" + gc.buildingDic[gc.executeEventDic[eventID].value[1][0]].name + "</color>升级中";
                break;
            case ExecuteEventType.TechnologyResearch:
                progressGoDic[eventID].transform.GetChild(3).GetComponent<Text>().text = "<color=#F3DE60>" + gc.districtDic[gc.executeEventDic[eventID].value[0][0]].name + "</color>执行<color=#78D2FF>" + DataManager.mTechnologyDict[gc.executeEventDic[eventID].value[1][0]].Name + "</color>研究中";
                break;
        }

        int nowNeedTime = needTime - nowTime;
        int h = nowNeedTime / 10;
        int days = h / 24;
        int hours = h % 24;
        progressGoDic[eventID].transform.GetChild(3).GetComponent<Text>().text = days + "D " + hours + "H";
    }


    public override void OnHide()
    {
        gameObject.SetActive(false);
        isShow = false;
    }
}
