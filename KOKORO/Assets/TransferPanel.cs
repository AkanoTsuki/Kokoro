using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransferPanel : BasePanel
{
    public static TransferPanel Instance;

    GameControl gc;

    public RectTransform listRt;
    public RectTransform arrowRt;
    public RectTransform nowRt;

    public GameObject list_heroGo;
    public GameObject list_districtGo;

    public Text list_heroNumText;

    public Image now_picImage;
    public Text now_desText;

    public Button doBtn;
    public Button closeBtn;

    List<GameObject> heroGoPool = new List<GameObject>();
    List<GameObject> districtGoPool = new List<GameObject>();

    public List<int> selectedHeroID = new List<int>();
    public short selectedDistrict = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }


    void Start()
    {
      
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public void OnShow(string type, short districtID)
    {
       

        

        if (type == "To")
        {

            selectedDistrict = -1;
            doBtn.onClick.RemoveAllListeners();
            doBtn.onClick.AddListener(delegate () { gc.Transfer(districtID, selectedDistrict, selectedHeroID); });
          
            UpdateHeroList(districtID);
            UpdateHeroNum();
            UpdateDistrictList(type,districtID);
            UpdateNow( districtID);
            listRt.anchoredPosition = new Vector2(114.5f, -26f);
            arrowRt.anchoredPosition = new Vector2(96f, -70f);
            nowRt.anchoredPosition = new Vector2(6f, -26f);
        }
        else if (type == "From")
        {
            //selectedHeroID.Clear();
            selectedDistrict = -1;
            doBtn.onClick.RemoveAllListeners();
            doBtn.onClick.AddListener(delegate () { gc.Transfer( selectedDistrict, districtID, selectedHeroID); });
      
            ClearUpdateHeroList();
            UpdateHeroNum();
            UpdateDistrictList(type, districtID);
            UpdateNow(districtID);
            listRt.anchoredPosition = new Vector2(6f, -26f);
            arrowRt.anchoredPosition = new Vector2(538f, -70f);
            nowRt.anchoredPosition = new Vector2(556f, -26f);
        }

      
        SetAnchoredPosition(64, -88);
        isShow = true;

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }

    public void UpdateHeroNum()
    {
        list_heroNumText.text = "选择角色(已选择"+ selectedHeroID .Count+ "人)";
    }

    public void UpdateHeroList( short districtID)
    {
        selectedHeroID.Clear();
        List<HeroObject> heroObjects = new List<HeroObject>();

        foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
        {
            if (kvp.Value.inDistrict == districtID)
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
            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/"+ heroObjects[i].pic + "/Pic" );
            go.transform.GetChild(1).GetComponent<Text>().text= heroObjects[i].name ;
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
        GameObject go = GameObject.Find("Canvas/TransferPanel/HeroList/ScrollView/Viewport/Content/Hero_" + heroID);
       
        
        string str = "Lv." + gc.heroDic[heroID].level + "<color=#" + DataManager.mHeroDict[gc.heroDic[heroID].prototypeID].Color + ">" + DataManager.mHeroDict[gc.heroDic[heroID].prototypeID].Name + "</color>";
        if (gc.heroDic[heroID].workerInBuilding != -1)
        {
            str += "[" + gc.buildingDic[gc.heroDic[heroID].workerInBuilding].name + "工作]";
        }
        bool canSelect = true;
        if (gc.heroDic[heroID].adventureInTeam != -1)
        {

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

    public void ClearUpdateHeroList()
    {
        selectedHeroID.Clear();
        for (int i = 0; i < heroGoPool.Count; i++)
        {
            heroGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        list_heroGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(681f, 245f);
    }

    public void UpdateDistrictList(string type, short districtID)
    {
        List<DistrictObject> districtObjects = new List<DistrictObject>();

        for (int i = 0; i < gc.districtDic.Length; i++)
        {
            if (type == "To")
            {
                if (gc.districtDic[i].isOpen && gc.districtDic[i].id != districtID)
                {
                    districtObjects.Add(gc.districtDic[i]);
                }
            }
            else if (type == "From")
            {
                if (gc.districtDic[i].heroList.Count>0 && gc.districtDic[i].id != districtID)
                {
                    districtObjects.Add(gc.districtDic[i]);
                }
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
                go.transform.SetParent(list_districtGo.transform);
                districtGoPool.Add(go);
            }
            go.name = "District_" + districtObjects[i].id;

            int row = i == 0 ? 0 : (i % 6);
            int col = i == 0 ? 0 : (i / 6);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(16f + row * 80f,  col * -100f);
            go.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/AreaPic/" + DataManager.mDistrictDict[districtObjects[i].id].Pic);

            go.transform.GetChild(0).GetComponent<Text>().text = districtObjects[i].name + (gc.districtDic[districtObjects[i].id].heroList.Count >0?("\n[" + gc.districtDic[districtObjects[i].id].heroList.Count + "人]"):"") + (districtObjects[i].force==0? "\n[领地]":"") ;

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
                go.GetComponent<Button>().onClick.AddListener(delegate () { 
                    gc.SetTransferDistrict(type, did);
                    if (type == "From")
                    {
                        UpdateHeroList(did);
                    }
                       
                });
            }

        }
        for (int i = districtObjects.Count; i < districtGoPool.Count; i++)
        {
            districtGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        list_districtGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(512f, Mathf.Max(100f, 100+ (districtObjects.Count / 6) * 100f));
    }

    public void UpdateDistrictListSingle(string type, short districtID)
    {
        GameObject go = GameObject.Find("Canvas/TransferPanel/DistrictList/ScrollView/Viewport/Content/District_" + districtID);

        Debug.Log("districtID="+ districtID+ " selectedDistrict=" + selectedDistrict);
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
            go.GetComponent<Button>().onClick.AddListener(delegate () { 
                gc.SetTransferDistrict(type, districtID);
                if (type == "From")
                {
                    UpdateHeroList(districtID);
                }
            });
        }
    }

    public void UpdateNow( short districtID)
    {
        now_picImage.sprite = Resources.Load<Sprite>("Image/AreaPic/" + DataManager.mDistrictDict[districtID].Pic);
        now_desText.text = DataManager.mDistrictDict[districtID].Name;
    }
}
