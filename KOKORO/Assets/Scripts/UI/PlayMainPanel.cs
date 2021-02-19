using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayMainPanel : BasePanel
{
    public static PlayMainPanel Instance;

    GameControlInPlay gci;
    GameControl gc;

    #region 【UI控件】
    public Image top_flagImage;
    public Text top_nameText;
    public Text top_goldText;
    public Image top_weatherImage;
    public Image top_seasonImage;
    public Text top_dateLeftText;
    public Text top_dateRightText;
    public Text top_hourText;
    public List<RectTransform> top_timeBarRtList;
    public Button top_pauseBtn;
    public Button top_playBtn;
    public Button top_fastBtn;
    public Button top_saveBtn;
    public Button top_loadBtn;
    public Button top_helpBtn;
    public Button top_setBtn;
    public Button top_homeBtn;

    public Button top_resourcesBtn;
    public Text top_resourcesSignText;
    public Text top_resourcesFoodText;
    public Text top_resourcesStuffText;
    public RectTransform top_resourcesBlockRt;
    public Text top_resourcesBlock_foodCerealText;
    public Text top_resourcesBlock_foodVegetableText;
    public Text top_resourcesBlock_foodFruitText;
    public Text top_resourcesBlock_foodMeatText;
    public Text top_resourcesBlock_foodFishText;
    public Text top_resourcesBlock_foodBeerText;
    public Text top_resourcesBlock_foodWineText;

    public Text top_resourcesBlock_stuffWoodText;
    public Text top_resourcesBlock_stuffStoneText;
    public Text top_resourcesBlock_stuffMetalText;
    public Text top_resourcesBlock_stuffLeatherText;
    public Text top_resourcesBlock_stuffClothText;
    public Text top_resourcesBlock_stuffTwineText;
    public Text top_resourcesBlock_stuffBoneText;
    public Text top_resourcesBlock_stuffWindText;
    public Text top_resourcesBlock_stuffFireText;
    public Text top_resourcesBlock_stuffWaterText;
    public Text top_resourcesBlock_stuffGroundText;
    public Text top_resourcesBlock_stuffLightText;
    public Text top_resourcesBlock_stuffDarkText;

    public Text left_inventoryNumText;
    public RectTransform left_inventoryNumBgRt;
    public RectTransform left_inventoryNumBarRt;

    public Button left_inventoryEquipBtn;
    public RectTransform left_inventoryEquipNumRt;
    public Text left_inventoryEquipNumText;
    public Button left_inventoryScrollBtn;
    public RectTransform left_inventoryScrollNumRt;
    public Text left_inventoryScrollNumText;
    public Button left_fiscalBtn;
    public Button left_technologyBtn;
    public RectTransform left_technologyTipRt;
    public Button left_diplomacyBtn;
    public Button left_policyBtn;
    public Button left_heroBtn;
    public RectTransform left_heroNumRt;
    public Text left_heroNumText;

    public Button left_warBtn;
    public Button left_adventureBtn;
    public RectTransform left_adventureTipRt;

    public Button bottom_adventureLastBtn;
    public Button bottom_adventureNextBtn;
    public Text bottom_adventurePageText;

    public List<RectTransform> bottom_adventureRt;
    public List<Text> bottom_adventure_teamNameText;
    public List<Button> bottom_adventure_detailBtn;
    public List<Image> bottom_adventure_mapImage;
    public List<Text> bottom_adventure_desText;
    public List<Text> bottom_adventure_logText;
    public List<Transform> bottom_adventure_herosTf;
    #endregion

    //运行变量
    public List<byte> adventureTeamIDList = new List<byte>();
    byte adventureStartIndex = 0;
    public bool IsShowResourcesBlock = false;
    public bool IsShowSetBlock = false;
    bool Leftis0 = true;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();

        left_inventoryEquipBtn.onClick.AddListener(delegate () { gci.OpenInventoryEquip(); AudioControl.Instance.PlaySound("system_button"); });
        left_inventoryScrollBtn.onClick.AddListener(delegate () { gci.OpenInventorySkill(); AudioControl.Instance.PlaySound("system_button"); });
        left_technologyBtn.onClick.AddListener(delegate () { gci.OpenTechnology(); AudioControl.Instance.PlaySound("system_button"); });
        left_diplomacyBtn.onClick.AddListener(delegate () { gci.OpenDiplomacy(); AudioControl.Instance.PlaySound("system_button"); });
        left_heroBtn.onClick.AddListener(delegate () { gci.OpenHeroSelect(-1); AudioControl.Instance.PlaySound("system_button"); });
        left_adventureBtn.onClick.AddListener(delegate () { gci.OpenAdventureMain(); });


        top_saveBtn.onClick.AddListener(delegate () { gci.GameSave(); });
        top_setBtn.onClick.AddListener(delegate () { SystemSetPanel.Instance.OnShow() ; });

        top_pauseBtn.onClick.AddListener(delegate () { gci.TimePause(); });
        top_playBtn.onClick.AddListener(delegate () { gci.TimePlay(); });
        top_fastBtn.onClick.AddListener(delegate () { gci.TimeFast(); });

        top_resourcesBtn.onClick.AddListener(delegate ()
        {
            if (IsShowResourcesBlock)
            {
                HideResourcesBlock();
            }
            else { ShowResourcesBlock(); }
        });


        bottom_adventureLastBtn.onClick.AddListener(delegate () { AdventureStartIndexToLast(); });
        bottom_adventureNextBtn.onClick.AddListener(delegate () { AdventureStartIndexToNext(); });
    }

    //主面板显示
    public override void OnShow()
    {
        SetAnchoredPosition(0, 0);

        UpdateInventoryNum();
        UpdateKingdomInfo();
        UpdateGold();
        UpdateResources();
        UpdateDateInfo();
        UpdateTimeButtonState();

        UpdateButtonItemNum();
        UpdateButtonSkillNum();
        UpdateButtonHeroNum();

        HideResourcesBlock();

        UpdateAdventurePageText();
        UpdateAdventureAll();
    }

    #region 【方法组】顶部区域
    //顶部区域-势力栏目-名字旗帜-更新
    public void UpdateKingdomInfo()
    {
        top_flagImage.overrideSprite = Resources.Load("Image/Other/icon_flag_"+gc.forceDic[0].flagIndex+"_a", typeof(Sprite)) as Sprite;
        top_nameText.text = "克克洛地区";     
    }

    //顶部区域-势力栏目-持有金数-更新
    public void UpdateGold()
    {
        top_goldText.text = gc.forceDic[0].gold.ToString();
    }

    //顶部区域-势力栏目-持有食物/材料数-更新
    public void UpdateResources()
    {
        top_resourcesFoodText.text = gc.GetForceFoodAll(0)+ "/"+gc.forceDic[0].rFoodLimit;
        top_resourcesStuffText.text = gc.GetForceStuffAll(0) + "/" + gc.forceDic[0].rStuffLimit;
    }

    //顶部区域-势力栏目-持有食物/材料数-详情块-更新
    public void UpdateResourcesBlock()
    {
        top_resourcesBlock_foodCerealText.text = gc.forceDic[0].rFoodCereal.ToString();
        top_resourcesBlock_foodVegetableText.text = gc.forceDic[0].rFoodVegetable.ToString();
        top_resourcesBlock_foodFruitText.text = gc.forceDic[0].rFoodFruit.ToString();
        top_resourcesBlock_foodMeatText.text = gc.forceDic[0].rFoodMeat.ToString();
        top_resourcesBlock_foodFishText.text = gc.forceDic[0].rFoodFish.ToString();
        top_resourcesBlock_foodBeerText.text = gc.forceDic[0].rFoodBeer.ToString();
        top_resourcesBlock_foodWineText.text = gc.forceDic[0].rFoodWine.ToString();

        top_resourcesBlock_stuffWoodText.text = gc.forceDic[0].rStuffWood.ToString();
        top_resourcesBlock_stuffStoneText.text = gc.forceDic[0].rStuffStone.ToString();
        top_resourcesBlock_stuffMetalText.text = gc.forceDic[0].rStuffMetal.ToString();
        top_resourcesBlock_stuffLeatherText.text = gc.forceDic[0].rStuffLeather.ToString();
        top_resourcesBlock_stuffClothText.text = gc.forceDic[0].rStuffCloth.ToString();
        top_resourcesBlock_stuffTwineText.text = gc.forceDic[0].rStuffTwine.ToString();
        top_resourcesBlock_stuffBoneText.text = gc.forceDic[0].rStuffBone.ToString();
        top_resourcesBlock_stuffWindText.text = gc.forceDic[0].rStuffWind.ToString();
        top_resourcesBlock_stuffFireText.text = gc.forceDic[0].rStuffFire.ToString();
        top_resourcesBlock_stuffWaterText.text = gc.forceDic[0].rStuffWater.ToString();
        top_resourcesBlock_stuffGroundText.text = gc.forceDic[0].rStuffGround.ToString();
        top_resourcesBlock_stuffLightText.text = gc.forceDic[0].rStuffLight.ToString();
        top_resourcesBlock_stuffDarkText.text = gc.forceDic[0].rStuffDark.ToString();
    }

    //顶部区域-势力栏目-持有食物/材料数-详情块-显示
    public void ShowResourcesBlock()
    {

        if (BuildPanel.Instance.isShow)
        {
            BuildPanel.Instance.OnHide();
        }
        if (BuildingPanel.Instance.isShow)
        {
            BuildingPanel.Instance.OnHide();
        }

        UpdateResourcesBlock();
        top_resourcesSignText.text = "▲";
        top_resourcesBlockRt.localScale = Vector2.one;
        IsShowResourcesBlock = true;
    }

    //顶部区域-势力栏目-持有食物/材料数-详情块-关闭
    public void HideResourcesBlock()
    {
        top_resourcesSignText.text = "▼";
        top_resourcesBlockRt.localScale = Vector2.zero;
        IsShowResourcesBlock = false;
    }

    //顶部区域-日期栏目（全部）-更新
    public void UpdateDateInfo()
    {
        UpdateYearSeason();
        UpdateMonthDayHour();
        UpdateTimeBar();
    }

    //顶部区域-日期栏目-月日时间-更新
    public void UpdateMonthDayHour()
    {
        top_dateRightText.text = gc.timeMonth + "月" + gc.timeDay + "日 "+gc.OutputWeekStr(gc.timeWeek,true);
        top_hourText.text = gc.timeHour.ToString();
    }

    //顶部区域-日期栏目-年季-更新
    public void UpdateYearSeason()
    {
        top_dateLeftText.text = "第" + gc.timeYear + "年 " + gc.OutputSeasonStr(gc.timeMonth, true);
        switch (gc.timeMonth)
        {
            case 1:
            case 2:
            case 3:
                top_seasonImage.overrideSprite= Resources.Load("Image/Other/season_spring2", typeof(Sprite)) as Sprite;break;
            case 4:
            case 5:
            case 6:
                top_seasonImage.overrideSprite = Resources.Load("Image/Other/season_summer2", typeof(Sprite)) as Sprite; break;
            case 7:
            case 8:
            case 9:
                top_seasonImage.overrideSprite = Resources.Load("Image/Other/season_autumn2", typeof(Sprite)) as Sprite; break;
            case 10:
            case 11:
            case 12:
                top_seasonImage.overrideSprite = Resources.Load("Image/Other/season_winter2", typeof(Sprite)) as Sprite; break;
        }
        
    }

    //顶部区域-日期栏目-时间滚动条-更新
    public void UpdateTimeBar()
    {
        if (gc.timeHour==18)
        {
            Leftis0 = !Leftis0;
           // Debug.Log("切换 Leftis0="+ Leftis0.ToString());
        }

        if (Leftis0)
        {
            top_timeBarRtList[0].anchoredPosition = new Vector2((gc.timeHour - 5) * -20f+(-2f*gc.timeS), 0);
            if (gc.timeHour <= 6)
            {
                top_timeBarRtList[1].anchoredPosition = new Vector2(top_timeBarRtList[0].anchoredPosition.x - 480f, 0);
            }
            else
            {
                top_timeBarRtList[1].anchoredPosition = new Vector2(top_timeBarRtList[0].anchoredPosition.x + 480f, 0);
            } 
        }
        else
        {
            top_timeBarRtList[1].anchoredPosition = new Vector2((gc.timeHour - 5) * -20f + (-2f * gc.timeS), 0);
            if (gc.timeHour <= 6)
            {
                top_timeBarRtList[0].anchoredPosition = new Vector2(top_timeBarRtList[1].anchoredPosition.x - 480f, 0);
            }
            else
            {
                top_timeBarRtList[0].anchoredPosition = new Vector2(top_timeBarRtList[1].anchoredPosition.x + 480f, 0);
            }

        }
    }

    //顶部区域-日期栏目-时间速度控制按钮-更新
    public void UpdateTimeButtonState()
    {
        if (gc.timeFlowSpeed == 0)
        {
            top_pauseBtn.interactable = false;
            top_playBtn.interactable = true;
            top_fastBtn.interactable = true;
        }
        else if (gc.timeFlowSpeed == 1)
        {
            top_pauseBtn.interactable = true;
            top_playBtn.interactable = false;
            top_fastBtn.interactable = true;
        }
        else if (gc.timeFlowSpeed == 2)
        {
            top_pauseBtn.interactable = true;
            top_playBtn.interactable = true;
            top_fastBtn.interactable = false;
        }
    }
    #endregion

    #region 【方法组】左侧区域
    //左侧区域-收藏库信息-更新
    public void UpdateInventoryNum()
    {
        left_inventoryNumText.text = gc.forceDic[0].rProductNow + "/" + gc.forceDic[0].rProductLimit;
        left_inventoryNumBgRt.sizeDelta = new Vector2(gc.forceDic[0].rProductLimit, 16f);
        left_inventoryNumBarRt.sizeDelta = new Vector2((float)gc.forceDic[0].rProductNow / gc.forceDic[0].rProductLimit * gc.forceDic[0].rProductLimit, 16f);
    }

    //左侧区域-功能按钮组-收藏装备按钮-数字文本-更新
    public void UpdateButtonItemNum()
    {
        int num = 0;
        foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
        {
            if (kvp.Value.districtID == -1 && kvp.Value.heroID == -1 && kvp.Value.isGoods == false)
            {
                num++;
            }
        }

        if (num > 0)
        {
            left_inventoryEquipNumText.text = num.ToString();
            left_inventoryEquipNumRt.sizeDelta = new Vector2(left_inventoryEquipNumText.preferredWidth + 8f, 20f);
        }
        else
        {
            left_inventoryEquipNumText.text = "";
            left_inventoryEquipNumRt.sizeDelta = Vector2.zero;
        }

    }

    //左侧区域-功能按钮组-收藏卷轴按钮-数字文本-更新
    public void UpdateButtonSkillNum()
    {
        int num = 0;
        foreach (KeyValuePair<int, SkillObject> kvp in gc.skillDic)
        {
            if (kvp.Value.districtID == -1 && kvp.Value.heroID == -1 && kvp.Value.isGoods == false)
            {
                num++;
            }
        }

        if (num > 0)
        {
            left_inventoryScrollNumText.text = num.ToString();
            left_inventoryScrollNumRt.sizeDelta = new Vector2(left_inventoryScrollNumText.preferredWidth + 8f, 20f);
        }
        else
        {
            left_inventoryScrollNumText.text = "";
            left_inventoryScrollNumRt.sizeDelta = Vector2.zero;
        }

    }

    //左侧区域-功能按钮组-英雄按钮-数字文本-更新
    public void UpdateButtonHeroNum()
    {
        left_heroNumText.text = gc.GetHeroNum(0).ToString();
        left_heroNumRt.sizeDelta = new Vector2(left_heroNumText.preferredWidth + 8f, 20f);
    }
    #endregion

    #region 【方法组】底部区域
    //底部区域-探险队栏目-导航条文本-更新
    void UpdateAdventurePageText()
    {
        string str = "";
        for (byte i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (adventureTeamIDList.Contains(i))
            {
                str += "◈";
            }
            else
            {
                str += "·";
            }
        }
        bottom_adventurePageText.text = str;

        bottom_adventurePageText.GetComponentInParent<RectTransform>().sizeDelta = new Vector2(bottom_adventurePageText.preferredWidth + 40f, 24f);
    }

    //底部区域-探险队栏目-导航条向前切换
    void AdventureStartIndexToLast()
    {
        if (adventureStartIndex == 0)
        {
            return;
        }
        adventureStartIndex--;
        UpdateAdventureAll();
    }

    //底部区域-探险队栏目-导航条向后切换
    void AdventureStartIndexToNext()
    {
        if (gc.adventureTeamList.Count <= 3)
        {
            return;
        }
        if (adventureStartIndex >=gc.adventureTeamList.Count-3 )
        {
            return;
        }
        adventureStartIndex++;
        UpdateAdventureAll();
    }

    //底部区域-探险队栏目-探险队块（全部）-更新
    public void UpdateAdventureAll()
    {
        adventureTeamIDList.Clear();
        for (byte i = 0; i < System.Math.Min(gc.adventureTeamList.Count, 3); i++)
        {
            adventureTeamIDList.Add((byte)(adventureStartIndex + i));
        }
        UpdateAdventurePageText();
        for (int i = 0 ; i <System.Math.Min(gc.adventureTeamList.Count, 3) ; i++)
        {
            UpdateAdventure(i, (byte)(adventureStartIndex + i));
        }
        for (int i = System.Math.Min(gc.adventureTeamList.Count, 3); i < 3; i++)
        {
            HideAdventure(i);
        }

    }

    //底部区域-探险队栏目-探险队块（单个队伍）-更新
    public void UpdateAdventureSingle(byte teamID)
    {
        if (adventureTeamIDList.Contains(teamID))
        {
            UpdateAdventure(adventureTeamIDList.IndexOf(teamID), teamID);
        }
    }

    //底部区域-探险队栏目-探险队块信息（单个队伍）-更新
    public void UpdateAdventure(int index,byte teamID)
    {
        bottom_adventureRt[index].localScale=Vector2.one;
        bottom_adventure_teamNameText[index].text="第"+(teamID+1) +"探险队";
        // bottom_adventure_detailBtn[index].;
        if (gc.adventureTeamList[teamID].state == AdventureState.Free)
        {
            bottom_adventure_mapImage[index].overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_B", typeof(Sprite)) as Sprite;
            bottom_adventure_desText[index].text = "未指派";
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Sending)
        {
            bottom_adventure_mapImage[index].overrideSprite = Resources.Load("Image/AdventureBG/ABG_OnTheWay", typeof(Sprite)) as Sprite;
            bottom_adventure_desText[index].text = "前往目的地中";
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Backing)
        {
            bottom_adventure_mapImage[index].overrideSprite = Resources.Load("Image/AdventureBG/ABG_OnTheWay", typeof(Sprite)) as Sprite;
            bottom_adventure_desText[index].text = "返回中";
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
        {
            bottom_adventure_mapImage[index].overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_B", typeof(Sprite)) as Sprite;
            bottom_adventure_desText[index].text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "[营地待命]";
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            bottom_adventure_mapImage[index].overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_B", typeof(Sprite)) as Sprite;
            float rate = (float)gc.adventureTeamList[teamID].nowDay / DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum;


            if (gc.adventureTeamList[teamID].action == AdventureAction.Fight)
            {
                bottom_adventure_desText[index].text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "[" + (int)(rate * 100) + "%][战斗中]";
            }
            else
            {
                bottom_adventure_desText[index].text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "[" + (int)(rate * 100) + "%][行进中]";
            }
        }
        else
        {
            bottom_adventure_mapImage[index].overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_B", typeof(Sprite)) as Sprite;
            float rate = (float)gc.adventureTeamList[teamID].nowDay / DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].PartNum;

            if (gc.adventureTeamList[teamID].state == AdventureState.Done)
            {
                bottom_adventure_desText[index].text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "[" + (int)(rate * 100) + "%]<color=green>[完成]</color>";
            }
            else if (gc.adventureTeamList[teamID].state == AdventureState.Retreat)
            {
                bottom_adventure_desText[index].text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "[" + (int)(rate * 100) + "%]<color=red>[中止]</color>";
            }
            else if (gc.adventureTeamList[teamID].state == AdventureState.Fail)
            {
                bottom_adventure_desText[index].text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name + "[" + (int)(rate * 100) + "%]<color=red>[失败]</color>";
            }
        }

        if (gc.adventureTeamList[teamID].log.Count > 1)
        {
            bottom_adventure_logText[index].text = gc.adventureTeamList[teamID].log[gc.adventureTeamList[teamID].log.Count - 1] + "\n" + gc.adventureTeamList[teamID].log[gc.adventureTeamList[teamID].log.Count - 2];
        }
        else if (gc.adventureTeamList[teamID].log.Count ==1)
        {
            bottom_adventure_logText[index].text = gc.adventureTeamList[teamID].log[0];
        }
        else
        {
            bottom_adventure_logText[index].text = "";
        }


        for (int i = 0; i < gc.adventureTeamList[teamID].heroIDList.Count; i++)
        {
            bottom_adventure_herosTf[index].GetChild(i).GetComponent<RectTransform>().localScale = Vector2.one;
            bottom_adventure_herosTf[index].GetChild(i).GetComponent<Image>().overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[gc.adventureTeamList[teamID].heroIDList[i]].pic+"/Pic", typeof(Sprite)) as Sprite;
            bottom_adventure_herosTf[index].GetChild(i).GetChild(0).GetComponent<Text>().text = gc.heroDic[gc.adventureTeamList[teamID].heroIDList[i]].level.ToString();
            bottom_adventure_herosTf[index].GetChild(i).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2((float)gc.adventureTeamList[teamID].heroHpList[i] / gc.GetHeroAttr(Attribute.Hp, gc.adventureTeamList[teamID].heroIDList[i]) * 32f, 4f);
            bottom_adventure_herosTf[index].GetChild(i).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2((float)gc.adventureTeamList[teamID].heroMpList[i] / gc.GetHeroAttr(Attribute.Mp, gc.adventureTeamList[teamID].heroIDList[i]) * 32f, 4f);


        }
        for (int i = gc.adventureTeamList[teamID].heroIDList.Count; i < 3; i++)
        {
            bottom_adventure_herosTf[index].GetChild(i).GetComponent<RectTransform>().localScale = Vector2.zero;
        }


        if (gc.adventureTeamList[teamID].state == AdventureState.Free)
        {
            bottom_adventure_detailBtn[index].GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        else
        {
            bottom_adventure_detailBtn[index].GetComponent<RectTransform>().localScale = Vector2.one;
            bottom_adventure_detailBtn[index].onClick.RemoveAllListeners();
            bottom_adventure_detailBtn[index].onClick.AddListener(delegate ()
            {
                AdventureMainPanel.Instance.OnShow(teamID);
            });
        }
    }

    //底部区域-探险队栏目-探险队块（单个队伍）-隐藏
    public void HideAdventure(int index)
    {
        bottom_adventureRt[index].localScale = Vector2.zero;
    }
    #endregion

}
