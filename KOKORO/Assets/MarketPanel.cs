using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MarketPanel : BasePanel
{
    public static MarketPanel Instance;
    GameControl gc;

    public Text titleText;

     public Text numText;

    public Button filter_typeBig_allBtn;
    public Button filter_typeBig_weaponBtn;
    public Button filter_typeBig_subhandBtn;
    public Button filter_typeBig_armorBtn;
    public Button filter_typeBig_jewelryBtn;
    public Button filter_typeBig_scrollBtn;

    public Text filter_typeBig_allText;
    public Text filter_typeBig_weaponText;
    public Text filter_typeBig_subhandText;
    public Text filter_typeBig_armorText;
    public Text filter_typeBig_jewelryText;
    public Text filter_typeBig_scrollText;

    public Button filter_typeSmall_allBtn;
    public Text filter_typeSmall_allText;
    public List<Button> filter_typeSmall_btnList;
    public List<Text> filter_typeSmall_textList;

    public GameObject itemListGo;

    public Button supplyAndDemandBtn;
    public Button closeBtn;

    public ItemTypeBig itemTypeBig;
    public ItemTypeSmall itemTypeSmall;
    //对象池
    List<GameObject> itemSkillGoPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    void Start()
    {
        filter_typeBig_allBtn.onClick.AddListener(delegate () { UpdateList(gc.nowCheckingDistrictID, ItemTypeBig.None, ItemTypeSmall.None); UpdateTypeSmallButton(gc.nowCheckingDistrictID, ItemTypeBig.None); });
        filter_typeBig_weaponBtn.onClick.AddListener(delegate () { UpdateList(gc.nowCheckingDistrictID, ItemTypeBig.Weapon, ItemTypeSmall.None); UpdateTypeSmallButton(gc.nowCheckingDistrictID, ItemTypeBig.Weapon); });
        filter_typeBig_subhandBtn.onClick.AddListener(delegate () { UpdateList(gc.nowCheckingDistrictID, ItemTypeBig.Subhand, ItemTypeSmall.None); UpdateTypeSmallButton(gc.nowCheckingDistrictID, ItemTypeBig.Subhand); });
        filter_typeBig_armorBtn.onClick.AddListener(delegate () { UpdateList(gc.nowCheckingDistrictID, ItemTypeBig.Armor, ItemTypeSmall.None); UpdateTypeSmallButton(gc.nowCheckingDistrictID, ItemTypeBig.Armor); });
        filter_typeBig_jewelryBtn.onClick.AddListener(delegate () { UpdateList(gc.nowCheckingDistrictID, ItemTypeBig.Jewelry, ItemTypeSmall.None); UpdateTypeSmallButton(gc.nowCheckingDistrictID, ItemTypeBig.Jewelry); });
        filter_typeBig_scrollBtn.onClick.AddListener(delegate () { UpdateList(gc.nowCheckingDistrictID, ItemTypeBig.SkillRoll, ItemTypeSmall.None); UpdateTypeSmallButton(gc.nowCheckingDistrictID, ItemTypeBig.SkillRoll); });

        supplyAndDemandBtn.onClick.AddListener(delegate () { SupplyAndDemandPanel.Instance.OnShow(gc.nowCheckingDistrictID, (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)gameObject.GetComponent<RectTransform>().anchoredPosition.y); }); 
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }



    public void OnShow(short districtID, ItemTypeBig itemTypeBig, ItemTypeSmall itemTypeSmall,  int x, int y)
    {
   
        UpdateAllInfo(districtID, itemTypeBig, itemTypeSmall);
        SetAnchoredPosition(x, y);
        isShow = true;
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }

    public void UpdateAllInfo(short districtID, ItemTypeBig itemTypeBig, ItemTypeSmall itemTypeSmall)
    {
        this.itemTypeBig = itemTypeBig;
        this.itemTypeSmall = itemTypeSmall;
        UpdateTypeBigButtonText(districtID);
        UpdateTypeSmallButton(districtID, itemTypeBig);
        UpdateList(districtID, itemTypeBig, itemTypeSmall);
    }

    public void UpdateTypeBigButtonText(short districtID)
    {
        int weapon = 0;
        int subhand = 0;
        int armor = 0;
        int jewelry = 0;
        int scroll = 0;
        foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
        {
            if (kvp.Value.districtID == districtID && kvp.Value.isGoods )
            {
                switch (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig)
                {
                    case ItemTypeBig.Weapon: weapon++; break;
                    case ItemTypeBig.Subhand: subhand++; break;
                    case ItemTypeBig.Armor: armor++; break;
                    case ItemTypeBig.Jewelry: jewelry++; break;

                }
            }
        }
        foreach (KeyValuePair<int, SkillObject> kvp in gc.skillDic)
        {
            if (kvp.Value.districtID == districtID && kvp.Value.isGoods)
            {
                scroll++;
            }
        }

        filter_typeBig_allText.text = "全部[" + (weapon + subhand + armor + jewelry + scroll) + "]";
        filter_typeBig_weaponText.text = "武器[" + weapon + "]";
        filter_typeBig_subhandText.text = "副手[" + subhand + "]"; ;
        filter_typeBig_armorText.text = "防具[" + armor + "]"; ;
        filter_typeBig_jewelryText.text = "饰品[" + jewelry + "]"; ;
        filter_typeBig_scrollText.text = "卷轴[" + scroll + "]"; ;
    }

    public void UpdateTypeSmallButton(short districtID, ItemTypeBig itemTypeBig)
    {
        switch (itemTypeBig)
        {
            case ItemTypeBig.None:
                for (byte i = 0; i < 13; i++)
                {
                    filter_typeSmall_btnList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                filter_typeSmall_allBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                break;
            case ItemTypeBig.Weapon:
                int sword = 0;
                int axe = 0;
                int spear = 0;
                int hammer = 0;
                int bow = 0;
                int staff = 0;
                foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
                {
                    if (kvp.Value.districtID == districtID && kvp.Value.isGoods && DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == itemTypeBig)
                    {
                        switch (DataManager.mItemDict[kvp.Value.prototypeID].TypeSmall)
                        {
                            case ItemTypeSmall.Sword: sword++; break;
                            case ItemTypeSmall.Axe: axe++; break;
                            case ItemTypeSmall.Spear: spear++; break;
                            case ItemTypeSmall.Hammer: hammer++; break;
                            case ItemTypeSmall.Bow: bow++; break;
                            case ItemTypeSmall.Staff: staff++; break;
                        }
                    }
                }
                filter_typeSmall_allText.text="全部["+ (sword+ axe+ spear+ hammer+ bow+ staff) + "]";
                filter_typeSmall_allBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_allBtn.onClick.RemoveAllListeners();
                filter_typeSmall_allBtn.onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.None); });

                filter_typeSmall_btnList[0].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[0].text = "剑[" + sword + "]";
                filter_typeSmall_btnList[0].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[0].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Sword); });

                filter_typeSmall_btnList[1].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[1].text = "斧[" + axe + "]";
                filter_typeSmall_btnList[1].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[1].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Axe); });

                filter_typeSmall_btnList[2].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[2].text = "枪[" + spear + "]";
                filter_typeSmall_btnList[2].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[2].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Spear); });

                filter_typeSmall_btnList[3].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[3].text = "锤[" + hammer + "]";
                filter_typeSmall_btnList[3].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[3].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Hammer); });

                filter_typeSmall_btnList[4].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[4].text = "弓[" + bow + "]";
                filter_typeSmall_btnList[4].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[4].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Bow); });

                filter_typeSmall_btnList[5].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[5].text = "杖[" + staff + "]";
                filter_typeSmall_btnList[5].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[5].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Staff); });

                for (byte i = 6; i < 13; i++)
                {
                    filter_typeSmall_btnList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                break;
            case ItemTypeBig.Subhand:
                int shield = 0;
                int dorlach = 0;
                foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
                {
                    if (kvp.Value.districtID == districtID && kvp.Value.isGoods && DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == itemTypeBig)
                    {
                        switch (DataManager.mItemDict[kvp.Value.prototypeID].TypeSmall)
                        {
                            case ItemTypeSmall.Shield: shield++; break;
                            case ItemTypeSmall.Dorlach: dorlach++; break;
                        }
                    }
                }
                filter_typeSmall_allText.text = "全部[" + (shield + dorlach ) + "]";
                filter_typeSmall_allBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_allBtn.onClick.RemoveAllListeners();
                filter_typeSmall_allBtn.onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.None); });

                filter_typeSmall_btnList[0].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[0].text = "盾[" + shield + "]";
                filter_typeSmall_btnList[0].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[0].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Shield); });

                filter_typeSmall_btnList[1].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[1].text = "箭袋[" + dorlach + "]";
                filter_typeSmall_btnList[1].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[1].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Dorlach); });


                for (byte i = 3; i < 13; i++)
                {
                    filter_typeSmall_btnList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                break;
            case ItemTypeBig.Armor:
                int headH = 0;
                int bodyH = 0;
                int handH = 0;
                int backH = 0;
                int footH = 0;
                int headL = 0;
                int bodyL = 0;
                int handL = 0;
                int backL = 0;
                int footL = 0;
                foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
                {
                    if (kvp.Value.districtID == districtID && kvp.Value.isGoods && DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == itemTypeBig)
                    {
                        switch (DataManager.mItemDict[kvp.Value.prototypeID].TypeSmall)
                        {
                            case ItemTypeSmall.HeadH: headH++; break;
                            case ItemTypeSmall.BodyH: bodyH++; break;
                            case ItemTypeSmall.HandH: handH++; break;
                            case ItemTypeSmall.BackH: backH++; break;
                            case ItemTypeSmall.FootH: footH++; break;
                            case ItemTypeSmall.HeadL: headL++; break;
                            case ItemTypeSmall.BodyL: bodyL++; break;
                            case ItemTypeSmall.HandL: handL++; break;
                            case ItemTypeSmall.BackL: backL++; break;
                            case ItemTypeSmall.FootL: footL++; break;
                        }
                    }
                }
                filter_typeSmall_allText.text = "全部[" + (headH + bodyH+ handH+ backH+ footH+ headL+ bodyL+handL+ backL+ footL) + "]";
                filter_typeSmall_allBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_allBtn.onClick.RemoveAllListeners();
                filter_typeSmall_allBtn.onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.None); });

                filter_typeSmall_btnList[0].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[0].text = "头(重)[" + headH + "]";
                filter_typeSmall_btnList[0].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[0].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.HeadH); });

                filter_typeSmall_btnList[1].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[1].text = "身(重)[" + bodyH + "]";
                filter_typeSmall_btnList[1].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[1].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.BodyH); });

                filter_typeSmall_btnList[2].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[2].text = "手(重)[" + handH + "]";
                filter_typeSmall_btnList[2].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[2].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.HandH); });

                filter_typeSmall_btnList[3].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[3].text = "背(重)[" + backH + "]";
                filter_typeSmall_btnList[3].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[3].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.BackH); });

                filter_typeSmall_btnList[4].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[4].text = "腿(重)[" + footH + "]";
                filter_typeSmall_btnList[4].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[4].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.FootH); });

                filter_typeSmall_btnList[5].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[5].text = "头(轻)[" + headL + "]";
                filter_typeSmall_btnList[5].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[5].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.HeadL); });

                filter_typeSmall_btnList[6].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[6].text = "身(轻)[" + bodyL + "]";
                filter_typeSmall_btnList[6].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[6].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.BodyL); });

                filter_typeSmall_btnList[7].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[7].text = "手(轻)[" + handL + "]";
                filter_typeSmall_btnList[7].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[7].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.HandL); });

                filter_typeSmall_btnList[8].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[8].text = "背(轻)[" + backL + "]";
                filter_typeSmall_btnList[8].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[8].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.BackL); });

                filter_typeSmall_btnList[9].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[9].text = "腿(轻)[" + footL + "]";
                filter_typeSmall_btnList[9].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[9].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.FootL); });

                for (byte i = 10; i < 13; i++)
                {
                    filter_typeSmall_btnList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                break;
            case ItemTypeBig.Jewelry:
                int neck = 0;
                int finger = 0;
                foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
                {
                    if (kvp.Value.districtID == districtID && kvp.Value.isGoods && DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == itemTypeBig)
                    {
                        switch (DataManager.mItemDict[kvp.Value.prototypeID].TypeSmall)
                        {
                            case ItemTypeSmall.Neck: neck++; break;
                            case ItemTypeSmall.Finger: finger++; break;
                        }
                    }
                }
                filter_typeSmall_allText.text = "全部[" + (neck + finger) + "]";
                filter_typeSmall_allBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_allBtn.onClick.RemoveAllListeners();
                filter_typeSmall_allBtn.onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.None); });

                filter_typeSmall_btnList[0].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[0].text = "项链[" + neck + "]";
                filter_typeSmall_btnList[0].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[0].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Neck); });

                filter_typeSmall_btnList[1].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[1].text = "戒指[" + finger + "]";
                filter_typeSmall_btnList[1].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[1].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.Finger); });


                for (byte i = 3; i < 13; i++)
                {
                    filter_typeSmall_btnList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
                }
                break;
            case ItemTypeBig.SkillRoll:
                int none = 0;
                int windI = 0;
                int fireI = 0;
                int waterI = 0;
                int groundI = 0;
                int lightI = 0;
                int darkI = 0;
                int windII = 0;
                int fireII = 0;
                int waterII = 0;
                int groundII = 0;
                int lightII = 0;
                int darkII = 0;
                foreach (KeyValuePair<int, SkillObject> kvp in gc.skillDic)
                {
                    if (kvp.Value.districtID == districtID && kvp.Value.isGoods)
                    {
                        switch (DataManager.mSkillDict[kvp.Value.prototypeID].TypeSmall)
                        {
                            case ItemTypeSmall.ScrollWindI: windI++; break;
                            case ItemTypeSmall.ScrollFireI: fireI++; break;
                            case ItemTypeSmall.ScrollWaterI: waterI++; break;
                            case ItemTypeSmall.ScrollGroundI: groundI++; break;
                            case ItemTypeSmall.ScrollLightI: lightI++; break;
                            case ItemTypeSmall.ScrollDarkI: darkI++; break;
                            case ItemTypeSmall.ScrollNone: none++; break;
                            case ItemTypeSmall.ScrollWindII: windII++; break;
                            case ItemTypeSmall.ScrollFireII: fireII++; break;
                            case ItemTypeSmall.ScrollWaterII: waterII++; break;
                            case ItemTypeSmall.ScrollGroundII: groundII++; break;
                            case ItemTypeSmall.ScrollLightII: lightII++; break;
                            case ItemTypeSmall.ScrollDarkII: darkII++; break;

                        }

                    }
                }
                filter_typeSmall_allText.text = "全部[" + (none + windI+ fireI+ waterI+ groundI+ lightI+ darkI+ windII+ fireII+ waterII+ groundII+ lightII+ darkII) + "]";
                filter_typeSmall_allBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_allBtn.onClick.RemoveAllListeners();
                filter_typeSmall_allBtn.onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.None); });

                filter_typeSmall_btnList[0].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[0].text = "无[" + none + "]";
                filter_typeSmall_btnList[0].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[0].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollNone); });

                filter_typeSmall_btnList[1].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[1].text = "风[" + windI + "]";
                filter_typeSmall_btnList[1].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[1].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollWindI); });

                filter_typeSmall_btnList[2].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[2].text = "火[" + fireI + "]";
                filter_typeSmall_btnList[2].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[2].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollFireI); });

                filter_typeSmall_btnList[3].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[3].text = "水[" + waterI + "]";
                filter_typeSmall_btnList[3].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[3].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollWaterI); });

                filter_typeSmall_btnList[4].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[4].text = "地[" + groundI + "]";
                filter_typeSmall_btnList[4].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[4].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollGroundI); });

                filter_typeSmall_btnList[5].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[5].text = "光[" + lightI + "]";
                filter_typeSmall_btnList[5].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[5].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollLightI); });

                filter_typeSmall_btnList[6].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[6].text = "暗[" + darkI + "]";
                filter_typeSmall_btnList[6].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[6].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollDarkI); });

                filter_typeSmall_btnList[7].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[7].text = "雷[" + windII + "]";
                filter_typeSmall_btnList[7].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[7].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollWindII); });

                filter_typeSmall_btnList[8].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[8].text = "爆炸[" + fireII + "]";
                filter_typeSmall_btnList[8].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[8].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollFireII); });

                filter_typeSmall_btnList[9].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[9].text = "冰[" + waterII + "]";
                filter_typeSmall_btnList[9].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[9].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollWaterII); });

                filter_typeSmall_btnList[10].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[10].text = "自然[" + groundII + "]";
                filter_typeSmall_btnList[10].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[10].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollGroundII); });

                filter_typeSmall_btnList[11].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[11].text = "时空[" + lightII + "]";
                filter_typeSmall_btnList[11].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[11].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollLightII); });

                filter_typeSmall_btnList[12].GetComponent<RectTransform>().localScale = Vector2.one;
                filter_typeSmall_textList[12].text = "死亡[" + darkII + "]";
                filter_typeSmall_btnList[12].onClick.RemoveAllListeners();
                filter_typeSmall_btnList[12].onClick.AddListener(delegate () { UpdateList(districtID, itemTypeBig, ItemTypeSmall.ScrollDarkII); });
      
                break;

        }
    }

   

    public void UpdateList(short districtID, ItemTypeBig itemTypeBig,ItemTypeSmall itemTypeSmall)
    {
        

        List<ItemObject> itemObjects = new List<ItemObject>();

        if (itemTypeBig != ItemTypeBig.SkillRoll)
        {
            foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
            {
                if (kvp.Value.districtID == districtID && kvp.Value.isGoods)
                {
                    if (itemTypeBig != ItemTypeBig.None)//指定了大类
                    {
                        if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == itemTypeBig)
                        {

                            if (itemTypeSmall != ItemTypeSmall.None)//指定了小类
                            {
                                if (DataManager.mItemDict[kvp.Value.prototypeID].TypeSmall == itemTypeSmall)
                                {
                                    itemObjects.Add(kvp.Value);
                                }
                            }
                            else
                            {
                                itemObjects.Add(kvp.Value);
                            }
                               
                        }
                    }
                    else
                    {
                        itemObjects.Add(kvp.Value);
                    }
                }
            }
        }

        List<SkillObject> skillObjects = new List<SkillObject>();
        if (itemTypeBig == ItemTypeBig.None || itemTypeBig == ItemTypeBig.SkillRoll)
        {
            foreach (KeyValuePair<int, SkillObject> kvp in gc.skillDic)
            {
                if (kvp.Value.districtID == districtID && kvp.Value.isGoods)
                {
                    if (itemTypeSmall != ItemTypeSmall.None)//指定了小类
                    {

                        if (DataManager.mSkillDict[kvp.Value.prototypeID].TypeSmall == itemTypeSmall)
                        {
                            skillObjects.Add(kvp.Value);
                        }
                    }
                    else
                    {
                        skillObjects.Add(kvp.Value);
                    }
                }
            }
        }



        GameObject go;

        for (int i = 0; i < itemObjects.Count; i++)
        {
            if (i < itemSkillGoPool.Count)
            {
                go = itemSkillGoPool[i];
                itemSkillGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Goods")) as GameObject;
                go.transform.SetParent(itemListGo.transform);
                itemSkillGoPool.Add(go);
            }

            int row = i == 0 ? 0 : (i % 5);
            int col = i == 0 ? 0 : (i / 5);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 100f, -4 + col * -22f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/ItemPic/" + itemObjects[i].pic);
            go.transform.GetChild(1).GetComponent<Text>().text = "<color=#" + gc.OutputItemRankColorString(itemObjects[i].rank) + ">" +DataManager.mItemDict[ itemObjects[i].prototypeID].Name + "</color>";

        }
        for (int i = itemObjects.Count; i < itemObjects.Count+skillObjects.Count; i++)
        {
            if (i < itemSkillGoPool.Count)
            {
                go = itemSkillGoPool[i];
                itemSkillGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Goods")) as GameObject;
                go.transform.SetParent(itemListGo.transform);
                itemSkillGoPool.Add(go);
            }

            int row = i == 0 ? 0 : (i % 5);
            int col = i == 0 ? 0 : (i / 5);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 100f, -4 + col * -22f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/SkillPic/" +DataManager.mSkillDict[ skillObjects[i].prototypeID].Pic);
            go.transform.GetChild(1).GetComponent<Text>().text = DataManager.mSkillDict[skillObjects[i].prototypeID].Name + "卷";

        }

        for (int i = itemObjects.Count + skillObjects.Count; i < itemSkillGoPool.Count; i++)
        {
            itemSkillGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        itemListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(505f, Mathf.Max(425f, 4 + (itemObjects.Count / 5) * 22f));
    }
}
