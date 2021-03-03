using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsumableListAndInfoPanel : BasePanel
{
    public static ConsumableListAndInfoPanel Instance;

    GameControl gc;

    #region 【UI控件】
    public RectTransform listRt;
    public GameObject itemListGo;
    public RectTransform list_selectedRt;

    public Image info_picImage;
    public Text info_nameText;
    public Text info_desText;

    public List<Button> funcBtn;
    public Button closeBtn;
    #endregion

    //运行变量
    public int nowItemID = -1;

    //对象池
    List<GameObject> itemGoPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    //主面板显示-查看列表
    public void OnShow(int x, int y)
    {
        nowItemID = -1;
        UpdateSelectedPos(new Vector2(0, 5000));
        GetComponent<RectTransform>().sizeDelta = new Vector2(712f, 520f);
        listRt.localScale = Vector2.one;
        UpdateList();
        ClearInfo();

        funcBtn[0].GetComponent<RectTransform>().localScale = Vector2.one;
        funcBtn[0].GetComponent<Image>().color = new Color(132 / 255f, 236 / 255f, 137 / 255f, 255 / 255f);
        funcBtn[0].transform.GetChild(0).GetComponent<Text>().text = "使用";
        funcBtn[0].onClick.RemoveAllListeners();
        funcBtn[0].onClick.AddListener(delegate () { /**ShowBatch("sale"); */});
        HideFuncBtn(3);

        SetAnchoredPosition(x, y);
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        isShow = true;
    }

    //主面板显示-选择强化装备
    public void OnShowByChoose(string type, int buidingID, int index,int x, int y)
    {
        nowItemID = -1;
        UpdateSelectedPos(new Vector2(0, 5000));
        GetComponent<RectTransform>().sizeDelta = new Vector2(712f, 520f);
        listRt.localScale = Vector2.one;
        UpdateList();
        ClearInfo();

        funcBtn[0].GetComponent<RectTransform>().localScale = Vector2.one;
        funcBtn[0].GetComponent<Image>().color = new Color(132 / 255f, 236 / 255f, 137 / 255f, 255 / 255f);
        funcBtn[0].transform.GetChild(0).GetComponent<Text>().text = "选择";
        funcBtn[0].onClick.RemoveAllListeners();
        funcBtn[0].onClick.AddListener(delegate () {
            if (type == "strengthen")
            {
                BuildingPanel.Instance.strengthenItemID[index] =(short) nowItemID;
                BuildingPanel.Instance.UpdateStrengthenPart(gc.buildingDic[buidingID]);
            }
            else if (type == "inlay")
            {
                BuildingPanel.Instance.inlayItemID[index] = (short)nowItemID;
                BuildingPanel.Instance.UpdateInlayPart(gc.buildingDic[buidingID]);
            }
            OnHide();
        });
        HideFuncBtn(3);

        SetAnchoredPosition(x, y);
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        isShow = true;
    }

    //主面板显示-查看物品详情
    public void OnShow(int itemID, int x, int y)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(236f, 520f);
        listRt.localScale = Vector2.zero;
        UpdateInfo(itemID);

        HideFuncBtn(4);

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

    //功能按钮组-隐藏（指定个数）
    public void HideFuncBtn(int count)
    {
        for (int i = funcBtn.Count - 1; i >= funcBtn.Count - count; i--)
        {
            funcBtn[i].GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    //物品列表-更新
    public void UpdateList()
    {
        List<int> itemObjects = new List<int>();
        for (int i = 0; i < gc.consumableNum.Count; i++)
        {
            if (gc.consumableNum[i] > 0)
            {
                itemObjects.Add(i);
            }
        }

        GameObject go;
        for (int i = 0; i < itemObjects.Count; i++)
        {
            if (i < itemGoPool.Count)
            {
                go = itemGoPool[i];
                itemGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Consumable")) as GameObject;
                go.transform.SetParent(itemListGo.transform);
                itemGoPool.Add(go);
            }

            int row = i == 0 ? 0 : (i % 9);
            int col = i == 0 ? 0 : (i / 9);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(row * 50f,  col * -50f);
            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[itemObjects[i]].Pic);
            go.transform.GetChild(1).GetComponent<Text>().text = gc.consumableNum[itemObjects[i]].ToString();
            go.transform.GetComponent<InteractiveLabel>().index = itemObjects[i];
            int ID = itemObjects[i];
            go.transform.GetComponent<Button>().onClick.AddListener(delegate () {
                UpdateSelectedPos(new Vector2(row * 50f, col * -50f));
                nowItemID = ID; 
                UpdateInfo(ID); });
        }
        for (int i = itemObjects.Count; i < itemGoPool.Count; i++)
        {
            itemGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        itemListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(455f, Mathf.Max(442f, (itemObjects.Count / 9) * 50f));

    }

    public void UpdateInfo(int itemID)
    {
        if (itemID == -1)
        {
            ClearInfo();
            return;
        }

        info_picImage.sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[itemID].Pic);
        info_nameText.text = "<color=#" + gc.OutputItemRankColorString(DataManager.mConsumableDict[itemID].Rank) + ">" + DataManager.mConsumableDict[itemID].Name + "</color>\n" + 
            gc.OutputSignStr("★", DataManager.mConsumableDict[itemID].Rank)+"\n持有数量 "+gc.consumableNum[itemID];
        string str = "道具（";

        switch (DataManager.mConsumableDict[itemID].Type)
        {
            case ConsumableType.Drug: str += "药水）"; break;
            case ConsumableType.SlotStone: 
                str += "镶嵌品）\n<color=#F3EE89>";
                for (int j = 0; j < DataManager.mConsumableDict[itemID].AttributeType.Count; j++)
                {
                    ItemAttribute itemAttribute = new ItemAttribute(DataManager.mConsumableDict[itemID].AttributeType[j],
                        AttributeSource.SlotAdd,
                        DataManager.mConsumableDict[itemID].SkillID[j],
                        DataManager.mConsumableDict[itemID].SkillAddType[j],
                        DataManager.mConsumableDict[itemID].Value[j]);
                    str += "\n   " + GetAttrLineStr(itemAttribute); 
                }
                str += "</color>";
                break;

        }

        str += "\n──────────────\n" + DataManager.mConsumableDict[itemID].Des + "\n价值 " + DataManager.mConsumableDict[itemID].Cost;
        info_desText.text = str;
    }

    void UpdateSelectedPos(Vector2 pos)
    {
        list_selectedRt.anchoredPosition = pos;
        list_selectedRt.SetAsLastSibling();
    }

    //物品详情-清空
    public void ClearInfo()
    {
        info_picImage.sprite = Resources.Load<Sprite>("Image/Empty");
        info_nameText.text = "";
        info_desText.text = "";
    }

    //物品详情-更新（辅助方法：输出属性行）
    string GetAttrLineStr(ItemAttribute itemAttribute)
    {
        string str;
        string strValue;
        if (itemAttribute.value > 0)
        {
            strValue = " +" + itemAttribute.value;
        }
        else
        {
            strValue = " " + itemAttribute.value;
        }

        switch (itemAttribute.attr)
        {
            case Attribute.Hp: str = "体力上限" + strValue; break;
            case Attribute.Mp: str = "魔力上限" + strValue; break;
            case Attribute.HpRenew: str = "体力恢复" + strValue + "%"; break;
            case Attribute.MpRenew: str = "魔力恢复" + strValue + "%"; break;
            case Attribute.AtkMin: str = "最小物攻" + strValue; break;
            case Attribute.AtkMax: str = "最大物攻" + strValue; break;
            case Attribute.MAtkMin: str = "最小魔攻" + strValue; break;
            case Attribute.MAtkMax: str = "最大魔攻" + strValue; break;
            case Attribute.Def: str = "物防" + strValue; break;
            case Attribute.MDef: str = "魔防" + strValue; break;
            case Attribute.Hit: str = "命中" + strValue; break;
            case Attribute.Dod: str = "闪避" + strValue; break;
            case Attribute.CriR: str = "暴击" + strValue; break;
            case Attribute.CriD: str = "暴击伤害" + strValue + "%"; break;
            case Attribute.Spd: str = "速度" + strValue; break;
            case Attribute.WindDam: str = "风系伤害" + strValue + "%"; break;
            case Attribute.FireDam: str = "火系伤害" + strValue + "%"; break;
            case Attribute.WaterDam: str = "水系伤害" + strValue + "%"; break;
            case Attribute.GroundDam: str = "地系伤害" + strValue + "%"; break;
            case Attribute.LightDam: str = "光系伤害" + strValue + "%"; break;
            case Attribute.DarkDam: str = "暗系伤害" + strValue + "%"; break;
            case Attribute.WindRes: str = "风系抗性" + strValue + "%"; break;
            case Attribute.FireRes: str = "火系抗性" + strValue + "%"; break;
            case Attribute.WaterRes: str = "水系抗性" + strValue + "%"; break;
            case Attribute.GroundRes: str = "地系抗性" + strValue + "%"; break;
            case Attribute.LightRes: str = "光系抗性" + strValue + "%"; break;
            case Attribute.DarkRes: str = "暗系抗性" + strValue + "%"; break;
            case Attribute.DizzyRes: str = "眩晕抗性" + strValue + "%"; break;
            case Attribute.ConfusionRes: str = "混乱抗性" + strValue + "%"; break;
            case Attribute.PoisonRes: str = "中毒抗性" + strValue + "%"; break;
            case Attribute.SleepRes: str = "睡眠抗性" + strValue + "%"; break;
            case Attribute.GoldGet: str = "金币加成" + strValue + "%"; break;
            case Attribute.ExpGet: str = "经验值加成" + strValue + "%"; break;
            case Attribute.ItemGet: str = "稀有掉落加成" + strValue + "%"; break;
            case Attribute.WorkPlanting: str = "种植能力" + strValue; break;
            case Attribute.WorkFeeding: str = "饲养能力" + strValue; break;
            case Attribute.WorkFishing: str = "钓鱼能力" + strValue; break;
            case Attribute.WorkHunting: str = "打猎能力" + strValue; break;
            case Attribute.WorkMining: str = "采石能力" + strValue; break;
            case Attribute.WorkQuarrying: str = "挖矿能力" + strValue; break;
            case Attribute.WorkFelling: str = "伐木能力" + strValue; break;
            case Attribute.WorkBuild: str = "建筑能力" + strValue; break;
            case Attribute.WorkMakeWeapon: str = "武器锻造能力" + strValue; break;
            case Attribute.WorkMakeArmor: str = "防具制作能力" + strValue; break;
            case Attribute.WorkMakeJewelry: str = "饰品制作能力" + strValue; break;
            case Attribute.WorkSundry: str = "打杂能力" + strValue; break;
            case Attribute.Skill:

                if (DataManager.mSkillDict[itemAttribute.skillID].FlagDamage)
                {
                    switch (itemAttribute.skillAddType)
                    {
                        case AttributeSkill.Damage: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的伤害提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.CriDamage: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>造成暴击时伤害提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.Probability: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的触发几率提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.CostMp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>消耗MP减少" + itemAttribute.value + "%"; break;
                        case AttributeSkill.IgnoreDef: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>" + itemAttribute.value + "%几率无视目标物防"; break;
                        case AttributeSkill.IgnoreMDef: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>" + itemAttribute.value + "%几率无视目标魔防"; break;
                        case AttributeSkill.SuckHp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>伤害" + itemAttribute.value + "%转化为体力"; break;
                        case AttributeSkill.SuckMp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>伤害" + itemAttribute.value + "%转化为魔力"; break;
                        case AttributeSkill.TargetNum: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的影响目标数量增加" + itemAttribute.value; break;
                        case AttributeSkill.Invincible: str = "使用<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>后有" + itemAttribute.value + "%几率在下一回合无敌"; break;
                        default: str = "未定义技能类型"; break;
                    }
                }
                else if (DataManager.mSkillDict[itemAttribute.skillID].Cure > 0)
                {
                    switch (itemAttribute.skillAddType)
                    {
                        case AttributeSkill.Damage: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的回复量提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.Probability: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的触发几率提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.CostMp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>消耗MP减少" + itemAttribute.value + "%"; break;
                        case AttributeSkill.Ap: str = "使用<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>后加快行动"; break;
                        case AttributeSkill.Invincible: str = "使用<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>后有" + itemAttribute.value + "%几率在下一回合无敌"; break;
                        default: str = "未定义技能类型"; break;
                    }
                }
                else
                {
                    switch (itemAttribute.skillAddType)
                    {
                        case AttributeSkill.Probability: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的触发几率提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.CostMp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>消耗MP减少" + itemAttribute.value + "%"; break;
                        case AttributeSkill.Invincible: str = "使用<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>后有" + itemAttribute.value + "%几率在下一回合无敌"; break;
                        default: str = "未定义技能类型"; break;
                    }
                }
                break;

            default: str = "未定义类型"; break;
        }
        if (itemAttribute.value < 0)
        {
            str = "<color=#FF554F>" + str + "</color>";
        }
        return str;
    }

}
