using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
public class ProgressPanel : BasePanel
{
    public static ProgressPanel Instance;
    GameControl gc;

    //UI组件


    //对象池
    List<GameObject> progressGoPool = new List<GameObject>();//空闲
    Dictionary<int, GameObject> progressGoDic = new Dictionary<int, GameObject>();//在用

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if(isShow)
        {
            UpdateBar();
        }
    }

    public override void OnShow()
    {
        gameObject.SetActive(true);
        isShow = true;
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
        isShow = false;
    }

    void Init()
    {
        for (int i = 0; i < gc.executeEventList.Count; i++)
        {
            if (gc.executeEventList[i].type == ExecuteEventType.Build || gc.executeEventList[i].type == ExecuteEventType.BuildingUpgrade || gc.executeEventList[i].type == ExecuteEventType.TechnologyResearch)
            {
                AddSingle(gc.executeEventList[i].id);
            }
        }
    }

    public void AddSingle(int eventID)
    {
        GameObject go;
        if (progressGoPool.Count > 0)
        {
            go = progressGoPool[0];
            progressGoPool.RemoveAt(0);
            go.transform.GetComponent<RectTransform>().localScale = Vector2.one;
        }
        else
        {
            go = Instantiate(Resources.Load("Prefab/UILabel/Label_Progress")) as GameObject;
            go.transform.SetParent(transform);
        }
        go.name = eventID.ToString();
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,  progressGoDic.Count * -48f);
        progressGoDic.Add(eventID, go);
        UpdateSingleBasic(eventID);
        UpdateSingleBar(eventID);
    }

    public IEnumerator DeleteSingle(int eventID)
    {
       // progressGoDic[eventID].transform.DOLocalJump(progressGoDic[eventID].transform.localPosition, 2f, 3, 2.5f);
        progressGoDic[eventID].transform.DOScale(Vector2.one * 1.05f, 0.5f).SetLoops(6, LoopType.Yoyo);

        yield return new WaitForSeconds(3.2f);
        progressGoDic[eventID].transform.DOComplete();
        progressGoDic[eventID].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        progressGoPool.Add(progressGoDic[eventID]);
        progressGoDic.Remove(eventID);
        SetPosition();
    }


   
    public void UpdateBar()
    {
        foreach (KeyValuePair<int, GameObject> kvp in progressGoDic)         
        {
            UpdateSingleBar(kvp.Key);
        }
    }

    void SetPosition()
    {
        Dictionary<int, GameObject> dic1Asc = progressGoDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
        int index = 0;

        foreach (KeyValuePair<int, GameObject> kvp in dic1Asc)
        {
            //kvp.Value.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, index * -48f);
            kvp.Value.GetComponent<RectTransform>().DOLocalMove(new Vector2(-68f, index * -48f), 0.5f);
            index++;
        }
    }

    public void UpdateSingleBasic(int eventID)
    {
        switch (gc.executeEventDic[eventID].type)
        {
            case ExecuteEventType.Build:
                progressGoDic[eventID].transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon940");
                progressGoDic[eventID].transform.GetChild(3).GetComponent<Text>().text = "<color=#F3DE60>" + gc.districtDic[gc.executeEventDic[eventID].value[0][0]].name + "</color>\n<color=#F89629>" + gc.buildingDic[gc.executeEventDic[eventID].value[1][0]].name + "</color>建筑中";
                break;
            case ExecuteEventType.BuildingUpgrade:
                progressGoDic[eventID].transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon400");
                progressGoDic[eventID].transform.GetChild(3).GetComponent<Text>().text = "<color=#F3DE60>" + gc.districtDic[gc.executeEventDic[eventID].value[0][0]].name + "</color>\n<color=#F89629>" + gc.buildingDic[gc.executeEventDic[eventID].value[1][0]].name + "</color>升级中";
                break;
            case ExecuteEventType.TechnologyResearch:
                progressGoDic[eventID].transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon951");
                progressGoDic[eventID].transform.GetChild(3).GetComponent<Text>().text = "<color=#F3DE60>" + gc.districtDic[gc.executeEventDic[eventID].value[0][0]].name + "</color>\n<color=#78D2FF>" + DataManager.mTechnologyDict[gc.executeEventDic[eventID].value[1][0]].Name + "</color>研究中";
                break;
        }
    }


    public void UpdateSingleBar(int eventID)
    {
        if (gc.executeEventDic.ContainsKey(eventID))
        {
            int needTime = gc.executeEventDic[eventID].endTime - gc.executeEventDic[eventID].startTime;
            int nowTime = gc.standardTime - gc.executeEventDic[eventID].startTime;
            progressGoDic[eventID].transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2((float)nowTime / needTime * 128f, 16f);



            int nowNeedTime = needTime - nowTime;
            int h = nowNeedTime / 10;
            int days = h / 24;
            int hours = h % 24;
            if (nowNeedTime > 0)
            {
                progressGoDic[eventID].transform.GetChild(4).GetComponent<Text>().text = (days != 0 ? (days + "D ") : "") + +hours + "H";
            }
            else
            {
                progressGoDic[eventID].transform.GetChild(4).GetComponent<Text>().text = "<color=#63FF4C>已完成</color>";
            }
        }
        else
        {
            progressGoDic[eventID].transform.GetChild(4).GetComponent<Text>().text = "<color=#63FF4C>已完成</color>";
        }
    }


   
}
