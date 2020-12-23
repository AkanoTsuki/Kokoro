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

    List<GameObject> adventurePartGo = new List<GameObject>();

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
        List<int> dungeonID = new List<int> { };
        for (int i = 0; i < gc.dungeonList.Count; i++)
        {
            if (gc.dungeonList[i].unlock)
            {
                dungeonID.Add(i);
            }
        }

        GameObject go;

        for (int i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (i < adventurePartGo.Count)
            {
                go = adventurePartGo[i];
                adventurePartGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UIBlock/Block_AdventureTeam")) as GameObject;
                go.transform.SetParent(teamListGo.transform);
                adventurePartGo.Add(go);
            }

            go.GetComponent<RectTransform>().anchoredPosition = new Vector3( i * 276f, 0f, 0f);

            if(gc.adventureTeamList[i].dungeonID!=-1)
            {
                go.GetComponent<AdventureTeamBlock>().dungeon_nameText.text = DataManager.mDungeonDict[gc.adventureTeamList[i].dungeonID].Name;
            }
            for (int j = 0; j < gc.adventureTeamList[i].heroIDList.Count; j++)
            {
                int heroID = gc.adventureTeamList[i].heroIDList[j];
                go.GetComponent<AdventureTeamBlock>().hero_picImage[j].overrideSprite = Resources.Load("Image/RolePic/" +gc.heroDic[heroID].pic, typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().hero_nameText[j].text = gc.heroDic[heroID].name + "\nLv." + gc.heroDic[heroID].level;
                go.GetComponent<AdventureTeamBlock>().hero_hpmpText[j].text = "<color=#76ee00>体力 "+ gc.adventureTeamList[i].heroHpList[j] + "/"+ gc.GetHeroAttr(Attribute.Hp, heroID) + "</color>\n<color=#428DFD>魔力 " + gc.adventureTeamList[i].heroMpList[j] + "/" + gc.GetHeroAttr(Attribute.Hp, heroID) + "</color>";
                go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_down", typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].onClick.RemoveAllListeners();
                go.GetComponent<AdventureTeamBlock>().hero_setBtn[j].onClick.AddListener(delegate () {
                    /*卸下*/
                });
            }
            for (int j = gc.adventureTeamList[i].heroIDList.Count; j < 3; j++)
            {
                if (j == gc.adventureTeamList[i].heroIDList.Count)
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

                go.GetComponent<AdventureTeamBlock>().hero_picImage[j].overrideSprite = Resources.Load("Image/Empty" , typeof(Sprite)) as Sprite;
                go.GetComponent<AdventureTeamBlock>().hero_nameText[j].text = "";
                go.GetComponent<AdventureTeamBlock>().hero_hpmpText[j].text = "";
               
            }

            // go.GetComponent<AdventureTeamBlock>()

        }
        for (int i = gc.adventureTeamList.Count; i < adventurePartGo.Count; i++)
        {
            adventurePartGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }

    }


    public void ShowDungeonPage()
    {
        dungeonRt.localScale = Vector2.one;
    }

    public void HideDungeonPage()
    {
        dungeonRt.localScale = Vector2.zero;
    }
}
