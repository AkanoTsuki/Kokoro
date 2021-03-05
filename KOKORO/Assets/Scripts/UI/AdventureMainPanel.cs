﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
public class AdventureMainPanel : BasePanel
{
    public static AdventureMainPanel Instance;

    GameControl gc;

    #region 【UI控件】
    public GameObject teamListGo;

    public Button closeBtn;
    #endregion

    //运行变量
    List<AdventureTeamBlock> adventureTeamBlocks = new List<AdventureTeamBlock>();
    List<float> rollCount = new List<float> { 0f, 0f, 0f, 0f, 0f };
    public short nowCheckingTeamID = -1;

    //对象池
    List<GameObject> adventureTeamGo = new List<GameObject>();
    public List<GameObject> effectPool = new List<GameObject>();
    public List<GameObject> numPool = new List<GameObject>();
    public List<GameObject> talkPool = new List<GameObject>();

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
        for (byte i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (gc.adventureTeamList[i].action == AdventureAction.Walk)
            {
                UpdateSceneBar(i);
            }
            else if (gc.adventureTeamList[i].state== AdventureState.Sending|| gc.adventureTeamList[i].state == AdventureState.Backing)
            {
                UpdateSceneBarOnTheWay(i);
            }
        }
    }

    //主面板显示
    public void OnShow(byte teamID)
    {
        nowCheckingTeamID = teamID;

        UpdateAllInfo(teamID);

        if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
        {
            AudioControl.Instance.StopMusic();
            AudioControl.Instance.PlayMusic("08Thebattle");
        }

        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Instance.enabled = true;
        isShow = true;
    }

    //主面板关闭
    public override void OnHide()
    {
        if (AdventureTeamPanel.Instance.isShow)
        {
            AdventureTeamPanel.Instance.OnHide();
        }

        if (nowCheckingTeamID != -1)
        {
            if (gc.adventureTeamList[nowCheckingTeamID].action == AdventureAction.Fight)
            {
                AudioControl.Instance.StopMusic();
                AudioControl.Instance.PlayMusic(AudioControl.Instance.nowMusic);
            }
        }

        nowCheckingTeamID = -1;

        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        Instance.enabled = false;
        isShow = false;
    }

    //默认信息更新
    public void UpdateAllInfo(byte teamID)
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

            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
            if (i == teamID)
            {               
                //go.SetActive(true);
                go.GetComponent<CanvasGroup>().alpha = 1f;
                go.GetComponent<CanvasGroup>().blocksRaycasts = true;
                UpdateTeam(i);
                HideGets(i);
            }
            else
            {
                //go.SetActive(false);
                go.GetComponent<CanvasGroup>().alpha = 0f;
                go.GetComponent<CanvasGroup>().blocksRaycasts = false;
                UpdateTeam(i);
            }
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

    //队伍块信息-单个-更新
    public void UpdateTeam(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        adventureTeamBlock.titleText.text = "第" + (teamID + 1) + "探险队";
        if (teamID == 0)
        {
            adventureTeamBlock.titleText.text += "(近卫队)";
        }
        adventureTeamBlock.dungeon_progressNowFlagRt.transform.GetComponent<Image>().sprite = Resources.Load("Image/Other/icon_flag_" + gc.forceDic[0].flagIndex + "_b", typeof(Sprite)) as Sprite;

        adventureTeamBlock.retreatBtn.onClick.RemoveAllListeners();
        adventureTeamBlock.retreatBtn.onClick.AddListener(delegate ()
        {
            if (gc.adventureTeamList[teamID].state != AdventureState.Retreat)
            {
                if (gc.adventureTeamList[teamID].action == AdventureAction.Walk)
                {
                    gc.AdventureTeamEnd(teamID, AdventureState.Retreat);
                }
                else
                {
                    gc.adventureTeamList[teamID].state = AdventureState.Retreat;
                }
             
            }
        });

        adventureTeamBlock.detailBtn.onClick.RemoveAllListeners();
        adventureTeamBlock.detailBtn.onClick.AddListener(delegate ()
        {
            /*详情*/
            AdventureTeamPanel.Instance.OnShow(teamID, (int)(GetComponent<RectTransform>().anchoredPosition.x+GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)GetComponent<RectTransform>().anchoredPosition.y);
        });

        if (gc.adventureTeamList[teamID].state == AdventureState.Free)
        {
            adventureTeamBlock.freeRt.localScale = Vector2.one;
            UpdateSceneRole(teamID);          
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Sending)
        {
            adventureTeamBlock.freeRt.localScale = Vector2.zero;
            

            adventureTeamBlock.dungeon_nameText.text = "派遣中 目的地:" + DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name;

            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_OnTheWay", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            }

            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().contentText.text = "派遣中";
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.one;
            adventureTeamBlock.dungeon_destinationImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].ScenePic[0] + "_B", typeof(Sprite)) as Sprite;
            adventureTeamBlock.dungeon_campRt.localScale = Vector2.zero;

            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            HideSceneRoleAp(teamID);
            HideSceneRoleSharpness(teamID);
            HideSceneRoleBuff(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            HideElementPoint(teamID);
            HideProgress(teamID);
            HideSceneRoleHalo(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Backing)
        {
            adventureTeamBlock.freeRt.localScale = Vector2.zero;
     
            adventureTeamBlock.dungeon_nameText.text = "返回中 目的地:" + DataManager.mDistrictDict[gc.adventureTeamList[teamID].districtID].Name;
            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_OnTheWay", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            }

            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().contentText.text = "返回中";
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.one;
            adventureTeamBlock.dungeon_destinationImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_B" , typeof(Sprite)) as Sprite;

            adventureTeamBlock.dungeon_campRt.localScale = Vector2.zero;

            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            HideSceneRoleAp(teamID);
            HideSceneRoleSharpness(teamID);
            HideSceneRoleBuff(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            HideElementPoint(teamID);
            HideProgress(teamID);
            HideSceneRoleHalo(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
        {
            adventureTeamBlock.freeRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "-营地";

            adventureTeamBlock.dungeon_sceneBgRt[0].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].ScenePic[0] + "_B", typeof(Sprite)) as Sprite;
            adventureTeamBlock.dungeon_sceneFgRt[0].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].ScenePic[0] + "_F", typeof(Sprite)) as Sprite;


            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_campRt.localScale = Vector2.one;

            adventureTeamBlock.dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlock.dungeon_fgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            HideSceneRoleAp(teamID);
            HideSceneRoleSharpness(teamID);
            HideSceneRoleBuff(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            HideElementPoint(teamID);
            //TeamLogAdd()
            UpdateLogContent(teamID);
            UpdateProgress(teamID);
            HideSceneRoleHalo(teamID);

            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "出发";
            adventureTeamBlock.startBtn.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon_advSend", typeof(Sprite)) as Sprite;
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                gc.AdventureTeamStart(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            adventureTeamBlock.freeRt.localScale = Vector2.zero;

            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "[击杀 " + gc.adventureTeamList[teamID].killNum + "]";

            //滚动背景图设置
            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
            }
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_campRt.localScale = Vector2.zero;


            UpdateProgress(teamID);
          
            UpdateTeamHero(teamID);
      
            UpdateSceneRoleFormations(teamID);
            switch (gc.adventureTeamList[teamID].action)
            {
                case AdventureAction.Walk:
                    HideSceneRoleHpMp(teamID);
                    HideSceneRoleAp(teamID);
                    HideSceneRoleSharpness(teamID);
                    HideSceneRoleBuff(teamID);
                    UpdateSceneRole(teamID);//
                    HideElementPoint(teamID);
                    HideSceneRoleHalo(teamID);

                    break;
                case AdventureAction.Fight:
                    UpdateSceneRoleHpMp(teamID, gc.fightMenberObjectSS[teamID]);
                    ShowSceneRoleAp(teamID, gc.fightMenberObjectSS[teamID]);
                    UpdateSceneRoleAp(teamID, gc.fightMenberObjectSS[teamID]);
                    UpdateSceneRoleApText(teamID, gc.fightMenberObjectSS[teamID]);
                    UpdateSceneRoleSharpness(teamID, gc.fightMenberObjectSS[teamID]);
                    UpdateSceneRoleBuff(teamID, gc.fightMenberObjectSS[teamID]);
                    UpdateSceneRole(teamID);
                    UpdateSceneEnemy(teamID);
                    UpdateElementPoint(teamID);
                    UpdateSceneRoleHalo(teamID, gc.fightMenberObjectSS[teamID]);
                    break;
                case AdventureAction.GetSomething:
                case AdventureAction.TrapHp:
                case AdventureAction.TrapMp:
                case AdventureAction.SpringHp:
                case AdventureAction.SpringMp:
                    HideSceneRoleHpMp(teamID);
                    HideSceneRoleAp(teamID);
                    HideSceneRoleSharpness(teamID);
                    HideSceneRoleBuff(teamID);
                    UpdateSceneRole(teamID);
                    UpdateSceneEnemy(teamID);
                    HideElementPoint(teamID);
                    HideSceneRoleHalo(teamID);
                    break;
            }



            UpdateLogContent(teamID);

            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.one;

            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.one;

            
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "战利品";
            adventureTeamBlock.startBtn.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon009", typeof(Sprite)) as Sprite;
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                ShowGets(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Done)
        {
            adventureTeamBlock.freeRt.localScale = Vector2.zero;

            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name+"(终点)";

            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
            }
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_campRt.localScale = Vector2.zero;
            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            HideSceneRoleAp(teamID);
            HideSceneRoleSharpness(teamID);
            HideSceneRoleBuff(teamID);
            HideSceneRoleHalo(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            HideElementPoint(teamID);
            UpdateLogContent(teamID);
            UpdateProgress(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "结算";
            adventureTeamBlock.startBtn.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon009", typeof(Sprite)) as Sprite;
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                ShowGets(teamID);
            });

          
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Fail)
        {
            adventureTeamBlock.freeRt.localScale = Vector2.zero;

            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "<color=red>(失败)</color>";

            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
            }
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_campRt.localScale = Vector2.zero;
            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            HideSceneRoleAp(teamID);
            HideSceneRoleSharpness(teamID);
            HideSceneRoleBuff(teamID);
            HideSceneRoleHalo(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            HideElementPoint(teamID);
            UpdateLogContent(teamID);
            UpdateProgress(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "结算";
            adventureTeamBlock.startBtn.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon009", typeof(Sprite)) as Sprite;
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                ShowGets(teamID);
            });
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Retreat)
        {
            adventureTeamBlock.freeRt.localScale = Vector2.zero;

            adventureTeamBlock.dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "<color=red>(探索中止)</color>";

            for (int i = 0; i < gc.adventureTeamList[teamID].scenePicList.Count; i++)
            {
                adventureTeamBlock.dungeon_sceneBgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_B", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_sceneFgRt[i].GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[i] + "_F", typeof(Sprite)) as Sprite;
            }
            adventureTeamBlock.dungeon_destinationRt.localScale = Vector2.zero;
            adventureTeamBlock.dungeon_campRt.localScale = Vector2.zero;
            UpdateSceneRoleFormations(teamID);
            HideSceneRoleHpMp(teamID);
            HideSceneRoleAp(teamID);
            HideSceneRoleSharpness(teamID);
            HideSceneRoleBuff(teamID);
            HideSceneRoleHalo(teamID);
            UpdateTeamHero(teamID);
            UpdateSceneRole(teamID);
            HideElementPoint(teamID);
            UpdateLogContent(teamID);
            UpdateProgress(teamID);
            adventureTeamBlock.detailBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.retreatBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.startBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.startBtn.transform.GetChild(0).GetComponent<Text>().text = "结算";
            adventureTeamBlock.startBtn.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon009", typeof(Sprite)) as Sprite;
            adventureTeamBlock.startBtn.onClick.RemoveAllListeners();
            adventureTeamBlock.startBtn.onClick.AddListener(delegate ()
            {
                ShowGets(teamID);
            });
        }
    }


    //动画场景-背景滚动（向地牢派遣中，返回据点中）
    public void UpdateSceneBarOnTheWay(byte teamID)
    {
        adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition += 50f * Vector2.left * Time.deltaTime;

        if (adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition.x <= -512f)
        {
            adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[1].anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[2].anchoredPosition = new Vector2(512f, 0);
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[0].anchoredPosition = new Vector2(1024f, 0);
            RectTransform temp = adventureTeamBlocks[teamID].dungeon_sceneBgRt[0];
            adventureTeamBlocks[teamID].dungeon_sceneBgRt.RemoveAt(0);
            adventureTeamBlocks[teamID].dungeon_sceneBgRt.Add(temp);
        }
    }

    //动画场景-背景滚动（地牢探索行进中）
    public void UpdateSceneBar(byte teamID)
    {
        adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition+= 50f* Vector2.left* Time.deltaTime;
        adventureTeamBlocks[teamID].dungeon_fgListGo.GetComponent<RectTransform>().anchoredPosition += 50f * Vector2.left * Time.deltaTime;

        rollCount[teamID] += 50f * Time.deltaTime;
        if (rollCount[teamID] > 20f)
        {
            gc.adventureTeamList[teamID].nowDay++;
            UpdateProgress(teamID);
            if (gc.adventureTeamList[teamID].nowDay >= DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum)
            {
                gc.AdventureTeamEnd(teamID, AdventureState.Done);
            }
            rollCount[teamID] = 0f;
        }

        if (adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition.x <= -512f)
        {
            adventureTeamBlocks[teamID].dungeon_progressText.text = ((float)gc.adventureTeamList[teamID].nowDay / DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum * 100) + "%";
            adventureTeamBlocks[teamID].dungeon_bgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[1].anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[2].anchoredPosition = new Vector2(512f, 0);
            adventureTeamBlocks[teamID].dungeon_sceneBgRt[0].anchoredPosition = new Vector2(1024f, 0);
            RectTransform temp = adventureTeamBlocks[teamID].dungeon_sceneBgRt[0];
            adventureTeamBlocks[teamID].dungeon_sceneBgRt.RemoveAt(0);
            adventureTeamBlocks[teamID].dungeon_sceneBgRt.Add(temp);

            adventureTeamBlocks[teamID].dungeon_fgListGo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneFgRt[1].anchoredPosition = Vector2.zero;
            adventureTeamBlocks[teamID].dungeon_sceneFgRt[2].anchoredPosition = new Vector2(512f, 0);
            adventureTeamBlocks[teamID].dungeon_sceneFgRt[0].anchoredPosition = new Vector2(1024f, 0);
             temp = adventureTeamBlocks[teamID].dungeon_sceneFgRt[0];
            adventureTeamBlocks[teamID].dungeon_sceneFgRt.RemoveAt(0);
            adventureTeamBlocks[teamID].dungeon_sceneFgRt.Add(temp);
        }
        
    }


    //动画场景-进度值，进度条，旗子标记-更新
    void UpdateProgress(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        if (gc.adventureTeamList[teamID].dungeonID != -1)
        {
            float rate = (float)gc.adventureTeamList[teamID].nowDay / DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum;
            adventureTeamBlock.dungeon_progressText.text = (int)(rate * 100) + "%";
            adventureTeamBlock.dungeon_progressNowBarRt.sizeDelta = new Vector2(rate*512, 4f);
            adventureTeamBlock.dungeon_progressNowFlagRt.anchoredPosition = new Vector2(rate*512+5, -1f);
        }
        else
        {
            adventureTeamBlock.dungeon_progressText.text = "";
            adventureTeamBlock.dungeon_progressNowBarRt.sizeDelta =Vector2.zero;
            adventureTeamBlock.dungeon_progressNowFlagRt.anchoredPosition = Vector2.zero;
        }
        
    }

    //动画场景-进度值，进度条，旗子标记-隐藏
    void HideProgress(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        adventureTeamBlock.dungeon_progressText.text = "";
        adventureTeamBlock.dungeon_progressNowBarRt.sizeDelta = Vector2.zero;
        adventureTeamBlock.dungeon_progressNowFlagRt.anchoredPosition = Vector2.zero;
    }


    //动画场景-人物图片资源集、动作（敌人）-更新配置
    public void UpdateSceneEnemy(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        adventureTeamBlock.dungeon_side1Go[0].GetComponent<Image>().color = Color.white;
        if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
        {
            for (int i = 0; i < gc.adventureTeamList[teamID].enemyIDList.Count; i++)
            {
                int hp = 0;
                for (int j = 0; j < gc.fightMenberObjectSS[teamID].Count; j++)
                {
                    if (gc.fightMenberObjectSS[teamID][j].side == 1 && gc.fightMenberObjectSS[teamID][j].sideIndex == i)
                    {
                        hp = gc.fightMenberObjectSS[teamID][j].hpNow;
                        break;
                    }
                }
                //TODO:怪物图集处理
                
                adventureTeamBlock.dungeon_side1Go[i].GetComponent<AnimatiorControl>().SetCharaFramesSimple(DataManager.mMonsterDict[gc.adventureTeamList[teamID].enemyIDList[i]].Pic);
                adventureTeamBlock.dungeon_side1Go[i].GetComponent<Image>().sprite = Resources.Load("Image/RolePic/" + DataManager.mMonsterDict[gc.adventureTeamList[teamID].enemyIDList[i]].Pic + "/Walk_Left", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_side1Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkLeft);
                if (hp <= 0) 
                { 
                    adventureTeamBlock.dungeon_side1Go[i].GetComponent<AnimatiorControl>().Stop();
                }

            }
            for (int i = gc.adventureTeamList[teamID].enemyIDList.Count; i < 6; i++)
            {
                adventureTeamBlock.dungeon_side1Go[i].GetComponent<AnimatiorControl>().Stop();
                adventureTeamBlock.dungeon_side1Go[i].GetComponent<Image>().sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                Debug.Log("修复的问题语句待观察 i="+ i+ "  adventureTeamBlock.dungeon_side1Go[i].GetComponent<Image>().sprite="+ adventureTeamBlock.dungeon_side1Go[i].GetComponent<Image>().sprite);
            }
        }
        else if (gc.adventureTeamList[teamID].action == AdventureAction.GetSomething)
        {
            adventureTeamBlock.dungeon_side1Go[0].GetComponent<AnimatiorControl>().SetChestFrames(1);
            adventureTeamBlock.dungeon_side1Go[0].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.ChestOpen);
        }
        else if (gc.adventureTeamList[teamID].action == AdventureAction.SpringHp)
        {
            adventureTeamBlock.dungeon_side1Go[0].GetComponent<AnimatiorControl>().SetSpringFrames("Hp");
            adventureTeamBlock.dungeon_side1Go[0].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.SpringAppear);
        }
        else if (gc.adventureTeamList[teamID].action == AdventureAction.SpringMp)
        {
            adventureTeamBlock.dungeon_side1Go[0].GetComponent<AnimatiorControl>().SetSpringFrames("Mp");
            adventureTeamBlock.dungeon_side1Go[0].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.SpringAppear);
        }
       
    }

    //动画场景-人物图片资源集、动作（己方）-更新配置
    public void UpdateSceneRole(byte teamID)
    {
        InitHeroCharaFrames(teamID);
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (int i = 0; i < gc.adventureTeamList[teamID].heroIDList.Count; i++)
        {
            if (gc.adventureTeamList[teamID].state == AdventureState.Free)
            { 
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Front); 
            }
            else if (gc.adventureTeamList[teamID].state == AdventureState.Sending || gc.adventureTeamList[teamID].state == AdventureState.Backing)
            {
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkRight);
            }
            else if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
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
                    int hp = 0;
                    //TODO:报错
                    Debug.Log("teamID=" + teamID);
                    Debug.Log("gc.fightMenberObjectSS.Count=" + gc.fightMenberObjectSS.Count);
                    for (int j = 0; j < gc.fightMenberObjectSS[teamID].Count; j++)
                    {
                        if (gc.fightMenberObjectSS[teamID][j].side == 0 && gc.fightMenberObjectSS[teamID][j].sideIndex == i)
                        {
                            hp = gc.fightMenberObjectSS[teamID][j].hpNow;
                            break;
                        }
                    }

                    if (hp > 0)
                    {
                        adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Idle);
                    }
                    else
                    {
                        adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Death);
                    }
                }
                else if (gc.adventureTeamList[teamID].action == AdventureAction.SpringHp ||
                    gc.adventureTeamList[teamID].action == AdventureAction.SpringMp ||
                    gc.adventureTeamList[teamID].action == AdventureAction.TrapHp ||
                    gc.adventureTeamList[teamID].action == AdventureAction.TrapMp ||
                    gc.adventureTeamList[teamID].action == AdventureAction.GetSomething)
                {
                    adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkRight);
                }
                else
                {
                    adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkRight);
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
            else if (gc.adventureTeamList[teamID].state == AdventureState.Retreat)
            {
                adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkLeft);
                adventureTeamBlock.dungeon_side0Go[i].transform.DOLocalMoveX(adventureTeamBlock.dungeon_side0Go[i].transform.localPosition.x - 300f, 2f);
            }
        }
    }

    //用作任务完成英雄动画
    public void SetAllRoleAnimInEnd(byte teamID, AdventureState endAdventureState)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (byte i = 0; i < gc.adventureTeamList[teamID].heroIDList.Count; i++)
        {

            if (endAdventureState == AdventureState.Done)
            {
                if (Random.Range(0, 5) < 4)
                {
                    switch (Random.Range(0, 5))
                    {
                        case 0: ShowTalk(teamID, 0, i, "好啊"); break;
                        case 1: ShowTalk(teamID, 0, i, "胜利"); break;
                        case 2: ShowTalk(teamID, 0, i, "icon_talk_love"); break;
                        case 3: ShowTalk(teamID, 0, i, "icon_talk_happy"); break;
                        case 4: ShowTalk(teamID, 0, i, "顺利回来了"); break;
                    }
                }

                if (Random.Range(0, 5) < 4)
                {
                    adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(Random.Range(0, 2) == 0 ? AnimStatus.Laugh : AnimStatus.SayYes);
                }

            }
            else if (endAdventureState == AdventureState.Fail)
            {
                if (Random.Range(0, 5) < 2)
                {
                    switch (Random.Range(0, 5))
                    {
                        case 0: ShowTalk(teamID, 0, i, "哎呀"); break;
                        case 1: ShowTalk(teamID, 0, i, "好疼"); break;
                        case 2: ShowTalk(teamID, 0, i, "icon_talk_sad"); break;
                        case 3: ShowTalk(teamID, 0, i, "icon_talk_exclamation"); break;
                        case 4: ShowTalk(teamID, 0, i, "全军覆没"); break;
                    }
                }
                if (Random.Range(0, 5) < 2)
                {
                    adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(Random.Range(0, 2) == 0 ? AnimStatus.Surprise : AnimStatus.SayNo);
                }
                   
            }
            else if (endAdventureState == AdventureState.Retreat)
            {
                if (Random.Range(0, 5) < 3)
                {
                    switch (Random.Range(0, 5))
                    {
                        case 0: ShowTalk(teamID, 0, i, "唔.."); break;
                        case 1: ShowTalk(teamID, 0, i, "撤退回来了"); break;
                        case 2: ShowTalk(teamID, 0, i, "icon_talk_question"); break;
                        case 3: ShowTalk(teamID, 0, i, "icon_talk_exclamation"); break;
                        case 4: ShowTalk(teamID, 0, i, "好惊险"); break;
                    }
                }
                if (Random.Range(0, 5) < 3)
                {
                    adventureTeamBlock.dungeon_side0Go[i].GetComponent<AnimatiorControl>().SetAnim(Random.Range(0, 2) == 0 ? AnimStatus.Surprise : AnimStatus.SayNo);
                }
                    
            }




        }
    }

    //动画场景-人物图片资源集、动作（己方）-更新配置（辅助方法）
    void InitHeroCharaFrames(byte teamID)
    {
        for (int j = 0; j < gc.adventureTeamList[teamID].heroIDList.Count; j++)
        {
            int heroID = gc.adventureTeamList[teamID].heroIDList[j];
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side0Go[j].GetComponent<AnimatiorControl>().SetCharaFrames(gc.heroDic[heroID].pic);
        }

    }


    //动画场景-人物HP/MP条（全部）-更新
    public void UpdateSceneRoleHpMp(byte teamID, List<FightMenberObject> fightMenberObjects)
    {
        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            UpdateSceneRoleHpMpSingle(teamID, fightMenberObjects[i]);
        }
    }

    //动画场景-人物HP/MP条（单个人物）-更新
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

    //动画场景-人物HP/MP条（全部）-隐藏
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
        for (byte i = 3; i < 6; i++)
        {
            adventureTeamBlock.dungeon_side1HpRt[i].sizeDelta = Vector2.zero;
            adventureTeamBlock.dungeon_side1MpRt[i].sizeDelta = Vector2.zero;
        }
    }


    //动画场景-人物AP条（全部）-显示（用作初始化）
    public void ShowSceneRoleAp(byte teamID, List<FightMenberObject> fightMenberObjects)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        HideSceneRoleAp(teamID);
        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            if (fightMenberObjects[i].side == 0)
            {
                adventureTeamBlock.dungeon_side0ApBgRt[fightMenberObjects[i].sideIndex].localScale = Vector2.one;
            }
            else if (fightMenberObjects[i].side == 1)
            {
                adventureTeamBlock.dungeon_side1ApBgRt[fightMenberObjects[i].sideIndex].localScale = Vector2.one;
            }

        }
    }

    //动画场景-人物AP条（全部）-更新
    public void UpdateSceneRoleAp(byte teamID, List<FightMenberObject> fightMenberObjects)
    {
        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            UpdateSceneRoleApSingle(teamID, fightMenberObjects[i]);
        }
    }

    //动画场景-人物AP条（单个人物）-更新
    public void UpdateSceneRoleApSingle(byte teamID, FightMenberObject fightMenberObject)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        if (fightMenberObject.side == 0)
        {
            adventureTeamBlock.dungeon_side0ApImage[fightMenberObject.sideIndex].fillAmount = fightMenberObject.actionBar /10000f;
        }
        else if (fightMenberObject.side == 1)
        {
            adventureTeamBlock.dungeon_side1ApImage[fightMenberObject.sideIndex].fillAmount = fightMenberObject.actionBar / 10000f;
        }
    }

    //动画场景-人物AP条文本（全部）-更新
    public void UpdateSceneRoleApText(byte teamID, List<FightMenberObject> fightMenberObjects)
    {
        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            UpdateSceneRoleApTextSingle(teamID, fightMenberObjects[i]);
        }
    }

    //动画场景-人物AP条文本（单个人物）-更新
    public void UpdateSceneRoleApTextSingle(byte teamID, FightMenberObject fightMenberObject)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        if (fightMenberObject.side == 0)
        {
            if (fightMenberObject.hpNow > 0)
            {
                adventureTeamBlock.dungeon_side0ApText[fightMenberObject.sideIndex].text = "AP";
                adventureTeamBlock.dungeon_side0ApImage[fightMenberObject.sideIndex].fillAmount = fightMenberObject.actionBar / 10000f;
            }
            else
            {
                adventureTeamBlock.dungeon_side0ApText[fightMenberObject.sideIndex].text = "<color=#FF5139>DEATH</color>";
                adventureTeamBlock.dungeon_side0ApImage[fightMenberObject.sideIndex].fillAmount = 0f;

            }
        }
        else if (fightMenberObject.side == 1)
        {
            if (fightMenberObject.hpNow > 0)
            {
                adventureTeamBlock.dungeon_side1ApText[fightMenberObject.sideIndex].text = "AP";
                adventureTeamBlock.dungeon_side1ApImage[fightMenberObject.sideIndex].fillAmount = fightMenberObject.actionBar / 10000f;
            }
            else
            {
                adventureTeamBlock.dungeon_side1ApText[fightMenberObject.sideIndex].text = "<color=#FF5139>DEATH</color>";
                adventureTeamBlock.dungeon_side1ApImage[fightMenberObject.sideIndex].fillAmount = 0f;
            }
        }
    }

    //动画场景-人物AP条（全部）-隐藏
    public void HideSceneRoleAp(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (byte i = 0; i < 3; i++)
        {
            adventureTeamBlock.dungeon_side0ApBgRt[i].localScale = Vector2.zero;
            adventureTeamBlock.dungeon_side1ApBgRt[i].localScale = Vector2.zero;
        }
        for (byte i = 3; i < 6; i++)
        {
            adventureTeamBlock.dungeon_side1ApBgRt[i].localScale = Vector2.zero;
        }
    }

    //动画场景-人物武器锋利度（全部己方）-更新（用作初始化）
    public void UpdateSceneRoleSharpness(byte teamID, List<FightMenberObject> fightMenberObjects)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        HideSceneRoleSharpness(teamID);
        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            if (fightMenberObjects[i].side == 0)
            {
                adventureTeamBlock.dungeon_side0SharpnessBgImage[fightMenberObjects[i].sideIndex].color = Color.white;
                if (gc.heroDic[fightMenberObjects[i].objectID].equipWeapon == -1)
                {
                    adventureTeamBlock.dungeon_side0SharpnessBgImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_none_bg");
                    adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_none");
                }
                else
                {
                    switch (DataManager.mItemDict[ gc.itemDic[ gc.heroDic[fightMenberObjects[i].objectID].equipWeapon].prototypeID].TypeSmall)
                    {
                        case ItemTypeSmall.Sword: 
                            adventureTeamBlock.dungeon_side0SharpnessBgImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_sword_bg");
                            adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_sword");
                            break;
                        case ItemTypeSmall.Axe:
                            adventureTeamBlock.dungeon_side0SharpnessBgImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_axe_bg");
                            adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_axe");
                            break;
                        case ItemTypeSmall.Spear:
                            adventureTeamBlock.dungeon_side0SharpnessBgImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_spear_bg");
                            adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_spear"); 
                            break;
                        case ItemTypeSmall.Hammer:
                            adventureTeamBlock.dungeon_side0SharpnessBgImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_hammer_bg");
                            adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_hammer");
                            break;
                        case ItemTypeSmall.Bow:
                            adventureTeamBlock.dungeon_side0SharpnessBgImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_bow_bg");
                            adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_bow");
                            break;
                        case ItemTypeSmall.Staff:
                            adventureTeamBlock.dungeon_side0SharpnessBgImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_staff_bg");
                            adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObjects[i].sideIndex].sprite = Resources.Load<Sprite>("Image/Other/icon_weaponType_staff"); 
                            break;
                    }
                }
               

                UpdateSceneRoleSharpnessSingle(teamID, fightMenberObjects[i]);
            }
        }
    }

    //动画场景-人物武器锋利度（单个人物）-更新
    public void UpdateSceneRoleSharpnessSingle(byte teamID, FightMenberObject fightMenberObject)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        byte sharpnessLevel = gc.GetSharpnessLevel(fightMenberObject);
        switch (sharpnessLevel)
        {
            case 0:adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].color = new Color(250 / 255f, 63 / 255f, 63 / 255f, 0.85f);break;
            case 1: adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].color = new Color(226 / 255f, 132 / 255f, 14 / 255f, 0.85f); break;
            case 2: adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].color = new Color(226 / 255f, 197 / 255f, 14 / 255f, 0.85f); break;
            case 3: adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].color = new Color(17 / 255f, 188 / 255f, 31 / 255f, 0.85f); break;
            case 4: adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].color = new Color(14 / 255f, 173 / 255f, 226 / 255f, 0.85f); break;
            case 5: adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0.85f); break;
            case 6: adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].color = new Color(140 / 255f, 83 / 255f, 190 / 255f, 0.85f); break;
        }
        if (gc.heroDic[fightMenberObject.objectID].equipWeapon != -1)
        {
            byte tc = 0;
            //Debug.Log(fightMenberObject.name+"sharpnessLevel=" + sharpnessLevel);
            string teststr = "";
            for (byte i = 0; i < sharpnessLevel; i++)
            {
                tc += DataManager.mItemDict[fightMenberObject.weaponPID].Sharpness[i];
                teststr += "," + DataManager.mItemDict[fightMenberObject.weaponPID].Sharpness[i];
            }
            //Debug.Log("teststr=" + teststr);
            //Debug.Log("fightMenberObject.sharpnessNow=" + fightMenberObject.sharpnessNow);
            //Debug.Log("tc=" + tc);
            //Debug.Log(" "+( fightMenberObject.sharpnessNow - tc)+" "+ DataManager.mItemDict[fightMenberObject.weaponPID].Sharpness[sharpnessLevel]);
            adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].fillAmount = (float)(fightMenberObject.sharpnessNow - tc) / (float)DataManager.mItemDict[fightMenberObject.weaponPID].Sharpness[sharpnessLevel];
        }
        else
        {
            adventureTeamBlock.dungeon_side0SharpnessImage[fightMenberObject.sideIndex].fillAmount = 1f;
        }

    }

    //动画场景-人物武器锋利度（全部）-隐藏
    public void HideSceneRoleSharpness(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (byte i = 0; i < 3; i++)
        {
            adventureTeamBlock.dungeon_side0SharpnessBgImage[i].color = Color.clear;
            adventureTeamBlock.dungeon_side0SharpnessImage[i].color = Color.clear;
        }
    }

    //动画场景-人物光环（全部己方）-更新（用作初始化）
    public void UpdateSceneRoleHalo(byte teamID, List<FightMenberObject> fightMenberObjects)
    {
        //AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        //HideSceneRoleHalo(teamID);
        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            if (fightMenberObjects[i].side == 0)
            {
                UpdateSceneRoleHaloSingle(teamID, fightMenberObjects[i]);
            }

        }
    }
    public void UpdateSceneRoleHaloSingle(byte teamID, FightMenberObject fightMenberObject)
    {
       
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        if (gc.heroDic[fightMenberObject.objectID].halo != -1 && fightMenberObject.haloStatus)
        {
            adventureTeamBlock.dungeon_side0HaloGo[fightMenberObject.sideIndex].GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.dungeon_side0HaloGo[fightMenberObject.sideIndex].GetComponent<LoopEffect>().Play(DataManager.mHaloDict[gc.heroDic[fightMenberObject.objectID].halo].Pic);
            Debug.Log("UpdateSceneRoleHaloSingle() gc.heroDic[fightMenberObject.objectID].halo="+ gc.heroDic[fightMenberObject.objectID].halo);
        }
        else
        {
            adventureTeamBlock.dungeon_side0HaloGo[fightMenberObject.sideIndex].GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.dungeon_side0HaloGo[fightMenberObject.sideIndex].GetComponent<LoopEffect>().Stop();
        }
    }

    public void HideSceneRoleHalo(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (byte i = 0; i < 3; i++)
        {
            adventureTeamBlock.dungeon_side0HaloGo[i].GetComponent<RectTransform>().localScale = Vector2.zero;
            adventureTeamBlock.dungeon_side0HaloGo[i].GetComponent<LoopEffect>().Stop();
        }
    }

    //动画场景-人物BUFF组（全部）-更新
    public void UpdateSceneRoleBuff(byte teamID, List<FightMenberObject> fightMenberObjects)
    {
        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            UpdateSceneRoleBuffSingle(teamID, fightMenberObjects[i]);
        }
    }

    //动画场景-人物BUFF组（单个人物）-更新
    public void UpdateSceneRoleBuffSingle(byte teamID, FightMenberObject fightMenberObject)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        int signLimit = 7;// 最多显示7个

        for (int i = 0; i < fightMenberObject.buff.Count; i++)
        {
            switch (fightMenberObject.buff[i].type)
            {
                case FightBuffType.Dizzy:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_dizzy", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_dizzy", typeof(Sprite)) as Sprite;
                    }
                    break;
                case FightBuffType.Confusion:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_confusion", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_confusion", typeof(Sprite)) as Sprite;
                    }
                    break;
                case FightBuffType.Poison:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_poison", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_poison", typeof(Sprite)) as Sprite;
                    }
                    break;
                case FightBuffType.Sleep:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_sleep", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_sleep", typeof(Sprite)) as Sprite;
                    }
                    break;
                case FightBuffType.UpAtk:
                case FightBuffType.UpMAtk:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_upatk", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_upatk", typeof(Sprite)) as Sprite;
                    }
                    break;
                case FightBuffType.UpDef:
                case FightBuffType.UpMDef:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_updef", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_updef", typeof(Sprite)) as Sprite;
                    }
                    break;
                case FightBuffType.UpHit:
                case FightBuffType.UpDod:
                case FightBuffType.UpCriD:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_uphit", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_uphit", typeof(Sprite)) as Sprite;
                    }
                    break;
                case FightBuffType.UpWindDam:
                case FightBuffType.UpFireDam:
                case FightBuffType.UpWaterDam:
                case FightBuffType.UpGroundDam:
                case FightBuffType.UpLightDam:
                case FightBuffType.UpDarkDam:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_updam", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_updam", typeof(Sprite)) as Sprite;
                    }
                    break;
                case FightBuffType.UpWindRes:
                case FightBuffType.UpFireRes:
                case FightBuffType.UpWaterRes:
                case FightBuffType.UpGroundRes:
                case FightBuffType.UpLightRes:
                case FightBuffType.UpDarkRes:
                    if (fightMenberObject.side == 0)
                    {
                        adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_upres", typeof(Sprite)) as Sprite;
                    }
                    else if (fightMenberObject.side == 1)
                    {
                        adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/icon_status_upres", typeof(Sprite)) as Sprite;
                    }
                    break;
            }
        }
        for (int i = fightMenberObject.buff.Count; i < signLimit; i++)
        {
            if (fightMenberObject.side == 0)
            {
                adventureTeamBlock.dungeon_side0BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            }
            else if (fightMenberObject.side == 1)
            {
                adventureTeamBlock.dungeon_side1BuffsGo[fightMenberObject.sideIndex].transform.GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            }
        }

    }

    //动画场景-人物BUFF组（全部）-显示
    public void ShowSceneRoleBuff(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (byte i = 0; i < 3; i++)
        {
            adventureTeamBlock.dungeon_side0BuffsGo[i].GetComponent<RectTransform>().localScale = Vector2.one;
            adventureTeamBlock.dungeon_side1BuffsGo[i].GetComponent<RectTransform>().localScale = Vector2.one;
        }
        for (byte i = 3; i < 6; i++)
        {
            adventureTeamBlock.dungeon_side1BuffsGo[i].GetComponent<RectTransform>().localScale = Vector2.one;
        }
    }

    //动画场景-人物BUFF组（全部）-隐藏
    public void HideSceneRoleBuff(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (byte i = 0; i < 3; i++)
        {
            for (byte j = 0; j < 7; j++)
            {
                adventureTeamBlock.dungeon_side0BuffsGo[i].transform.GetChild(j).GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                adventureTeamBlock.dungeon_side1BuffsGo[i].transform.GetChild(j).GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            }
        }
        for (byte i = 3; i < 6; i++)
        {
            for (byte j = 0; j < 7; j++)
            {
                adventureTeamBlock.dungeon_side1BuffsGo[i].transform.GetChild(j).GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            }
        }
    }


    //动画场景-人物队形-设置
    public void UpdateSceneRoleFormations(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        if (gc.adventureTeamList[teamID].state == AdventureState.Free )
        {
            adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);

            adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Sending ||
            gc.adventureTeamList[teamID].state == AdventureState.Backing)
        {
            adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, 60);
            adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 60);
            adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, 60);

            adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.NotSend ||
            gc.adventureTeamList[teamID].state == AdventureState.Done ||
            gc.adventureTeamList[teamID].state == AdventureState.Retreat ||
            gc.adventureTeamList[teamID].state == AdventureState.Fail)
        {
            adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, 45);
            adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 45);
            adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, 45);

            adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            adventureTeamBlock.dungeon_side1Go[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            if (gc.adventureTeamList[teamID].action == AdventureAction.Walk)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, 45);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 45);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, 45);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            }
            else if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-90, 45);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 15);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 75);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 45);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 15);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 75);
                adventureTeamBlock.dungeon_side1Go[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(140, 45);
                adventureTeamBlock.dungeon_side1Go[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(110, 15);
                adventureTeamBlock.dungeon_side1Go[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(110, 75);
            }
            else if (gc.adventureTeamList[teamID].action == AdventureAction.GetSomething)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, 45);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 45);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, 45);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(78, 45);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            }
            else if (gc.adventureTeamList[teamID].action == AdventureAction.SpringHp || gc.adventureTeamList[teamID].action == AdventureAction.SpringMp)
            {
                adventureTeamBlock.dungeon_side0Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(39, 45);
                adventureTeamBlock.dungeon_side0Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 45);
                adventureTeamBlock.dungeon_side0Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-39, 45);

                adventureTeamBlock.dungeon_side1Go[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(12, 60);
                adventureTeamBlock.dungeon_side1Go[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                adventureTeamBlock.dungeon_side1Go[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            }
        }

    }


    //动画场景-地牢元素标识-更新
    public void UpdateElementPoint(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        byte index = 0;

        Dictionary<Element, short> keyValuePairs = new Dictionary<Element, short>();
        keyValuePairs.Add(Element.Wind, gc.adventureTeamList[teamID].dungeonEVWind);
        keyValuePairs.Add(Element.Fire, gc.adventureTeamList[teamID].dungeonEVFire);
        keyValuePairs.Add(Element.Water, gc.adventureTeamList[teamID].dungeonEVWater);
        keyValuePairs.Add(Element.Ground, gc.adventureTeamList[teamID].dungeonEVGround);
        keyValuePairs.Add(Element.Light, gc.adventureTeamList[teamID].dungeonEVLight);
        keyValuePairs.Add(Element.Dark, gc.adventureTeamList[teamID].dungeonEVDark);
       
         var dicSort = from objDic in keyValuePairs orderby objDic.Value descending select objDic;

        List<float> x = new List<float> { 0, -20, 20, -40, 40 };

        foreach (KeyValuePair<Element, short> kvp in dicSort)
        {
            switch (kvp.Key)
            {
                case Element.Wind:
                    for (byte i = 0; i < gc.adventureTeamList[teamID].dungeonEPWind; i++)
                    {
                        if(index<4)
                        {
                            adventureTeamBlock.dungeon_elementImage[index].sprite = Resources.Load("Image/Other/icon912", typeof(Sprite)) as Sprite;
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOComplete();
                            adventureTeamBlock.dungeon_elementImage[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(x[index], 0f);
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOLocalJump(new Vector2(x[index],24f), 5, 5, 3f);
                            index++;
                        }                    
                    }
                    break;
                case Element.Fire:
                    for (byte i = 0; i < gc.adventureTeamList[teamID].dungeonEPFire; i++)
                    {
                        if (index < 4)
                        {
                            adventureTeamBlock.dungeon_elementImage[index].sprite = Resources.Load("Image/Other/icon913", typeof(Sprite)) as Sprite;
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOComplete();
                            adventureTeamBlock.dungeon_elementImage[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(x[index], 0f);
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOLocalJump(new Vector2(x[index], 24f), 5, 5, 3f);
                            index++;
                        }
                    }
                    break;
                case Element.Water:
                    for (byte i = 0; i < gc.adventureTeamList[teamID].dungeonEPWater; i++)
                    {
                        if (index < 4)
                        {
                            adventureTeamBlock.dungeon_elementImage[index].sprite = Resources.Load("Image/Other/icon914", typeof(Sprite)) as Sprite;
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOComplete();
                            adventureTeamBlock.dungeon_elementImage[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(x[index], 0f);
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOLocalJump(new Vector2(x[index], 24f), 5, 5, 3f);
                            index++;
                        }
                    }
                    break;
                case Element.Ground:
                    for (byte i = 0; i < gc.adventureTeamList[teamID].dungeonEPGround; i++)
                    {
                        if (index < 4)
                        {
                            adventureTeamBlock.dungeon_elementImage[index].sprite = Resources.Load("Image/Other/icon919", typeof(Sprite)) as Sprite;
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOComplete();
                            adventureTeamBlock.dungeon_elementImage[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(x[index], 0f);
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOLocalJump(new Vector2(x[index], 24f), 5, 5, 3f);
                            index++;
                        }
                    }
                    break;
                case Element.Light:
                    for (byte i = 0; i < gc.adventureTeamList[teamID].dungeonEPLight; i++)
                    {
                        if (index < 4)
                        {
                            adventureTeamBlock.dungeon_elementImage[index].sprite = Resources.Load("Image/Other/icon917", typeof(Sprite)) as Sprite;
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOComplete();
                            adventureTeamBlock.dungeon_elementImage[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(x[index], 0f);
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOLocalJump(new Vector2(x[index], 24f), 5, 5, 3f);
                            index++;
                        }
                    }
                    break;
                case Element.Dark:
                    for (byte i = 0; i < gc.adventureTeamList[teamID].dungeonEPDark; i++)
                    {
                        if (index < 4)
                        {
                            adventureTeamBlock.dungeon_elementImage[index].sprite = Resources.Load("Image/Other/icon916", typeof(Sprite)) as Sprite;
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOComplete();
                            adventureTeamBlock.dungeon_elementImage[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(x[index], 0f);
                            adventureTeamBlock.dungeon_elementImage[index].transform.DOLocalJump(new Vector2(x[index], 24f), 5, 5, 3f);
                            index++;
                        }
                    }
                    break;
            }
        }

        for (byte i = index; i < 5; i++)
        {
            adventureTeamBlock.dungeon_elementImage[index].sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }

    }

    //动画场景-地牢元素标识-隐藏
    public void HideElementPoint(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
        for (byte i = 0; i < 5; i++)
        {
            adventureTeamBlock.dungeon_elementImage[i].sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
    }


    //动画场景-人物动作动画（单个人物）-设置
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


    //动画场景-显示伤害数字
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
            go.GetComponent<MomentText>().pool = numPool;
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

    //动画场景-显示技能特效
    public void ShowEffect(byte teamID, byte side, byte index, string effectName,float size)
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
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(96f * size, 96f * size);
        //go.GetComponent<MomentEffect>().isInAdventureMainPanel = true;
        go.GetComponent<MomentEffect>().Play(effectName, targetLocation);
    }

    //动画场景-显示角色对白
    public void ShowTalk(byte teamID, byte side, byte index, string content)
    {
        GameObject go;
        if (talkPool.Count > 0)
        {
            go = talkPool[0];

            talkPool.RemoveAt(0);
        }
        else
        {
            go = Instantiate(Resources.Load("Prefab/Moment/Moment_Talk")) as GameObject;
        }
        go.transform.SetParent(adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_talkLayerGo.transform);

        Vector2 targetLocation = Vector2.zero;
        if (side == 0)
        {
            go.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(10f, 1);
            go.transform.GetChild(0).GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0.5f);
            go.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0.5f);
            go.transform.GetChild(0).GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
            targetLocation = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side0Go[index].GetComponent<RectTransform>().anchoredPosition+new Vector2(24f,40f);
        }
        else if (side == 1)
        {
            go.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(-10f, 1);
            go.transform.GetChild(0).GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0.5f);
            go.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.5f);
            go.transform.GetChild(0).GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);
 
            targetLocation = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().dungeon_side1Go[index].GetComponent<RectTransform>().anchoredPosition + new Vector2(-24f, 40f);
        }
        go.GetComponent<MomentTalk>().Show(content, side, targetLocation);

    }


    //队伍英雄状态栏目-全部-更新
    public void UpdateTeamHero(byte teamID)
    {
        AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();

        for (int j = 0; j < gc.adventureTeamList[teamID].heroIDList.Count; j++)
        {
            int heroID = gc.adventureTeamList[teamID].heroIDList[j];

            adventureTeamBlock.hero_picImage[j].overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[heroID].pic + "/Pic", typeof(Sprite)) as Sprite;
            adventureTeamBlock.hero_nameText[j].text = gc.heroDic[heroID].name + "\nLv." + gc.heroDic[heroID].level;
            adventureTeamBlock.hero_hpmpText[j].text = "<color=#" + (gc.adventureTeamList[teamID].heroHpList[j] == 0 ? "FF684F>体力 " : "76ee00>体力 ") + gc.adventureTeamList[teamID].heroHpList[j] + "/" + gc.GetHeroAttr(Attribute.Hp, heroID) + "</color>\n<color=#428DFD>魔力 " + gc.adventureTeamList[teamID].heroMpList[j] + "/" + gc.GetHeroAttr(Attribute.Mp, heroID) + "</color>";
        }
        for (int j = gc.adventureTeamList[teamID].heroIDList.Count; j < 3; j++)
        {
            adventureTeamBlock.dungeon_side0Go[j].GetComponent<Image>().sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;

            adventureTeamBlock.hero_picImage[j].overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            adventureTeamBlock.hero_nameText[j].text = "";
            adventureTeamBlock.hero_hpmpText[j].text = "";
        }
    }

    //队伍英雄状态栏目-单个-更新（仅HPMP）
    public void UpdateHeroHpMpSingle(byte teamID, FightMenberObject fightMenberObject)
    {
        if (fightMenberObject.side == 0)
        {
            AdventureTeamBlock adventureTeamBlock = adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>();
            adventureTeamBlock.hero_hpmpText[fightMenberObject.sideIndex].text = "<color=#" + (fightMenberObject.hpNow == 0 ? "FF684F>体力 " : "76ee00>体力 ") + fightMenberObject.hpNow + "/" + fightMenberObject.hp + "</color>\n<color=#428DFD>魔力 " + fightMenberObject.mpNow + "/" + fightMenberObject.mp + "</color>";
        }
    }


    //日志框栏目-更新
    public void UpdateLogContent(byte teamID)
    {
        const int MaxRow = 50;
        adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().contentText.text = "";

        for (int i = gc.adventureTeamList[teamID].log.Count - 1; i >= System.Math.Max(0, gc.adventureTeamList[teamID].log.Count - MaxRow); i--)
        {
            adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().contentText.text += gc.adventureTeamList[teamID].log[i] + "\n";
        }
    }


    //战利品面板-显示
    public void ShowGets(byte teamID)
    {
        string str = "";
        if (gc.adventureTeamList[teamID].getExp != 0)
        {
            str += "[经验值:" + gc.adventureTeamList[teamID].getExp + "] ";
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
            str += "[" + DataManager.mItemDict[gc.adventureTeamList[teamID].getItemList[i]].Name + "] ";
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
        else if (gc.adventureTeamList[teamID].state == AdventureState.Done ||
            gc.adventureTeamList[teamID].state == AdventureState.Fail ||
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

    //战利品面板-关闭
    public void HideGets(byte teamID)
    {
        adventureTeamGo[teamID].GetComponent<AdventureTeamBlock>().getsRt.localScale = Vector2.zero;
    }
}