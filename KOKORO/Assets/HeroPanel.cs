using System.Collections;
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

        UpdateAllInfo(gc, heroObject, connY);
        nowEquipState = false;
        nowSkillState = false;
        UpdateButtonStatus();
        SetAnchoredPosition(x, y);
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
    }

    public void UpdateButtonStatus()
    {
        //Debug.Log("nowEquipState=" + nowEquipState);
        if (!nowEquipState)
        {
            totalSet_equipBtn.transform.GetChild(0).GetComponent<Text>().text = "装备[调整模式]";
            equip_weaponBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_weaponBtn.onClick.RemoveAllListeners();
            equip_weaponBtn.onClick.AddListener(delegate ()
            {
                int index= equip_weaponBtn.transform.GetComponent<InteractiveLabel>().index;
                Debug.Log("查看模式 equip_weaponBtn点击了 index=" + index);
                if (index != -1)
                {
                   
                    ItemListAndInfoPanel.Instance.nowItemID = index;
                    ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing),
                   (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                }
            });
            equip_subhandBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_headBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_bodyBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_handBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_backBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_footBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_neckBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_finger1Btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
            equip_finger2Btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentLook;
        }
        else 
        {
            totalSet_equipBtn.transform.GetChild(0).GetComponent<Text>().text = "装备[查看模式]";
            equip_weaponBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_weaponBtn.onClick.RemoveAllListeners();
            equip_weaponBtn.onClick.AddListener(delegate ()
            {
                //ItemListAndInfoPanel.Instance.nowItemID = index;

                int heroID = equip_weaponBtn.transform.GetComponent<InteractiveLabel>().heroID;
                EquipPart equipPart = equip_weaponBtn.transform.GetComponent<InteractiveLabel>().equipPart;
                Debug.Log("调整模式 equip_weaponBtn点击了 heroID=" + heroID+ " equipPart=" + equipPart);
                ItemListAndInfoPanel.Instance.OnShow(heroID, equipPart,(int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x+ gameObject.GetComponent<RectTransform>().sizeDelta.x+ GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            });

            equip_subhandBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_headBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_bodyBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_handBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_backBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_footBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_neckBtn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_finger1Btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
            equip_finger2Btn.transform.GetComponent<InteractiveLabel>().labelType = LabelType.EquipmentSet;
        }

        if (nowSkillState)
        {
            totalSet_skillBtn.transform.GetChild(0).GetComponent<Text>().text = "招式[调整模式]";
        }
        else 
        {
            totalSet_skillBtn.transform.GetChild(0).GetComponent<Text>().text = "招式[查看模式]";
        }
    }

    public void UpdateAllInfo(GameControl gc, HeroObject heroObject, int connY)
    {
        connRt.anchoredPosition = new Vector2(-19, connY);
        UpdateBasicInfo(heroObject);
        UpdateFightInfo( heroObject, 1);
        UpdateWorkInfo(gc, heroObject);
        UpdateEquip(gc, heroObject);
    }

    public void UpdateBasicInfo(HeroObject heroObject)
    {
        nameText.text = heroObject.name;
        picImage.overrideSprite = Resources.Load("Image/RolePic/" + heroObject.pic, typeof(Sprite)) as Sprite; ;
        infoText.text = "Lv." + heroObject.level+""+ DataManager.mCreateHeroTypeDict[heroObject.type].Name + " [Exp "+ heroObject .exp+ "]"+
            "\n薪金 150金币/月";
    }


    public void UpdateFightInfo(HeroObject heroObject ,int page)
    {
        if (page == 1)
        {
            infoFight_des1Text.text = "体力上限 " + heroObject.hp +
                "\n体力恢复 " + heroObject.hpRenew +
                "\n物攻 " + heroObject.atkMin +" - "+ heroObject.atkMax +
                "\n魔攻 " + heroObject.mAtkMin + " - " + heroObject.mAtkMax +
                "\n物防 " + heroObject.def +
                "\n命中 " + heroObject.hit +
                "\n闪避 " + heroObject.dod +
                "\n速度 " + heroObject.spd +
                "\n风系伤害 " + heroObject.windDam +
                "\n火系伤害 " + heroObject.fireDam +
                "\n水系伤害 " + heroObject.waterDam +
                "\n地系伤害 " + heroObject.groundDam +
                "\n光系伤害 " + heroObject.lightDam +
                "\n暗系伤害 " + heroObject.darkDam;
            infoFight_des2Text.text = "魔力上限 " + heroObject.mp +
                "\n魔力恢复 " + heroObject.mpRenew +
                "\n " + 
                "\n " + 
                "\n魔防 " + heroObject.mDef +
                "\n暴击 " + heroObject.criR +
                "\n爆伤 " + heroObject.criD +"%"+
                "\n " +
                "\n风系抗性 " + heroObject.windRes +
                "\n火系抗性 " + heroObject.fireRes +
                "\n水系抗性 " + heroObject.waterRes +
                "\n地系抗性 " + heroObject.groundRes +
                "\n光系抗性 " + heroObject.lightRes +
                "\n暗系抗性 " + heroObject.darkRes;
        }

    }

    public void UpdateWorkInfo(GameControl gc,HeroObject heroObject)
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

    public void UpdateEquip(GameControl gc, HeroObject heroObject)
    {
        if (heroObject.equipWeapon != -1)
        {
            equip_weaponText.text = gc.itemDic[heroObject.equipWeapon].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipWeapon].rank);
            equip_weaponImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipWeapon].pic, typeof(Sprite)) as Sprite;
            
            //equip_weaponBtn.onClick.AddListener(delegate () { });
        }
        else
        {
            equip_weaponText.text = "主手<color=red>（未装备）</color>";
            equip_weaponImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_weaponBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipWeapon;
        equip_weaponBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipSubhand != -1)
        {
            equip_subhandText.text = gc.itemDic[heroObject.equipSubhand].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipSubhand].rank);
            equip_subhandImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipSubhand].pic, typeof(Sprite)) as Sprite;
    
        }
        else
        {
            equip_subhandText.text = "副手<color=red>（未装备）</color>";
            equip_subhandImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_subhandBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipSubhand;
        equip_subhandBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipHead != -1)
        {
            equip_headText.text = gc.itemDic[heroObject.equipHead].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipHead].rank);
            equip_headImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipHead].pic, typeof(Sprite)) as Sprite;
           
        }
        else
        {
            equip_headText.text = "头部防具<color=red>（未装备）</color>";
            equip_headImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_headBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipHead;
        equip_headBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipBody != -1)
        {
            equip_bodyText.text = gc.itemDic[heroObject.equipBody].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipBody].rank);
            equip_bodyImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipBody].pic, typeof(Sprite)) as Sprite;

        }
        else
        {
            equip_bodyText.text = "身体防具<color=red>（未装备）</color>";
            equip_bodyImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_bodyBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipBody;
        equip_bodyBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipHand != -1)
        {
            equip_handText.text = gc.itemDic[heroObject.equipHand].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipHand].rank);
            equip_handImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipHand].pic, typeof(Sprite)) as Sprite;
            
        }
        else
        {
            equip_handText.text = "手部防具<color=red>（未装备）</color>";
            equip_handImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_handBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipHand;
        equip_handBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipBack != -1)
        {
            equip_backText.text = gc.itemDic[heroObject.equipBack].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipBack].rank);
            equip_backImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipBack].pic, typeof(Sprite)) as Sprite;

        }
        else
        {
            equip_backText.text = "背部防具<color=red>（未装备）</color>";
            equip_backImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_backBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipBack;
        equip_backBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipFoot != -1)
        {
            equip_footText.text = gc.itemDic[heroObject.equipFoot].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFoot].rank);
            equip_footImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFoot].pic, typeof(Sprite)) as Sprite;
          
        }
        else
        {
            equip_footText.text = "腿部防具<color=red>（未装备）</color>";
            equip_footImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_footBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFoot;
        equip_footBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipNeck != -1)
        {
            equip_neckText.text = gc.itemDic[heroObject.equipNeck].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipNeck].rank);
            equip_neckImage.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipNeck].pic, typeof(Sprite)) as Sprite;
        }
        else
        {
            equip_neckText.text = "项链<color=red>（未装备）</color>";
            equip_neckImage.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_neckBtn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipNeck;
        equip_neckBtn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipFinger1 != -1)
        {
            equip_finger1Text.text = gc.itemDic[heroObject.equipFinger1].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFinger1].rank);
            equip_finger1Image.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFinger1].pic, typeof(Sprite)) as Sprite;
            
        }
        else
        {
            equip_finger1Text.text = "戒指<color=red>（未装备）</color>";
            equip_finger1Image.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_finger1Btn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFinger1;
        equip_finger1Btn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;

        if (heroObject.equipFinger2 != -1)
        {
            equip_finger2Text.text = gc.itemDic[heroObject.equipFinger2].name + "\n" + gc.OutputSignStr("★", gc.itemDic[heroObject.equipFinger2].rank);
            equip_finger2Image.overrideSprite = Resources.Load("Image/ItemPic/" + gc.itemDic[heroObject.equipFinger2].pic, typeof(Sprite)) as Sprite;
           
        }
        else
        {
            equip_finger2Text.text = "戒指<color=red>（未装备）</color>";
            equip_finger2Image.overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }
        equip_finger2Btn.transform.GetComponent<InteractiveLabel>().index = heroObject.equipFinger2;
        equip_finger2Btn.transform.GetComponent<InteractiveLabel>().heroID = heroObject.id;
    }
}
