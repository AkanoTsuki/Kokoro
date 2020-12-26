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


    public void OnShow( int x, int y)
    {
        UpdateAllInfo("All");
        UpdateFilterButtonText();
        SetAnchoredPosition(x, y);
        isShow = true;
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
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
                    case "Municipal": municipalCount++; break;
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
                    if (DataManager.mBuildingDict[kvp.Key].PanelType == typePanel)
                    {
                        temp.Add(DataManager.mBuildingDict[kvp.Key]);
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
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, -4 + i * -92f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + temp[i].MainPic);
            go.transform.GetChild(1).GetComponent<Text>().text = temp[i].Name+ CheckNeedToStr("grid", temp[i].Grid) ;
            go.transform.GetChild(2).GetComponent<Text>().text =  CheckNeedToStr("wood", temp[i].NeedWood) + CheckNeedToStr("stone", temp[i].NeedStone)  + CheckNeedToStr("metal", temp[i].NeedMetal) + CheckNeedToStr("gold", temp[i].NeedGold) + 
                "\n☀维持费 " + temp[i].Expense + "金/月\n" + temp[i].Des ;

            go.transform.GetChild(4).GetComponent<Text>().text = CheckNeedToStr("grid", temp[i].Grid);
            if (!CheckStuff(temp[i].Grid <= gc.districtDic[gc.nowCheckingDistrictID].gridEmpty,
                temp[i].NeedWood<= gc.districtDic[gc.nowCheckingDistrictID].rStuffWood, 
                temp[i].NeedStone <= gc.districtDic[gc.nowCheckingDistrictID].rStuffStone,
                temp[i].NeedMetal <= gc.districtDic[gc.nowCheckingDistrictID].rStuffMetal,
                temp[i].NeedGold <= gc.gold))
            {
                go.transform.GetChild(3).GetComponent<Button>().interactable = false;
            }
            
            go.GetComponent<InteractiveLabel>().index = temp[i].ID;

        }
        buildingListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(313f, Mathf.Max(231f, 4 + temp.Count * 92f));


    }
    bool CheckStuff(bool grid, bool wood, bool stone,bool metal,bool gold)
    {
        return grid&&wood && stone && metal && gold;
    }

    string CheckNeedToStr(string type,int value)
    {
        switch (type)
        {
            case "grid": return " <color=" + (value > gc.districtDic[gc.nowCheckingDistrictID].gridEmpty ? "red>" : "white>") + "[占地 " + value + "]</color>";
            case "wood":return  "<color="+ (value > gc.districtDic[gc.nowCheckingDistrictID].rStuffWood ? "red>" : "white>" )+ "木材" + value + "</color>" ;
            case "stone": return " <color=" + (value > gc.districtDic[gc.nowCheckingDistrictID].rStuffStone ? "red>" : "white>") + "石料" + value + "</color>";
            case "metal": return " <color=" + (value > gc.districtDic[gc.nowCheckingDistrictID].rStuffMetal ? "red>" : "white>") + "金属" + value + "</color>";
            case "gold": return " <color=" + (value > gc.gold ? "red>" : "white>") + "金币" + value + "</color>";
            default:return "未定义类型";
        }
    }
}
