using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureSendPanel : BasePanel
{
    public static AdventureSendPanel Instance;

    GameControl gc;
    public List<Button> team_Btn;
    public List<RectTransform> team_selectedRt;
    public List<Text> team_desText;

    public GameObject list_heroGo;
    public Text list_heroNumText;

    public RectTransform toRt;
    public Image to_now_picImage;
    public Text to_now_desText;
    public GameObject to_list_dungeonGo;

    public RectTransform fromRt;
    public Image from_now_picImage;
    public Text from_now_desText;
    public GameObject from_list_districtGo;


    public Button doBtn;
    public Button closeBtn;

    List<GameObject> heroGoPool = new List<GameObject>();
    List<GameObject> districtGoPool = new List<GameObject>();
    List<GameObject> dungeonGoPool = new List<GameObject>();

    public List<int> selectedHeroID = new List<int>();
    public short selectedDistrict = -1;
    public short selectedDungeon = -1;


    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }


    void Start()
    {

        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public void OnShow(string type,short districtOrDungeonID)
    {
        
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }

    public void UpdateTeamList()
    {
        List<AdventureTeamObject> teamObjects = new List<AdventureTeamObject>();
        for (int i = 0; i < gc.adventureTeamList.Count; i++)
        {
            if (gc.adventureTeamList[i].state == AdventureState.Free)
            {
                team_Btn[i].GetComponent<RectTransform>().localScale = Vector2.one;

            }
        }
    }

    public void UpdateHeroNum()
    {
        list_heroNumText.text = "选择角色(已选择" + selectedHeroID.Count + "/3人)";
    }
    public void UpdateHeroList(short districtID)
    {
        List<HeroObject> heroObjects = new List<HeroObject>();

        foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
        {
            if (kvp.Value.inDistrict == districtID && kvp.Value.adventureInTeam == -1)
            {
                heroObjects.Add(kvp.Value);
            }
        }

        GameObject go;
        for (int i = 0; i < heroObjects.Count; i++)
        {


            if (i < heroGoPool.Count)
            {
                go = heroGoPool[i];
                heroGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_HeroInTransfer")) as GameObject;
                go.transform.SetParent(list_heroGo.transform);
                heroGoPool.Add(go);
            }
            go.name = "Hero_" + heroObjects[i].id;

            int row = i == 0 ? 0 : (i % 4);
            int col = i == 0 ? 0 : (i / 4);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f + row * 154f, -4 + col * -52f);
            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + heroObjects[i].pic + "/Pic");
            go.transform.GetChild(1).GetComponent<Text>().text = heroObjects[i].name;
            string str = "Lv." + heroObjects[i].level + "<color=#" + DataManager.mHeroDict[heroObjects[i].prototypeID].Color + ">" + DataManager.mHeroDict[heroObjects[i].prototypeID].Name + "</color>\n";
            if (heroObjects[i].workerInBuilding != -1)
            {
                str += "[" + gc.buildingDic[heroObjects[i].workerInBuilding].name + "工作]";
            }
            bool canSelect = true;
            if (heroObjects[i].adventureInTeam != -1)
            {
                if (gc.adventureTeamList[heroObjects[i].adventureInTeam].state != AdventureState.Doing)
                {
                    canSelect = false;
                    str += "[探险中]";
                }
            }
            go.transform.GetChild(2).GetComponent<Text>().text = str;

            if (canSelect)
            {
                go.transform.GetChild(3).GetComponent<RectTransform>().localScale = Vector2.one;
                if (selectedHeroID.Contains(heroObjects[i].id))
                {
                    go.transform.GetChild(4).GetComponent<RectTransform>().localScale = Vector2.one;
                }
                else
                {
                    go.transform.GetChild(4).GetComponent<RectTransform>().localScale = Vector2.zero;
                }

                go.GetComponent<Button>().interactable = true;
                go.GetComponent<Button>().onClick.RemoveAllListeners();
                int heroID = heroObjects[i].id;
                go.GetComponent<Button>().onClick.AddListener(delegate () { gc.SetTransferHero(heroID); });
            }
            else
            {
                go.transform.GetChild(3).GetComponent<RectTransform>().localScale = Vector2.zero;
                go.GetComponent<Button>().interactable = false;
            }
        }

        for (int i = heroObjects.Count; i < heroGoPool.Count; i++)
        {
            heroGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        list_heroGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(681f, Mathf.Max(245f, 4 + (heroObjects.Count / 4) * 52f));
    }

    public void UpdateHeroListSingle(int heroID)
    {
        GameObject go = GameObject.Find("Canvas/AdventureSendPanel/HeroList/ScrollView/Viewport/Content/Hero_" + heroID);


        string str = "Lv." + gc.heroDic[heroID].level + "<color=#" + DataManager.mHeroDict[gc.heroDic[heroID].prototypeID].Color + ">" + DataManager.mHeroDict[gc.heroDic[heroID].prototypeID].Name + "</color>";
        if (gc.heroDic[heroID].workerInBuilding != -1)
        {
            str += "[" + gc.buildingDic[gc.heroDic[heroID].workerInBuilding].name + "工作]";
        }
        bool canSelect = true;
        if (gc.heroDic[heroID].adventureInTeam != -1)
        {
            str += "[已编入"+(gc.heroDic[heroID].adventureInTeam+1) +"]";
            if (gc.adventureTeamList[gc.heroDic[heroID].adventureInTeam].state != AdventureState.Doing)
            {
                canSelect = false;
                str += "[探险中]";
            }
        }
        go.transform.GetChild(2).GetComponent<Text>().text = str;

        if (canSelect)
        {
            go.transform.GetChild(3).GetComponent<RectTransform>().localScale = Vector2.one;
            if (selectedHeroID.Contains(gc.heroDic[heroID].id))
            {
                go.transform.GetChild(4).GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go.transform.GetChild(4).GetComponent<RectTransform>().localScale = Vector2.zero;
            }

            go.GetComponent<Button>().interactable = true;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(delegate () { gc.SetTransferHero(heroID); });
        }
        else
        {
            go.transform.GetChild(3).GetComponent<RectTransform>().localScale = Vector2.zero;
            go.GetComponent<Button>().interactable = false;
        }
    }

    public void UpdateFromDistrictList(short dungeonID)
    {
        List<DistrictObject> districtObjects = new List<DistrictObject>();

        for (int i = 0; i < gc.districtDic.Length; i++)
        {
            if (gc.districtDic[i].isOpen && DataManager.mDistrictDict[i].DungeonList.Contains(dungeonID))
            {
                districtObjects.Add(gc.districtDic[i]);
            }
        }

        GameObject go;
        for (int i = 0; i < districtObjects.Count; i++)
        {
            if (i < districtGoPool.Count)
            {
                go = districtGoPool[i];
                districtGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_CityButtonInTransfer")) as GameObject;
                go.transform.SetParent(from_list_districtGo.transform);
                districtGoPool.Add(go);
            }
            go.name = "District_" + districtObjects[i].id;

            int row = i == 0 ? 0 : (i % 6);
            int col = i == 0 ? 0 : (i / 6);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(16f + row * 80f, col * -100f);
            go.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/AreaPic/" + DataManager.mDistrictDict[districtObjects[i].id].Pic);
            go.transform.GetChild(0).GetComponent<Text>().text = districtObjects[i].name + (districtObjects[i].isOwn ? "\n[领地]" : "");

            if (selectedDistrict == districtObjects[i].id)
            {
                go.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector2.one;
                go.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else
            {
                go.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector2.zero;
                go.GetComponent<Button>().onClick.RemoveAllListeners();
                short did = districtObjects[i].id;
                go.GetComponent<Button>().onClick.AddListener(delegate () { gc.SetTransferDistrict(did); });
            }

        }
        for (int i = districtObjects.Count; i < districtGoPool.Count; i++)
        {
            districtGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        from_list_districtGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(512f, Mathf.Max(100f, 100 + (districtObjects.Count / 6) * 100f));
    }
    public void UpdateFromDistrictListSingle(short districtID)
    {
        GameObject go = GameObject.Find("Canvas/AdventureSendPanel/DistrictList/ScrollView/Viewport/Content/District_" + districtID);

        Debug.Log("districtID=" + districtID + " selectedDistrict=" + selectedDistrict);
        Debug.Log(" go.name=" + go.name);
        if (selectedDistrict == districtID)
        {
            go.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector2.one;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        else
        {
            go.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector2.zero;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(delegate () { gc.SetTransferDistrict(districtID); });
        }
    }
    public void UpdateFromNow(short dungeonID)
    {
        from_now_picImage.sprite = Resources.Load<Sprite>("Image/AreaPic/" + DataManager.mDungeonDict[dungeonID].ScenePic[0]);
        from_now_desText.text = DataManager.mDungeonDict[dungeonID].Name;
    }

    public void UpdateToDungeonList(short districtID)
    {
        List<DungeonObject> dungeonObjects = new List<DungeonObject>();

        for (int i = 0; i < gc.dungeonList.Count; i++)
        {
            if (gc.dungeonList[i].stage== DungeonStage.Open &&DataManager.mDistrictDict[districtID].DungeonList.Contains((short)i))
            {
                dungeonObjects.Add(gc.dungeonList[i]);
            }
        }

        GameObject go;
        for (int i = 0; i < dungeonObjects.Count; i++)
        {
            if (i < districtGoPool.Count)
            {
                go = districtGoPool[i];
                districtGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_DungeonButton")) as GameObject;
                go.transform.SetParent(from_list_districtGo.transform);
                districtGoPool.Add(go);
            }
            go.name = "Dungeon_" + dungeonObjects[i].id;

            int row = i == 0 ? 0 : (i % 6);
            int col = i == 0 ? 0 : (i / 6);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(16f + row * 80f, col * -100f);

            go.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/AdventureBG/" + DataManager.mDungeonDict[dungeonObjects[i].id].ScenePic[0]+ "_B");
            go.transform.GetChild(2).GetComponent<Text>().text = DataManager.mDungeonDict[dungeonObjects[i].id].Name;

            if (selectedDungeon == dungeonObjects[i].id)
            {
                go.transform.GetChild(3).GetComponent<RectTransform>().localScale = Vector2.one;
                go.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else
            {
                go.transform.GetChild(3).GetComponent<RectTransform>().localScale = Vector2.zero;
                go.GetComponent<Button>().onClick.RemoveAllListeners();
                short did = dungeonObjects[i].id;
                go.GetComponent<Button>().onClick.AddListener(delegate () {/**/ });
            }

        }
        for (int i = dungeonObjects.Count; i < districtGoPool.Count; i++)
        {
            districtGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        from_list_districtGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(512f, Mathf.Max(100f, 100 + (dungeonObjects.Count / 6) * 100f));
    }

    public void UpdateToDungeonListSingle(short dungeonID)
    {
        GameObject go = GameObject.Find("Canvas/AdventureSendPanel/DungeonList/ScrollView/Viewport/Content/Dungeon_" + dungeonID);
        if (selectedDungeon == dungeonID)
        {
            go.transform.GetChild(3).GetComponent<RectTransform>().localScale = Vector2.one;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        else
        {
            go.transform.GetChild(3).GetComponent<RectTransform>().localScale = Vector2.zero;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            //short did = dungeonObjects[i].id;
            go.GetComponent<Button>().onClick.AddListener(delegate () {/**/ });
        }
    }

    public void UpdateToNow(short districtID)
    {
        to_now_picImage.sprite = Resources.Load<Sprite>("Image/AreaPic/" + DataManager.mDistrictDict[districtID].Pic);
        to_now_desText.text = DataManager.mDistrictDict[districtID].Name;
    }
}
