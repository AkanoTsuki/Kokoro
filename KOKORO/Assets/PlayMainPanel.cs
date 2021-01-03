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

    public Button left_districtMainBtn;
    public Button left_inventoryMainBtn;
    public Button left_inventoryScrollBtn;
    public Button left_heroMainBtn;
    public Button left_adventureMainBtn;
    public Button left_buildBtn;
    public Button left_buildingSelectBtn;
    public Button left_marketBtn;

    public Image bottom_disImage;
    public Text bottom_nameText;
    public Text bottom_foodText;
    public Text bottom_woodText;
    public Text bottom_stoneText;
    public Text bottom_metalText;
    public Text bottom_leatherText;
    public Text bottom_clothText;
    public Text bottom_twineText;
    public Text bottom_boneText;
    public Text bottom_totalText;

    List<Button> leftBtnList = new List<Button>();
    short nowLeftIndex = -1;//TODO:关闭其他窗口系列，或子窗口

    bool Leftis0 = true;
    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();
        
        left_districtMainBtn.onClick.AddListener(delegate () { gci.OpenDistrictMain(); });
        left_inventoryMainBtn.onClick.AddListener(delegate () { gci.OpenItemListAndInfo(); });
        left_inventoryScrollBtn.onClick.AddListener(delegate () { gci.OpenSkillListAndInfo(); });
        left_buildBtn.onClick.AddListener(delegate () { gci.OpenBuild(); });
        left_buildingSelectBtn.onClick.AddListener(delegate () { gci.OpenBuildingSelect(); });
        left_heroMainBtn.onClick.AddListener(delegate () { gci.OpenHeroSelect(); });
        left_adventureMainBtn.onClick.AddListener(delegate () { gci.OpenAdventureMain(); });
        left_marketBtn.onClick.AddListener(delegate () { gci.OpenMarket(); });

        top_saveBtn.onClick.AddListener(delegate () { gci.GameSave(); });
        top_pauseBtn.onClick.AddListener(delegate () { gci.TimePause(); });
        top_playBtn.onClick.AddListener(delegate () { gci.TimePlay(); });
        top_fastBtn.onClick.AddListener(delegate () { gci.TimeFast(); });
      


        leftBtnList.Add(left_districtMainBtn);
        leftBtnList.Add(left_inventoryMainBtn);
        leftBtnList.Add(left_buildBtn);
        leftBtnList.Add(left_buildingSelectBtn);
        leftBtnList.Add(left_heroMainBtn);
        leftBtnList.Add(left_adventureMainBtn);
    }

    public override void OnShow()
    {
        SetAnchoredPosition(0, 0);

        UpdateKingdomInfo();
        UpdateGold();
        UpdateDateInfo();
        UpdateTimeButtonState();
        UpdateDistrictInfo(gc.nowCheckingDistrictID);
        UpdateResourcesInfo(gc.nowCheckingDistrictID);
      
    }

    public void UpdateKingdomInfo()
    {
        top_nameText.text = gc.heroDic[0].name+"的领地";
      
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

    public void UpdateLeftButtonState()
    {
        
    }

    public void UpdateDistrictInfo(short districtID)
    {
        bottom_nameText.text = gc.districtDic[districtID].name+"·"+ gc.districtDic[districtID].baseName;
    }

    public void UpdateResourcesInfo(short districtID)
    {
        bottom_foodText.text = (gc.districtDic[districtID].rFoodCereal + gc.districtDic[districtID].rFoodVegetable + gc.districtDic[districtID].rFoodFruit + gc.districtDic[districtID].rFoodMeat + gc.districtDic[districtID].rFoodFish) + "/" + gc.districtDic[districtID].rFoodLimit;
        bottom_woodText.text = gc.districtDic[districtID].rStuffWood.ToString();
        bottom_stoneText.text = gc.districtDic[districtID].rStuffStone.ToString();
        bottom_metalText.text = gc.districtDic[districtID].rStuffMetal.ToString();
        bottom_leatherText.text = gc.districtDic[districtID].rStuffLeather.ToString();
        bottom_clothText.text = gc.districtDic[districtID].rStuffCloth.ToString();
        bottom_twineText.text = gc.districtDic[districtID].rStuffTwine.ToString();
        bottom_boneText.text = gc.districtDic[districtID].rStuffBone.ToString();
        bottom_totalText.text = (gc.districtDic[districtID].rStuffWood + gc.districtDic[districtID].rStuffStone +
                gc.districtDic[districtID].rStuffMetal + gc.districtDic[districtID].rStuffLeather +
                gc.districtDic[districtID].rStuffCloth + gc.districtDic[districtID].rStuffTwine + gc.districtDic[districtID].rStuffBone) + "/" + gc.districtDic[districtID].rStuffLimit;
    }


}
