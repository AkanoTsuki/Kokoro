using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingPanel : BasePanel
{
    public static BuildingPanel Instance;

    GameControl gc;

    #region 【UI控件】
    public Text nameText;
    public Image picImage;
    public Text desText;

    public List<RectTransform> titleSubRt;
    public List<Text> titleSubText;

    public Image bgImage;
    //public List<Image> bgRoleImageList;
    public List<AnimatiorControlByNpc> bgRoleAcList;

    public RectTransform outputInfoRt;
    public List<Image> outputInfo_iconImage;
    public Text outputInfo_desText;
    public RectTransform outputInfo_taskRt;
    public List<RectTransform> outputInfo_taskRtList;
    public List<Image> outputInfo_taskImageList;
    public List<Text> outputInfo_taskTextList;
    public List<RectTransform> outputInfo_taskAddRtList;
    public List<Button> outputInfo_taskDeleteBtnList;

    public RectTransform setManagerRt;
    public List<Image> setManager_imageList;
    public List<Text> setManager_textList;
    public List<Button> setManager_btnList;

    public RectTransform setWorkerRt;
    public Text setWorker_desText;
    public Button setWorker_minusBtn;
    public Button setWorker_addBtn;

    public RectTransform infoHistoryRt;
    public Text infoHistory_contentText;

    public RectTransform SetForgeRt;
    public Text setForge_outputText;
    public Text setForge_inputText;
    public Dropdown setForge_typeDd;
    public Dropdown setForge_levelDd;
    public List<Image> setForge_imageList;
    public List<Text> setForge_textList;
    public List<Button> setForge_btnList;
    public RectTransform setForge_addBlockRt;
    public Button setForge_addUnsetBtn;
    public RectTransform setForge_addUnsetSelectedRt;
    public Button setForge_numSet0;
    public Button setForge_numMinus10;
    public Button setForge_numMinus1;
    public Button setForge_numAdd1;
    public Button setForge_numAdd10;
    public Button setForge_numSetInfinite;
    public Text setForge_numText;
    public Button setForge_updateBtn;

    public GameObject recruitGo;
    public List<RectTransform> recruit_heroRtList;
    public List<Image> recruit_picImageList;
    public List<Text> recruit_nameTextList;
    public List<Text> recruit_growTextList;
    public List<Radar> recruit_radarList;

    public RectTransform strengthenRt;
    public Image strengthen_targetPicImage;
    public Text strengthen_targetNameText;
    public Button strengthen_targetBtn;
    public List<Image> strengthen_itemImageList;
    public List<Text> strengthen_itemTextList;
    public List<Button> strengthen_itemBtnList;
    public List<RectTransform> strengthen_itemSelectedRtList;
    public Text strengthen_infoText;
    public Text strengthen_goldCostText;
    public Button strengthen_doBtn;

    public RectTransform inlayRt;
    public Image inlay_targetPicImage;
    public Text inlay_targetNameText;
    public Button inlay_targetBtn;
    public List<Image> inlay_itemImageList;
    public List<Text> inlay_itemTextList;
    public List<Button> inlay_itemDoBtnList;
    public List<Button> inlay_itemChooseBtnList;
    public List<Text> inlay_itemDoBtnTextList;
    public List<Text> inlay_itemChooseBtnTextList;
    public Text inlay_goldCostText;

    public List<Button> totalSet_btnList;
    public Button totalSet_backBtn;

    public RectTransform upgradeBlockRt;
    public Text upgradeBlock_nameText;
    public Text upgradeBlock_desText;
    public Button upgradeBlock_cancelBtn;
    public Button upgradeBlock_confrimBtn;

    public RectTransform pullDownBlockRt;
    public Button pullDownBlock_cancelBtn;
    public Button pullDownBlock_confrimBtn;

    public Button closeBtn;
    #endregion

    //运行变量
    public int nowCheckingBuildingID = -1;
    //运行变量-栏目状态标记
    public bool IsShowOutputInfoPart = false;
    public bool IsShowOutputInfoPartTask = false;
    public bool IsShowSetManagerPart = false;
    public bool IsShowSetWorkerPart = false;
    public bool IsShowHistoryInfoPart = false;
    public bool IsShowSetForgePart = false;
    public bool IsShowRecruitPart = false;
    public bool IsShowStrengthenPart = false;
    public bool IsShowInlayPart = false;

    //运行变量-订单栏目
    public List<StuffType> forgeAddStuff = new List<StuffType> { StuffType.None, StuffType.None, StuffType.None};
    public short setForgePartNum = -1;
    public int setForgeTypeSmall = 0;
    public int setForgeType = 0;
    public int setForgeLevel = 0;

    //运行变量-强化栏目
    public int strengthenTargetID = -1;//装备实例ID
    public List<short> strengthenItemID = new List<short> { -1, -1, -1 };//消耗品强化石模板ID
    public int strengthenChooseIndex = -1;

    public int inlayTargetID = -1;//装备实例ID
    public List<short> inlayItemID =new List<short> { -1,-1,-1};//消耗品镶嵌石模板ID
    //public int inlayChooseIndex = -1;

    //对象池
    List<GameObject> forgeGoPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
      
        forgeAddStuff = new List<StuffType> { StuffType.None, StuffType.None, StuffType.None };

        totalSet_backBtn.onClick.AddListener(delegate () {
            HideOutputInfoPart();
            HideOutputInfoPartTask();
            HideSetForgePart();
            HideSetManagerPart();
            HideSetWorkerPart();
            HideRecruitPart();
            HideStrengthenPart();
            HideInlayPart();
            ShowHistoryInfoPart(gc.buildingDic[nowCheckingBuildingID], 796, -12);
            totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        });

        outputInfo_taskDeleteBtnList[0].onClick.AddListener(delegate () { gc.DeleteProduceItemTask(nowCheckingBuildingID,0); });
        outputInfo_taskDeleteBtnList[1].onClick.AddListener(delegate () { gc.DeleteProduceItemTask(nowCheckingBuildingID, 1); });
        outputInfo_taskDeleteBtnList[2].onClick.AddListener(delegate () { gc.DeleteProduceItemTask(nowCheckingBuildingID, 2); });

        upgradeBlock_cancelBtn.onClick.AddListener(delegate () { HideUpgradeBlock(); });
        pullDownBlock_cancelBtn.onClick.AddListener(delegate () { HidePullDownBlock(); });
     
        
        setForge_typeDd.onValueChanged.AddListener(delegate  { setForgeType = setForge_typeDd.value; UpdateSetForgeOutputInput(nowCheckingBuildingID, setForgeType, setForgeLevel); });
        setForge_levelDd.onValueChanged.AddListener(delegate { setForgeLevel = setForge_levelDd.value; UpdateSetForgeOutputInput(nowCheckingBuildingID, setForgeType, setForgeLevel); });
        setForge_btnList[0].onClick.AddListener(delegate () { ShowSetForgeAddBlock(0); });
        setForge_btnList[1].onClick.AddListener(delegate () {  ShowSetForgeAddBlock(1); });
        setForge_btnList[2].onClick.AddListener(delegate () {  ShowSetForgeAddBlock(2); });

        setForge_numSet0.onClick.AddListener(delegate () { gc.SetProduceEquipNum(nowCheckingBuildingID,0); });
        setForge_numMinus10.onClick.AddListener(delegate () { gc.ChangeProduceEquipNum(nowCheckingBuildingID, -10); });
        setForge_numMinus1.onClick.AddListener(delegate () { gc.ChangeProduceEquipNum(nowCheckingBuildingID, -1); });
        setForge_numAdd1.onClick.AddListener(delegate () { gc.ChangeProduceEquipNum(nowCheckingBuildingID, 1); });
        setForge_numAdd10.onClick.AddListener(delegate () { gc.ChangeProduceEquipNum(nowCheckingBuildingID, 10); });
        setForge_numSetInfinite.onClick.AddListener(delegate () { gc.SetProduceEquipNum(nowCheckingBuildingID, -1); });

        //TODO
        strengthen_targetBtn.onClick.AddListener(delegate () {
            if (ConsumableListAndInfoPanel.Instance.isShow)
            {
                ConsumableListAndInfoPanel.Instance.OnHide();
            }
            ItemListAndInfoPanel.Instance.isChooseTarget = true;
            ItemListAndInfoPanel.Instance.OnShowByChoose("strengthen", nowCheckingBuildingID,76,-104); }
        );
        strengthen_itemBtnList[0].onClick.AddListener(delegate () { strengthenChooseIndex = 0;UpdateStrengthenPart(gc.buildingDic[nowCheckingBuildingID]); });
        strengthen_itemBtnList[1].onClick.AddListener(delegate () { strengthenChooseIndex = 1; UpdateStrengthenPart(gc.buildingDic[nowCheckingBuildingID]); });
        strengthen_itemBtnList[2].onClick.AddListener(delegate () { strengthenChooseIndex = 2; UpdateStrengthenPart(gc.buildingDic[nowCheckingBuildingID]); });

        inlay_targetBtn.onClick.AddListener(delegate () {
            if (ConsumableListAndInfoPanel.Instance.isShow)
            {
                ConsumableListAndInfoPanel.Instance.OnHide();
            }
            ItemListAndInfoPanel.Instance.isChooseTarget = true;
            ItemListAndInfoPanel.Instance.OnShowByChoose("inlay", nowCheckingBuildingID, 76, -104); 
        });
        inlay_itemChooseBtnList[0].onClick.AddListener(delegate () { ConsumableListAndInfoPanel.Instance.OnShowByChoose("inlay", nowCheckingBuildingID, 0, 76, -104);});
        inlay_itemChooseBtnList[1].onClick.AddListener(delegate () { ConsumableListAndInfoPanel.Instance.OnShowByChoose("inlay", nowCheckingBuildingID, 1, 76, -104); });
        inlay_itemChooseBtnList[2].onClick.AddListener(delegate () { ConsumableListAndInfoPanel.Instance.OnShowByChoose("inlay", nowCheckingBuildingID, 2, 76, -104); });

        setWorker_minusBtn.onClick.AddListener(delegate () { gc.BuildingWorkerMinus(nowCheckingBuildingID); });

        setWorker_addBtn.onClick.AddListener(delegate () { gc.BuildingWorkerAdd(nowCheckingBuildingID); });

        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    //主面板显示
    public void OnShow(BuildingObject buildingObject)
    {
        nowCheckingBuildingID = buildingObject.id;
        
        UpdateBasicPart(buildingObject);
        HideOutputInfoPart();
        HideSetManagerPart();
        HideSetWorkerPart();
        ShowHistoryInfoPart(buildingObject,796,-12);
        HideSetForgePart();
        HideRecruitPart();
        HideStrengthenPart();
        HideInlayPart();
        UpdateTotalSetButton(buildingObject);
        UpdateSceneRolePic(buildingObject);
        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.zero;

        HideUpgradeBlock();
        HidePullDownBlock();

        if (BuildPanel.Instance.isShow)
        {
            BuildPanel.Instance.OnHide();
        }
        if (DistrictMainPanel.Instance.isShow)
        {
            DistrictMainPanel.Instance.OnHide();
        }

        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        isShow = true;
    }
    
    //主面板关闭
    public override void OnHide()
    {
        nowCheckingBuildingID = -1;

        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isShow = false;       
    }

    //动画场景-背景、人物更新
    public void UpdateSceneRolePic(BuildingObject buildingObject)
    {
        string text = "";
        for (int i = 0; i < buildingObject.npcPicList.Count; i++)
        {
            text += "," + buildingObject.npcPicList[i];
        }

        Debug.Log("npcPicList=" + text);

        int roleIndex = 0;


        for (int i = 0; i < DataManager.mBuildingDict[buildingObject.prototypeID].NpcPosX.Count; i++)
        {
            bgRoleAcList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(DataManager.mBuildingDict[buildingObject.prototypeID].NpcPosX[i], -DataManager.mBuildingDict[buildingObject.prototypeID].NpcPosY[i]);
        }
        for (int i = DataManager.mBuildingDict[buildingObject.prototypeID].NpcPosX.Count; i < bgRoleAcList.Count; i++)
        {
            bgRoleAcList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
        }

        if (buildingObject.buildProgress == 1)
        {
            bgImage.sprite = Resources.Load("Image/DistrictBG/" + DataManager.mBuildingDict[buildingObject.prototypeID].BgPic, typeof(Sprite)) as Sprite;

           

            switch (DataManager.mBuildingDict[buildingObject.prototypeID].BgPic)
            {
                case "DBG_WeaponShop":
                    bgRoleAcList[0].SetCharaFrames(buildingObject.npcPicList[0], 56, 54,true);
                    bgRoleAcList[0].SetAnim("Idle", -1);
                    roleIndex++;
                    bgRoleAcList[1].SetCharaFrames(buildingObject.npcPicList[1], 56, 54, false);
                    bgRoleAcList[1].SetAnim(buildingObject.isOpen?("Beat" + Random.Range(1, 3)): "Beat_Free", -1);
                    roleIndex++;
                    if (buildingObject.level >= 3)
                    {
                        bgRoleAcList[roleIndex].SetCharaFrames(buildingObject.npcPicList[roleIndex], 56, 54, false);
                        bgRoleAcList[roleIndex].SetAnim(buildingObject.isOpen ? ("Beat" + Random.Range(1, 3)) : "Beat_Free", -1);
                        roleIndex++;
                    }
                    if (buildingObject.level >= 5)
                    {
                        bgRoleAcList[roleIndex].SetCharaFrames(buildingObject.npcPicList[roleIndex], 56, 54, false);
                        bgRoleAcList[roleIndex].SetAnim(buildingObject.isOpen ? ("Beat" + Random.Range(1, 3)) : "Beat_Free", -1);
                        roleIndex++;
                    }
                    bgRoleAcList[roleIndex].SetCharaFrames(buildingObject.npcPicList[roleIndex], 56, 54, false);
                    bgRoleAcList[roleIndex].SetAnim(buildingObject.isOpen ? "Water" : "Water_Empty", -1);
                    roleIndex++;
                    break;
                case "DBG_WheatField":
                case "DBG_VegetableField":
                case "DBG_Orchard":
                case "DBG_FlaxField":
                    for (int i = 0; i < Mathf.Min(5, buildingObject.workerNow); i++)
                    {
                        bgRoleAcList[i].SetCharaFrames("npc_farmer1", 56, 54, true);
                        bgRoleAcList[i].SetAnim("Wipe", -1);
                        bgRoleAcList[i].SetRandomAnim(new List<string> { }, new List<byte> { }, new List<short> { });
                        roleIndex++;
                    }
                    break;
                case "DBG_Lair":
                    for (int i = 0; i <3; i++)
                    {
                        bgRoleAcList[i].SetCharaFrames(buildingObject.npcPicList[0], 56, 54, true);
                        bgRoleAcList[i].SetAnim("Idle", -1);
                        roleIndex++;
                    }
                    for (int i = 3; i < Mathf.Min(3, buildingObject.workerNow)+3; i++)
                    {
                        bgRoleAcList[i].SetCharaFrames(buildingObject.npcPicList[i], 56, 54, true);
                        bgRoleAcList[i].SetAnim("Idle", -1);
                        bgRoleAcList[i].SetRandomAnim(new List<string> { "Wipe", "Sit", "Watering" }, new List<byte> { 20, 5, 15 }, new List<short> { 5, 1, 5 });
                        roleIndex++;
                    }
                    break;
                case "DBG_Inn":
                    bgRoleAcList[0].SetCharaFrames(buildingObject.npcPicList[0], 56, 54, true);
                    bgRoleAcList[0].SetAnim("Idle", -1);
                    roleIndex++;
                    bgRoleAcList[1].SetCharaFrames(buildingObject.npcPicList[1], 56, 54, true);
                    bgRoleAcList[1].SetAnim("Idle", -1);
                    roleIndex++;
                    for (int i = 2; i < gc.districtDic[buildingObject.districtID].recruitList.Count+2; i++)
                    {
                        bgRoleAcList[i].SetCharaFrames(gc.heroDic[gc.districtDic[buildingObject.districtID].recruitList[i]].pic, 39, 54, true);
                        bgRoleAcList[i].SetAnim("Pic", -1);
                        bgRoleAcList[i].SetRandomAnim(new List<string> { "SayYes", "Laugh", "Surprise" }, new List<byte> { 20, 5, 15 }, new List<short> { 5, 5, 5 });
                        roleIndex++;
                    }
                    break;
                case "DBG_Base1":
                case "DBG_Base2":
                case "DBG_Base3":
                case "DBG_Base4":
                case "DBG_Base5":
                    for (int i = 0; i < buildingObject.npcPicList.Count; i++)
                    {
                        bgRoleAcList[i].SetCharaFrames(buildingObject.npcPicList[i], 56, 54, true);
                        bgRoleAcList[i].SetAnim("Idle", -1);
                        bgRoleAcList[i].SetRandomAnim(new List<string> { "SayYes" }, new List<byte> { 60 }, new List<short> { 5 });
                        roleIndex++;
                    }
                    break;


                default:
                    for (int i = 0; i < buildingObject.npcPicList.Count; i++)
                    {
                        bgRoleAcList[i].SetCharaFrames(buildingObject.npcPicList[i], 56, 54, true);
                        bgRoleAcList[i].SetAnim("Idle", -1);
                        bgRoleAcList[i].SetRandomAnim(new List<string> { "SayYes"}, new List<byte> { 60 }, new List<short> {  5 });
                        roleIndex++;
                    }
                    break;
            }


     
        }
        else
        {
            bgImage.sprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
        }

        for (int i = roleIndex; i < bgRoleAcList.Count; i++)
        {
            bgRoleAcList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
            bgRoleAcList[i].Stop();
        }
    }

    //功能按钮组-设置
    public void UpdateTotalSetButton(BuildingObject buildingObject)
    {
        byte buttonIndex = 0;
        switch (buildingObject.panelType)
        {
            case "Resource":
                //开工停工
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-192, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon940", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    if (buildingObject.isOpen)
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "停产";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            gc.StopProduceResource("Stop",buildingObject.id);
                            UpdateTotalSetButton(buildingObject);

                        });
                    }
                    else
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "生产";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            if (buildingObject.workerNow != 0&& buildingObject.produceEquipNow!=-1)
                            {
                                gc.CreateProduceResourceEvent(buildingObject.id);
                                UpdateTotalSetButton(buildingObject);
                        
                              
                            }
                            else
                            {
                                if (buildingObject.workerNow == 0)
                                {
                                    MessagePanel.Instance.AddMessage("缺少工人，无法开工");
                                }
                                if (buildingObject.produceEquipNow == -1)
                                {
                                    MessagePanel.Instance.AddMessage("未设置生产目标，无法开工");
                                }
                            }
                            
                        });
                    }
                    buttonIndex++;
                }
                //人事
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-140, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon240", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "人事";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                        HideOutputInfoPart();
                        HideOutputInfoPartTask();
                        HideSetForgePart();
                        ShowSetManagerPart(buildingObject, 796, -12);
                        ShowSetWorkerPart(buildingObject, 796, -120);
                        HideHistoryInfoPart();
                        HideRecruitPart();
                        HideStrengthenPart();
                        HideInlayPart();
                        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    });
                    buttonIndex++;
                }

                //升级
                if (buildingObject.upgradeTo != -1 && buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(140, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon400", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "升级";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowUpgradeBlock(buildingObject.id); });
                    buttonIndex++;
                }


                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(192, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon396", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }


                break;
            case "Forge":
                //开工停工
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-192,-32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon940", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    if (buildingObject.isOpen)
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "停产";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            gc.StopProduceItem(buildingObject.id);
                            UpdateTotalSetButton(buildingObject);
                           
                        });
                    }
                    else
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "复产";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            if (buildingObject.workerNow != 0 && buildingObject.taskList.Count != 0)
                            {
                                gc.CreateProduceItemEvent(buildingObject.id);
                                UpdateTotalSetButton(buildingObject);
                               
                            }
                            else
                            {
                                if (buildingObject.workerNow == 0)
                                {
                                    MessagePanel.Instance.AddMessage("缺少工人，无法开工");
                                }
                                if (buildingObject.taskList.Count == 0)
                                {
                                    MessagePanel.Instance.AddMessage("未设置生产目标，无法开工");
                                }
                            }
                        });
                    }
                    buttonIndex++;
                }

                //销售/关闭
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-140, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon367", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    if (buildingObject.isSale)
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "停售";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            buildingObject.isSale = false;
                            gc.BuildingStopSale(buildingObject.id);
                            UpdateTotalSetButton(buildingObject);
                        });
                    }
                    else
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "销售";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            buildingObject.isSale = true;
                            gc.CreateBuildingSaleEvent(buildingObject.id);
                            UpdateTotalSetButton(buildingObject);
                        });
                    }
                    buttonIndex++;
                }

                //下单
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-88, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon372", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "下单";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                        ShowOutputInfoPart(buildingObject, 6, -92);
                        ShowOutputInfoPartTask(buildingObject);
                        ShowSetForgePart(buildingObject, 752, -12);
                        HideSetManagerPart();
                        HideSetWorkerPart();
                        HideHistoryInfoPart();
                        HideRecruitPart();
                        HideStrengthenPart();
                        HideInlayPart();
                        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    });
                    buttonIndex++;
                }

                //强化
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-140, -84);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon855", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "强化";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                        HideOutputInfoPart();
                        HideOutputInfoPartTask();
                        HideSetForgePart();
                        HideSetManagerPart();
                        HideSetWorkerPart();
                        HideHistoryInfoPart();
                        HideRecruitPart();
                        ShowStrengthenPart(buildingObject,728,-12);
                        HideInlayPart();
                        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    });
                    buttonIndex++;
                }

                //镶嵌
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-88, -84);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon1023", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "镶嵌";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                        HideOutputInfoPart();
                        HideOutputInfoPartTask();
                        HideSetForgePart();
                        HideSetManagerPart();
                        HideSetWorkerPart();
                        HideHistoryInfoPart();
                        HideRecruitPart();
                        HideStrengthenPart();
                        ShowInlayPart(buildingObject, 700, -12);
                        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    });
                    buttonIndex++;
                }

                //人事
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-36, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon240", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "人事";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                        HideOutputInfoPart();
                        HideOutputInfoPartTask();
                        HideSetForgePart();
                        ShowSetManagerPart(buildingObject, 796, -12);
                        ShowSetWorkerPart(buildingObject, 796, -120);
                        HideHistoryInfoPart();
                        HideRecruitPart();
                        HideStrengthenPart();
                        HideInlayPart();
                        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    });
                    buttonIndex++;
                }

                //升级
                if (buildingObject.upgradeTo != -1 && buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(140, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon400", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "升级";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowUpgradeBlock(buildingObject.id); });
                    buttonIndex++;
                }

          
                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(192, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon396", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }
  

                break;
            case "House":
                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(192, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon396", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }

                break;
            case "Municipal":


                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(192, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon396", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }


                break;
            case "Inn":

                //人事
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-192, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon240", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "人事";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                        HideOutputInfoPart();
                        HideOutputInfoPartTask();
                        HideSetForgePart();
                        ShowSetManagerPart(buildingObject, 796, -12);
                        ShowSetWorkerPart(buildingObject, 796, -120);
                        HideHistoryInfoPart();
                        HideRecruitPart();
                        HideStrengthenPart();
                        HideInlayPart();
                        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    });
                    buttonIndex++;
                }


                //休息
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-140, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon234", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "休息";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                        HideOutputInfoPart();
                        HideOutputInfoPartTask();
                        HideSetForgePart();
                        HideSetManagerPart();
                        HideSetWorkerPart();
                        HideHistoryInfoPart();
                        HideRecruitPart();
                        HideStrengthenPart();
                        HideInlayPart();
                        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    });
                    buttonIndex++;
                }

                //招募
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-88, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon_talk_happy_1", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "招募";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                        HideOutputInfoPart();
                        HideOutputInfoPartTask();
                        HideSetForgePart();
                        HideSetManagerPart();
                        HideSetWorkerPart();
                        HideHistoryInfoPart();
                        HideStrengthenPart();
                        HideInlayPart();
                        ShowRecruitPart(buildingObject);
                        totalSet_backBtn.GetComponent<RectTransform>().localScale = Vector2.one;
                    });
                    buttonIndex++;
                }

                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(192, -32);
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/Other/icon396", typeof(Sprite)) as Sprite;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(1).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }


                

                break;
           
        }
        for (int i = buttonIndex; i < 7; i++)
        {
            totalSet_btnList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
        }

    }


    //基础信息栏目-更新
    public void UpdateBasicPart(BuildingObject buildingObject)
    {
        int subTitleIndex = 0;
        nameText.text = buildingObject.name;
        if (buildingObject.buildProgress == 0)
        {
            titleSubRt[subTitleIndex].localScale = Vector2.one;
            titleSubText[subTitleIndex].text = "<color=#7AC4FF>建造中</color>";
            subTitleIndex++;
        }
        else if (buildingObject.buildProgress == 2)
        {
            titleSubRt[subTitleIndex].localScale = Vector2.one;
            titleSubText[subTitleIndex].text = "<color=#7AC4FF>升级中</color>";
            subTitleIndex++;
        }

        switch (buildingObject.panelType)
        {
            case "Resource":
                if (buildingObject.isOpen)
                {
                    titleSubRt[subTitleIndex].localScale = Vector2.one;
                    titleSubText[subTitleIndex].text = "<color=#4FFF5F>生产中</color>";
                    subTitleIndex++;
                }
                else 
                {
                    titleSubRt[subTitleIndex].localScale = Vector2.one;
                    titleSubText[subTitleIndex].text = "<color=#FF342F>停止生产</color>";
                    subTitleIndex++;
                }
                break;
            case "Forge":
                if (buildingObject.isOpen)
                {
                    titleSubRt[subTitleIndex].localScale = Vector2.one;
                    titleSubText[subTitleIndex].text = "<color=#4FFF5F>制作中</color>";
                    subTitleIndex++;
                }
                else
                {
                    titleSubRt[subTitleIndex].localScale = Vector2.one;
                    titleSubText[subTitleIndex].text = "<color=#FF342F>停止制作</color>";
                    subTitleIndex++;
                }
                if (buildingObject.isSale)
                {
                    titleSubRt[subTitleIndex].localScale = Vector2.one;
                    titleSubText[subTitleIndex].text = "<color=#4FFF5F>销售中</color>";
                    subTitleIndex++;
                }
                else
                {
                    titleSubRt[subTitleIndex].localScale = Vector2.one;
                    titleSubText[subTitleIndex].text = "<color=#FF342F>停止销售</color>";
                    subTitleIndex++;
                }
                break;

        }
        for (int i = subTitleIndex; i < titleSubRt.Count; i++)
        {
            titleSubRt[i].localScale = Vector2.zero;
        }

                // picImage.overrideSprite = Resources.Load("Image/BuildingPic/" + buildingObject.mainPic, typeof(Sprite)) as Sprite;
                desText.text =  "[维护费 " + buildingObject.expense+"金币/月]\n"+ buildingObject.des;
    }


    //产出栏目-显示
    public void ShowOutputInfoPart(BuildingObject buildingObject,int x,int y)
    {
        UpdateOutputInfoPart(buildingObject);
        outputInfoRt.anchoredPosition = new Vector2(x, y);
        outputInfoRt.gameObject.SetActive(true);
        IsShowOutputInfoPart = true;
    }

    //产出栏目-更新
    public void UpdateOutputInfoPart(BuildingObject buildingObject)
    {     
        switch (buildingObject.panelType)
        {
            case "Forge":
                if (buildingObject.taskList.Count > 0)
                {
                    outputInfo_desText.text = "生产目标:" + "工艺" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].Level + "的" + gc.OutputItemTypeSmallStr(DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].Type)+
                        "\n生产数量:"+(buildingObject.taskList[0].num ==-1?"无限制": buildingObject.taskList[0].num.ToString());
                }
                else
                {
                    outputInfo_desText.text = "生产目标:<color=#FF4500>未设置</color>" ;
                }

                if (buildingObject.isOpen)
                {
                    int needTime = 0;
                    int nowTime = 0;
                    for (int i = 0; i < gc.executeEventList.Count; i++)
                    {
                        if (gc.executeEventList[i].value[1][0] == buildingObject.id)
                        {
                            needTime = gc.executeEventList[i].endTime - gc.executeEventList[i].startTime;
                            nowTime = gc.standardTime - gc.executeEventList[i].startTime;
                            break;
                        }
                    }
                    
                    outputInfo_iconImage[0].color = new Color(58 / 255f, 46 / 255f, 46 / 255f, 174 / 255f);
                    outputInfo_iconImage[0].overrideSprite = Resources.Load("Image/ItemPic/" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].OutputPic, typeof(Sprite)) as Sprite;
                    for (int i = 1; i < outputInfo_iconImage.Count; i++)
                    {
                        outputInfo_iconImage[i].color = Color.clear ;
                    }
                    
                    outputInfo_desText.text += "\n制作中[" + nowTime + "/" + needTime + "]\n消耗"+
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputWood!=0?" 木材"+ DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputWood:"")+
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputStone != 0 ? " 石料" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputStone : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputMetal != 0 ? " 金属" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputMetal : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputLeather != 0 ? " 皮革" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputLeather : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputCloth != 0 ? " 布料" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputCloth : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputTwine != 0 ? " 麻绳" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputTwine : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputBone != 0 ? " 骨块" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputBone : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputWind != 0 ? " 风粉尘" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputWind : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputFire != 0 ? " 火粉尘" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputFire : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputWater != 0 ? " 水粉尘" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputWater : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputGround != 0 ? " 地粉尘" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputGround : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputLight != 0 ? " 光粉尘" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputLight : "") +
                        (DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputDark != 0 ? " 暗粉尘" + DataManager.mProduceEquipDict[buildingObject.taskList[0].produceEquipNow].InputDark : "") + "\n";
                }
                else
                {
                    for (int i = 0; i < outputInfo_iconImage.Count; i++)
                    {
                        outputInfo_iconImage[i].color = Color.clear;
                    }
                    outputInfo_desText.text += "\n<color=#FF4500>停工中</color>";
                }
                break;
            case "Resource":
                if (buildingObject.isOpen)
                {
                    int needTime = 0;
                    int nowTime = 0;
                    for (int i = 0; i < gc.executeEventList.Count; i++)
                    {
                        if (gc.executeEventList[i].value[1][0] == buildingObject.id)
                        {
                            needTime = gc.executeEventList[i].endTime - gc.executeEventList[i].startTime;
                            nowTime = gc.standardTime - gc.executeEventList[i].startTime;
                            break;
                        }
                    }
                  
                    for (int i = 0; i < DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputPic.Count; i++)
                    {
                        outputInfo_iconImage[i].color = new Color(1f, 1f, 1f, 174 / 255f);
                        outputInfo_iconImage[i].overrideSprite = Resources.Load("Image/Other/" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputPic[i], typeof(Sprite)) as Sprite;
                    }
                    for (int i = DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputPic.Count; i < outputInfo_iconImage.Count; i++)
                    {
                        outputInfo_iconImage[i].color = Color.clear;
                    }
                    outputInfo_desText.text = DataManager.mProduceResourceDict[buildingObject.produceEquipNow].Action + "中[" + nowTime + "/" + needTime + "]\n消耗" +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputCereal != 0 ? " 谷物" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputCereal : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputVegetable != 0 ? " 蔬菜" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputVegetable : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputFruit != 0 ? " 水果" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputFruit : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputMetal != 0 ? " 肉类" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputMetal : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputFish != 0 ? " 鱼类" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputFish : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputWood != 0 ? " 木材" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputWood : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputStone != 0 ? " 石料" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputStone : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputMetal != 0 ? " 金属" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputMetal : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputLeather != 0 ? " 皮革" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputLeather : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputCloth != 0 ? " 布料" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputCloth : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputTwine != 0 ? " 麻绳" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputTwine : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputBone != 0 ? " 骨块" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputBone : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputWind != 0 ? " 风粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputWind : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputFire != 0 ? " 火粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputFire : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputWater != 0 ? " 水粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputWater : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputGround != 0 ? " 地粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputGround : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputLight != 0 ? " 光粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputLight : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputDark != 0 ? " 暗粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputDark : "") +
                        "\n基础产出" +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputCereal != 0 ? " 谷物" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputCereal+"(效率"+ (int)(gc.GetProduceResourceLaborRate(buildingObject.id) *100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputVegetable != 0 ? " 蔬菜" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputVegetable + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFruit != 0 ? " 水果" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFruit + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputMetal != 0 ? " 肉类" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputMetal + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFish != 0 ? " 鱼类" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFish + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputWood != 0 ? " 木材" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputWood + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputStone != 0 ? " 石料" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputStone + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputMetal != 0 ? " 金属" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputMetal + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputLeather != 0 ? " 皮革" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputLeather + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputCloth != 0 ? " 布料" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputCloth + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputTwine != 0 ? " 麻绳" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputTwine + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputBone != 0 ? " 骨块" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputBone + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputWind != 0 ? " 风粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputWind + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFire != 0 ? " 火粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFire + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputWater != 0 ? " 水粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputWater + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputGround != 0 ? " 地粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputGround + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputLight != 0 ? " 光粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputLight + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputDark != 0 ? " 暗粉尘" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputDark + "(效率" + (int)(gc.GetProduceResourceLaborRate(buildingObject.id) * 100) + "%)" : "");

                    if (gc.GetProduceResourceOutputUp(buildingObject.id) != 0f)
                    {
                        outputInfo_desText.text += "\n<color=#00FF00>管理者能力 +" + (int)(gc.GetProduceResourceOutputUp(buildingObject.id) * 100) + "%产出</color>";
                    }
                }
                else
                {
                    for (int i = 0; i < outputInfo_iconImage.Count; i++)
                    {
                        outputInfo_iconImage[i].color = Color.clear;
                    }
                    outputInfo_desText.text = "<color=#FF4500>停工中</color>";
                }
             
                break;
            default: break;
        }
       
    }
    
    //产出栏目-隐藏
    public void HideOutputInfoPart()
    {
        outputInfoRt.gameObject.SetActive(false);
        IsShowOutputInfoPart = false;
    }

    //产出栏目-订单子栏目-显示
    public void ShowOutputInfoPartTask(BuildingObject buildingObject)
    {
        UpdateOutputInfoPartTask(buildingObject);
        outputInfo_taskRt.localScale = Vector2.one;
        IsShowOutputInfoPartTask = true;
    }

    //产出栏目-订单子栏目-更新
    public void UpdateOutputInfoPartTask(BuildingObject buildingObject)
    {   
        for (int i = 0; i < buildingObject.taskList.Count; i++)
        {
            outputInfo_taskRtList[i].localScale = Vector2.one;
            outputInfo_taskImageList[i].sprite= Resources.Load("Image/ItemPic/" + DataManager.mProduceEquipDict[buildingObject.taskList[i].produceEquipNow].OutputPic, typeof(Sprite)) as Sprite;            
            outputInfo_taskTextList[i].text = gc.OutputItemTypeSmallStr(DataManager.mProduceEquipDict[buildingObject.taskList[i].produceEquipNow].Type)+"("+ DataManager.mProduceEquipDict[buildingObject.taskList[i].produceEquipNow].Level + "级) "+(buildingObject.taskList[i].num==-1?"∞":( "x"+buildingObject.taskList[i].num)) ;
           
            //Debug.Log(" buildingObject.taskList[i].forgeAddStuff.Count=" + buildingObject.taskList[i].forgeAddStuff.Count);
            for (int j = 0; j < 3; j++)
            {
                //Debug.Log(" buildingObject.taskList[i].forgeAddStuff[j]=" + buildingObject.taskList[i].forgeAddStuff[j]);
                switch (buildingObject.taskList[i].forgeAddStuff[j])
                {
                    case StuffType.Wood: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon959"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Stone: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon858"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Metal: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon961"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Leather: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon956"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Cloth: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon426"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Twine: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon397"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Bone: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon892"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Wind: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon920"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Fire: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon921"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Water: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon922"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Ground: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon927"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Light: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon925"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.Dark: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon924"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text = "x10"; break;
                    case StuffType.None: outputInfo_taskAddRtList[i].GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Empty"); outputInfo_taskAddRtList[i].GetChild(j).GetChild(0).GetComponent<Text>().text =""; break;
                }
            }
        }
        for (int i = buildingObject.taskList.Count; i < outputInfo_taskRtList.Count; i++)
        {
            outputInfo_taskRtList[i].localScale = Vector2.zero;
        }
    }

    //产出栏目-订单子栏目-隐藏
    public void HideOutputInfoPartTask()
    {
        outputInfo_taskRt.localScale = Vector2.zero;
        IsShowOutputInfoPartTask = false;
    }
    

    //管理人员栏目-显示
    public void ShowSetManagerPart(BuildingObject buildingObject, int x, int y)
    {
        UpdateSetManagerPart(buildingObject);

        setManagerRt.anchoredPosition = new Vector2(x, y);
        setManagerRt.gameObject.SetActive(true);
        IsShowSetManagerPart = true;
    }

    //管理人员栏目-更新
    public void UpdateSetManagerPart(BuildingObject buildingObject)
    {
        for (int i = 0; i < buildingObject.heroList.Count; i++)
        {
            setManager_imageList[i].overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[buildingObject.heroList[i]].pic + "/Pic", typeof(Sprite)) as Sprite;
            string workValue = "";
            switch (buildingObject.prototypeID)
            {
                case 9:
                case 10:
                case 11:
                case 12: workValue = "\n<color=#FFBD58>种植</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workPlanting); break;
                case 13:
                case 14: workValue = "\n<color=#FFBD58>饲养</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workFeeding); break;
                case 15: workValue = "\n<color=#FFBD58>钓鱼</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workFishing); break;
                case 16:
                case 17:
                case 18: workValue = "\n<color=#FFBD58>伐木</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workFelling); break;
                case 19:
                case 20:
                case 21: workValue = "\n<color=#FFBD58>挖矿</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workQuarrying); break;
                case 22:
                case 23:
                case 24: workValue = "\n<color=#FFBD58>采石</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workMining); break;
                case 32:
                case 33:
                case 34:
                case 35:
                case 36: workValue = "\n<color=#F0A0FF>武器锻造</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workMakeWeapon); break;
                case 37:
                case 38:
                case 39:
                case 40:
                case 41: workValue = "\n<color=#F0A0FF>防具制作</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workMakeArmor); break;
                case 42:
                case 43:
                case 44:
                case 45:
                case 46: workValue = "\n<color=#F0A0FF>饰品制作</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workMakeJewelry); break;
                case 73:
                case 74:
                case 75:
                case 76:
                case 77: workValue = "\n<color=#F0A0FF>卷轴制作</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workMakeScroll); break;


                default: workValue = "\n<color=#62D5EE>管理</color> " + gc.OutputWorkValueToRank(gc.heroDic[buildingObject.heroList[i]].workSundry); break;
            }
            setManager_textList[i].text = gc.heroDic[buildingObject.heroList[i]].name+ workValue;
            setManager_btnList[i].gameObject.GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_down", typeof(Sprite)) as Sprite;
            int hid = gc.heroDic[buildingObject.heroList[i]].id;
            setManager_btnList[i].onClick.RemoveAllListeners();
            setManager_btnList[i].onClick.AddListener(delegate () {
                /*卸下*/
                gc.BuildingManagerMinus(buildingObject.id, hid);
                UpdateSetManagerPart(buildingObject);
            });
        }
        for (int i = buildingObject.heroList.Count; i < 4; i++)
        {
            setManager_imageList[i].overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            if (i == buildingObject.heroList.Count)
            {
                setManager_textList[i].text = " <未指派>";
                setManager_btnList[i].gameObject.GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_up", typeof(Sprite)) as Sprite;
                setManager_btnList[i].onClick.RemoveAllListeners();
                setManager_btnList[i].onClick.AddListener(delegate () {
                    HeroSelectPanel.Instance.OnShow("指派管理者", buildingObject.districtID, buildingObject.id, 1, 76, -104);
                });
            }
            else
            {
                setManager_textList[i].text = " <未指派>";
                setManager_btnList[i].gameObject.GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                setManager_btnList[i].onClick.RemoveAllListeners();
            }

        }
    }

    //管理人员栏目-隐藏
    public void HideSetManagerPart()
    {
        setManagerRt.gameObject.SetActive(false);
        IsShowSetManagerPart = false;
    }


    //员工栏目-显示
    public void ShowSetWorkerPart(BuildingObject buildingObject, int x, int y)
    {
        UpdateSetWorkerPart(buildingObject);

        setWorkerRt.anchoredPosition = new Vector2(x, y);
        setWorkerRt.gameObject.SetActive(true);
        IsShowSetWorkerPart = true;
    }

    //员工栏目-更新
    public void UpdateSetWorkerPart(BuildingObject buildingObject)
    {
        int feed = gc.districtDic[gc.nowCheckingDistrictID].people - gc.districtDic[gc.nowCheckingDistrictID].worker;
        setWorker_desText.text = "空闲:" + feed + "\n 人数 " + buildingObject.workerNow + "/" + buildingObject.worker;
        if (buildingObject.workerNow > 0)
        {
            setWorker_minusBtn.interactable = true;
        }
        else
        {
            setWorker_minusBtn.interactable = false;
        }
        if (feed > 0 && (buildingObject.workerNow < buildingObject.worker))
        {
            setWorker_addBtn.interactable = true;
        }
        else
        {
            setWorker_addBtn.interactable = false;
        }
    }

    //员工栏目-隐藏
    public void HideSetWorkerPart()
    {
        setWorkerRt.gameObject.SetActive(false);
        IsShowSetWorkerPart = false;
    }


    //日志栏目-显示
    public void ShowHistoryInfoPart(BuildingObject buildingObject, int x, int y)
    {
        UpdateHistoryInfoPart(buildingObject);

        infoHistoryRt.anchoredPosition = new Vector2(x, y);
        infoHistoryRt.gameObject.SetActive(true);
        IsShowHistoryInfoPart = true;
    }

    //日志栏目-更新
    public void UpdateHistoryInfoPart(BuildingObject buildingObject )
    {
        string str = "";
        //List<LogObject> temp = new List<LogObject> { };
        foreach (KeyValuePair<int, LogObject> kvp in gc.logDic)
        {
            if (kvp.Value.type == LogType.ProduceDone && kvp.Value.value[1] == buildingObject.id)
            {
                str = "[" +gc.OutputDateStr( kvp.Value.standardTime, "Y年M月D日") + "]生产了" +DataManager.mItemDict[kvp.Value.value[2]].Name + "\n" + str;
            }
        }
        infoHistory_contentText.text = str;
    }

    //日志栏目-隐藏
    public void HideHistoryInfoPart()
    {
        infoHistoryRt.gameObject.SetActive(false);
        IsShowHistoryInfoPart = false;
    }

    //装备强化栏目-显示
    public void ShowStrengthenPart(BuildingObject buildingObject, int x, int y)
    {
        strengthenItemID = new List<short> { -1, -1, -1 };
   
        strengthenTargetID = -1;
        strengthenChooseIndex = -1;
        UpdateStrengthenPart(buildingObject);

        strengthenRt.anchoredPosition = new Vector2(x, y);
        strengthenRt.gameObject.SetActive(true);
        IsShowStrengthenPart = true;
    }

    public void UpdateStrengthenPart(BuildingObject buildingObject)
    {
        strengthen_targetPicImage.GetComponent<InteractiveLabel>().index = strengthenTargetID;
        if (strengthenTargetID != -1)
        {
            strengthen_targetPicImage.sprite = Resources.Load<Sprite>("Image/ItemPic/" + gc.itemDic[strengthenTargetID].pic);
            strengthen_targetNameText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[strengthenTargetID].rank) + ">" + gc.itemDic[strengthenTargetID].name + (gc.itemDic[strengthenTargetID].level == 0 ? "" : (" +" + gc.itemDic[strengthenTargetID].level)) + "</color>";

            if (gc.itemDic[strengthenTargetID].level >= 10)
            {
                for (int i = 0; i < strengthen_itemImageList.Count; i++)
                {
                    strengthen_itemImageList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
                    strengthenItemID[i] = -1;
                }
            }
            else
            {
                SetStrengthenItem(0, new List<short> { 28, 31, 34 });

                if (gc.itemDic[strengthenTargetID].level < 7)
                {
                    SetStrengthenItem(1, new List<short> { 27, 30, 33 });
                }
                else
                {
                    strengthen_itemImageList[1].GetComponent<RectTransform>().localScale = Vector2.zero;
                    strengthenItemID[1] = -1;
                }

                if (gc.itemDic[strengthenTargetID].level < 4)
                {
                    SetStrengthenItem(2, new List<short> { 26, 29, 32 });

                }
                else
                {
                    strengthen_itemImageList[2].GetComponent<RectTransform>().localScale = Vector2.zero;
                    strengthenItemID[2] = -1;
                }
            }

            strengthen_goldCostText.text = "费用 " + (gc.itemDic[strengthenTargetID].level+1) * 100 + "金币";
        }
        else
        {
            strengthen_targetPicImage.sprite = Resources.Load<Sprite>("Image/Other/icon071");
            strengthen_targetNameText.text = "<点击选择>";

            for (int i = 0; i < strengthen_itemImageList.Count; i++)
            {
                strengthen_itemImageList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
            }

            strengthen_goldCostText.text = "";
        }


       

        strengthen_doBtn.onClick.RemoveAllListeners();
        strengthen_doBtn.onClick.AddListener(delegate () {
            if (strengthenChooseIndex != -1)
            { 
                gc.EquipmentStrengthen(strengthenTargetID, strengthenItemID[strengthenChooseIndex]); 
            }
            else
            {
                MessagePanel.Instance.AddMessage("未选择强化道具");
            }
        });

    }

    //辅助方法
    public void SetStrengthenItem(byte uiIndex,List<short> itemIDList)
    {
        strengthen_itemImageList[uiIndex].GetComponent<RectTransform>().localScale = Vector2.one;
        strengthen_itemSelectedRtList[uiIndex].localScale = (strengthenChooseIndex == uiIndex ? Vector2.one : Vector2.zero);
        switch (DataManager.mItemDict[gc.itemDic[strengthenTargetID].prototypeID].TypeBig)
        {
            case ItemTypeBig.Weapon:
            case ItemTypeBig.Subhand:
                strengthen_itemImageList[uiIndex].sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[itemIDList[0]].Pic);
                strengthen_itemTextList[uiIndex].text = DataManager.mConsumableDict[itemIDList[0]].Name + "(持有:" + gc.consumableNum[itemIDList[0]] + ")\n强化成功率 " + DataManager.mConsumableDict[itemIDList[0]].Value[gc.itemDic[strengthenTargetID].level] + "%";
                strengthenItemID[uiIndex] = itemIDList[0];
                break;
            case ItemTypeBig.Armor:
                strengthen_itemImageList[uiIndex].sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[itemIDList[1]].Pic);
                strengthen_itemTextList[uiIndex].text = DataManager.mConsumableDict[itemIDList[1]].Name + "(持有:" + gc.consumableNum[itemIDList[1]] + ")\n强化成功率 " + DataManager.mConsumableDict[itemIDList[1]].Value[gc.itemDic[strengthenTargetID].level] + "%";
                strengthenItemID[uiIndex] = itemIDList[1];
                break;
            case ItemTypeBig.Jewelry:
                strengthen_itemImageList[uiIndex].sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[itemIDList[2]].Pic);
                strengthen_itemTextList[uiIndex].text = DataManager.mConsumableDict[itemIDList[2]].Name + "(持有:" + gc.consumableNum[itemIDList[2]] + ")\n强化成功率 " + DataManager.mConsumableDict[itemIDList[2]].Value[gc.itemDic[strengthenTargetID].level] + "%";
                strengthenItemID[uiIndex] = itemIDList[2];
                break;
        }
    }

    public void HideStrengthenPart()
    {
        strengthenRt.gameObject.SetActive(false);
        IsShowStrengthenPart = false;
    }



    //装备镶嵌栏目-显示
    public void ShowInlayPart(BuildingObject buildingObject, int x, int y)
    {
        inlayItemID = new List<short> { -1, -1, -1 };
        inlayTargetID = -1;

        UpdateInlayPart(buildingObject);

        inlayRt.anchoredPosition = new Vector2(x, y);
        inlayRt.gameObject.SetActive(true);
        IsShowInlayPart = true;
    }

    public void UpdateInlayPart(BuildingObject buildingObject)
    {
        inlay_targetPicImage.GetComponent<InteractiveLabel>().index = inlayTargetID;
        if (inlayTargetID != -1)
        {
            inlay_targetPicImage.sprite = Resources.Load<Sprite>("Image/ItemPic/" + gc.itemDic[inlayTargetID].pic);
            inlay_targetNameText.text = "<color=#" + gc.OutputItemRankColorString(gc.itemDic[inlayTargetID].rank) + ">" + gc.itemDic[inlayTargetID].name+(gc.itemDic[inlayTargetID].level==0?"":(" +"+ gc.itemDic[inlayTargetID].level)) + "</color>";
            

            for (byte i = 0; i < gc.itemDic[inlayTargetID].slotItemID.Count; i++)
            {
                inlay_itemImageList[i].GetComponent<RectTransform>().localScale = Vector2.one;
                if (gc.itemDic[inlayTargetID].slotItemID[i] != -1)
                {
                    inlay_itemImageList[i].sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[gc.itemDic[inlayTargetID].slotItemID[i]].Pic);
                    string strSlot = "";
                    for (int j = 0; j < DataManager.mConsumableDict[gc.itemDic[inlayTargetID].slotItemID[i]].AttributeType.Count; j++)
                    {
                        ItemAttribute itemAttribute = new ItemAttribute(DataManager.mConsumableDict[gc.itemDic[inlayTargetID].slotItemID[i]].AttributeType[j],
                            AttributeSource.SlotAdd, 0,
                            DataManager.mConsumableDict[gc.itemDic[inlayTargetID].slotItemID[i]].SkillID[j],
                            DataManager.mConsumableDict[gc.itemDic[inlayTargetID].slotItemID[i]].SkillAddType[j],
                            DataManager.mConsumableDict[gc.itemDic[inlayTargetID].slotItemID[i]].Value[j]);
                        strSlot += "\n " + gc.OutputAttrLineStr(itemAttribute); 
                    }

                    inlay_itemTextList[i].text ="["+ gc.itemDic[inlayTargetID].slotLevel[i] + "]-"+ DataManager.mConsumableDict[gc.itemDic[inlayTargetID].slotItemID[i]].Name+ "<color=#F3EE89>" + strSlot+"</color>"; 
                    inlay_itemChooseBtnList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
                    inlay_itemDoBtnTextList[i].text = "解除";
                    inlay_itemDoBtnList[i].GetComponent<RectTransform>().localScale = Vector2.one;
                    inlay_itemDoBtnList[i].onClick.RemoveAllListeners();
                    byte index = i;
                    inlay_itemDoBtnList[i].onClick.AddListener(delegate () {
                        gc.EquipmentUnlay(inlayTargetID, index);
                    });
                }
                else
                {
                    if (inlayItemID[i] == -1)//未镶嵌且未选择宝石
                    {
                        inlay_itemImageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon102"+(gc.itemDic[inlayTargetID].slotLevel[i]-1));
                        inlay_itemTextList[i].text = "["+ gc.itemDic[inlayTargetID].slotLevel[i] + "]-未镶嵌";


                        inlay_itemChooseBtnTextList[i].text = "选择";
                        inlay_itemChooseBtnList[i].GetComponent<RectTransform>().localScale = Vector2.one;

                        inlay_itemDoBtnList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
                    }
                    else//未镶嵌 但已选择宝石
                    {
                        inlay_itemImageList[i].sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[inlayItemID[i]].Pic);
                        string strSlot = "";
                        for (int j = 0; j < DataManager.mConsumableDict[inlayItemID[i]].AttributeType.Count; j++)
                        {
                            ItemAttribute itemAttribute = new ItemAttribute(DataManager.mConsumableDict[inlayItemID[i]].AttributeType[j],
                                AttributeSource.SlotAdd, 0,
                                DataManager.mConsumableDict[inlayItemID[i]].SkillID[j],
                                DataManager.mConsumableDict[inlayItemID[i]].SkillAddType[j],
                                DataManager.mConsumableDict[inlayItemID[i]].Value[j]);
                            strSlot += "\n " + gc.OutputAttrLineStr(itemAttribute);
                        }

                        inlay_itemTextList[i].text = "["+ gc.itemDic[inlayTargetID].slotLevel[i] + "]-未镶嵌(选择:<i>" + DataManager.mConsumableDict[inlayItemID[i]].Name + "</i>)<color=#F3EE89><i>" + strSlot + "</i></color>";

                 

                        inlay_itemChooseBtnTextList[i].text = "重选";
                        inlay_itemChooseBtnList[i].GetComponent<RectTransform>().localScale = Vector2.one;

                        inlay_itemDoBtnTextList[i].text = "镶嵌";
                        inlay_itemDoBtnList[i].GetComponent<RectTransform>().localScale = Vector2.one;
                        inlay_itemDoBtnList[i].onClick.RemoveAllListeners();
                        byte index = i;
                        inlay_itemDoBtnList[i].onClick.AddListener(delegate () {
                            //Debug.Log("inlayTargetID=" + inlayTargetID + " index=" + index + " inlayItemID[index]=" + inlayItemID[index]);
                            gc.EquipmentInlay(inlayTargetID, index, inlayItemID[index]);
                        });
                    }
                }
            }
            for (int i = gc.itemDic[inlayTargetID].slotItemID.Count; i < inlay_itemImageList.Count; i++)
            {
                inlay_itemImageList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
            }
            inlay_goldCostText.text = "费用 100金币";
        }
        else
        {
            inlay_targetPicImage.sprite = Resources.Load<Sprite>("Image/Other/icon071");
            inlay_targetNameText.text = "<点击选择>";

            for (int i = 0; i < inlay_itemImageList.Count; i++)
            {
                inlay_itemImageList[i].GetComponent<RectTransform>().localScale = Vector2.zero;
            }
            inlay_goldCostText.text = "";
        }
    }

    public void HideInlayPart()
    {
        inlayRt.gameObject.SetActive(false);
        IsShowInlayPart = false;
    }


    //订单任务栏目-显示
    public void ShowSetForgePart(BuildingObject buildingObject,int x,int y)
    {
        UpdateSetForgePart(buildingObject);

        SetForgeRt.anchoredPosition = new Vector2(x, y);
        SetForgeRt.gameObject.SetActive(true);
        IsShowSetForgePart = true;
    }

    //订单任务栏目-更新
    public void UpdateSetForgePart(BuildingObject buildingObject)
    {
        Debug.Log("前 setForge_typeDd.value=" + setForge_typeDd.value);
        Debug.Log("前 setForge_levelDd.value=" + setForge_levelDd.value);

        HideSetForgeAddBlock();
        UpdateSetForgePartNum();
        switch (buildingObject.prototypeID)
        {
            case 32:
            case 33:
            case 34:
            case 35:
            case 36:
                setForge_typeDd.ClearOptions();
                setForge_typeDd.AddOptions(new List<Dropdown.OptionData> { new Dropdown.OptionData("剑", Resources.Load<Sprite>("Image/ItemPic/w_sword_1")),
                    new Dropdown.OptionData("斧、镰刀", Resources.Load<Sprite>("Image/ItemPic/w_axe_1")),
                    new Dropdown.OptionData("枪、矛", Resources.Load<Sprite>("Image/ItemPic/w_spear_1")),
                    new Dropdown.OptionData("锤、棍棒", Resources.Load<Sprite>("Image/ItemPic/w_hammer_1")),
                    new Dropdown.OptionData("弓", Resources.Load<Sprite>("Image/ItemPic/w_bow_1")),
                    new Dropdown.OptionData("杖", Resources.Load<Sprite>("Image/ItemPic/w_staff_1")),
                    new Dropdown.OptionData("箭袋", Resources.Load<Sprite>("Image/ItemPic/s_dorlach_1")),
                    new Dropdown.OptionData("盾", Resources.Load<Sprite>("Image/ItemPic/s_shield_1")) });
                break;
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
                setForge_typeDd.ClearOptions();
                setForge_typeDd.AddOptions(new List<string> { "头部防具（重）", "头部防具（轻）", "身体防具（重）", "身体防具（轻）", "手部防具（重）", "手部防具（轻）", "背部防具（重）", "背部防具（轻）", "腿部防具（重）", "腿部防具（轻）" });
                break;
            case 42:
            case 43:
            case 44:
            case 45:
            case 46:
                setForge_typeDd.ClearOptions();
                setForge_typeDd.AddOptions(new List<string> { "项链", "戒指" });
                break;
            case 73:
            case 74:
            case 75:
            case 76:
            case 77:
                setForge_typeDd.ClearOptions();
                setForge_typeDd.AddOptions(new List<string> { "无属性卷轴", "风属性卷轴", "火属性卷轴", "水属性卷轴", "地属性卷轴", "光属性卷轴", "暗属性卷轴", "雷卷轴", "爆炸卷轴", "冰卷轴", "自然卷轴", "时空卷轴", "死亡卷轴" });
                break;
        }
        setForge_typeDd.RefreshShownValue();
        // Debug.Log(buildingObject.produceEquipNow);
        // Debug.Log(DataManager.mProduceEquipDict[buildingObject.produceEquipNow].OptionValue);
        if (buildingObject.taskList.Count>0)
        {
            setForge_typeDd.value = DataManager.mProduceEquipDict[buildingObject.taskList[buildingObject.taskList.Count-1].produceEquipNow].OptionValue;
        }
        else
        {
            setForge_typeDd.value = 0;
        }
     


        setForge_levelDd.ClearOptions();
        for (int i = 0; i < buildingObject.level; i++)
        {
            setForge_levelDd.AddOptions(new List<string> { (i + 1) + "级" });
        }
        setForge_levelDd.RefreshShownValue();
        if (buildingObject.taskList.Count > 0)
        {
            setForge_levelDd.value = DataManager.mProduceEquipDict[buildingObject.taskList[buildingObject.taskList.Count - 1].produceEquipNow].Level - 1;
        }
        else
        {
            setForge_levelDd.value = 0;
        }
        UpdateSetForgeOutputInput(buildingObject.id, setForge_typeDd.value, setForge_levelDd.value);


        Debug.Log("后 setForge_typeDd.value=" + setForge_typeDd.value);
        Debug.Log("后 setForge_levelDd.value=" + setForge_levelDd.value);
        setForgeType = setForge_typeDd.value;
        setForgeLevel = setForge_levelDd.value;

        for (int i = 0; i < forgeAddStuff.Count; i++)
        {
            setForge_btnList[i].interactable = true;

            switch (forgeAddStuff[i])
            {
                case StuffType.Wood: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon959"); setForge_textList[i].text = "木材*10"; break;
                case StuffType.Stone: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon858"); setForge_textList[i].text = "石料*10"; break;
                case StuffType.Metal: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon961"); setForge_textList[i].text = "金属*10"; break;
                case StuffType.Leather: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon956"); setForge_textList[i].text = "皮革*10"; break;
                case StuffType.Cloth: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon426"); setForge_textList[i].text = "布料*10"; break;
                case StuffType.Twine: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon397"); setForge_textList[i].text = "麻绳*10"; break;
                case StuffType.Bone: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon892"); setForge_textList[i].text = "骨块*10"; break;
                case StuffType.Wind: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon920"); setForge_textList[i].text = "风粉尘*10"; break;
                case StuffType.Fire: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon921"); setForge_textList[i].text = "火粉尘*10"; break;
                case StuffType.Water: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon922"); setForge_textList[i].text = "水粉尘*10"; break;
                case StuffType.Ground: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon927"); setForge_textList[i].text = "地粉尘*10"; break;
                case StuffType.Light: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon925"); setForge_textList[i].text = "光粉尘*10"; break;
                case StuffType.Dark: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Other/icon924"); setForge_textList[i].text = "暗粉尘*10"; break;
                case StuffType.None: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Empty"); setForge_textList[i].text = "<点击设置>"; break;
            }

        }


        List<StuffType> temp_forgeAddStuff = new List<StuffType>();
        for (int i = 0; i < forgeAddStuff.Count; i++)
        {
            temp_forgeAddStuff.Add(forgeAddStuff[i]);
        }

        setForge_updateBtn.onClick.RemoveAllListeners();
        setForge_updateBtn.onClick.AddListener(delegate () {
            gc.AddProduceItemTask(buildingObject.id, temp_forgeAddStuff, setForgePartNum);
            //gc.ChangeProduceEquipNow(buildingObject.id);
        });

    }

    //订单任务栏目-附加材料选择块-显示
    void ShowSetForgeAddBlock(int index)
    {
        setForge_addBlockRt.localScale = Vector2.one;

        setForge_addUnsetBtn.onClick.RemoveAllListeners();

        if (forgeAddStuff[index] == StuffType.None)
        {
            setForge_addUnsetSelectedRt.localScale = Vector2.one;
            setForge_addUnsetBtn.onClick.AddListener(delegate () {
                HideSetForgeAddBlock();
                    });
        }
        else
        {
            setForge_addUnsetSelectedRt.localScale = Vector2.zero;
            setForge_addUnsetBtn.onClick.AddListener(delegate () {
                gc.ChangeProduceEquipAddStuff(nowCheckingBuildingID, index, StuffType.None);
            });
        } 

        int count = 0;
        GameObject go;
        foreach (KeyValuePair<StuffType, bool> kvp in gc.forgeAddUnlock)
        {
            if (kvp.Value)
            {
                if (count < forgeGoPool.Count)
                {
                    go = forgeGoPool[count];
                    go.transform.GetComponent<RectTransform>().localScale = Vector2.one;
                }
                else
                {
                    go = Instantiate(Resources.Load("Prefab/UILabel/Label_ForgeAdd")) as GameObject;
                    go.transform.SetParent(setForge_addBlockRt.transform);
                    forgeGoPool.Add(go);
                }
                int row = count == 0 ? 0 : (count % 2);
                int col = count == 0 ? 0 : (count / 2);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(row * 124f, -26+ col * -26f);

                switch (kvp.Key)
                {
                    case StuffType.Wood: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon959"); go.transform.GetChild(2).GetComponent<Text>().text = "木材*10"; break;
                    case StuffType.Stone: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon858"); go.transform.GetChild(2).GetComponent<Text>().text = "石料*10"; break;
                    case StuffType.Metal: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon961"); go.transform.GetChild(2).GetComponent<Text>().text = "金属*10"; break;
                    case StuffType.Leather: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon956"); go.transform.GetChild(2).GetComponent<Text>().text = "皮革*10"; break;
                    case StuffType.Cloth: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon426"); go.transform.GetChild(2).GetComponent<Text>().text = "布料*10"; break;
                    case StuffType.Twine: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon397"); go.transform.GetChild(2).GetComponent<Text>().text = "麻绳*10"; break;
                    case StuffType.Bone: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon892"); go.transform.GetChild(2).GetComponent<Text>().text = "骨块*10"; break;
                    case StuffType.Wind: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon920"); go.transform.GetChild(2).GetComponent<Text>().text = "风粉尘*10"; break;
                    case StuffType.Fire: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon921"); go.transform.GetChild(2).GetComponent<Text>().text = "火粉尘*10"; break;
                    case StuffType.Water: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon922"); go.transform.GetChild(2).GetComponent<Text>().text = "水粉尘*10"; break;
                    case StuffType.Ground: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon927"); go.transform.GetChild(2).GetComponent<Text>().text = "地粉尘*10"; break;
                    case StuffType.Light: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon925"); go.transform.GetChild(2).GetComponent<Text>().text = "光粉尘*10"; break;
                    case StuffType.Dark: go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon924"); go.transform.GetChild(2).GetComponent<Text>().text = "暗粉尘*10"; break;
                }
                go.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                if (forgeAddStuff[index] == kvp.Key)
                {
                    go.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector2.one;
                    go.transform.GetComponent<Button>().onClick.AddListener(delegate () {
                        HideSetForgeAddBlock();

                    });
                }
                else
                {
                    go.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector2.zero;
                    go.transform.GetComponent<Button>().onClick.AddListener(delegate () {
                        gc.ChangeProduceEquipAddStuff(nowCheckingBuildingID, index, kvp.Key);

                    });
                }
                count++;
            }
            for (int i = count; i < forgeGoPool.Count; i++)
            {
                forgeGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
            }
        }
    }

    //订单任务栏目-附加材料选择块-关闭
    public void HideSetForgeAddBlock()
    {
        setForge_addBlockRt.localScale = Vector2.zero;
    }

    //订单任务栏目-材料输入成品输出信息-更新
    void UpdateSetForgeOutputInput(int buildingID,int type,int level)
    {
        int produceEquipID = -1;
        foreach (KeyValuePair<int, ProduceEquipPrototype> kvp in DataManager.mProduceEquipDict)
        {
            if (kvp.Value.MakePlace.Contains((byte)gc.buildingDic[buildingID].prototypeID) && kvp.Value.OptionValue == type && kvp.Value.Level == (level + 1))
            {
                produceEquipID = kvp.Value.ID;
                break;
            }
        }

        string outputStr = "";
        string inputStr = "";
        if (produceEquipID != -1)
        {
            ProduceEquipPrototype produceEquipPrototype = DataManager.mProduceEquipDict[produceEquipID];

            switch (produceEquipPrototype.Type)
            {
                case ItemTypeSmall.Sword:
                case ItemTypeSmall.Axe:
                case ItemTypeSmall.Spear:
                case ItemTypeSmall.Hammer:
                case ItemTypeSmall.Bow:
                case ItemTypeSmall.Staff:
                case ItemTypeSmall.Shield:
                case ItemTypeSmall.Dorlach:
                case ItemTypeSmall.Neck:
                case ItemTypeSmall.Finger:
                case ItemTypeSmall.HeadH:
                case ItemTypeSmall.BodyH:
                case ItemTypeSmall.HandH:
                case ItemTypeSmall.BackH:
                case ItemTypeSmall.FootH:
                case ItemTypeSmall.HeadL:
                case ItemTypeSmall.BodyL:
                case ItemTypeSmall.HandL:
                case ItemTypeSmall.BackL:
                case ItemTypeSmall.FootL:
                    for (int i = 0; i < produceEquipPrototype.OutputID.Count; i++)
                    {
                        outputStr += "<color=#" + gc.OutputItemRankColorString(DataManager.mItemDict[produceEquipPrototype.OutputID[i]].Rank) + ">" + DataManager.mItemDict[produceEquipPrototype.OutputID[i]].Name + "</color> ";
                    }
                    break;
                case ItemTypeSmall.ScrollWindI:
                case ItemTypeSmall.ScrollFireI:
                case ItemTypeSmall.ScrollWaterI:
                case ItemTypeSmall.ScrollGroundI:
                case ItemTypeSmall.ScrollLightI:
                case ItemTypeSmall.ScrollDarkI:
                case ItemTypeSmall.ScrollNone:
                case ItemTypeSmall.ScrollWindII:
                case ItemTypeSmall.ScrollFireII:
                case ItemTypeSmall.ScrollWaterII:
                case ItemTypeSmall.ScrollGroundII:
                case ItemTypeSmall.ScrollLightII:
                case ItemTypeSmall.ScrollDarkII:
                    for (int i = 0; i < produceEquipPrototype.OutputID.Count; i++)
                    {
                        outputStr += "<color=#" + gc.OutputItemRankColorString(DataManager.mSkillDict[produceEquipPrototype.OutputID[i]].Rank) + ">" + DataManager.mSkillDict[produceEquipPrototype.OutputID[i]].Name + "</color> ";
                    }
                    break;
            }
           

            if (produceEquipPrototype.InputWood != 0) { inputStr += "木材*" + produceEquipPrototype.InputWood + " "; }
            if (produceEquipPrototype.InputStone != 0) { inputStr += "石料*" + produceEquipPrototype.InputStone + " "; }
            if (produceEquipPrototype.InputMetal != 0) { inputStr += "金属*" + produceEquipPrototype.InputMetal + " "; }
            if (produceEquipPrototype.InputLeather != 0) { inputStr += "皮革*" + produceEquipPrototype.InputLeather + " "; }
            if (produceEquipPrototype.InputCloth != 0) { inputStr += "布料*" + produceEquipPrototype.InputCloth + " "; }
            if (produceEquipPrototype.InputTwine != 0) { inputStr += "麻绳*" + produceEquipPrototype.InputTwine + " "; }
            if (produceEquipPrototype.InputBone != 0) { inputStr += "骨块*" + produceEquipPrototype.InputBone + " "; }
            if (produceEquipPrototype.InputWind != 0) { inputStr += "风粉尘*" + produceEquipPrototype.InputWind + " "; }
            if (produceEquipPrototype.InputFire != 0) { inputStr += "火粉尘*" + produceEquipPrototype.InputFire + " "; }
            if (produceEquipPrototype.InputWater != 0) { inputStr += "水粉尘*" + produceEquipPrototype.InputWater + " "; }
            if (produceEquipPrototype.InputGround != 0) { inputStr += "地粉尘*" + produceEquipPrototype.InputGround + " "; }
            if (produceEquipPrototype.InputLight != 0) { inputStr += "光粉尘*" + produceEquipPrototype.InputLight + " "; }
            if (produceEquipPrototype.InputDark != 0) { inputStr += "暗粉尘*" + produceEquipPrototype.InputDark + " "; }

        }

        setForge_outputText.text = outputStr;
        setForge_inputText.text = inputStr;
    }
 
    //订单任务栏目-订单物品数量-更新
    public void UpdateSetForgePartNum()
    {
        setForge_numText.text = (setForgePartNum==-1?"∞": setForgePartNum.ToString());
    }

    //订单任务栏目-隐藏
    public void HideSetForgePart()
    {
        SetForgeRt.gameObject.SetActive(false);
        IsShowSetForgePart = false;
    }


    //招募栏目-显示更新
    public void ShowRecruitPart(BuildingObject buildingObject)
    {
        UpdateRecruitPart(buildingObject);

        recruitGo.SetActive(true);      
        IsShowRecruitPart = true;
    }

    //招募栏目-更新
    public void UpdateRecruitPart(BuildingObject buildingObject)
    {
        for (int i = 0; i < gc.districtDic[buildingObject.districtID].recruitList.Count; i++)
        {
            int heroID = gc.districtDic[buildingObject.districtID].recruitList[i];
            short prototypeID = gc.heroDic[heroID].prototypeID;


            float Hp = gc.heroDic[heroID].baseHp != 40 ? (float)(gc.heroDic[heroID].baseHp - 40) / (150 - 40) : 0;
            float Mp = gc.heroDic[heroID].baseMp != 40 ? (float)(gc.heroDic[heroID].baseMp - 40) / (150 - 40) : 0;
            float AtkMax = gc.heroDic[heroID].baseAtkMax != 3 ? (float)(gc.heroDic[heroID].baseAtkMax - 3) / (15 - 3) : 0;
            float MAtkMax = gc.heroDic[heroID].baseMAtkMax != 3 ? (float)(gc.heroDic[heroID].baseMAtkMax - 3) / (15 - 3) : 0;
            float Def = gc.heroDic[heroID].baseDef != 1 ? (float)(gc.heroDic[heroID].baseDef - 1) / (10 - 1) : 0;
            float MDef = gc.heroDic[heroID].baseMDef != 1 ? (float)(gc.heroDic[heroID].baseMDef - 1) / (10 - 1) : 0;
            float Hit = gc.heroDic[heroID].baseHit != 9 ? (float)(gc.heroDic[heroID].baseHit - 9) / (20 - 9) : 0;
            float Dod = gc.heroDic[heroID].baseDod != 9 ? (float)(gc.heroDic[heroID].baseDod - 9) / (20 - 9) : 0;
            float CriR = gc.heroDic[heroID].baseCriR != 9 ? (float)(gc.heroDic[heroID].baseCriR - 9) / (20 - 9) : 0;

            float WorkPlanting = gc.heroDic[heroID].workPlanting != 10 ? (float)(gc.heroDic[heroID].workPlanting - 10) / (150 - 10) : 0;
            float WorkFeeding = gc.heroDic[heroID].workFeeding != 10 ? (float)(gc.heroDic[heroID].workFeeding - 10) / (150 - 10) : 0;
            float WorkFishing = gc.heroDic[heroID].workFishing != 10 ? (float)(gc.heroDic[heroID].workFishing - 10) / (150 - 10) : 0;
            float WorkHunting = gc.heroDic[heroID].workHunting != 10 ? (float)(gc.heroDic[heroID].workHunting - 10) / (150 - 10) : 0;
            float WorkMining = gc.heroDic[heroID].workMining != 10 ? (float)(gc.heroDic[heroID].workMining - 10) / (150 - 10) : 0;
            float WorkQuarrying = gc.heroDic[heroID].workQuarrying != 10 ? (float)(gc.heroDic[heroID].workQuarrying - 10) / (150 - 10) : 0;
            float WorkFelling = gc.heroDic[heroID].workFelling != 10 ? (float)(gc.heroDic[heroID].workFelling - 10) / (150 - 10) : 0;
            float WorkBuild = gc.heroDic[heroID].workBuild != 10 ? (float)(gc.heroDic[heroID].workBuild - 10) / (150 - 10) : 0;

            float WorkMakeWeapon = gc.heroDic[heroID].workMakeWeapon != 10 ? (float)(gc.heroDic[heroID].workMakeWeapon - 10) / (150 - 10) : 0;
            float WorkMakeArmor = gc.heroDic[heroID].workMakeArmor != 10 ? (float)(gc.heroDic[heroID].workMakeArmor - 10) / (150 - 10) : 0;
            float WorkMakeJewelry = gc.heroDic[heroID].workMakeJewelry != 10 ? (float)(gc.heroDic[heroID].workMakeJewelry - 10) / (150 - 10) : 0;
            float WorkMakeScroll = gc.heroDic[heroID].workMakeScroll != 10 ? (float)(gc.heroDic[heroID].workMakeScroll - 10) / (150 - 10) : 0;

            float WindDam = gc.heroDic[heroID].windDam != 0 ? (float)(gc.heroDic[heroID].windDam - 0) / (20 - 0) : 0;
            float FireDam = gc.heroDic[heroID].fireDam != 0 ? (float)(gc.heroDic[heroID].fireDam - 0) / (20 - 0) : 0;
            float WaterDam = gc.heroDic[heroID].waterDam != 0 ? (float)(gc.heroDic[heroID].waterDam - 0) / (20 - 0) : 0;
            float GroundDam = gc.heroDic[heroID].groundDam != 0 ? (float)(gc.heroDic[heroID].groundDam - 0) / (20 - 0) : 0;
            float LightDam = gc.heroDic[heroID].lightDam != 0 ? (float)(gc.heroDic[heroID].lightDam - 0) / (20 - 0) : 0;
            float DarkDam = gc.heroDic[heroID].darkDam != 0 ? (float)(gc.heroDic[heroID].darkDam - 0) / (20 - 0) : 0;

            float WindRes = gc.heroDic[heroID].windRes != 0 ? (float)(gc.heroDic[heroID].windRes - 0) / (20 - 0) : 0;
            float FireRes = gc.heroDic[heroID].fireRes != 0 ? (float)(gc.heroDic[heroID].fireRes - 0) / (20 - 0) : 0;
            float WaterRes = gc.heroDic[heroID].waterRes != 0 ? (float)(gc.heroDic[heroID].waterRes - 0) / (20 - 0) : 0;
            float GroundRes = gc.heroDic[heroID].groundRes != 0 ? (float)(gc.heroDic[heroID].groundRes - 0) / (20 - 0) : 0;
            float LightRes = gc.heroDic[heroID].lightRes != 0 ? (float)(gc.heroDic[heroID].lightRes - 0) / (20 - 0) : 0;
            float DarkRes = gc.heroDic[heroID].darkRes != 0 ? (float)(gc.heroDic[heroID].darkRes - 0) / (20 - 0) : 0;

            int mj = (int)(((Hit + Dod + CriR + 0.1f) / 3f) * 50f);
            int zh = (int)(((Mp + MAtkMax + MDef + 0.1f) / 3f) * 50f);
            int ql = (int)(((WorkPlanting + WorkFeeding + WorkFishing + WorkHunting + WorkMining + WorkQuarrying + WorkFelling + WorkBuild + 0.1f) / 8f) * 50f);
            int ys = (int)(((WindDam + FireDam + WaterDam + GroundDam + LightDam + DarkDam + WindRes + FireRes + WaterRes + GroundRes + LightRes + DarkRes + 0.1f) / 12f) * 50f);
            int sy = (int)(((WorkMakeWeapon + WorkMakeArmor + WorkMakeJewelry + WorkMakeScroll + 0.1f) / 4f) * 50f);
            int yq = (int)(((Hp + AtkMax + Def + 0.1f) / 3f) * 50f);

            Debug.Log("i="+i +" " + mj + " " + zh + " " + sy + " " + ys + " " + ql + " " + yq);

            recruit_heroRtList[i].localScale = Vector2.one;
            recruit_picImageList[i].overrideSprite = Resources.Load("Image/RolePic/"  +gc.heroDic[gc.districtDic[buildingObject.districtID].recruitList[i]].pic + "/Pic", typeof(Sprite)) as Sprite; 
            recruit_nameTextList[i].text = gc.heroDic[gc.districtDic[buildingObject.districtID].recruitList[i]].name+"\n<color=#" + DataManager.mHeroDict[prototypeID].Color + ">" + DataManager.mHeroDict[prototypeID].Name +"</color>";
            recruit_growTextList[i].text = "成长 "+System.Math.Round( gc.heroDic[gc.districtDic[buildingObject.districtID].recruitList[i]].groupRate,3);


            int index = i;
            recruit_radarList[i].dataList = new List<int> { mj, zh, sy , ys, ql, yq };
            recruit_radarList[i].UpdateRadarVisualData();
            recruit_heroRtList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            recruit_heroRtList[i].GetComponent<Button>().onClick.AddListener(delegate () {
              
                HeroPanel.Instance.OnShow(buildingObject.id, gc.heroDic[gc.districtDic[buildingObject.districtID].recruitList[index]], 100, -90);
            });
        }
        for (int i = gc.districtDic[buildingObject.districtID].recruitList.Count; i < 5; i++)
        {
            recruit_heroRtList[i].localScale = Vector2.zero;
        }
    }
    
    //招募栏目-隐藏
    public void HideRecruitPart()
    {
        for (int i = 0; i < 5; i++)
        {
            recruit_radarList[i].Clear();
        }

            recruitGo.SetActive(false);
        IsShowRecruitPart = false;
    }


    //建筑物升级页-显示
    void ShowUpgradeBlock(int buildingID)
    {
        upgradeBlockRt.localScale = Vector2.one;

        BuildingPrototype bp = DataManager.mBuildingDict[gc.buildingDic[buildingID].upgradeTo];
        BuildingObject bo = gc.buildingDic[buildingID];
        upgradeBlock_nameText.text = bp.Name;

        string des = "所需资源\n" + CheckNeedToStr("wood", bp.NeedWood) + CheckNeedToStr("stone", bp.NeedStone) + CheckNeedToStr("metal", bp.NeedMetal) + CheckNeedToStr("gold", bp.NeedGold) +"\n";

        upgradeBlock_desText.text = des;

        bool canDo = true;
        if (bp.NeedWood > gc.forceDic[0].rStuffWood) { canDo = false; }
        if (bp.NeedStone > gc.forceDic[0].rStuffStone) { canDo = false; }
        if (bp.NeedMetal > gc.forceDic[0].rStuffMetal) { canDo = false; }
        if (bp.NeedGold > gc.forceDic[0].gold) { canDo = false; }

        if (canDo)
        {
            upgradeBlock_confrimBtn.GetComponent<RectTransform>().localScale = Vector2.one;
            upgradeBlock_confrimBtn.onClick.RemoveAllListeners();
            upgradeBlock_confrimBtn.onClick.AddListener(delegate ()
            {
                gc.CreateBuildingUpgradeEvent(buildingID);
                OnHide();
            });
        }
        else
        {
            upgradeBlock_confrimBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }
    //建筑物升级页-显示（辅助方法：升级所需资源）
    string CheckNeedToStr(string type, int value)
    {
        switch (type)
        {
            case "wood": return "<color=" + (value > gc.forceDic[0].rStuffWood ? "#FF5B5B>" : "white>") + "木材" + value + "</color>";
            case "stone": return " <color=" + (value > gc.forceDic[0].rStuffStone ? "#FF5B5B>" : "white>") + "石料" + value + "</color>";
            case "metal": return " <color=" + (value > gc.forceDic[0].rStuffMetal ? "#FF5B5B>" : "white>") + "金属" + value + "</color>";
            case "gold": return " <color=" + (value > gc.forceDic[0].gold ? "#FF5B5B>" : "white>") + "金币" + value + "</color>";
            default: return "未定义类型";
        }
    }

    //建筑物升级页-隐藏
    void HideUpgradeBlock()
    {
        upgradeBlockRt.localScale = Vector2.zero;
    }
    

    //建筑物拆除页-显示
    void ShowPullDownBlock(int buildingID)
    {
        pullDownBlockRt.localScale = Vector2.one;
        pullDownBlock_confrimBtn.onClick.RemoveAllListeners();
        pullDownBlock_confrimBtn.onClick.AddListener(delegate () {
            gc.BuildingPullDown(buildingID);
            OnHide();
   
        });
    }

    //建筑物拆除页-隐藏
    void HidePullDownBlock()
    {
        pullDownBlockRt.localScale = Vector2.zero;
    }

}