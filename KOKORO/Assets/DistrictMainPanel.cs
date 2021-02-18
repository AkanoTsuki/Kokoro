using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DistrictMainPanel : BasePanel
{
    public static DistrictMainPanel Instance;

    GameControl gc;

    
    public Text desText;
    public Text contentText;
    public Image hpImage;

    public Text peopleText;

    public Text fiscal0Text;
    public Text fiscal1Text;


    public GameObject policyBlockGo;
    public Slider policyBlock_rationCerealSlider;
    public Slider policyBlock_rationVegetableSlider;
    public Slider policyBlock_rationMeatSlider;
    public Slider policyBlock_rationFishSlider;
    public Slider policyBlock_rationFruitSlider;
    public Slider policyBlock_rationBeerSlider;
    public Slider policyBlock_rationWineSlider;
    public Slider policyBlock_taxPeopleSlider;
    public Slider policyBlock_taxPassSlider;
    public Slider policyBlock_taxGoodsSlider;

    public Text policyBlock_taxPeopleText;
    public Text policyBlock_taxPassText;
    public Text policyBlock_taxGoodsText;

    public GameObject policyReadOnlyBlockGo;
    public Text policyReadOnlyBlock_taxPeopleText;
    public Text policyReadOnlyBlock_taxPassText;
    public Text policyReadOnlyBlock_taxGoodsText;

    public Button closeBtn;



    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    // Start is called before the first frame update
    void Start()
    {

        policyBlock_rationCerealSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetRation(gc.nowCheckingDistrictID, StuffType.Cereal,(byte)value);
        });
        policyBlock_rationVegetableSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetRation(gc.nowCheckingDistrictID, StuffType.Vegetable, (byte)value);
        });
        policyBlock_rationMeatSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetRation(gc.nowCheckingDistrictID, StuffType.Meat, (byte)value);
        });
        policyBlock_rationFishSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetRation(gc.nowCheckingDistrictID, StuffType.Fish, (byte)value);
        });
        policyBlock_rationFruitSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetRation(gc.nowCheckingDistrictID, StuffType.Fruit, (byte)value);
        });
        policyBlock_rationBeerSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetRation(gc.nowCheckingDistrictID, StuffType.Beer, (byte)value);
        });
        policyBlock_rationWineSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetRation(gc.nowCheckingDistrictID, StuffType.Wine, (byte)value);
        });

        policyBlock_taxPeopleSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetTaxPeople(gc.nowCheckingDistrictID, (byte)value);
            UpdatePolicyBlockTaxValue(gc.districtDic[gc.nowCheckingDistrictID]);
        });
        policyBlock_taxPassSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetTaxPass(gc.nowCheckingDistrictID, (byte)value);
            UpdatePolicyBlockTaxValue(gc.districtDic[gc.nowCheckingDistrictID]);
        });
        policyBlock_taxGoodsSlider.onValueChanged.AddListener((value) =>
        {
            gc.DistrictSetTaxGoods(gc.nowCheckingDistrictID, (byte)value);
            UpdatePolicyBlockTaxValue(gc.districtDic[gc.nowCheckingDistrictID]);
        });
        //GameControlInPlay gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();
        //buildBtn.onClick.AddListener(delegate () { gci.OpenBuild(); });
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public void OnShow(DistrictObject districtObject)
    {

        UpdateAllInfo(gc, districtObject);

        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        //ShowByImmediately(true);

        if (BuildingPanel.Instance.isShow)
        {
            BuildingPanel.Instance.OnHide();
        }
        if (BuildPanel.Instance.isShow)
        {
            BuildPanel.Instance.OnHide();
        }

        isShow = true;
    }

    public void UpdateAllInfo(GameControl gc, DistrictObject districtObject)
    {
        desText.text = districtObject.des;

        UpdateBasicInfo(districtObject);
        UpdatePeopleInfo(districtObject);
        UpdateFiscal0Info(districtObject);
        UpdateFiscal1Info(districtObject);
        //UpdateHeroInfo(districtObject);
        if (districtObject.force == 0)
        {
            ShowPolicyBlock(districtObject);
            HidePolicyReadOnlyBlock();
        }
        else
        {
            HidePolicyBlock();
            ShowPolicyReadOnlyBlock(districtObject);
        }


    }

    public void UpdateBasicInfo(DistrictObject districtObject)
    {
        //natureDesText.text = 
        //"\n<color=#26F39A>风 " + districtObject.eWind + "</color> <color=#E74624>  火 " + districtObject.eFire + "</color> <color=#24CDE7>  水 " + districtObject.eWater + "</color> " +
        //"\n<color=#C08342>地 " + districtObject.eGround + "</color> <color=#E0DE60>  光 " + districtObject.eLight + "</color> <color=#DA7CFF>  暗 " + districtObject.eDark + "</color> ";
      
        contentText.text = "<color=#ECC74F>" + gc.OutputSignStr("★", districtObject.level) + gc.OutputSignStr("☆", DataManager.mDistrictDict[districtObject.id].MaxLevel - districtObject.level) + "</color>\n领主 " + gc.forceDic[districtObject.force].leader + "\n耐久值 " + districtObject.hpNow + "/" + districtObject.hp + "\n城防 " + districtObject.def + "\n\n建筑 " + districtObject.buildingList.Count +
            "\n\n繁荣 " + districtObject.prosperous + "\n安全 " + districtObject.security;
        hpImage.fillAmount = (float)districtObject.hpNow / districtObject.hp;

    }

    public void UpdatePeopleInfo(DistrictObject districtObject)
    {
        // cultureDesText.text = "人口 " + districtObject.people + "/" + districtObject.peopleLimit + "<color=#76ee00> [英雄 " + districtObject.heroList.Count + "]</color>";
        peopleText.text = "居民 " + districtObject.people + "/" + districtObject.peopleLimit + "<color=#76ee00> [英雄 " + districtObject.heroList.Count + "]</color>"+
            "\n工作 " + districtObject.worker+ "\n空闲 " + (districtObject.people- districtObject.worker) + "\n满意度 " + districtObject.satisfaction;
    }

    public void UpdateFiscal0Info(DistrictObject districtObject)
    {
        int totalIncome = districtObject.fiscals[0].incomeTaxPeople + districtObject.fiscals[0].incomeTaxPass + districtObject.fiscals[0].incomeTaxGoods + districtObject.fiscals[0].incomeLogistics + districtObject.fiscals[0].incomeOther;
        int totalExpend = districtObject.fiscals[0].expendMaintenance + districtObject.fiscals[0].expendOther;
        fiscal0Text.text = "上月\n 居 民 税 <color=#62FF4C>" + districtObject.fiscals[0].incomeTaxPeople +
            "</color>\n 通 行 税 <color=#62FF4C>" + districtObject.fiscals[0].incomeTaxPass +
            "</color>\n 交 易 税 <color=#62FF4C>" + districtObject.fiscals[0].incomeTaxGoods +
                "</color>\n 后勤服务 <color=#62FF4C>" + districtObject.fiscals[0].incomeLogistics +
            "</color>\n 其它收入 <color=#62FF4C>" + districtObject.fiscals[0].incomeOther +
            "</color>\n 总 收 入 <color=#62FF4C>" + totalIncome +
             "</color>\n 维 护 费 <color=#FF634C>" + districtObject.fiscals[0].expendMaintenance +
              "</color>\n 其它支出 <color=#FF634C>" + districtObject.fiscals[0].expendOther +
              "</color>\n 总 支 出 <color=#FF634C>" + totalExpend +
                "</color>\n\n 结    算 <color=#" + ((totalIncome - totalExpend)<0? "FF634C>" : "62FF4C>" )+ (totalIncome - totalExpend)+ "</color>";
    }
    public void UpdateFiscal1Info(DistrictObject districtObject)
    {
        int totalIncome = districtObject.fiscals[1].incomeTaxPeople + districtObject.fiscals[1].incomeTaxPass + districtObject.fiscals[1].incomeTaxGoods + districtObject.fiscals[1].incomeLogistics + districtObject.fiscals[1].incomeOther;
        int totalExpend = districtObject.fiscals[1].expendMaintenance + districtObject.fiscals[1].expendOther;
        fiscal1Text.text = "本月(当前)\n 居 民 税 <color=#62FF4C>" + districtObject.fiscals[1].incomeTaxPeople +
            "</color>\n 通 行 税 <color=#62FF4C>" + districtObject.fiscals[1].incomeTaxPass +
            "</color>\n 交 易 税 <color=#62FF4C>" + districtObject.fiscals[1].incomeTaxGoods +
                "</color>\n 后勤服务 <color=#62FF4C>" + districtObject.fiscals[1].incomeLogistics +
            "</color>\n 其它收入 <color=#62FF4C>" + districtObject.fiscals[1].incomeOther +
            "</color>\n 总 收 入 <color=#62FF4C>" + totalIncome +
             "</color>\n 维 护 费 <color=#FF634C>" + districtObject.fiscals[1].expendMaintenance +
              "</color>\n 其它支出 <color=#FF634C>" + districtObject.fiscals[1].expendOther +
              "</color>\n 总 支 出 <color=#FF634C>" + totalExpend +
              "</color>\n\n 结    算 <color=#" + ((totalIncome - totalExpend) < 0 ? "FF634C>" : "62FF4C>") + (totalIncome - totalExpend) + "</color>";
    }

    public void ShowPolicyBlock(DistrictObject districtObject)
    {
        policyBlockGo.SetActive(true);
        UpdatePolicyBlock(districtObject);
        UpdatePolicyBlockTaxValue(districtObject);
    }

    public void UpdatePolicyBlock(DistrictObject districtObject)
    {

        policyBlock_rationCerealSlider.value = districtObject.rationCereal / 50;
        policyBlock_rationVegetableSlider.value = districtObject.rationVegetable / 50;
        policyBlock_rationMeatSlider.value = districtObject.rationMeat / 50;
        policyBlock_rationFishSlider.value = districtObject.rationFish / 50;
        policyBlock_rationFruitSlider.value = districtObject.rationFruit / 50;
        policyBlock_rationBeerSlider.value = districtObject.rationBeer / 50;
        policyBlock_rationWineSlider.value = districtObject.rationWine / 50;

        policyBlock_taxPeopleSlider.value = districtObject.taxPeople / 10;
        policyBlock_taxPassSlider.value = districtObject.taxPass / 10;
        policyBlock_taxGoodsSlider.value = districtObject.taxGoods / 10;
    }

    public void UpdatePolicyBlockTaxValue(DistrictObject districtObject)
    {
        policyBlock_taxPeopleText.text = districtObject.taxPeople + "%";
        policyBlock_taxPassText.text = districtObject.taxPass + "%";
        policyBlock_taxGoodsText.text = districtObject.taxGoods + "%";
    }

    public void HidePolicyBlock()
    {
        policyBlockGo.SetActive(false);
    }

    public void ShowPolicyReadOnlyBlock(DistrictObject districtObject)
    {
        policyReadOnlyBlockGo.SetActive(true);
        UpdatePolicyReadOnlyBlock(districtObject);
    }
    public void UpdatePolicyReadOnlyBlock(DistrictObject districtObject)
    {


        policyReadOnlyBlock_taxPeopleText.text = districtObject.taxPeople+"%";
        policyReadOnlyBlock_taxPassText.text = districtObject.taxPass + "%";
        policyReadOnlyBlock_taxGoodsText.text = districtObject.taxGoods + "%";
    }
    public void HidePolicyReadOnlyBlock()
    {
        policyReadOnlyBlockGo.SetActive(false);
    }


    public override void OnHide()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isShow = false;
    }
}
