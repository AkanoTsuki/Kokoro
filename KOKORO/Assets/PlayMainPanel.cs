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

    public Button bottom_baseline_resourcesBtn;
    public Text bottom_baseline_resourcesFoodText;
    public Text bottom_baseline_resourcesStuffText;
    public Text bottom_baseline_resourcesProductText;
    public Text bottom_baseline_resourcesSignText;

    public Button bottom_baseline_infoBtn;
    public Text bottom_baseline_infoPeopleText;
    public Text bottom_baseline_infoHeroText;
    public Text bottom_baseline_infoWorkerText;
    public Text bottom_baseline_infoSignText;

    public Button bottom_baseline_statusBtn;
  
    public Button bottom_baseline_messageBtn;
    public Text bottom_baseline_messageSignText;
    public Button bottom_baseline_openAllBtn;
    public Text bottom_baseline_openAllSignText;
    public Button bottom_baseline_hideAllBtn;
    public Text bottom_baseline_hideAllSignText;

    public RectTransform bottom_resourcesBlockRt;
    public Text bottom_resources_foodCerealText;
    public Text bottom_resources_foodVegetableText;
    public Text bottom_resources_foodFruitText;
    public Text bottom_resources_foodMeatText;
    public Text bottom_resources_foodFishText;
    public Text bottom_resources_foodBeerText;
    public Text bottom_resources_foodWineText;
    public Text bottom_resources_foodTotalText;

    public Text bottom_resources_stuffWoodText;
    public Text bottom_resources_stuffStoneText;
    public Text bottom_resources_stuffMetalText;
    public Text bottom_resources_stuffLeatherText;
    public Text bottom_resources_stuffClothText;
    public Text bottom_resources_stuffTwineText;
    public Text bottom_resources_stuffBoneText;
    public Text bottom_resources_stuffWindText;
    public Text bottom_resources_stuffFireText;
    public Text bottom_resources_stuffWaterText;
    public Text bottom_resources_stuffGroundText;
    public Text bottom_resources_stuffLightText;
    public Text bottom_resources_stuffDarkText;
    public Text bottom_resources_stuffTotalText;

    public Text bottom_resources_productWeaponText;
    public Text bottom_resources_productArmorText;
    public Text bottom_resources_productJewelryText;
    public Text bottom_resources_productSkillRollText;
    public Text bottom_resources_productTotalText;

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

    List<Button> leftBtnList = new List<Button>();
    short nowLeftIndex = -1;//TODO:关闭其他窗口系列，或子窗口

    public bool IsShowResourcesBlock = false;
    public bool IsShowInfoBlock = false;
    public bool IsShowStatusBlock = false;
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
      

        leftBtnList.Add(left_heroMainBtn);
        leftBtnList.Add(left_adventureMainBtn);

       
        bottom_baseline_resourcesBtn.onClick.AddListener(delegate () { if (IsShowResourcesBlock) { HideResourcesBlock(); } else { ShowResourcesBlock(gc.nowCheckingDistrictID); }  });
        bottom_baseline_infoBtn.onClick.AddListener(delegate () { if (IsShowInfoBlock) { HideInfoBlock(); } else { ShowInfoBlock(gc.nowCheckingDistrictID); } });
        bottom_baseline_statusBtn.onClick.AddListener(delegate () { if (IsShowStatusBlock) { HideStatusBlock(); } else { ShowStatusBlock(gc.nowCheckingDistrictID); } });

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

        HideResourcesBlock();
        HideInfoBlock();
        HideStatusBlock();
        UpdateBaselineResourcesText(gc.nowCheckingDistrictID);
        UpdateBaselineInfoText(gc.nowCheckingDistrictID);
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


    public void UpdateBaselineResourcesText(short districtID)
    {
        bottom_baseline_resourcesFoodText.text = gc.GetDistrictFoodAll(districtID)+"/"+ gc.districtDic[districtID].rFoodLimit;
        bottom_baseline_resourcesStuffText.text = gc.GetDistrictStuffAll(districtID) + "/" + gc.districtDic[districtID].rStuffLimit;
        bottom_baseline_resourcesProductText.text = gc.GetDistrictProductAll(districtID) + "/" + gc.districtDic[districtID].rProductLimit;
    }
    public void UpdateBaselineInfoText(short districtID)
    {
        bottom_baseline_infoPeopleText.text = gc.districtDic[districtID].people + "/" + gc.districtDic[districtID].peopleLimit;
        bottom_baseline_infoHeroText.text = gc.districtDic[districtID].heroList.Count.ToString();
        bottom_baseline_infoWorkerText.text = gc.districtDic[districtID].worker.ToString();
    }


    public void UpdateResourcesBlock(short districtID)
    {
        bottom_resources_foodCerealText.text = gc.districtDic[districtID].rFoodCereal.ToString();
        bottom_resources_foodVegetableText.text = gc.districtDic[districtID].rFoodVegetable.ToString();
        bottom_resources_foodFruitText.text = gc.districtDic[districtID].rFoodFruit.ToString();
        bottom_resources_foodMeatText.text = gc.districtDic[districtID].rFoodMeat.ToString();
        bottom_resources_foodFishText.text = gc.districtDic[districtID].rFoodFish.ToString();
        bottom_resources_foodBeerText.text = gc.districtDic[districtID].rFoodBeer.ToString();
        bottom_resources_foodWineText.text = gc.districtDic[districtID].rFoodWine.ToString();
        bottom_resources_foodTotalText.text ="食物库存 "+ gc.GetDistrictFoodAll(districtID) + "/" + gc.districtDic[districtID].rFoodLimit;

        bottom_resources_stuffWoodText.text = gc.districtDic[districtID].rStuffWood.ToString();
        bottom_resources_stuffStoneText.text = gc.districtDic[districtID].rStuffStone.ToString();
        bottom_resources_stuffMetalText.text = gc.districtDic[districtID].rStuffMetal.ToString();
        bottom_resources_stuffLeatherText.text = gc.districtDic[districtID].rStuffLeather.ToString();
        bottom_resources_stuffClothText.text = gc.districtDic[districtID].rStuffCloth.ToString();
        bottom_resources_stuffTwineText.text = gc.districtDic[districtID].rStuffTwine.ToString();
        bottom_resources_stuffBoneText.text = gc.districtDic[districtID].rStuffBone.ToString();
        bottom_resources_stuffWindText.text = gc.districtDic[districtID].rStuffWind.ToString();
        bottom_resources_stuffFireText.text = gc.districtDic[districtID].rStuffFire.ToString();
        bottom_resources_stuffWaterText.text = gc.districtDic[districtID].rStuffWater.ToString();
        bottom_resources_stuffGroundText.text = gc.districtDic[districtID].rStuffGround.ToString();
        bottom_resources_stuffLightText.text = gc.districtDic[districtID].rStuffLight.ToString();
        bottom_resources_stuffDarkText.text = gc.districtDic[districtID].rStuffDark.ToString();
        bottom_resources_stuffTotalText.text = "材料库存 " + gc.GetDistrictStuffAll(districtID) + "/" + gc.districtDic[districtID].rStuffLimit;

        bottom_resources_productWeaponText.text = gc.districtDic[districtID].rProductWeapon.ToString();
        bottom_resources_productArmorText.text = gc.districtDic[districtID].rProductArmor.ToString();
        bottom_resources_productJewelryText.text = gc.districtDic[districtID].rProductJewelry.ToString();
        bottom_resources_productSkillRollText.text = gc.districtDic[districtID].rProductScroll.ToString();
        bottom_resources_productTotalText.text = "制品库存 " + gc.GetDistrictProductAll(districtID) + "/" + gc.districtDic[districtID].rProductLimit;
    }
    public void ShowResourcesBlock(short districtID)
    {
        UpdateResourcesBlock(districtID);
        bottom_baseline_resourcesSignText.text = "▼";
        bottom_resourcesBlockRt.localScale = Vector2.one;
        IsShowResourcesBlock = true;
    }
    public void HideResourcesBlock()
    {
        bottom_baseline_resourcesSignText.text = "▲";
        bottom_resourcesBlockRt.localScale = Vector2.zero;
        IsShowResourcesBlock = false;
    }

    public void UpdateInfoBlock(short districtID)
    {
        bottom_info_personPeopleText.text = gc.districtDic[districtID].people + "/" + gc.districtDic[districtID].peopleLimit;
        bottom_info_personHeroText.text = gc.districtDic[districtID].heroList.Count.ToString();
        bottom_info_personWorkerText.text = gc.districtDic[districtID].worker.ToString();

        bottom_info_buildingBuildingText.text = gc.districtDic[districtID].buildingList.Count.ToString();


        bottom_info_elementWindText.text = "风 "+gc.districtDic[districtID].eWind;
        bottom_info_elementFireText.text = "火 " + gc.districtDic[districtID].eFire;
        bottom_info_elementWaterText.text = "水 " + gc.districtDic[districtID].eWater;
        bottom_info_elementGroundText.text = "地 " + gc.districtDic[districtID].eGround;
        bottom_info_elementLightText.text = "光 " + gc.districtDic[districtID].eLight;
        bottom_info_elementDarkText.text = "暗 " + gc.districtDic[districtID].eDark;


    }
    public void ShowInfoBlock(short districtID)
    {
        UpdateInfoBlock(districtID);
        bottom_baseline_infoSignText.text = "▼";
        bottom_infoBlockRt.localScale = Vector2.one;
        IsShowInfoBlock = true;
    }
    public void HideInfoBlock()
    {
        bottom_baseline_infoSignText.text = "▲";
        bottom_infoBlockRt.localScale = Vector2.zero;
        IsShowInfoBlock = false;
    }

    public void ShowStatusBlock(short districtID)
    {
        // UpdateStatusBlock(districtID);
        //bottom_baseline_statusSignText.text = "▼";
        bottom_statusBlockRt.localScale = Vector2.one;
        IsShowStatusBlock = true;
    }
    public void HideStatusBlock()
    {
       // bottom_baseline_statusSignText.text = "▲";
        bottom_statusBlockRt.localScale = Vector2.zero;
        IsShowStatusBlock = false;
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
