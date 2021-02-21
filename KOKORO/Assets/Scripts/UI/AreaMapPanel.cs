using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class AreaMapPanel : BasePanel, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static AreaMapPanel Instance;

    GameControl gc;

    #region 【UI控件】
    public GameObject roadGo;
    public GameObject travellerGo;
    public GameObject wallGo;

    public RectTransform districtInfoBlockRt;
    public Text districtInfoBlock_nameText;
    public Image districtInfoBlock_flagImage;
    public Text districtInfoBlock_desText;
    public Button districtInfoBlock_GotoBtn;
    public Button districtInfoBlock_LeaveBtn;
    public Button districtInfoBlock_DetailBtn;
    public Button districtInfoBlock_ManagerBtn;

    public RectTransform dungeonInfoBlockRt;
    public Image dungeonInfoBlock_picImage;
    public Text dungeonInfoBlock_nameText;
    public Text dungeonInfoBlock_stateText;
    public Text dungeonInfoBlock_desText;
    public GameObject dungeonInfoBlock_teamGo;
    public Button dungeonInfoBlock_sendBtn;
    public Button dungeonInfoBlock_detailBtn;

    public RectTransform travellerInfoBlockRt;
    public Image travellerInfoBlock_flagImage;
    public Text travellerInfoBlock_desText;

    public List<GameObject> districtGo;
    public List<Image> districtForceImage;
    public List<GameObject> districtHeroListGo;

    public List<GameObject> dungeonGo;
    public List<GameObject> dungeonHeroListGo;

    public GameObject numGo;
    #endregion

    //运行变量-地图拖拽
    Vector3 offset;
    RectTransform rt;
    float minWidth;             //水平最小拖拽范围
    float maxWidth;            //水平最大拖拽范围
    float minHeight;            //垂直最小拖拽范围  
    float maxHeight;            //垂直最大拖拽范围
    float rangeX;               //拖拽范围
    float rangeY;               //拖拽范围

    //运行变量
    public int districtInfoBlockID = -1;
    public int dungeonInfoBlockID = -1;

    //对象池
    public List<GameObject> numPool = new List<GameObject>();
    public List<GameObject> travellerGoPool = new List<GameObject>();
    public List<GameObject> teamGoPool = new List<GameObject>();
    List<GameObject> heroInGoPool = new List<GameObject>();

    //TODO：开发阶段用
    public List<GameObject> pathPoint;
    public List<GameObject> districtPoint;
    public List<GameObject> dungeonPoint;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        rt = GetComponent<RectTransform>();
        //pos = rt.position;

        minWidth = rt.rect.width / 2;
        maxWidth = Screen.width - (rt.rect.width / 2);
        minHeight = rt.rect.height / 2;
        maxHeight = Screen.height - (rt.rect.height / 2);

        //开发阶段使用
        //string str = "";
        //for (int i = 0; i < pathPoint.Count; i++)
        //{
        //    str += pathPoint[i].name + "," + (int)pathPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.x + "," + (int)pathPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.y + "\\n";
        //}
        //Debug.Log(str);
        //string str = "";
        //for (int i = 0; i < districtPoint.Count; i++)
        //{
        //    str += districtPoint[i].name + "," + (int)districtPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.x + "," + (int)districtPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.y + "\\n";
        //}
        //Debug.Log(str);
        //str = "";
        //for (int i = 0; i < dungeonPoint.Count; i++)
        //{
        //    str += dungeonPoint[i].name + "," + (int)dungeonPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.x + "," + (int)dungeonPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.y + "\\n";
        //}
        //Debug.Log(str);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (districtInfoBlockID != -1)
            {
                HideDistrictInfoBlock();
            }
            if (dungeonInfoBlockID != -1)
            {
                HideDungeonInfoBlock();
            }
        }
        //DragRangeLimit();
    }

    //主面板显示
    public void OnShow( int x, int y)
    {
        SetAnchoredPosition(x, y);
        UpdateDistrictAll();
        UpdateDungeonAll();
        SetTraveller();
        HideDistrictInfoBlock();
        HideDungeonInfoBlock();
    }

    //据点信息浮动块-显示、更新
    public void ShowDistrictInfoBlock( int id,int x, int y)
    {      
        if ( districtInfoBlockID == id)
        {
            HideDistrictInfoBlock();
            return;
        }

        if (dungeonInfoBlockID != -1)
        {
            HideDungeonInfoBlock();
        }

        districtInfoBlockID = id;
        districtInfoBlockRt.anchoredPosition = new Vector2(x, y);
        districtInfoBlockRt.gameObject.SetActive(true);

        districtInfoBlock_nameText.text = gc.districtDic[id].name;

        string str = "";
        string strTx = "";
        str += "<color=#ECC74F>" + gc.OutputSignStr("★", gc.districtDic[id].level) + gc.OutputSignStr("☆", DataManager.mDistrictDict[id].MaxLevel - gc.districtDic[id].level)  + "</color>\n";
        if (gc.districtDic[id].force != -1)
        {
            districtInfoBlock_flagImage.sprite = Resources.Load("Image/Other/icon_flag_" + gc.forceDic[gc.districtDic[id].force].flagIndex + "_a", typeof(Sprite)) as Sprite;
            str += gc.forceDic[gc.districtDic[id].force].name + "\n<color=#EFDDB1>领主</color> " + gc.forceDic[gc.districtDic[id].force].leader + (gc.districtDic[id].force == 0 ? "<color=green>(己方)</color>" : "");

            if (gc.districtDic[id].force == 0)
            {
                districtInfoBlock_GotoBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                districtInfoBlock_GotoBtn.onClick.RemoveAllListeners();
                districtInfoBlock_GotoBtn.onClick.AddListener(delegate ()
                {
                    /*移动*/
                    TransferPanel.Instance.OnShow("From", (short)id);
                });

                districtInfoBlock_ManagerBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                districtInfoBlock_ManagerBtn.transform.GetChild(0).GetComponent<Text>().text = "管理";
                districtInfoBlock_ManagerBtn.onClick.RemoveAllListeners();
                districtInfoBlock_ManagerBtn.onClick.AddListener(delegate ()
                {
                    gc.nowCheckingDistrictID = (short)id;
                    DistrictMapPanel.Instance.OnShow();
                    HideDistrictInfoBlock();
                });
            }
            else
            {
                //TODO：开发配置阶段 ，有没有英雄派驻都可以访问 原值0 ，临时-1
                if (gc.districtDic[id].heroList.Count > 0)
                {
                    districtInfoBlock_ManagerBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    districtInfoBlock_ManagerBtn.transform.GetChild(0).GetComponent<Text>().text = "访问";
                    districtInfoBlock_ManagerBtn.onClick.RemoveAllListeners();
                    districtInfoBlock_ManagerBtn.onClick.AddListener(delegate ()
                    {
                        gc.nowCheckingDistrictID = (short)id;
                        DistrictMapPanel.Instance.OnShow();
                        HideDistrictInfoBlock();
                    });
                }
                else
                {
                    districtInfoBlock_ManagerBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }

                if (gc.districtDic[id].isOpen)
                {
                    strTx += "\n<color=#3BFF55>[已获得通行权]</color>";
                    districtInfoBlock_GotoBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    districtInfoBlock_GotoBtn.onClick.RemoveAllListeners();
                    districtInfoBlock_GotoBtn.onClick.AddListener(delegate ()
                    {
                        /*移动*/
                        TransferPanel.Instance.OnShow("From", (short)id);
                    });


                }
                else
                {
                    strTx += "\n<color=#FF523B>[未获得通行权]</color>";
                    districtInfoBlock_GotoBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
            }

            if (gc.districtDic[id].heroList.Count > 0)
            {
                districtInfoBlock_LeaveBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                districtInfoBlock_LeaveBtn.onClick.RemoveAllListeners();
                districtInfoBlock_LeaveBtn.onClick.AddListener(delegate ()
                {
                    /*移动*/
                    TransferPanel.Instance.OnShow("To", (short)id);
                });
            }
            else
            {
                districtInfoBlock_LeaveBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            }
        }
        else
        {
            districtInfoBlock_flagImage.sprite = Resources.Load<Sprite>("Image/Empty");
            str += "无人控制的领地";
        }

        if (gc.districtDic[id].heroList.Count > 0)
        {
            str += "\n<color=#EFDDB1>派驻英雄</color> " + gc.districtDic[id].heroList.Count;
        }

        str += "\n<color=#EFDDB1>设施</color> " + gc.districtDic[id].buildingList.Count + "   <color=#EFDDB1>人口</color> " + gc.districtDic[id].people + "/" + gc.districtDic[id].peopleLimit;

        districtInfoBlock_desText.text = str+ "\n——————————————\n" + DataManager.mDistrictDict[id].Des+ strTx;
    }

    //据点信息浮动块-隐藏
    public void HideDistrictInfoBlock()
    {
        districtInfoBlockID = -1;
        districtInfoBlockRt.gameObject.SetActive(false);
    }

    //地牢信息浮动块-显示
    public void ShowDungeonInfoBlock(int id, int x, int y)
    {
        if (dungeonInfoBlockID == id)
        {
            HideDungeonInfoBlock();
            return;
        }

        if (districtInfoBlockID != -1)
        {
            HideDistrictInfoBlock();
        }

        dungeonInfoBlockRt.anchoredPosition = new Vector2(x, y);
        dungeonInfoBlockRt.gameObject.SetActive(true);
        UpdateDungeonInfoBlock(id);
    }

    //地牢信息浮动块-更新
    public void UpdateDungeonInfoBlock(int id)
    {
        dungeonInfoBlockID = id;

        dungeonInfoBlock_picImage.sprite = Resources.Load("Image/AdventureBG/ABG_" + DataManager.mDungeonDict[id].ScenePic[0] + "_B", typeof(Sprite)) as Sprite;
        dungeonInfoBlock_nameText.text = DataManager.mDungeonDict[id].Name;
        switch (gc.dungeonList[id].stage)
        {
            case DungeonStage.Close: dungeonInfoBlock_stateText.text = "未开放"; break;
            case DungeonStage.Open: dungeonInfoBlock_stateText.text = "可开拓"; break;
            case DungeonStage.OpenUp: dungeonInfoBlock_stateText.text = "开拓中"; break;
            case DungeonStage.Done: dungeonInfoBlock_stateText.text = "已开拓"; break;
        }
        string str = "";


        for (int j = 0; j < DataManager.mDungeonDict[id].MonsterID.Count; j++)
        {
            str += DataManager.mMonsterDict[DataManager.mDungeonDict[id].MonsterID[j]].Name + " ";
        }

        dungeonInfoBlock_desText.text = "<color=#EFDDB1>地图等级</color> " + DataManager.mDungeonDict[id].Level + "   <color=#EFDDB1>旅程</color> " + (DataManager.mDungeonDict[id].PartNum * 100) + "M\n" + DataManager.mDungeonDict[id].Des + "\n<color=#EFDDB1>出现怪物</color> " + str;
        GameObject go;
      
    
            for (byte i = 0; i < gc.dungeonList[id].teamList.Count; i++)
        {
            byte teamID = gc.dungeonList[id].teamList[i];

            
               
                if (i < teamGoPool.Count)
                {
                    go = teamGoPool[i];
                    teamGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;

                }
                else
                {
                    go = Instantiate(Resources.Load("Prefab/UILabel/Label_TeamInDungeonInfo")) as GameObject;
                    go.transform.SetParent(dungeonInfoBlock_teamGo.transform);
                    teamGoPool.Add(go);
                }
                go.name = (teamID + 1).ToString();
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * -59f);

                for (int j = 0; j < gc.adventureTeamList[teamID].heroIDList.Count; j++)
                {
                    go.transform.GetChild(1).GetChild(j).GetComponent<RectTransform>().localScale = Vector2.one;
                    go.transform.GetChild(1).GetChild(j).GetComponent<Image>().overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[gc.adventureTeamList[teamID].heroIDList[j]].pic + "/Pic", typeof(Sprite)) as Sprite;
                    go.transform.GetChild(1).GetChild(j).GetChild(0).GetComponent<Text>().text = gc.heroDic[gc.adventureTeamList[teamID].heroIDList[j]].level.ToString();
                    go.transform.GetChild(1).GetChild(j).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2((float)gc.adventureTeamList[teamID].heroHpList[j] / gc.GetHeroAttr(Attribute.Hp, gc.adventureTeamList[teamID].heroIDList[j]) * 24f, 4f);
                    go.transform.GetChild(1).GetChild(j).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2((float)gc.adventureTeamList[teamID].heroMpList[j] / gc.GetHeroAttr(Attribute.Mp, gc.adventureTeamList[teamID].heroIDList[j]) * 24f, 4f);


                }
                for (int j = gc.adventureTeamList[teamID].heroIDList.Count; j < 3; j++)
                {
                    go.transform.GetChild(1).GetChild(j).GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                go.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = (teamID + 1).ToString();
              
                string strdes = "";
                go.transform.GetChild(4).localScale = Vector2.one;
                go.transform.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
                go.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate ()
                {

                    AdventureMainPanel.Instance.OnShow(teamID);
                });
           

                if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
                {
                    
                    strdes = "待命";

                go.transform.GetChild(5).localScale = Vector2.zero;

                go.transform.GetChild(6).localScale = Vector2.zero;

                    go.transform.GetChild(7).localScale = Vector2.one;
                    go.transform.GetChild(7).GetComponent<Button>().onClick.RemoveAllListeners();
                    go.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(delegate ()
                    {

                        gc.AdventureTeamStart(teamID);
                    });
                    go.transform.GetChild(8).localScale = Vector2.one;
                    go.transform.GetChild(8).GetComponent<Button>().onClick.RemoveAllListeners();
                    go.transform.GetChild(8).GetComponent<Button>().onClick.AddListener(delegate ()
                    {

                        gc.AdventureTeamBack(teamID);
                    });
                }
                else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
                {

                    float rate = (float)gc.adventureTeamList[teamID].nowDay / DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum;

                    go.transform.GetChild(5).localScale = Vector2.one;
                    go.transform.GetChild(5).GetComponent<Button>().onClick.RemoveAllListeners();
                    go.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        AdventureTeamPanel.Instance.OnShow(teamID, 76, -104);
                    });

                    go.transform.GetChild(6).localScale = Vector2.zero;
                    go.transform.GetChild(7).localScale = Vector2.zero;
                    go.transform.GetChild(8).localScale = Vector2.zero;
                    if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
                    {
                        strdes = "探险中[" + (int)(rate * 100) + "%][战斗]";
                    }
                    else
                    {
                        strdes = "探险中[" + (int)(rate * 100) + "%][行进]";
                    }

                    //Debug.Log("strdes="+ strdes);
                }
                else
                {
                    go.transform.GetChild(5).localScale = Vector2.one;
                    go.transform.GetChild(5).GetComponent<Button>().onClick.RemoveAllListeners();
                    go.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        AdventureTeamPanel.Instance.OnShow(teamID, 76, -104);
                    });

                    go.transform.GetChild(6).localScale = Vector2.one;
                    go.transform.GetChild(6).GetComponent<Button>().onClick.RemoveAllListeners();
                    go.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        gc.AdventureTakeGets(teamID);
                    });

                    go.transform.GetChild(7).localScale = Vector2.zero;
                    //go.transform.GetChild(7).GetComponent<Button>().onClick.RemoveAllListeners();
                    //go.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(delegate ()
                    //{

                    //    gc.AdventureTeamStart(teamID);
                    //});
                    go.transform.GetChild(8).localScale = Vector2.zero;
                    //go.transform.GetChild(8).GetComponent<Button>().onClick.RemoveAllListeners();
                    //go.transform.GetChild(8).GetComponent<Button>().onClick.AddListener(delegate ()
                    //{

                    //    gc.AdventureTeamBack(teamID);
                    //});

                    float rate = (float)gc.adventureTeamList[teamID].nowDay / DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum;

                    if (gc.adventureTeamList[teamID].state == AdventureState.Done)
                    {
                        strdes = "结束[" + (int)(rate * 100) + "%]<color=green>[完成]</color>";
                    }
                    else if (gc.adventureTeamList[teamID].state == AdventureState.Retreat)
                    {
                        strdes = "结束[" + (int)(rate * 100) + "%]<color=red>[中止]</color>";
                    }
                    else if (gc.adventureTeamList[teamID].state == AdventureState.Fail)
                    {
                        strdes = "结束[" + (int)(rate * 100) + "%]<color=red>[失败]</color>";
                    }
                }

                go.transform.GetChild(3).GetComponent<Text>().text = strdes;

                
            
        }

        for (int i = gc.dungeonList[id].teamList.Count; i < teamGoPool.Count; i++)
        {
            teamGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        dungeonInfoBlockRt.sizeDelta = new Vector2(214f, gc.dungeonList[id].teamList.Count * 59f + 162f);

        dungeonInfoBlock_sendBtn.onClick.RemoveAllListeners();
        dungeonInfoBlock_sendBtn.onClick.AddListener(delegate ()
        {
            AdventureSendPanel.Instance.OnShow("From", (short)id);
        });
    }

    //地牢信息浮动块-隐藏
    public void HideDungeonInfoBlock()
    {
        dungeonInfoBlockID = -1;
        dungeonInfoBlockRt.gameObject.SetActive(false);
    }

    //旅人信息浮动块-显示
    public void ShowTravellerInfoBlock(int id, int x, int y)
    {
        string str = "      "+ gc.travellerDic[id].personType+ "["+ gc.travellerDic[id].personNum + "人]\n";
        if (gc.travellerDic[id].force != -1)
        {
            str += "从属 " + gc.forceDic[gc.travellerDic[id].force].name+"\n";
            travellerInfoBlock_flagImage.sprite = Resources.Load("Image/Other/icon_flag_" + gc.forceDic[gc.travellerDic[id].force].flagIndex + "_a", typeof(Sprite)) as Sprite;
        }
        else
        {
            travellerInfoBlock_flagImage.sprite = Resources.Load<Sprite>("Image/Empty");
        }

        if (gc.travellerDic[id].endType == "District")
        {
            str += "前往<color=#FFF48A>" + gc.districtDic[gc.travellerDic[id].endDistrictOrDungeonID].name +"</color>";
        }
        else if(gc.travellerDic[id].endType == "Dungeon")
        {
            str += "前往<color=#5AFF6F>" + DataManager.mDungeonDict[gc.travellerDic[id].endDistrictOrDungeonID].Name + "</color>";
        }

        travellerInfoBlock_desText.text = str;

        travellerInfoBlockRt.anchoredPosition = new Vector2(x, y);
    }

    //旅人信息浮动块-隐藏
    public void HideTravellerInfoBlock()
    {
        travellerInfoBlockRt.anchoredPosition = new Vector2(0, 5000);
    }

    //旅人图块-全部-初始化设置
    public void SetTraveller()
    {
        foreach (KeyValuePair<int, TravellerObject> kvp in gc.travellerDic)
        {
            CreateTraveller(kvp.Key, kvp.Value.pathPointList, kvp.Value.nowPointIndex, kvp.Value.pic, kvp.Value.heroList);
        }
    }

    //旅人图块-创建
    public void CreateTraveller(int travellerID, List<int> pathList,int nowPointIndex, string pic, List<int> heroID)
    {
        GameObject go;

        if (travellerGoPool.Count > 0)
        {
            go = travellerGoPool[0];
            go.transform.localScale = Vector2.one;
            travellerGoPool.RemoveAt(0);
            go.GetComponent<AnimatiorControlByTraveller>().enabled = true;
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
            go.transform.GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_flag_" + gc.forceDic[0].flagIndex + "_b", typeof(Sprite)) as Sprite;
        }
        else
        {
            go.transform.GetChild(0).localScale = Vector2.zero;
        }
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

    //据点图块-全部-更新（实力旗帜、派驻英雄图标等）
    public void UpdateDistrictAll()
    {
        for (int i = 0; i < DataManager.mDistrictDict.Count; i++)
        {
            UpdateDistrictSingle(i);
        }
    }

    //据点图块-单个-更新（实力旗帜、派驻英雄图标等）
    public void UpdateDistrictSingle(int districtID)
    {
        districtGo[districtID].GetComponent<Button>().onClick.RemoveAllListeners();
        districtGo[districtID].GetComponent<Button>().onClick.AddListener(delegate () {

            if (districtInfoBlockID != districtID)
            {
                float x = Screen.width / 2f - districtGo[districtID].GetComponent<RectTransform>().anchoredPosition.x;
                float y = -districtGo[districtID].GetComponent<RectTransform>().anchoredPosition.y - Screen.height / 2f;
                transform.DOComplete();
                transform.DOLocalMove(new Vector2(x - Screen.width / 2f, y + Screen.height / 2f), 0.5f);
            }

            ShowDistrictInfoBlock( districtID,(int)( districtGo[districtID].GetComponent<RectTransform>().anchoredPosition.x+60f),(int)( districtGo[districtID].GetComponent<RectTransform>().anchoredPosition.y)); 
        });


        if (gc.districtDic[districtID].force != -1)
        {
            districtForceImage[districtID].sprite = Resources.Load<Sprite>("Image/Other/icon_flag_" +gc.forceDic[gc.districtDic[districtID].force].flagIndex  + "_a");
        }
        else
        {
            districtForceImage[districtID].sprite = Resources.Load<Sprite>("Image/Empty");
        }

        for (int i = districtHeroListGo[districtID].transform.childCount-1; i >=0; i--)
        {
          
            districtHeroListGo[districtID].transform.GetChild(i).GetComponent<RectTransform>().localScale = Vector2.zero;
            heroInGoPool.Add(districtHeroListGo[districtID].transform.GetChild(i).gameObject);
            districtHeroListGo[districtID].transform.GetChild(i).SetParent(districtHeroListGo[districtID].transform.parent);
           
        }

        if (gc.districtDic[districtID].heroList.Count > 0)
        {
            GameObject go;

            for (int i = 0; i < gc.districtDic[districtID].heroList.Count; i++)
            {
                if (heroInGoPool.Count > 0)
                {
                    go = heroInGoPool[0];
                    heroInGoPool[0].transform.GetComponent<RectTransform>().localScale = Vector2.one;
                    heroInGoPool.RemoveAt(0);
                }
                else
                {
                    go = Instantiate(Resources.Load("Prefab/UILabel/Label_AreaMapHeroIn")) as GameObject;
                }
                go.transform.SetParent(districtHeroListGo[districtID].transform);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f + i * 26f, -4);

                go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + gc.heroDic[gc.districtDic[districtID].heroList[i]].pic + "/Pic");
            }

            districtHeroListGo[districtID].transform.GetComponent<RectTransform>().sizeDelta = new Vector2(6f + gc.districtDic[districtID].heroList.Count * 26f, 36f);
        }
        else
        {
            districtHeroListGo[districtID].transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }
    }


    //地牢图块-全部-更新（派驻英雄图标等）
    public void UpdateDungeonAll()
    {
        for (int i = 0; i < DataManager.mDungeonDict.Count; i++)
        {
            UpdateDungeonSingle(i);
        }
    }

    //地牢图块-单个-更新（派驻英雄图标等）
    public void UpdateDungeonSingle(int dungeonID)
    {
        dungeonGo[dungeonID].GetComponent<Button>().onClick.RemoveAllListeners();
        dungeonGo[dungeonID].GetComponent<Button>().onClick.AddListener(delegate () {

            if (dungeonInfoBlockID != dungeonID)
            {
                float x = Screen.width / 2f - dungeonGo[dungeonID].GetComponent<RectTransform>().anchoredPosition.x;
                float y = -dungeonGo[dungeonID].GetComponent<RectTransform>().anchoredPosition.y - Screen.height / 2f;
                transform.DOComplete();
                transform.DOLocalMove(new Vector2(x - Screen.width / 2f, y + Screen.height / 2f), 0.5f);
            }

            ShowDungeonInfoBlock( dungeonID, (int)(dungeonGo[dungeonID].GetComponent<RectTransform>().anchoredPosition.x + 60f), (int)(dungeonGo[dungeonID].GetComponent<RectTransform>().anchoredPosition.y)); });

        if (gc.dungeonList[dungeonID].stage == DungeonStage.Close)
        {
            dungeonGo[dungeonID].GetComponent<RectTransform>().localScale = Vector2.zero;
            return;
        }
        for (int i = 0; i < dungeonHeroListGo[dungeonID].transform.childCount; i++)
        {
            dungeonHeroListGo[dungeonID].transform.GetChild(i).GetComponent<RectTransform>().localScale = Vector2.zero;
            heroInGoPool.Add(dungeonHeroListGo[dungeonID].transform.GetChild(i).gameObject);
        }

        int heroNum = 0;
        for (int i = 0; i < gc.dungeonList[dungeonID].teamList.Count; i++)
        {
            heroNum += gc.adventureTeamList[gc.dungeonList[dungeonID].teamList[i]].heroIDList.Count;
            for (int j = 0; j < gc.adventureTeamList[gc.dungeonList[dungeonID].teamList[i]].heroIDList.Count; j++)
            {
                //TODO：？
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
                    if (heroInGoPool.Count > 0)
                    {
                        go = heroInGoPool[0];
                        heroInGoPool[0].transform.GetComponent<RectTransform>().localScale = Vector2.one;
                        heroInGoPool.RemoveAt(0);
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

    //显示收入数字
    public void ShowNumText(short districtID, string content)
    {
        GameObject go;
        if (numPool.Count > 0)
        {
            go = numPool[0];
            numPool.RemoveAt(0);
        }
        else
        {
            go = Instantiate(Resources.Load("Prefab/Moment/Moment_Text")) as GameObject;
            go.transform.SetParent(numGo.transform);
            go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            go.GetComponent<RectTransform>().anchorMax= new Vector2(0, 1);
            go.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            go.GetComponent<MomentText>().pool = numPool;
        }

        go.GetComponent<RectTransform>().anchoredPosition = districtGo[districtID].GetComponent<RectTransform>().anchoredPosition;

        go.GetComponent<MomentText>().Play(content, districtGo[districtID].GetComponent<RectTransform>().anchoredPosition);
    }

    #region 【方法组】地图拖拽
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
        transform.DOComplete();
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
        //Debug.Log(rt.position);
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
    #endregion
}