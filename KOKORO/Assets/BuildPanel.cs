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

            go.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + temp[i].MainPic);
            go.transform.GetChild(2).GetComponent<Text>().text = temp[i].Name;
            go.transform.GetChild(3).GetComponent<Text>().text =  CheckNeedToStr("NeedWood", temp[i].NeedWood) + CheckNeedToStr("NeedStone", temp[i].NeedStone)  + CheckNeedToStr("NeedMetal", temp[i].NeedMetal) + CheckNeedToStr("gold", temp[i].NeedGold) +
                CheckNeedToStr("NatureGrass", temp[i].NatureGrass) + CheckNeedToStr("NatureWood", temp[i].NatureWood) + CheckNeedToStr("NatureWater", temp[i].NatureWater) + CheckNeedToStr("NatureStone", temp[i].NatureStone) + CheckNeedToStr("NatureMetal", temp[i].NatureMetal) +
                 "\n" + temp[i].Des + ".维持费" + temp[i].Expense + "金/月 " ;

            go.transform.GetChild(5).GetComponent<Text>().text = CheckNeedToStr("grid", temp[i].Grid);

            //Debug.Log(temp[i].Grid <= gc.districtDic[gc.nowCheckingDistrictID].gridEmpty);
            //Debug.Log(temp[i].NeedWood <= gc.districtDic[gc.nowCheckingDistrictID].rStuffWood);
            //Debug.Log(temp[i].NeedStone <= gc.districtDic[gc.nowCheckingDistrictID].rStuffStone);
            //Debug.Log(temp[i].NeedMetal <= gc.districtDic[gc.nowCheckingDistrictID].rStuffMetal);
            //Debug.Log(temp[i].NeedGold <= gc.gold);
            //Debug.Log(temp[i].NatureGrass <= (gc.districtDic[gc.nowCheckingDistrictID].totalGrass - gc.districtDic[gc.nowCheckingDistrictID].usedGrass));
            //Debug.Log(temp[i].NatureWood <= (gc.districtDic[gc.nowCheckingDistrictID].totalWood - gc.districtDic[gc.nowCheckingDistrictID].usedWood));
            //Debug.Log(temp[i].NatureWater <= (gc.districtDic[gc.nowCheckingDistrictID].totalWater - gc.districtDic[gc.nowCheckingDistrictID].usedWater));
            //Debug.Log(temp[i].NatureStone <= (gc.districtDic[gc.nowCheckingDistrictID].totalStone - gc.districtDic[gc.nowCheckingDistrictID].usedStone));
            //Debug.Log(temp[i].NatureMetal <= (gc.districtDic[gc.nowCheckingDistrictID].totalMetal - gc.districtDic[gc.nowCheckingDistrictID].usedMetal));
            short bpID = temp[i].ID;
            if (temp[i].Grid <= gc.districtDic[gc.nowCheckingDistrictID].gridEmpty &&
                temp[i].NeedWood <= gc.districtDic[gc.nowCheckingDistrictID].rStuffWood &&
                temp[i].NeedStone <= gc.districtDic[gc.nowCheckingDistrictID].rStuffStone &&
                temp[i].NeedMetal <= gc.districtDic[gc.nowCheckingDistrictID].rStuffMetal &&
                temp[i].NeedGold <= gc.gold &&
                temp[i].NatureGrass <= (gc.districtDic[gc.nowCheckingDistrictID].totalGrass - gc.districtDic[gc.nowCheckingDistrictID].usedGrass) &&
                temp[i].NatureWood <= (gc.districtDic[gc.nowCheckingDistrictID].totalWood - gc.districtDic[gc.nowCheckingDistrictID].usedWood) &&
                temp[i].NatureWater <= (gc.districtDic[gc.nowCheckingDistrictID].totalWater - gc.districtDic[gc.nowCheckingDistrictID].usedWater) &&
                temp[i].NatureStone <= (gc.districtDic[gc.nowCheckingDistrictID].totalStone - gc.districtDic[gc.nowCheckingDistrictID].usedStone) &&
                temp[i].NatureMetal <= (gc.districtDic[gc.nowCheckingDistrictID].totalMetal - gc.districtDic[gc.nowCheckingDistrictID].usedMetal)
               )
            {
                go.transform.GetChild(4).GetComponent<Button>().interactable = true;
                go.transform.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
                go.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate () { gc.CreateBuildEvent(bpID); });
               
            }
            else
            {
                go.transform.GetChild(4).GetComponent<Button>().interactable = false;
            }
            go.GetComponent<InteractiveLabel>().index = temp[i].ID;

        }
        buildingListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(313f, Mathf.Max(231f, 4 + temp.Count * 92f));


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
                case "grid": return " <color=" + (value > gc.districtDic[gc.nowCheckingDistrictID].gridEmpty ? "#FF5B5B>" : "white>") + "[占地 " + value + "]</color>";
                case "NeedWood": return "<color=" + (value > gc.districtDic[gc.nowCheckingDistrictID].rStuffWood ? "#FF5B5B>" : "white>") + "木材" + value + "</color>";
                case "NeedStone": return " <color=" + (value > gc.districtDic[gc.nowCheckingDistrictID].rStuffStone ? "#FF5B5B>" : "white>") + "石料" + value + "</color>";
                case "NeedMetal": return " <color=" + (value > gc.districtDic[gc.nowCheckingDistrictID].rStuffMetal ? "#FF5B5B>" : "white>") + "金属" + value + "</color>";
                case "NatureGrass": return " <color=" + (value > (gc.districtDic[gc.nowCheckingDistrictID].totalGrass - gc.districtDic[gc.nowCheckingDistrictID].usedGrass )? "#FF5B5B>" : "white>") + "草地" + value + "</color>";
                case "NatureWood": return " <color=" + (value > (gc.districtDic[gc.nowCheckingDistrictID].totalWood - gc.districtDic[gc.nowCheckingDistrictID].usedWood) ? "#FF5B5B>" : "white>") + "林地" + value + "</color>";
                case "NatureWater": return " <color=" + (value > (gc.districtDic[gc.nowCheckingDistrictID].totalWater - gc.districtDic[gc.nowCheckingDistrictID].usedWater) ? "#FF5B5B>" : "white>") + "水域" + value + "</color>";
                case "NatureStone": return " <color=" + (value > (gc.districtDic[gc.nowCheckingDistrictID].totalStone - gc.districtDic[gc.nowCheckingDistrictID].usedStone) ? "#FF5B5B>" : "white>") + "石头矿区" + value + "</color>";
                case "NatureMetal": return " <color=" + (value > (gc.districtDic[gc.nowCheckingDistrictID].totalMetal - gc.districtDic[gc.nowCheckingDistrictID].usedMetal) ? "#FF5B5B>" : "white>") + "金属矿区" + value + "</color>";


                case "gold": return " <color=" + (value > gc.gold ? "#FF5B5B>" : "white>") + "金币" + value + "</color>";
                default: return "未定义类型";
            }
        }
        else
        {
            return "";
        }
    }
}
