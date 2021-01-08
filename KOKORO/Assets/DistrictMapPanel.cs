using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DistrictMapPanel : BasePanel
{

    GameControl gc;
    public static DistrictMapPanel Instance;


    public Button closeBtn;


    GameObject wantBuidingGo;
    int wantBuidingSizeX;
    int wantBuidingSizeY;
    int wantBuidingSizeYBase;
    public Canvas canvas;//画布
    public RectTransform contentRt;//坐标
    public List<Transform> layer;
    bool isChoose = false;
    Vector2 wantBuidingPos;
    Vector2 wantBuidingPos_Temp;

    List<int> x = new List<int>();
    List<int> y = new List<int>();
    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //contentRt = this.transform as RectTransform; //也可以写成this.GetComponent<RectTransform>(),但是不建议；

      //  closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChoosePosition(0);
        }

        if (isChoose)
        {

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(contentRt, Input.mousePosition, canvas.worldCamera, out wantBuidingPos))
            {

                
                wantBuidingPos = new Vector2(((int)wantBuidingPos.x / 16) * 16f- wantBuidingSizeX * 16f, ((int)wantBuidingPos.y / 16) * 16f+ wantBuidingSizeY * 16f);
                wantBuidingGo.GetComponent<RectTransform>().anchoredPosition = wantBuidingPos;

                if (wantBuidingPos != wantBuidingPos_Temp)
                {
                    if (wantBuidingPos.y != wantBuidingPos_Temp.y)
                    {
                        int layerIndex = ((int)wantBuidingPos.y / 16) * -1 + wantBuidingSizeY-1;
                        if (layerIndex >= 0 && layerIndex < 19)
                        {
                            wantBuidingGo.transform.SetParent(layer[layerIndex].transform);
                        }
                       
                    }
                    Debug.Log("zxl");
                    if (CheckCanBuild())
                    {
                        wantBuidingGo.GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        wantBuidingGo.GetComponent<Image>().color = Color.red;
                    }
                    wantBuidingPos_Temp = wantBuidingPos;
                }


            }

            if (Input.GetMouseButtonDown(0))
            {
                if (CheckCanBuild())
                {
                    ToBuild();
                    wantBuidingGo.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }


    public void OnShow(int x, int y)
    {
        SetAnchoredPosition(x, y);
        isShow = true;
    }
    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }


    public void ChoosePosition(short buildingID)
    {
        wantBuidingGo = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisBuilding")) as GameObject;
        wantBuidingSizeX = DataManager.mBuildingDict[buildingID].SizeX;
        wantBuidingSizeY = DataManager.mBuildingDict[buildingID].SizeY;
        wantBuidingSizeYBase = DataManager.mBuildingDict[buildingID].SizeYBase;
        wantBuidingGo.GetComponent<RectTransform>().sizeDelta = new Vector2(wantBuidingSizeX * 16f, wantBuidingSizeY * 16f);
        wantBuidingGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + DataManager.mBuildingDict[buildingID].MainPic);
       
        isChoose = true;
    }

    public bool CheckCanBuild()
    {
        x.Clear();
        y.Clear();
        int startx = ((int)wantBuidingPos.x / 16);
        int starty = ((int)wantBuidingPos.y / 16) * -1 + (wantBuidingSizeY - wantBuidingSizeYBase);
        Debug.Log("左上角点(占地)=" + startx + "," + starty);

        for (int i = 0; i < wantBuidingSizeX; i++)
        {
            for (int j = 0; j < wantBuidingSizeYBase; j++)
            {
                x.Add(startx + i);
                y.Add(starty + j);
            }
        }
        string str = "";
        string str2 = "";
        bool can = true;
        for (int i = 0; i < x.Count; i++)
        {

            string index = gc.nowCheckingDistrictID + "_" + x[i] + "," + y[i];
            if (gc.districtGridDic[gc.nowCheckingDistrictID].ContainsKey(index))
            {
                if (gc.districtGridDic[gc.nowCheckingDistrictID][index].buildingID != -1)
                {
                    str += "(" + x[i] + "," + y[i] + ")";
                    can = false;
                    break;
                }
            }
            else 
            {
                str2 += "(" + x[i] + "," + y[i] + ")";
                can = false;
                break;
            }

        }

        return can;

        //if (can)
        //{
          
        //}
        //else
        //{
        //    Debug.Log("不存在的点：" + str2+"不是空地的点：" + str);


        //}
        
        
        
        
    }


    void ToBuild()
    {
        for (int i = 0; i < x.Count; i++)
        {
            string index = gc.nowCheckingDistrictID + "_" + x[i] + "," + y[i];
            gc.districtGridDic[gc.nowCheckingDistrictID][index].buildingID = 0;
        }
        isChoose = false;
    }



    void  UpdateAllInfo(int districtID)
    {
        List<BuildingObject> temp = new List<BuildingObject> { };
        foreach (KeyValuePair<int, BuildingObject> kvp in gc.buildingDic)
        {
            if (kvp.Value.districtID == districtID)//&& kvp.Value.buildProgress==1
            {
                temp.Add(kvp.Value);
            }
        }

    }
}
