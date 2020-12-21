﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanel : BasePanel
{
    public static HeroPanel Instance;

    GameControl gc;

    public RectTransform connRt;

    public Text nameText;
    public Image picImage;
    public Text infoText;

    public Button infoFight_page1Btn;
    public Button infoFight_page2Btn;
    public Text infoFight_des1Text;
    public Text infoFight_des2Text;
    public Text infoWork_desText;

    public Text equip_titleText;
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

    public Text skill_titleText;
    public List<Button> skill_Btn;
    public List<Image> skill_Image;
    public List<Text> skill_Text;

    public Button totalSet_equipBtn;
    public Button totalSet_skillBtn;

    public Button closeBtn;

    public int nowSelectedHeroID = -1;
    public bool nowEquipState = false;//false为查看模式 true为调整模式
    public bool nowSkillState = false;//false为查看模式 true为调整模式

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {

        totalSet_equipBtn.onClick.AddListener(delegate () { nowEquipState = !nowEquipState; UpdateButtonStatus(); });
        totalSet_skillBtn.onClick.AddListener(delegate () { nowSkillState = !nowSkillState; UpdateButtonStatus(); });
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public void OnShow( HeroObject heroObject, int x,int y,int connY)
    {

        UpdateAllInfo( heroObject, connY);
        nowEquipState = false;
        nowSkillState = false;
        UpdateButtonStatus();
        SetAnchoredPosition(x, y);

        isShow = true;
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }

    public void UpdateButtonStatus()
    {
        //Debug.Log("nowEquipState=" + nowEquipState);
        if (!nowEquipState)
        {
            equip_titleText.text = "装备[查看模式]";
            equip_weaponBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_weaponBtn.onClick.RemoveAllListeners();
            equip_weaponBtn.onClick.AddListener(delegate ()
            {
                int index= equip_weaponBtn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {  
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing),(int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_subhandBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_subhandBtn.onClick.RemoveAllListeners();
            equip_subhandBtn.onClick.AddListener(delegate ()
            {
                int index = equip_subhandBtn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_headBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_headBtn.onClick.RemoveAllListeners();
            equip_headBtn.onClick.AddListener(delegate ()
            {
                int index = equip_headBtn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_bodyBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_bodyBtn.onClick.RemoveAllListeners();
            equip_bodyBtn.onClick.AddListener(delegate ()
            {
                int index = equip_bodyBtn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_handBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_handBtn.onClick.RemoveAllListeners();
            equip_handBtn.onClick.AddListener(delegate ()
            {
                int index = equip_handBtn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_backBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_backBtn.onClick.RemoveAllListeners();
            equip_backBtn.onClick.AddListener(delegate ()
            {
                int index = equip_backBtn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_footBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_footBtn.onClick.RemoveAllListeners();
            equip_footBtn.onClick.AddListener(delegate ()
            {
                int index = equip_footBtn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_neckBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_neckBtn.onClick.RemoveAllListeners();
            equip_neckBtn.onClick.AddListener(delegate ()
            {
                int index = equip_neckBtn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_finger1Btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_finger1Btn.onClick.RemoveAllListeners();
            equip_finger1Btn.onClick.AddListener(delegate ()
            {
                int index = equip_finger1Btn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });

            equip_finger2Btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_finger2Btn.onClick.RemoveAllListeners();
            equip_finger2Btn.onClick.AddListener(delegate ()
            {
                int index = equip_finger2Btn.transform.GetComponent<InteractiveLabel>().index;
                if (index != -1)
                {
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });
        }
        else 
        {
            equip_titleText.text = "装备[调整模式]";
            equip_weaponBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_weaponBtn.onClick.RemoveAllListeners();
            equip_weaponBtn.onClick.AddListener(delegate ()
            {
                int heroID = equip_weaponBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_weaponBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart,(int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x+ gameObject.GetComponent<RectTransform>().sizeDelta.x+ GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_subhandBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_subhandBtn.onClick.RemoveAllListeners();
            equip_subhandBtn.onClick.AddListener(delegate ()
            {
                int heroID = equip_subhandBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_subhandBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_headBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_weaponBtn.onClick.RemoveAllListeners();
            equip_weaponBtn.onClick.AddListener(delegate ()
            {
                int heroID = equip_weaponBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_weaponBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_bodyBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_bodyBtn.onClick.RemoveAllListeners();
            equip_bodyBtn.onClick.AddListener(delegate ()
            {
                int heroID = equip_bodyBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_bodyBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_handBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_handBtn.onClick.RemoveAllListeners();
            equip_handBtn.onClick.AddListener(delegate ()
            {
                int heroID = equip_handBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_handBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_backBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_backBtn.onClick.RemoveAllListeners();
            equip_backBtn.onClick.AddListener(delegate ()
            {
                int heroID = equip_backBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_backBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_footBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_footBtn.onClick.RemoveAllListeners();
            equip_footBtn.onClick.AddListener(delegate ()
            {
                int heroID = equip_footBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_footBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_neckBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_neckBtn.onClick.RemoveAllListeners();
            equip_neckBtn.onClick.AddListener(delegate ()
            {
                int heroID = equip_neckBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_neckBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_finger1Btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_finger1Btn.onClick.RemoveAllListeners();
            equip_finger1Btn.onClick.AddListener(delegate ()
            {
                int heroID = equip_finger1Btn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_finger1Btn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_finger2Btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_finger2Btn.onClick.RemoveAllListeners();
            equip_finger2Btn.onClick.AddListener(delegate ()
            {
                int heroID = equip_finger2Btn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_finger2Btn.transform.GetComponent<InteractiveLabel>().equipPart;
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });
        }

        if (!nowSkillState)
        {
            skill_titleText.text = "招式[查看模式]";
        }
        else 
        {
            skill_titleText.text = "招式[调整模式]";
        }
    }

    public void UpdateAllInfo( HeroObject heroObject, int connY)
    {
        connRt.anchoredPosition = new Vector2(-19, connY);
        UpdateBasicInfo(heroObject);
        UpdateFightInfo( heroObject,EquipPart.None,null, 1);
        UpdateWorkInfo(heroObject);
        UpdateEquipAll( heroObject);
    }

    public void UpdateBasicInfo(HeroObject heroObject)
    {
        nameText.text = heroObject.name;
        picImage.overrideSprite = Resources.Load("Image/RolePic/" + heroObject.pic, typeof(Sprite)) as Sprite; ;
        infoText.text = "Lv." + heroObject.level+""+ DataManager.mCreateHeroTypeDict[heroObject.type].Name + " [Exp "+ heroObject .exp+ "]"+
            "\n薪金 150金币/月";
    }


    public void UpdateFightInfo(HeroObject heroObject ,EquipPart equipPart ,ItemObject itemObject, int page)
    {
        Debug.Log("UpdateFightInfo() equipPart=" + equipPart );
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

        int equipItemID = -1;
        

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

        if (page == 1)
        {
            infoFight_des1Text.text = "体力上限 " + ((equipPart==EquipPart.None)? OutputAttrStr(heroObject.hp,hpEquipAdd): OutputAttrChangeStr (heroObject.hp, hpEquipAdd,hpEquipAddNew, "")) +
                "\n体力恢复 " + ((equipPart == EquipPart.None) ? OutputAttrStr( heroObject.hpRenew,hpRenewEquipAdd) : OutputAttrChangeStr(heroObject.hpRenew, hpRenewEquipAdd, hpRenewEquipAddNew, "")) +
                "\n物攻 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.atkMin,atkMinEquipAdd) : OutputAttrChangeStr(heroObject.atkMin, atkMinEquipAdd, atkMinEquipAddNew, "")) + " - " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.atkMax,atkMaxEquipAdd) : OutputAttrChangeStr(heroObject.atkMax, atkMaxEquipAdd, atkMaxEquipAddNew, "")) +
                "\n魔攻 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mAtkMin,mAtkMinEquipAdd) : OutputAttrChangeStr(heroObject.mAtkMin, mAtkMinEquipAdd, mAtkMinEquipAddNew, "")) + " - " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mAtkMax,mAtkMaxEquipAdd) : OutputAttrChangeStr(heroObject.mAtkMax, mAtkMaxEquipAdd, mAtkMaxEquipAddNew, "")) +
                "\n物防 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.def,defEquipAdd) : OutputAttrChangeStr(heroObject.def, defEquipAdd, defEquipAddNew, "")) +
                "\n命中 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.hit,hitEquipAdd) : OutputAttrChangeStr(heroObject.hit, hitEquipAdd, hitEquipAddNew, "")) +
                "\n闪避 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.dod,dodEquipAdd) : OutputAttrChangeStr(heroObject.dod, dodEquipAdd, dodEquipAddNew, "")) +
                "\n速度 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.spd,spdEquipAdd) : OutputAttrChangeStr(heroObject.spd, spdEquipAdd, spdEquipAddNew, "")) +
                "\n风系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.windDam,windDamEquipAdd) + "%" : OutputAttrChangeStr(heroObject.windDam, windDamEquipAdd, windDamEquipAddNew, "%"))  +
                "\n火系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.fireDam,fireDamEquipAdd) + "%" : OutputAttrChangeStr(heroObject.fireDam, fireDamEquipAdd, fireDamEquipAddNew, "%"))  +
                "\n水系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.waterDam,waterDamEquipAdd) + "%" : OutputAttrChangeStr(heroObject.waterDam, waterDamEquipAdd, waterDamEquipAddNew, "%"))  +
                "\n地系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.groundDam,groundDamEquipAdd) + "%" : OutputAttrChangeStr(heroObject.groundDam, groundDamEquipAdd, groundDamEquipAddNew, "%"))  +
                "\n光系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.lightDam,lightDamEquipAdd) + "%" : OutputAttrChangeStr(heroObject.lightDam, lightDamEquipAdd, lightDamEquipAddNew, "%"))  +
                "\n暗系伤害 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.darkDam,darkDamEquipAdd) + "%" : OutputAttrChangeStr(heroObject.darkDam, darkDamEquipAdd, darkDamEquipAddNew, "%")) ;
            infoFight_des2Text.text = "魔力上限 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mp,mpEquipAdd) : OutputAttrChangeStr(heroObject.mp, mpEquipAdd, mpEquipAddNew, "")) +
                "\n魔力恢复 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mpRenew,mpRenewEquipAdd) : OutputAttrChangeStr(heroObject.mpRenew, mpRenewEquipAdd, mpRenewEquipAddNew, "")) +
                "\n " +
                "\n " +
                "\n魔防 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.mDef,mDefEquipAdd) : OutputAttrChangeStr(heroObject.mDef, mDefEquipAdd, mDefEquipAddNew, "")) +
                "\n暴击 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.criR,criREquipAdd) : OutputAttrChangeStr(heroObject.criR, criREquipAdd, criREquipAddNew, "")) +
                "\n爆伤 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.criD,criDEquipAdd) + "%" : OutputAttrChangeStr(heroObject.criD, criDEquipAdd, criDEquipAddNew, "%"))  +
                "\n " +
                "\n风系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.windRes,windResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.windRes, windResEquipAdd, windResEquipAddNew, "%"))  +
                "\n火系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.fireRes,fireResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.fireRes, fireResEquipAdd, fireResEquipAddNew, "%")) +
                "\n水系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.waterRes,waterResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.waterRes, waterResEquipAdd, waterResEquipAddNew, "%"))  +
                "\n地系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.groundRes,groundResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.groundRes, groundResEquipAdd, groundResEquipAddNew, "%"))  +
                "\n光系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.lightRes,lightResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.lightRes, lightResEquipAdd, lightResEquipAddNew, "%")) +
                "\n暗系抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.darkRes,darkResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.darkRes, darkResEquipAdd, darkResEquipAddNew, "%")) ;
            infoFight_page2Btn.interactable = true;
            infoFight_page2Btn.onClick.RemoveAllListeners();
            infoFight_page2Btn.onClick.AddListener(delegate () { UpdateFightInfo(heroObject,equipPart,itemObject, 2); });
            infoFight_page1Btn.interactable = false;
        }
        else if((page == 2))
        {
            infoFight_des1Text.text = "眩晕抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.dizzyRes,dizzyResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.hp, hpEquipAdd, hpEquipAddNew, "%")) +
                "\n混乱抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.confusionRes,confusionResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.hp, hpEquipAdd, hpEquipAddNew, "%")) +
                "\n中毒抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.poisonRes,poisonResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.hp, hpEquipAdd, hpEquipAddNew, "%"))  +
                "\n睡眠抗性 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.sleepRes,sleepResEquipAdd) + "%" : OutputAttrChangeStr(heroObject.hp, hpEquipAdd, hpEquipAddNew, "%"))  +
                "\n经验值获得加成 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.expGet,expGetEquipAdd) + "%" : OutputAttrChangeStr(heroObject.hp, hpEquipAdd, hpEquipAddNew, "%"))  +
                "\n金币获得加成 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.goldGet,goldGetEquipAdd) + "%" : OutputAttrChangeStr(heroObject.hp, hpEquipAdd, hpEquipAddNew, "%"))  +
                "\n稀有物品掉落加成 " + ((equipPart == EquipPart.None) ? OutputAttrStr(heroObject.itemGet,itemGetEquipAdd) + "%" : OutputAttrChangeStr(heroObject.hp, hpEquipAdd, hpEquipAddNew, "%")) ;
            infoFight_des2Text.text = "";

            infoFight_page1Btn.interactable = true;
            infoFight_page1Btn.onClick.RemoveAllListeners();
            infoFight_page1Btn.onClick.AddListener(delegate () { UpdateFightInfo(heroObject, equipPart, itemObject, 1); });
            infoFight_page2Btn.interactable = false;

        }

    }



    string OutputAttrStr(int basic, int equipAdd)
    {
        return (basic + equipAdd) >= 0 ? (basic + equipAdd).ToString():"0";
    }

    string OutputAttrChangeStr(int basic,int addNow ,int addNew ,string suffixes)
    {
        int nowValue = (basic + addNow) >= 0 ? basic + addNow : 0;
        int newValue = (basic + addNew) >= 0 ? basic + addNew : 0;
        int change = newValue - nowValue;
        string changeStr = " ";
        if (change > 0)
        {
            changeStr = " <color=#76EE00>+" + change + suffixes+"</color>";
        }
        else if (change < 0)
        {
            changeStr = " <color=#FF4A4A>-" + change + suffixes+"</color>";
        }
        
        return newValue+ suffixes + changeStr;
    }

    public void UpdateWorkInfo(HeroObject heroObject)
    {
        infoWork_desText.text = "<color=#FFBD58>种植</color> " + gc.ValueToRank(heroObject.workPlanting) +
            "  <color=#FFBD58>饲养</color> " + gc.ValueToRank(heroObject.workFeeding) +
            "  <color=#FFBD58>钓鱼</color> " + gc.ValueToRank(heroObject.workFishing) +
            "  <color=#FFBD58>打猎</color> " + gc.ValueToRank(heroObject.workHunting) +
            "\n<color=#FFBD58>伐木</color> " + gc.ValueToRank(heroObject.workFelling) +
            "  <color=#FFBD58>挖矿</color> " + gc.ValueToRank(heroObject.workQuarrying) +
            "  <color=#FFBD58>采石</color> " + gc.ValueToRank(heroObject.workMining) +
            "  <color=#FFBD58>建筑</color> " + gc.ValueToRank(heroObject.workBuild) +
            "\n<color=#F0A0FF>武器锻造</color> " + gc.ValueToRank(heroObject.workMakeWeapon) +
            "        <color=#F0A0FF>防具制作</color> " + gc.ValueToRank(heroObject.workMakeArmor) +
            "\n<color=#F0A0FF>饰品制作</color> " + gc.ValueToRank(heroObject.workMakeJewelry) +
            "        <color=#F0A0FF>卷轴研究</color> " + "???" +
            "\n<color=#62D5EE>管理</color> " + gc.ValueToRank(heroObject.workSundry);
    }

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

    public void UpdateEquip(HeroObject heroObject, EquipPart equipPart)
    {
        switch (equipPart)
        {
            case EquipPart.Weapon:
                if (heroObject.equipWeapon != -1)
                {
                    equip_weaponText.text = gc.itemDic[heroObject.equipWeapon].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipWeapon].rank);
                    equip_weaponImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipWeapon].pic, typeof(Sprite)) as Sprite;
                    equip_weaponUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_weaponUnSetBtn.onClick.RemoveAllListeners();
                    equip_weaponUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Weapon); });
                }
                else
                {
                    equip_weaponText.text = "主手<color=red>（未装备）</color>";
                    equip_weaponImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_weaponUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_weaponBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipWeapon;
                equip_weaponBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Subhand:
                if (heroObject.equipSubhand != -1)
                {
                    equip_subhandText.text = gc.itemDic[heroObject.equipSubhand].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipSubhand].rank);
                    equip_subhandImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipSubhand].pic, typeof(Sprite)) as Sprite;
                    equip_subhandUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_subhandUnSetBtn.onClick.RemoveAllListeners();
                    equip_subhandUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Subhand); });
                }
                else
                {
                    equip_subhandText.text = "副手<color=red>（未装备）</color>";
                    equip_subhandImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_subhandUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_subhandBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipSubhand;
                equip_subhandBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Head:
                if (heroObject.equipHead != -1)
                {
                    equip_headText.text = gc.itemDic[heroObject.equipHead].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipHead].rank);
                    equip_headImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipHead].pic, typeof(Sprite)) as Sprite;
                    equip_headUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_headUnSetBtn.onClick.RemoveAllListeners();
                    equip_headUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Head); });
                }
                else
                {
                    equip_headText.text = "头部防具<color=red>（未装备）</color>";
                    equip_headImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_headUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_headBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipHead;
                equip_headBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Body:
                if (heroObject.equipBody != -1)
                {
                    equip_bodyText.text = gc.itemDic[heroObject.equipBody].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipBody].rank);
                    equip_bodyImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipBody].pic, typeof(Sprite)) as Sprite;
                    equip_bodyUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_bodyUnSetBtn.onClick.RemoveAllListeners();
                    equip_bodyUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Body); });
                }
                else
                {
                    equip_bodyText.text = "身体防具<color=red>（未装备）</color>";
                    equip_bodyImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_bodyUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_bodyBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipBody;
                equip_bodyBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Hand:
                if (heroObject.equipHand != -1)
                {
                    equip_handText.text = gc.itemDic[heroObject.equipHand].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipHand].rank);
                    equip_handImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipHand].pic, typeof(Sprite)) as Sprite;
                    equip_handUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_handUnSetBtn.onClick.RemoveAllListeners();
                    equip_handUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Hand); });
                }
                else
                {
                    equip_handText.text = "手部防具<color=red>（未装备）</color>";
                    equip_handImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_handUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_handBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipHand;
                equip_handBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Back:
                if (heroObject.equipBack != -1)
                {
                    equip_backText.text = gc.itemDic[heroObject.equipBack].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipBack].rank);
                    equip_backImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipBack].pic, typeof(Sprite)) as Sprite;
                    equip_backUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_backUnSetBtn.onClick.RemoveAllListeners();
                    equip_backUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Back); });
                }
                else
                {
                    equip_backText.text = "背部防具<color=red>（未装备）</color>";
                    equip_backImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_backUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_backBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipBack;
                equip_backBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Foot:
                if (heroObject.equipFoot != -1)
                {
                    equip_footText.text = gc.itemDic[heroObject.equipFoot].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFoot].rank);
                    equip_footImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFoot].pic, typeof(Sprite)) as Sprite;
                    equip_footUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_footUnSetBtn.onClick.RemoveAllListeners();
                    equip_footUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Foot); });
                }
                else
                {
                    equip_footText.text = "腿部防具<color=red>（未装备）</color>";
                    equip_footImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_footUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_footBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFoot;
                equip_footBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Neck:
                if (heroObject.equipNeck != -1)
                {
                    equip_neckText.text = gc.itemDic[heroObject.equipNeck].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipNeck].rank);
                    equip_neckImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipNeck].pic, typeof(Sprite)) as Sprite;
                    equip_neckUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_neckUnSetBtn.onClick.RemoveAllListeners();
                    equip_neckUnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Neck); });
                }
                else
                {
                    equip_neckText.text = "项链<color=red>（未装备）</color>";
                    equip_neckImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_neckUnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_neckBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipNeck;
                equip_neckBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Finger1:
                if (heroObject.equipFinger1 != -1)
                {
                    equip_finger1Text.text = gc.itemDic[heroObject.equipFinger1].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFinger1].rank);
                    equip_finger1Image.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFinger1].pic, typeof(Sprite)) as Sprite;
                    equip_finger1UnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_finger1UnSetBtn.onClick.RemoveAllListeners();
                    equip_finger1UnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Finger1); });
                }
                else
                {
                    equip_finger1Text.text = "戒指<color=red>（未装备）</color>";
                    equip_finger1Image.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_finger1UnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_finger1Btn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFinger1;
                equip_finger1Btn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
            case EquipPart.Finger2:
                if (heroObject.equipFinger2 != -1)
                {
                    equip_finger2Text.text = gc.itemDic[heroObject.equipFinger2].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFinger2].rank);
                    equip_finger2Image.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFinger2].pic, typeof(Sprite)) as Sprite;
                    equip_finger2UnSetBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    equip_finger2UnSetBtn.onClick.RemoveAllListeners();
                    equip_finger2UnSetBtn.onClick.AddListener(delegate () { gc.HeroEquipUnSet(heroObject.id, EquipPart.Finger2); });
                }
                else
                {
                    equip_finger2Text.text = "戒指<color=red>（未装备）</color>";
                    equip_finger2Image.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                    equip_finger2UnSetBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                equip_finger2Btn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFinger2;
                equip_finger2Btn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
                break;
        }
    }
}
