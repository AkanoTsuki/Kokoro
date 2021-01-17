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

    List<GameObject> technologyGoPool = new List<GameObject>();

    public Button doBtn;
    public Button closeBtn;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {

        closeBtn.onClick.AddListener(delegate () { OnHide(); });

    }


    public override void OnShow()
    {
        UpdateAllInfo();
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
        
    }

    public void UpdateListDone()
    {
        List<TechnologyObject> technologyObjects = new List<TechnologyObject>();

        foreach (KeyValuePair<int, TechnologyObject> kvp in gc.technologyDic)
        {
            if (kvp.Value.isDone)
            {
                technologyObjects.Add(kvp.Value);
            }
        }

        GameObject go;
        for (int i = 0; i < technologyObjects.Count; i++)
        {
            if (i < technologyObjects.Count)
            {
                go = technologyGoPool[i];
                technologyGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Item")) as GameObject;
                go.transform.SetParent(list_doneGo.transform);
                technologyGoPool.Add(go);
            }
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f, i * -44f);
            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/TechnologyPic/" + DataManager.mTechnologyDict[technologyObjects[i].id].Pic);
            go.transform.GetChild(1).GetComponent<Text>().text = DataManager.mTechnologyDict[technologyObjects[i].id].Name;

            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(delegate () { UpdateInfo(technologyObjects[i].id); });
        }
    }

    public void UpdateInfo(int technologyID)
    {
        info_picImage.sprite = Resources.Load<Sprite>("Image/TechnologyPic/" + DataManager.mTechnologyDict[technologyID].Pic);
        info_nameText.text = DataManager.mTechnologyDict[technologyID].Name;

        string str = "";
        switch (DataManager.mTechnologyDict[technologyID].Type)
        {
            case "Work": str += "生产类"; break;
            case "Fight": str += "战斗类"; break;
        }

        if (DataManager.mTechnologyDict[technologyID].ParentID.Count != 0)
        {
            str += "\n<color=#EFDDB1>前置条件</color>";
            for (int i = 0; i < DataManager.mTechnologyDict[technologyID].ParentID.Count; i++)
            {
                str += "\n  完成["+ DataManager.mTechnologyDict[DataManager.mTechnologyDict[technologyID].ParentID[i]].Name+"]";
            }
        }
        if (DataManager.mTechnologyDict[technologyID].NeedBuilding != -1)
        {
            str += "\n<color=#EFDDB1>需要建筑</color>";
            str += "\n  " + DataManager.mBuildingDict[DataManager.mTechnologyDict[technologyID].NeedBuilding].Name ;
        }

        if (DataManager.mTechnologyDict[technologyID].NeedStuff.Count != 0|| DataManager.mTechnologyDict[technologyID].NeedGold!=0)
        {
            str += "\n<color=#EFDDB1>需要资源</color>";

            string strStuff = "\n ";

            for (int i = 0; i < DataManager.mTechnologyDict[technologyID].NeedStuff.Count; i++)
            {
                switch (DataManager.mTechnologyDict[technologyID].NeedStuff[i])
                {
                    case StuffType.Wood: strStuff += " 木材*"+ DataManager.mTechnologyDict[technologyID].NeedStuffValue[i]; break;
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
        str += "<color=#EFDDB1>研究时间</color> " + DataManager.mTechnologyDict[technologyID].NeedTime+"天";
        str += "\n\n" + DataManager.mTechnologyDict[technologyID].Des;
        if (DataManager.mTechnologyDict[technologyID].ChildrenID.Count != 0)
        {
            str += "\n解锁";
            for (int i = 0; i < DataManager.mTechnologyDict[technologyID].ChildrenID.Count; i++)
            {
                str += "["+ DataManager.mTechnologyDict[DataManager.mTechnologyDict[technologyID].ChildrenID [i]].Name+ "]";
            }
        }

        info_desText.text = str;

    }
}
