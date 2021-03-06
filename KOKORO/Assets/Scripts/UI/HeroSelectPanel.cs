﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroSelectPanel : BasePanel
{   
    public static HeroSelectPanel Instance;

    GameControl gc;

    #region 【UI控件】
    public RectTransform goRt;
    public RectTransform listRt;

    public Text nameText;
    public Text desText;
    public Text numText;
    public GameObject heroListGo;
    public Button doBtn;
    public Button closeBtn;
    #endregion

    //运行变量
    public int nowSelectedHeroID = -1;

    //对象池
    List<GameObject> heroGoPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    //主面板显示（districtID在指派冒险者的时候作为TeamID使用）
    public void OnShow(string type, int districtID, int buildingID, byte columns, int x, int y)
    {
        UpdateAllInfo(type, districtID, buildingID, columns);

        SetAnchoredPosition(x, y);
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        isShow = true;
    }

    //主面板关闭
    public override void OnHide()
    {
        if (HeroPanel.Instance.isShow)
        {
            HeroPanel.Instance.OnHide();
        }

        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isShow = false;
    }

    //选择状态文本-更新
    public void UpdateDesInfo()
    {
        desText.text = nowSelectedHeroID != -1 ? "已选中：" + gc.heroDic[nowSelectedHeroID].name : "未选中";
    }

    //默认信息更新
    public void UpdateAllInfo(string type,int districtID, int buildingID, byte columns)
    {
        if (columns == 2)
        {
            goRt.sizeDelta = new Vector2(185f+154f, 520f);
            listRt.sizeDelta = new Vector2(174f + 154f, 438f);
        }
        else if (columns == 1)
        {
            goRt.sizeDelta = new Vector2(185f, 520f);
            listRt.sizeDelta = new Vector2(174f, 438f);
        }
        List<HeroObject> temp = new List<HeroObject> { };
        GameObject go;
        switch (type)
        {
            case "":
                foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
                {
                    if (kvp.Value.force == 0)
                    {
                        if (districtID != -1)
                        {
                            if (gc.districtDic[districtID].heroList.Contains(kvp.Key))
                            {
                                temp.Add(kvp.Value);
                            }
                        }
                        else
                        {
                            temp.Add(kvp.Value);
                        }
                    }
            
                }
                numText.text = temp.Count + "人";
                for (int i = 0; i < temp.Count; i++)
                {
                    if (i < heroGoPool.Count)
                    {
                        go = heroGoPool[i];
                        heroGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
                    }
                    else
                    {
                        go = Instantiate(Resources.Load("Prefab/UILabel/Label_HeroInDis")) as GameObject;
                        go.transform.SetParent(heroListGo.transform);
                        heroGoPool.Add(go);
                    }
                    int row = i == 0 ? 0 : (i % columns);
                    int col = i == 0 ? 0 : (i / columns);
                    go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 154f, -4 + col * -36f, 0f);

                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + temp[i].pic + "/Pic");

                    go.transform.GetChild(1).GetComponent<Text>().text = temp[i].name;
                    go.transform.GetChild(2).GetComponent<Text>().text = "Lv." + temp[i].level + " <color=#" + DataManager.mHeroDict[temp[i].prototypeID].Color + ">" + DataManager.mHeroDict[temp[i].prototypeID].Name + "</color>";
                    go.transform.GetComponent<InteractiveLabel>().labelType = LabelType.HeroInSelectToCheck;
                    go.transform.GetComponent<InteractiveLabel>().index = temp[i].id;

                    int oid = temp[i].id;
                    go.GetComponent<Button>().onClick.RemoveAllListeners();
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        nowSelectedHeroID = oid;
                        UpdateDesInfo();
                        HeroPanel.Instance.nowSelectedHeroID = oid;
                        HeroPanel.Instance.OnShow(gc.heroDic[oid], HeroPanel.Instance.nowEquipState, (int)(GetComponent<RectTransform>().anchoredPosition.x + GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)(GetComponent<RectTransform>().anchoredPosition.y));

                    });
                }
                for (int i = temp.Count; i < heroGoPool.Count; i++)
                {
                    heroGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
                }

                heroListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(413f, 4 + (temp.Count / columns) * 36f));
                doBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
                break;
            case "指派管理者":
                foreach (KeyValuePair<int, HeroObject> kvp in gc.heroDic)
                {
                    if (gc.districtDic[districtID].heroList.Contains(kvp.Key)&& kvp.Value.workerInBuilding==-1 && kvp.Value.force==0)
                    {
                        temp.Add(kvp.Value);
                    }
                }
                numText.text = temp.Count + "人";
                for (int i = 0; i < temp.Count; i++)
                {
                    if (i < heroGoPool.Count)
                    {
                        go = heroGoPool[i];
                        heroGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
                    }
                    else
                    {
                        go = Instantiate(Resources.Load("Prefab/UILabel/Label_HeroInDis")) as GameObject;
                        go.transform.SetParent(heroListGo.transform);
                        heroGoPool.Add(go);
                    }
                    int row = i == 0 ? 0 : (i % columns);
                    int col = i == 0 ? 0 : (i / columns);
                    go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f + row * 154f, -4 + col * -36f, 0f);

                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + temp[i].pic + "/Pic");

                    go.transform.GetChild(1).GetComponent<Text>().text = temp[i].name;
                    go.transform.GetChild(2).GetComponent<Text>().text = temp[i].workerInBuilding == -1 ? "<color=#00FF00>空闲</color>" : "<color=#7B68EE>" + gc.buildingDic[temp[i].workerInBuilding].name + "工作中</color>";
                    go.transform.GetComponent<InteractiveLabel>().labelType = LabelType.HeroInSelect;
                    go.transform.GetComponent<InteractiveLabel>().index = temp[i].id;
                    int oid = temp[i].id;
                    go.GetComponent<Button>().onClick.RemoveAllListeners();
                    go.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        nowSelectedHeroID = oid;
                        UpdateDesInfo();
                    });
                }
                for (int i = temp.Count; i < heroGoPool.Count; i++)
                {
                    heroGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
                }

                heroListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(413f, 4 + (temp.Count / columns) * 36f));
                doBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                doBtn.onClick.RemoveAllListeners();
                doBtn.onClick.AddListener(delegate () {
                    gc.BuildingManagerAdd(buildingID, nowSelectedHeroID);
                    OnHide();
                });

                break;

        }

    }
}
