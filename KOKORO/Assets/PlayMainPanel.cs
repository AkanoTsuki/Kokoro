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

    public Button left_heroMainBtn;
    public Button left_adventureMainBtn;




 
    public Image bottom_baseline_disImage;
    public Text bottom_baseline_nameText;



  
    public Button bottom_baseline_messageBtn;
    public Text bottom_baseline_messageSignText;
    public Button bottom_baseline_openAllBtn;
    public Text bottom_baseline_openAllSignText;
    public Button bottom_baseline_hideAllBtn;
    public Text bottom_baseline_hideAllSignText;

 

    public RectTransform bottom_infoBlockRt;
    public Text bottom_info_personPeopleText;
    public Text bottom_info_personHeroText;
    public Text bottom_info_personWorkerText;

    public Text bottom_info_buildingBuildingText;


    public Text bottom_info_elementWindText;
    public Text bottom_info_elementFireText;
    public Text bottom_info_elementWaterText;
    public Text bottom_info_elementGroundText;
    public Text bottom_info_elementLightText;
    public Text bottom_info_elementDarkText;



    public RectTransform bottom_statusBlockRt;

 

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
        

        left_heroMainBtn.onClick.AddListener(delegate () { gci.OpenHeroSelect(); });
        left_adventureMainBtn.onClick.AddListener(delegate () { gci.OpenAdventureMain(); });


        top_districtDd.onValueChanged.AddListener(delegate { gc.nowCheckingDistrictID = (short)top_districtDd.value;  });
        top_districtBtn.onClick.AddListener(delegate () { ShowDistrictMap(); });

        top_saveBtn.onClick.AddListener(delegate () { gci.GameSave(); });
        top_pauseBtn.onClick.AddListener(delegate () { gci.TimePause(); });
        top_playBtn.onClick.AddListener(delegate () { gci.TimePlay(); });
        top_fastBtn.onClick.AddListener(delegate () { gci.TimeFast(); });
      

    

       
        bottom_baseline_messageBtn.onClick.AddListener(delegate () { if (IsShowMessageBlock) { HideMessageBlock(); } else { ShowMessageBlock(); } });
    }

    public override void OnShow()
    {
        SetAnchoredPosition(0, 0);

        UpdateKingdomInfo();
        UpdateGold();
        UpdateDateInfo();
        UpdateTimeButtonState();
        UpdateDistrictInfo(gc.nowCheckingDistrictID);

        UpdateTopDistrict();

 
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
            DistrictMapPanel.Instance.OnShow(0, -90);
            top_districtBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
       
    }

    public void UpdateDistrictInfo(short districtID)
    {
        bottom_baseline_nameText.text = gc.districtDic[districtID].name+"·"+ gc.districtDic[districtID].baseName;
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
