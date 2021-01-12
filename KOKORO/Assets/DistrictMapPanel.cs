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

    public RectTransform tipRt;
    public Text tipText;

    public Button left_districtMainBtn;
    public Button left_heroMainBtn;
    public Button left_inventoryMainBtn;
    public Button left_inventoryScrollBtn;
    public Button left_marketBtn;
    public Button left_buildBtn;
  

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
    Color colorRes = new Color(255/ 255f,189/ 255f,88/ 255f, 1f);
    Color colorMake = new Color(221/ 255f,90/ 255f,246/ 255f, 1f);
    Color colorBuild = new Color(0/ 255f,98/ 255f,251/ 255f, 1f);
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


    public bool IsShowResourcesBlock = false;

    //对象池
    List<GameObject> buildingGoPool = new List<GameObject>();

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
                   // Debug.Log("zxl");
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


    public void OnShow(int x, int y)
    {

        SetAnchoredPosition(x, y);
        UpdateBasicInfo();
        UpdateAllBuilding(gc.nowCheckingDistrictID);
        HideTip();

        HideResourcesBlock();
        UpdateBaselineResourcesText(gc.nowCheckingDistrictID);
        UpdateBaselineElementText(gc.nowCheckingDistrictID);
        isShow = true;

        InvokeRepeating("UpdateBar", 0, 0.2f);
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
        //Debug.Log("SetBuilding() buildingID="+ buildingID);
        //Debug.Log("SetBuilding() buildingGoPool.Count=" + buildingGoPool.Count);
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
                //buildingGoPool.RemoveAt(buildingGoPool.Count - 1);
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
        Transform statusSubTf = go.transform.GetChild(2).GetChild(2);

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
                        statusSubTf.GetComponent<Text>().text = "<color=#FFDC7C>运作中</color>";
                        statusBarTf.GetComponent<RectTransform>().localScale = Vector2.one;
                        statusBarImageDic[buildingID].color = colorRes;
                    }
                    else
                    {
                        statusSubTf.GetComponent<Text>().text = "<color=#FF4500>停工</color>";
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
                        statusSubTf.GetComponent<Text>().text = "<color=#FF4500>停工</color>";
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
        bottom_baseline_resourcesFoodText.text = gc.GetDistrictFoodAll(districtID) + "/" + gc.districtDic[districtID].rFoodLimit;
        bottom_baseline_resourcesStuffText.text = gc.GetDistrictStuffAll(districtID) + "/" + gc.districtDic[districtID].rStuffLimit;
        bottom_baseline_resourcesProductText.text = gc.GetDistrictProductAll(districtID) + "/" + gc.districtDic[districtID].rProductLimit;
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


        bottom_resources_productWeaponText.text = gc.districtDic[districtID].rProductWeapon.ToString();
        bottom_resources_productArmorText.text = gc.districtDic[districtID].rProductArmor.ToString();
        bottom_resources_productJewelryText.text = gc.districtDic[districtID].rProductJewelry.ToString();
        bottom_resources_productSkillRollText.text = gc.districtDic[districtID].rProductScroll.ToString();

    }
    public void ShowResourcesBlock(short districtID)
    {
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
    public void CreateCustomer(int customerID)
    {
        GameObject go= Instantiate(Resources.Load("Prefab/UIBlock/Block_DisCustomer")) as GameObject;
        go.name = "Customer_"+ customerID;
       
        float roleHeight = 54f;//人物高度
        Vector2 startPos;
        Vector2 targetPos;
        Vector2 doorPos;
        Vector2 backPos;
        int startX = 20 * gc.districtDic[gc.customerDic[customerID].districtID].level + 10;
        if (gc.customerDic[customerID].buildingIDList.Count > 0)
        {
            int buildingID = gc.customerDic[customerID].buildingIDList[0];
            go.transform.SetParent(layer[gc.buildingDic[buildingID].layer].transform);

            
   
            startPos = new Vector2((64 + startX*(Random.Range(0, 2) == 0?-1:1)) * 16f, (gc.buildingDic[buildingID].positionY + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase) * -16f + roleHeight);
            doorPos = new Vector2((gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition - gc.buildingDic[buildingID].customerList.Count) * 16f, (gc.buildingDic[buildingID].positionY + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase) * -16f + roleHeight);
            targetPos = new Vector2((gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition + (gc.buildingDic[buildingID].positionY < 64 ? (1 + gc.buildingDic[buildingID].customerList.Count) * -1 : gc.buildingDic[buildingID].customerList.Count)) * 16f, (gc.buildingDic[buildingID].positionY + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase) * -16f + roleHeight);

            go.GetComponent<RectTransform>().anchoredPosition = startPos;
            go.GetComponent<AnimatiorControlByNPC>().SetCharaFrames(gc.customerDic[customerID].pic);
            go.GetComponent<AnimatiorControlByNPC>().SetAnim((startPos.x > targetPos.x) ? AnimStatus.WalkLeft : AnimStatus.WalkRight);
            go.GetComponent<AnimatiorControlByNPC>().Play();
            go.transform.DOLocalMove(targetPos, 5f);

            StartCoroutine(CustomerStandInLine(customerID,go, 5f, (targetPos.x > doorPos.x) ? AnimStatus.WalkLeft : AnimStatus.WalkRight));

        }
        else
        {
        
            int ranY = Random.Range(0, 2);
            int layerIndex = (ranY == 0 ? 11 : 18);
            go.transform.SetParent(layer[layerIndex].transform);
            startPos = new Vector2((64 + startX * (Random.Range(0, 2) == 0 ? -1 : 1)) * 16f, (layerIndex+1) * -16f + roleHeight);
            targetPos = new Vector2(Random.Range(58, 72) * 16f, (layerIndex + 1) * -16f + roleHeight);
            backPos = new Vector2((64 + startX * (Random.Range(0, 2) == 0 ? -1 : 1)) * 16f, (layerIndex + 1) * -16f + roleHeight);

            go.GetComponent<RectTransform>().anchoredPosition = startPos;
            go.GetComponent<AnimatiorControlByNPC>().SetCharaFrames(gc.customerDic[customerID].pic);
            go.GetComponent<AnimatiorControlByNPC>().SetAnim((startPos.x > targetPos.x) ? AnimStatus.WalkLeft : AnimStatus.WalkRight);
            go.GetComponent<AnimatiorControlByNPC>().Play();
            go.transform.DOLocalMove(targetPos, 5f);

            StartCoroutine(CustomerGoBack(customerID,go, 5f, backPos, (backPos.x > targetPos.x) ? AnimStatus.WalkRight : AnimStatus.WalkLeft));
        }


    }
    IEnumerator CustomerStandInLine(int customerID,GameObject customerGo ,float waitTime, AnimStatus endFaceTo)
    {
        yield return new WaitForSeconds(waitTime);
        customerGo.transform.DOComplete();
        customerGo.GetComponent<AnimatiorControlByNPC>().SetAnim(endFaceTo);
        customerGo.GetComponent<AnimatiorControlByNPC>().Stop();
    }

    IEnumerator CustomerGoBack(int customerID,GameObject customerGo, float waitTime, Vector2 backPos, AnimStatus backFaceTo)
    {
        yield return new WaitForSeconds(waitTime);
        customerGo.transform.DOComplete();
        customerGo.GetComponent<AnimatiorControlByNPC>().Stop();
       GameObject go = Instantiate(Resources.Load("Prefab/Moment/Moment_Talk")) as GameObject;
        go.transform.SetParent(customerGo.transform);

        int ran = Random.Range(0, 5);
        string str = "";
        switch (ran)
        {
            case 0: str = "没有合适的店啊"; break;
            case 1: str = "回去了"; break;
            case 2: str = "白来一趟了"; break;
            case 3: str = "好荒凉啊"; break;
            case 4:
                switch (gc.customerDic[customerID].shopType)
                {
                    case ShopType.WeaponAndSubhand: str = "这里没有武器店吗？"; break;
                    case ShopType.Armor: str = "这里没有防具店吗？"; break;
                    case ShopType.Jewelry: str = "这里没有饰品店吗？"; break;
                    case ShopType.Scroll: str = "这里没有卷轴店吗？"; break;
                }
                break;

        }

        go.GetComponent<MomentTalk>().Show(str, 0, new Vector2(0,48f));
        yield return new WaitForSeconds(2f);
        customerGo.GetComponent<AnimatiorControlByNPC>().SetAnim(backFaceTo);
        customerGo.transform.DOLocalMove(backPos, 5f);
        yield return new WaitForSeconds(5f);
        customerGo.transform.DOComplete();
        customerGo.GetComponent<AnimatiorControlByNPC>().Stop();
    }

    public IEnumerator CustomerGoToBuilding(int customerID)
    {
        GameObject customerGo = GameObject.Find("Customer_" + customerID);
        customerGo.GetComponent<AnimatiorControlByNPC>().SetAnim( AnimStatus.WalkUp);
        customerGo.transform.DOLocalMove(customerGo.transform.localPosition+ Vector3.up*10f, 1f);
        yield return new WaitForSeconds(1f);
        customerGo.GetComponent<AnimatiorControlByNPC>().SetAnim(AnimStatus.WalkDown);
        customerGo.transform.DOLocalMove(customerGo.transform.localPosition + Vector3.down * 10f, 1f);
        yield return new WaitForSeconds(1f);
        customerGo.transform.DOLocalMove(customerGo.transform.localPosition + Vector3.left * 200f, 5f);
    }

    public void UpdateBuildingCustomer(int buildingID)
    {
        int targetBuildingID = -1;
        Vector2 targetPos;

        // targetPos=new Vector2(gc.customerDic[customerID].buildingIDList[0])

        for (int i = 0; i < gc.buildingDic[buildingID].customerList.Count; i++)
        {
            GameObject customerGo = GameObject.Find("Customer_" + gc.buildingDic[buildingID].customerList[i]);
            customerGo.transform.DOLocalMove(new Vector2((gc.buildingDic[buildingID].positionX + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].DoorPosition + i) * 16f, (gc.buildingDic[buildingID].positionY + DataManager.mBuildingDict[gc.buildingDic[buildingID].prototypeID].SizeYBase) * 16f), 0.5f);
        

        }

    }
}
