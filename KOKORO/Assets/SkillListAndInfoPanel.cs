using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillListAndInfoPanel : BasePanel
{
    public static SkillListAndInfoPanel Instance;

    GameControl gc;

    public Text titleText;
    public Text numText;
    public RectTransform list_nowRt;
    public Text list_nowTitleText;
    public Button list_nowUseBtn;
    public Image list_nowPicImage;
    public Text list_nowNameText;
    public Button list_nowEmptyBtn;

    public RectTransform list_filterRt;
    public Button list_filterAllBtn;
    public Button list_filterNoneBtn;
    public Button list_filterWindBtn;
    public Button list_filterFireBtn;
    public Button list_filterWaterBtn;
    public Button list_filterGroundBtn;
    public Button list_filterLightBtn;
    public Button list_filterDarkBtn;
    public Button list_filterThunderBtn;
    public Button list_filterExplodeBtn;
    public Button list_filterIceBtn;
    public Button list_filterNaturalBtn;
    public Button list_filterSpaceBtn;
    public Button list_filterDeathBtn;

    public RectTransform list_scrollViewRt;
    public GameObject list_skillListGo;

    public Image info_picImage;
    public Text info_nameText;
    public Text info_desText;

    public Text tipText;
    public List<Button> funcBtn;

    public Button closeBtn;

    List<GameObject> skillGo = new List<GameObject>();

    public int nowSkillID = -1;
    public short nowDistrictID = -1;
    public int nowHeroID = -1;
    public short nowHeroSkillIndex = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        list_filterAllBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, null, nowHeroID, nowHeroSkillIndex); });
        list_filterNoneBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 0 }, nowHeroID, nowHeroSkillIndex); });
        list_filterWindBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 1 }, nowHeroID, nowHeroSkillIndex); });
        list_filterFireBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 2 }, nowHeroID, nowHeroSkillIndex); });
        list_filterWaterBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 3 }, nowHeroID, nowHeroSkillIndex); });
        list_filterGroundBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 4 }, nowHeroID, nowHeroSkillIndex); });
        list_filterLightBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 5 }, nowHeroID, nowHeroSkillIndex); });
        list_filterDarkBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 6 }, nowHeroID, nowHeroSkillIndex); });
        list_filterThunderBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 1,5 }, nowHeroID, nowHeroSkillIndex); });
        list_filterExplodeBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 2,4 }, nowHeroID, nowHeroSkillIndex); });
        list_filterIceBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 3,6 }, nowHeroID, nowHeroSkillIndex); });
        list_filterNaturalBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 4,3 }, nowHeroID, nowHeroSkillIndex); });
        list_filterSpaceBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 5,1 }, nowHeroID, nowHeroSkillIndex); });
        list_filterDeathBtn.onClick.AddListener(delegate () { UpdateAllInfo(nowDistrictID, new List<int> { 6,2 }, nowHeroID, nowHeroSkillIndex); });
        closeBtn.onClick.AddListener(delegate () { OnHide(); });

    }
    public void OnShow(short districtID, List<int> element, int heroID, byte heroSkillIndex, int x, int y)
    {

        funcBtn[0].GetComponent<RectTransform>().localScale = Vector2.one;
        funcBtn[0].GetComponent<Image>().color = new Color(229 / 255f, 181 / 255f, 105 / 255f, 255 / 255f);
        funcBtn[0].transform.GetChild(0).GetComponent<Text>().text = "装备";
        funcBtn[0].onClick.RemoveAllListeners();
        funcBtn[0].onClick.AddListener(delegate () {
            gc.HeroSkillSet(heroID, heroSkillIndex, nowSkillID);
        });
        HideFuncBtn(3);

        UpdateAllInfo(districtID, element, heroID, heroSkillIndex);
        SetAnchoredPosition(x - 200, y);

        nowDistrictID = districtID;
        nowHeroID = heroID;
        nowHeroSkillIndex = heroSkillIndex;
        isShow = true;
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }

    public void HideFuncBtn(int count)
    {
        for (int i = funcBtn.Count - 1; i >= funcBtn.Count - count; i--)
        {
            funcBtn[i].GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    public void UpdateAllInfo(short districtID, List<int> element, int heroID, short heroSkillIndex)
    {
        UpdateList(districtID, element, heroID, heroSkillIndex);
        if (heroID != -1)
        {
            UpdateInfo(gc.heroDic[heroID].skill[heroSkillIndex]);
        }
        else
        {
            //ClearInfo();
        }
        // UpdateInfo(null);
    }
    //element==null 查全部
    public void UpdateList(short districtID, List<int> element,int heroID,short heroSkillIndex)
    {
        Debug.Log("gc.skillDic.Count" + gc.skillDic.Count);
        

        if (heroID != -1)
        {
            list_nowRt.localScale = Vector2.one;
            list_filterRt.anchoredPosition = new Vector2(8f, -48f);
            list_scrollViewRt.sizeDelta = new Vector2(462f, 374f);

            list_nowTitleText.text = "当前["+ (heroSkillIndex+1) + "]";
            if (gc.heroDic[heroID].skill[heroSkillIndex] == -1)//未配置技能
            {
                list_nowPicImage.overrideSprite = Resources.Load<Sprite>("Image/Other/icon007") as Sprite;
                list_nowNameText.text = "<普通攻击>";
                list_nowUseBtn.GetComponent<InteractiveLabel>().index = -1;

                list_nowEmptyBtn.onClick.RemoveAllListeners();

            }
            else
            {
                list_nowPicImage.overrideSprite = Resources.Load("Image/Skill/"+DataManager.mSkillDict[ gc.skillDic[ gc.heroDic[heroID].skill[heroSkillIndex]].prototypeID].Pic, typeof(Sprite)) as Sprite;
                list_nowNameText.text = gc.skillDic[gc.heroDic[heroID].skill[heroSkillIndex]].name+"之卷";
                list_nowUseBtn.GetComponent<InteractiveLabel>().index = gc.heroDic[heroID].skill[heroSkillIndex];
            }

        }
        else
        {
            list_nowRt.localScale = Vector2.zero;
            list_filterRt.anchoredPosition = new Vector2(8f, -24f);
            list_scrollViewRt.sizeDelta = new Vector2(462f, 398f);
        }


        List<SkillObject> skillObjects = new List<SkillObject>();

        foreach (KeyValuePair<int, SkillObject> kvp in gc.skillDic)
        {
            if (kvp.Value.districtID == districtID && kvp.Value.heroID == -1)
            {
                if (element != null)
                {
                    bool match = true;
                    for (int i = 0; i < element.Count; i++)
                    {
                        if (!DataManager.mSkillDict[kvp.Value.prototypeID].Element.Contains(element[i]))
                        {
                            match = false;
                        }
                    }
                    if (match)
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

        GameObject go;
        for (int i = 0; i < skillObjects.Count; i++)
        {
            if (i < skillGo.Count)
            {
                go = skillGo[i];
                skillGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Item")) as GameObject;
                go.transform.SetParent(list_skillListGo.transform);
                skillGo.Add(go);
            }

            int row = i == 0 ? 0 : (i % 2);
            int col = i == 0 ? 0 : (i / 2);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 224f, -4 + col * -22f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/SkillPic/" +DataManager.mSkillDict[ skillObjects[i].prototypeID].Pic);
            go.transform.GetChild(1).GetComponent<Text>().text = skillObjects[i].name ;
            go.transform.GetComponent<InteractiveLabel>().labelType = LabelType.Skill;
            go.transform.GetComponent<InteractiveLabel>().index = skillObjects[i].id;
        }
        for (int i = skillObjects.Count; i < skillGo.Count; i++)
        {
            skillGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }

        list_skillListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(425f, 4 + (skillObjects.Count / 2) * 22f));
        if (districtID != -1)
        {
            numText.text = skillObjects.Count + "/" + gc.districtDic[districtID].rRollLimit;
        }
        else
        {
            numText.text = skillObjects.Count.ToString();
        }

    }

    public void UpdateInfo(int skillID)
    {
        if (skillID == -1)
        {
            info_picImage.overrideSprite = Resources.Load<Sprite>("Image/Other/icon007");
            info_nameText.text = "普通攻击";
            info_desText.text = "物理攻击影响 100%\n造成无元素属性伤害\n──────────────\n使用武器对单个敌人进行普通攻击";
        }
        else
        {
            SkillObject so = gc.skillDic[skillID];
            SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];
            info_picImage.overrideSprite= Resources.Load<Sprite>("Image/SkillPic/" + sp.Pic);
            info_nameText.text = so.name+"之卷";
            string str = "";

            if (sp.Element.Count == 1)
            {
                str += "基础技能卷轴";
            }
            else if (sp.Element.Count == 2)
            {
                str += "进阶技能卷轴";
            }

            if (sp.Element[0] != 0)
            {
                str += "·";
            }
            for (int i = 0; i < sp.Element.Count; i++)
            {
                switch (sp.Element[i])
                {
                    case 0: str += ""; break;
                    case 1: str += "<color=#26F39A>风</color> "; break;
                    case 2: str += "<color=#E74624>火</color> "; break;
                    case 3: str += "<color=#24CDE7>水</color> "; break;
                    case 4: str += "<color=#C08342>地</color> "; break;
                    case 5: str += "<color=#E0DE60>光</color> "; break;
                    case 6: str += "<color=#DA7CFF>暗</color> "; break;
                }
            }
            
            
            str += "\n发动几率 "+sp.Probability+ "%\n消耗魔力 " + sp.Mp+ "\n最大目标 " + sp.Max+"\n";

            if (sp.FlagDamage)
            {
                if (sp.Atk != 0) { str += "\n物理攻击影响 " + sp.Atk + "%"; }
                if (sp.MAtk != 0) { str += "\n魔法攻击影响 " + sp.Atk + "%"; }
                if (sp.Sword != 0) { str += "\n剑类武器增加" + sp.Atk + "%伤害"; }
                if (sp.Axe != 0) { str += "\n斧、镰类武器增加" + sp.Atk + "%伤害"; }
                if (sp.Spear != 0) { str += "\n枪、矛类武器增加" + sp.Atk + "%伤害"; }
                if (sp.Hammer != 0) { str += "\n锤、棍类武器增加" + sp.Atk + "%伤害"; }
                if (sp.Bow != 0) { str += "\n弓类武器增加" + sp.Atk + "%伤害"; }
                if (sp.Staff != 0) { str += "\n杖类武器增加" + sp.Atk + "%伤害"; }
                if (sp.Wind != 0) { str += "\n<color=#26F39A>造成风系伤害</color>（风系伤害+" + sp.Atk + "%）"; }
                if (sp.Fire != 0) { str += "\n<color=#E74624>造成火系伤害</color>（火系伤害+" + sp.Atk + "%）"; }
                if (sp.Water!= 0) { str += "\n<color=#24CDE7>造成水系伤害</color>（水系伤害+" + sp.Atk + "%）"; }
                if (sp.Ground != 0) { str += "\n<color=#C08342>造成地系伤害</color>（地系伤害+" + sp.Atk + "%）"; }
                if (sp.Light != 0) { str += "\n<color=#E0DE60>造成光系伤害</color>（光系伤害+" + sp.Atk + "%）"; }
                if (sp.Dark != 0) { str += "\n<color=#DA7CFF>造成暗系伤害</color>（暗系伤害+" + sp.Atk + "%）"; }
            }
            if (sp.FlagDebuff)
            {
                if (sp.Dizzy != 0) { str += "\n"+ sp.Dizzy + "%几率触发眩晕效果，持续" + sp.DizzyValue + "回合"; }
                if (sp.Confusion != 0) { str += "\n" + sp.Dizzy + "%几率触发混乱效果，持续" + sp.ConfusionValue + "回合"; }
                if (sp.Poison != 0) { str += "\n" + sp.Dizzy + "%几率触发中毒效果，持续" + sp.PoisonValue + "回合"; }
                if (sp.Sleep != 0) { str += "\n"+ sp.Dizzy + "%几率触发睡眠效果，持续" + sp.SleepValue + "回合"; }
            }
            if (sp.Cure != 0) { str += "\n恢复目标生命值" + sp.DizzyValue + "%"; }
            if (sp.FlagBuff)
            {
                if (sp.UpAtk != 0) { str += "\n提示目标物理攻击" + sp.UpAtk + "%，持续2回合"; }
                if (sp.UpMAtk != 0) { str += "\n提示目标魔法攻击" + sp.UpMAtk + "%，持续2回合"; }
                if (sp.UpDef != 0) { str += "\n提示目标物理防御" + sp.UpDef + "%，持续2回合"; }
                if (sp.UpMDef != 0) { str += "\n提示目标魔法防御" + sp.UpMDef + "%，持续2回合"; }
                if (sp.UpHit != 0) { str += "\n提示目标命中" + sp.UpHit + "%，持续2回合"; }
                if (sp.UpDod != 0) { str += "\n提示目标闪避" + sp.UpDod + "%，持续2回合"; }
                if (sp.UpCriD != 0) { str += "\n提示目标暴击伤害" + sp.UpCriD + "%，持续2回合"; }
                if (sp.UpWindDam != 0) { str += "\n提示目标风系伤害" + sp.UpWindDam + "%，持续2回合"; }
                if (sp.UpFireDam != 0) { str += "\n提示目标火系伤害" + sp.UpFireDam + "%，持续2回合"; }
                if (sp.UpWaterDam != 0) { str += "\n提示目标水系伤害" + sp.UpWaterDam + "%，持续2回合"; }
                if (sp.UpGroundDam != 0) { str += "\n提示目标地系伤害" + sp.UpGroundDam + "%，持续2回合"; }
                if (sp.UpLightDam != 0) { str += "\n提示目标光系伤害" + sp.UpLightDam + "%，持续2回合"; }
                if (sp.UpDarkDam != 0) { str += "\n提示目标暗系伤害" + sp.UpDarkDam + "%，持续2回合"; }
                if (sp.UpWindRes != 0) { str += "\n提示目标风系抗性" + sp.UpWindRes + "%，持续2回合"; }
                if (sp.UpFireRes != 0) { str += "\n提示目标火系抗性" + sp.UpFireRes + "%，持续2回合"; }
                if (sp.UpWaterRes != 0) { str += "\n提示目标水系抗性" + sp.UpWaterRes + "%，持续2回合"; }
                if (sp.UpGroundRes != 0) { str += "\n提示目标地系抗性" + sp.UpGroundRes + "%，持续2回合"; }
                if (sp.UpLightRes != 0) { str += "\n提示目标光系抗性" + sp.UpLightRes + "%，持续2回合"; }
                if (sp.UpDarkRes != 0) { str += "\n提示目标暗系抗性" + sp.UpDarkRes + "%，持续2回合"; }
            }

            string strLemma = "";
            if (so.rateModify != 0)
            {
                if (so.rateModify > 0)
                {
                    strLemma += "\n[积极]\n 发动几率提升" + so.rateModify+"%";
                }
                else
                {
                    strLemma += "\n[消极]\n 发动几率降低" + System.Math.Abs(so.rateModify)  + "%";
                }
            }

            if (so.mpModify != 0)
            {
                if (so.mpModify > 0)
                {
                    strLemma += "\n[奢侈]\n 魔力消耗增加" + so.mpModify + "%";
                }
                else
                {
                    strLemma += "\n[节约]\n 魔力消耗减少" + System.Math.Abs(so.mpModify) + "%";
                }
            }

            string cstr = "";
            if (so.comboRate != 0)
            {
                switch (so.comboMax)
                {
                    case 1: strLemma += "\n[二连" ; break;
                    case 2: strLemma += "\n[三连"; break;
                    case 3: strLemma += "\n[四连" ; break;
                    case 4: strLemma += "\n[五连"; break;
                }
                if (so.comboRate < 5)
                {
                    strLemma += "快速]\n" ; cstr = " 很低";
                }
                else if (so.comboRate >= 5 && so.comboRate < 10)
                {
                    strLemma += "迅速]\n" ; cstr = " 较低";
                }
                else if (so.comboRate >= 10 && so.comboRate < 15)
                {
                    strLemma += "急速]\n" ; cstr = " 低";
                }
                else
                {
                    strLemma += "极速]\n" ; cstr = " 一般";
                }
                strLemma += cstr + "概率发动追击，上限"+ so.comboMax + "次";
            }

            if (so.gold != 0)
            {
                strLemma += "\n[夺金]\n 每次攻击在目标身上夺取" + so.gold + "%金币";
            }

            str +=(strLemma!=""?("\n<color=#53C2FF>" + strLemma+"</color>"):"") + "\n──────────────\n";
            str += sp.Des+ "\n价值 "+so.cost;

            info_desText.text = str;
        }
    }

    public void ClearInfo()
    {
        info_picImage.sprite = Resources.Load<Sprite>("Image/Empty");
        info_nameText.text = "";
        info_desText.text = "";
    }
}
