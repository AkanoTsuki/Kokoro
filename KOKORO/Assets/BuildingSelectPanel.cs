using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingSelectPanel : BasePanel
{
    GameControl gc;
    public static BuildingSelectPanel Instance;

    public RectTransform goRt;
    public RectTransform listRt;

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

    public Text nameText;
    public Text numText;
    public GameObject buildingListGo;

    public Button closeBtn;

    List<GameObject> buildingGoPool = new List<GameObject>();
    public string nowTypePanel = "All";

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    void Start()
    {
        filterAllBtn.onClick.AddListener(delegate () { UpdateAllInfo(gc.nowCheckingDistrictID, "All", 2); });
        filterHouseBtn.onClick.AddListener(delegate () { UpdateAllInfo(gc.nowCheckingDistrictID, "House", 2); });
        filterResourceBtn.onClick.AddListener(delegate () { UpdateAllInfo(gc.nowCheckingDistrictID, "Resource", 2); });
        filterForgeBtn.onClick.AddListener(delegate () { UpdateAllInfo(gc.nowCheckingDistrictID, "Forge", 2); });
        filterMunicipalBtn.onClick.AddListener(delegate () { UpdateAllInfo(gc.nowCheckingDistrictID, "Municipal", 2); });
        filterMilitaryBtn.onClick.AddListener(delegate () { UpdateAllInfo(gc.nowCheckingDistrictID, "Military", 2); });

        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public void OnShow( int x, int y)
    {

        UpdateAllInfo( gc.nowCheckingDistrictID,"All", 2);
        SetAnchoredPosition(x, y);

        UpdateFilterButtonText(gc.nowCheckingDistrictID);
        isShow = true;

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }
    void UpdateFilterButtonText(int districtID)
    {
        short houseCount = 0;
        short resourceCount = 0;
        short forgeCount = 0;
        short municipalCount = 0;
        short militaryCount = 0;
        foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
        {
            if (kvp.Value.districtID == districtID)//&& kvp.Value.buildProgress==1
            {

                switch (kvp.Value.panelType)
                {
                    case "House": houseCount++;break;
                    case "Resource":resourceCount++; break;
                    case "Forge":forgeCount++;break;
                    case "Municipal":municipalCount++; break;
                    case "Military":militaryCount++; break;
                }
                
            }
        }


        filterHouseText.text = "住房[" + houseCount + "]";
        filterResourceText.text = "资源[" + resourceCount + "]";
        filterForgeText.text = "制造[" + forgeCount + "]";
        filterMunicipalText.text = "公共[" + municipalCount + "]";
        filterMilitaryText.text = "军事[" + militaryCount + "]";

        filterAllText.text = "全部[" + (houseCount+ resourceCount+ forgeCount+ municipalCount+ militaryCount) + "]";
    }

    public void UpdateAllInfo( int districtID,string typePanel, byte columns)
    {
        nameText.text = "建筑 - " + DataManager.mBuildingDict[districtID].Name;
        nowTypePanel = typePanel;
        if (columns == 2)
        {
            goRt.sizeDelta = new Vector2(240f + 154f, 538f);
            listRt.sizeDelta = new Vector2(214f + 154f, 438f);
        }
        else if (columns == 1)
        {
            goRt.sizeDelta = new Vector2(240f, 538f);
            listRt.sizeDelta = new Vector2(214f, 438f);
        }


        List<BuildingObject> temp = new List<BuildingObject> { };
        foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
        {
            if (kvp.Value.districtID == districtID)//&& kvp.Value.buildProgress==1
            {
                if (typePanel == "All")
                {
                    temp.Add(kvp.Value);
                }
                else
                {
                    if (typePanel == kvp.Value.panelType)
                    {
                        temp.Add(kvp.Value);
                    }
                }
            }
        }

        numText.text = temp.Count.ToString();

        GameObject go;
        for (int i = 0; i < temp.Count; i++)
        {
            if (i < buildingGoPool.Count)
            {
                go = buildingGoPool[i];
                buildingGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_BuildingInDistrictMain")) as GameObject;
                
                go.transform.SetParent(buildingListGo.transform);
                buildingGoPool.Add(go);
            }
            int row = i == 0 ? 0 : (i % columns);
            int col = i == 0 ? 0 : (i / columns);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 174f, -4 + col * -40f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + temp[i].mainPic);
            go.transform.GetChild(1).GetComponent<Text>().text = temp[i].name;

            if (temp[i].buildProgress == 0)
            {
                go.transform.GetChild(2).GetComponent<Text>().text = "建造中";
            }
            else if (temp[i].buildProgress == 2)
            {
                go.transform.GetChild(2).GetComponent<Text>().text = "升级中";
            }
            else if (temp[i].buildProgress == 1)
            {
                switch (temp[i].panelType)
                {
                    case "House":
                        go.transform.GetChild(2).GetComponent<Text>().text = "";
                        break;
                    case "Resource":
                        if (temp[i].produceEquipNow != -1)
                        {
                            go.transform.GetChild(2).GetComponent<Text>().text = "<color=#FFDC7C>运作中</color>";
                        }
                        else
                        {
                            go.transform.GetChild(2).GetComponent<Text>().text = "<color=#FF4500>停工</color>";
                        }

                        break;
                    case "Forge":
                        if (temp[i].produceEquipNow != -1)
                        {
                            go.transform.GetChild(2).GetComponent<Text>().text = "<color=#D583EC>" + gc.OutputItemTypeSmallStr(DataManager.mProduceEquipDict[temp[i].produceEquipNow].Type) + "(" + DataManager.mProduceEquipDict[temp[i].produceEquipNow].Level + ")制作中</color>";

                        }
                        else
                        {
                            go.transform.GetChild(2).GetComponent<Text>().text = "<color=#FF4500>停工</color>";
                        }
                        break;
                    case "Municipal":
                        go.transform.GetChild(2).GetComponent<Text>().text = "";
                        break;
                    case "Military":
                        go.transform.GetChild(2).GetComponent<Text>().text = "";
                        break;
                }
            }
            go.transform.GetComponent<InteractiveLabel>().index = temp[i].id;
            int index = temp[i].id;
            go.transform.GetComponent<Button>().onClick.RemoveAllListeners();
            go.transform.GetComponent<Button>().onClick.AddListener(delegate () { BuildingPanel.Instance.OnShow(gc.buildingDic[index]); });


        }
        for (int i = temp.Count; i < buildingGoPool.Count; i++)
        {
            buildingGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        buildingListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(352f, Mathf.Max(413f, 4 + (temp.Count / columns) * 40f));


    }
}
