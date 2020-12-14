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

    public RectTransform outputInfoRt;
    public Image outputInfo_iconImage;
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

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnShow(BuildingObject buildingObject, int x, int y, int connY)
    {


        switch (buildingObject.panelType)
        {
            case "Forge":
                UpdateForge(buildingObject);break;
            default:break;
        }
        SetAnchoredPosition(x, y);
    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
    }


    public void UpdateForge(BuildingObject buildingObject)
    {


        nameText.text = buildingObject.name;
        picImage.overrideSprite = Resources.Load("Image/BuildingPic/" + buildingObject.mainPic, typeof(Sprite)) as Sprite; 
        desText.text = gc.OutputSignStr("★", buildingObject.level)+"\n 维护费 "+ buildingObject.expense;

        outputInfoRt.anchoredPosition = new Vector2(16f,-120f);


        setManagerRt.anchoredPosition = new Vector2(16f, -298f);

        for(int i=0;i< buildingObject.heroList.Count; i++)
        {
            setManager_imageList[i].overrideSprite = Resources.Load("Image/RolePic/" + gc.heroDic[buildingObject.heroList[i]].pic, typeof(Sprite)) as Sprite;
            setManager_textList[i].text = gc.heroDic[buildingObject.heroList[i]].name;
            setManager_btnList[i].gameObject.GetComponent<Image>().overrideSprite = Resources.Load("Image/Other/to_down", typeof(Sprite)) as Sprite;
            setManager_btnList[i].onClick.RemoveAllListeners();
            setManager_btnList[i].onClick.AddListener(delegate () { /*卸下*/ });
        }
        for (int i = buildingObject.heroList.Count; i < 4; i++)
        {
            setManager_imageList[i].overrideSprite = Resources.Load("Image/Empty" , typeof(Sprite)) as Sprite;
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


        setWorkerRt.anchoredPosition = new Vector2(16f, -404f);

        int feed = gc.districtDic[gc.nowCheckingDistrictID].people - gc.districtDic[gc.nowCheckingDistrictID].worker;
        setWorker_desText.text = "空闲:" + feed + "\n 人数 " + buildingObject.workerNow + "/" + buildingObject.worker;

        if (buildingObject.workerNow>0)
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

        infoHistoryRt.anchoredPosition = new Vector2(278f, -16f);
        infoHistoryRt.sizeDelta = new Vector2(256f, 276f);

        //infoHistory_contentText
        string str = "";
        List<LogObject> temp = new List<LogObject> { };

        foreach (KeyValuePair<int, LogObject> kvp in gc.logDic)
        {
            if (kvp.Value.type == LogType.ProduceDone && kvp.Value.value2 == buildingObject.id)
            {
                str = "[" + kvp.Value.standardTime + "]生产了" + gc.itemDic[kvp.Value.value3].name+"\n"+str;
            }

        }
        infoHistory_contentText.text = str;
    }
}
