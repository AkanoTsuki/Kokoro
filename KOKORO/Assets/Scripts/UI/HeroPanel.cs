using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanel : BasePanel
{
    public static HeroPanel Instance;

    GameControl gc;

    #region 【UI控件】
    public Text nameText;
    public Image picImage;
    public Image sexImage;
    public Text infoText;

    public List<Image> iconImage;
    public RectTransform icon_infoRt;
    public Text icon_infoNameText;
    public Text icon_infoDesText;
    //public RectTransform icon_infoNeedElementRt;
    public List<Image> icon_infoNeedElementImage;

    public Button title_changeNameBtn;
    public RectTransform title_changeNameRt;
    public InputField title_changeNameIf;
    public Button title_changeNameConfirmBtn;

    public Button infoFight_page1Btn;
    public Button infoFight_page2Btn;
    public Text infoFight_des1Text;
    public Text infoFight_des2Text;
    public Text infoWork_desText;

    public RectTransform equipRt;
    public Text equip_titleText;
    public Button totalSet_equipBtn;
    public Button equip_weaponBtn;
    public Button equip_subhandBtn;
    public Button equip_headBtn;
    public Button equip_bodyBtn;
    public Button equip_handBtn;
    public Button equip_backBtn;
    public Button equip_footBtn;
    public Button equip_neckBtn;
    public Button equip_finger1Btn;
    public Button equip_finger2Btn;
    public Image equip_weaponImage;
    public Image equip_subhandImage;
    public Image equip_headImage;
    public Image equip_bodyImage;
    public Image equip_handImage;
    public Image equip_backImage;
    public Image equip_footImage;
    public Image equip_neckImage;
    public Image equip_finger1Image;
    public Image equip_finger2Image;
    public Text equip_weaponText;
    public Text equip_subhandText;
    public Text equip_headText;
    public Text equip_bodyText;
    public Text equip_handText;
    public Text equip_backText;
    public Text equip_footText;
    public Text equip_neckText;
    public Text equip_finger1Text;
    public Text equip_finger2Text;
    public Button equip_weaponUnSetBtn;
    public Button equip_subhandUnSetBtn;
    public Button equip_headUnSetBtn;
    public Button equip_bodyUnSetBtn;
    public Button equip_handUnSetBtn;
    public Button equip_backUnSetBtn;
    public Button equip_footUnSetBtn;
    public Button equip_neckUnSetBtn;
    public Button equip_finger1UnSetBtn;
    public Button equip_finger2UnSetBtn;

    public RectTransform skillRt;
    public List<Image> skill_bgImage;//12个
    public List<Button> skill_Btn;
    public List<Image> skill_Image;
    public List<RectTransform> skill_textRt;
    public List<Text> skill_Text;
    public List<Text> skill_infoText;//4个

    public RectTransform pageSkillRt;
    public GameObject pageSkill_listGo;

    public RectTransform pageDataAndHistoryRt;
    public Text pageDataAndHistory_dataText;
    public Text pageDataAndHistory_historyText;

    public Button skillBtn;
    public Button dataAndHistoryBtn;
    public Button recruitBtn;

    public Button closeBtn;
    #endregion

    //运行变量
    public int nowSelectedHeroID = -1;
    public bool nowEquipState = false;//false为查看模式 true为调整模式
    bool IsShowPageSkill = false;
    bool IsShowPageDataAndHistory = false;

    //对象池
    List<GameObject> skillGoPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        skillBtn.onClick.AddListener(delegate () {
            if (IsShowPageSkill)
            {
                HideSkillPage();
            }
            else
            {
                ShowSkillPage(gc.heroDic[ nowSelectedHeroID]);
            }
            });
        dataAndHistoryBtn.onClick.AddListener(delegate () {
            if (IsShowPageDataAndHistory)
            {
                HideDataAndHistoryPage();
            }
            else
            {
                ShowDataAndHistoryPage(gc.heroDic[nowSelectedHeroID]);
            }
        });
        totalSet_equipBtn.onClick.AddListener(delegate () { nowEquipState = !nowEquipState; UpdateEquipButtonStatus(); });
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
        title_changeNameBtn.onClick.AddListener(delegate () { ShowChangeName(); });
        title_changeNameConfirmBtn.onClick.AddListener(delegate () { gc.HeroChangeName(nowSelectedHeroID, title_changeNameIf.text);HideChangeName(); });
    }

    //主面板显示
    public void OnShow( HeroObject heroObject,bool equipState, int x,int y)
    {
        nowEquipState = equipState;

        UpdateAllInfo( heroObject);    
        SetTotalButton(-1,heroObject.id, false);
        UpdateEquipButtonStatus();
        equipRt.localScale = Vector2.one;
        skillRt.localScale = Vector2.one;

        //GetComponent<RectTransform>().sizeDelta = new Vector2(559f,520f);
        SetAnchoredPosition(x, y);
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        isShow = true;
    }

    //主面板显示（征募用）
    public void OnShow(int buildingID, HeroObject heroObject,  int x, int y)
    {
        nowEquipState = false;

        UpdateAllInfo(heroObject);
        SetTotalButton(buildingID, heroObject.id,  true);
        UpdateEquipButtonStatus();
        equipRt.localScale = Vector2.zero;
        skillRt.localScale = Vector2.zero;

        //GetComponent<RectTransform>().sizeDelta = new Vector2(307f, 520f);
        SetAnchoredPosition(x, y);
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        isShow = true;
    }

    //主面板关闭
    public override void OnHide()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isShow = false;
    }

    //底部按钮组状态设置
    void SetTotalButton(int buildingID, int heroID, bool isRecruit)
    {
        if (isRecruit)
        {
            skillBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            dataAndHistoryBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            totalSet_equipBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
            recruitBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            recruitBtn.onClick.RemoveAllListeners();
            recruitBtn.onClick.AddListener(delegate () {
                gc.HeroRecruit(buildingID, heroID,0);
            });
        }
        else
        {
            skillBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            dataAndHistoryBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            totalSet_equipBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            recruitBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    //装备栏模式按钮设置
    void UpdateEquipButtonStatus()
    {
        if (nowEquipState==false)
        {
            equip_titleText.text = "装备[查看模式]";
            SetEquipButtonModeLook(equip_weaponBtn, equip_weaponUnSetBtn);
            SetEquipButtonModeLook(equip_subhandBtn, equip_subhandUnSetBtn);
            SetEquipButtonModeLook(equip_headBtn, equip_headUnSetBtn);
            SetEquipButtonModeLook(equip_bodyBtn, equip_bodyUnSetBtn);
            SetEquipButtonModeLook(equip_handBtn, equip_handUnSetBtn);
            SetEquipButtonModeLook(equip_backBtn, equip_backUnSetBtn);
            SetEquipButtonModeLook(equip_footBtn, equip_footUnSetBtn);
            SetEquipButtonModeLook(equip_neckBtn, equip_neckUnSetBtn);
            SetEquipButtonModeLook(equip_finger1Btn, equip_finger1UnSetBtn);
            SetEquipButtonModeLook(equip_finger2Btn, equip_finger2UnSetBtn);
        }
        else 
        {
            equip_titleText.text = "装备[调整模式]";
            SetEquipButtonModeSet(equip_weaponBtn, equip_weaponUnSetBtn, gc.heroDic[nowSelectedHeroID].equipWeapon, EquipPart.Weapon);
            SetEquipButtonModeSet(equip_subhandBtn, equip_subhandUnSetBtn, gc.heroDic[nowSelectedHeroID].equipSubhand, EquipPart.Subhand);
            SetEquipButtonModeSet(equip_headBtn, equip_headUnSetBtn, gc.heroDic[nowSelectedHeroID].equipHead, EquipPart.Head);
            SetEquipButtonModeSet(equip_bodyBtn, equip_bodyUnSetBtn, gc.heroDic[nowSelectedHeroID].equipBody, EquipPart.Body);
            SetEquipButtonModeSet(equip_handBtn, equip_handUnSetBtn, gc.heroDic[nowSelectedHeroID].equipHand, EquipPart.Hand);
            SetEquipButtonModeSet(equip_backBtn, equip_backUnSetBtn, gc.heroDic[nowSelectedHeroID].equipBack, EquipPart.Back);
            SetEquipButtonModeSet(equip_footBtn, equip_footUnSetBtn, gc.heroDic[nowSelectedHeroID].equipFoot, EquipPart.Foot);
            SetEquipButtonModeSet(equip_neckBtn, equip_neckUnSetBtn, gc.heroDic[nowSelectedHeroID].equipNeck, EquipPart.Neck);
            SetEquipButtonModeSet(equip_finger1Btn, equip_finger1UnSetBtn, gc.heroDic[nowSelectedHeroID].equipFinger1, EquipPart.Finger1);
            SetEquipButtonModeSet(equip_finger2Btn, equip_finger2UnSetBtn, gc.heroDic[nowSelectedHeroID].equipFinger2, EquipPart.Finger2);
        }

        
    }
    //装备栏模式按钮设置-查看模式
    void SetEquipButtonModeLook(Button btn, Button unsetBtn)
    {
        btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(delegate ()
        {
            int index = btn.transform.GetComponent<InteractiveLabel>().index;
            if (index != -1)
            {
                ItemListAndInfoPanel.Instance.nowItemID = index;
                ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            }
        });
        unsetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
    }
    //装备栏模式按钮设置-更换装备模式
    void SetEquipButtonModeSet(Button btn,Button unsetBtn,int heroEquipID,EquipPart equipPartA)
    {
        btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(delegate ()
        {
            int heroID = btn.transform.GetComponent<InteractiveLabel>().heroID;
            EquipPart equipPart = btn.transform.GetComponent<InteractiveLabel>().equipPart;
            ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
        });
        if (heroEquipID != -1)
        {
            unsetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            unsetBtn.onClick.RemoveAllListeners();
            unsetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(nowSelectedHeroID, equipPartA); });
        }
        else
        {
            unsetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    //默认信息更新
    public void UpdateAllInfo( HeroObject heroObject)
    {
        HideChangeName();
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3)
        {
            UpdateIcon(heroObject.id);
        }
    
        UpdateBasicInfo(heroObject);
        UpdateFightInfo(heroObject, EquipPart.None, null, 1);
        UpdateWorkInfo(heroObject);
        UpdateEquipAll(heroObject);
        UpdateSkillAll(heroObject);

        HideIconInfo();
        HideSkillPage();
        HideDataAndHistoryPage();
    }

    //能力页面-基础信息栏目-更新
    public void UpdateBasicInfo(HeroObject heroObject)
    {
        nameText.text = heroObject.name;
        picImage.overrideSprite = Resources.Load("Image/RolePic/" + heroObject.pic + "/Pic", typeof(Sprite)) as Sprite; ;
        sexImage.overrideSprite = Resources.Load("Image/Other/sex_" + (heroObject.sex==0?"man":"woman" ), typeof(Sprite)) as Sprite; ;
        string str = "";
        for (int i = 0; i < heroObject.characteristic.Count; i++)
        {
            str += " <color=#F3CE59>[" + DataManager.mCharacteristicDict[heroObject.characteristic[i]].Name+ "]</color>";
        }
        if (heroObject.halo != -1)
        {
            str += " <color=#F3CE59>[" + DataManager.mHaloDict[heroObject.halo].Name + "]</color>";
        }
        
        infoText.text = "Lv." + heroObject.level+"<color=#"+ DataManager.mHeroDict[heroObject.prototypeID].Color+ ">"+ DataManager.mHeroDict[heroObject.prototypeID].Name +
            "</color>[升级经验值<color=#FFFFFF>" + ((int)System.Math.Pow(1.05f, heroObject.level)*200-heroObject .exp)+
            "</color>][成长率<color=#7DF3AE>" + System.Math.Round( heroObject.groupRate,3)+ "</color>]" +
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex==3?
            ("\n薪金 <color=#FFFFFF>"+ heroObject.salary+ "</color>金币/月" +
            ( heroObject.workerInBuilding!=-1?(" <color=#66D5FF>在" + DataManager.mDistrictDict[gc.buildingDic[heroObject.workerInBuilding].districtID].Name+"的"+  gc.buildingDic[heroObject.workerInBuilding].name + "工作</color>") :"")+
            (heroObject.adventureInTeam!=-1?(" <color=#FF9167>已编入第" + (heroObject.adventureInTeam+1) + "探险队</color>") :"")
             ):""
            )+
            "\n"+DataManager.mHeroDict[ heroObject.prototypeID].Des+ str;
    }

    //能力页面-战斗属性栏目-更新
    public void UpdateFightInfo(HeroObject heroObject ,EquipPart equipPart ,ItemObject itemObject, int page)
    {
        int hpEquipAdd = 0;
        int mpEquipAdd = 0;
        short hpRenewEquipAdd = 0;
        short mpRenewEquipAdd = 0;
        short atkMinEquipAdd = 0;
        short atkMaxEquipAdd = 0;
        short mAtkMinEquipAdd = 0;
        short mAtkMaxEquipAdd = 0;
        short defEquipAdd = 0;
        short mDefEquipAdd = 0;
        short hitEquipAdd = 0;
        short dodEquipAdd = 0;
        short criREquipAdd = 0;
        short criDEquipAdd = 0;
        short spdEquipAdd = 0;
        short windDamEquipAdd = 0;
        short fireDamEquipAdd = 0;
        short waterDamEquipAdd = 0;
        short groundDamEquipAdd = 0;
        short lightDamEquipAdd = 0;
        short darkDamEquipAdd = 0;
        short windResEquipAdd = 0;
        short fireResEquipAdd = 0;
        short waterResEquipAdd = 0;
        short groundResEquipAdd = 0;
        short lightResEquipAdd = 0;
        short darkResEquipAdd = 0;
        short dizzyResEquipAdd = 0;
        short confusionResEquipAdd = 0;
        short poisonResEquipAdd = 0;
        short sleepResEquipAdd = 0;
        short expGetEquipAdd = 0;
        short goldGetEquipAdd = 0;
        short itemGetEquipAdd = 0;

        int hpSuiteAdd = 0;
        int mpSuiteAdd = 0;
        short hpRenewSuiteAdd = 0;
        short mpRenewSuiteAdd = 0;
        short atkMinSuiteAdd = 0;
        short atkMaxSuiteAdd = 0;
        short mAtkMinSuiteAdd = 0;
        short mAtkMaxSuiteAdd = 0;
        short defSuiteAdd = 0;
        short mDefSuiteAdd = 0;
        short hitSuiteAdd = 0;
        short dodSuiteAdd = 0;
        short criRSuiteAdd = 0;
        short criDSuiteAdd = 0;
        short spdSuiteAdd = 0;
        short windDamSuiteAdd = 0;
        short fireDamSuiteAdd = 0;
        short waterDamSuiteAdd = 0;
        short groundDamSuiteAdd = 0;
        short lightDamSuiteAdd = 0;
        short darkDamSuiteAdd = 0;
        short windResSuiteAdd = 0;
        short fireResSuiteAdd = 0;
        short waterResSuiteAdd = 0;
        short groundResSuiteAdd = 0;
        short lightResSuiteAdd = 0;
        short darkResSuiteAdd = 0;
        short dizzyResSuiteAdd = 0;
        short confusionResSuiteAdd = 0;
        short poisonResSuiteAdd = 0;
        short sleepResSuiteAdd = 0;
        short expGetSuiteAdd = 0;
        short goldGetSuiteAdd = 0;
        short itemGetSuiteAdd = 0;

        int hpEquipAddNew = 0;
        int mpEquipAddNew = 0;
        short hpRenewEquipAddNew = 0;
        short mpRenewEquipAddNew = 0;
        short atkMinEquipAddNew = 0;
        short atkMaxEquipAddNew = 0;
        short mAtkMinEquipAddNew = 0;
        short mAtkMaxEquipAddNew = 0;
        short defEquipAddNew = 0;
        short mDefEquipAddNew = 0;
        short hitEquipAddNew = 0;
        short dodEquipAddNew = 0;
        short criREquipAddNew = 0;
        short criDEquipAddNew = 0;
        short spdEquipAddNew = 0;
        short windDamEquipAddNew = 0;
        short fireDamEquipAddNew = 0;
        short waterDamEquipAddNew = 0;
        short groundDamEquipAddNew = 0;
        short lightDamEquipAddNew = 0;
        short darkDamEquipAddNew = 0;
        short windResEquipAddNew = 0;
        short fireResEquipAddNew = 0;
        short waterResEquipAddNew = 0;
        short groundResEquipAddNew = 0;
        short lightResEquipAddNew = 0;
        short darkResEquipAddNew = 0;
        short dizzyResEquipAddNew = 0;
        short confusionResEquipAddNew = 0;
        short poisonResEquipAddNew = 0;
        short sleepResEquipAddNew = 0;
        short expGetEquipAddNew = 0;
        short goldGetEquipAddNew = 0;
        short itemGetEquipAddNew = 0;

        int hpSuiteAddNew = 0;
        int mpSuiteAddNew = 0;
        short hpRenewSuiteAddNew = 0;
        short mpRenewSuiteAddNew = 0;
        short atkMinSuiteAddNew = 0;
        short atkMaxSuiteAddNew = 0;
        short mAtkMinSuiteAddNew = 0;
        short mAtkMaxSuiteAddNew = 0;
        short defSuiteAddNew = 0;
        short mDefSuiteAddNew = 0;
        short hitSuiteAddNew = 0;
        short dodSuiteAddNew = 0;
        short criRSuiteAddNew = 0;
        short criDSuiteAddNew = 0;
        short spdSuiteAddNew = 0;
        short windDamSuiteAddNew = 0;
        short fireDamSuiteAddNew = 0;
        short waterDamSuiteAddNew = 0;
        short groundDamSuiteAddNew = 0;
        short lightDamSuiteAddNew = 0;
        short darkDamSuiteAddNew = 0;
        short windResSuiteAddNew = 0;
        short fireResSuiteAddNew = 0;
        short waterResSuiteAddNew = 0;
        short groundResSuiteAddNew = 0;
        short lightResSuiteAddNew = 0;
        short darkResSuiteAddNew = 0;
        short dizzyResSuiteAddNew = 0;
        short confusionResSuiteAddNew = 0;
        short poisonResSuiteAddNew = 0;
        short sleepResSuiteAddNew = 0;
        short expGetSuiteAddNew = 0;
        short goldGetSuiteAddNew = 0;
        short itemGetSuiteAddNew = 0;

        int equipItemID = -1;

        //计算当前的suite加成
        List<short> suiteIDList = new List<short> { };
        List<byte> suiteNumList = new List<byte> { };
        for (int i = 0; i < heroObject.equipSuitePart.Count; i++)
        {
            if (heroObject.equipSuitePart[i] != -1)
            {
                if (!suiteIDList.Contains(heroObject.equipSuitePart[i]))
                {
                    suiteIDList.Add(heroObject.equipSuitePart[i]);
                    suiteNumList.Add(1);
                }
                else
                {
                    suiteNumList[suiteIDList.IndexOf(heroObject.equipSuitePart[i])]++;
                }
            }
        }

        for (int i = 0; i < suiteIDList.Count; i++)
        {
            for (int j = 0; j < DataManager.mItemSuiteDict[suiteIDList[i]].NeedPart.Count; j++)
            {
                if (suiteNumList[i] >= DataManager.mItemSuiteDict[suiteIDList[i]].NeedPart[j])
                {
                    switch (DataManager.mItemSuiteDict[suiteIDList[i]].AttributeType[j])
                    {
                        case Attribute.Hp: hpSuiteAdd += DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Mp: mpSuiteAdd += DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.HpRenew: hpRenewSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.MpRenew: mpRenewSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.AtkMin: atkMinSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.AtkMax: atkMaxSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.MAtkMin: mAtkMinSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.MAtkMax: mAtkMaxSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Def: defSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.MDef: mDefSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Hit: hitSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Dod: dodSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.CriR: criRSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.CriD: criDSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Spd: spdSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.WindDam: windDamSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.FireDam: fireDamSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.WaterDam: waterDamSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.GroundDam: groundDamSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.LightDam: lightDamSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.DarkDam: darkDamSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.WindRes: windResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.FireRes: fireResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.WaterRes: waterResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.GroundRes: groundResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.LightRes: lightResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.DarkRes: darkResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.DizzyRes: dizzyResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.ConfusionRes: confusionResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.PoisonRes: poisonResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.SleepRes: sleepResSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.ExpGet: expGetSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.GoldGet: goldGetSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.ItemGet: itemGetSuiteAdd += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                    }

                }
            }
            //Debug.Log("前  i="+i+" suiteIDList=" + suiteIDList[i]+ " suiteNumList=" + suiteNumList[i] );
        }

        //计算替换后suite加成
        List<short> temp_equipSuitePart = new List<short> { };
        for (int i = 0; i < heroObject.equipSuitePart.Count; i++)
        {
            temp_equipSuitePart.Add(heroObject.equipSuitePart[i]);
        }



        if (heroObject.equipWeapon != -1)
        {
            equipItemID = heroObject.equipWeapon;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Weapon)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[0] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipWeapon != -1)
        {
            equipItemID = heroObject.equipWeapon;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }

        if (heroObject.equipSubhand != -1)
        {
            equipItemID = heroObject.equipSubhand;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Subhand)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[1] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None && heroObject.equipSubhand != -1)
        {
            equipItemID = heroObject.equipSubhand;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }

        if (heroObject.equipHead != -1)
        {
            equipItemID = heroObject.equipHead;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Head)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[2] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipHead != -1)
        {
            equipItemID = heroObject.equipHead;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }

        if (heroObject.equipBody != -1)
        {
            equipItemID = heroObject.equipBody;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Body)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[3] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipBody != -1)
        {
            equipItemID = heroObject.equipBody;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }

        if (heroObject.equipHand != -1)
        {
            equipItemID = heroObject.equipHand;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Hand)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[4] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipHand != -1)
        {
            equipItemID = heroObject.equipHand;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }

        if (heroObject.equipBack != -1)
        {
            equipItemID = heroObject.equipBack;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Back)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[5] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipBack != -1)
        {
            equipItemID = heroObject.equipBack;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }


        if (heroObject.equipFoot != -1)
        {
            equipItemID = heroObject.equipFoot;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Foot)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[6] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipFoot != -1)
        {
            equipItemID = heroObject.equipFoot;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
     

        if (heroObject.equipNeck != -1)
        {
            equipItemID = heroObject.equipNeck;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Neck)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[7] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipNeck != -1)
        {
            equipItemID = heroObject.equipNeck;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
 

        if (heroObject.equipFinger1 != -1)
        {
            equipItemID = heroObject.equipFinger1;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Finger1)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[8] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipFinger1 != -1)
        {
            equipItemID = heroObject.equipFinger1;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }

        if (heroObject.equipFinger2 != -1)
        {
            equipItemID = heroObject.equipFinger2;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAdd += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAdd += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }
        if (equipPart == EquipPart.Finger2)
        {
            for (int i = 0; i < itemObject.attr.Count; i++)
            {
                switch (itemObject.attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += itemObject.attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)itemObject.attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)itemObject.attr[i].value; break;
                }
            }
            temp_equipSuitePart[9] = DataManager.mItemDict[itemObject.prototypeID].SuiteID;
        }
        else if (equipPart != EquipPart.None&& heroObject.equipFinger2 != -1)
        {
            equipItemID = heroObject.equipFinger2;
            for (int i = 0; i < gc.itemDic[equipItemID].attr.Count; i++)
            {
                switch (gc.itemDic[equipItemID].attr[i].attr)
                {
                    case Attribute.Hp: hpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Mp: mpEquipAddNew += gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.HpRenew: hpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MpRenew: mpRenewEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMin: atkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.AtkMax: atkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMin: mAtkMinEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MAtkMax: mAtkMaxEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Def: defEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.MDef: mDefEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Hit: hitEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Dod: dodEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriR: criREquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.CriD: criDEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.Spd: spdEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindDam: windDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireDam: fireDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterDam: waterDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundDam: groundDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightDam: lightDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkDam: darkDamEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WindRes: windResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.FireRes: fireResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.WaterRes: waterResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GroundRes: groundResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.LightRes: lightResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DarkRes: darkResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.DizzyRes: dizzyResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ConfusionRes: confusionResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.PoisonRes: poisonResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.SleepRes: sleepResEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ExpGet: expGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.GoldGet: goldGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                    case Attribute.ItemGet: itemGetEquipAddNew += (short)gc.itemDic[equipItemID].attr[i].value; break;
                }
            }
        }


        //计算suite新的，部分2
        suiteIDList.Clear();
        suiteNumList.Clear();
        for (int i = 0; i < temp_equipSuitePart.Count; i++)
        {
            if (temp_equipSuitePart[i] != -1)
            {
                if (!suiteIDList.Contains(temp_equipSuitePart[i]))
                {
                    suiteIDList.Add(temp_equipSuitePart[i]);
                    suiteNumList.Add(1);
                }
                else
                {
                    suiteNumList[suiteIDList.IndexOf(temp_equipSuitePart[i])]++;
                }
            }
        }

        for (int i = 0; i < suiteIDList.Count; i++)
        {
            for (int j = 0; j < DataManager.mItemSuiteDict[suiteIDList[i]].NeedPart.Count; j++)
            {
                if (suiteNumList[i] >= DataManager.mItemSuiteDict[suiteIDList[i]].NeedPart[j])
                {
                    switch (DataManager.mItemSuiteDict[suiteIDList[i]].AttributeType[j])
                    {
                        case Attribute.Hp: hpSuiteAddNew += DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Mp: mpSuiteAddNew += DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.HpRenew: hpRenewSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.MpRenew: mpRenewSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.AtkMin: atkMinSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.AtkMax: atkMaxSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.MAtkMin: mAtkMinSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.MAtkMax: mAtkMaxSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Def: defSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.MDef: mDefSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Hit: hitSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Dod: dodSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.CriR: criRSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.CriD: criDSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.Spd: spdSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.WindDam: windDamSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.FireDam: fireDamSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.WaterDam: waterDamSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.GroundDam: groundDamSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.LightDam: lightDamSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.DarkDam: darkDamSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.WindRes: windResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.FireRes: fireResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.WaterRes: waterResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.GroundRes: groundResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.LightRes: lightResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.DarkRes: darkResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.DizzyRes: dizzyResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.ConfusionRes: confusionResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.PoisonRes: poisonResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.SleepRes: sleepResSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.ExpGet: expGetSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.GoldGet: goldGetSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                        case Attribute.ItemGet: itemGetSuiteAddNew += (short)DataManager.mItemSuiteDict[suiteIDList[i]].Value[j]; break;
                    }

                }
            }

            //Debug.Log("后  i=" + i + " suiteIDList=" + suiteIDList[i] + " suiteNumList=" + suiteNumList[i]);
        }

        if (page == 1)
        {
            string spdStr = "";
            if (equipPart != EquipPart.None)//切换准备模式
            {
                if (equipPart == EquipPart.Weapon)//切换的是武器
                {
                    if (heroObject.equipWeapon == -1)
                    {
                        spdStr = OutputAttrChangeStrBySpd(heroObject.spd, spdEquipAddNew + spdSuiteAddNew);
                    }
                    else
                    {
                        spdStr = OutputAttrChangeStrBySpd(spdEquipAdd + spdSuiteAdd, spdEquipAddNew + spdSuiteAddNew);
                    }
                }
                else
                {
                    if (heroObject.equipWeapon == -1)
                    {
                        spdStr = OutputAttrChangeStrBySpd(heroObject.spd, heroObject.spd+spdEquipAddNew + spdSuiteAddNew);
                    }
                    else
                    {
                        spdStr = OutputAttrChangeStrBySpd(spdEquipAdd + spdSuiteAdd, spdEquipAddNew + spdSuiteAddNew);
                    }
                }
            }



            infoFight_des1Text.text = "体力上限 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.hp, hpEquipAdd + hpSuiteAdd) : OutputAttrChangeStr(heroObject.hp, hpEquipAdd + hpSuiteAdd, hpEquipAddNew + hpSuiteAddNew, "")) +
                "\n体力恢复 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.hpRenew, hpRenewEquipAdd + hpRenewSuiteAdd) : OutputAttrChangeStr(heroObject.hpRenew, hpRenewEquipAdd + hpRenewSuiteAdd, hpRenewEquipAddNew + hpRenewSuiteAddNew, "")) +
                "\n物攻 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.atkMin, atkMinEquipAdd + atkMinSuiteAdd) : OutputAttrChangeStr(heroObject.atkMin, atkMinEquipAdd + atkMinSuiteAdd, atkMinEquipAddNew + atkMinSuiteAddNew, "")) + " - " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.atkMax, atkMaxEquipAdd + atkMaxSuiteAdd) : OutputAttrChangeStr(heroObject.atkMax, atkMaxEquipAdd + atkMaxSuiteAdd, atkMaxEquipAddNew + atkMaxSuiteAddNew, "")) +
                "\n魔攻 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mAtkMin, mAtkMinEquipAdd + mAtkMinSuiteAdd) : OutputAttrChangeStr(heroObject.mAtkMin, mAtkMinEquipAdd + mAtkMinSuiteAdd, mAtkMinEquipAddNew + mAtkMinSuiteAddNew, "")) + " - " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mAtkMax, mAtkMaxEquipAdd + mAtkMaxSuiteAdd) : OutputAttrChangeStr(heroObject.mAtkMax, mAtkMaxEquipAdd + mAtkMaxSuiteAdd, mAtkMaxEquipAddNew + mAtkMaxSuiteAddNew, "")) +
                "\n物防 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.def, defEquipAdd + defSuiteAdd) : OutputAttrChangeStr(heroObject.def, defEquipAdd + defSuiteAdd, defEquipAddNew + defSuiteAddNew, "")) +
                "\n命中 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.hit, hitEquipAdd + hitSuiteAdd) : OutputAttrChangeStr(heroObject.hit, hitEquipAdd + hitSuiteAdd, hitEquipAddNew + hitSuiteAddNew, "")) +
                "\n闪避 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.dod, dodEquipAdd + dodSuiteAdd) : OutputAttrChangeStr(heroObject.dod, dodEquipAdd + dodSuiteAdd, dodEquipAddNew + dodSuiteAddNew, "")) +
                "\n速度 " + ((equipPart == EquipPart.None) ? (heroObject.equipWeapon == -1 ? heroObject.spd.ToString() : (spdEquipAdd + spdSuiteAdd).ToString()) : spdStr) +
                "\n风系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.windDam, windDamEquipAdd + windDamSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.windDam, windDamEquipAdd + windDamSuiteAdd, windDamEquipAddNew + windDamSuiteAddNew, "%")) +
                "\n火系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.fireDam, fireDamEquipAdd + fireDamSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.fireDam, fireDamEquipAdd + fireDamSuiteAdd, fireDamEquipAddNew + fireDamSuiteAddNew, "%")) +
                "\n水系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.waterDam, waterDamEquipAdd + waterDamSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.waterDam, waterDamEquipAdd + waterDamSuiteAdd, waterDamEquipAddNew + waterDamSuiteAddNew, "%")) +
                "\n地系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.groundDam, groundDamEquipAdd + groundDamSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.groundDam, groundDamEquipAdd + groundDamSuiteAdd, groundDamEquipAddNew + groundDamSuiteAddNew, "%")) +
                "\n光系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.lightDam, lightDamEquipAdd + lightDamSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.lightDam, lightDamEquipAdd + lightDamSuiteAdd, lightDamEquipAddNew + lightDamSuiteAddNew, "%")) +
                "\n暗系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.darkDam, darkDamEquipAdd + darkDamSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.darkDam, darkDamEquipAdd + darkDamSuiteAdd, darkDamEquipAddNew + darkDamSuiteAddNew, "%")) +
                "\n眩晕抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.dizzyRes, dizzyResEquipAdd + dizzyResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.dizzyRes, dizzyResEquipAdd + dizzyResSuiteAdd, dizzyResEquipAddNew + dizzyResSuiteAddNew, "%")) +
                "\n中毒抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.poisonRes, poisonResEquipAdd + poisonResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.poisonRes, poisonResEquipAdd + poisonResSuiteAdd, poisonResEquipAddNew + poisonResSuiteAddNew, "%")) +
                "\n\n经验值获得加成 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.expGet, expGetEquipAdd + expGetSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.expGet, expGetEquipAdd + expGetSuiteAdd, expGetEquipAddNew + expGetSuiteAddNew, "%")) +
                "\n金币获得加成 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.goldGet, goldGetEquipAdd + goldGetSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.goldGet, goldGetEquipAdd + goldGetSuiteAdd, goldGetEquipAddNew + goldGetSuiteAddNew, "%")) +
                "\n稀有物品掉落加成 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.itemGet, itemGetEquipAdd + itemGetSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.itemGet, itemGetEquipAdd + itemGetSuiteAdd, itemGetEquipAddNew + itemGetSuiteAddNew, "%"));
            infoFight_des2Text.text = "魔力上限 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mp,mpEquipAdd+ mpSuiteAdd) : OutputAttrChangeStr(heroObject.mp, mpEquipAdd + mpSuiteAdd, mpEquipAddNew+ mpSuiteAddNew, "")) +
                "\n魔力恢复 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mpRenew,mpRenewEquipAdd+ mpRenewSuiteAdd) : OutputAttrChangeStr(heroObject.mpRenew, mpRenewEquipAdd + mpRenewSuiteAdd, mpRenewEquipAddNew+ mpRenewSuiteAddNew, "")) +
                "\n " +
                "\n " +
                "\n魔防 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mDef,mDefEquipAdd+ mDefSuiteAdd) : OutputAttrChangeStr(heroObject.mDef, mDefEquipAdd + mDefSuiteAdd, mDefEquipAddNew+ mDefSuiteAddNew, "")) +
                "\n暴击 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.criR,criREquipAdd+ criRSuiteAdd) : OutputAttrChangeStr(heroObject.criR, criREquipAdd + criRSuiteAdd, criREquipAddNew+ criRSuiteAddNew, "")) +
                "\n爆伤 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.criD,criDEquipAdd+ criDSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.criD, criDEquipAdd + criDSuiteAdd, criDEquipAddNew+ criDSuiteAddNew, "%"))  +
                "\n " +
                "\n风系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.windRes,windResEquipAdd+ windResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.windRes, windResEquipAdd + windResSuiteAdd, windResEquipAddNew+ windResSuiteAddNew, "%"))  +
                "\n火系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.fireRes,fireResEquipAdd+ fireResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.fireRes, fireResEquipAdd + fireResSuiteAdd, fireResEquipAddNew+ fireResSuiteAddNew, "%")) +
                "\n水系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.waterRes,waterResEquipAdd+ waterResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.waterRes, waterResEquipAdd + waterResSuiteAdd, waterResEquipAddNew+ waterResSuiteAddNew, "%"))  +
                "\n地系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.groundRes,groundResEquipAdd+ groundResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.groundRes, groundResEquipAdd + groundResSuiteAdd, groundResEquipAddNew+ groundResSuiteAddNew, "%"))  +
                "\n光系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.lightRes,lightResEquipAdd+ lightResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.lightRes, lightResEquipAdd + lightResSuiteAdd, lightResEquipAddNew+ lightResSuiteAddNew, "%")) +
                "\n暗系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.darkRes,darkResEquipAdd+ darkResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.darkRes, darkResEquipAdd + darkResSuiteAdd, darkResEquipAddNew+ darkResSuiteAddNew, "%")) +
                "\n混乱抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.confusionRes, confusionResEquipAdd + confusionResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.confusionRes, confusionResEquipAdd + confusionResSuiteAdd, confusionResEquipAddNew + confusionResSuiteAddNew, "%")) +
                "\n睡眠抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.sleepRes, sleepResEquipAdd + sleepResSuiteAdd) + "%" : OutputAttrChangeStr(heroObject.sleepRes, sleepResEquipAdd + sleepResSuiteAdd, sleepResEquipAddNew + sleepResSuiteAddNew, "%"));

            infoFight_page2Btn.interactable = true;
            infoFight_page2Btn.onClick.RemoveAllListeners();
            infoFight_page2Btn.onClick.AddListener(delegate () { UpdateFightInfo(heroObject,equipPart,itemObject, 2); });
            infoFight_page1Btn.interactable = false;
        }
        else if((page == 2))
        {
            infoFight_des1Text.text = "";
            infoFight_des2Text.text = "";

            infoFight_page1Btn.interactable = true;
            infoFight_page1Btn.onClick.RemoveAllListeners();
            infoFight_page1Btn.onClick.AddListener(delegate () { UpdateFightInfo(heroObject, equipPart, itemObject, 1); });
            infoFight_page2Btn.interactable = false;

        }

    }
    //能力页面-战斗属性栏目-更新（辅助方法：输出当前数值）
    string OutputAttrStr(float basic, int equipAdd)
    {
        return ((int)basic + equipAdd) >= 0 ? ((int)basic + equipAdd).ToString():"0";
    }
    //能力页面-战斗属性栏目-更新（辅助方法：输出除SPD外变化数值）
    string OutputAttrChangeStr(float basic,int addNow ,int addNew ,string suffixes)
    {
        int nowValue = ((int)basic + addNow) >= 0 ? (int)basic + addNow : 0;
        int newValue = ((int)basic + addNew) >= 0 ? (int)basic + addNew : 0;
        int change = newValue - nowValue;
        string changeStr = " ";
        if (change > 0)
        {
            changeStr = " <color=#76EE00>+" + change + suffixes+"</color>";
        }
        else if (change < 0)
        {
            changeStr = " <color=#FF4A4A>" + change + suffixes+"</color>";
        }
        
        return newValue+ suffixes + changeStr;
    }
    //能力页面-战斗属性栏目-更新（辅助方法：输出SPD变化数值）
    string OutputAttrChangeStrBySpd(int nowValue, int newValue)
    {
        int change = newValue - nowValue;
        string changeStr = " ";
        if (change > 0)
        {
            changeStr = " <color=#76EE00>+" + change  + "</color>";
        }
        else if (change < 0)
        {
            changeStr = " <color=#FF4A4A>" + change  + "</color>";
        }
        return newValue  + changeStr;
    }

    //能力页面-工作属性栏目-更新
    public void UpdateWorkInfo(HeroObject heroObject)
    {
        infoWork_desText.text = "<color=#FFBD58>种植</color> " + gc.OutputWorkValueToRank(heroObject.workPlanting) +
            "  <color=#FFBD58>饲养</color> " + gc.OutputWorkValueToRank(heroObject.workFeeding) +
            "  <color=#FFBD58>钓鱼</color> " + gc.OutputWorkValueToRank(heroObject.workFishing) +
            "\n<color=#FFBD58>打猎</color> " + gc.OutputWorkValueToRank(heroObject.workHunting) +
            "  <color=#FFBD58>伐木</color> " + gc.OutputWorkValueToRank(heroObject.workFelling) +
            "  <color=#FFBD58>挖矿</color> " + gc.OutputWorkValueToRank(heroObject.workQuarrying) +
            "\n<color=#FFBD58>采石</color> " + gc.OutputWorkValueToRank(heroObject.workMining) +
            "  <color=#FFBD58>建筑</color> " + gc.OutputWorkValueToRank(heroObject.workBuild) +
            "  <color=#62D5EE>管理</color> " + gc.OutputWorkValueToRank(heroObject.workSundry)+
            "\n<color=#F0A0FF>武器锻造</color> " + gc.OutputWorkValueToRank(heroObject.workMakeWeapon) +
            "      <color=#F0A0FF>防具制作</color> " + gc.OutputWorkValueToRank(heroObject.workMakeArmor) +
            "\n<color=#F0A0FF>饰品制作</color> " + gc.OutputWorkValueToRank(heroObject.workMakeJewelry) +
            "      <color=#F0A0FF>卷轴研究</color> " + gc.OutputWorkValueToRank(heroObject.workMakeScroll) ;
    }

    //能力页面-装备栏目-更新
    public void UpdateEquipAll( HeroObject heroObject)
    {
        UpdateEquip(heroObject,EquipPart.Weapon);
        UpdateEquip(heroObject, EquipPart.Subhand);
        UpdateEquip(heroObject, EquipPart.Head);
        UpdateEquip(heroObject, EquipPart.Body);
        UpdateEquip(heroObject, EquipPart.Hand);
        UpdateEquip(heroObject, EquipPart.Back);
        UpdateEquip(heroObject, EquipPart.Foot);
        UpdateEquip(heroObject, EquipPart.Neck);
        UpdateEquip(heroObject, EquipPart.Finger1);
        UpdateEquip(heroObject, EquipPart.Finger2);
    }
    //能力页面-装备栏目-单个部位-更新
    public void UpdateEquip(HeroObject heroObject, EquipPart equipPart)
    {
        switch (equipPart)
        {
            case EquipPart.Weapon:
                if (heroObject.equipWeapon != -1)
                {
                    equip_weaponText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipWeapon].rank) + ">"+ gc.itemDic[heroObject.equipWeapon].name+ (gc.itemDic[heroObject.equipWeapon].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipWeapon].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipWeapon].rank)+ "</color>";
                    equip_weaponImage.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipWeapon].pic, typeof(Sprite)) as Sprite;
                    equip_weaponUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_weaponUnSetBtn.onClick.RemoveAllListeners();
                    equip_weaponUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Weapon); });
                }
                else
                {
                    equip_weaponText.text = "主手<color=#FF9786>（未装备）</color>";
                    equip_weaponImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_weaponUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_weaponBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipWeapon;
                equip_weaponBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Subhand:
                if (heroObject.equipSubhand != -1)
                {
                    equip_subhandText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipSubhand].rank) + ">" + gc.itemDic[heroObject.equipSubhand].name + (gc.itemDic[heroObject.equipSubhand].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipSubhand].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipSubhand].rank) + "</color>";
                    equip_subhandImage.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipSubhand].pic, typeof(Sprite)) as Sprite;
                    equip_subhandUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_subhandUnSetBtn.onClick.RemoveAllListeners();
                    equip_subhandUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Subhand); });
                }
                else
                {
                    equip_subhandText.text = "副手<color=#FF9786>（未装备）</color>";
                    equip_subhandImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_subhandUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_subhandBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipSubhand;
                equip_subhandBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Head:
                if (heroObject.equipHead != -1)
                {
                    equip_headText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipHead].rank) + ">" + gc.itemDic[heroObject.equipHead].name + (gc.itemDic[heroObject.equipHead].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipHead].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipHead].rank) + "</color>";
                    equip_headImage.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipHead].pic, typeof(Sprite)) as Sprite;
                    equip_headUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_headUnSetBtn.onClick.RemoveAllListeners();
                    equip_headUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Head); });
                }
                else
                {
                    equip_headText.text = "头部防具<color=#FF9786>（未装备）</color>";
                    equip_headImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_headUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_headBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipHead;
                equip_headBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Body:
                if (heroObject.equipBody != -1)
                {
                    equip_bodyText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipBody].rank) + ">" + gc.itemDic[heroObject.equipBody].name + (gc.itemDic[heroObject.equipBody].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipBody].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipBody].rank) + "</color>";
                    equip_bodyImage.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipBody].pic, typeof(Sprite)) as Sprite;
                    equip_bodyUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_bodyUnSetBtn.onClick.RemoveAllListeners();
                    equip_bodyUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Body); });
                }
                else
                {
                    equip_bodyText.text = "身体防具<color=#FF9786>（未装备）</color>";
                    equip_bodyImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_bodyUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_bodyBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipBody;
                equip_bodyBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Hand:
                if (heroObject.equipHand != -1)
                {
                    equip_handText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipHand].rank) + ">" + gc.itemDic[heroObject.equipHand].name + (gc.itemDic[heroObject.equipHand].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipHand].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipHand].rank) + "</color>";
                    equip_handImage.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipHand].pic, typeof(Sprite)) as Sprite;
                    equip_handUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_handUnSetBtn.onClick.RemoveAllListeners();
                    equip_handUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Hand); });
                }
                else
                {
                    equip_handText.text = "手部防具<color=#FF9786>（未装备）</color>";
                    equip_handImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_handUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_handBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipHand;
                equip_handBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Back:
                if (heroObject.equipBack != -1)
                {
                    equip_backText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipBack].rank) + ">" + gc.itemDic[heroObject.equipBack].name + (gc.itemDic[heroObject.equipBack].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipBack].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipBack].rank) + "</color>";
                    equip_backImage.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipBack].pic, typeof(Sprite)) as Sprite;
                    equip_backUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_backUnSetBtn.onClick.RemoveAllListeners();
                    equip_backUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Back); });
                }
                else
                {
                    equip_backText.text = "背部防具<color=#FF9786>（未装备）</color>";
                    equip_backImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_backUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_backBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipBack;
                equip_backBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Foot:
                if (heroObject.equipFoot != -1)
                {
                    equip_footText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipFoot].rank) + ">" + gc.itemDic[heroObject.equipFoot].name + (gc.itemDic[heroObject.equipFoot].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipFoot].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFoot].rank) + "</color>";
                    equip_footImage.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFoot].pic, typeof(Sprite)) as Sprite;
                    equip_footUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_footUnSetBtn.onClick.RemoveAllListeners();
                    equip_footUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Foot); });
                }
                else
                {
                    equip_footText.text = "腿部防具<color=#FF9786>（未装备）</color>";
                    equip_footImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_footUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_footBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFoot;
                equip_footBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Neck:
                if (heroObject.equipNeck != -1)
                {
                    equip_neckText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipNeck].rank) + ">" + gc.itemDic[heroObject.equipNeck].name + (gc.itemDic[heroObject.equipNeck].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipNeck].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipNeck].rank) + "</color>";
                    equip_neckImage.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipNeck].pic, typeof(Sprite)) as Sprite;
                    equip_neckUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_neckUnSetBtn.onClick.RemoveAllListeners();
                    equip_neckUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Neck); });
                }
                else
                {
                    equip_neckText.text = "项链<color=#FF9786>（未装备）</color>";
                    equip_neckImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_neckUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_neckBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipNeck;
                equip_neckBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Finger1:
                if (heroObject.equipFinger1 != -1)
                {
                    equip_finger1Text.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipFinger1].rank) + ">" + gc.itemDic[heroObject.equipFinger1].name + (gc.itemDic[heroObject.equipFinger1].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipFinger1].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFinger1].rank) + "</color>";
                    equip_finger1Image.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFinger1].pic, typeof(Sprite)) as Sprite;
                    equip_finger1UnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_finger1UnSetBtn.onClick.RemoveAllListeners();
                    equip_finger1UnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Finger1); });
                }
                else
                {
                    equip_finger1Text.text = "戒指<color=#FF9786>（未装备）</color>";
                    equip_finger1Image.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_finger1UnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_finger1Btn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFinger1;
                equip_finger1Btn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Finger2:
                if (heroObject.equipFinger2 != -1)
                {
                    equip_finger2Text.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[heroObject.equipFinger2].rank) + ">" + gc.itemDic[heroObject.equipFinger2].name + (gc.itemDic[heroObject.equipFinger2].level == 0 ? "" : (" +" + gc.itemDic[heroObject.equipFinger2].level)) + "</color>\n<color=#FFD700>" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFinger2].rank) + "</color>";
                    equip_finger2Image.sprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFinger2].pic, typeof(Sprite)) as Sprite;
                    equip_finger2UnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_finger2UnSetBtn.onClick.RemoveAllListeners();
                    equip_finger2UnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Finger2); });
                }
                else
                {
                    equip_finger2Text.text = "戒指<color=#FF9786>（未装备）</color>";
                    equip_finger2Image.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_finger2UnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_finger2Btn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFinger2;
                equip_finger2Btn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
        }
    }
    //能力页面-技能栏目-更新
    public void UpdateSkillAll(HeroObject heroObject)
    {
        for (byte i = 0; i < heroObject.skill.Count; i++)
        {
            UpdateSkill(heroObject, i);
        }
        UpdateSkillGroupInfoAll(heroObject);
    }
    //能力页面-技能栏目-单个技能-更新
    public void UpdateSkill(HeroObject heroObject, byte skillIndex)
    {
        skill_Btn[skillIndex].onClick.RemoveAllListeners();
        skill_Btn[skillIndex].onClick.AddListener(delegate () { SkillListAndInfoPanel.Instance.OnShow(-1,null,heroObject.id,skillIndex,(int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x+ gameObject.GetComponent<RectTransform>().sizeDelta.x+GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y); });

        if (heroObject.skill[skillIndex] >= 0)
        {
            SkillObject so = gc.skillDic[heroObject.skill[skillIndex]];
            SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];
            skill_bgImage[skillIndex].color = new Color(skill_bgImage[skillIndex].color.r, skill_bgImage[skillIndex].color.g, skill_bgImage[skillIndex].color.b, 0.7f);
            skill_Image[skillIndex].sprite = Resources.Load("Image/SkillPic/" + sp.Pic, typeof(Sprite)) as Sprite;
            skill_Btn[skillIndex].interactable = true;

            string str = "";
            for (int i = 0; i < sp.Element.Count; i++)
            {
                if (i != 0)
                {
                    str += "/";
                }
                switch (sp.Element[i])
                {
                    case 0: str += "-"; break;
                    case 1: str += "<color=#26F39A>风</color>"; break;
                    case 2: str += "<color=#E74624>火</color>"; break;
                    case 3: str += "<color=#24CDE7>水</color>"; break;
                    case 4: str += "<color=#C08342>地</color>"; break;
                    case 5: str += "<color=#E0DE60>光</color>"; break;
                    case 6: str += "<color=#DA7CFF>暗</color>"; break;
                }
            }

            int probability = sp.Probability;
            if (so.rateModify != 0)
            {
                probability += so.rateModify;
                if (probability < 0)
                {
                    probability = 0;
                }
                else if (probability > 0)
                {
                    probability = 100;
                }
            }
            // skill_Text[skillIndex].text = str + "\n" + probability + "%\n<color=#38B9FB>MP " + gc.GetSkillMpCost(so.id) + "</color>";
            skill_Text[skillIndex].text = str;
            skill_textRt[skillIndex].sizeDelta = new Vector2(skill_Text[skillIndex].preferredWidth + 5f, 16f);
        }
        else if (heroObject.skill[skillIndex] == -1)//已解锁 未配置
        {
            skill_bgImage[skillIndex].color = new Color(skill_bgImage[skillIndex].color.r, skill_bgImage[skillIndex].color.g, skill_bgImage[skillIndex].color.b, 0.7f);
            skill_Image[skillIndex].sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            skill_Btn[skillIndex].interactable = true;
            skill_Text[skillIndex].text = "";
            skill_textRt[skillIndex].sizeDelta = Vector2.zero;
        }
        else if (heroObject.skill[skillIndex] == -2)//可解锁 未解锁
        {
            skill_bgImage[skillIndex].color = new Color(skill_bgImage[skillIndex].color.r, skill_bgImage[skillIndex].color.g, skill_bgImage[skillIndex].color.b, 0.4f);
            skill_Image[skillIndex].sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            skill_Btn[skillIndex].interactable = false;
            skill_Text[skillIndex].text = "";
            skill_textRt[skillIndex].sizeDelta = Vector2.zero;
        }
        else if (heroObject.skill[skillIndex] == -3)//不可解锁
        {
            skill_bgImage[skillIndex].color = Color.clear;
            skill_Image[skillIndex].sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            skill_Btn[skillIndex].interactable = false;
            skill_Text[skillIndex].text = "";
            skill_textRt[skillIndex].sizeDelta = Vector2.zero;
        }
    }

    public void UpdateSkillGroupInfoAll(HeroObject heroObject)
    {
        for (byte i = 0; i < skill_infoText.Count; i++)
        {
            UpdateSkillGroupInfo(heroObject, i);
        }
    }

    public void UpdateSkillGroupInfo(HeroObject heroObject, byte groupIndex)
    {
        //规则
        //星座位置符合，最终概率=（1f+符合个数*5%）
        //元素相克相同绝对值 每点影响最终魔耗5% 伤害5%
        //招式原型相同 2个起步 伤害提升10% 20%

        int probabilityTotal = 0;
        int mpTotal = 0;
        int hasCount = 0;
        int repelCount = 0;
        int sameCount = 0;
        int skillIndex;
        int matchCount = 0;
        int samePidCount = 0;
        List<int> elementList = new List<int> { };
        List<int> spList = new List<int> { };


        for (int j = 0; j < 3; j++)
        {
            skillIndex = groupIndex * 3 + j;
            if (heroObject.skill[skillIndex] >= 0)
            {
                hasCount++;
                SkillObject so = gc.skillDic[heroObject.skill[skillIndex]];
                SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];

                spList.Add(so.prototypeID);

                int probability = sp.Probability;
                if (so.rateModify != 0)
                {
                    probability += so.rateModify;
                    if (probability < 0)
                    {
                        probability = 0;
                    }
                    else if (probability > 0)
                    {
                        probability = 100;
                    }
                }
                probabilityTotal += probability;
                mpTotal += gc.GetSkillMpCost(so.id);

                for (int k = 0; k < sp.Element.Count; k++)
                {
                    elementList.Add(sp.Element[k]);

                }

                switch (groupIndex * 3 + j)
                {
                    case 0:
                    case 4:
                    case 8:
                        if (sp.Element.Contains(2)) { matchCount++; }
                        break;
                    case 1:
                    case 5:
                    case 9:
                        if (sp.Element.Contains(4)) { matchCount++; }
                        break;
                    case 2:
                    case 6:
                    case 10:
                        if (sp.Element.Contains(1)) { matchCount++; }
                        break;
                    case 3:
                    case 7:
                    case 11:
                        if (sp.Element.Contains(3)) { matchCount++; }
                        break;
                }
            }
        }

        for (int m = 0; m < elementList.Count; m++)
        {
            for (int n = m + 1; n < elementList.Count; n++)
            {
                if (elementList[m] == elementList[n])
                {
                    sameCount++;
                }
                else if (isElementRepel(elementList[m], elementList[n]))
                {
                    repelCount++;
                }
            }
        }

        if (spList.Count == 3)
        {
            if (spList[0] == spList[1] && spList[0] == spList[2])
            {
                samePidCount = 4;
            }
            else if (spList[0] == spList[1] || spList[0] == spList[2] || spList[1] == spList[2])
            {
                samePidCount = 2;
            }
        }
        else if (spList.Count == 2)
        {
            if (spList[0] == spList[1] )
            {
                samePidCount = 2;
            }
        }


        string str = "";
        string strMpUp = "";

        if (hasCount > 0)
        {

            if ((sameCount  - repelCount) > 0)
            {
                strMpUp = "↓" ;
            }
            else if ((sameCount  - repelCount) < 0)
            {
                strMpUp = "↑";
            }

            str = (int)(probabilityTotal*(1f+matchCount*0.05f) / hasCount) + "%"+(matchCount>0?"↑":"") +"  <color=#38B9FB>MP" + (int)(mpTotal *(1f+ -0.05f* (sameCount - repelCount))) + strMpUp + "</color>";
            if ((sameCount+ samePidCount - repelCount) > 0)
            {
                str += "\n威力" + gc.OutputSignStr("+", sameCount - repelCount);
            }
            else if ((sameCount + samePidCount - repelCount) < 0)
            {
                str += "\n威力" + gc.OutputSignStr("-",  repelCount- sameCount- samePidCount);
            }
            skill_infoText[groupIndex].text = str;

        }
        else
        {
            skill_infoText[groupIndex].text = "<color=#FFFFFF>[普通攻击]</color>";
        }

    }

    bool isElementRepel(int a, int b)
    {
        switch (a)
        {
            case 0: return false;
            case 1: return b == 2 ? true : false;
            case 2: return b == 3 ? true : false;
            case 3: return b == 4 ? true : false;
            case 4: return b == 1 ? true : false;
            case 5: return b == 6 ? true : false;
            case 6: return b == 5 ? true : false;
            default: return false;
        }
    }

    //技能等级页面-显示
    void ShowSkillPage(HeroObject heroObject)
    {
        HideDataAndHistoryPage();
        pageSkillRt.localScale = Vector2.one;
        IsShowPageSkill = true;

        List<HeroSkill> heroSkills = new List<HeroSkill>();
        foreach (KeyValuePair<short, HeroSkill> kvp in heroObject.skillInfo)
        {
            heroSkills.Add(kvp.Value);
        }

        GameObject go;
        for (int i = 0; i < heroSkills.Count; i++)
        {
            if (i < skillGoPool.Count)
            {
                go = skillGoPool[i];
                skillGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_SkillInHero")) as GameObject;
                go.transform.SetParent(pageSkill_listGo.transform);
                skillGoPool.Add(go);
            }

            int row = i == 0 ? 0 : (i % 4);
            int col = i == 0 ? 0 : (i / 4);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 120f, -4 + col * -28f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/SkillPic/" +DataManager.mSkillDict[ heroSkills[i].skillID].Pic);
            go.transform.GetChild(2).GetComponent<Text>().text = DataManager.mSkillDict[heroSkills[i].skillID].Name + "  Lv.<color=" + (heroSkills[i].level == 10 ?"yellow>": "white>") + heroSkills[i].level+"</color>";
            go.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(heroSkills[i].level!=10?(((float)heroSkills[i].exp/ (heroSkills[i].level*200)) * 92f):92f, 8f);
        }
        for (int i = heroSkills.Count ; i < skillGoPool.Count; i++)
        {
            skillGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        pageSkill_listGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(484f, Mathf.Max(400f, 4 + (heroSkills.Count / 4) * 28f));
    }
    //技能等级页面-隐藏
    void HideSkillPage()
    {
        pageSkillRt.localScale = Vector2.zero;
        IsShowPageSkill = false;
    }

    //数据与日志页面-显示
    void ShowDataAndHistoryPage(HeroObject heroObject)
    {
        HideSkillPage();
        pageDataAndHistoryRt.localScale = Vector2.one;
        IsShowPageDataAndHistory = true;

        int UseTotal = heroObject.countUseWind + heroObject.countUseFire + heroObject.countUseWater + heroObject.countUseGround + heroObject.countUseLight + heroObject.countUseDark+ heroObject.countUseNone;
        pageDataAndHistory_dataText.text = "生产/制作\n 制作武器 " + heroObject.countMakeWeapon +
            " 件\n 制作防具 " + heroObject.countMakeArmor +
            " 件\n 制作饰物 " + heroObject.countMakeJewelry +
            " 件\n 制作卷轴 " + heroObject.countMakeScroll +
            " 件\n\n冒险 " + heroObject.countAdventure +
            " 次\n 完成 " + heroObject.countAdventureDone +
            " 次\n击杀 " + heroObject.countKill +
            " \n倒下 " + heroObject.countDeath +
            "\n\n无属性技能使用 " + heroObject.countUseNone + " (" + System.Math.Round((float)heroObject.countUseNone / UseTotal*100, 2) + "%)" +
            "\n风系技能使用 " + heroObject.countUseWind + " (" + System.Math.Round((float)heroObject.countUseWind / UseTotal * 100, 2) + "%)" +
            "\n火系技能使用 " + heroObject.countUseFire + " (" + System.Math.Round((float)heroObject.countUseFire / UseTotal * 100, 2) + "%)" +
            "\n水系技能使用 " + heroObject.countUseWater + " (" + System.Math.Round((float)heroObject.countUseWater / UseTotal * 100, 2) + "%)" +
            "\n地系技能使用 " + heroObject.countUseGround + " (" + System.Math.Round((float)heroObject.countUseGround / UseTotal * 100, 2) + "%)" +
            "\n光系技能使用 " + heroObject.countUseLight + " (" + System.Math.Round((float)heroObject.countUseLight / UseTotal * 100, 2) + "%)" +
            "\n暗系技能使用 " + heroObject.countUseDark + " (" + System.Math.Round((float)heroObject.countUseDark / UseTotal * 100, 2) + "%)";

        string log = "";
        for (int i = heroObject.log.Count - 1; i >= 0; i--)
        {
            log += heroObject.log[i] + "\n";
        }
        pageDataAndHistory_historyText.text = log;
    }
    //数据与日志页面-隐藏
    void HideDataAndHistoryPage()
    {
        pageDataAndHistoryRt.localScale = Vector2.zero;
        IsShowPageDataAndHistory = false;
    }

    //名字修改块-显示
    public void ShowChangeName()
    {
        title_changeNameRt.localScale = Vector2.one;
    }
    //名字修改块-关闭
    public void HideChangeName()
    {
        title_changeNameRt.localScale = Vector2.zero;
    }

    void UpdateIcon(int heroID)
    {
        int index = 0;

        if (gc.heroDic[heroID].halo != -1)
        {
            iconImage[index].GetComponent<RectTransform>().localScale = Vector2.one;
            iconImage[index].sprite = Resources.Load<Sprite>("Image/Other/"+DataManager.mHaloDict[gc.heroDic[heroID].halo].Pic);
            iconImage[index].GetComponent<InteractiveLabel>().iconType = "halo";
            iconImage[index].GetComponent<InteractiveLabel>().heroID = heroID;
            iconImage[index].GetComponent<InteractiveLabel>().index = gc.heroDic[heroID].halo;
            index++;
        }
        for (int i = 0; i < gc.heroDic[heroID].characteristic.Count; i++)
        {
            iconImage[index].GetComponent<RectTransform>().localScale = Vector2.one;
            iconImage[index].sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mCharacteristicDict[gc.heroDic[heroID].characteristic[i]].Pic);
            iconImage[index].GetComponent<InteractiveLabel>().iconType = "characteristic";
            iconImage[index].GetComponent<InteractiveLabel>().heroID = heroID;
            iconImage[index].GetComponent<InteractiveLabel>().index = gc.heroDic[heroID].characteristic[i];
            index++;
        }
        if (gc.heroDic[heroID].job != -1)
        {
            iconImage[index].GetComponent<RectTransform>().localScale = Vector2.one;
            iconImage[index].sprite = Resources.Load<Sprite>("Image/Other/icon239");
            iconImage[index].GetComponent<InteractiveLabel>().iconType = "job";
            index++;
        }

        if (gc.heroDic[heroID].forceLeader)
        {
            iconImage[index].GetComponent<RectTransform>().localScale = Vector2.one;
            iconImage[index].sprite = Resources.Load<Sprite>("Image/Other/icon401");
            iconImage[index].GetComponent<InteractiveLabel>().iconType = "leader";
            iconImage[index].GetComponent<InteractiveLabel>().heroID = heroID;
            index++;
        }

        for (int i = index; i < iconImage.Count; i++)
        {
            iconImage[i].GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    //图标详情-显示，更新信息
    public void ShowIconInfo(string type,int heroID, int id, float x)
    {
        icon_infoRt.anchoredPosition = new Vector2(x, -30);
        string str = "";
        switch (type)            
        {
            case "halo":
                icon_infoNameText.text = DataManager.mHaloDict[id].Name;
                str = "战斗天赋\n\n触发条件";
                string strNeedElement = "";
                for (int i = 0; i < DataManager.mHaloDict[id].NeedElementType.Count; i++)
                {
                    switch (DataManager.mHaloDict[id].NeedElementType[i])
                    {
                        case Element.Wind:icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Other/icon912"); break;
                        case Element.Fire: icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Other/icon913"); break;
                        case Element.Water: icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Other/icon914"); break;
                        case Element.Ground: icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Other/icon919"); break;
                        case Element.Light: icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Other/icon917"); break;
                        case Element.Dark: icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Other/icon916"); break;
                    }
                    strNeedElement += "   " + DataManager.mHaloDict[id].NeedElementPoint[i];
                }
                for (int i = DataManager.mHaloDict[id].NeedElementType.Count; i < icon_infoNeedElementImage.Count; i++)
                {
                    icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Empty");
                }

                if (strNeedElement != "")
                {
                    str += "\n " + strNeedElement;
                }

                if (DataManager.mHaloDict[id].NeedHpNow != -1)
                {
                    str += "\n 生命值低于" + DataManager.mHaloDict[id].NeedHpNow+"%";
                }

                str += "\n";

                if (DataManager.mHaloDict[id].DamageUp != 0) { str += "\n 伤害提升" + DataManager.mHaloDict[id].DamageUp + "%"; }
                if (DataManager.mHaloDict[id].Offset != 0) { str += "\n 伤害抵消" + DataManager.mHaloDict[id].Offset + "%"; }
                if (DataManager.mHaloDict[id].EDamageUp != 0) { str += "\n 元素伤害提升" + DataManager.mHaloDict[id].EDamageUp + "%"; }
                if (DataManager.mHaloDict[id].EOffset != 0) { str += "\n 元素伤害抵消" + DataManager.mHaloDict[id].EOffset + "%"; }
                if (DataManager.mHaloDict[id].HitUp != 0) { str += "\n 命中值提升" + DataManager.mHaloDict[id].HitUp + "%"; }
                if (DataManager.mHaloDict[id].DodUp != 0) { str += "\n 闪避值提升" + DataManager.mHaloDict[id].DodUp + "%"; }
                if (DataManager.mHaloDict[id].CriRUp != 0) { str += "\n 暴击几率提升" + DataManager.mHaloDict[id].CriRUp + "%"; }
                if (DataManager.mHaloDict[id].CriDUp != 0) { str += "\n 暴击伤害提升" + DataManager.mHaloDict[id].CriDUp + "%"; }
                if (DataManager.mHaloDict[id].SpdUp != 0) { str += "\n 速度提升" + DataManager.mHaloDict[id].SpdUp + "%"; }
                if (DataManager.mHaloDict[id].SuckBlood != 0) { str += "\n 对敌方造成伤害的" + DataManager.mHaloDict[id].SuckBlood + "%转化为自身体力"; }
                if (DataManager.mHaloDict[id].DamageReflection != 0) { str += "\n 受到攻击伤害时对攻击者造成" + DataManager.mHaloDict[id].DamageReflection + "%的反击伤害"; }
                if (DataManager.mHaloDict[id].Detonate != 0) { str += "\n 倒下时对敌方全体造成自身最大体力" + DataManager.mHaloDict[id].Detonate + "%的伤害"; }

                break;
            case "characteristic":
                icon_infoNameText.text = DataManager.mCharacteristicDict[id].Name;
                switch (DataManager.mCharacteristicDict[id].Type)
                {
                    case "Work":str = "\n工作天赋"; break;
                    case "Fight": str = "\n战斗天赋"; break;
                }
                str += "\n\n"+ DataManager.mCharacteristicDict[id].Des;
                for (int i = 0; i < icon_infoNeedElementImage.Count; i++)
                {
                    icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Empty");
                }
                break;
            case "job":
                icon_infoNameText.text = "职衔";
                for (int i = 0; i < icon_infoNeedElementImage.Count; i++)
                {
                    icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Empty");
                }
                break;
            case "leader":
                icon_infoNameText.text = "领袖";
                str = gc.forceDic[gc.heroDic[heroID].force].name + "的统治者";
                for (int i = 0; i < icon_infoNeedElementImage.Count; i++)
                {
                    icon_infoNeedElementImage[i].sprite = Resources.Load<Sprite>("Image/Empty");
                }
                break;

        }
        icon_infoDesText.text = str;
        icon_infoRt.sizeDelta = new Vector2(168f, icon_infoDesText.preferredHeight + 28f);
    }

    //图标详情-关闭
    public void HideIconInfo()
    {
        icon_infoRt.anchoredPosition = new Vector2(0, 5000);
    }
}
