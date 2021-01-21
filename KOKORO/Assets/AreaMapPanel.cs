using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AreaMapPanel : BasePanel, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static AreaMapPanel Instance;
    GameControl gc;
    public GameObject roadGo;
    public GameObject travellerGo;
    public GameObject wallGo;

    public RectTransform infoBlockRt;
    public Text infoBlock_desText;
    public Button infoBlock_GotoBtn;
    public Button infoBlock_DetailBtn;
    public Button infoBlock_ManagerBtn;
   // public Button infoBlock_closeBtn;

    public List<GameObject> districtGo;
    public List<Image> districtForceImage;
    public List<GameObject> districtHeroListGo;

    public List<GameObject> dungeonGo;
    public List<GameObject> dungeonHeroListGo;

    Vector3 offset;
    RectTransform rt;
    Vector3 pos;
    float minWidth;             //水平最小拖拽范围
    float maxWidth;            //水平最大拖拽范围
    float minHeight;            //垂直最小拖拽范围  
    float maxHeight;            //垂直最大拖拽范围
    float rangeX;               //拖拽范围
    float rangeY;               //拖拽范围


    public List<GameObject> pathPoint;//TODO：开发阶段用

    public List<GameObject> travellerGoPool = new List<GameObject>();
    List<GameObject> heroInGo = new List<GameObject>();

    public string infoBlockType = "";
    public int infoBlockID = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        pos = rt.position;

        minWidth = rt.rect.width / 2;
        maxWidth = Screen.width - (rt.rect.width / 2);
        minHeight = rt.rect.height / 2;
        maxHeight = Screen.height - (rt.rect.height / 2);

        //string str = "";
        //for (int i = 0; i < pathPoint.Count; i++)
        //{
        //    str += pathPoint[i] .name+ ","+ (int)pathPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.x + "," + (int)pathPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.y+"\\n";
        //}
        //Debug.Log(str);
    }

    void Update()
    {
        // DragRangeLimit();

        if (Input.GetKeyDown(KeyCode.M))
        {
           // CreateTraveller(0, 1,new List<int> { });
        }
    }

    public void OnShow( int x, int y)
    {

        SetAnchoredPosition(x, y);
        UpdateDistrictAll();
        UpdateDungeonAll();
        SetTraveller();
        HideInfoBlock();
    }

    public void ShowInfoBlock(string type, int id,int x, int y)
    {
        if (infoBlockType == type && infoBlockID == id)
        {
            HideInfoBlock();
            return;
        }
        infoBlockType = type;
        infoBlockID = id;
        infoBlockRt.anchoredPosition = new Vector2(x, y);

        string str = "";
        if (type == "district")
        {
            str += gc.districtDic[id].level+ "级据点\n<color=#EFDDB1>设施</color> " + gc.districtDic[id].buildingList.Count+ "   <color=#EFDDB1>人口</color> " + gc.districtDic[id].people+"/"+ gc.districtDic[id].peopleLimit+"\n";
            if (gc.districtDic[id].isOwn)
            {
                str += "<color=#EFDDB1>领主</color> " + gc.heroDic[0].name + "(己方)";

                infoBlock_GotoBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                infoBlock_GotoBtn.onClick.RemoveAllListeners();
                infoBlock_GotoBtn.onClick.AddListener(delegate () {
                    /*移动*/
                });

                infoBlock_ManagerBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                infoBlock_ManagerBtn.onClick.RemoveAllListeners();
                infoBlock_ManagerBtn.onClick.AddListener(delegate () {
                    gc.nowCheckingDistrictID = (short)id;
                    DistrictMapPanel.Instance.OnShow();
                    HideInfoBlock();
                });

            }
            else
            {
                infoBlock_ManagerBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                if (gc.districtDic[id].isOpen)
                {
                    str += "已获得通行权";
                    infoBlock_GotoBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    infoBlock_GotoBtn.onClick.RemoveAllListeners();
                    infoBlock_GotoBtn.onClick.AddListener(delegate () {
                        /*移动*/
                    });
                }
                else
                {
                    str += "未获得通行权";
                    infoBlock_GotoBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
            }
        }
        else if (type == "dungeon")
        {
            switch (gc.dungeonList[id].stage)
            {
                case DungeonStage.Open: str += "可探索"; break;
                case DungeonStage.OpenUp: str += "开拓中"; break;
                case DungeonStage.Done: str += "已探索"; break;
            }
        
        }
        infoBlock_desText.text = str;
    }

    public void HideInfoBlock()
    {
        infoBlockType = "";
        infoBlockID = -1;
        infoBlockRt.anchoredPosition = new Vector2(0, 5000);
    }

    public void SetTraveller()
    {
        foreach (KeyValuePair<int, TravellerObject> kvp in gc.travellerDic)
        {
            CreateTraveller(kvp.Key, kvp.Value.pathPointList, kvp.Value.nowPointIndex, kvp.Value.pic, new List<int> { });
        }
    }

    public void CreateTraveller(int travellerID, List<int> pathList,int nowPointIndex, string pic, List<int> heroID)
    {
      //  List<int> pathList = DataManager.mAreaPathDict[StartDistrict + "-" + EndDistrict].Path;

        GameObject go;

        if (travellerGoPool.Count > 0)
        {
            go = travellerGoPool[0];
            go.transform.localScale = Vector2.one;
            travellerGoPool.RemoveAt(0);
        }
        else
        {
            go = Instantiate(Resources.Load("Prefab/UIBlock/Block_Traveller")) as GameObject;
            go.transform.SetParent(travellerGo.transform);
        }

        go.name = "Traveller_" + travellerID ;

        if (heroID.Count > 0)
        {
            go.transform.GetChild(0).localScale = Vector2.one;
        }
        else
        {
            go.transform.GetChild(0).localScale = Vector2.zero;
        }
        //go.GetComponent<RectTransform>().anchoredPosition = new Vector2(DataManager.mAreaPathPointDict[pathList[nowPointIndex]].X, DataManager.mAreaPathPointDict[pathList[nowPointIndex]].Y);
        go.GetComponent<AnimatiorControlByTraveller>().travellerID=  travellerID;
        go.GetComponent<AnimatiorControlByTraveller>().pathPointList = pathList;
        go.GetComponent<AnimatiorControlByTraveller>().SetCharaFrames(pic);
        go.GetComponent<AnimatiorControlByTraveller>().nowPointIndex = nowPointIndex;

        if (nowPointIndex == 0)
        {
            go.GetComponent<AnimatiorControlByTraveller>().StartMove();
        }
        else
        {
            go.GetComponent<AnimatiorControlByTraveller>().ContinueMove();
        }

    }

    public void UpdateDistrictAll()
    {
        for (int i = 0; i < DataManager.mDistrictDict.Count; i++)
        {
            UpdateDistrictSingle(i);
        }
    }

    public void UpdateDistrictSingle(int districtID)
    {
        districtGo[districtID].GetComponent<Button>().onClick.RemoveAllListeners();
        districtGo[districtID].GetComponent<Button>().onClick.AddListener(delegate () { ShowInfoBlock("district", districtID,(int)( districtGo[districtID].GetComponent<RectTransform>().anchoredPosition.x+60f),(int)( districtGo[districtID].GetComponent<RectTransform>().anchoredPosition.y)); });


        if (gc.districtDic[districtID].isOwn)
        {
            districtForceImage[districtID].sprite = Resources.Load<Sprite>("Image/Other/icon834");
        }
        else
        {
            districtForceImage[districtID].sprite = Resources.Load<Sprite>("Image/Empty");
        }

        for (int i = 0; i < districtHeroListGo[districtID].transform.childCount; i++)
        {
            districtHeroListGo[districtID].transform.GetChild(i).GetComponent<RectTransform>().localScale = Vector2.zero;
            heroInGo.Add(districtHeroListGo[districtID].transform.GetChild(i).gameObject);
        }

        if (gc.districtDic[districtID].heroList.Count > 0)
        {
            GameObject go;

            for (int i = 0; i < gc.districtDic[districtID].heroList.Count; i++)
            {
                if (heroInGo.Count > 0)
                {
                    go = heroInGo[0];
                    heroInGo[0].transform.GetComponent<RectTransform>().localScale = Vector2.one;
                    heroInGo.RemoveAt(0);
                }
                else
                {
                    go = Instantiate(Resources.Load("Prefab/UILabel/Label_AreaMapHeroIn")) as GameObject;
                }
                go.transform.SetParent(districtHeroListGo[districtID].transform);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f + i * 26f, -4);

                go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + gc.heroDic[gc.districtDic[districtID].heroList[i]].pic + "/Pic");

                if (gc.heroDic[gc.districtDic[districtID].heroList[i]].adventureInTeam != -1)
                {
                    if (gc.adventureTeamList[gc.heroDic[gc.districtDic[districtID].heroList[i]].adventureInTeam].state == AdventureState.Doing)
                    {
                        go.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                    }
                    else
                    {
                        go.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    }
                }
                else
                {
                    go.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                }
            }

            districtHeroListGo[districtID].transform.GetComponent<RectTransform>().sizeDelta = new Vector2(6f + gc.districtDic[districtID].heroList.Count * 26f, 36f);
        }
        else
        {
            districtHeroListGo[districtID].transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        }
    }


    public void UpdateDungeonAll()
    {
        for (int i = 0; i < DataManager.mDungeonDict.Count; i++)
        {
            UpdateDungeonSingle(i);
        }
    }

    public void UpdateDungeonSingle(int dungeonID)
    {
        dungeonGo[dungeonID].GetComponent<Button>().onClick.RemoveAllListeners();
        dungeonGo[dungeonID].GetComponent<Button>().onClick.AddListener(delegate () { ShowInfoBlock("dungeon", dungeonID, (int)(dungeonGo[dungeonID].GetComponent<RectTransform>().anchoredPosition.x + 60f), (int)(dungeonGo[dungeonID].GetComponent<RectTransform>().anchoredPosition.y)); });

        if (gc.dungeonList[dungeonID].stage == DungeonStage.Close)
        {
            dungeonGo[dungeonID].GetComponent<RectTransform>().localScale = Vector2.zero;
            return;
        }
        for (int i = 0; i < dungeonHeroListGo[dungeonID].transform.childCount; i++)
        {
            dungeonHeroListGo[dungeonID].transform.GetChild(i).GetComponent<RectTransform>().localScale = Vector2.zero;
            heroInGo.Add(dungeonHeroListGo[dungeonID].transform.GetChild(i).gameObject);
        }

        int heroNum = 0;
        for (int i = 0; i < gc.dungeonList[dungeonID].teamList.Count; i++)
        {
            heroNum += gc.adventureTeamList[gc.dungeonList[dungeonID].teamList[i]].heroIDList.Count;
            for (int j = 0; j < gc.adventureTeamList[gc.dungeonList[dungeonID].teamList[i]].heroIDList.Count; j++)
            {
                
            }
        }

        if (heroNum > 0)
        {
            int index = 0;
            GameObject go;
            for (int i = 0; i < gc.dungeonList[dungeonID].teamList.Count; i++)
            {
                for (int j = 0; j < gc.adventureTeamList[gc.dungeonList[dungeonID].teamList[i]].heroIDList.Count; j++)
                {
                    if (heroInGo.Count > 0)
                    {
                        go = heroInGo[0];
                        heroInGo[0].transform.GetComponent<RectTransform>().localScale = Vector2.one;
                        heroInGo.RemoveAt(0);
                    }
                    else
                    {
                        go = Instantiate(Resources.Load("Prefab/UILabel/Label_AreaMapHeroIn")) as GameObject;
                    }
                    go.transform.SetParent(dungeonHeroListGo[dungeonID].transform);
                    go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f + index * 26f, -4);

                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + gc.heroDic[gc.adventureTeamList[gc.dungeonList[dungeonID].teamList[i]].heroIDList[j]].pic + "/Pic");
                    index++;
                }
            }
            dungeonHeroListGo[dungeonID].transform.GetComponent<RectTransform>().sizeDelta = new Vector2(6f + heroNum * 26f, 36f);
        }
        else
        {
            dungeonHeroListGo[dungeonID].transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }
    }

    //    GameObject go;

    //    go = Instantiate(Resources.Load("Prefab/UILabel/Label_MapGrid")) as GameObject;
    //    go.transform.SetParent(buildingGo.transform);
    //    go.GetComponent<RectTransform>().anchoredPosition = new Vector3(gridX * 16f, gridY * -16f, 0f);
    //   // go.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/" + str + gc.buildingDic[buildingID].mapPic);
    //    go.name = gridX +","+ gridY;


    /// <summary>
    /// 拖拽范围限制
    /// </summary>
    void DragRangeLimit()
    {
        //限制水平/垂直拖拽范围在最小/最大值内
        rangeX = Mathf.Clamp(rt.position.x, minWidth, maxWidth);
        rangeY = Mathf.Clamp(rt.position.y, minHeight, maxHeight);
        //更新位置
        rt.position = new Vector3(rangeX, rangeY, 0);
    }

    /// <summary>
    /// 开始拖拽
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;

        //将屏幕坐标转换成世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            //计算UI和指针之间的位置偏移量
            offset = rt.position - globalMousePos;
        }
    }

    /// <summary>
    /// 拖拽中
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// 更新UI的位置
    /// </summary>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 globalMousePos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            rt.position = offset + globalMousePos;
        }
    }

    }
