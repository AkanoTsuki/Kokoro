using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AreaMapPanel : BasePanel, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static AreaMapPanel Instance;
    GameControl gc;
    public GameObject roadGo;
    public GameObject buildingGo;
    public GameObject wallGo;


    Vector3 offset;
    RectTransform rt;
    Vector3 pos;
    float minWidth;             //水平最小拖拽范围
    float maxWidth;            //水平最大拖拽范围
    float minHeight;            //垂直最小拖拽范围  
    float maxHeight;            //垂直最大拖拽范围
    float rangeX;               //拖拽范围
    float rangeY;               //拖拽范围


    public List<GameObject> pathPoint;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        pos = rt.position;

        minWidth = rt.rect.width / 2;
        maxWidth = Screen.width - (rt.rect.width / 2);
        minHeight = rt.rect.height / 2;
        maxHeight = Screen.height - (rt.rect.height / 2);

        string str = "";
        for (int i = 0; i < pathPoint.Count; i++)
        {
            str += pathPoint[i] .name+ ","+ (int)pathPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.x + "," + (int)pathPoint[i].transform.GetComponent<RectTransform>().anchoredPosition.y+"\\n";
        }
        Debug.Log(str);
    }

    void Update()
    {
       // DragRangeLimit();
    }

    public void OnShow( int x, int y)
    {

        //for (int i = 0; i < gc.buildingDic.Count; i++)
        //{
        //    AddIconByBuilding(gc.buildingDic[i].id);
        //}
        SetAnchoredPosition(x, y);
        //ShowByImmediately(true);
    }

    void CreateTraveller(int StartPoint)
    {
        
    }


    //public void AddIconByBuilding( int buildingID)
    //{
    //    for (int i = 0; i < gc.buildingDic[buildingID].gridList.Count; i++)
    //    {
    //        AddIconByGrid(gc.buildingDic[buildingID].districtID, buildingID, DataManager.mDistrictGridDict[gc.buildingDic[buildingID].gridList[i]].X, DataManager.mDistrictGridDict[gc.buildingDic[buildingID].gridList[i]].Y);
    //    }
    //}
    //public void RemoveIconByBuilding(int buildingID)
    //{
    //    for (int i = 0; i < gc.buildingDic[buildingID].gridList.Count; i++)
    //    {
    //        RemoveIconByGrid( DataManager.mDistrictGridDict[gc.buildingDic[buildingID].gridList[i]].X, DataManager.mDistrictGridDict[gc.buildingDic[buildingID].gridList[i]].Y);

    //    }
    //}



    //public void AddIconByGrid(int districtID,int buildingID,int gridX, int gridY)
    //{

    //    string str = "";
    //    if (buildingID == 59 || buildingID == 60 || buildingID == 61 ||
    //        buildingID == 62 || buildingID == 63 || buildingID == 64)
    //    {
    //        str = "BuildingPic/"+ gc.buildingDic[buildingID].name;
    //    }
    //    else
    //    {
    //        switch (districtID)
    //        {
    //            case 2: str = "BuildingMap/Snow_"; break;
    //            case 4: str = "BuildingMap/Sand_"; break;
    //            default: str = "BuildingMap/Red_"; break;
    //        }
    //    }



    //    GameObject go;

    //    go = Instantiate(Resources.Load("Prefab/UILabel/Label_MapGrid")) as GameObject;
    //    go.transform.SetParent(buildingGo.transform);
    //    go.GetComponent<RectTransform>().anchoredPosition = new Vector3(gridX * 16f, gridY * -16f, 0f);
    //   // go.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/" + str + gc.buildingDic[buildingID].mapPic);
    //    go.name = gridX +","+ gridY;

    //}

    //public void RemoveIconByGrid(int gridX, int gridY)
    //{
    //    GameObject go = GameObject.Find("Canvas/AreaMapPanel/Building/" + gridX + "," + gridY);
    //    Destroy(go);
    //}

    /// <summary>
    /// 拖拽范围限制
    /// </summary>
    void DragRangeLimit()
    {
        //限制水平/垂直拖拽范围在最小/最大值内
        rangeX = Mathf.Clamp(rt.position.x, minWidth, maxWidth);
        rangeY = Mathf.Clamp(rt.position.y, minHeight, maxHeight);
        //更新位置
        rt.position = new Vector3(rangeX, rangeY, 0);
    }

    /// <summary>
    /// 开始拖拽
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;

        //将屏幕坐标转换成世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            //计算UI和指针之间的位置偏移量
            offset = rt.position - globalMousePos;
        }
    }

    /// <summary>
    /// 拖拽中
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// 更新UI的位置
    /// </summary>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 globalMousePos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            rt.position = offset + globalMousePos;
        }
    }

    }
