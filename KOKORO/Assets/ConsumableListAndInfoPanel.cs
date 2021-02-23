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

    public Image info_picText;
    public Text info_nameText;
    public Text info_desText;

    public List<Button> funcBtn;
    public Button closeBtn;
    #endregion

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

    public void OnShow(int x, int y)
    {
        
    }

    //主面板显示-查看物品详情
    public void OnShow(int itemID, int x, int y)
    {
        
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
    public void UpdateList()
    {
        
    }

    public void UpdateInfo(int itemID)
    {
        
    }
}
