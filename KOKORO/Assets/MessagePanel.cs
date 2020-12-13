using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessagePanel : BasePanel
{
    public static MessagePanel Instance;

    GameControl gc;
    GameControlInPlay gci;
    public GameObject messageListGo;
    public Scrollbar messageSb;
    int count = 0;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();
    }

    public void OnShow(int x,int y)
    {

        SetAnchoredPosition(0, 26);

    }

    public override void OnHide()
    {
        SetAnchoredPosition(0, -156);
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
            case LogType.ProduceDone: str =gc.districtDic[ logObject.value1].baseName +"的"+gc.buildingDic[logObject.value2]+"生产了"+gc.itemDic[logObject.value3]; break;
            case LogType.BuildDone: str = gc.districtDic[logObject.value1].baseName + "的" + gc.buildingDic[logObject.value2] + "建造完成了"; break;
            default:str = "未定义日志类型";break;
        }
   

        GameObject go;
        go = Instantiate(Resources.Load("Prefab/UILabel/Label_Message")) as GameObject;
        go.transform.SetParent(messageListGo.transform);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, count * 30f, 0f);
        go.transform.GetChild(0).GetComponent<Text>().text = str;
        if (logObject.value1 != -1)
        {
            go.GetComponent<InteractiveLabel>().index = logObject.id;
        }
        else
        {
            go.transform.GetChild(1).localScale = Vector3.zero;
        }
        messageListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(313f, 30f+count * 30f);

        count++;
        messageSb.value = 1f;
    }

}
