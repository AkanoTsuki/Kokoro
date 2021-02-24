using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DistrictMapPanel : BasePanel
{
    public static DistrictMapPanel Instance;

    GameControl gc;
    GameControlInPlay gci;

    #region 【UI控件】
    public Image forceImage;
    public Text nameText;
    public Text levelText;
    public Text peopleText;
    public Text peopleWorkerText;
    public Text peopleFreeText;
    public Text buildingText;

    public Image skyBgImage;
    public Image skyFgImage;

    public Image sceneImage;
    public Image sceneBgImage;
    public List<Image> sceneBorderUpImage;
    public List<Image> sceneBorderMidImage;
    public List<Image> sceneBorderDownImage;

    public List<RectTransform> sceneGroundUpRt;
    public Image sceneWallLeftUpImage;
    public Image sceneWallLeftMidImage;
    public Image sceneWallLeftDownImage;
    public Image sceneWallRightUpImage;
    public Image sceneWallRightMidImage;
    public Image sceneWallRightDownImage;

    public RectTransform tipRt;
    public Text tipText;
    public RectTransform customerInfoRt;
    public Text customerInfo_nameText;
    public Text customerInfo_desText;
    public Image customerInfo_goldImage;

    public Button left_districtMainBtn;
    public Button left_heroMainBtn;
    public Button left_inventoryMainBtn;
    public RectTransform left_inventoryMainNumRt;
    public Text left_inventoryMainNumText;
    public Button left_inventoryScrollBtn;
    public RectTransform left_inventoryScrollNumRt;
    public Text left_inventoryScrollNumText;


    public Button left_marketBtn;
    public Button left_buildBtn;

    public Button right_transferBtn;
    public Button right_adventureSendBtn;

    public Text bottom_baseline_productWeaponText;
    public Text bottom_baseline_productArmorText;
    public Text bottom_baseline_productJewelryText;
    public Text bottom_baseline_productSkillRollText;
    public Text bottom_baseline_productLimitText;

    public Text bottom_baseline_elementWindText;
    public Text bottom_baseline_elementFireText;
    public Text bottom_baseline_elementWaterText;
    public Text bottom_baseline_elementGroundText;
    public Text bottom_baseline_elementLightText;
    public Text bottom_baseline_elementDarkText;

    public Button closeBtn;
    #endregion

    //配置常量
    //float roleHeight = 54f;//人物高度
    Color colorRes = new Color(255 / 255f, 189 / 255f, 88 / 255f, 1f);
    Color colorMake = new Color(221 / 255f, 90 / 255f, 246 / 255f, 1f);
    Color colorBuild = new Color(0 / 255f, 98 / 255f, 251 / 255f, 1f);
    List<Color> colorHourBg = new List<Color> { new Color(100 / 255f, 102 / 255f, 128 / 255f, 1f),//0
        new Color(100 / 255f, 102 / 255f, 128 / 255f, 1f),
        new Color(100 / 255f, 102 / 255f, 128 / 255f, 1f),
        new Color(100 / 255f, 102 / 255f, 128 / 255f, 1f),
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),//6
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),
        new Color(1f, 1f, 1f, 1f),
        new Color(1f, 1f, 1f, 1f),
        new Color(1f, 1f, 1f, 1f),
        new Color(1f, 1f, 1f, 1f),//12
        new Color(1f, 1f, 1f, 1f),
        new Color(1f, 1f, 1f, 1f),
        new Color(1f, 1f, 1f, 1f),
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),//18
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),
        new Color(219 / 255f, 223 / 255f, 245 / 255f, 1f),
        new Color(100 / 255f, 102 / 255f, 128 / 255f, 1f),
        new Color(100 / 255f, 102 / 255f, 128 / 255f, 1f),
        new Color(100 / 255f, 102 / 255f, 128 / 255f, 1f)};
    List<Color> colorHourFg = new List<Color> { new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f) ,//0
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f) ,
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f) ,
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f) ,
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f) ,
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 50 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 50 / 255f),//6
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),//12
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 0 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 50 / 255f),//18
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 50 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f),
        new Color(12 / 255f, 18 / 255f, 48 / 255f, 120 / 255f)};

    //运行变量
    public short nowDistrict = -1;
    //运行变量-选择建筑位置
    GameObject wantBuidingGo;
    int wantBuidingSizeX;
    int wantBuidingSizeY;
    int wantBuidingSizeYBase;
    int wantBuidingPosX;
    int wantBuidingPosY;
    short wantBuidingPID;
    int layerIndex;
    public Canvas canvas;//画布
    public RectTransform contentRt;//坐标
    public List<Transform> layer;
    bool isChoose = false;
    Vector2 wantBuidingPos;
    Vector2 wantBuidingPos_Temp;
    List<int> x = new List<int>();
    List<int> y = new List<int>();
    Vector3 ClickBuildingPos;

    //对象池
    List<GameObject> buildingGoPool = new List<GameObject>();
    public List<GameObject> customerGoPool = new List<GameObject>();//空闲
    Dictionary<int, GameObject> buildingGoDic = new Dictionary<int, GameObject>();//在用
    public Dictionary<int, GameObject> customerGoDic = new Dictionary<int, GameObject>();//在用
    Dictionary<int, Image> statusBarImageDic = new Dictionary<int, Image>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();

        left_districtMainBtn.onClick.AddListener(delegate () { gci.OpenDistrictMain(); });
        left_inventoryMainBtn.onClick.AddListener(delegate () { gci.OpenItemListAndInfo(); });
        left_inventoryScrollBtn.onClick.AddListener(delegate () { gci.OpenSkillListAndInfo(); });
        left_buildBtn.onClick.AddListener(delegate () { gci.OpenBuild(); });
        left_heroMainBtn.onClick.AddListener(delegate () { gci.OpenHeroSelect(gc.nowCheckingDistrictID); });
        left_marketBtn.onClick.AddListener(delegate () { gci.OpenMarket(); });

        right_transferBtn.onClick.AddListener(delegate () { gci.OpenTransfer("To"); });
        right_adventureSendBtn.onClick.AddListener(delegate () { gci.OpenAdventureSend(); });

        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    void Update()
    {
        if (isChoose)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(contentRt, Input.mousePosition, canvas.worldCamera, out wantBuidingPos))
            {
                wantBuidingPos = new Vector2(((int)wantBuidingPos.x / 16) * 16f - wantBuidingSizeX * 16f, ((int)wantBuidingPos.y / 16) * 16f + wantBuidingSizeY * 16f);
                wantBuidingGo.GetComponent<RectTransform>().anchoredPosition = wantBuidingPos;

                if (wantBuidingPos != wantBuidingPos_Temp)
                {
                    if (wantBuidingPos.y != wantBuidingPos_Temp.y)
                    {
                        layerIndex = ((int)wantBuidingPos.y / 16) * -1 + wantBuidingSizeY - 1;
                        if (layerIndex >= 0 && layerIndex < 19)
                        {
                            wantBuidingGo.transform.SetParent(layer[layerIndex].transform);
                        }
                    }
                    if (CheckCanBuild())
                    {
                        wantBuidingGo.GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        wantBuidingGo.GetComponent<Image>().color = Color.red;
                    }
                    wantBuidingPos_Temp = wantBuidingPos;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (CheckCanBuild())
                {
                    ToBuild();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                ShopChoosePosition();
            }
        }

    }

    //主面板显示
    public override void OnShow()
    {
        if (gc.nowCheckingDistrictID != nowDistrict)
        {
            nowDistrict = gc.nowCheckingDistrictID;
            UpdateAllCustomer(nowDistrict);
            UpdateScene(nowDistrict);
        }

        SetAnchoredPosition(0, -90);
        UpdateBasicInfo();
        UpdateAllBuilding(gc.nowCheckingDistrictID);
        HideTip();

        HideCustomerInfo();

        UpdateBaselineResourcesText(gc.nowCheckingDistrictID);
        UpdateBaselineElementText(gc.nowCheckingDistrictID);
        ChangeSkyColor();

        if (gc.districtDic[gc.nowCheckingDistrictID].force == 0)
        {
            UpdateButtonItemNum(gc.nowCheckingDistrictID);
            UpdateButtonScrollNum(gc.nowCheckingDistrictID);
        }
        SetFunctionButton(gc.districtDic[gc.nowCheckingDistrictID].force == 0);

        if (DataManager.mDistrictDict[gc.nowCheckingDistrictID].Music!="")
        {
            AudioControl.Instance.PlayMusic(DataManager.mDistrictDict[gc.nowCheckingDistrictID].Music);
            AudioControl.Instance.nowMusic = DataManager.mDistrictDict[gc.nowCheckingDistrictID].Music;
        }

        InvokeRepeating("UpdateBar", 0, 0.2f);

        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Instance.enabled = true;
        isShow = true;
    }

    //主面板关闭
    public override void OnHide()
    {
        if (isChoose == true)
        {
            ShopChoosePosition();
        }
        if (BuildPanel.Instance.isShow)
        {
            BuildPanel.Instance.OnHide();
        }
        if (BuildingPanel.Instance.isShow)
        {
            BuildingPanel.Instance.OnHide();
        }
        if (DistrictMainPanel.Instance.isShow)
        {
            DistrictMainPanel.Instance.OnHide();
        }

        CancelInvoke("UpdateBar");

        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        Instance.enabled = false;
        isShow = false;
    }

 
    //功能按钮组-设置
    void SetFunctionButton(bool isOwn)
    {
        if (isOwn)
        {
            left_inventoryMainBtn.transform.localScale = Vector2.one;
            left_inventoryScrollBtn.transform.localScale = Vector2.one;
            left_marketBtn.transform.localScale = Vector2.one;
            left_buildBtn.transform.localScale = Vector2.one;
        }
        else
        {
            left_inventoryMainBtn.transform.localScale = Vector2.zero;
            left_inventoryScrollBtn.transform.localScale = Vector2.zero;
            left_marketBtn.transform.localScale = Vector2.zero;
            //TODO:开发配置阶段建筑按钮都开启 正常为zero
            left_buildBtn.transform.localScale = Vector2.zero;
        }
    }

    //功能按钮组-装备产出按钮-数量文本-更新
    public void UpdateButtonItemNum(short districtID)
    {
        int num = 0;
        foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
        {
            if (kvp.Value.districtID == districtID && kvp.Value.heroID == -1 && kvp.Value.isGoods == false)
            {
                num++;
            }
        }

        if (num > 0)
        {
            left_inventoryMainNumText.text = num.ToString();
            left_inventoryMainNumRt.sizeDelta = new Vector2(left_inventoryMainNumText.preferredWidth + 8f, 20f);
        }
        else
        {
            left_inventoryMainNumText.text = "";
            left_inventoryMainNumRt.sizeDelta = Vector2.zero;
        }

    }

    //功能按钮组-卷轴产出按钮-数量文本-更新
    public void UpdateButtonScrollNum(short districtID)
    {
        int num = 0;
        foreach (KeyValuePair<int, SkillObject> kvp in gc.skillDic)
        {
            if (kvp.Value.districtID == districtID && kvp.Value.heroID == -1 && kvp.Value.isGoods == false)
            {
                num++;
            }
        }

        if (num > 0)
        {
            left_inventoryScrollNumText.text = num.ToString();
            left_inventoryScrollNumRt.sizeDelta = new Vector2(left_inventoryScrollNumText.preferredWidth + 8f, 20f);
        }
        else
        {
            left_inventoryScrollNumText.text = "";
            left_inventoryScrollNumRt.sizeDelta = Vector2.zero;
        }
    }


    //顶部数据栏目-更新
    public void UpdateBasicInfo()
    {
        forceImage.overrideSprite = Resources.Load("Image/Other/icon_flag_" + gc.forceDic[gc.districtDic[gc.nowCheckingDistrictID].force].flagIndex + "_a", typeof(Sprite)) as Sprite;
        nameText.text = gc.districtDic[gc.nowCheckingDistrictID].name ;
        levelText.text = gc.districtDic[gc.nowCheckingDistrictID].level.ToString();
        peopleText.text = "居民 " + gc.districtDic[gc.nowCheckingDistrictID].people+"/" + gc.districtDic[gc.nowCheckingDistrictID].peopleLimit;
        peopleWorkerText.text = gc.districtDic[gc.nowCheckingDistrictID].worker.ToString();
        peopleFreeText.text = (gc.districtDic[gc.nowCheckingDistrictID].people-gc.districtDic[gc.nowCheckingDistrictID].worker).ToString();

        buildingText.text ="设施 "+ gc.districtDic[gc.nowCheckingDistrictID].buildingList.Count; 
    }

    //底部数据栏目-资源子栏目-更新
    public void UpdateBaselineResourcesText(short districtID)
    {
        if (gc.districtDic[districtID].force == 0)
        {
            bottom_baseline_productWeaponText.text = gc.districtDic[districtID].rProductWeapon + "<color=#92FF9D>[在售 " + gc.districtDic[districtID].rProductGoodWeapon + "]</color>";
            bottom_baseline_productArmorText.text = gc.districtDic[districtID].rProductArmor + "<color=#92FF9D>[在售 " + gc.districtDic[districtID].rProductGoodArmor + "]</color>";
            bottom_baseline_productJewelryText.text = gc.districtDic[districtID].rProductJewelry + "<color=#92FF9D>[在售 " + gc.districtDic[districtID].rProductGoodJewelry + "]</color>";
            bottom_baseline_productSkillRollText.text = gc.districtDic[districtID].rProductScroll + "<color=#92FF9D>[在售 " + gc.districtDic[districtID].rProductGoodScroll + "]</color>";
            bottom_baseline_productLimitText.text = gc.GetDistrictProductAll(districtID) + "/" + gc.districtDic[districtID].rProductLimit;
        }
        else
        {
            bottom_baseline_productWeaponText.text = "-";
            bottom_baseline_productArmorText.text = "-";
            bottom_baseline_productJewelryText.text = "-";
            bottom_baseline_productSkillRollText.text = "-";
            bottom_baseline_productLimitText.text = "-";
        }
    }

    //底部数据栏目-元素子栏目-更新
    public void UpdateBaselineElementText(short districtID)
    {
        bottom_baseline_elementWindText.text = "风 " + gc.districtDic[districtID].eWind;
        bottom_baseline_elementFireText.text = "火 " + gc.districtDic[districtID].eFire;
        bottom_baseline_elementWaterText.text = "水 " + gc.districtDic[districtID].eWater;
        bottom_baseline_elementGroundText.text = "地 " + gc.districtDic[districtID].eGround;
        bottom_baseline_elementLightText.text = "光 " + gc.districtDic[districtID].eLight;
        bottom_baseline_elementDarkText.text = "暗 " + gc.districtDic[districtID].eDark;
    }


    //建筑建造-开始选择建筑位置
    public void ChoosePosition(short buildingID)
    {
        if (isChoose == true)
        {
            MessagePanel.Instance.AddMessage("正在建造指定中，无法再次选择");
            return;
        }

        wantBuidingPID = buildingID;
        wantBuidingGo = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisBuilding")) as GameObject;
        wantBuidingSizeX = DataManager.mBuildingDict[buildingID].SizeX;
        wantBuidingSizeY = DataManager.mBuildingDict[buildingID].SizeY;
        wantBuidingSizeYBase = DataManager.mBuildingDict[buildingID].SizeYBase;
        wantBuidingGo.GetComponent<RectTransform>().sizeDelta = new Vector2(wantBuidingSizeX * 16f, wantBuidingSizeY * 16f);
        wantBuidingGo.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(wantBuidingSizeX * 16f, wantBuidingSizeYBase * 16f);
        wantBuidingGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + DataManager.mBuildingDict[buildingID].MainPic);
        wantBuidingGo.transform.GetChild(2).GetComponent<RectTransform>().localScale = Vector2.zero;

         isChoose = true;

        ShowTip("请指定建造位置（右键取消）");
    }

    //建筑建造-中止选择建筑位置
    public void ShopChoosePosition()
    {
        if (isChoose == false)
        {
            return;
        }

        HideTip();
        Destroy(wantBuidingGo);
        isChoose = false;
    }

    //建筑建造-检查位置是否可用
    bool CheckCanBuild()
    {
        x.Clear();
        y.Clear();
         wantBuidingPosX = ((int)wantBuidingPos.x / 16);
         wantBuidingPosY = ((int)wantBuidingPos.y / 16) * -1 + (wantBuidingSizeY - wantBuidingSizeYBase);
       // Debug.Log("左上角点(占地)=" + wantBuidingPosX + "," + wantBuidingPosY);

        for (int i = 0; i < wantBuidingSizeX; i++)
        {
            for (int j = 0; j < wantBuidingSizeYBase; j++)
            {
                x.Add(wantBuidingPosX + i);
                y.Add(wantBuidingPosY + j);
            }
        }

        bool can = true;
        for (int i = 0; i < x.Count; i++)
        {
            string index = gc.nowCheckingDistrictID + "_" + x[i] + "," + y[i];
            if (gc.districtGridDic[gc.nowCheckingDistrictID].ContainsKey(index))
            {
                if (gc.districtGridDic[gc.nowCheckingDistrictID][index].buildingID != -1|| gc.districtGridDic[gc.nowCheckingDistrictID][index].level>gc.districtDic[gc.nowCheckingDistrictID].level)
                {
                    can = false;
                    break;
                }
            }
            else 
            {
                can = false;
                break;
            }
        }

        if (DataManager.mBuildingDict[wantBuidingPID].PanelType == "Forge")
        {
            //Debug.Log("wantBuidingPosY=" + wantBuidingPosY + " wantBuidingSizeYBase=" + wantBuidingSizeYBase);
            if ((wantBuidingPosY + wantBuidingSizeYBase-1) != 10 && (wantBuidingPosY + wantBuidingSizeYBase-1) != 18)
            {
                can = false;
            }
        }
        return can;       
    }

    //建筑建造-执行确定建筑指令
    void ToBuild()
    {
        isChoose = false;
        ClickBuildingPos = Input.mousePosition;

        HideTip();
        gc.CreateBuildEvent(wantBuidingPID, (short)wantBuidingPosX, (short)wantBuidingPosY, (byte)layerIndex,x,y);
        SetBuilding(gc.buildingIndex-1,true,-1);
        Destroy(wantBuidingGo);
    }

    //据点场景-天空颜色变化
    public void ChangeSkyColor()
    {
        skyBgImage.DOColor(colorHourBg[gc.timeHour], 1f);
        skyFgImage.DOColor(colorHourFg[gc.timeHour], 1f);
    }

    //据点场景-更新（地表、背景、边界物体群）
    void UpdateScene(int districtID)
    {
        sceneImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/" +DataManager.mDistrictDict[districtID].ScenePic);
        sceneBgImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/" + DataManager.mDistrictDict[districtID].SceneBgPic);

        if (districtID == 0 || districtID == 4)
        {
            for (int i = 0; i < 5; i++)
            {
                if (gc.districtDic[districtID].level > i)
                {
                    sceneGroundUpRt[i].localScale = Vector2.one;
                }
                else
                {
                    sceneGroundUpRt[i].localScale = Vector2.zero;
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                sceneGroundUpRt[i].localScale = Vector2.zero;
            }
        }

        switch (gc.districtDic[districtID].wallLevel)
        {
            case 0:
                sceneWallLeftUpImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
                sceneWallLeftMidImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
                sceneWallLeftDownImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
                sceneWallRightUpImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
                sceneWallRightMidImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
                sceneWallRightDownImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
                break;
            case 1: 
                sceneWallLeftUpImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_wood_up");
                sceneWallLeftMidImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_wood_mid");
                sceneWallLeftDownImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_wood_mid");
                sceneWallRightUpImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_wood_up");
                sceneWallRightMidImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_wood_mid");
                sceneWallRightDownImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_wood_mid");
                break;
            case 2:
                sceneWallLeftUpImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_stone_up");
                sceneWallLeftMidImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_stone_mid");
                sceneWallLeftDownImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_stone_mid");
                sceneWallRightUpImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_stone_up");
                sceneWallRightMidImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_stone_mid");
                sceneWallRightDownImage.sprite = Resources.Load<Sprite>("Image/DistrictBG/sw_stone_mid");
                break;
        }
        switch (gc.districtDic[districtID].level)
        {
            case 1:
                sceneWallLeftUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-320f, 64f);
                sceneWallLeftMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-320f, -64f);
                sceneWallLeftDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-320f, -192f);
                sceneWallRightUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(320f, 64f);
                sceneWallRightMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(320f, -64f);
                sceneWallRightDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(320f, -192f);
                break;
            case 2:
                sceneWallLeftUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640f, 64f);
                sceneWallLeftMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640f, -64f);
                sceneWallLeftDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640f, -192f);
                sceneWallRightUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(320f, 64f);
                sceneWallRightMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(320f, -64f);
                sceneWallRightDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(320f, -192f);
                break;
            case 3:
                sceneWallLeftUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640f, 64f);
                sceneWallLeftMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640f, -64f);
                sceneWallLeftDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640f, -192f);
                sceneWallRightUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(640f, 64f);
                sceneWallRightMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(640f, -64f);
                sceneWallRightDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(640f, -192f);
                break;
            case 4:
                sceneWallLeftUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960f, 64f);
                sceneWallLeftMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960f, -64f);
                sceneWallLeftDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960f, -192f);
                sceneWallRightUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(640f, 64f);
                sceneWallRightMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(640f, -64f);
                sceneWallRightDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(640f, -192f);
                break;
            case 5:
                sceneWallLeftUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960f, 64f);
                sceneWallLeftMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960f, -64f);
                sceneWallLeftDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960f, -96f);
                sceneWallRightUpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(960f, 64f);
                sceneWallRightMidImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(960f, -192f);
                sceneWallRightDownImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(960f, -192f);
                break;
        }

        for (int i = 0; i < 4; i++)
        {
            if (gc.districtDic[districtID].level > (i+1))
            {
                sceneBorderUpImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
                sceneBorderMidImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
                sceneBorderDownImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/Empty");
            }
            else
            {
                sceneBorderUpImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/" + DataManager.mDistrictDict[districtID].SceneBorderUpPic[i]);
                sceneBorderMidImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/" + DataManager.mDistrictDict[districtID].SceneBorderMidPic[i]);
                sceneBorderDownImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/" + DataManager.mDistrictDict[districtID].SceneBorderDownPic[i]);
            }
        }

        for (int i = 4; i < 6; i++)
        {
            sceneBorderUpImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/" + DataManager.mDistrictDict[districtID].SceneBorderUpPic[i]);
            sceneBorderMidImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/" + DataManager.mDistrictDict[districtID].SceneBorderMidPic[i]);
            sceneBorderDownImage[i].sprite = Resources.Load<Sprite>("Image/DistrictBG/" + DataManager.mDistrictDict[districtID].SceneBorderDownPic[i]);
        }

    }

    //据点场景-建筑物（全部）-更新
    void UpdateAllBuilding(int districtID)
    {
        List<BuildingObject> temp = new List<BuildingObject> { };
        foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
        {
            if (kvp.Value.districtID == districtID)
            {
                temp.Add(kvp.Value);
            }
        }
        buildingGoDic.Clear();
        for (int i = 0; i < temp.Count; i++)
        {
            SetBuilding(temp[i].id,false,i);
        }
        for (int i = temp.Count; i < buildingGoPool.Count; i++)
        {
            buildingGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    //据点场景-建筑物（单个）-更新
    public void UpdateSingleBuilding( int buildingID)
    {
        GameObject go = buildingGoDic[buildingID];
        UpdateBuildingGo(go, buildingID);
    }

    //据点场景-建筑物块-创建
    void SetBuilding(int buildingID, bool isNew, int index)
    {
        GameObject go;

        if (isNew)
        {
            go = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisBuilding")) as GameObject;
            buildingGoPool.Add(go);
        }
        else
        {
            if (index < buildingGoPool.Count)
            {
                go = buildingGoPool[index];
                buildingGoPool[index].transform.GetComponent<RectTransform>().localScale = Vector2.one;
                
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisBuilding")) as GameObject;
                buildingGoPool.Add(go);
            }
        }
        buildingGoDic.Add(buildingID, go); 
        UpdateBuildingGo(go, buildingID);
    }

    //据点场景-建筑物块-删除
    public void DeleteBuilding(int buildingID)
    {
        GameObject go = buildingGoDic[buildingID];

        go.transform.GetComponent<RectTransform>().localScale = Vector2.zero;

        buildingGoDic.Remove(buildingID);
        if (statusBarImageDic.ContainsKey(buildingID))
        {
            statusBarImageDic.Remove(buildingID);
        }
    }

    //据点场景-建筑物信息（名字标签、运行状态等）-更新
    void UpdateBuildingGo(GameObject go,int buildingID)
    {
        go.name = buildingID.ToString();
        go.transform.SetParent(layer[gc.buildingDic[buildingID].layer].transform);

        go.GetComponent<RectTransform>().sizeDelta = new Vector2(DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeX * 16f, DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeY * 16f);

        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(gc.buildingDic[buildingID].positionX * 16f, (gc.buildingDic[buildingID].positionY - (DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeY - DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase)) * -16f);
        Transform baseTf = go.transform.GetChild(0);
        Transform nameBgTf = go.transform.GetChild(1);
        Transform nameTf = go.transform.GetChild(1).GetChild(0);
        Transform statusTf = go.transform.GetChild(2);
        Transform statusBarTf = go.transform.GetChild(2).GetChild(0);
        Transform statusSubTf = go.transform.GetChild(2).GetChild(1);

        if (!statusBarImageDic.ContainsKey(buildingID))
        {
            statusBarImageDic.Add(buildingID, go.transform.GetChild(2).GetChild(0).GetComponent<Image>());
        }

        nameTf.GetComponent<Text>().text = gc.buildingDic[buildingID].name;
        nameBgTf.GetComponent<RectTransform>().sizeDelta = new Vector2(nameTf.GetComponent<Text>().preferredWidth + 6f, nameBgTf.GetComponent<RectTransform>().sizeDelta.y);

        if (gc.buildingDic[buildingID].buildProgress == 0)
        {

            go.GetComponent<Image>().color = Color.clear;
            baseTf.GetComponent<Image>().color = Color.white;
            baseTf.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildNow/BuildNow_" + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeX + "x" + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase);
            baseTf.GetComponent<RectTransform>().sizeDelta = new Vector2(DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeX * 16f, DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase * 16f);

            statusTf.GetComponent<RectTransform>().localScale = Vector2.one;
            statusSubTf.GetComponent<Text>().text = "建筑中";
            statusBarTf.GetComponent<RectTransform>().localScale = Vector2.one;
            statusBarImageDic[buildingID].color = colorBuild;
        }
        else if (gc.buildingDic[buildingID].buildProgress == 2)
        {

            go.GetComponent<Image>().color = Color.clear;
            baseTf.GetComponent<Image>().color = Color.white;
            baseTf.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildNow/BuildNow_" + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeX + "x" + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase);
            baseTf.GetComponent<RectTransform>().sizeDelta = new Vector2(DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeX * 16f, DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase * 16f);

            statusTf.GetComponent<RectTransform>().localScale = Vector2.one;
            statusSubTf.GetComponent<Text>().text = "升级中";
            statusBarTf.GetComponent<RectTransform>().localScale = Vector2.one;
            statusBarImageDic[buildingID].color = colorBuild;
        }
        else if (gc.buildingDic[buildingID].buildProgress == 1)
        {
            go.GetComponent<Image>().color = Color.white;
            go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + gc.buildingDic[buildingID].mainPic);


            baseTf.GetComponent<Image>().color = Color.clear;

            switch (gc.buildingDic[buildingID].panelType)
            {
                case "House":
                    statusTf.GetComponent<RectTransform>().localScale = Vector2.zero;
                    break;
                case "Resource":
                    statusTf.GetComponent<RectTransform>().localScale = Vector2.one;
                    if (gc.buildingDic[buildingID].isOpen)
                    {
                        statusSubTf.GetComponent<Text>().text = "<color=#FFDC7C>生产中</color>";
                        statusBarTf.GetComponent<RectTransform>().localScale = Vector2.one;
                        statusBarImageDic[buildingID].color = colorRes;
                    }
                    else
                    {
                        statusSubTf.GetComponent<Text>().text = "<color=#FF4500></color>";
                        statusBarTf.GetComponent<RectTransform>().localScale = Vector2.zero;
                    }
                    break;
                case "Forge":
                    statusTf.GetComponent<RectTransform>().localScale = Vector2.one;
                    if (gc.buildingDic[buildingID].isOpen)
                    {
                        statusSubTf.GetComponent<Text>().text = "<color=#D583EC>" + gc.OutputItemTypeSmallStr(DataManager.mProduceEquipDict[gc.buildingDic[buildingID].taskList[0].produceEquipNow].Type) + "(" + DataManager.mProduceEquipDict[gc.buildingDic[buildingID].taskList[0].produceEquipNow].Level + ")制作中</color>";
                        statusBarTf.GetComponent<RectTransform>().localScale = Vector2.one;
                        statusBarImageDic[buildingID].color = colorMake;
                    }
                    else
                    {
                        statusSubTf.GetComponent<Text>().text = "<color=#FF4500></color>";
                        statusBarTf.GetComponent<RectTransform>().localScale = Vector2.zero;
                    }
                    break;
                case "Municipal":
                    statusTf.GetComponent<RectTransform>().localScale = Vector2.zero;
                    break;
                case "Inn":
                    statusTf.GetComponent<RectTransform>().localScale = Vector2.zero;
                    break;
                case "Military":
                    statusTf.GetComponent<RectTransform>().localScale = Vector2.zero;
                    break;
            }
        }

        go.transform.GetComponent<Button>().onClick.RemoveAllListeners();
        go.transform.GetComponent<Button>().onClick.AddListener(delegate () {
            if (ClickBuildingPos != Input.mousePosition)
            {
                BuildingPanel.Instance.OnShow(gc.buildingDic[buildingID]);
            }

        });
    }

    //据点场景-建筑物运行状态（全部建筑）-更新
    void UpdateBar()
    {
        for (int i = 0; i < gc.executeEventList.Count; i++)
        {
            if (gc.executeEventList[i].type == ExecuteEventType.Build)
            {
              
                if (gc.executeEventList[i].value[0][0] == gc.nowCheckingDistrictID)
                {
                    statusBarImageDic[gc.executeEventList[i].value[1][0]].fillAmount = (float)(gc.standardTime - gc.executeEventList[i].startTime) / (gc.executeEventList[i].endTime - gc.executeEventList[i].startTime);
                }
            }
            else if (gc.executeEventList[i].type == ExecuteEventType.BuildingUpgrade)
            {
                if (gc.executeEventList[i].value[0][0] == gc.nowCheckingDistrictID)
                {
                    statusBarImageDic[gc.executeEventList[i].value[1][0]].fillAmount = (float)(gc.standardTime - gc.executeEventList[i].startTime) / (gc.executeEventList[i].endTime - gc.executeEventList[i].startTime);
                }
            }
            else if (gc.executeEventList[i].type == ExecuteEventType.ProduceResource)
            {
                if (gc.executeEventList[i].value[0][0] == gc.nowCheckingDistrictID)
                {
                    statusBarImageDic[gc.executeEventList[i].value[1][0]].fillAmount = (float)(gc.standardTime - gc.executeEventList[i].startTime) / (gc.executeEventList[i].endTime - gc.executeEventList[i].startTime);
                }
            }
            else if (gc.executeEventList[i].type == ExecuteEventType.ProduceItem)
            {
                if (gc.executeEventList[i].value[0][0] == gc.nowCheckingDistrictID)
                {
                    statusBarImageDic[gc.executeEventList[i].value[1][0]].fillAmount = (float)(gc.standardTime - gc.executeEventList[i].startTime) / (gc.executeEventList[i].endTime - gc.executeEventList[i].startTime);
                }
            }
        }

    }

    //提示信息条-显示
    void ShowTip(string str)
    {
        tipRt.localScale = Vector2.one;
        tipText.text = str;
    }

    //提示信息条-隐藏
    void HideTip()
    {
        tipRt.localScale = Vector2.zero;
    }


    //访客-初始化
    public void InitCustomer()
    {
        List<CustomerObject> temp = new List<CustomerObject> { };
        foreach (KeyValuePair<int, CustomerObject> kvp in gc.customerDic)
        {         
            temp.Add(kvp.Value);
        }

        for (int i = 0; i < temp.Count; i++)
        {
            SetCustomer(temp[i].id);
            gc.UpdateCustomerByStage(temp[i].id);
        }
    }

    //访客-设置GO显示/隐藏 通过缩放
    public void UpdateAllCustomer(int districtID)
    {
        foreach (KeyValuePair<int, CustomerObject> kvp in gc.customerDic)
        {
            if (kvp.Value.districtID == districtID)
            {
                customerGoDic[kvp.Key].transform.localScale = new Vector2(1f,1.25f);
                customerGoDic[kvp.Key].GetComponent<AnimatiorControlByNPC>().isShow = true;
            }
            else
            {
                customerGoDic[kvp.Key].transform.localScale = Vector2.zero;
                customerGoDic[kvp.Key].GetComponent<AnimatiorControlByNPC>().isShow = false;
            }
        }
    }

    //访客-创建实例 
    public void SetCustomer(int customerID)
    {
        GameObject go;

        if (customerGoPool.Count > 0)
        {
            go = customerGoPool[0];
            customerGoPool.RemoveAt(0);
            go.GetComponent<AnimatiorControlByNPC>().enabled = true;
        }
        else
        {
            go = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisCustomer")) as GameObject;
        }
        customerGoDic.Add(customerID, go);

        go.name = "Customer_" + customerID;
        go.GetComponent<AnimatiorControlByNPC>().customerID = customerID;
        go.GetComponent<AnimatiorControlByNPC>().SetCharaFrames(gc.customerDic[customerID].pic);
 

        if (gc.customerDic[customerID].buildingID != -1)
        {
            int buildingID = gc.customerDic[customerID].buildingID;      
            gc.customerDic[customerID].layer =(byte)( gc.buildingDic[buildingID].layer+1);
            go.GetComponent<AnimatiorControlByNPC>().SetAnim(gc.buildingDic[buildingID].doorInLine);
        }
        else
        {
            if(gc.customerDic[customerID].layer==0)
            {
                int ranY = Random.Range(0, 2);
                int layerIndex = (ranY == 0 ? 11 : 19);

                gc.customerDic[customerID].layer = (byte)layerIndex;
            }
    
            go.GetComponent<AnimatiorControlByNPC>().SetAnim(Random.Range(0,2)==0? AnimStatus.WalkLeft: AnimStatus.WalkRight);

        }
        go.GetComponent<AnimatiorControlByNPC>().Stop();
        go.transform.SetParent(layer[gc.customerDic[customerID].layer].transform);

        if (nowDistrict == gc.customerDic[customerID].districtID)
        {
            go.transform.localScale = new Vector2(1f, 1.25f);
            go.transform.GetComponent<AnimatiorControlByNPC>().isShow = true;
        }
        else
        {
            go.transform.localScale = Vector2.zero;
            go.transform.GetComponent<AnimatiorControlByNPC>().isShow = false;
        }

        SetCustomerPos(customerID);
    }

    //访客-初始化位置
    void SetCustomerPos(int customerID)
    {
        float roleHeight = 54f;
        //Debug.Log("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/Customer_" + customerID);
        //Debug.Log("gc.customerDic[customerID].stage=" + gc.customerDic[customerID].stage);
        //Debug.Log("gc.customerDic[customerID].buildingID=" + gc.customerDic[customerID].buildingID);
        //Debug.Log("gc.customerDic[customerID].buildingID=" + gc.customerDic[customerID].);

        //GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/Customer_" + customerID);
        GameObject go = customerGoDic[customerID];

        int buildingID = gc.customerDic[customerID].buildingID;
        int waitIndex;
        int ranY;
        int layerIndex;
        Vector2 startPos;
        switch (gc.customerDic[customerID].stage)
        {
            case CustomerStage.Come:
                int startX = 20 * gc.districtDic[gc.customerDic[customerID].districtID].level + Random.Range(10, 100);
                startPos = new Vector2((64 + startX * (Random.Range(0, 2) == 0 ? -1 : 1)) * 16f, gc.customerDic[customerID].layer * -16f + roleHeight);
                break;
            case CustomerStage.Observe:
                startPos = new Vector2(Random.Range(58, 72) * 16f, gc.customerDic[customerID].layer * -16f + roleHeight);
                break;
            case CustomerStage.Wait:
                waitIndex = gc.buildingDic[buildingID].customerList.IndexOf(customerID);
                startPos = new Vector2((gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition + (gc.buildingDic[buildingID].positionY < 64 ? (1 + waitIndex) * -1 : waitIndex)) * 16f, gc.customerDic[customerID].layer * -16f + 54f);
                break;
            case CustomerStage.IntoShop:
                waitIndex = 0;
                startPos = new Vector2((gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition + (gc.buildingDic[buildingID].positionY < 64 ? (1 + waitIndex) * -1 : waitIndex)) * 16f, gc.customerDic[customerID].layer * -16f + 54f);
                break;
            case CustomerStage.Gone:
                if (buildingID != -1)
                {
                    waitIndex = gc.buildingDic[buildingID].customerList.IndexOf(customerID);
                    startPos = new Vector2((gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition + (gc.buildingDic[buildingID].positionY < 64 ? (1 + waitIndex) * -1 : waitIndex)) * 16f, gc.customerDic[customerID].layer * -16f + 54f);
                }
                else
                {
                    ranY = Random.Range(0, 2);
                    layerIndex = (ranY == 0 ? 11 : 18);
                    startPos = new Vector2(Random.Range(58, 72) * 16f, layerIndex * -16f + roleHeight);
                }
                break;
            default:
                startPos = Vector2.zero;
                break;
        }
        go.GetComponent<RectTransform>().anchoredPosition = startPos;
    }

    //访客-说话
    public IEnumerator CustomerTalk(int customerID,string talkContent,float delay)
    {
        yield return new WaitForSeconds(delay);
       // GameObject cgo= GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/Customer_" + customerID);
        GameObject cgo = null;
        if (customerGoDic.ContainsKey(customerID))
        {
            cgo = customerGoDic[customerID];
        }

        if (cgo != null)
        {
            if (talkContent != "")
            {
                GameObject go = Instantiate(Resources.Load("Prefab/Moment/Moment_Talk")) as GameObject;
                go.transform.SetParent(cgo.transform);
                go.GetComponent<MomentTalk>().Show(talkContent, 0, new Vector2(0, 40f));
            }
        }
        else
        {
            Debug.LogWarning("CustomerTalk已丢失 customerID="+ customerID);
        }
    }

    //访客-浮动信息块-显示
    public void ShowCustomerInfo(int customerID,Vector2 pos)
    {
        customerInfoRt.anchoredPosition = pos + Vector2.up * 50f;
        customerInfo_nameText.text = gc.customerDic[customerID].name;
        string str = "";

        if (gc.customerDic[customerID].shopType == ShopType.WeaponAndSubhand
        || gc.customerDic[customerID].shopType == ShopType.Armor
        || gc.customerDic[customerID].shopType == ShopType.Jewelry)
        {
            if (gc.customerDic[customerID].bucketList.prototypeID != -1)
            {
                str += "[" + DataManager.mItemDict[gc.customerDic[customerID].bucketList.prototypeID].Name + "]";
            }
            else
            {
                if (gc.customerDic[customerID].bucketList.typeSmall != ItemTypeSmall.None)
                {
                    switch (gc.customerDic[customerID].bucketList.typeSmall)
                    {
                        case ItemTypeSmall.Sword: str += "[剑类武器]"; break;
                        case ItemTypeSmall.Axe: str += "[斧镰类武器]"; break;
                        case ItemTypeSmall.Spear: str += "[枪矛类武器]"; break;
                        case ItemTypeSmall.Hammer: str += "[锤棍类武器]"; break;
                        case ItemTypeSmall.Bow: str += "[弓类武器]"; break;
                        case ItemTypeSmall.Staff: str += "[杖类武器]"; break;
                        case ItemTypeSmall.Shield: str += "[盾]"; break;
                        case ItemTypeSmall.Dorlach: str += "[箭袋]"; break;
                        case ItemTypeSmall.HeadH: str += "[重型头部防具]"; break;
                        case ItemTypeSmall.BodyH: str += "[重型身体防具]"; break;
                        case ItemTypeSmall.HandH: str += "[重型手部防具]"; break;
                        case ItemTypeSmall.BackH: str += "[重型背部防具]"; break;
                        case ItemTypeSmall.FootH: str += "[重型腿部防具]"; break;
                        case ItemTypeSmall.HeadL: str += "[轻型头部防具]"; break;
                        case ItemTypeSmall.BodyL: str += "[轻型身体防具]"; break;
                        case ItemTypeSmall.HandL: str += "[轻型手部防具]"; break;
                        case ItemTypeSmall.BackL: str += "[轻型背部防具]"; break;
                        case ItemTypeSmall.FootL: str += "[轻型腿部防具]"; break;
                        case ItemTypeSmall.Neck: str += "[项链]"; break;
                        case ItemTypeSmall.Finger: str += "[指环]"; break;
                    }
                }
                else
                {
                    switch (gc.customerDic[customerID].bucketList.typeBig)
                    {
                        case ItemTypeBig.Weapon: str += "[武器]"; break;
                        case ItemTypeBig.Subhand: str += "[副手]"; break;
                        case ItemTypeBig.Armor: str += "[防具]"; break;
                        case ItemTypeBig.Jewelry: str += "[饰品]"; break;
                    }
                }
            }
        }
        else if (gc.customerDic[customerID].shopType == ShopType.Scroll)
        {
            if (gc.customerDic[customerID].bucketList.prototypeID != -1)
            {
                str += "[" + DataManager.mSkillDict[gc.customerDic[customerID].bucketList.prototypeID].Name + "卷轴]";
            }
            else
            {
                if (gc.customerDic[customerID].bucketList.typeSmall != ItemTypeSmall.None)
                {
                    switch (gc.customerDic[customerID].bucketList.typeSmall)
                    {
                        case ItemTypeSmall.ScrollWindI: str += "[风系卷轴]"; break;
                        case ItemTypeSmall.ScrollFireI: str += "[火系卷轴]"; break;
                        case ItemTypeSmall.ScrollWaterI: str += "[水系卷轴]"; break;
                        case ItemTypeSmall.ScrollGroundI: str += "[地系卷轴]"; break;
                        case ItemTypeSmall.ScrollLightI: str += "[光系卷轴]"; break;
                        case ItemTypeSmall.ScrollDarkI: str += "[暗系卷轴]"; break;
                        case ItemTypeSmall.ScrollNone: str += "[无元素卷轴]"; break;
                        case ItemTypeSmall.ScrollWindII: str += "[雷系卷轴]"; break;
                        case ItemTypeSmall.ScrollFireII: str += "[爆炸系卷轴]"; break;
                        case ItemTypeSmall.ScrollWaterII: str += "[冰系卷轴]"; break;
                        case ItemTypeSmall.ScrollGroundII: str += "[自然系卷轴]"; break;
                        case ItemTypeSmall.ScrollLightII: str += "[时空系卷轴]"; break;
                        case ItemTypeSmall.ScrollDarkII: str += "[死亡系卷轴]"; break;
                    }
                }
                else
                {
                    str += "[卷轴]";
                }
            }
        }

        customerInfo_desText.text = "<color=#" + DataManager.mHeroDict[gc.customerDic[customerID].heroType].Color + ">" + DataManager.mHeroDict[gc.customerDic[customerID].heroType].Name + "</color>想要"+ str;

       // Debug.Log(gc.customerDic[customerID].gold.ToString());
        if (gc.customerDic[customerID].gold < 200)
        {
            customerInfo_goldImage.overrideSprite = Resources.Load("Image/Other/icon361", typeof(Sprite)) as Sprite;
        }
        else if (gc.customerDic[customerID].gold >= 200&& gc.customerDic[customerID].gold < 500)
        {
            customerInfo_goldImage.overrideSprite = Resources.Load("Image/Other/icon362", typeof(Sprite)) as Sprite;
        }
        else if (gc.customerDic[customerID].gold >= 500 && gc.customerDic[customerID].gold < 1000)
        {
            customerInfo_goldImage.overrideSprite = Resources.Load("Image/Other/icon366", typeof(Sprite)) as Sprite;
        }
        else 
        {
            customerInfo_goldImage.overrideSprite = Resources.Load("Image/Other/icon367", typeof(Sprite)) as Sprite;
        }
    }

    //访客-浮动信息块-隐藏
    public void HideCustomerInfo()
    {
        customerInfoRt.anchoredPosition =  Vector2.up * 5000f;
    }

    


}
