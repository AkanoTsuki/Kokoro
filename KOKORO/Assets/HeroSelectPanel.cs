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
    public int nowSelectedHeroID = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    //districtID在指派冒险者的时候作为TeamID使用
    public void OnShow(string type, int districtID, int buildingID, byte columns, int x, int y)
    {

        UpdateAllInfo(type, districtID, buildingID, columns);
        SetAnchoredPosition(x, y);
        isShow = true;

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }
    public void UpdateDesInfo()
    {
        desText.text = nowSelectedHeroID != -1 ? "已选中：" + gc.heroDic[nowSelectedHeroID].name : "未选中";
    }


    public void UpdateAllInfo(string type,int districtID, int buildingID, byte columns)
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
        List<HeroObject> temp = new List<HeroObject> { };
        GameObject go;
        switch (type)
        {
            case "":
                foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
                {
                    if (gc.districtDic[districtID].heroList.Contains(kvp.Key))
                    {
                        temp.Add(kvp.Value);
                    }
                }
                numText.text = temp.Count + "人";
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

                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + temp[i].pic + "/Pic");

                    go.transform.GetChild(1).GetComponent<Text>().text = temp[i].name;
                    go.transform.GetChild(2).GetComponent<Text>().text = "Lv." + temp[i].level + " <color=#" + DataManager.mHeroDict[temp[i].prototypeID].Color + ">" + DataManager.mHeroDict[temp[i].prototypeID].Name + "</color>";
                    go.transform.GetComponent<InteractiveLabel>().labelType = LabelType.HeroInSelectToCheck;
                    go.transform.GetComponent<InteractiveLabel>().index = temp[i].id;

                }
                for (int i = temp.Count; i < heroGo.Count; i++)
                {
                    heroGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
                }

                heroListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(413f, 4 + (temp.Count / columns) * 36f));
                doBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                break;
            case "指派管理者":
                foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
                {
                    if (gc.districtDic[districtID].heroList.Contains(kvp.Key)&& kvp.Value.workerInBuilding==-1)
                    {
                        temp.Add(kvp.Value);
                    }
                }
                numText.text = temp.Count + "人";
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

                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + temp[i].pic + "/Pic");

                    go.transform.GetChild(1).GetComponent<Text>().text = temp[i].name;
                    go.transform.GetChild(2).GetComponent<Text>().text = temp[i].workerInBuilding == -1 ? "<color=#00FF00>空闲</color>" : "<color=#7B68EE>" + gc.buildingDic[temp[i].workerInBuilding].name + "工作中</color>";
                    go.transform.GetComponent<InteractiveLabel>().labelType = LabelType.HeroInSelect;
                    go.transform.GetComponent<InteractiveLabel>().index = temp[i].id;

                }
                for (int i = temp.Count; i < heroGo.Count; i++)
                {
                    heroGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
                }

                heroListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(413f, 4 + (temp.Count / columns) * 36f));
                doBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                doBtn.onClick.RemoveAllListeners();
                doBtn.onClick.AddListener(delegate () {
                    gc.BuildingManagerAdd(buildingID, nowSelectedHeroID);
                    OnHide();
                });

                break;
            case "指派探险者":
                foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
                {
                    if ( kvp.Value.adventureInTeam == -1)
                    {
                        temp.Add(kvp.Value);
                    }
                }
                numText.text = temp.Count + "人";
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

                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + temp[i].pic + "/Pic");

                    go.transform.GetChild(1).GetComponent<Text>().text = temp[i].name;
                    go.transform.GetChild(2).GetComponent<Text>().text = "Lv." + temp[i].level;
                    go.transform.GetComponent<InteractiveLabel>().labelType = LabelType.HeroInSelect;
                    go.transform.GetComponent<InteractiveLabel>().index = temp[i].id;

                }
                for (int i = temp.Count; i < heroGo.Count; i++)
                {
                    heroGo[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
                }

                heroListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(413f, 4 + (temp.Count / columns) * 36f));
                doBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                doBtn.onClick.RemoveAllListeners();
                doBtn.onClick.AddListener(delegate () {
                    Debug.Log("(byte)districtID=" + (byte)districtID);
                    gc.AdventureTeamHeroAdd((byte)districtID, nowSelectedHeroID);
                    OnHide();
                });

                break;
        }

    }
}
