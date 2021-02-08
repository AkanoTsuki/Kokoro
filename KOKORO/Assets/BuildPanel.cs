using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildPanel : BasePanel
{
    GameControl gc;
    public static BuildPanel Instance;

    public Button filterAllBtn;
    public Button filterHouseBtn;
    public Button filterResourceBtn;
    public Button filterForgeBtn;
    public Button filterMunicipalBtn;
    public Button filterMilitaryBtn;

    public Text filterAllText;
    public Text filterHouseText;
    public Text filterResourceText;
    public Text filterForgeText;
    public Text filterMunicipalText;
    public Text filterMilitaryText;

    public GameObject buildingListGo;
    public Button closeBtn;
    public string nowTypePanel = "All";
    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    void Start()
    {
        filterAllBtn.onClick.AddListener(delegate () { UpdateAllInfo( "All"); });
        filterHouseBtn.onClick.AddListener(delegate () { UpdateAllInfo( "House"); });
        filterResourceBtn.onClick.AddListener(delegate () { UpdateAllInfo( "Resource"); });
        filterForgeBtn.onClick.AddListener(delegate () { UpdateAllInfo("Forge"); });
        filterMunicipalBtn.onClick.AddListener(delegate () { UpdateAllInfo( "Municipal"); });
        filterMilitaryBtn.onClick.AddListener(delegate () { UpdateAllInfo( "Military"); });

        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }


    public override void OnShow( )
    {
        UpdateAllInfo(nowTypePanel);
        UpdateFilterButtonText();
        

        if (BuildingPanel.Instance.isShow)
        {
            BuildingPanel.Instance.OnHide();
        }
        gameObject.SetActive(true);
        isShow = true;
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
        isShow = false;
    }

    void UpdateFilterButtonText()
    {
        short houseCount = 0;
        short resourceCount = 0;
        short forgeCount = 0;
        short municipalCount = 0;
        short militaryCount = 0;
        foreach (KeyValuePair<int, BuildingPrototype> kvp in DataManager.mBuildingDict)
        {
            if (DataManager.mBuildingDict[kvp.Key].Level == 1 && gc.buildingUnlock[kvp.Key])
            {
                switch (kvp.Value.PanelType)
                {
                    case "House": houseCount++; break;
                    case "Resource": resourceCount++; break;
                    case "Forge": forgeCount++; break;
                    case "Municipal":
                    case "Inn":
                        municipalCount++; break;
                    case "Military": militaryCount++; break;
                }

            }

        }


        filterHouseText.text = "住房[" + houseCount + "]";
        filterResourceText.text = "资源[" + resourceCount + "]";
        filterForgeText.text = "制造[" + forgeCount + "]";
        filterMunicipalText.text = "公共[" + municipalCount + "]";
        filterMilitaryText.text = "军事[" + militaryCount + "]";

        filterAllText.text = "全部[" + (houseCount + resourceCount + forgeCount + municipalCount + militaryCount) + "]";
    }

    public void UpdateAllInfo(string typePanel)
    {
        nowTypePanel = typePanel;
        //  infoText.text = "木材 " + gc.districtDic[gc.nowCheckingDistrictID].rStuffWood + "     石料 " + gc.districtDic[gc.nowCheckingDistrictID].rStuffStone + "     金属 " + gc.districtDic[gc.nowCheckingDistrictID].rStuffMetal + "\n金币 " + gc.gold;


        List <BuildingPrototype> temp = new List<BuildingPrototype> { };
        foreach (KeyValuePair<int, BuildingPrototype> kvp in DataManager.mBuildingDict)
        {
            if (DataManager.mBuildingDict[kvp.Key].Level == 1&&gc.buildingUnlock[kvp.Key])
            {
                if (typePanel == "All")
                {
                    temp.Add(DataManager.mBuildingDict[kvp.Key]);
                }
                else
                {
                    if (typePanel == "Municipal")
                    {
                        if (DataManager.mBuildingDict[kvp.Key].PanelType == typePanel|| DataManager.mBuildingDict[kvp.Key].PanelType =="Inn")
                        {
                            temp.Add(DataManager.mBuildingDict[kvp.Key]);
                        }
                    }
                    else
                    {
                        if (DataManager.mBuildingDict[kvp.Key].PanelType == typePanel)
                        {
                            temp.Add(DataManager.mBuildingDict[kvp.Key]);
                        }
                    }
                  
                }
             
            }
        }

        for (int i = 0; i < buildingListGo.transform.childCount; i++)
        {
            Destroy(buildingListGo.transform.GetChild(i).gameObject);
        }

        GameObject go;
        for (int i = 0; i < temp.Count; i++)
        {
            go = Instantiate(Resources.Load("Prefab/UILabel/Label_BuildingInBuild")) as GameObject;
            go.transform.SetParent(buildingListGo.transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(4f+i*188f, -2f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingFace/" + temp[i].MainPic);
            go.transform.GetChild(1).GetComponent<Text>().text = temp[i].Name;
            go.transform.GetChild(2).GetComponent<Text>().text ="["+ temp[i].SizeX + "x" + temp[i].SizeYBase+ "]" + CheckNeedToStr("NeedWood", temp[i].NeedWood) + CheckNeedToStr("NeedStone", temp[i].NeedStone) + CheckNeedToStr("NeedMetal", temp[i].NeedMetal) + CheckNeedToStr("gold", temp[i].NeedGold);
            go.transform.GetChild(3).GetChild(0).GetComponent<Text>().text =  temp[i].Des + ".维持费" + temp[i].Expense + "金/月 " ;


            short bpID = temp[i].ID;
            if (
                temp[i].NeedWood <= gc.forceDic[0].rStuffWood &&
                temp[i].NeedStone <= gc.forceDic[0].rStuffStone &&
                temp[i].NeedMetal <= gc.forceDic[0].rStuffMetal &&
                temp[i].NeedGold <= gc.forceDic[0].gold 
               )
            {
                go.transform.GetChild(4).GetComponent<Button>().interactable = true;
                go.transform.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
                go.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate () {
                    DistrictMapPanel.Instance.ChoosePosition(bpID);
                    //gc.CreateBuildEvent(bpID); 
                });
               
            }
            else
            {
                go.transform.GetChild(4).GetComponent<Button>().interactable = false;
            }
            go.GetComponent<InteractiveLabel>().index = temp[i].ID;

        }
        buildingListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Max(4 + temp.Count * 188f, 1024f),148f );


    }
    //bool CheckStuff(bool grid, bool wood, bool stone,bool metal, bool gold, bool NatureGrass, bool NatureWood, bool NatureWater, bool NatureStone, bool NatureMetal)
    //{
    //    return grid&&wood && stone && metal && gold;
    //}

    string CheckNeedToStr(string type,int value)
    {
        if (value != 0)
        {
            switch (type)
            {
               
                case "NeedWood": return "<color=" + (value > gc.forceDic[0].rStuffWood ? "#FF5B5B>" : "white>") + "木材" + value + "</color>";
                case "NeedStone": return " <color=" + (value > gc.forceDic[0].rStuffStone ? "#FF5B5B>" : "white>") + "石料" + value + "</color>";
                case "NeedMetal": return " <color=" + (value > gc.forceDic[0].rStuffMetal ? "#FF5B5B>" : "white>") + "金属" + value + "</color>";
              

                case "gold": return " <color=" + (value > gc.forceDic[0].gold ? "#FF5B5B>" : "white>") + "金币" + value + "</color>";
                default: return "未定义类型";
            }
        }
        else
        {
            return "";
        }
    }
}
