using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsumableListAndInfoPanel : BasePanel
{
    public static ConsumableListAndInfoPanel Instance;

    GameControl gc;

    #region 【UI控件】
    public RectTransform listRt;
    public GameObject itemListGo;
    public RectTransform list_selectedRt;

    public Image info_picImage;
    public Text info_nameText;
    public Text info_desText;

    public List<Button> funcBtn;
    public Button closeBtn;
    #endregion

    //运行变量
    public int nowItemID = -1;

    //对象池
    List<GameObject> itemGoPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    //主面板显示-查看列表
    public void OnShow(int x, int y)
    {
        nowItemID = -1;
        UpdateSelectedPos(new Vector2(0, 5000));
        GetComponent<RectTransform>().sizeDelta = new Vector2(712f, 520f);
        listRt.localScale = Vector2.one;
        UpdateList( ConsumableType.None,-1);
        ClearInfo();

        funcBtn[0].GetComponent<RectTransform>().localScale = Vector2.one;
        funcBtn[0].GetComponent<Image>().color = new Color(132 / 255f, 236 / 255f, 137 / 255f, 255 / 255f);
        funcBtn[0].transform.GetChild(0).GetComponent<Text>().text = "使用";
        funcBtn[0].onClick.RemoveAllListeners();
        funcBtn[0].onClick.AddListener(delegate () { /**ShowBatch("sale"); */});
        HideFuncBtn(3);

        SetAnchoredPosition(x, y);
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        isShow = true;
    }

    //主面板显示-选择镶嵌消耗道具
    public void OnShowByChoose(string type, int buidingID, int index,int x, int y)
    {
        nowItemID = -1;
        UpdateSelectedPos(new Vector2(0, 5000));
        GetComponent<RectTransform>().sizeDelta = new Vector2(712f, 520f);
        listRt.localScale = Vector2.one;
        if (type == "inlay")
        {

            UpdateList(ConsumableType.SlotStone, gc.itemDic[BuildingPanel.Instance.inlayTargetID].slotLevel[index]);
        }
        else
        {
            UpdateList( ConsumableType.None,-1);
        }
           
        ClearInfo();

        funcBtn[0].GetComponent<RectTransform>().localScale = Vector2.one;
        funcBtn[0].GetComponent<Image>().color = new Color(132 / 255f, 236 / 255f, 137 / 255f, 255 / 255f);
        funcBtn[0].transform.GetChild(0).GetComponent<Text>().text = "选择";
        funcBtn[0].onClick.RemoveAllListeners();
        funcBtn[0].onClick.AddListener(delegate () {
            if (type == "inlay")
            {
                BuildingPanel.Instance.inlayItemID[index] = (short)nowItemID;
                BuildingPanel.Instance.UpdateInlayPart(gc.buildingDic[buidingID]);
            }
            OnHide();
        });
        HideFuncBtn(3);

        SetAnchoredPosition(x, y);
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        isShow = true;
    }

    //主面板显示-查看物品详情
    public void OnShow(int itemID, int x, int y)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(236f, 520f);
        listRt.localScale = Vector2.zero;
        UpdateInfo(itemID);

        HideFuncBtn(4);

        SetAnchoredPosition(x, y);
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        isShow = true;
    }



    //主面板关闭
    public override void OnHide()
    {

        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isShow = false;
    }

    //功能按钮组-隐藏（指定个数）
    public void HideFuncBtn(int count)
    {
        for (int i = funcBtn.Count - 1; i >= funcBtn.Count - count; i--)
        {
            funcBtn[i].GetComponent<RectTransform>().localScale = Vector2.zero;
        }
    }

    //物品列表-更新
    public void UpdateList(ConsumableType consumableType, int slotLevelLimit)
    {
        List<int> itemObjects = new List<int>();
        for (int i = 0; i < gc.consumableNum.Count; i++)
        {
            
            if ( gc.consumableNum[i] > 0)
            {
                if (consumableType == ConsumableType.None)
                {
                    itemObjects.Add(i);
                }
                else
                {
                    if (DataManager.mConsumableDict[i].Type == consumableType && DataManager.mConsumableDict[i].SlotLevel <= slotLevelLimit )
                    {
                        itemObjects.Add(i);
                    }
                }
               
            }
        }

        GameObject go;
        for (int i = 0; i < itemObjects.Count; i++)
        {
            if (i < itemGoPool.Count)
            {
                go = itemGoPool[i];
                itemGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_Consumable")) as GameObject;
                go.transform.SetParent(itemListGo.transform);
                itemGoPool.Add(go);
            }

            int row = i == 0 ? 0 : (i % 9);
            int col = i == 0 ? 0 : (i / 9);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(row * 50f,  col * -50f);
            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[itemObjects[i]].Pic);
            go.transform.GetChild(1).GetComponent<Text>().text = gc.consumableNum[itemObjects[i]].ToString();
            go.transform.GetComponent<InteractiveLabel>().index = itemObjects[i];
            int ID = itemObjects[i];
            go.transform.GetComponent<Button>().onClick.AddListener(delegate () {
                UpdateSelectedPos(new Vector2(row * 50f, col * -50f));
                nowItemID = ID; 
                UpdateInfo(ID); });
        }
        for (int i = itemObjects.Count; i < itemGoPool.Count; i++)
        {
            itemGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        itemListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(455f, Mathf.Max(442f, (itemObjects.Count / 9) * 50f));

    }

    public void UpdateInfo(int itemID)
    {
        if (itemID == -1)
        {
            ClearInfo();
            return;
        }

        info_picImage.sprite = Resources.Load<Sprite>("Image/Other/" + DataManager.mConsumableDict[itemID].Pic);
        info_nameText.text = "<color=#" + gc.OutputItemRankColorString(DataManager.mConsumableDict[itemID].Rank) + ">" + DataManager.mConsumableDict[itemID].Name + "</color>\n" + 
            gc.OutputSignStr("★", DataManager.mConsumableDict[itemID].Rank)+"\n持有数量 "+gc.consumableNum[itemID];
        string str = "道具（";

        switch (DataManager.mConsumableDict[itemID].Type)
        {
            case ConsumableType.Drug: str += "药水）\n<color=#3FF380>";
                switch (DataManager.mConsumableDict[itemID].AttributeType[0])
                {
                    case Attribute.Hp: str += "\n   体力恢复" + DataManager.mConsumableDict[itemID].Value[0]; break;
                    case Attribute.Mp: str += "\n   魔力恢复" + DataManager.mConsumableDict[itemID].Value[0]; break;
                }
                str += "</color>";
                break;
            case ConsumableType.StrengthenStone: str += "强化石）"; break;
            case ConsumableType.SlotStone: 
                str += "镶嵌品）\n<color=#F3EE89>";
                for (int j = 0; j < DataManager.mConsumableDict[itemID].AttributeType.Count; j++)
                {
                    ItemAttribute itemAttribute = new ItemAttribute(DataManager.mConsumableDict[itemID].AttributeType[j],
                        AttributeSource.SlotAdd,0,
                        DataManager.mConsumableDict[itemID].SkillID[j],
                        DataManager.mConsumableDict[itemID].SkillAddType[j],
                        DataManager.mConsumableDict[itemID].Value[j]);
                    str += "\n   " + gc.OutputAttrLineStr(itemAttribute); 
                }
                str += "</color>";
                break;

        }

        str += "\n──────────────\n" + DataManager.mConsumableDict[itemID].Des + "\n价值 " + DataManager.mConsumableDict[itemID].Cost;
        info_desText.text = str;
    }

    void UpdateSelectedPos(Vector2 pos)
    {
        list_selectedRt.anchoredPosition = pos+new Vector2(2f,-2f);
        list_selectedRt.SetAsLastSibling();
    }

    //物品详情-清空
    public void ClearInfo()
    {
        info_picImage.sprite = Resources.Load<Sprite>("Image/Empty");
        info_nameText.text = "";
        info_desText.text = "";
    }



}
