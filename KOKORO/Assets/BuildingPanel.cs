﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingPanel : BasePanel
{

    public static BuildingPanel Instance;

    GameControl gc;

    public RectTransform connRt;

    public Text nameText;
    public Image picImage;
    public Text desText;

    public List<RectTransform> titleSubRt;
    public List<Text> titleSubText;

    public RectTransform outputInfoRt;
    public List<Image> outputInfo_iconImage;
    public Text outputInfo_desText;

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
    public Button setForge_updateBtn;

    public List<Button> totalSet_btnList;

    public RectTransform upgradeBlockRt;
    public Text upgradeBlock_nameText;
    public Text upgradeBlock_desText;
    public Button upgradeBlock_cancelBtn;
    public Button upgradeBlock_confrimBtn;

    public RectTransform pullDownBlockRt;
    public Button pullDownBlock_cancelBtn;
    public Button pullDownBlock_confrimBtn;

    public Button closeBtn;

    public int setForgeTypeSmall = 0;
    public int setForgeType = 0;
    public int setForgeLevel = 0;

    int setForgeAddIndex = 0;

    public int nowCheckingBuildingID = -1;
    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        upgradeBlock_cancelBtn.onClick.AddListener(delegate () { HideUpgradeBlock(); });
        pullDownBlock_cancelBtn.onClick.AddListener(delegate () { HidePullDownBlock(); });
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
        
        setForge_typeDd.onValueChanged.AddListener(delegate  { setForgeType = setForge_typeDd.value; UpdateSetForgeOutputInput(nowCheckingBuildingID, setForgeType, setForgeLevel); });
        setForge_levelDd.onValueChanged.AddListener(delegate { setForgeLevel = setForge_levelDd.value; UpdateSetForgeOutputInput(nowCheckingBuildingID, setForgeType, setForgeLevel); });
        setForge_btnList[0].onClick.AddListener(delegate () { setForgeAddIndex = 0;ShowSetForgeAddBlock(); });
        setForge_btnList[1].onClick.AddListener(delegate () { setForgeAddIndex = 1; ShowSetForgeAddBlock(); });
        setForge_btnList[2].onClick.AddListener(delegate () { setForgeAddIndex = 2; ShowSetForgeAddBlock(); });
    }

    public void OnShow(BuildingObject buildingObject)
    {
        nowCheckingBuildingID = buildingObject.id;
        switch (buildingObject.panelType)
        {
            case "Forge":
                UpdateForge(buildingObject);break;
            case "Resource":
                UpdateResource(buildingObject); break;
            case "House":
                UpdateHouse(buildingObject); break;
            case "Municipal":
                UpdateMunicipal(buildingObject); break;
            default:break;
        }
        SetAnchoredPosition(0, -432);
        isShow = true;
        
        setWorker_minusBtn.onClick.RemoveAllListeners();
        setWorker_minusBtn.onClick.AddListener(delegate () { gc.BuildingWorkerMinus(buildingObject.id); });
        setWorker_addBtn.onClick.RemoveAllListeners();
        setWorker_addBtn.onClick.AddListener(delegate () { gc.BuildingWorkerAdd(buildingObject.id); });

        HideUpgradeBlock();
        HidePullDownBlock();

        if (BuildPanel.Instance.isShow)
        {
            BuildPanel.Instance.OnHide();
        }
 

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
        nowCheckingBuildingID = -1;
    }

    //制作设施模板配置
    public void UpdateForge(BuildingObject buildingObject)
    {
        UpdateBasicPart(buildingObject);
        UpdateOutputInfoPart(buildingObject);
        UpdateSetManagerPart(buildingObject);
        UpdateSetWorkerPart(buildingObject);
        UpdateHistoryInfoPart(buildingObject);
        UpdateSetForgePart(buildingObject);
        UpdateTotalSetButton(buildingObject);
    }
    //基础资源设施模板配置
    public void UpdateResource(BuildingObject buildingObject)
    {
        UpdateBasicPart(buildingObject);
        UpdateOutputInfoPart(buildingObject);
        UpdateSetManagerPart(buildingObject);
        UpdateSetWorkerPart(buildingObject);
        UpdateHistoryInfoPart(buildingObject);
        HideSetForgePart();

        UpdateTotalSetButton(buildingObject);
    }

    public void UpdateHouse(BuildingObject buildingObject)
    {
        UpdateBasicPart(buildingObject);
        HideOutputInfoPart();
        HideSetManagerPart();
        HideSetWorkerPart();
        HideHistoryInfoPart();
        HideSetForgePart();

        UpdateTotalSetButton(buildingObject);
    }
    public void UpdateMunicipal(BuildingObject buildingObject)
    {
        HideOutputInfoPart();
        HideSetManagerPart();
        HideSetWorkerPart();
        HideHistoryInfoPart();
        HideSetForgePart();

        UpdateTotalSetButton(buildingObject);
    }
    //
    public void UpdateTotalSetButton(BuildingObject buildingObject)
    {
        byte buttonIndex = 0;
        switch (buildingObject.panelType)
        {
            case "Resource":
                //开工停工
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    if (buildingObject.isOpen)
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "停产";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            gc.StopProduceResource(buildingObject.id);
                            UpdateTotalSetButton(buildingObject);

                        });
                    }
                    else
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "生产";
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
                //升级
                if (buildingObject.upgradeTo != -1 && buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "升级";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowUpgradeBlock(buildingObject.id); });
                    buttonIndex++;
                }


                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }


                for (int i = buttonIndex; i < 6; i++)
                {
                    totalSet_btnList[i].GetComponent<RectTransform>().localScale = Vector3.zero;
                }

                break;
            case "Forge":
                //开工停工
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    if (buildingObject.isOpen)
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "停止制作";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            gc.StopProduceItem(buildingObject.id);
                            UpdateTotalSetButton(buildingObject);
                           
                        });
                    }
                    else
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "允许制作";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            if (buildingObject.workerNow != 0 && buildingObject.produceEquipNow != -1)
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
                                if (buildingObject.produceEquipNow == -1)
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
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    if (buildingObject.isSale)
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "停止销售";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            buildingObject.isSale = false;
                            gc.BuildingStopSale(buildingObject.id);
                            UpdateTotalSetButton(buildingObject);
                        });
                    }
                    else
                    {
                        totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "允许销售";
                        totalSet_btnList[buttonIndex].onClick.AddListener(delegate () {
                            buildingObject.isSale = true;
                            gc.CreateBuildingSaleEvent(buildingObject.id);
                            UpdateTotalSetButton(buildingObject);
                        });
                    }
                    buttonIndex++;
                }
                //升级
                if (buildingObject.upgradeTo != -1 && buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "升级";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowUpgradeBlock(buildingObject.id); });
                    buttonIndex++;
                }


                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }


                for (int i = buttonIndex; i < 6; i++)
                {
                    totalSet_btnList[i].GetComponent<RectTransform>().localScale = Vector3.zero;
                }

                break;
            case "House":
                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }


                for (int i = buttonIndex; i < 6; i++)
                {
                    totalSet_btnList[i].GetComponent<RectTransform>().localScale = Vector3.zero;
                }

                break;
            case "Municipal":
                //拆除
                if (buildingObject.buildProgress == 1)
                {
                    totalSet_btnList[buttonIndex].GetComponent<RectTransform>().localScale = Vector3.one;
                    totalSet_btnList[buttonIndex].onClick.RemoveAllListeners();
                    totalSet_btnList[buttonIndex].transform.GetChild(0).GetComponent<Text>().text = "拆除";
                    totalSet_btnList[buttonIndex].onClick.AddListener(delegate () { ShowPullDownBlock(buildingObject.id); });
                    buttonIndex++;
                }


                for (int i = buttonIndex; i < 6; i++)
                {
                    totalSet_btnList[i].GetComponent<RectTransform>().localScale = Vector3.zero;
                }

                break;
        }
    }




    //各栏目更新
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

    public void UpdateOutputInfoPart(BuildingObject buildingObject)
    {
        outputInfoRt.anchoredPosition = new Vector2(274f, -92f);
        switch (buildingObject.panelType)
        {
            case "Forge":
                if (buildingObject.produceEquipNow != -1)
                {
                    outputInfo_desText.text = "生产目标:" + "工艺" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].Level + "的" + gc.OutputItemTypeSmallStr(DataManager.mProduceEquipDict[buildingObject.produceEquipNow].Type);
                }
                else
                {
                    outputInfo_desText.text = "生产目标:未设置";
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
                    outputInfo_iconImage[0].overrideSprite = Resources.Load("Image/ItemPic/" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].OutputPic, typeof(Sprite)) as Sprite;
                    for (int i = 1; i < outputInfo_iconImage.Count; i++)
                    {
                        outputInfo_iconImage[i].color = Color.clear ;
                    }
                    
                    outputInfo_desText.text += "\n制作中[" + nowTime + "/" + needTime + "]\n消耗"+
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputWood!=0?" 木材"+ DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputWood:"")+
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputStone != 0 ? " 石料" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputStone : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputMetal != 0 ? " 金属" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputMetal : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputLeather != 0 ? " 皮革" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputLeather : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputCloth != 0 ? " 布料" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputCloth : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputTwine != 0 ? " 麻绳" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputTwine : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputBone != 0 ? " 骨块" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputBone : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputWind != 0 ? " 风粉尘" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputWind : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputFire != 0 ? " 火粉尘" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputFire : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputWater != 0 ? " 水粉尘" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputWater : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputGround != 0 ? " 地粉尘" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputGround : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputLight != 0 ? " 光粉尘" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputLight : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputDark != 0 ? " 暗粉尘" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputDark : "") + "\n";
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

    public void HideOutputInfoPart()
    {
        outputInfoRt.anchoredPosition = new Vector2(0, 5000f);
    }

    public void UpdateSetManagerPart(BuildingObject buildingObject)
    {
        setManagerRt.anchoredPosition = new Vector2(12f, -92f);
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

    public void HideSetManagerPart()
    {
        setManagerRt.anchoredPosition = new Vector2(0, 5000f);
    }

    public void UpdateSetWorkerPart(BuildingObject buildingObject)
    {
        setWorkerRt.anchoredPosition = new Vector2(12f, -200f);
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

    public void HideSetWorkerPart()
    {
        setWorkerRt.anchoredPosition = new Vector2(0, 5000f);
    }

    public void UpdateHistoryInfoPart(BuildingObject buildingObject )
    {
        infoHistoryRt.anchoredPosition = new Vector2(798f, -12f);
        //infoHistoryRt.sizeDelta = new Vector2(256f, height );
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

    public void HideHistoryInfoPart()
    {
        infoHistoryRt.anchoredPosition = new Vector2(0, 5000f);
    }

    public void UpdateSetForgePart(BuildingObject buildingObject)
    {
        //  SetForgeRt.anchoredPosition = new Vector2(278f, -212f);
        SetForgeRt.localScale = Vector2.one;
        HideSetForgeAddBlock();

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

              //  setForge_typeDd.AddOptions(new List<string> { "剑", "斧、镰刀", "枪、矛", "锤、棍棒", "弓", "杖", "箭袋", "盾" });

  
          
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
        if (buildingObject.produceEquipNow != -1)
        {
            setForge_typeDd.value = DataManager.mProduceEquipDict[buildingObject.produceEquipNow].OptionValue;
        }
        else
        {
            setForge_typeDd.value = 0;
        }
        //setForgeType = setForge_typeDd.value;

        setForge_levelDd.ClearOptions();
        for (int i = 0; i < buildingObject.level; i++)
        {
            setForge_levelDd.AddOptions(new List<string> { (i + 1) + "级" });
        }
        setForge_levelDd.RefreshShownValue();
        if (buildingObject.produceEquipNow != -1)
        {
            setForge_levelDd.value = DataManager.mProduceEquipDict[buildingObject.produceEquipNow].Level - 1;
        }
        else
        {
            setForge_levelDd.value = 0;
        }
        UpdateSetForgeOutputInput(buildingObject.id, setForge_typeDd.value, setForge_levelDd.value);

        // setForgeLevel = setForge_levelDd.value;

        for (int i = 0; i < buildingObject.forgeAddStuff.Count; i++)
        {
            //setForge_imageList
            setForge_btnList[i].interactable = true;

            switch (buildingObject.forgeAddStuff[i])
            {
                case StuffType.Wood:setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon959");setForge_textList[i].text = "木材*10";break;
                case StuffType.Stone: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon858"); setForge_textList[i].text = "石料*10"; break;
                case StuffType.Metal: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon961"); setForge_textList[i].text = "金属*10"; break;
                case StuffType.Leather: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon956"); setForge_textList[i].text = "皮革*10"; break;
                case StuffType.Cloth: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon426"); setForge_textList[i].text = "布料*10"; break;
                case StuffType.Twine: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon397"); setForge_textList[i].text = "麻绳*10"; break;
                case StuffType.Bone: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon892"); setForge_textList[i].text = "骨块*10"; break;
                case StuffType.Wind: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon920"); setForge_textList[i].text = "风粉尘*10"; break;
                case StuffType.Fire: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon921"); setForge_textList[i].text = "火粉尘*10"; break;
                case StuffType.Water: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon922"); setForge_textList[i].text = "水粉尘*10"; break;
                case StuffType.Ground: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon927"); setForge_textList[i].text = "地粉尘*10"; break;
                case StuffType.Light: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon925"); setForge_textList[i].text = "光粉尘*10"; break;
                case StuffType.Dark: setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/ItemPic/icon924"); setForge_textList[i].text = "暗粉尘*10"; break;
            }

        }
        for (int i = buildingObject.forgeAddStuff.Count; i < setForge_btnList.Count; i++)
        {
            setForge_imageList[i].sprite = Resources.Load<Sprite>("Image/Empty");
            if (i == buildingObject.forgeAddStuff.Count)
            {
                setForge_btnList[i].interactable = true;
                setForge_textList[i].text = "<点击设置>";

            }
            else
            {
                setForge_btnList[i].interactable = false;
                setForge_textList[i].text = "";
            }
        }


        if (buildingObject.produceEquipNow != -1)
        {
            setForge_updateBtn.transform.GetChild(0).GetComponent<Text>().text = "更新";
        }
        else
        {
            setForge_updateBtn.transform.GetChild(0).GetComponent<Text>().text = "设置";
        }
        setForge_updateBtn.onClick.RemoveAllListeners();
        setForge_updateBtn.onClick.AddListener(delegate () {
            gc.ChangeProduceEquipNow(buildingObject.id);
        });

    }

    void ShowSetForgeAddBlock()
    {
        setForge_addBlockRt.localScale = Vector2.one;



    }
    void HideSetForgeAddBlock()
    {
        setForge_addBlockRt.localScale = Vector2.one;

    }
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

    public void HideSetForgePart()
    {
        SetForgeRt.localScale= Vector2.zero;
    }




    //block
    void ShowUpgradeBlock(int buildingID)
    {
        upgradeBlockRt.localScale = Vector2.one;

        BuildingPrototype bp = DataManager.mBuildingDict[gc.buildingDic[buildingID].upgradeTo];
        BuildingObject bo = gc.buildingDic[buildingID];
        upgradeBlock_nameText.text = bp.Name;

        string des = "所需资源\n" + CheckNeedToStr("wood", bp.NeedWood) + CheckNeedToStr("stone", bp.NeedStone) + CheckNeedToStr("metal", bp.NeedMetal) + CheckNeedToStr("gold", bp.NeedGold) +
                 "\n";

        

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
  
                //OnShow(gc.buildingDic[buildingID], (int)GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().anchoredPosition.y);
            });
        }
        else
        {
            upgradeBlock_confrimBtn.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    void HideUpgradeBlock()
    {
        upgradeBlockRt.localScale = Vector2.zero;
    }

    void ShowPullDownBlock(int buildingID)
    {
        pullDownBlockRt.localScale = Vector2.one;
        pullDownBlock_confrimBtn.onClick.RemoveAllListeners();
        pullDownBlock_confrimBtn.onClick.AddListener(delegate () {
            gc.BuildingPullDown(buildingID);
            OnHide();
   
        });
    }

    void HidePullDownBlock()
    {
        pullDownBlockRt.localScale = Vector2.zero;
    }

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
    string CheckNatureToStr(string type, int nowValue,int newValue,int DisCanUse)
    {
        string str = "";
        switch (type)
        {
            case "grid": str = "地块 "; break;
            case "grass": str="草地资源占用 ";break;
            case "wood": str = "森林资源占用 "; break;
            case "water": str = "水域资源占用 "; break;
            case "stone": str = "石矿资源占用 "; break;
            case "metal": str = "金属矿资源占用 "; break;
            default: return "未定义类型";
        }

        if (nowValue == newValue)
        {
            return "";
            }
        else
        {
            return "\n"+ str+"<color=" + ((DisCanUse > newValue - nowValue) ? "FF5B5B>" : "white>") + nowValue + "→" + newValue + "(当前可用 " + DisCanUse + ")</color>";

        }
    }
}
