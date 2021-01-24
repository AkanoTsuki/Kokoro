using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayMainPanel : BasePanel
{
    public static PlayMainPanel Instance;
    GameControlInPlay gci;
    GameControl gc;

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

    public Dropdown top_districtDd;
    public Button top_districtBtn;

    public Button left_technologyBtn;
    public Button left_heroMainBtn;
    public Button left_adventureMainBtn;
 

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
    //public List<List<RectTransform>> bottom_adventure_heroRt;
    //public List<List<Image>> bottom_adventure_hero_picImage;
    //public List<List<Text>> bottom_adventure_hero_levelText;
    //public List<List<RectTransform>> bottom_adventure_hero_hpRt;
    //public List<List<RectTransform>> bottom_adventure_hero_mpRt;



    public Button bottom_baseline_messageBtn;
    public Text bottom_baseline_messageSignText;
    public Button bottom_baseline_openAllBtn;
    public Text bottom_baseline_openAllSignText;
    public Button bottom_baseline_hideAllBtn;
    public Text bottom_baseline_hideAllSignText;


    public List<byte> adventureTeamIDList = new List<byte>();
    byte adventureStartIndex = 0;

    public bool IsShowMessageBlock = false;
    bool Leftis0 = true;
    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();



        left_technologyBtn.onClick.AddListener(delegate () { gci.OpenTechnology(); });
        left_heroMainBtn.onClick.AddListener(delegate () { gci.OpenHeroSelect(); });
        left_adventureMainBtn.onClick.AddListener(delegate () { gci.OpenAdventureMain(); });


        top_districtDd.onValueChanged.AddListener(delegate { gc.nowCheckingDistrictID = (short)top_districtDd.value;  });
        top_districtBtn.onClick.AddListener(delegate () { ShowDistrictMap(); });

        top_saveBtn.onClick.AddListener(delegate () { gci.GameSave(); });
        top_pauseBtn.onClick.AddListener(delegate () { gci.TimePause(); });
        top_playBtn.onClick.AddListener(delegate () { gci.TimePlay(); });
        top_fastBtn.onClick.AddListener(delegate () { gci.TimeFast(); });



        bottom_adventureLastBtn.onClick.AddListener(delegate () { AdventureStartIndexToLast(); });
        bottom_adventureNextBtn.onClick.AddListener(delegate () { AdventureStartIndexToNext(); });
        bottom_baseline_messageBtn.onClick.AddListener(delegate () { if (IsShowMessageBlock) { HideMessageBlock(); } else { ShowMessageBlock(); } });
    }

    public override void OnShow()
    {
        SetAnchoredPosition(0, 0);

        UpdateKingdomInfo();
        UpdateGold();
        UpdateDateInfo();
        UpdateTimeButtonState();


        UpdateTopDistrict();
        UpdateAdventurePageText();
        UpdateAdventureAll();
    }

    public void UpdateKingdomInfo()
    {
        top_flagImage.overrideSprite = Resources.Load("Image/Other/icon_flag_"+gc.forceFlag+"_a", typeof(Sprite)) as Sprite;
        top_nameText.text = "克克洛地区";
      
    }
    public void UpdateGold()
    {
        top_goldText.text = gc.gold.ToString();
    }

    public void UpdateDateInfo()
    {
        UpdateYearSeason();
        UpdateMonthDayHour();
        UpdateTimeBar();
    }
    public void UpdateMonthDayHour()
    {
        top_dateRightText.text = gc.timeMonth + "月" + gc.timeDay + "日 "+gc.OutputWeekStr(gc.timeWeek,true);
        top_hourText.text = gc.timeHour.ToString();
    }

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



    public void UpdateTopDistrict()
    {
        List<string> disList = new List<string>();
        for (int i = 0; i < gc.districtDic.Length; i++)
        {
            if (gc.districtDic[i].isOpen)
            {
                disList.Add(gc.districtDic[i].name + "·" + gc.districtDic[i].baseName);
            }
            
        }

        top_districtDd.ClearOptions();
        top_districtDd.AddOptions(disList);
    }

    void ShowDistrictMap()
    {
        if (DistrictMapPanel.Instance.isShow == false)
        {
            DistrictMapPanel.Instance.OnShow();
            top_districtBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
       
    }

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

    void AdventureStartIndexToLast()
    {
        if (adventureStartIndex == 0)
        {
            return;
        }

        adventureStartIndex--;

    
        UpdateAdventureAll();
    }

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

    public void UpdateAdventureSingle(byte teamID)
    {
        if (adventureTeamIDList.Contains(teamID))
        {
            UpdateAdventure(adventureTeamIDList.IndexOf(teamID), teamID);
        }
    }

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
            bottom_adventure_mapImage[index].overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_B", typeof(Sprite)) as Sprite;
            bottom_adventure_desText[index].text = "前往目的地中";
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Backing)
        {
            bottom_adventure_mapImage[index].overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_B", typeof(Sprite)) as Sprite;
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


        if (gc.adventureTeamList[teamID].state == AdventureState.Free||
            gc.adventureTeamList[teamID].state == AdventureState.Sending ||
            gc.adventureTeamList[teamID].state == AdventureState.Backing ||
            gc.adventureTeamList[teamID].state == AdventureState.NotSend)
        {
            bottom_adventure_detailBtn[index].GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        else
        {
            bottom_adventure_detailBtn[index].GetComponent<RectTransform>().localScale = Vector2.one;
            bottom_adventure_detailBtn[index].onClick.RemoveAllListeners();
            bottom_adventure_detailBtn[index].onClick.AddListener(delegate ()
            {
                /*详情*/
                AdventureTeamPanel.Instance.OnShow(teamID, 60, -88);
            });
        }
      

    }

    public void HideAdventure(int index)
    {
        bottom_adventureRt[index].localScale = Vector2.zero;
    }

    public void ShowMessageBlock()
    {
        // UpdateStatusBlock(districtID);
       bottom_baseline_messageSignText.text = "消息框 ▼ ";
        MessagePanel.Instance.OnShow(0,26);
        IsShowMessageBlock = true;
    }
    public void HideMessageBlock()
    {
        // UpdateStatusBlock(districtID);
        bottom_baseline_messageSignText.text = "消息框 ▲ ";
        MessagePanel.Instance.OnHide();
        IsShowMessageBlock = false;
    }
}
