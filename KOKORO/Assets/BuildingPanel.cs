using System.Collections;
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
    public Dropdown setForge_typeDd;
    public Dropdown setForge_levelDd;
    public List<Image> setForge_imageList;
    public List<Text> setForge_textList;
    public List<Button> setForge_btnList;
    public Button setForge_updateBtn;

    public Button closeBtn;

    public int setForgeTypeSmall = 0;
    public int setForgeType = 0;
    public int setForgeLevel = 0;

    public int nowCheckingBuildingID = -1;
    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
        
        setForge_typeDd.onValueChanged.AddListener(delegate  { setForgeType = setForge_typeDd.value; });
        setForge_levelDd.onValueChanged.AddListener(delegate { setForgeLevel = setForge_levelDd.value; });
    }

    public void OnShow(BuildingObject buildingObject, int x, int y, int connY)
    {
        switch (buildingObject.panelType)
        {
            case "Forge":
                UpdateForge(buildingObject);break;
            case "Resource":

                break;
            default:break;
        }
        SetAnchoredPosition(x, y);
        isShow = true;
        nowCheckingBuildingID = buildingObject.id;
        setWorker_minusBtn.onClick.RemoveAllListeners();
        setWorker_minusBtn.onClick.AddListener(delegate () { gc.BuildingWorkerMinus(buildingObject.id); });
        setWorker_addBtn.onClick.RemoveAllListeners();
        setWorker_addBtn.onClick.AddListener(delegate () { gc.BuildingWorkerAdd(buildingObject.id); });


    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
        nowCheckingBuildingID = -1;
    }


    public void UpdateForge(BuildingObject buildingObject)
    {
        UpdateBasicPart(buildingObject);
        UpdateOutputInfoPart(buildingObject);
        UpdateSetManagerPart(buildingObject);
        UpdateSetWorkerPart(buildingObject);
        UpdateHistoryInfoPart(buildingObject, 276f);
        UpdateSetForgePart(buildingObject);
    }

    //各栏目更新
    public void UpdateBasicPart(BuildingObject buildingObject)
    {
        nameText.text = buildingObject.name;
        picImage.overrideSprite = Resources.Load("Image/BuildingPic/" + buildingObject.mainPic, typeof(Sprite)) as Sprite;
        desText.text = gc.OutputSignStr("★", buildingObject.level) + "\n 维护费 " + buildingObject.expense;
    }

    public void UpdateOutputInfoPart(BuildingObject buildingObject)
    {
        outputInfoRt.anchoredPosition = new Vector2(16f, -120f);
        switch (buildingObject.panelType)
        {
            case "Forge":
                if (buildingObject.produceEquipNow != -1)
                {
                    int needTime = 0;
                    int nowTime = 0;
                    for (int i = 0; i < gc.executeEventList.Count; i++)
                    {
                        if (gc.executeEventList[i].value[1] == buildingObject.id)
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
                    
                    outputInfo_desText.text = gc.OutputItemTypeSmallStr(DataManager.mProduceEquipDict[buildingObject.produceEquipNow].Type) +
                        "-级别" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].Level + " 制作中...[" + nowTime + "/" + needTime + "]\n消耗"+
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputWood!=0?" 木材"+ DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputWood:"")+
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputStone != 0 ? " 石料" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputStone : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputMetal != 0 ? " 金属" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputMetal : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputLeather != 0 ? " 皮革" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputLeather : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputCloth != 0 ? " 布料" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputCloth : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputTwine != 0 ? " 麻绳" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputTwine : "") +
                        (DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputBone != 0 ? " 骨块" + DataManager.mProduceEquipDict[buildingObject.produceEquipNow].InputBone : "") +"\n";
                }
                else
                {
                    for (int i = 0; i < outputInfo_iconImage.Count; i++)
                    {
                        outputInfo_iconImage[i].color = Color.clear;
                    }
                    outputInfo_desText.text = "停工中";
                }
                break;
            case "Resource":
                if (buildingObject.produceEquipNow != -1)
                {
                    int needTime = 0;
                    int nowTime = 0;
                    for (int i = 0; i < gc.executeEventList.Count; i++)
                    {
                        if (gc.executeEventList[i].value[1] == buildingObject.id)
                        {
                            needTime = gc.executeEventList[i].endTime - gc.executeEventList[i].startTime;
                            nowTime = gc.standardTime - gc.executeEventList[i].startTime;
                            break;
                        }
                    }
                  
                    for (int i = 0; i < DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputPic.Count; i++)
                    {
                        outputInfo_iconImage[i].color = new Color(1f, 1f, 1f, 174 / 255f);
                        outputInfo_iconImage[i].overrideSprite = Resources.Load("Image/" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputPic[i], typeof(Sprite)) as Sprite;
                    }
                    for (int i = DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputPic.Count; i < outputInfo_iconImage.Count; i++)
                    {
                        outputInfo_iconImage[i].color = Color.clear;
                    }
                    outputInfo_desText.text = DataManager.mProduceResourceDict[buildingObject.produceEquipNow].Action + "中...[" + nowTime + "/" + needTime + "]\n消耗" +
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
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputBone != 0 ? " 骨块" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].InputBone : "") + "\n产出" +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputCereal != 0 ? " 谷物" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputCereal : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputVegetable != 0 ? " 蔬菜" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputVegetable : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFruit != 0 ? " 水果" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFruit : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputMetal != 0 ? " 肉类" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputMetal : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFish != 0 ? " 鱼类" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputFish : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputWood != 0 ? " 木材" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputWood : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputStone != 0 ? " 石料" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputStone : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputMetal != 0 ? " 金属" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputMetal : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputLeather != 0 ? " 皮革" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputLeather : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputCloth != 0 ? " 布料" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputCloth : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputTwine != 0 ? " 麻绳" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputTwine : "") +
                        (DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputBone != 0 ? " 骨块" + DataManager.mProduceResourceDict[buildingObject.produceEquipNow].OutputBone : "");

                }
                else
                {
                    for (int i = 0; i < outputInfo_iconImage.Count; i++)
                    {
                        outputInfo_iconImage[i].color = Color.clear;
                    }
                    outputInfo_desText.text = "停工中";
                }
             
                break;
            default: break;
        }
       
    }

    public void UpdateSetManagerPart(BuildingObject buildingObject)
    {
        setManagerRt.anchoredPosition = new Vector2(16f, -298f);
        for (int i = 0; i < buildingObject.heroList.Count; i++)
        {
            setManager_imageList[i].overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[buildingObject.heroList[i]].pic, typeof(Sprite)) as Sprite;
            setManager_textList[i].text = gc.heroDic[buildingObject.heroList[i]].name;
            setManager_btnList[i].gameObject.GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_down", typeof(Sprite)) as Sprite;
            setManager_btnList[i].onClick.RemoveAllListeners();
            setManager_btnList[i].onClick.AddListener(delegate () { /*卸下*/ });
        }
        for (int i = buildingObject.heroList.Count; i < 4; i++)
        {
            setManager_imageList[i].overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
            if (i == buildingObject.heroList.Count)
            {
                setManager_textList[i].text = " <未指派>";
                setManager_btnList[i].gameObject.GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_up", typeof(Sprite)) as Sprite;
                setManager_btnList[i].onClick.RemoveAllListeners();
                setManager_btnList[i].onClick.AddListener(delegate () { /*指派*/ });
            }
            else
            {
                setManager_textList[i].text = " <未指派>";
                setManager_btnList[i].gameObject.GetComponent<Image>().overrideSprite = Resources.Load("Image/Empty", typeof(Sprite)) as Sprite;
                setManager_btnList[i].onClick.RemoveAllListeners();
            }

        }
    }


    public void UpdateSetWorkerPart(BuildingObject buildingObject)
    {
        setWorkerRt.anchoredPosition = new Vector2(16f, -404f);
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

    public void UpdateHistoryInfoPart(BuildingObject buildingObject ,float height)
    {
        infoHistoryRt.anchoredPosition = new Vector2(278f, -16f);
        infoHistoryRt.sizeDelta = new Vector2(256f, height );
        string str = "";
        List<LogObject> temp = new List<LogObject> { };
        foreach (KeyValuePair<int, LogObject> kvp in gc.logDic)
        {
            if (kvp.Value.type == LogType.ProduceDone && kvp.Value.value[1] == buildingObject.id)
            {
                str = "[" +gc.OutputDateStr( kvp.Value.standardTime, "Y年M月D日") + "]生产了" +DataManager.mItemDict[kvp.Value.value[2]].Name + "\n" + str;
            }
        }
        infoHistory_contentText.text = str;
    }

    public void UpdateSetForgePart(BuildingObject buildingObject)
    {
        switch (buildingObject.prototypeID)
        {
            case 32:
            case 33:
            case 34:
            case 35:
            case 36:
                setForge_typeDd.ClearOptions();
                setForge_typeDd.AddOptions(new List<string> { "剑", "斧、镰刀", "枪、矛", "锤、棍棒", "弓", "杖", "箭袋" });
                break;
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
                setForge_typeDd.ClearOptions();
                setForge_typeDd.AddOptions(new List<string> { "头部防具（重）", "头部防具（轻）", "身体防具（重）", "身体防具（轻）", "手部防具（重）", "手部防具（轻）", "背部防具（重）", "背部防具（轻）", "腿部防具（重）", "腿部防具（轻）", "盾" });
                break;
            case 42:
            case 43:
            case 44:
            case 45:
            case 46:
                setForge_typeDd.ClearOptions();
                setForge_typeDd.AddOptions(new List<string> { "项链", "戒指" });
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

        // setForgeLevel = setForge_levelDd.value;

        if (buildingObject.produceEquipNow != -1)
        {
            setForge_updateBtn.transform.GetChild(0).GetComponent<Text>().text = "更新";
        }
        else
        {
            setForge_updateBtn.transform.GetChild(0).GetComponent<Text>().text = "生产";
        }
        setForge_updateBtn.onClick.RemoveAllListeners();
        setForge_updateBtn.onClick.AddListener(delegate () { gc.ChangeProduceEquipNow(buildingObject.id); });

    }
}
