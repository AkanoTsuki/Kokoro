using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanel : BasePanel
{
    public static HeroPanel Instance;

    public Text nameText;
    public Image picImage;
    public Text infoText;

    public Button infoFight_page1Btn;
    public Button infoFight_page2Btn;
    public Text infoFight_des1Text;
    public Text infoFight_des2Text;
    public Text infoWork_desText;

    public Button equip_weaponBtn;
    public Button equip_headBtn;
    public Button equip_bodyBtn;
    public Button equip_handBtn;
    public Button equip_lowerBtn;
    public Button equip_footBtn;
    public Button equip_neckBtn;
    public Button equip_finger1Btn;
    public Button equip_finger2Btn;

    public Image equip_weaponImage;
    public Image equip_headImage;
    public Image equip_bodyImage;
    public Image equip_handImage;
    public Image equip_lowerImage;
    public Image equip_footImage;
    public Image equip_neckImage;
    public Image equip_finger1Image;
    public Image equip_finger2Image;

    public Text equip_weaponText;
    public Text equip_headText;
    public Text equip_bodyText;
    public Text equip_handText;
    public Text equip_lowerText;
    public Text equip_footText;
    public Text equip_neckText;
    public Text equip_finger1Text;
    public Text equip_finger2Text;

    public Button closeBtn;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameControl gc = GameObject.Find("GameManager").GetComponent<GameControl>();


    }
    public void OnShow(GameControl gc, int heroID)
    {

        UpdateAllInfo(gc, heroID);
        ShowByImmediately(true);
    }

    public override void OnHide()
    {
        HideByImmediately();
    }

    public void UpdateAllInfo(GameControl gc, int heroID)
    {
        UpdateFightInfo(gc, heroID, 1);
        UpdateWorkInfo(gc, heroID);
        UpdateEquip(gc, heroID);
    }


    public void UpdateFightInfo(GameControl gc,int heroID,int page)
    {
        if (page == 1)
        {
            infoFight_des1Text.text = "体力上限 " + gc.heroDic[heroID].hp +
                "\n体力恢复 " + gc.heroDic[heroID].hpRenew +
                "\n物攻 " + gc.heroDic[heroID].atkMin +" - "+ gc.heroDic[heroID].atkMax +
                "\n魔攻 " + gc.heroDic[heroID].mAtkMin + " - " + gc.heroDic[heroID].mAtkMax +
                "\n物防 " + gc.heroDic[heroID].def +
                "\n命中 " + gc.heroDic[heroID].hit +
                "\n闪避 " + gc.heroDic[heroID].dod +
                "\n速度 " + gc.heroDic[heroID].spd +
                "\n风系伤害 " + gc.heroDic[heroID].windDam +
                "\n火系伤害 " + gc.heroDic[heroID].fireDam +
                "\n水系伤害 " + gc.heroDic[heroID].waterDam +
                "\n地系伤害 " + gc.heroDic[heroID].groundDam +
                "\n光系伤害 " + gc.heroDic[heroID].lightDam +
                "\n暗系伤害 " + gc.heroDic[heroID].darkDam;
            infoFight_des2Text.text = "魔力上限 " + gc.heroDic[heroID].mp +
                "\n魔力恢复 " + gc.heroDic[heroID].mpRenew +
                "\n " + 
                "\n " + 
                "\n魔防 " + gc.heroDic[heroID].mDef +
                "\n暴击 " + gc.heroDic[heroID].criR +
                "\n爆伤 " + gc.heroDic[heroID].criD +"%"+
                "\n " +
                "\n风系抗性 " + gc.heroDic[heroID].windRes +
                "\n火系抗性 " + gc.heroDic[heroID].fireRes +
                "\n水系抗性 " + gc.heroDic[heroID].waterRes +
                "\n地系抗性 " + gc.heroDic[heroID].groundRes +
                "\n光系抗性 " + gc.heroDic[heroID].lightRes +
                "\n暗系抗性 " + gc.heroDic[heroID].darkRes;
        }

    }

    public void UpdateWorkInfo(GameControl gc, int heroID)
    {
        infoWork_desText.text = "<color=#FFBD58>种植</color> " + gc.ValueToRank(gc.heroDic[heroID].workPlanting) +
            "   <color=#FFBD58>饲养</color> " + gc.ValueToRank(gc.heroDic[heroID].workFeeding) +
            "   <color=#FFBD58>钓鱼</color> " + gc.ValueToRank(gc.heroDic[heroID].workFishing) +
            "   <color=#FFBD58>打猎</color> " + gc.ValueToRank(gc.heroDic[heroID].workHunting) +
            "\n<color =#FFBD58>伐木</color> " + gc.ValueToRank(gc.heroDic[heroID].workFelling) +
            "   <color=#FFBD58>挖矿</color> " + gc.ValueToRank(gc.heroDic[heroID].workQuarrying) +
            "   <color=#FFBD58>采石</color> " + gc.ValueToRank(gc.heroDic[heroID].workMining) +
            "   <color=#FFBD58>建筑</color> " + gc.ValueToRank(gc.heroDic[heroID].workBuild) +
            "\n<color =#F0A0FF>武器锻造</color> " + gc.ValueToRank(gc.heroDic[heroID].workMakeWeapon) +
            "          <color=#F0A0FF>防具制作</color> " + gc.ValueToRank(gc.heroDic[heroID].workMakeArmor) +
            "\n<color =#F0A0FF>饰品制作</color> " + gc.ValueToRank(gc.heroDic[heroID].workMakeJewelry) +
            "          <color=#F0A0FF>卷轴研究</color> " + "???" +
            "\n<color =#62D5EE>管理</color> " + gc.ValueToRank(gc.heroDic[heroID].workSundry) ;
    }

    public void UpdateEquip(GameControl gc, int heroID)
    {
        equip_weaponText.text = gc.itemDic[gc.heroDic[heroID].equipWeapon].name + "\n" + gc.OutputSignStr("★", gc.itemDic[gc.heroDic[heroID].equipWeapon].rank);
        equip_weaponImage.overrideSprite = Resources.Load("Image/RolePic/"+gc.heroDic[heroID].pic, typeof(Sprite)) as Sprite;
        equip_weaponBtn.onClick.AddListener(delegate () {  });
    }
}
