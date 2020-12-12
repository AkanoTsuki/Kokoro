using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildPanel : BasePanel
{
    GameControl gc;
    public static BuildPanel Instance;

    public Text infoText;
    public GameObject buildingListGo;
    public Button closeBtn;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    void Start()
    {

        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }


    public void OnShow( int x, int y)
    {

        UpdateAllInfo(gc);
        SetAnchoredPosition(x, y);
        //ShowByImmediately(true);

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
    }

    public void UpdateAllInfo(GameControl gc)
    {

        infoText.text = "木材 " + gc.districtDic[gc.nowCheckingDistrictID].rStuffWood + "     石料 " + gc.districtDic[gc.nowCheckingDistrictID].rStuffWood + "     金属 " + gc.districtDic[gc.nowCheckingDistrictID].rStuffWood + "\n金币 " + gc.gold;

        List <BuildingPrototype> temp = new List<BuildingPrototype> { };
        foreach (KeyValuePair<int, BuildingPrototype> kvp in DataManager.mBuildingDict)
        {
            if (DataManager.mBuildingDict[kvp.Key].Level == 1)
            {
                temp.Add(DataManager.mBuildingDict[kvp.Key]);
            }
        }

        GameObject go;
        for (int i = 0; i < temp.Count; i++)
        {
            go = Instantiate(Resources.Load("Prefab/UILabel/Label_BuildingInBuild")) as GameObject;
            go.transform.SetParent(buildingListGo.transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, -4 + i * -92f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + temp[i].MainPic);
            go.transform.GetChild(1).GetComponent<Text>().text = temp[i].Name+ " <color=#76ee00><i>[占地 " + temp[i].Grid+ "]</i></color>";
            go.transform.GetChild(2).GetComponent<Text>().text = "☀建造费 木"+ temp[i].NeedWood + " 石材" + temp[i].NeedStone + " 铁块" + temp[i].NeedMetal + " 金币" + temp[i].NeedGold + "     ☀维持费 " + temp[i].Expense + "金/月\n" + temp[i].Des ;
            go.GetComponent<InteractiveLabel>().index = temp[i].ID;
        }
        buildingListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(313f, Mathf.Max(231f, 4 + temp.Count * 92f));


    }
}
