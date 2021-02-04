using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiplomacyPanel : BasePanel
{
    public static DiplomacyPanel Instance;

    GameControl gc;
    public GameObject forceListGo;

    public Button closeBtn;

    List<GameObject> forceGoPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public override void OnShow()
    {
        UpdateList();

        gameObject.SetActive(true);
        isShow = true;
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
        isShow = false;
    }

    public void UpdateList()
    {
        List<ForceObject> itemObjects = new List<ForceObject>();

        foreach (KeyValuePair<int, ForceObject> kvp in gc.forceDic)
        {
            if (kvp.Value.id!=0)
            {
                itemObjects.Add(kvp.Value);
            }
        }

        GameObject go;
        for (int i = 0; i < itemObjects.Count; i++)
        {
            if (i < forceGoPool.Count)
            {
                go = forceGoPool[i];
                forceGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.one;
            }
            else
            {
                go = Instantiate(Resources.Load("Prefab/UILabel/Label_ForceInDiplomacy")) as GameObject;
                go.transform.SetParent(forceListGo.transform);
                forceGoPool.Add(go);
            }
            int row = i == 0 ? 0 : (i % 2);
            int col = i == 0 ? 0 : (i / 2);

            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(2f + row * 310f, -2 + col * -174f, 0f);
            go.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon_flag_" + itemObjects[i].flagIndex + "_a");
            go.transform.GetChild(2).GetComponent<Text>().text = itemObjects[i].name;
            go.transform.GetChild(3).GetComponent<Text>().text = "领主 "+itemObjects[i].leader;

            string str = itemObjects[i].parentID!=-1? gc.forceDic[itemObjects[i].parentID].name :"无";

            for (int j = 0; j < itemObjects[i].childrenID.Count; j++)
            {
                str += "\n" + gc.forceDic[itemObjects[i].childrenID[j]].name + "[" + itemObjects[i].relation[itemObjects[i].childrenID[j]] + "]";
            }
            if (itemObjects[i].childrenID.Count == 0)
            {
                str += "\n无";
            }
            go.transform.GetChild(5).GetComponent<Text>().text = str;

            for (int j = 0; j < itemObjects[i].districtID.Count; j++)
            {
                go.transform.GetChild(6).GetChild(j+1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/AreaPic/" + DataManager.mDistrictDict[itemObjects[i].districtID[j]].Pic);
            }
            for (int j = itemObjects[i].districtID.Count; j < 12; j++)
            {
                go.transform.GetChild(6).GetChild(j + 1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Empty");
            }

            go.transform.GetChild(7).GetChild(1).GetChild(0).GetComponent<Text>().text = itemObjects[i].gold.ToString();
            go.transform.GetChild(7).GetChild(2).GetChild(0).GetComponent<Text>().text = gc.GetForceStuffAll(itemObjects[i].id).ToString();
            go.transform.GetChild(7).GetChild(3).GetChild(0).GetComponent<Text>().text = "未知";

            if (itemObjects[i].relation[0] < -50)
            {
                go.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon_talk_exclamation_1");
                go.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "对你十分愤怒";
            }
            else if (itemObjects[i].relation[0] >= -50 && itemObjects[i].relation[0] < 0)
            {
                go.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon_talk_sad_1");
                go.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "有点讨厌你";
            }
            else if (itemObjects[i].relation[0] >= 0 && itemObjects[i].relation[0] < 50)
            {
                go.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Empty");
                go.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "对你没什么特别看法";
            }
            else if (itemObjects[i].relation[0] >= 50 && itemObjects[i].relation[0] < 100)
            {
                go.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon_talk_happy_1");
                go.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "认为你值得交流";
            }
            else
            {
                go.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Other/icon_talk_love_1");
                go.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "觉得你是很好的朋友";
            }
  


        }
        for (int i = itemObjects.Count; i < forceGoPool.Count; i++)
        {
            forceGoPool[i].transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        forceListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(623f, Mathf.Max(523f, 2 + (itemObjects.Count / 2) * 174f));
    }
}
