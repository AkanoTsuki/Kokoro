using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureTeamPanel : BasePanel
{
    public static AdventureTeamPanel Instance;

    Color colorFight = new Color(186 / 255f, 90 / 255f, 66 / 255f, 1f);
    Color colorTrap = new Color(121 / 255f, 97 / 255f, 91 / 255f, 1f);
    Color colorSpring = new Color(74 / 255f, 132 / 255f, 79 / 255f, 1f);
    Color colorGetSomething = new Color(152 / 255f, 134 / 255f, 54 / 255f, 1f);

    GameControl gc;

    public Text titleText;

   // public Text dungeon_desText;

    //public List<Image> hero_picImage;
    //public List<Text> hero_nameText;
    //public List<Text> hero_hpmpText;

    public Text part_baseText;
    public Image part_baseBgImage;
    public Image part_baseFgImage;

    public Transform part_nowRoleTf;
    public RectTransform part_nowLineRt;
    public Text part_nowText;
    public Text part_nowTitleText;
    public Image part_nowBgImage;
    public Image part_nowFgImage;

    public Text part_lastQuestText;
    public Image part_lastBgImage;
    public Image part_lastFgImage;
   

    public GameObject partListGo;
    public Button closeBtn;


    //对象池
    List<GameObject> adventurePartGoPool = new List<GameObject>();

    public byte nowTeam = 0;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }


    public void OnShow(byte teamID,int x, int y)
    {
        nowTeam = teamID;
        UpdateAllInfo(teamID);
        SetAnchoredPosition(x, y);
        gameObject.SetActive(true);
        isShow = true;
    }

    public override void OnHide()
    {
        //SetAnchoredPosition(0, 5000);
        gameObject.SetActive(false);
        isShow = false;
    }

    public void UpdateAllInfo(byte teamID)
    {
        //titleText.text = "冒险 - 详情[第"+(teamID+1) + "探险队]";

        //UpdateDungeon(teamID);
        //UpdateHero(teamID);
        part_baseText.text = gc.OutputDateStr(gc.adventureTeamList[teamID].standardTimeStart, "Y年M月D日") + "探险队" + gc.adventureTeamList[teamID].heroIDList.Count + "人启程";
        part_baseBgImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_B", typeof(Sprite)) as Sprite; 
        part_baseFgImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_F", typeof(Sprite)) as Sprite;

        part_nowBgImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_B", typeof(Sprite)) as Sprite;
        part_nowFgImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_F", typeof(Sprite)) as Sprite;

        part_lastBgImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_B", typeof(Sprite)) as Sprite; 
        part_lastFgImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_F", typeof(Sprite)) as Sprite; 

        UpdatePart(teamID);

        UpdateLast(teamID);
        UpdateNow(teamID);
    }




    public void UpdatePart(byte teamID)
    {
        GameObject go;
        for (byte i = 0; i < gc.adventureTeamList[teamID].part.Count; i++)
        {
            if (i < adventurePartGoPool.Count)
            {
                go = adventurePartGoPool[i];
                adventurePartGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UIBlock/Block_AdventurePart")) as GameObject;
                go.transform.SetParent(partListGo.transform);
                adventurePartGoPool.Add(go);
            }
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(170f + i * 170f, 0f);

            UpdatePartSingle(teamID, i);
        }
        for (int i = gc.adventureTeamList[teamID].part.Count; i < adventurePartGoPool.Count; i++)
        {
            adventurePartGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }

        partListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(170f * (gc.adventureTeamList[teamID].part.Count + 3), 488f);
    }

    public void UpdatePartSingle(byte teamID, byte part)
    {

        Debug.Log("UpdatePart() teamID=" + teamID + " part=" + part);
        Debug.Log("gc.adventureTeamList[teamID].part.Count=" + gc.adventureTeamList[teamID].part.Count);
        AdventurePartBlock adventurePartBlock = adventurePartGoPool[part].GetComponent<AdventurePartBlock>();

        adventurePartBlock.bgImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_B", typeof(Sprite)) as Sprite;
        adventurePartBlock.fgImage.overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + gc.adventureTeamList[teamID].scenePicList[0] + "_F", typeof(Sprite)) as Sprite;

        Debug.Log("gc.adventureTeamList[teamID].part[part].eventType=" + gc.adventureTeamList[teamID].part[part].eventType);
        if (gc.adventureTeamList[teamID].part[part].eventType == AdventureEvent.None)
        {
            adventurePartBlock.eventImage.GetComponent<RectTransform>().localScale = Vector2.zero;
            return;
        }
        else 
        {
            adventurePartBlock.eventImage.GetComponent<RectTransform>().localScale = Vector2.one;
        }


        if (part % 2==1)
        {
            adventurePartBlock.event_lineImage.GetComponent<RectTransform>().sizeDelta = new Vector2(4f, 50f);
        }
        else
        {
            adventurePartBlock.event_lineImage.GetComponent<RectTransform>().sizeDelta = new Vector2(4f, 220f);
        }

        switch (gc.adventureTeamList[teamID].part[part].eventType)
        {
            case AdventureEvent.Monster:
                adventurePartBlock.eventImage.overrideSprite = Resources.Load("Image/Other/icon007", typeof(Sprite)) as Sprite;
                adventurePartBlock.event_desTypeImage.overrideSprite = Resources.Load("Image/Other/icon007", typeof(Sprite)) as Sprite;
                adventurePartBlock.event_pointImage.color = colorFight;
                adventurePartBlock.event_lineImage.color = colorFight;
                adventurePartBlock.event_infoImage.color = colorFight;
                adventurePartBlock.event_desTitleImage.color = colorFight;
                adventurePartBlock.event_elementTitleImage.color = colorFight;
                adventurePartBlock.event_heroTitleImage.color = colorFight;
                adventurePartBlock.event_desTitleText.text = "[" + (part + 1) + "]已探索 - 事件：战斗";
                break;
            case AdventureEvent.TrapHp:
            case AdventureEvent.TrapMp:
                adventurePartBlock.eventImage.overrideSprite = Resources.Load("Image/Other/icon219", typeof(Sprite)) as Sprite;
                adventurePartBlock.event_desTypeImage.overrideSprite = Resources.Load("Image/Other/icon219", typeof(Sprite)) as Sprite;
                adventurePartBlock.event_pointImage.color = colorTrap;
                adventurePartBlock.event_lineImage.color = colorTrap;
                adventurePartBlock.event_infoImage.color = colorTrap;
                adventurePartBlock.event_desTitleImage.color = colorTrap;
                adventurePartBlock.event_elementTitleImage.color = colorTrap;
                adventurePartBlock.event_heroTitleImage.color = colorTrap;
                adventurePartBlock.event_desTitleText.text = "[" + (part + 1) + "]已探索 - 事件：陷阱";
                break;
            case AdventureEvent.SpringHp:
            case AdventureEvent.SpringMp:
                adventurePartBlock.eventImage.overrideSprite = Resources.Load("Image/Other/icon148", typeof(Sprite)) as Sprite;
                adventurePartBlock.event_desTypeImage.overrideSprite = Resources.Load("Image/Other/icon148", typeof(Sprite)) as Sprite;
                adventurePartBlock.event_pointImage.color = colorSpring;
                adventurePartBlock.event_lineImage.color = colorSpring;
                adventurePartBlock.event_infoImage.color = colorSpring;
                adventurePartBlock.event_desTitleImage.color = colorSpring;
                adventurePartBlock.event_elementTitleImage.color = colorSpring;
                adventurePartBlock.event_heroTitleImage.color = colorSpring;
                adventurePartBlock.event_desTitleText.text = "[" + (part + 1) + "]已探索 - 事件：泉水";
                break;
            case AdventureEvent.Gold:
            case AdventureEvent.Item:
            case AdventureEvent.Resource:
                adventurePartBlock.eventImage.overrideSprite = Resources.Load("Image/Other/icon009", typeof(Sprite)) as Sprite;
                adventurePartBlock.event_desTypeImage.overrideSprite = Resources.Load("Image/Other/icon009", typeof(Sprite)) as Sprite;
                adventurePartBlock.event_pointImage.color = colorGetSomething;
                adventurePartBlock.event_lineImage.color = colorGetSomething;
                adventurePartBlock.event_infoImage.color = colorGetSomething;
                adventurePartBlock.event_desTitleImage.color = colorGetSomething;
                adventurePartBlock.event_elementTitleImage.color = colorGetSomething;
                adventurePartBlock.event_heroTitleImage.color = colorGetSomething;
                adventurePartBlock.event_desTitleText.text = "[" + (part + 1) + "]已探索 - 事件：资源";
                break;

        }

        adventurePartBlock.event_desContentText.text = gc.adventureTeamList[teamID].part[part].log;
        byte index = 0;
        for (byte i = 0; i < gc.adventureTeamList[teamID].part[part].elementPointList.Count; i++)
        {
            for (byte j = 0; j < gc.adventureTeamList[teamID].part[part].elementPointList[i]; j++)
            {
                if (index < 4)
                {
                    switch (i)
                    {
                        case 0: adventurePartBlock.event_elementImage[index].overrideSprite = Resources.Load("Image/Other/icon912", typeof(Sprite)) as Sprite; break;
                        case 1: adventurePartBlock.event_elementImage[index].overrideSprite = Resources.Load("Image/Other/icon913", typeof(Sprite)) as Sprite; break;
                        case 2: adventurePartBlock.event_elementImage[index].overrideSprite = Resources.Load("Image/Other/icon914", typeof(Sprite)) as Sprite; break;
                        case 3: adventurePartBlock.event_elementImage[index].overrideSprite = Resources.Load("Image/Other/icon919", typeof(Sprite)) as Sprite; break;
                        case 4: adventurePartBlock.event_elementImage[index].overrideSprite = Resources.Load("Image/Other/icon917", typeof(Sprite)) as Sprite; break;
                        case 5: adventurePartBlock.event_elementImage[index].overrideSprite = Resources.Load("Image/Other/icon916", typeof(Sprite)) as Sprite; break;
                    }
                    index++;
                }
            }
        }
        Debug.Log("index=" + index);
        for (byte i = index; i < 5; i++)
        {
            adventurePartBlock.event_elementImage[i].sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }

        for (byte i = 0; i < gc.adventureTeamList[teamID].heroIDList.Count; i++)
        {
            adventurePartBlock.event_heroPicImage[i].overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[gc.adventureTeamList[teamID].heroIDList[i]].pic + "/Pic", typeof(Sprite)) as Sprite;
            adventurePartBlock.event_heroInfoText[i].text = gc.heroDic[gc.adventureTeamList[teamID].heroIDList[i]].name +
                "\n<color=#76ee00>体力 " + gc.adventureTeamList[teamID].part[part].heroHpList[i] + "</color>" +
                "\n<color=#47B1FF>魔力 " + gc.adventureTeamList[teamID].part[part].heroMpList[i] + "</color>";
        }
        for (int i = gc.adventureTeamList[teamID].heroIDList.Count; i < 3; i++)
        {
            adventurePartBlock.event_heroPicImage[i].overrideSprite = Resources.Load("Image/Empty" , typeof(Sprite)) as Sprite;
            adventurePartBlock.event_heroInfoText[i].text = "";
        }
    }


    public void UpdateLast(byte teamID)
    {
        if (gc.adventureTeamList[teamID].state == AdventureState.Doing||
            gc.adventureTeamList[teamID].state == AdventureState.Fail ||
            gc.adventureTeamList[teamID].state == AdventureState.Retreat )
        {
            part_lastBgImage.color = new Color(120 / 255f, 120 / 255f, 120 / 255f, 1f);
            part_lastFgImage.color = new Color(120 / 255f, 120 / 255f, 120 / 255f, 1f);

            part_lastQuestText.text = "?";
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Done )
        {
            part_lastBgImage.color = Color.clear;
            part_lastFgImage.color = Color.clear;

            part_lastQuestText.text = "";
            
        }
    }

    public void UpdateNow(byte teamID)
    {
        part_nowRoleTf.GetComponent<AnimatiorControl>().SetCharaFrames(gc.heroDic[gc.adventureTeamList[teamID].heroIDList[0]].pic);
        if (gc.adventureTeamList[teamID].state == AdventureState.Doing)
        {
            part_nowRoleTf.localScale = Vector2.one;
    
            //part_nowRoleTf.GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkRight);

            part_nowLineRt.localScale = Vector2.one;

            part_nowTitleText.text = "当前";
            switch (gc.adventureTeamList[teamID].action)
            {
                case AdventureAction.Fight:
                    part_nowText.text = "战斗中";
                    part_nowRoleTf.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -12);
                    part_nowRoleTf.GetComponent<AnimatiorControl>().SetAnim(AnimStatus.AttackLoop);
                    break;
                case AdventureAction.TrapHp:
                case AdventureAction.TrapMp:
                    part_nowRoleTf.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -12);
                    part_nowRoleTf.GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Hit);
                    part_nowText.text = "陷入陷阱中"; 
                    break;
                case AdventureAction.SpringHp:
                case AdventureAction.SpringMp:
                    part_nowRoleTf.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -12);
                    part_nowRoleTf.GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Magic);
                    part_nowText.text = "泉水补给中"; 
                    break;
                case AdventureAction.GetSomething:
                    part_nowRoleTf.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -12);
                    part_nowRoleTf.GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Idle);
                    part_nowText.text = "发现了一些东西"; 
                    break;
                case AdventureAction.Walk:
                case AdventureAction.None:
                    part_nowText.text = "行进中";
                    part_nowRoleTf.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -24);
                    part_nowRoleTf.GetComponent<AnimatiorControl>().SetAnim(AnimStatus.WalkRight); 
                    break;
            }

            if (gc.adventureTeamList[teamID].part.Count % 2 == 1)
            {
                part_nowLineRt.sizeDelta = new Vector2(4f, 50f);
            }
            else
            {
                part_nowLineRt.sizeDelta = new Vector2(4f, 220f);
            }
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Done)
        {
            part_nowRoleTf.localScale = Vector2.one;
            part_nowRoleTf.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -24);
            part_nowRoleTf.GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Front);

            part_nowLineRt.localScale = Vector2.one;

            part_nowTitleText.text = "结果";
            part_nowText.text = "完成旅程";

            if (gc.adventureTeamList[teamID].part.Count % 2 == 1)
            {
                part_nowLineRt.sizeDelta = new Vector2(4f, 50f);
            }
            else
            {
                part_nowLineRt.sizeDelta = new Vector2(4f, 220f);
            }

        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Fail)
        {
            part_nowRoleTf.localScale = Vector2.one;
            part_nowRoleTf.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -12);
            part_nowRoleTf.GetComponent<AnimatiorControl>().SetAnim(AnimStatus.Death);

            part_nowLineRt.localScale = Vector2.one;

            part_nowTitleText.text = "结果";
            part_nowText.text = "被击败了";

            if (gc.adventureTeamList[teamID].part.Count % 2 == 1)
            {
                part_nowLineRt.sizeDelta = new Vector2(4f, 50f);
            }
            else
            {
                part_nowLineRt.sizeDelta = new Vector2(4f, 220f);
            }
        }
        else if (gc.adventureTeamList[teamID].state == AdventureState.Retreat)
        {
            part_nowRoleTf.localScale = Vector2.zero;

            part_nowRoleTf.GetComponent<AnimatiorControl>().Stop();


            part_nowLineRt.localScale = Vector2.one;

            part_nowTitleText.text = "结果";
            part_nowText.text = "撤退了"; 

            if (gc.adventureTeamList[teamID].part.Count % 2 == 1)
            {
                part_nowLineRt.sizeDelta = new Vector2(4f, 50f);
            }
            else
            {
                part_nowLineRt.sizeDelta = new Vector2(4f, 220f);
            }
        }
    }

}
