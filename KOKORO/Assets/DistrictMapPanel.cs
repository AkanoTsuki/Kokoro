using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    public Button closeBtn;


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

    //对象池
    List<GameObject> buildingGoPool = new List<GameObject>();



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
        closeBtn.onClick.AddListener(delegate () { OnHide(); });

        InvokeRepeating("UpdateBar", 0, 0.2f);
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
        SetAnchoredPosition(0, 5000);
        PlayMainPanel.Instance.top_districtBtn.GetComponent<RectTransform>().localScale = Vector2.one;
        isShow = false;
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
        string str = "";
        string str2 = "";
        bool can = true;
        for (int i = 0; i < x.Count; i++)
        {

            string index = gc.nowCheckingDistrictID + "_" + x[i] + "," + y[i];
            if (gc.districtGridDic[gc.nowCheckingDistrictID].ContainsKey(index))
            {
                //Debug.Log("gc.districtGridDic[gc.nowCheckingDistrictID][index].level="+ gc.districtGridDic[gc.nowCheckingDistrictID][index].level);
                if (gc.districtGridDic[gc.nowCheckingDistrictID][index].buildingID != -1|| gc.districtGridDic[gc.nowCheckingDistrictID][index].level>gc.districtDic[gc.nowCheckingDistrictID].level)
                {
                    str += "(" + x[i] + "," + y[i] + ")";
                    can = false;
                    break;
                }
            }
            else 
            {
                str2 += "(" + x[i] + "," + y[i] + ")";
                can = false;
                break;
            }

        }

        return can;       
    }


    void ToBuild()
    {
        // wantBuidingGo.name = gc.buildingIndex.ToString();
       

        isChoose = false;
        HideTip();
        gc.CreateBuildEvent(wantBuidingPID, (short)wantBuidingPosX, (short)wantBuidingPosY, (byte)layerIndex,x,y);
        SetBuilding(gc.buildingIndex-1,true,-1);
        Destroy(wantBuidingGo);
    }

    public void  UpdateAllBuilding(int districtID)
    {
       // buildingGoDic.Clear();
        List<BuildingObject> temp = new List<BuildingObject> { };
        foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
        {
            if (kvp.Value.districtID == districtID)//&& kvp.Value.buildProgress==1
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



     void SetBuilding(int buildingID,bool isNew,int index)
    {
        Debug.Log("SetBuilding() buildingID="+ buildingID);
        Debug.Log("SetBuilding() buildingGoPool.Count=" + buildingGoPool.Count);
        GameObject go;

        if (isNew)
        {
            go = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisBuilding")) as GameObject;
            buildingGoPool.Add(go);
        }
        else
        {
            if (index<buildingGoPool.Count )
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
                    if (gc.buildingDic[buildingID].produceEquipNow != -1)
                    {
                        statusSubTf.GetComponent<Text>().text = "<color=#FFDC7C>运作中</color>";
                        statusBarTf.GetComponent<RectTransform>().localScale = Vector2.one;
                    }
                    else
                    {
                        statusSubTf.GetComponent<Text>().text = "<color=#FF4500>停工</color>";
                        statusBarTf.GetComponent<RectTransform>().localScale = Vector2.zero;
                    }

                    break;
                case "Forge":
                    statusTf.GetComponent<RectTransform>().localScale = Vector2.one;
                    if (gc.buildingDic[buildingID].produceEquipNow != -1)
                    {
                        statusSubTf.GetComponent<Text>().text = "<color=#D583EC>" + gc.OutputItemTypeSmallStr(DataManager.mProduceEquipDict[gc.buildingDic[buildingID].produceEquipNow].Type) + "(" + DataManager.mProduceEquipDict[gc.buildingDic[buildingID].produceEquipNow].Level + ")制作中</color>";
                        statusBarTf.GetComponent<RectTransform>().localScale = Vector2.one;
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
        if (!isShow)
        {
            return;
        }

       // Debug.Log("UpdateBar");

        int needTime;
        int nowTime;
        for (int i = 0; i < gc.executeEventList.Count; i++)
        {
            if (gc.executeEventList[i].type == ExecuteEventType.Build)
            {
              
                if (gc.executeEventList[i].value[0][0] == gc.nowCheckingDistrictID)
                {

                    needTime = gc.executeEventList[i].endTime - gc.executeEventList[i].startTime;
                    nowTime = gc.standardTime - gc.executeEventList[i].startTime;

                    int buildingID = gc.executeEventList[i].value[1][0];
                    GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.buildingDic[buildingID].layer + "/" + buildingID);
                    Transform statusBarTf = go.transform.GetChild(2).GetChild(0);
                    statusBarTf.GetComponent<RectTransform>().sizeDelta = new Vector2(((float)nowTime / needTime) * 40f, 12f);
                }
            }
            else if (gc.executeEventList[i].type == ExecuteEventType.BuildingUpgrade)
            {
               // Debug.Log("UpdateBar() gc.executeEventList[i].value[0][0]=" + gc.executeEventList[i].value[0][0]);
                if (gc.executeEventList[i].value[0][0] == gc.nowCheckingDistrictID)
                {

                    needTime = gc.executeEventList[i].endTime - gc.executeEventList[i].startTime;
                    nowTime = gc.standardTime - gc.executeEventList[i].startTime;

                    int buildingID = gc.executeEventList[i].value[1][0];
                    Debug.Log("buildingID=" + buildingID);
                    GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.buildingDic[buildingID].layer + "/" + buildingID);
                    Transform statusBarTf = go.transform.GetChild(2).GetChild(0);
                    statusBarTf.GetComponent<RectTransform>().sizeDelta = new Vector2(((float)nowTime / needTime) * 40f, 12f);
                }
            }
            else if (gc.executeEventList[i].type == ExecuteEventType.ProduceResource)
            {
                if (gc.executeEventList[i].value[0][0] == gc.nowCheckingDistrictID)
                {

                    needTime = gc.executeEventList[i].endTime - gc.executeEventList[i].startTime;
                    nowTime = gc.standardTime - gc.executeEventList[i].startTime;

                    int buildingID = gc.executeEventList[i].value[1][0];
                    GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.buildingDic[buildingID].layer + "/" + buildingID);
                    Transform statusBarTf = go.transform.GetChild(2).GetChild(0);
                    statusBarTf.GetComponent<RectTransform>().sizeDelta = new Vector2(((float)nowTime / needTime) * 40f, 12f);
                }
            }
            else if (gc.executeEventList[i].type == ExecuteEventType.ProduceItem)
            {
                if (gc.executeEventList[i].value[0][0] == gc.nowCheckingDistrictID)
                {

                    needTime = gc.executeEventList[i].endTime - gc.executeEventList[i].startTime;
                    nowTime = gc.standardTime - gc.executeEventList[i].startTime;

                    int buildingID = gc.executeEventList[i].value[1][0];
                    GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.buildingDic[buildingID].layer + "/" + buildingID);
                    Transform statusBarTf = go.transform.GetChild(2).GetChild(0);
                    statusBarTf.GetComponent<RectTransform>().sizeDelta = new Vector2(((float)nowTime / needTime) * 40f, 12f);
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



    //功能按钮组

}
