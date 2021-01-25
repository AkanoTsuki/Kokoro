using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DistrictMapPanel : BasePanel
{
    GameControl gc;
    GameControlInPlay gci;
  
    public static DistrictMapPanel Instance;

    public Text nameText;
    public Text levelText;
    public Text peopleText;
    public Text buildingText;

    public Image skyBgImage;
    public Image skyFgImage;

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

    public Button bottom_baseline_resourcesBtn;
    public Text bottom_baseline_resourcesFoodText;
    public Text bottom_baseline_resourcesStuffText;
    public Text bottom_baseline_resourcesProductText;
    public Text bottom_baseline_resourcesSignText;

    public Text bottom_baseline_elementWindText;
    public Text bottom_baseline_elementFireText;
    public Text bottom_baseline_elementWaterText;
    public Text bottom_baseline_elementGroundText;
    public Text bottom_baseline_elementLightText;
    public Text bottom_baseline_elementDarkText;

    public RectTransform bottom_resourcesBlockRt;
    public Text bottom_resources_foodCerealText;
    public Text bottom_resources_foodVegetableText;
    public Text bottom_resources_foodFruitText;
    public Text bottom_resources_foodMeatText;
    public Text bottom_resources_foodFishText;
    public Text bottom_resources_foodBeerText;
    public Text bottom_resources_foodWineText;

    public Text bottom_resources_stuffWoodText;
    public Text bottom_resources_stuffStoneText;
    public Text bottom_resources_stuffMetalText;
    public Text bottom_resources_stuffLeatherText;
    public Text bottom_resources_stuffClothText;
    public Text bottom_resources_stuffTwineText;
    public Text bottom_resources_stuffBoneText;
    public Text bottom_resources_stuffWindText;
    public Text bottom_resources_stuffFireText;
    public Text bottom_resources_stuffWaterText;
    public Text bottom_resources_stuffGroundText;
    public Text bottom_resources_stuffLightText;
    public Text bottom_resources_stuffDarkText;

    public Text bottom_resources_productWeaponText;
    public Text bottom_resources_productArmorText;
    public Text bottom_resources_productJewelryText;
    public Text bottom_resources_productSkillRollText;





    public Button closeBtn;


    //配置
    float roleWidth = 24f;//人物宽度
    float roleHeight = 54f;//人物高度
    Color colorRes = new Color(255/ 255f,189/ 255f,88/ 255f, 1f);
    Color colorMake = new Color(221/ 255f,90/ 255f,246/ 255f, 1f);
    Color colorBuild = new Color(0/ 255f,98/ 255f,251/ 255f, 1f);

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
    //运行变量组
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

    public short nowDistrict=-1;

    public bool IsShowResourcesBlock = false;

    //对象池
    List<GameObject> buildingGoPool = new List<GameObject>();
    List<GameObject> customerGoPool = new List<GameObject>();//用作保存空闲的go

    Dictionary<int, Image> statusBarImageDic = new Dictionary<int, Image>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //contentRt = this.transform as RectTransform; //也可以写成this.GetComponent<RectTransform>(),但是不建议；
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();

        left_districtMainBtn.onClick.AddListener(delegate () { gci.OpenDistrictMain(); });
        left_inventoryMainBtn.onClick.AddListener(delegate () { gci.OpenItemListAndInfo(); });
        left_inventoryScrollBtn.onClick.AddListener(delegate () { gci.OpenSkillListAndInfo(); });
        left_buildBtn.onClick.AddListener(delegate () { gci.OpenBuild(); });
        left_heroMainBtn.onClick.AddListener(delegate () { gci.OpenHeroSelect(); });
        left_marketBtn.onClick.AddListener(delegate () { gci.OpenMarket(); });

        right_transferBtn.onClick.AddListener(delegate () { gci.OpenTransfer(); });
        right_adventureSendBtn.onClick.AddListener(delegate () { gci.OpenAdventureSend(); });


        bottom_baseline_resourcesBtn.onClick.AddListener(delegate () {
            if (IsShowResourcesBlock)
            {
                HideResourcesBlock();
            }
            else { ShowResourcesBlock(gc.nowCheckingDistrictID); }
        });

        closeBtn.onClick.AddListener(delegate () { OnHide(); });

       
    }

    // Update is called once per frame
    void Update()
    {
        if (isChoose)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(contentRt, Input.mousePosition, canvas.worldCamera, out wantBuidingPos))
            {
                wantBuidingPos = new Vector2(((int)wantBuidingPos.x / 16) * 16f- wantBuidingSizeX * 16f, ((int)wantBuidingPos.y / 16) * 16f+ wantBuidingSizeY * 16f);
                wantBuidingGo.GetComponent<RectTransform>().anchoredPosition = wantBuidingPos;

                if (wantBuidingPos != wantBuidingPos_Temp)
                {
                    if (wantBuidingPos.y != wantBuidingPos_Temp.y)
                    {
                         layerIndex = ((int)wantBuidingPos.y / 16) * -1 + wantBuidingSizeY-1;
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


    public override void OnShow()
    {
        if (gc.nowCheckingDistrictID != nowDistrict)
        {
            nowDistrict = gc.nowCheckingDistrictID;
            UpdateAllCustomer(nowDistrict);
        }

        SetAnchoredPosition(0, -90);
        UpdateBasicInfo();
        UpdateAllBuilding(gc.nowCheckingDistrictID);
        HideTip();

        HideResourcesBlock();
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



        InvokeRepeating("UpdateBar", 0, 0.2f);

        isShow = true;
    }
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
        if (IsShowResourcesBlock)
        {
            HideResourcesBlock();
        }

        SetAnchoredPosition(0, 5000);
        PlayMainPanel.Instance.top_districtBtn.GetComponent<RectTransform>().localScale = Vector2.one;
        isShow = false;

        CancelInvoke("UpdateBar");
    }

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
            left_buildBtn.transform.localScale = Vector2.zero;
        }

    }


    public void UpdateBasicInfo()
    {
        nameText.text = gc.districtDic[gc.nowCheckingDistrictID].name + "·" + gc.districtDic[gc.nowCheckingDistrictID].baseName;
        levelText.text = gc.districtDic[gc.nowCheckingDistrictID].level.ToString();
        peopleText.text = "居民 " + gc.districtDic[gc.nowCheckingDistrictID].people+"/" + gc.districtDic[gc.nowCheckingDistrictID].peopleLimit;
        buildingText.text ="设施 "+ gc.districtDic[gc.nowCheckingDistrictID].buildingList.Count; 
    }


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

    public bool CheckCanBuild()
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
                //Debug.Log("gc.districtGridDic[gc.nowCheckingDistrictID][index].level="+ gc.districtGridDic[gc.nowCheckingDistrictID][index].level);
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
            Debug.Log("wantBuidingPosY=" + wantBuidingPosY + " wantBuidingSizeYBase=" + wantBuidingSizeYBase);
            if ((wantBuidingPosY + wantBuidingSizeYBase-1) != 10 && (wantBuidingPosY + wantBuidingSizeYBase-1) != 18)
            {
                can = false;
            }
        }


        return can;       
    }

    void ToBuild()
    {

        isChoose = false;
        HideTip();
        gc.CreateBuildEvent(wantBuidingPID, (short)wantBuidingPosX, (short)wantBuidingPosY, (byte)layerIndex,x,y);
        SetBuilding(gc.buildingIndex-1,true,-1);
        Destroy(wantBuidingGo);
    }

    public void UpdateAllBuilding(int districtID)
    {
        List<BuildingObject> temp = new List<BuildingObject> { };
        foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
        {
            if (kvp.Value.districtID == districtID)
            {
                temp.Add(kvp.Value);
            }
        }

        for (int i = 0; i < temp.Count; i++)
        {
            SetBuilding(temp[i].id,false,i);
        }
        for (int i = temp.Count; i < buildingGoPool.Count; i++)
        {
            buildingGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

   

    public void UpdateSingleBuilding( int buildingID)
    {
        GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.buildingDic[buildingID].layer + "/" + buildingID);
        UpdateBuildingGo(go, buildingID);
    }



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
        UpdateBuildingGo(go, buildingID);
    }

    
    public void DeleteBuilding(int buildingID)
    {
        GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.buildingDic[buildingID].layer + "/" + buildingID);
        go.transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        if (statusBarImageDic.ContainsKey(buildingID))
        {
            statusBarImageDic.Remove(buildingID);
        }
    }
    public void DeleteCustomer(int customerID)
    {
        GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/" + customerID);
        go.transform.GetComponent<RectTransform>().localScale = Vector2.zero;
    }

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
                        statusSubTf.GetComponent<Text>().text = "<color=#D583EC>" + gc.OutputItemTypeSmallStr(DataManager.mProduceEquipDict[gc.buildingDic[buildingID].produceEquipNow].Type) + "(" + DataManager.mProduceEquipDict[gc.buildingDic[buildingID].produceEquipNow].Level + ")制作中</color>";
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
                case "Military":
                    statusTf.GetComponent<RectTransform>().localScale = Vector2.zero;
                    break;
            }
        }

        go.transform.GetComponent<Button>().onClick.RemoveAllListeners();
        go.transform.GetComponent<Button>().onClick.AddListener(delegate () { BuildingPanel.Instance.OnShow(gc.buildingDic[buildingID]); });
    }

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


    void ShowTip(string str)
    {
        tipRt.localScale = Vector2.one;
        tipText.text = str;
    }
    void HideTip()
    {
        tipRt.localScale = Vector2.zero;
    }

    public void UpdateBaselineResourcesText(short districtID)
    {
        if (gc.districtDic[districtID].force == 0)
        {
            bottom_baseline_resourcesFoodText.text = gc.GetDistrictFoodAll(districtID) + "/" + gc.districtDic[districtID].rFoodLimit;
            bottom_baseline_resourcesStuffText.text = gc.GetDistrictStuffAll(districtID) + "/" + gc.districtDic[districtID].rStuffLimit;
            bottom_baseline_resourcesProductText.text = gc.GetDistrictProductAll(districtID) + "<color=#92FF9D>[" + gc.GetDistrictProductGoodsAll(districtID) + "]</color>/" + gc.districtDic[districtID].rProductLimit;
        }
        else
        {
            bottom_baseline_resourcesFoodText.text = "-";
            bottom_baseline_resourcesStuffText.text = "-";
            bottom_baseline_resourcesProductText.text = "-";
        }
    }
    public void UpdateBaselineElementText(short districtID)
    {      
        bottom_baseline_elementWindText.text = "风 " + gc.districtDic[districtID].eWind;
        bottom_baseline_elementFireText.text = "火 " + gc.districtDic[districtID].eFire;
        bottom_baseline_elementWaterText.text = "水 " + gc.districtDic[districtID].eWater;
        bottom_baseline_elementGroundText.text = "地 " + gc.districtDic[districtID].eGround;
        bottom_baseline_elementLightText.text = "光 " + gc.districtDic[districtID].eLight;
        bottom_baseline_elementDarkText.text = "暗 " + gc.districtDic[districtID].eDark;
    }

    public void UpdateResourcesBlock(short districtID)
    {
        bottom_resources_foodCerealText.text = gc.districtDic[districtID].rFoodCereal.ToString();
        bottom_resources_foodVegetableText.text = gc.districtDic[districtID].rFoodVegetable.ToString();
        bottom_resources_foodFruitText.text = gc.districtDic[districtID].rFoodFruit.ToString();
        bottom_resources_foodMeatText.text = gc.districtDic[districtID].rFoodMeat.ToString();
        bottom_resources_foodFishText.text = gc.districtDic[districtID].rFoodFish.ToString();
        bottom_resources_foodBeerText.text = gc.districtDic[districtID].rFoodBeer.ToString();
        bottom_resources_foodWineText.text = gc.districtDic[districtID].rFoodWine.ToString();
   

        bottom_resources_stuffWoodText.text = gc.districtDic[districtID].rStuffWood.ToString();
        bottom_resources_stuffStoneText.text = gc.districtDic[districtID].rStuffStone.ToString();
        bottom_resources_stuffMetalText.text = gc.districtDic[districtID].rStuffMetal.ToString();
        bottom_resources_stuffLeatherText.text = gc.districtDic[districtID].rStuffLeather.ToString();
        bottom_resources_stuffClothText.text = gc.districtDic[districtID].rStuffCloth.ToString();
        bottom_resources_stuffTwineText.text = gc.districtDic[districtID].rStuffTwine.ToString();
        bottom_resources_stuffBoneText.text = gc.districtDic[districtID].rStuffBone.ToString();
        bottom_resources_stuffWindText.text = gc.districtDic[districtID].rStuffWind.ToString();
        bottom_resources_stuffFireText.text = gc.districtDic[districtID].rStuffFire.ToString();
        bottom_resources_stuffWaterText.text = gc.districtDic[districtID].rStuffWater.ToString();
        bottom_resources_stuffGroundText.text = gc.districtDic[districtID].rStuffGround.ToString();
        bottom_resources_stuffLightText.text = gc.districtDic[districtID].rStuffLight.ToString();
        bottom_resources_stuffDarkText.text = gc.districtDic[districtID].rStuffDark.ToString();


        bottom_resources_productWeaponText.text = gc.districtDic[districtID].rProductWeapon+ "<color=#92FF9D>[在售 " + gc.districtDic[districtID].rProductGoodWeapon + "]</color>";
        bottom_resources_productArmorText.text = gc.districtDic[districtID].rProductArmor + "<color=#92FF9D>[在售 " + gc.districtDic[districtID].rProductGoodArmor + "]</color>";
        bottom_resources_productJewelryText.text = gc.districtDic[districtID].rProductJewelry + "<color=#92FF9D>[在售 " + gc.districtDic[districtID].rProductGoodJewelry + "]</color>";
        bottom_resources_productSkillRollText.text = gc.districtDic[districtID].rProductScroll + "<color=#92FF9D>[在售 " + gc.districtDic[districtID].rProductGoodScroll + "]</color>";

    }
    public void ShowResourcesBlock(short districtID)
    {
        if (gc.districtDic[districtID].force != 0)
        {
            return;
        }


        if (BuildPanel.Instance.isShow)
        {
            BuildPanel.Instance.OnHide();
        }
        if (BuildingPanel.Instance.isShow)
        {
            BuildingPanel.Instance.OnHide();
        }

        UpdateResourcesBlock(districtID);
        bottom_baseline_resourcesSignText.text = "▲";
        bottom_resourcesBlockRt.localScale = Vector2.one;
        IsShowResourcesBlock = true;
    }
    public void HideResourcesBlock()
    {
        bottom_baseline_resourcesSignText.text = "▼";
        bottom_resourcesBlockRt.localScale = Vector2.zero;
        IsShowResourcesBlock = false;
    }


    //访客部分
    public void UpdateAllCustomer(int districtID)
    {
        List<CustomerObject> temp = new List<CustomerObject> { };
        foreach (KeyValuePair<int, CustomerObject> kvp in gc.customerDic)
        {
            if (kvp.Value.districtID == districtID)
            {
                temp.Add(kvp.Value);
            }
        }

        for (int i = 0; i < temp.Count; i++)
        {
            SetCustomer(temp[i].id, true);
            UpdateCustomerByStage(temp[i].id);
        }
        for (int i = temp.Count; i < customerGoPool.Count; i++)
        {
            customerGoPool[i].transform.GetComponent<Image>().color = Color.clear;
            customerGoPool[i].transform.GetComponent<AnimatiorControlByNPC>().Stop();
        }
    }
    public void UpdateSingleCustomer(int customerID)
    {
        SetCustomer(customerID, false);
    }


    //创建访客实例 isNew 用作进入游戏第一次打开本面板，
    void SetCustomer(int customerID, bool isNew)
    {
        GameObject go;

        if (isNew)
        {
            go = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisCustomer")) as GameObject;
           // customerGoPool.Add(go);
        }
        else
        {
            if (customerGoPool.Count>0)
            {
                go = customerGoPool[0];
                go.GetComponent<Image>().color = Color.white;
                customerGoPool.RemoveAt(0);
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisCustomer")) as GameObject;
            }
        }
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
        SetCustomerPos(customerID);
    }

    //初始化访客位置
    void SetCustomerPos(int customerID)
    {
        float roleHeight = 54f;
        //Debug.Log("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/Customer_" + customerID);
        //Debug.Log("gc.customerDic[customerID].stage=" + gc.customerDic[customerID].stage);
        //Debug.Log("gc.customerDic[customerID].buildingID=" + gc.customerDic[customerID].buildingID);
      //  Debug.Log("gc.customerDic[customerID].buildingID=" + gc.customerDic[customerID].);
        GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/Customer_" + customerID);
        int buildingID = gc.customerDic[customerID].buildingID;
        int waitIndex;
        int ranY;
        int layerIndex;
        Vector2 startPos;
        switch (gc.customerDic[customerID].stage)
        {
            case CustomerStage.Come:
             
                int startX = 20 * gc.districtDic[gc.customerDic[customerID].districtID].level +Random.Range( 10,100);
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
                    startPos = new Vector2(Random.Range(58, 72) * 16f, layerIndex  * -16f + roleHeight);
                }
                break;
            default:
                startPos = Vector2.zero;
                break;
        }
        go.GetComponent<RectTransform>().anchoredPosition = startPos;
    }

    public void UpdateCustomerByStage(int customerID)
    {
        GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/Customer_" + customerID);
        switch (gc.customerDic[customerID].stage)
        {
            case CustomerStage.Come:
                StartCoroutine(CustomerCome(customerID,go));
                break;
            case CustomerStage.Observe:
                StartCoroutine(CustomerObserve(customerID, go));
                break;
            case CustomerStage.Wait:
                StartCoroutine(CustomerWait(customerID, go));
                break;
            case CustomerStage.IntoShop:
                StartCoroutine(CustomerIntoShop(customerID, go));
                break;
            case CustomerStage.Gone:
                StartCoroutine(CustomerGone(customerID, go));
                break;
        }
    }


    IEnumerator CustomerCome(int customerID, GameObject go)
    {
    
        Vector2 startPos=go.transform.localPosition;
        Vector2 targetPos;

        if (gc.customerDic[customerID].buildingID!=-1)
        {
            int buildingID = gc.customerDic[customerID].buildingID;

            targetPos = new Vector2((gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition + (gc.buildingDic[buildingID].positionY < 64 ? (1 + gc.buildingDic[buildingID].customerList.Count) * -1 : gc.buildingDic[buildingID].customerList.Count)) * 16f, gc.customerDic[customerID].layer * -16f + roleHeight);


            go.GetComponent<AnimatiorControlByNPC>().SetAnim((startPos.x > targetPos.x) ? AnimStatus.WalkLeft : AnimStatus.WalkRight);
            go.transform.DOLocalMove(targetPos, 5f);

            yield return new WaitForSeconds(5f);
            go.transform.DOComplete();


            go.GetComponent<AnimatiorControlByNPC>().SetAnim(gc.buildingDic[buildingID].doorInLine);
            go.GetComponent<AnimatiorControlByNPC>().Stop();
            gc.customerDic[customerID].stage = CustomerStage.Wait;
            UpdateCustomerByStage(customerID);

        }
        else
        {
            targetPos = new Vector2(Random.Range(54, 74) * 16f, gc.customerDic[customerID].layer  * -16f + roleHeight);
       
            go.GetComponent<RectTransform>().anchoredPosition = startPos;

            go.GetComponent<AnimatiorControlByNPC>().SetAnim((startPos.x > targetPos.x) ? AnimStatus.WalkLeft : AnimStatus.WalkRight);

            go.transform.DOLocalMove(targetPos, 5f);

            yield return new WaitForSeconds(5f);
            gc.customerDic[customerID].stage = CustomerStage.Observe;
            UpdateCustomerByStage(customerID);
        }
       // Debug.Log("startPos=" + startPos);
       // Debug.Log("targetPos=" + targetPos);
    }

    IEnumerator CustomerWait(int customerID, GameObject go)
    {
        go.transform.DOComplete();
        int buildingID = gc.customerDic[customerID].buildingID;
        int waitIndex= gc.buildingDic[buildingID].customerList.IndexOf(customerID);
        Vector2 startPos = go.transform.localPosition;
        Vector2 targetPos = new Vector2((gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition + (gc.buildingDic[buildingID].positionY < 64 ? (1 + waitIndex) * -1 : waitIndex)) * 16f, gc.customerDic[customerID].layer * -16f + roleHeight);

        go.GetComponent<AnimatiorControlByNPC>().SetAnim((startPos.x> targetPos.x)? AnimStatus.WalkLeft: AnimStatus.WalkRight);
        go.GetComponent<AnimatiorControlByNPC>().Play();
        go.transform.DOLocalMove(targetPos, 1f);
        yield return new WaitForSeconds(1f);
        go.transform.DOComplete();
        go.GetComponent<AnimatiorControlByNPC>().Stop();
    }

    IEnumerator CustomerObserve(int customerID,GameObject go)
    {
        gc.customerRecordDic[gc.timeYear + "/" + gc.timeMonth].backNum[gc.customerDic[customerID].districtID]++;

        go.transform.DOComplete();
        go.GetComponent<AnimatiorControlByNPC>().Stop();
      

        yield return new WaitForSeconds( Random.Range(1.2f,2.0f) );

        string str = "";
        switch (gc.customerDic[customerID].shopType)
        {
            case ShopType.WeaponAndSubhand: str = "这里没有武器店吗？"; break;
            case ShopType.Armor: str = "这里没有防具店吗？"; break;
            case ShopType.Jewelry: str = "这里没有饰品店吗？"; break;
            case ShopType.Scroll: str = "这里没有卷轴店吗？"; break;
        }
        StartCoroutine(CustomerTalk(customerID, gc.OutputRandomStr(new List<string> { "没有合适的店啊", "icon_talk_sad", "白来一趟了", "好荒凉啊", str }), 0f));
        yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));

        gc.customerDic[customerID].stage = CustomerStage.Gone;
        UpdateCustomerByStage(customerID);
    }

    IEnumerator CustomerIntoShop(int customerID, GameObject go)
    {
        go.transform.DOComplete();
        int buildingID = gc.customerDic[customerID].buildingID;
        float doorPosX = (gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition)*16f;

        if (go.transform.localPosition.x > doorPosX)
        {
            go.GetComponent<AnimatiorControlByNPC>().SetAnim(AnimStatus.WalkLeft);
            go.transform.DOLocalMove(go.transform.localPosition + Vector3.left * (go.transform.localPosition.x - doorPosX), 1f);
        }
        else
        {
            go.GetComponent<AnimatiorControlByNPC>().SetAnim(AnimStatus.WalkRight);
            go.transform.DOLocalMove(go.transform.localPosition + Vector3.right * (doorPosX-go.transform.localPosition.x  -8f), 1f);
        }
        yield return new WaitForSeconds(1f);

        go.GetComponent<AnimatiorControlByNPC>().SetAnim(AnimStatus.WalkUp);
        go.transform.DOLocalMove(go.transform.localPosition + Vector3.up * 10f, 1f);
        yield return new WaitForSeconds(1f);
        go.GetComponent<AnimatiorControlByNPC>().SetAnim(AnimStatus.WalkDown);
        go.transform.DOLocalMove(go.transform.localPosition + Vector3.down * 10f, 1f);

        yield return new WaitForSeconds(1f);
        gc.customerDic[customerID].stage = CustomerStage.Gone;
        UpdateCustomerByStage(customerID);
    }
    IEnumerator CustomerGone(int customerID, GameObject go)
    {
        go.transform.DOComplete();
        int ran = Random.Range(0, 2);

        go.GetComponent<AnimatiorControlByNPC>().SetAnim(ran==0?AnimStatus.WalkLeft: AnimStatus.WalkRight);
        go.transform.DOLocalMove(new Vector2(ran == 0?0: 2008, go.transform.localPosition.y), 10f);
        yield return new WaitForSeconds(10f);
        go.transform.DOComplete();
        go.GetComponent<AnimatiorControlByNPC>().Stop();
        go.GetComponent<Image>().color = Color.clear;
        customerGoPool.Add(go);
        gc.CustomerGone(customerID);

    }

    public IEnumerator CustomerTalk(int customerID,string talkContent,float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject cgo= GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/Customer_" + customerID);
        if (talkContent != "")
        {
            GameObject go = Instantiate(Resources.Load("Prefab/Moment/Moment_Talk")) as GameObject;
            go.transform.SetParent(cgo.transform);
            go.GetComponent<MomentTalk>().Show(talkContent, 0, new Vector2(0, 40f));
        }
    }


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
                            case  ItemTypeBig.Weapon: str += "[武器]"; break;
                            case ItemTypeBig.Subhand: str += "[副手]"; break;
                            case ItemTypeBig.Armor: str += "[防具]"; break;
                            case ItemTypeBig.Jewelry: str += "[饰品]"; break;
                        }
                    }
                }
            }
            else if(gc.customerDic[customerID].shopType == ShopType.Scroll)
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

    public void HideCustomerInfo()
    {
        customerInfoRt.anchoredPosition =  Vector2.up * 5000f;
    }

    public void ChangeSkyColor()
    {
        skyBgImage.DOColor (colorHourBg[gc.timeHour],1f);
        skyFgImage.DOColor(colorHourFg[gc.timeHour], 1f);
    }

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


    //GameObject go = Instantiate(Resources.Load("Prefab/Moment/Moment_Talk")) as GameObject;
    //go.transform.SetParent(customerGo.transform);

    //go.GetComponent<MomentTalk>().Show(str, 0, new Vector2(0,40f));
}
