using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureMainPanel : BasePanel
{
    public static AdventureMainPanel Instance;

    GameControl gc;
    public GameObject teamListGo;

    public RectTransform dungeonRt;
    public GameObject dungeonListGo;
    public Button dungeonBtn;

    public Button closeBtn;

    //对象池
    List<GameObject> adventureTeamGo = new List<GameObject>();
    List<GameObject> dungeonGo = new List<GameObject>();

    public short nowSelectDungeonID = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public  void OnShow(int x ,int y)
    {
        UpdateAllInfo();
        HideDungeonPage();
        SetAnchoredPosition(x, y);
        isShow = true;
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }

    public void UpdateAllInfo()
    {
        Debug.Log("gc.adventureTeamList.Count=" + gc.adventureTeamList.Count);

        GameObject go;

        for (byte i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (i < adventureTeamGo.Count)
            {
                go = adventureTeamGo[i];
                adventureTeamGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UIBlock/Block_AdventureTeam")) as GameObject;
                go.transform.SetParent(teamListGo.transform);
                adventureTeamGo.Add(go);
            }

            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * 276f, 0f, 0f);
            UpdateTeam(i);


        }
        for (int i = gc.adventureTeamList.Count; i < adventureTeamGo.Count; i++)
        {
            adventureTeamGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }

        teamListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(260f* gc.adventureTeamList.Count, 488f);

    }

    void UpdateTeam(byte teamID)
    {
       GameObject go = adventureTeamGo[teamID];
        if (gc.adventureTeamList[teamID].state == AdventureState.NotSend)
        {
            if (gc.adventureTeamList[teamID].dungeonID != -1)
            {
                go.GetComponent<AdventureTeamBlock>().dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].Name;
                go.GetComponent<AdventureTeamBlock>().dungeon_progressText.text = "";
                go.GetComponent<AdventureTeamBlock>().dungeon_bgListGo.transform.GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].ScenePic[0] + "_B", typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().dungeon_fgListGo.transform.GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + DataManager.mDungeonDict[gc.adventureTeamList[teamID].dungeonID].ScenePic[0] + "_F", typeof(Sprite)) as Sprite;

                go.GetComponent<AdventureTeamBlock>().dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go.GetComponent<AdventureTeamBlock>().dungeon_nameText.text = "未选择地点";
                go.GetComponent<AdventureTeamBlock>().dungeon_progressText.text = "";
                go.GetComponent<AdventureTeamBlock>().dungeon_bgListGo.transform.GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_B", typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().dungeon_fgListGo.transform.GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_Home_F", typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            go.GetComponent<AdventureTeamBlock>().dungeon_selectBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            go.GetComponent<AdventureTeamBlock>().dungeon_selectBtn.onClick.RemoveAllListeners();
            go.GetComponent<AdventureTeamBlock>().dungeon_selectBtn.onClick.AddListener(delegate () {
                ShowDungeonPage(teamID);
            });


            for (int j = 0; j < gc.adventureTeamList[teamID].heroIDList.Count; j++)
            {
                int heroID = gc.adventureTeamList[teamID].heroIDList[j];

                go.GetComponent<AdventureTeamBlock>().dungeon_heroListGo.transform.GetChild(j).GetComponent<Image>().overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[heroID].pic, typeof(Sprite)) as Sprite;


                go.GetComponent<AdventureTeamBlock>().hero_picImage[j].overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[heroID].pic, typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().hero_nameText[j].text = gc.heroDic[heroID].name + "\nLv." + gc.heroDic[heroID].level;
                go.GetComponent<AdventureTeamBlock>().hero_hpmpText[j].text = "<color=#76ee00>体力 " + gc.adventureTeamList[teamID].heroHpList[j] + "/" + gc.GetHeroAttr(Attribute.Hp, heroID) + "</color>\n<color=#428DFD>魔力 " + gc.adventureTeamList[teamID].heroMpList[j] + "/" + gc.GetHeroAttr(Attribute.Hp, heroID) + "</color>";
                go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_down", typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].onClick.RemoveAllListeners();
                go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].onClick.AddListener(delegate () {
                    /*卸下*/
                });
            }
            for (int j = gc.adventureTeamList[teamID].heroIDList.Count; j < 3; j++)
            {
                go.GetComponent<AdventureTeamBlock>().dungeon_heroListGo.transform.GetChild(j).GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;


                if (j == gc.adventureTeamList[teamID].heroIDList.Count)
                {
                    go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_up", typeof(Sprite)) as Sprite;
                    go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].onClick.RemoveAllListeners();
                    go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].onClick.AddListener(delegate ()
                    {
                        /*打开选择hero面板*/
                    });
                }
                else
                {
                    go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].onClick.RemoveAllListeners();
                    go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                }

                go.GetComponent<AdventureTeamBlock>().hero_picImage[j].overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().hero_nameText[j].text = "";
                go.GetComponent<AdventureTeamBlock>().hero_hpmpText[j].text = "";

            }

            go.GetComponent<AdventureTeamBlock>().contentText.text = gc.adventureTeamList[teamID].log;

        }
    }


    public void ShowDungeonPage(byte teamID)
    {
        dungeonRt.localScale = Vector2.one;

        List<int> dungeonID = new List<int> { };
        for (int i = 0; i < gc.dungeonList.Count; i++)
        {
            if (gc.dungeonList[i].unlock)
            {
                dungeonID.Add(i);
            }
        }

        int columns = 3;

        GameObject go;

        Debug.Log(" dungeonID.Count=" + dungeonID.Count);
        for (int i = 0; i < dungeonID.Count; i++)
        {
            if (i < dungeonGo.Count)
            {
                go = dungeonGo[i];
                dungeonGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Dungeon")) as GameObject;
                go.transform.SetParent(dungeonListGo.transform);
                dungeonGo.Add(go);
            }
            int row = i == 0 ? 0 : (i % columns);
            int col = i == 0 ? 0 : (i / columns);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f + row * 262f, -4 + col * -102f);
            Debug.Log( " i=" + i);
            Debug.Log("DataManager.mDungeonDict[dungeonID[i]].ScenePic[0]" + DataManager.mDungeonDict[dungeonID[i]].ScenePic[0]+" i="+ i);
            go.transform.GetChild(0).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/AdventureBG/ABG_" + DataManager.mDungeonDict[dungeonID[i]].ScenePic[0]+"_B", typeof(Sprite)) as Sprite;
            go.transform.GetChild(1).GetComponent<Text>().text = "已开放";
            go.transform.GetChild(2).GetComponent<Text>().text = DataManager.mDungeonDict[dungeonID[i]].Name;
            string str = "";
            for (int j = 0; j < DataManager.mDungeonDict[dungeonID[i]].MonsterID.Count; j++)
            {
                str += DataManager.mMonsterDict[DataManager.mDungeonDict[dungeonID[i]].MonsterID[j]].Name+" ";
            }

            go.transform.GetChild(3).GetComponent<Text>().text ="地图等级 "+ DataManager.mDungeonDict[dungeonID[i]].Level+" / 耗时 "+ DataManager.mDungeonDict[dungeonID[i]].PartNum+"\n"+ DataManager.mDungeonDict[dungeonID[i]].Des+"\n出现怪物 "+str;
            go.GetComponent<InteractiveLabel>().index = dungeonID[i];


        }
        dungeonListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(260*3f, Mathf.Max(413f, 4 + (dungeonID.Count / columns) * 100f));

        dungeonBtn.onClick.RemoveAllListeners();
        dungeonBtn.onClick.AddListener(delegate ()
        {
            gc.adventureTeamList[teamID].dungeonID = nowSelectDungeonID;
            HideDungeonPage();
            UpdateTeam(teamID);
        });
    }




    public void HideDungeonPage()
    {
        dungeonRt.localScale = Vector2.zero;
    }
}
