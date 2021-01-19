using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TechnologyPanel : BasePanel
{
    public static TechnologyPanel Instance;

    GameControl gc;

    public GameObject list_doneGo;
    public GameObject list_noneGo;
    public Image info_picImage;
    public Text info_nameText;
    public Text info_desText;

    public RectTransform selectBuildingBlockBgRt;
    public RectTransform selectBuildingBlockRt;
    public GameObject selectBuildingBlockListGo;
    public Text selectBuildingBlockDesText;
    public Button selectBuildingBlockConfirmBtn;
    public Button selectBuildingBlockCloseBtn;

    public Button doBtn;
    public Button closeBtn;


    List<GameObject> technologyGoPool = new List<GameObject>();
    List<GameObject> districtGoPool = new List<GameObject>();
    public int nowCheckingTechnology = -1;
    public int nowSelectDistrict = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        selectBuildingBlockCloseBtn.onClick.AddListener(delegate () { HideSelectBuildingBlock(); });
        closeBtn.onClick.AddListener(delegate () { OnHide(); });

    }


    public override void OnShow()
    {
        UpdateAllInfo();
        HideSelectBuildingBlock();
        SetAnchoredPosition(64, -88);
        isShow = true;

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }

    public void UpdateAllInfo()
    {
        UpdateList("done");
        UpdateList("none");
        ClearInfo();
    }

    public void UpdateList(string type)
    {
        List<TechnologyObject> technologyObjects = new List<TechnologyObject>();

        foreach (KeyValuePair<int, TechnologyObject> kvp in gc.technologyDic)
        {
            if (type == "done")
            {
                if (kvp.Value.stage== TechnologyStage.Done)
                {
                    technologyObjects.Add(kvp.Value);
                }
            }
            else if (type == "none")
            {
                if (kvp.Value.stage == TechnologyStage.Open|| kvp.Value.stage == TechnologyStage.Research)
                {
                    technologyObjects.Add(kvp.Value);
                }
            }
        }

        GameObject go;
        for (int i = 0; i < technologyObjects.Count; i++)
        {
            int technologyID = technologyObjects[i].id;
            if (i < technologyGoPool.Count)
            {
                go = technologyGoPool[i];
                technologyGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Technology")) as GameObject;
                if (type == "done")
                {
                    go.transform.SetParent(list_doneGo.transform);
                }
                else if (type == "none")
                {
                    go.transform.SetParent(list_noneGo.transform);
                }
                    technologyGoPool.Add(go);
            }
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f, i * -44f);
            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mTechnologyDict[technologyObjects[i].id].Pic);
            string str ="";
            switch (DataManager.mTechnologyDict[technologyID].Type)
            {
                case "Work": str += "<color=#F8961B>[生产类]</color> " + DataManager.mTechnologyDict[technologyObjects[i].id].Name + "\n"; break;
                case "Fight": str += "<color=#FF533B>[战斗类]</color>" + DataManager.mTechnologyDict[technologyObjects[i].id].Name + "\n"; break;
            }
            if (gc.technologyDic[technologyID].stage == TechnologyStage.Open)
            {
                str += "<i>可研究</i>";
            }
            else if (gc.technologyDic[technologyID].stage == TechnologyStage.Research)
            {
                int needTime = 0;
                int nowTime = 0;
                for (int j = 0; j < gc.executeEventList.Count; j++)
                {
                    if (gc.executeEventList[j].type == ExecuteEventType.TechnologyResearch && gc.executeEventList[j].value[1][0] == technologyID)
                    {
                        needTime = gc.executeEventList[j].endTime - gc.executeEventList[j].startTime;
                        nowTime = gc.standardTime - gc.executeEventList[j].startTime;
                        break;
                    }
                }

                str += "<i>研究中("+System.Math.Round(((float)nowTime/ needTime)*100,2) + "%)</i>";
            }
            go.transform.GetChild(1).GetComponent<Text>().text =str;

            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(delegate () { UpdateInfo(technologyID); });
        }
        for (int i = technologyObjects.Count; i < technologyGoPool.Count; i++)
        {
            technologyGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }

        if (type == "done")
        {
            list_doneGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(220f, Mathf.Max(400f, technologyObjects.Count * 44f));
        }
        else if (type == "none")
        {
            list_noneGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(220f, Mathf.Max(400f, technologyObjects.Count * 44f));
        }
    }

   public void UpdateInfo(int technologyID)
    {
        nowCheckingTechnology = technologyID;

        info_picImage.sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mTechnologyDict[technologyID].Pic);
        info_nameText.text = DataManager.mTechnologyDict[technologyID].Name;

        string str = "";
        switch (DataManager.mTechnologyDict[technologyID].Type)
        {
            case "Work": str += "<color=#F8961B>[生产类]</color>\n"; break;
            case "Fight": str += "<color=#FF533B>[战斗类]</color>\n"; break;
        }

        if (DataManager.mTechnologyDict[technologyID].ParentID.Count != 0)
        {
            str += "\n<color=#EFDDB1>前置研究</color>";
            for (int i = 0; i < DataManager.mTechnologyDict[technologyID].ParentID.Count; i++)
            {
                str += "\n  完成[" + DataManager.mTechnologyDict[DataManager.mTechnologyDict[technologyID].ParentID[i]].Name + "]";
            }
        }
        if (DataManager.mTechnologyDict[technologyID].NeedBuilding.Count!= 0)
        {
            str += "\n<color=#EFDDB1>需要设施</color>\n  ";
            for (int i = 0; i < DataManager.mTechnologyDict[technologyID].NeedBuilding.Count; i++)
            {
                str += DataManager.mBuildingDict[DataManager.mTechnologyDict[technologyID].NeedBuilding[i]].Name+" ";
            }

             
        }

        if (DataManager.mTechnologyDict[technologyID].NeedStuff.Count != 0 || DataManager.mTechnologyDict[technologyID].NeedGold != 0)
        {
            str += "\n<color=#EFDDB1>资源消耗</color>";

            string strStuff = "\n ";

            for (int i = 0; i < DataManager.mTechnologyDict[technologyID].NeedStuff.Count; i++)
            {
                switch (DataManager.mTechnologyDict[technologyID].NeedStuff[i])
                {
                    case StuffType.Wood: strStuff += " 木材*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Stone: strStuff += " 石料*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Metal: strStuff += " 金属*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Leather: strStuff += " 皮革*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Cloth: strStuff += " 布料*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Twine: strStuff += " 麻绳*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Bone: strStuff += " 骨块*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Wind: strStuff += " 风粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Fire: strStuff += " 火粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Water: strStuff += " 水粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Ground: strStuff += " 地粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Light: strStuff += " 光粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                    case StuffType.Dark: strStuff += " 暗粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
                }


            }
            if (DataManager.mTechnologyDict[technologyID].NeedGold != 0)
            {
                strStuff += " 金币*" + DataManager.mTechnologyDict[technologyID].NeedGold;
            }

            str += strStuff;
        }
        str += "\n<color=#EFDDB1>研究时间</color> " + DataManager.mTechnologyDict[technologyID].NeedTime + "天";
        str += "\n\n" + DataManager.mTechnologyDict[technologyID].Des;
        if (DataManager.mTechnologyDict[technologyID].ChildrenID.Count != 0)
        {
            str += "\n解锁";
            for (int i = 0; i < DataManager.mTechnologyDict[technologyID].ChildrenID.Count; i++)
            {
                str += "[" + DataManager.mTechnologyDict[DataManager.mTechnologyDict[technologyID].ChildrenID[i]].Name + "]";
            }
        }

        info_desText.text = str;

        if (gc.technologyDic[technologyID].stage== TechnologyStage.Open)
        {
            doBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            doBtn.onClick.RemoveAllListeners();
            doBtn.onClick.AddListener(delegate () { ShowSelectBuildingBlock(technologyID); });
        }
        else
        {
            doBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    void ClearInfo()
    {
        nowCheckingTechnology = -1;
        info_picImage.sprite = Resources.Load<Sprite>("Image/Empty");
        info_nameText.text = "";
        info_desText.text = "";
    }

    //TODO：信息显示待完善
    void ShowSelectBuildingBlock(int technologyID)
    {
        selectBuildingBlockBgRt.localScale = Vector2.one;

        List<DistrictObject> districtObjects = new List<DistrictObject>();
        for (int i = 0; i < gc.districtDic.Length; i++)
        {
            if (gc.districtDic[i].isOwn)
            {
                districtObjects.Add(gc.districtDic[i]);
            }
        }
        int districtID;
        GameObject go;
        for (int i = 0; i < districtObjects.Count; i++)
        {
            if (i < districtGoPool.Count)
            {
                go = districtGoPool[i];
                districtGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_CityButton")) as GameObject;
                go.transform.SetParent(selectBuildingBlockListGo.transform);
                districtGoPool.Add(go);
            }
            int row = i == 0 ? 0 : (i % 4);
            int col = i == 0 ? 0 : (i / 4);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2( row * 80f,  col * -50f);
             districtID = districtObjects[i].id;

            go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/AreaPic/"+DataManager.mDistrictDict[districtID].Pic);
            go.transform.GetChild(0).GetComponent<Text>().text = gc.districtDic[districtID].name;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(delegate () { nowSelectDistrict= districtID; ShowSelectBuildingBlock(technologyID); });
        }

        for (int i = districtObjects.Count; i < districtGoPool.Count; i++)
        {
            districtGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }

        selectBuildingBlockRt.sizeDelta = new Vector2(320f, Mathf.Max(180f, 130+ (districtObjects.Count / 4) * 50f));


        if (districtObjects.Count > 0)
        {
            if (nowSelectDistrict == -1)
            {
                nowSelectDistrict = districtObjects[0].id;
            }

            selectBuildingBlockDesText.text = OutputTechnologyNeedStr(nowSelectDistrict, technologyID);
            selectBuildingBlockConfirmBtn.onClick.RemoveAllListeners();
            selectBuildingBlockConfirmBtn.onClick.AddListener(delegate () { gc.CreateTechnologyResearchEvent(nowSelectDistrict, technologyID); HideSelectBuildingBlock(); });

        }
        else
        {
            selectBuildingBlockDesText.text = "无可研究的地区";
            selectBuildingBlockConfirmBtn.onClick.RemoveAllListeners();

        }





    }

    string OutputTechnologyNeedStr(int districtID, int technologyID)
    {
        string str = "<color=#EFDDB1>选中地区</color> "+DataManager.mDistrictDict[districtID].Name;
        if (DataManager.mTechnologyDict[technologyID].ParentID.Count != 0)
        {
            str += "\n<color=#EFDDB1>前置研究</color> ";
        }

        for (int i = 0; i < DataManager.mTechnologyDict[technologyID].ParentID.Count; i++)
        {
            if (gc.technologyDic[DataManager.mTechnologyDict[technologyID].ParentID[i]].stage != TechnologyStage.Done)
            {
                str += "<color=red>" + DataManager.mTechnologyDict[DataManager.mTechnologyDict[technologyID].ParentID[i]].Name + "</color> ";
            }
            else
            {
                str += DataManager.mTechnologyDict[DataManager.mTechnologyDict[technologyID].ParentID[i]].Name + " ";
            }
        }

        if (DataManager.mTechnologyDict[technologyID].NeedBuilding.Count != 0)
        {
            str += "\n<color=#EFDDB1>需要建筑</color> ";
            string strb = "";
            bool ok = false;
            for (int i = 0; i < DataManager.mTechnologyDict[technologyID].NeedBuilding.Count; i++)
            {
                strb += DataManager.mBuildingDict[DataManager.mTechnologyDict[technologyID].NeedBuilding[i]].Name + "/";
            }

            foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
            {
                if (DataManager.mTechnologyDict[technologyID].NeedBuilding.Contains(kvp.Value.prototypeID) && kvp.Value.districtID == districtID)
                {

                    ok = true;
                }
            }
            strb = strb.Substring(0, strb.Length - 1);
            if (!ok)
            {
                strb = "<color=red>" + strb + "</color> ";
            }

            str += strb;
        }

        if (DataManager.mTechnologyDict[technologyID].NeedStuff.Count != 0 || DataManager.mTechnologyDict[technologyID].NeedGold != 0)
        {
            str += "\n<color=#EFDDB1>资源消耗</color> ";
        }

        for (int i = 0; i < DataManager.mTechnologyDict[technologyID].NeedStuff.Count; i++)
        {
            switch (DataManager.mTechnologyDict[technologyID].NeedStuff[i])
            {
                case StuffType.Wood:
                    Debug.Log("districtID=" + districtID + " technologyID=" + technologyID + " i=" + i);
                    if (gc.districtDic[districtID].rStuffWood >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "木材*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffWood + ") ";
                    }
                    else
                    {
                        str += "<color=red>木材*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffWood + ")</color> ";
                    }
                    break;
                case StuffType.Stone:
                    if (gc.districtDic[districtID].rStuffStone >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "石料*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffStone + ") ";
                    }
                    else
                    {
                        str += "<color=red>石料*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffStone + ")</color> ";
                    }
                    break;
                case StuffType.Metal:
                    if (gc.districtDic[districtID].rStuffMetal >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "金属*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffMetal + ") ";
                    }
                    else
                    {
                        str += "<color=red>金属*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffMetal + ")</color> ";
                    }
                    break;
                case StuffType.Leather:
                    if (gc.districtDic[districtID].rStuffLeather >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "皮革*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffLeather + ") ";
                    }
                    else
                    {
                        str += "<color=red>皮革*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffLeather + ")</color> ";
                    }
                    break;
                case StuffType.Cloth:
                    if (gc.districtDic[districtID].rStuffCloth >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "布料*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffCloth + ") ";
                    }
                    else
                    {
                        str += "<color=red>布料*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffCloth + ")</color> ";
                    }
                    break;
                case StuffType.Twine:
                    if (gc.districtDic[districtID].rStuffTwine >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "麻绳*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffTwine + ") ";
                    }
                    else
                    {
                        str += "<color=red>麻绳*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffTwine + ")</color> ";
                    }
                    break;
                case StuffType.Bone:
                    if (gc.districtDic[districtID].rStuffBone >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "骨块*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffBone + ") ";
                    }
                    else
                    {
                        str += "<color=red>骨块*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffBone + ")</color> ";
                    }
                    break;
                case StuffType.Wind:
                    if (gc.districtDic[districtID].rStuffWind >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "风粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffWind + ") ";
                    }
                    else
                    {
                        str += "<color=red>风粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffWind + ")</color> ";
                    }
                    break;
                case StuffType.Fire:
                    if (gc.districtDic[districtID].rStuffFire >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "火粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffFire + ") ";
                    }
                    else
                    {
                        str += "<color=red>火粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffFire + ")</color> ";
                    }
                    break;
                case StuffType.Water:
                    if (gc.districtDic[districtID].rStuffWater >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "水粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffWater + ") ";
                    }
                    else
                    {
                        str += "<color=red>水粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffWater + ")</color> ";
                    }
                    break;
                case StuffType.Ground:
                    if (gc.districtDic[districtID].rStuffGround >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "地粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffGround + ") ";
                    }
                    else
                    {
                        str += "<color=red>地粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffGround + ")</color> ";
                    }
                    break;
                case StuffType.Light:
                    if (gc.districtDic[districtID].rStuffLight >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "光粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffLight + ") ";
                    }
                    else
                    {
                        str += "<color=red>光粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffLight + ")</color> ";
                    }
                    break;
                case StuffType.Dark:
                    if (gc.districtDic[districtID].rStuffDark >= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        str += "暗粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffDark + ") ";
                    }
                    else
                    {
                        str += "<color=red>暗粉尘*" + DataManager.mTechnologyDict[technologyID].NeedStuffValue[i] + "(" + gc.districtDic[districtID].rStuffDark + ")</color> ";
                    }
                    break;

            }
        }
        if (DataManager.mTechnologyDict[technologyID].NeedGold != 0)
        {
            if (gc.gold >= DataManager.mTechnologyDict[technologyID].NeedGold)
            {
                str += "金币*" + DataManager.mTechnologyDict[technologyID].NeedGold + "(" + gc.gold + ") ";
            }
            else
            {
                str += "<color=red>金币*" + DataManager.mTechnologyDict[technologyID].NeedGold + "(" + gc.gold + ")</color> ";
            }
        }

        return str;
    }

    void HideSelectBuildingBlock()
    {
        selectBuildingBlockBgRt.localScale = Vector2.zero;
    }

 
}
