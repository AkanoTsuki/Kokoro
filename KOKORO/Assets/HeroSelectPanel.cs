using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroSelectPanel : BasePanel
{
    GameControl gc;
    public static HeroSelectPanel Instance;

    public RectTransform goRt;
    public RectTransform listRt;


    public Text nameText;
    public Text desText;
    public Text numText;
    public GameObject heroListGo;
    public Button doBtn;
    public Button closeBtn;

    List<GameObject> heroGo = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }


    public void OnShow(string type, int x, int y)
    {

        UpdateAllInfo(type,gc.nowCheckingDistrictID,1);
        SetAnchoredPosition(x, y);
        //ShowByImmediately(true);

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
    }

    public void UpdateAllInfo(string type,int districtID,byte columns)
    {
        if (columns == 2)
        {
            goRt.sizeDelta = new Vector2(200f+154f, 538f);
            listRt.sizeDelta = new Vector2(174f + 154f, 438f);
        }
        else if (columns == 1)
        {
            goRt.sizeDelta = new Vector2(200f, 538f);
            listRt.sizeDelta = new Vector2(174f, 438f);
        }

        switch (type)
        {
            case "":
            case "指派管理者":
                List<HeroObject> temp = new List<HeroObject> { };
                foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
                {
                    if (gc.districtDic[districtID].heroList.Contains(kvp.Key))
                    {
                        temp.Add(kvp.Value);
                    }
                }

                GameObject go;
                for (int i = 0; i < temp.Count; i++)
                {
                    if (i < heroGo.Count)
                    {
                        go = heroGo[i];
                        heroGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
                    }
                    else
                    {
                        go = Instantiate(Resources.Load("Prefab/UILabel/Label_HeroInDis")) as GameObject;
                        go.transform.SetParent(heroListGo.transform);
                        heroGo.Add(go);
                    }
                    int row = i == 0 ? 0 : (i % columns);
                    int col = i == 0 ? 0 : (i / columns);
                    go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 154f, -4 + col * -36f, 0f);

                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + temp[i].pic);
                    go.transform.GetChild(1).GetComponent<Text>().text = temp[i].name ;
                    go.transform.GetComponent<InteractiveLabel>().index = temp[i].id;

                }
                for (int i = temp.Count; i < heroGo.Count; i++)
                {
                    heroGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
                }

                heroListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(413f, 4 + (temp.Count / columns) * 36f));

                break;

        }

    }
}
