using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemListAndInfoPanel : BasePanel
{
    public static ItemListAndInfoPanel Instance;

    GameControl gc;

    public RectTransform goRt;
    public Text titleText;

    public RectTransform listRt;
    public Text numText;
    public GameObject itemListGo;

    public Image info_picText;
    public Text info_nameText;
    public Text info_desText;

    public Text tipText;
    public List<Button> funcBtn;

    public Button closeBtn;

    List<GameObject> itemGo=new List<GameObject>();

    public int nowItemID = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }
    public void OnShow(List<ItemObject> itemObjects, int x, int y)
    {
        UpdateAllInfo(itemObjects, 2);
        SetAnchoredPosition(x, y);
    }
    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
    }
    public void UpdateAllInfo(List<ItemObject> itemObjects,byte col)
    {
        UpdateList(itemObjects,col);
        UpdateInfo(null);
    }

    public void UpdateList(List<ItemObject> itemObjects, byte columns)
    {
        if (columns == 2)
        {
            goRt.sizeDelta = new Vector2(766f, 536f);
            listRt.sizeDelta = new Vector2(470f, 450f);
        }
        else if (columns == 1)
        {
            goRt.sizeDelta = new Vector2(766f - 232f, 536f);
            listRt.sizeDelta = new Vector2(470f - 232f, 450f);
        }

        //Debug.Log("前itemObjects.Count=" + itemObjects.Count + " itemGo.Count=" + itemGo.Count);
        GameObject go;
        for (int i = 0; i < itemObjects.Count; i++)
        {
            if (i < itemGo.Count)
            {
                go = itemGo[i];
                itemGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Item")) as GameObject;
                go.transform.SetParent(itemListGo.transform);
                itemGo.Add(go);
            }
            
            int row = i == 0 ? 0 : (i % columns);
            int col = i == 0 ? 0 : (i / columns);
           // Debug.Log("count=" + count + " row=" + row + " col=" + col);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 224f, -4 + col * -22f, 0f);

           // go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, -4 + i * -22f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/ItemPic/" + itemObjects[i].pic);
            go.transform.GetChild(1).GetComponent<Text>().text = itemObjects[i].name;
            go.transform.GetComponent<InteractiveLabel>().index = itemObjects[i].objectID;
        }
        for (int i = itemObjects.Count; i < itemGo.Count; i++)
        {
            itemGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
       // Debug.Log("后itemObjects.Count=" + itemObjects.Count + " itemGo.Count=" + itemGo.Count);
        itemListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(425f, 4 + (itemObjects.Count / columns) * 22f));
    }


    public void UpdateInfo(ItemObject itemObject)
    {
        if (itemObject == null)
        {
            ClearInfo();
            return;
        }

        info_picText.sprite = Resources.Load<Sprite>("Image/ItemPic/" + itemObject.pic);
        info_nameText.text = itemObject.name+"\n"+gc.OutputSignStr("★", itemObject.rank);
        string str = "";


        switch (DataManager.mItemDict[itemObject.prototypeID].TypeSmall)
        {
            case ItemTypeSmall.Sword: str += "武器（剑）"; break;
            case ItemTypeSmall.Axe: str += "武器（斧、镰刀）"; break;
            case ItemTypeSmall.Spear: str += "武器（枪、矛）"; break;
            case ItemTypeSmall.Hammer: str += "武器（锤、棍棒）"; break;
            case ItemTypeSmall.Bow: str += "武器（弓）"; break;
            case ItemTypeSmall.Staff: str += "武器（杖）"; break;
            case ItemTypeSmall.HeadH: str += "头部装备（重型）"; break;
            case ItemTypeSmall.HeadL: str += "头部装备（轻型）"; break;
            case ItemTypeSmall.BodyH: str += "身体装备（重型）"; break;
            case ItemTypeSmall.BodyL: str += "身体装备（轻型）"; break;
            case ItemTypeSmall.HandH: str += "手部装备（重型）"; break;
            case ItemTypeSmall.HandL: str += "手部装备（轻型）"; break;
            case ItemTypeSmall.BackH: str += "背部装备（重型）"; break;
            case ItemTypeSmall.BackL: str += "背部装备（轻型）"; break;
            case ItemTypeSmall.FootH: str += "腿部装备（重型）"; break;
            case ItemTypeSmall.FootL: str += "腿部装备（轻型）"; break;
            case ItemTypeSmall.Neck: str += "项链"; break;
            case ItemTypeSmall.Finger: str += "戒指"; break;
            case ItemTypeSmall.Shield: str += "副手装备（盾）"; break;
            case ItemTypeSmall.Dorlach: str += "副手装备（箭袋）"; break;

        }

    

        string strBasic = "";
        string strLemma = "";
        string strBasicFirst = "";
        List<int> atkValue = new List<int> { 0, 0, 0, 0, 0 };
        Debug.Log(" itemObject.attr.Count=" + itemObject.attr.Count);
        for (int i = 0; i < itemObject.attr.Count; i++)
        {

            if (itemObject.attr[i].attrS == AttributeSource.Basic)
            {
                //Debug.Log(" itemObject.attr[i].attr="+ itemObject.attr[i].attr);
                if (DataManager.mItemDict[itemObject.prototypeID].TypeBig == ItemTypeBig.Weapon)
                {
                    switch (itemObject.attr[i].attr)
                    {
                        case Attribute.AtkMin: atkValue[0] = itemObject.attr[i].value; break;
                        case Attribute.AtkMax: atkValue[1] = itemObject.attr[i].value; break;
                        case Attribute.MAtkMin: atkValue[2] = itemObject.attr[i].value; break;
                        case Attribute.MAtkMax: atkValue[3] = itemObject.attr[i].value; break;
                        case Attribute.Spd: atkValue[4] = itemObject.attr[i].value; break;
                        default: strBasic += GetAttrLineStr(itemObject.attr[i].attr, itemObject.attr[i].value) + "\n"; break;
                    }
                }
                else if (DataManager.mItemDict[itemObject.prototypeID].TypeBig == ItemTypeBig.Armor)
                {
                    switch (itemObject.attr[i].attr)
                    {
                        case Attribute.Def: atkValue[0] = itemObject.attr[i].value; break;
                        case Attribute.MDef: atkValue[1] = itemObject.attr[i].value; break;
                        default: strBasic += GetAttrLineStr(itemObject.attr[i].attr, itemObject.attr[i].value) + "\n"; break;
                    }
                }
                else
                { 
                    strBasic += GetAttrLineStr(itemObject.attr[i].attr, itemObject.attr[i].value) + "\n"; 
                }



            }
            else if (itemObject.attr[i].attrS == AttributeSource.LemmaAdd)
            {
               // Debug.Log(" LemmaAdd");
                strLemma += GetAttrLineStr(itemObject.attr[i].attr, itemObject.attr[i].value)  + "\n";
            }
        }


        if (DataManager.mItemDict[itemObject.prototypeID].TypeBig == ItemTypeBig.Weapon)
        {
            if (atkValue[0] != 0 && atkValue[1] != 0)
            {
                strBasicFirst += "物理攻击 " + atkValue[0] + " - " + atkValue[1] + "\n";
            }
            if (atkValue[2] != 0 && atkValue[3] != 0)
            {
                strBasicFirst += "魔法攻击 " + atkValue[2] + " - " + atkValue[3] + "\n";
            }
            if (atkValue[4] != 0 )
            {
                strBasicFirst += "攻击速度 " + atkValue[4] + "\n";
            }
        }
        else if (DataManager.mItemDict[itemObject.prototypeID].TypeBig == ItemTypeBig.Armor)
        {
            if (atkValue[0] != 0)
            {
                strBasicFirst += "物理防御 " + atkValue[0] + "\n";
            }
            if (atkValue[1] != 0)
            {
                strBasicFirst += "魔法防御 " + atkValue[1]+ "\n";
            }
        }

        strBasic = strBasicFirst + strBasic+ "\n<color=#53C2FF>" + strLemma+"</color>";
        str += "\n" + strBasic + "----------------------------------\n" + itemObject.des + "\n价值 "+ itemObject.cost;

        info_desText.text = str;
    }

    public void ClearInfo()
    {
        info_picText.sprite = Resources.Load<Sprite>("Image/Empty");
        info_nameText.text = "";
        info_desText.text = "";
    }

    string GetAttrLineStr(Attribute attribute,int value)
    {
        string str = "";
        string strValue;
        if (value > 0)
        {
            strValue = " +" + value;
        }
        else
        {
            strValue = " " + value;
        }

        switch (attribute)
        {
            case Attribute.Hp: str = "体力上限" + strValue; break;
            case Attribute.Mp: str = "魔力上限" + strValue; break;
            case Attribute.HpRenew: str = "体力恢复" + strValue+"%"; break;
            case Attribute.MpRenew: str = "魔力恢复" + strValue+"%"; break;
            case Attribute.AtkMin: str = "最小物攻" + strValue; break;
            case Attribute.AtkMax: str = "最大物攻" + strValue;break;
            case Attribute.MAtkMin: str = "最小魔攻" + strValue;break;
            case Attribute.MAtkMax: str = "最大魔攻" + strValue;break;
            case Attribute.Def: str = "物防" + strValue;break;
            case Attribute.MDef: str = "魔防" + strValue;break;
            case Attribute.Hit: str = "命中" + strValue;break;
            case Attribute.Dod: str = "闪避" + strValue;break;
            case Attribute.CriR: str = "暴击" + strValue;break;
            case Attribute.CriD: str = "暴击伤害" + strValue+"%";break;
            case Attribute.Spd: str = "速度" + strValue;break;
            case Attribute.WindDam: str = "风系伤害" + strValue + "%";break;
            case Attribute.FireDam: str = "火系伤害" + strValue + "%";break;
            case Attribute.WaterDam: str = "水系伤害" + strValue + "%";break;
            case Attribute.GroundDam: str = "地系伤害" + strValue + "%";break;
            case Attribute.LightDam: str = "光系伤害" + strValue + "%";break;
            case Attribute.DarkDam: str = "暗系伤害" + strValue + "%";break;
            case Attribute.WindRes: str = "风系抗性" + strValue + "%";break;
            case Attribute.FireRes: str = "火系抗性" + strValue + "%";break;
            case Attribute.WaterRes: str = "水系抗性" + strValue + "%";break;
            case Attribute.GroundRes: str = "地系抗性" + strValue + "%";break;
            case Attribute.LightRes: str = "光系抗性" + strValue + "%";break;
            case Attribute.DarkRes: str = "暗系抗性" + strValue + "%";break;
            case Attribute.DizzyRes: str = "眩晕抗性" + strValue + "%";break;
            case Attribute.ConfusionRes: str = "混乱抗性" + strValue + "%";break;
            case Attribute.PoisonRes: str = "中毒抗性" + strValue + "%";break;
            case Attribute.SleepRes: str = "睡眠抗性" + strValue + "%";break;
            case Attribute.GoldGet: str = "金币加成" + strValue + "%";break;
            case Attribute.ExpGet: str = "经验值加成" + strValue + "%";break;
            case Attribute.ItemGet: str = "稀有掉落加成" + strValue + "%";break;
            case Attribute.WorkPlanting: str = "种植能力" + strValue;break;
            case Attribute.WorkFeeding: str = "饲养能力" + strValue;break;
            case Attribute.WorkFishing: str = "钓鱼能力" + strValue;break;
            case Attribute.WorkHunting: str = "打猎能力" + strValue;break;
            case Attribute.WorkMining: str = "采石能力" + strValue;break;
            case Attribute.WorkQuarrying: str = "挖矿能力" + strValue;break;
            case Attribute.WorkFelling: str = "伐木能力" + strValue;break;
            case Attribute.WorkBuild: str = "建筑能力" + strValue;break;
            case Attribute.WorkMakeWeapon: str = "武器锻造能力" + strValue;break;
            case Attribute.WorkMakeArmor: str = "防具制作能力" + strValue;break;
            case Attribute.WorkMakeJewelry: str = "饰品制作能力" + strValue;break;
            case Attribute.WorkSundry: str = "打杂能力" + strValue;break;
            default: str = "未定义类型";break;
        }
        if (value < 0)
        {
            str = "<color=#FF554F>" + str+"</color>";
        }
        return str;
    }

}
