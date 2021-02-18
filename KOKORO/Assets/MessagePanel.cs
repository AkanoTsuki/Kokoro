using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MessagePanel : BasePanel
{
    public static MessagePanel Instance;

    GameControl gc;
    GameControlInPlay gci;
    public GameObject messageListGo;
    public Scrollbar messageSb;

    public Button modeBtn;
    public Text modeText;
    //public Button openOrCloseBtn;
    //public Text openOrCloseText;

    int count = 0;
    int mode = 86;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();
    }

    void Start()
    {
        mode = 86;
        modeText.text = "详细";
        modeBtn.onClick.AddListener(delegate () {
            if (mode==86)
            {
                SetMode(300);
                modeText.text = "隐藏";
            }
            else if (mode == 300)
            {
                SetMode(0);
                modeText.text = "显示";
            }
            else if (mode == 0)
            {
                SetMode(86);
                modeText.text = "详细";
            }
        });
    }

    //public void OnShow(int y)
    //{
    //   // GetComponent<RectTransform>().DOLocalMoveY(y, 2f);
    //    //SetAnchoredPosition(x, y);

    //    //openOrCloseText.text = "隐藏";
    //    //isShow = true;
    //}

    //public override void OnHide()
    //{
    //    //SetAnchoredPosition(0, 5000);

    //    openOrCloseText.text = "显示";
    //    isShow = false;
    //}

    void SetMode(int mode)
    {
        GetComponent<RectTransform>().DOSizeDelta(new Vector2(500, mode),1f);
        this.mode = mode;
    }


    public void UpdateAllInfo(GameControl gc)
    {
        for (int i = 0; i < gci.needShowMessageList.Count; i++)
        {
           // AddMessage
        }
    }
    public void AddMessage(LogObject logObject)
    {
        string str = "";
        switch (logObject.type)
        {
            case LogType.Info:str = logObject.text;break;
            case LogType.ProduceDone: str =gc.districtDic[ logObject.value[0]].baseName +"的"+gc.buildingDic[logObject.value[1]]+"生产了"+gc.itemDic[logObject.value[2]]; break;
            case LogType.BuildDone: str = gc.districtDic[logObject.value[0]].baseName + "的" + gc.buildingDic[logObject.value[1]] + "建造完成了"; break;
            default:str = "未定义日志类型";break;
        }
   

        GameObject go;
        go = Instantiate(Resources.Load("Prefab/UILabel/Label_Message")) as GameObject;
        go.transform.SetParent(messageListGo.transform);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, count * 16f, 0f);
        go.transform.GetChild(0).GetComponent<Text>().text = str;
        if (logObject.value[0] != -1)
        {
            go.GetComponent<InteractiveLabel>().index = logObject.id;
        }
        else
        {
            go.transform.GetChild(1).localScale = Vector3.zero;
        }
        messageListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(313f, 16f+count * 16f);

        count++;
        messageSb.value = 1f;
    }

    public void AddMessage(string content)
    {
        GameObject go;
        go = Instantiate(Resources.Load("Prefab/UILabel/Label_Message")) as GameObject;
        go.transform.SetParent(messageListGo.transform);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, count * 16f, 0f);
        go.transform.GetChild(0).GetComponent<Text>().text = content;
        go.transform.GetChild(1).localScale = Vector3.zero;
        messageListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(313f, 16f + count * 16f);

        count++;
        messageSb.value = 1f;
    }



}
