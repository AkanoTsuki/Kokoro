using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DistrictMainPanel : BasePanel
{
    public static DistrictMainPanel Instance;

    GameControl gc;

    public Text nameText;
    public Text desText;

    public Text natureDesText;
    public Text cultureDesText;
    public Text outputDesText;

    public Text buildingText;
    public GameObject buildingListGo;

    public Text heroText;
    public GameObject heroListGo;

    public Button buildBtn;

    public Button closeBtn;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameControlInPlay gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();
        //buildBtn.onClick.AddListener(delegate () { gci.OpenBuild(); });
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public void OnShow(DistrictObject districtObject, int x, int y)
    {

        UpdateAllInfo(gc, districtObject);
        SetAnchoredPosition(x, y);
        //ShowByImmediately(true);
        isShow = true;
    }

    public void UpdateAllInfo(GameControl gc, DistrictObject districtObject)
    {
        nameText.text = districtObject.name;
        desText.text = "·据点 <color=#ffffff>" + districtObject.baseName + "[镇]</color> "+ "\n·守护 <color=#ffffff>" + gc.heroDic[0].name + "</color> ";


        UpdateNatureInfo(districtObject);
        UpdateCultureInfo(districtObject);
        UpdateOutputInfo(districtObject);
        UpdateBuildingInfo(districtObject);
        UpdateHeroInfo(districtObject);
    }

    public void UpdateNatureInfo(DistrictObject districtObject)
    {
        natureDesText.text = 
        "\n<color=#26F39A>风 " + districtObject.eWind + "</color> <color=#E74624>  火 " + districtObject.eFire + "</color> <color=#24CDE7>  水 " + districtObject.eWater + "</color> " +
        "\n<color=#C08342>地 " + districtObject.eGround + "</color> <color=#E0DE60>  光 " + districtObject.eLight + "</color> <color=#DA7CFF>  暗 " + districtObject.eDark + "</color> ";

    }

    public void UpdateCultureInfo(DistrictObject districtObject)
    {
        cultureDesText.text = "人口 " + districtObject.people + "/" + districtObject.peopleLimit + "<color=#76ee00> [英雄 " + districtObject.heroList.Count + "]</color>";

    }

    public void UpdateOutputInfo(DistrictObject districtObject)
    {
        outputDesText.text = "食物 " + (districtObject.rFoodCereal + districtObject.rFoodVegetable + districtObject.rFoodFruit + districtObject.rFoodMeat + districtObject.rFoodFish) + "/" + districtObject.rFoodLimit + " <color=#76ee00>[+50]</color>" +
                        "\n ·谷物 " + districtObject.rFoodCereal + " <color=#76ee00>[+50]</color>" +
                        "\n ·蔬菜 " + districtObject.rFoodVegetable + " <color=#76ee00>[+50]</color>" +
                        "\n ·水果 " + districtObject.rFoodFruit + " <color=#76ee00>[+50]</color>" +
                        "\n ·肉类 " + districtObject.rFoodMeat + " <color=#76ee00>[+50]</color>" +
                        "\n ·鱼类 " + districtObject.rFoodFish + " <color=#76ee00>[+50]</color>\n" +

                        "\n材料 " + (districtObject.rStuffWood + districtObject.rStuffStone + districtObject.rStuffMetal + districtObject.rStuffCloth + districtObject.rStuffTwine + districtObject.rStuffLeather + districtObject.rStuffLeather + districtObject.rStuffBone) + "/" + districtObject.rStuffLimit + " <color=#76ee00> [+50]</color>" +
                        "\n ·木材 " + districtObject.rStuffWood + " <color=#76ee00>[+50]</color>" +
                        "\n ·石料 " + districtObject.rStuffStone + " <color=#76ee00>[+50]</color>" +
                        "\n ·金属 " + districtObject.rStuffMetal + " <color=#76ee00>[+50]</color>" +
                        "\n ·布料 " + districtObject.rStuffCloth + " <color=#76ee00>[+50]</color>" +
                        "\n ·麻绳 " + districtObject.rStuffTwine + " <color=#76ee00>[+50]</color>" +
                        "\n ·皮革 " + districtObject.rStuffLeather + " <color=#76ee00>[+50]</color>" +
                        "\n ·骨块 " + districtObject.rStuffBone + " <color=#76ee00>[+50]</color>" +
                        "\n ·特殊 0 <color=#76ee00>[+0]</color>\n" +

                        "\n 装备 " + (districtObject.rProductWeapon + districtObject.rProductArmor + districtObject.rProductJewelry) + "/" + districtObject.rProductLimit +
                        "\n ·武器 <color=#ffffff> " + districtObject.rProductWeapon + " </color>" +
                        "\n ·防具 <color=#ffffff> " + districtObject.rProductArmor + " </color>" +
                        "\n ·饰物 <color=#ffffff> " + districtObject.rProductJewelry + " </color>";
    }

    public void UpdateBuildingInfo(DistrictObject districtObject)
    {
        buildingText.text = districtObject.buildingList.Count + "个  " ;

        for (int i = 0; i < buildingListGo.transform.childCount; i++)
        {
            Destroy(buildingListGo.transform.GetChild(i).gameObject);
        }
        GameObject go;


        for (int i = 0; i < districtObject.buildingList.Count; i++)
        {
            go = Instantiate(Resources.Load("Prefab/UILabel/Label_BuildingInDistrictMain")) as GameObject;
            go.transform.SetParent(buildingListGo.transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, -4 + i * -22f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/BuildingPic/" + gc.buildingDic[districtObject.buildingList[i]].mainPic);
            go.transform.GetChild(1).GetComponent<Text>().text = gc.buildingDic[districtObject.buildingList[i]].name;
            go.transform.GetComponent<InteractiveLabel>().index = districtObject.buildingList[i];
        }
        buildingListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(231f, 4 + districtObject.buildingList.Count * 22f));

    }


    public void UpdateHeroInfo(DistrictObject districtObject)
    {
        
        heroText.text = districtObject.heroList.Count + "人";

        for (int i = 0; i < heroListGo.transform.childCount; i++)
        {
            Destroy(heroListGo.transform.GetChild(i).gameObject);
        }
        GameObject go;
        for (int i = 0; i < districtObject.heroList.Count; i++)
        {
            go = Instantiate(Resources.Load("Prefab/UILabel/Label_HeroInDis")) as GameObject;
            go.transform.SetParent(heroListGo.transform);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, -4 + i * -36f, 0f);

            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/RolePic/" + gc.heroDic[districtObject.heroList[i]].pic);
            go.transform.GetChild(1).GetComponent<Text>().text = gc.heroDic[districtObject.heroList[i]].name;

            if (gc.heroDic[districtObject.heroList[i]].workerInBuilding == -1)
            {
                go.transform.GetChild(2).GetComponent<Text>().text = "空闲中";
            }
            else
            {
                go.transform.GetChild(2).GetComponent<Text>().text = "工作中<" + gc.buildingDic[gc.heroDic[districtObject.heroList[i]].workerInBuilding].name + ">";
            }
        }
        heroListGo.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(157f, Mathf.Max(160f, 4 + districtObject.heroList.Count * 36f));
    }


    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }
}
