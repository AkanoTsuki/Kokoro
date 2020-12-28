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


        }
        for (int i = gc.adventureTeamList.Count; i < adventureTeamGo.Count; i++)
        {
            adventureTeamGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }

        teamListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(260f* gc.adventureTeamList.Count, 488f);

    }

    public void UpdateTeam(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        adventureTeamBlock.titleText.text = "第" + (teamID + 1) + "探险队";
        if (teamID == 0)
        {
            adventureTeamBlock.titleText.text += "(近卫队)";
        }

        if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
        {
            if (gc.adventureTeamList[teamID].dungeonID != -1)
            {
                adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name;
                adventureTeamBlock.dungeon_progressText.text = "";
                //adventureTeamBlock.dungeon_sceneBgRt.Clear();
                //adventureTeamBlock.dungeon_sceneFgRt.Clear();
                for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
                {
                    adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                    adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
                    //adventureTeamBlock.dungeon_sceneBgRt.Add(adventureTeamBlock.dungeon_bgListGo.transform.GetChild(i).GetComponent<RectTransform>());
                    //adventureTeamBlock.dungeon_sceneFgRt.Add(adventureTeamBlock.dungeon_fgListGo.transform.GetChild(i).GetComponent<RectTransform>());

                }

                adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                adventureTeamBlock.dungeon_nameText.text = "未选择地点";
                adventureTeamBlock.dungeon_progressText.text = "";
                adventureTeamBlock.dungeon_sceneBgRt[0].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[0].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_F", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.dungeon_selectBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.dungeon_selectBtn.onClick.AddListener(delegate ()
            {
                ShowDungeonPage(teamID);
            });

            UpdateSceneRoleFormations(teamID);

            UpdateTeamHero(teamID);
            //TeamLogAdd()
            TeamLogShow(teamID);
            //go.GetComponent<AdventureTeamBlock>().contentText.text = gc.adventureTeamList[teamID].log;

            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                gc.AdventureTeamSend(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name;
            adventureTeamBlock.dungeon_progressText.text = "%";
            //adventureTeamBlock.dungeon_sceneBgRt.Clear();
            //adventureTeamBlock.dungeon_sceneFgRt.Clear();
            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
                //adventureTeamBlock.dungeon_sceneBgRt.Add(adventureTeamBlock.dungeon_bgListGo.transform.GetChild(i).GetComponent<RectTransform>());
                //adventureTeamBlock.dungeon_sceneFgRt.Add(adventureTeamBlock.dungeon_fgListGo.transform.GetChild(i).GetComponent<RectTransform>());

            }

            adventureTeamBlock.dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            UpdateTeamHero(teamID);
            //go.GetComponent<AdventureTeamBlock>().contentText.text = gc.adventureTeamList[teamID].log;
            TeamLogShow(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.detailBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.detailBtn.onClick.AddListener(delegate ()
            {
               /*详情*/
            });
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.retreatBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.retreatBtn.onClick.AddListener(delegate ()
            {
                /*撤退*/
            });
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            
        }


    }

    public void UpdateTeamHero(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        for (int j = 0; j < gc.adventureTeamList[teamID].heroIDList.Count; j++)
        {
            int heroID = gc.adventureTeamList[teamID].heroIDList[j];


            adventureTeamBlock.dungeon_side0Go[j].GetComponent<Image>().sprite = Resources.Load("Image/RolePic/" + gc.heroDic[heroID].pic + "/Pic", typeof(Sprite)) as Sprite;
            adventureTeamBlock.dungeon_side0Go[j].GetComponent<AnimatiorControl>().SetCharaFrames( gc.heroDic[heroID].pic);

            adventureTeamBlock.hero_picImage[j].overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[heroID].pic + "/Pic", typeof(Sprite)) as Sprite;
            adventureTeamBlock.hero_nameText[j].text = gc.heroDic[heroID].name + "\nLv." + gc.heroDic[heroID].level;

            adventureTeamBlock.hero_hpmpText[j].text = "<color=#76ee00>体力 " + gc.adventureTeamList[teamID].heroHpList[j] + "/" + gc.GetHeroAttr(Attribute.Hp, heroID) + "</color>\n<color=#428DFD>魔力 " + gc.adventureTeamList[teamID].heroMpList[j] + "/" + gc.GetHeroAttr(Attribute.Mp, heroID) + "</color>";
            adventureTeamBlock.hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_down", typeof(Sprite)) as Sprite;
            adventureTeamBlock.hero_setBtn[j].onClick.RemoveAllListeners();
            adventureTeamBlock.hero_setBtn[j].onClick.AddListener(delegate () {
                gc.AdventureTeamHeroMinus(teamID,heroID);
                UpdateTeamHero(teamID);
            });
        }
        for (int j = gc.adventureTeamList[teamID].heroIDList.Count; j < 3; j++)
        {
            adventureTeamBlock.dungeon_side0Go[j].GetComponent<Image>().sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;


            if (j == gc.adventureTeamList[teamID].heroIDList.Count)
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

            go.transform.GetChild(3).GetComponent<Text>().text ="地图等级 "+ DataManager.mDungeonDict[dungeonID[i]].Level+" / 耗时 "+ DataManager.mDungeonDict[dungeonID[i]].PartNum+"\n"+ DataManager.mDungeonDict[dungeonID[i]].Des+"\n出现怪物 "+str;
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

    //场景背景滚动
    public void UpdateSceneBar(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        adventureTeamBlock.dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition+= 50f* Vector2.left* Time.deltaTime;
        adventureTeamBlock.dungeon_fgListGo.GetComponent<RectTransform>().anchoredPosition += 50f * Vector2.left * Time.deltaTime;

        if (adventureTeamBlock.dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition.x <= -384f)
        {
            adventureTeamBlock.dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlock.dungeon_sceneBgRt[1].anchoredPosition = Vector2.zero;
            adventureTeamBlock.dungeon_sceneBgRt[2].anchoredPosition = new Vector2(384f, 0);
            adventureTeamBlock.dungeon_sceneBgRt[0].anchoredPosition = new Vector2(768f, 0);
            RectTransform temp = adventureTeamBlock.dungeon_sceneBgRt[0];
            adventureTeamBlock.dungeon_sceneBgRt.RemoveAt(0);
            adventureTeamBlock.dungeon_sceneBgRt.Add(temp);

            adventureTeamBlock.dungeon_fgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlock.dungeon_sceneFgRt[1].anchoredPosition = Vector2.zero;
            adventureTeamBlock.dungeon_sceneFgRt[2].anchoredPosition = new Vector2(384f, 0);
            adventureTeamBlock.dungeon_sceneFgRt[0].anchoredPosition = new Vector2(768f, 0);
             temp = adventureTeamBlock.dungeon_sceneFgRt[0];
            adventureTeamBlock.dungeon_sceneFgRt.RemoveAt(0);
            adventureTeamBlock.dungeon_sceneFgRt.Add(temp);
        }
        
    }

    //场景敌人状态
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

    //场景人物状态
    public void UpdateSceneRole(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (int i = 0; i < gc.adventureTeamList[teamID].heroIDList.Count; i++)
        {
            if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
            {
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().Stop();
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<Image>().sprite = Resources.Load("Image/RolePic/" + gc.heroDic[gc.adventureTeamList[teamID].heroIDList[i]].pic + "/Pic", typeof(Sprite)) as Sprite;
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
        }
    }

    //场景人物队形
    public void UpdateSceneRoleFormations(byte teamID)
    {
        Debug.Log("UpdateSceneRoleFormations() gc.adventureTeamList[teamID].state ="+ gc.adventureTeamList[teamID].state+ " gc.adventureTeamList[teamID].action="+ gc.adventureTeamList[teamID].action);

        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
        {
            adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, -10);
            adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -10);
            adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, -10);

            adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            if (gc.adventureTeamList[teamID].action == AdventureAction.Walk)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, -10);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -10);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, -10);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            }
            else if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-90 , -10);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, -40);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 20 );

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(90, -10);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(60, -40);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 20);
            }
            else if (gc.adventureTeamList[teamID].action == AdventureAction.GetSomething)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, -10);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -10);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, -10);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            }
        }
    }

    public void SetAnim(byte teamID,byte side,byte index, AnimStatus animStatus)
    {
        if (side == 0)
        {
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side0Go[index].GetComponent<AnimatiorControl>().SetAnim(animStatus);
        }
        else if (side == 1)
        {
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side1Go[index].GetComponent<AnimatiorControl>().SetAnim(animStatus);
        }

    }

    public void ShowDamageText(byte teamID, byte side, byte index,int value)
    {
        
    }

    public void HideDungeonPage()
    {
        dungeonRt.localScale = Vector2.zero;
    }
}
