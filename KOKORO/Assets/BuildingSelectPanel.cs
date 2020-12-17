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

    public Text nameText;
    public Text desText;
    public Text numText;
    public GameObject buildingListGo;
    public Button doBtn;
    public Button closeBtn;

    List<GameObject> buildingGo = new List<GameObject>();

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

        UpdateAllInfo( gc.nowCheckingDistrictID, 2);
        SetAnchoredPosition(x, y);
        //ShowByImmediately(true);

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
    }

    public void UpdateAllInfo( int districtID, byte columns)
    {
        if (columns == 2)
        {
            goRt.sizeDelta = new Vector2(200f + 154f, 538f);
            listRt.sizeDelta = new Vector2(174f + 154f, 438f);
        }
        else if (columns == 1)
        {
            goRt.sizeDelta = new Vector2(200f, 538f);
            listRt.sizeDelta = new Vector2(174f, 438f);
        }


        List<BuildingObject> temp = new List<BuildingObject> { };
        foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
        {
            if (kvp.Value.districtID == districtID)
            {
                temp.Add(kvp.Value);
            }
        }

        GameObject go;
        for (int i = 0; i < temp.Count; i++)
        {
            if (i < buildingGo.Count)
            {
                go = buildingGo[i];
                buildingGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_BuildingInDistrictMain")) as GameObject;
                
                go.transform.SetParent(buildingListGo.transform);
                buildingGo.Add(go);
            }
            int row = i == 0 ? 0 : (i % columns);
            int col = i == 0 ? 0 : (i / columns);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 154f, -4 + col * -22f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + temp[i].mainPic);
            go.transform.GetChild(1).GetComponent<Text>().text = temp[i].name;
            go.transform.GetComponent<InteractiveLabel>().index = temp[i].id;
            int index = temp[i].id;
            go.transform.GetComponent<Button>().onClick.RemoveAllListeners();
            go.transform.GetComponent<Button>().onClick.AddListener(delegate () { BuildingPanel.Instance.OnShow(gc.buildingDic[index], (int)(gameObject.GetComponent<RectTransform>().anchoredPosition.x+ goRt.sizeDelta.x+8f), -88, -45); });


        }
        for (int i = temp.Count; i < buildingGo.Count; i++)
        {
            buildingGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        buildingListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(413f, 4 + (temp.Count / columns) * 22f));


    }
}
