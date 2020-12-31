using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureMainPanel : BasePanel
{
    public static AdventureMainPanel Instance;

    GameControl gc;
    public GameObject teamListGo;

    public RectTransform dungeonRt;
    public GameObject dungeonListGo;
    public Button dungeonBtn;

    public Button closeBtn;

 

    //对象池
    List<GameObject> adventureTeamGo = new List<GameObject>();
    List<GameObject> dungeonGo = new List<GameObject>();
    public List<GameObject> effectPool = new List<GameObject>();
    public List<GameObject> numPool = new List<GameObject>();

    List<AdventureTeamBlock> adventureTeamBlocks = new List<AdventureTeamBlock>();

    List<float> rollCount = new List<float> { 0f, 0f, 0f, 0f, 0f};
    public short nowSelectDungeonID = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("GetKeyDown");
          
        }
        for (byte i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (gc.adventureTeamList[i].action ==  AdventureAction.Walk)
            {
                UpdateSceneBar(i);
            }
        }
    }


    public  void OnShow(int x ,int y)
    {
        UpdateAllInfo();
        HideDungeonPage();
        SetAnchoredPosition(x, y);
        isShow = true;
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }

    public void UpdateAllInfo()
    {
        GameObject go;

        for (byte i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (i < adventureTeamGo.Count)
            {
                go = adventureTeamGo[i];
                adventureTeamGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UIBlock/Block_AdventureTeam")) as GameObject;
                go.transform.SetParent(teamListGo.transform);
                adventureTeamGo.Add(go);
            }

            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * 404f, 0f, 0f);
            UpdateTeam(i);
            HideGets(i);
            if (i < adventureTeamBlocks.Count)
            {
                adventureTeamBlocks[i] = go.GetComponent<AdventureTeamBlock>();
            }
            else
            {
                adventureTeamBlocks.Add(go.GetComponent<AdventureTeamBlock>());
            }
        }
        for (int i = gc.adventureTeamList.Count; i < adventureTeamGo.Count; i++)
        {
            adventureTeamGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }

        teamListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(260f* gc.adventureTeamList.Count, 488f);

    }

    //更新单个队伍块信息
    public void UpdateTeam(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        adventureTeamBlock.titleText.text = "第" + (teamID + 1) + "探险队";
        if (teamID == 0)
        {
            adventureTeamBlock.titleText.text += "(近卫队)";
        }

        //Debug.Log("UpdateTeam() teamID=" + teamID + " gc.adventureTeamList[teamID].state=" + gc.adventureTeamList[teamID].state);
        if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
        {
            if (gc.adventureTeamList[teamID].dungeonID != -1)
            {
                adventureTeamBlock.dungeon_nameText.text ="城镇 - 目的地:"+ DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name;

                adventureTeamBlock.dungeon_sceneBgRt[0].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[0].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_F", typeof(Sprite)) as Sprite;
                
                //for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
                //{
                //    adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                //    adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
                //}

                adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.one;
                adventureTeamBlock.dungeon_destinationImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_B", typeof(Sprite)) as Sprite;



            }
            else
            {
                adventureTeamBlock.dungeon_nameText.text = "城镇 - 未指定目的地";

                adventureTeamBlock.dungeon_sceneBgRt[0].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[0].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_F", typeof(Sprite)) as Sprite;

                adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;

            }
            adventureTeamBlock.dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlock.dungeon_fgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            UpdateProgress(teamID);
          
            adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.dungeon_selectBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.dungeon_selectBtn.onClick.AddListener(delegate ()
            {
                ShowDungeonPage(teamID);
            });

            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            //TeamLogAdd()
            TeamLogShow(teamID);
            //go.GetComponent<AdventureTeamBlock>().contentText.text = gc.adventureTeamList[teamID].log;

            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "出发";
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                gc.AdventureTeamSend(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "[击杀 " + gc.adventureTeamList[teamID].killNum + "]";

            //滚动背景图设置
            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
            }
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.zero;

            UpdateProgress(teamID);
            UpdateTeamHero(teamID);

            UpdateSceneRoleFormations(teamID);
            switch (gc.adventureTeamList[teamID].action)
            {
                case AdventureAction.Walk:
                    HideSceneRoleHpMp(teamID);

                    adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    adventureTeamBlock.retreatBtn.onClick.RemoveAllListeners();
                    adventureTeamBlock.retreatBtn.onClick.AddListener(delegate ()
                    {
                        gc.AdventureTeamBack(teamID, AdventureState.Retreat);
                    });
                    break;
                case AdventureAction.Fight:
                    UpdateSceneRoleHpMp(teamID, gc.fightMenberObjectSS[teamID]);
                    UpdateSceneRole(teamID);
                    UpdateSceneEnemy(teamID);

                    adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
    

                    break;
                case AdventureAction.GetSomething:
                    HideSceneRoleHpMp(teamID);
                    adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                    break;
            }




            TeamLogShow(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.detailBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.detailBtn.onClick.AddListener(delegate ()
            {
                /*详情*/
            });
            
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "战利品";
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                ShowGets(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Done)
        {
            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name+"(终点)";

            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
            }
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "结算";
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                ShowGets(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Fail)
        {
            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "<color=red>(失败)</color>";

            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
            }
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "结算";
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                ShowGets(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Retreat)
        {
            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "<color=red>(探索中止)</color>";

            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
            }
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "结算";
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                ShowGets(teamID);
            });
        }
    }

    //更新三个英雄信息格子（先锋，中军，后卫）
    public void UpdateTeamHero(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        for (int j = 0; j < gc.adventureTeamList[teamID].heroIDList.Count; j++)
        {
            int heroID = gc.adventureTeamList[teamID].heroIDList[j];
            adventureTeamBlock.dungeon_side0Go[j].GetComponent<AnimatiorControl>().SetCharaFrames( gc.heroDic[heroID].pic);

            adventureTeamBlock.hero_picImage[j].overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[heroID].pic + "/Pic", typeof(Sprite)) as Sprite;
            adventureTeamBlock.hero_nameText[j].text = gc.heroDic[heroID].name + "\nLv." + gc.heroDic[heroID].level;
            adventureTeamBlock.hero_hpmpText[j].text = "<color=#76ee00>体力 " + gc.adventureTeamList[teamID].heroHpList[j] + "/" + gc.GetHeroAttr(Attribute.Hp, heroID) + "</color>\n<color=#428DFD>魔力 " + gc.adventureTeamList[teamID].heroMpList[j] + "/" + gc.GetHeroAttr(Attribute.Mp, heroID) + "</color>";

            if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
            {
                adventureTeamBlock.hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_down", typeof(Sprite)) as Sprite;
                adventureTeamBlock.hero_setBtn[j].onClick.RemoveAllListeners();
                adventureTeamBlock.hero_setBtn[j].onClick.AddListener(delegate ()
                {
                    gc.AdventureTeamHeroMinus(teamID, heroID);
                    UpdateTeamHero(teamID);
                });
            }
            else
            {
                adventureTeamBlock.hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                adventureTeamBlock.hero_setBtn[j].onClick.RemoveAllListeners();
            }


        }
        for (int j = gc.adventureTeamList[teamID].heroIDList.Count; j < 3; j++)
        {
            adventureTeamBlock.dungeon_side0Go[j].GetComponent<Image>().sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;


            if (j == gc.adventureTeamList[teamID].heroIDList.Count&& gc.adventureTeamList[teamID].state == AdventureState.NotSend)
            {
                adventureTeamBlock.hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_up", typeof(Sprite)) as Sprite;
                adventureTeamBlock.hero_setBtn[j].onClick.RemoveAllListeners();
                adventureTeamBlock.hero_setBtn[j].onClick.AddListener(delegate ()
                {
                    HeroSelectPanel.Instance.OnShow("指派探险者", teamID, -1, 1, (int)(gameObject.GetComponent<RectTransform>().sizeDelta.x + gameObject.GetComponent<RectTransform>().anchoredPosition.x + GameControl.spacing), (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.y));
                });
            }
            else
            {
                adventureTeamBlock.hero_setBtn[j].onClick.RemoveAllListeners();
                adventureTeamBlock.hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            }

            adventureTeamBlock.hero_picImage[j].overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            adventureTeamBlock.hero_nameText[j].text = "";
            adventureTeamBlock.hero_hpmpText[j].text = "";

        }
    }

    //更新单个英雄信息格子 仅HPMP
    public void UpdateHeroHpMpSingle(byte teamID, FightMenberObject fightMenberObject)
    {

        if (fightMenberObject.side == 0)
        {
            AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
            adventureTeamBlock.hero_hpmpText[fightMenberObject.sideIndex].text = "<color=#76ee00>体力 " + fightMenberObject.hpNow + "/" + fightMenberObject.hp + "</color>\n<color=#428DFD>魔力 " + fightMenberObject.mpNow + "/" + fightMenberObject.mp + "</color>";
        }
    }

    //打开选择目的地界面
    public void ShowDungeonPage(byte teamID)
    {
        dungeonRt.localScale = Vector2.one;

        List<int> dungeonID = new List<int> { };
        for (int i = 0; i < gc.dungeonList.Count; i++)
        {
            if (gc.dungeonList[i].unlock)
            {
                dungeonID.Add(i);
            }
        }

        int columns = 3;

        GameObject go;

        for (int i = 0; i < dungeonID.Count; i++)
        {
            if (i < dungeonGo.Count)
            {
                go = dungeonGo[i];
                dungeonGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Dungeon")) as GameObject;
                go.transform.SetParent(dungeonListGo.transform);
                dungeonGo.Add(go);
            }
            int row = i == 0 ? 0 : (i % columns);
            int col = i == 0 ? 0 : (i / columns);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f + row * 262f, -4 + col * -102f);
            go.transform.GetChild(0).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + DataManager.mDungeonDict[dungeonID[i]].ScenePic[0]+"_B", typeof(Sprite)) as Sprite;
            go.transform.GetChild(1).GetComponent<Text>().text = "已开放";
            go.transform.GetChild(2).GetComponent<Text>().text = DataManager.mDungeonDict[dungeonID[i]].Name;
            string str = "";
            for (int j = 0; j < DataManager.mDungeonDict[dungeonID[i]].MonsterID.Count; j++)
            {
                str += DataManager.mMonsterDict[DataManager.mDungeonDict[dungeonID[i]].MonsterID[j]].Name+" ";
            }

            go.transform.GetChild(3).GetComponent<Text>().text ="地图等级 "+ DataManager.mDungeonDict[dungeonID[i]].Level+" / 旅程 "+(DataManager.mDungeonDict[dungeonID[i]].PartNum*100)+"M\n"+ DataManager.mDungeonDict[dungeonID[i]].Des+"\n出现怪物 "+str;
            go.GetComponent<InteractiveLabel>().index = dungeonID[i];


        }
        dungeonListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(260*3f, Mathf.Max(413f, 4 + (dungeonID.Count / columns) * 100f));

        dungeonBtn.onClick.RemoveAllListeners();
        dungeonBtn.onClick.AddListener(delegate ()
        {
            gc.AdventureTeamSetDungeon(teamID, nowSelectDungeonID);
            HideDungeonPage();
            UpdateTeam(teamID);
        });
    }
    //关闭选择目的地界面
    public void HideDungeonPage()
    {
        dungeonRt.localScale = Vector2.zero;
    }

    //日志框更新
    public void TeamLogAdd(byte teamID,string str)
    {


        gc.adventureTeamList[teamID].log.Add(str) ;
        TeamLogShow(teamID);

       // Debug.Log(adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().contentText.text.Length);
    }
    public void TeamLogShow(byte teamID)
    {
        const int MaxRow=50;
        adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().contentText.text = "";

        for (int i = gc.adventureTeamList[teamID].log.Count - 1; i >=System.Math.Max(0, gc.adventureTeamList[teamID].log.Count- MaxRow); i--)
        {
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().contentText.text +=  gc.adventureTeamList[teamID].log[i]+ "\n"  ;
        }
    }

    public void ShowGets(byte teamID)
    {
        string str = "";
        if (gc.adventureTeamList[teamID].getExp != 0)
        {
            str += "[经验值:"+ gc.adventureTeamList[teamID].getExp + "] ";
        }
        if (gc.adventureTeamList[teamID].getGold != 0)
        {
            str += "[金币:" + gc.adventureTeamList[teamID].getGold + "] ";
        }
        if (gc.adventureTeamList[teamID].getCereal != 0)
        {
            str += "[谷物*" + gc.adventureTeamList[teamID].getCereal + "] ";
        }
        if (gc.adventureTeamList[teamID].getVegetable != 0)
        {
            str += "[蔬菜*" + gc.adventureTeamList[teamID].getVegetable + "] ";
        }
        if (gc.adventureTeamList[teamID].getFruit != 0)
        {
            str += "[水果*" + gc.adventureTeamList[teamID].getFruit + "] ";
        }
        if (gc.adventureTeamList[teamID].getMeat != 0)
        {
            str += "[肉类*" + gc.adventureTeamList[teamID].getMeat + "] ";
        }
        if (gc.adventureTeamList[teamID].getFish != 0)
        {
            str += "[鱼类*" + gc.adventureTeamList[teamID].getFish + "] ";
        }
        if (gc.adventureTeamList[teamID].getWood != 0)
        {
            str += "[木材*" + gc.adventureTeamList[teamID].getWood + "] ";
        }
        if (gc.adventureTeamList[teamID].getStone != 0)
        {
            str += "[石料*" + gc.adventureTeamList[teamID].getStone + "] ";
        }
        if (gc.adventureTeamList[teamID].getMetal != 0)
        {
            str += "[金属*" + gc.adventureTeamList[teamID].getMetal + "] ";
        }
        if (gc.adventureTeamList[teamID].getLeather != 0)
        {
            str += "[皮革*" + gc.adventureTeamList[teamID].getLeather + "] ";
        }
        if (gc.adventureTeamList[teamID].getCloth != 0)
        {
            str += "[布料*" + gc.adventureTeamList[teamID].getCloth + "] ";
        }
        if (gc.adventureTeamList[teamID].getTwine != 0)
        {
            str += "[麻绳*" + gc.adventureTeamList[teamID].getTwine + "] ";
        }
        if (gc.adventureTeamList[teamID].getBone != 0)
        {
            str += "[骨块*" + gc.adventureTeamList[teamID].getBone + "] ";
        }

        for (int i = 0; i < gc.adventureTeamList[teamID].getItemList.Count; i++)
        {
            str += "[" +DataManager.mItemDict[ gc.adventureTeamList[teamID].getItemList[i]].Name + "] ";
        }

        adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().gets_contentText.text = str;

        adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().getsRt.localScale = Vector2.one;
        adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().gets_confrimBtn.onClick.RemoveAllListeners();
        if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().gets_confrimBtn.transform.GetChild(0).GetComponent<Text>().text = "关闭";
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().gets_confrimBtn.onClick.AddListener(delegate ()
            {
                HideGets(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Done||
            gc.adventureTeamList[teamID].state == AdventureState.Fail||
            gc.adventureTeamList[teamID].state == AdventureState.Retreat)
        {
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().gets_confrimBtn.transform.GetChild(0).GetComponent<Text>().text = "领取";
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().gets_confrimBtn.onClick.AddListener(delegate ()
            {
                gc.AdventureTakeGets(teamID);
                HideGets(teamID);
            });
        }
        
    }
    public void HideGets(byte teamID)
    {
        adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().getsRt.localScale = Vector2.zero;
    }


    //场景背景滚动
    public void UpdateSceneBar(byte teamID)
    {
        //Debug.Log("开始滚动背景");adventureTeamBlocks[teamID]
        //AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition+= 50f* Vector2.left* Time.deltaTime;
        adventureTeamBlocks[teamID].dungeon_fgListGo.GetComponent<RectTransform>().anchoredPosition += 50f * Vector2.left * Time.deltaTime;

        rollCount[teamID] += 50f * Time.deltaTime;
        if (rollCount[teamID] > 20f)
        {
            gc.adventureTeamList[teamID].nowDay++;
            UpdateProgress(teamID);
            if (gc.adventureTeamList[teamID].nowDay>=DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum)
            {
                gc.AdventureTeamBack(teamID, AdventureState.Done);
            }

            rollCount[teamID] = 0f;
        }

        if (adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition.x <= -384f)
        {
            

            adventureTeamBlocks[teamID].dungeon_progressText.text = ((float)gc.adventureTeamList[teamID].nowDay / DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum * 100) + "%";
            adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[1].anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[2].anchoredPosition = new Vector2(384f, 0);
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[0].anchoredPosition = new Vector2(768f, 0);
            RectTransform temp = adventureTeamBlocks[teamID].dungeon_sceneBgRt[0];
            adventureTeamBlocks[teamID].dungeon_sceneBgRt.RemoveAt(0);
            adventureTeamBlocks[teamID].dungeon_sceneBgRt.Add(temp);

            adventureTeamBlocks[teamID].dungeon_fgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneFgRt[1].anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneFgRt[2].anchoredPosition = new Vector2(384f, 0);
            adventureTeamBlocks[teamID].dungeon_sceneFgRt[0].anchoredPosition = new Vector2(768f, 0);
             temp = adventureTeamBlocks[teamID].dungeon_sceneFgRt[0];
            adventureTeamBlocks[teamID].dungeon_sceneFgRt.RemoveAt(0);
            adventureTeamBlocks[teamID].dungeon_sceneFgRt.Add(temp);
        }
        
    }

    //更新进度值，进度条，旗子标记
    void UpdateProgress(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        if (gc.adventureTeamList[teamID].dungeonID != -1)
        {
            float rate = (float)gc.adventureTeamList[teamID].nowDay / DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum;
            adventureTeamBlock.dungeon_progressText.text = (int)(rate * 100) + "%";
            adventureTeamBlock.dungeon_progressNowBarRt.sizeDelta = new Vector2(rate*384, 4f);
            adventureTeamBlock.dungeon_progressNowFlagRt.anchoredPosition = new Vector2(rate*384+5, -1f);
        }
        else
        {
            adventureTeamBlock.dungeon_progressText.text = "";
            adventureTeamBlock.dungeon_progressNowBarRt.sizeDelta =Vector2.zero;
            adventureTeamBlock.dungeon_progressNowFlagRt.anchoredPosition = Vector2.zero;
        }
        
    }


    //更新场景敌人图片资源集、动作
    public void UpdateSceneEnemy(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (int i = 0; i < gc.adventureTeamList[teamID].enemyIDList.Count; i++)
        {
            if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
            {
                //TODO:怪物图集处理
                adventureTeamBlock.dungeon_side1Go[i].GetComponent<AnimatiorControl>().SetCharaFramesSimple(DataManager.mMonsterDict[gc.adventureTeamList[teamID].enemyIDList[i]].Pic);
                adventureTeamBlock.dungeon_side1Go[i].GetComponent<Image>().sprite = Resources.Load("Image/RolePic/" +DataManager.mMonsterDict[gc.adventureTeamList[teamID].enemyIDList[i]].Pic + "/Pic", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_side1Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkLeft);
            }
        }
        for (int i = gc.adventureTeamList[teamID].enemyIDList.Count; i < 3; i++)
        {
            adventureTeamBlock.dungeon_side1Go[i].GetComponent<Image>().sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;

        }
    }

    //更新场景人物图片资源集、动作
    public void UpdateSceneRole(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (int i = 0; i < gc.adventureTeamList[teamID].heroIDList.Count; i++)
        {
            if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
            {
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Front);
            }
            else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
            {
                if (gc.adventureTeamList[teamID].action == AdventureAction.Walk)
                {
                    adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkRight);
                }
                else if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
                {
                    adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Idle);
                }
            }
            else if (gc.adventureTeamList[teamID].state == AdventureState.Done)
            {
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Front);
            }
            else if (gc.adventureTeamList[teamID].state == AdventureState.Fail)
            {
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Death);
            }
            else if (gc.adventureTeamList[teamID].state == AdventureState.Fail)
            {
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Front);
            }
        }
    }
    
    //更新场景全部人物血条蓝条
    public void UpdateSceneRoleHpMp(byte teamID, List<FightMenberObject> fightMenberObjects)
    {
        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            UpdateSceneRoleHpMpSingle(teamID, fightMenberObjects[i]);
        }
    }

    //更新场景单个人物血条蓝条
    public void UpdateSceneRoleHpMpSingle(byte teamID, FightMenberObject fightMenberObject)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        if (fightMenberObject.side == 0)
        {
            adventureTeamBlock.dungeon_side0HpRt[fightMenberObject.sideIndex].sizeDelta = new Vector2(32f * fightMenberObject.hpNow / fightMenberObject.hp, 3f);
            adventureTeamBlock.dungeon_side0MpRt[fightMenberObject.sideIndex].sizeDelta = new Vector2(32f * fightMenberObject.mpNow / fightMenberObject.mp, 3f);
        }
        else if (fightMenberObject.side == 1)
        {
            adventureTeamBlock.dungeon_side1HpRt[fightMenberObject.sideIndex].sizeDelta = new Vector2(32f * fightMenberObject.hpNow / fightMenberObject.hp, 3f);
            adventureTeamBlock.dungeon_side1MpRt[fightMenberObject.sideIndex].sizeDelta = new Vector2(32f * fightMenberObject.mpNow / fightMenberObject.mp, 3f);
        }
    }

    //隐藏场景全部人物(包括敌人)血条蓝条
    public void HideSceneRoleHpMp(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (byte i = 0; i < 3; i++)
        {
            adventureTeamBlock.dungeon_side0HpRt[i].sizeDelta = Vector2.zero;
            adventureTeamBlock.dungeon_side0MpRt[i].sizeDelta = Vector2.zero;

            adventureTeamBlock.dungeon_side1HpRt[i].sizeDelta = Vector2.zero;
            adventureTeamBlock.dungeon_side1MpRt[i].sizeDelta = Vector2.zero;
        }
    }

    //更新场景人物队形
    public void UpdateSceneRoleFormations(byte teamID)
    {
       // Debug.Log("UpdateSceneRoleFormations() gc.adventureTeamList[teamID].state ="+ gc.adventureTeamList[teamID].state+ " gc.adventureTeamList[teamID].action="+ gc.adventureTeamList[teamID].action);

        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        if (gc.adventureTeamList[teamID].state == AdventureState.NotSend||
            gc.adventureTeamList[teamID].state == AdventureState.Done||
            gc.adventureTeamList[teamID].state == AdventureState.Retreat||
            gc.adventureTeamList[teamID].state == AdventureState.Fail)
        {
            adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, 40);
            adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 40);
            adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, 40);

            adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            if (gc.adventureTeamList[teamID].action == AdventureAction.Walk)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, 40);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 40);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, 40);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            }
            else if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-90 , 40);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-60,15);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 65 );

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 40);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 15);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 65);
            }
            else if (gc.adventureTeamList[teamID].action == AdventureAction.GetSomething)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39,40);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 40);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, 40);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(78, 40);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<Image>().sprite = Resources.Load("Image/Other/icon268", typeof(Sprite)) as Sprite;
            }
        }
    }


    //设置场景人物/敌人动画
    public void SetAnim(byte teamID,byte side,byte index, AnimStatus animStatus)
    {
        //Debug.Log("SetAnim() teamID=" + teamID + " side=" + side + " index=" + index + " animStatus=" + animStatus);

        if (side == 0)
        {
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side0Go[index].GetComponent<AnimatiorControl>().SetAnim(animStatus);
        }
        else if (side == 1)
        {
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side1Go[index].GetComponent<AnimatiorControl>().SetAnim(animStatus);
        }

    }

    //显示伤害数字
    public void ShowDamageText(byte teamID, byte side, byte index, string content)
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
          
        }
        go.transform.SetParent(adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_numLayerGo.transform);
        Vector2 targetLocation = Vector2.zero;
        if (side == 0)
        {
            targetLocation = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side0Go[index].GetComponent<RectTransform>().anchoredPosition;
        }
        else if (side == 1)
        {
            targetLocation = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side1Go[index].GetComponent<RectTransform>().anchoredPosition;
        }

        go.GetComponent<MomentText>().Play(content, targetLocation);
    }
   
    //显示特效
    public void ShowEffect(byte teamID, byte side, byte index, string effectName)
    {
        GameObject go;
        if (effectPool.Count > 0)
        {
            go = effectPool[0];
            
            effectPool.RemoveAt(0);
        }
        else
        {
            go = Instantiate(Resources.Load("Prefab/Moment/Moment_Effect")) as GameObject;
        }
        go.transform.SetParent(adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_effectLayerGo.transform);

        Vector2 targetLocation=Vector2.zero;
        if (side == 0)
        {
            targetLocation = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side0Go[index].GetComponent<RectTransform>().anchoredPosition;
        }
        else if (side == 1)
        {
            targetLocation = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side1Go[index].GetComponent<RectTransform>().anchoredPosition;
        }
        go.GetComponent<MomentEffect>().Play(effectName, targetLocation);
    }




    
}
