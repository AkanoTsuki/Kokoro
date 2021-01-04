﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SupplyAndDemandPanel : BasePanel
{
    public static SupplyAndDemandPanel Instance;
    GameControl gc;

    public Text titleText;

    public Text weaponSwordDesText;
    public Image weaponSwordValueImage;
    public Text weaponSwordValueText;

    public Text weaponAxeDesText;
    public Image weaponAxeValueImage;
    public Text weaponAxeValueText;

    public Text weaponSpearDesText;
    public Image weaponSpearValueImage;
    public Text weaponSpearValueText;

    public Text weaponHammerDesText;
    public Image weaponHammerValueImage;
    public Text weaponHammerValueText;

    public Text weaponBowDesText;
    public Image weaponBowValueImage;
    public Text weaponBowValueText;

    public Text weaponStaffDesText;
    public Image weaponStaffValueImage;
    public Text weaponStaffValueText;

    public Text subhandShieldDesText;
    public Image subhandShieldValueImage;
    public Text subhandShieldValueText;

    public Text subhandDorlachDesText;
    public Image subhandDorlachValueImage;
    public Text subhandDorlachValueText;

    public Text jewelryNeckDesText;
    public Image jewelryNeckValueImage;
    public Text jewelryNeckValueText;

    public Text jewelryFingerDesText;
    public Image jewelryFingerValueImage;
    public Text jewelryFingerValueText;

    public Text armorHeadHDesText;
    public Image armorHeadHValueImage;
    public Text armorHeadHValueText;

    public Text armorBodyHDesText;
    public Image armorBodyHValueImage;
    public Text armorBodyHValueText;

    public Text armorHandHDesText;
    public Image armorHandHValueImage;
    public Text armorHandHValueText;

    public Text armorBackHDesText;
    public Image armorBackHValueImage;
    public Text armorBackHValueText;

    public Text armorFootHDesText;
    public Image armorFootHValueImage;
    public Text armorFootHValueText;

    public Text armorHeadLDesText;
    public Image armorHeadLValueImage;
    public Text armorHeadLValueText;

    public Text armorBodyLDesText;
    public Image armorBodyLValueImage;
    public Text armorBodyLValueText;

    public Text armorHandLDesText;
    public Image armorHandLValueImage;
    public Text armorHandLValueText;

    public Text armorBackLDesText;
    public Image armorBackLValueImage;
    public Text armorBackLValueText;

    public Text armorFootLDesText;
    public Image armorFootLValueImage;
    public Text armorFootLValueText;

    public Text scrollWindIDesText;
    public Image scrollWindIValueImage;
    public Text scrollWindIValueText;

    public Text scrollFireIDesText;
    public Image scrollFireIValueImage;
    public Text scrollFireIValueText;

    public Text scrollWaterIDesText;
    public Image scrollWaterIValueImage;
    public Text scrollWaterIValueText;

    public Text scrollGroundIDesText;
    public Image scrollGroundIValueImage;
    public Text scrollGroundIValueText;

    public Text scrollLightIDesText;
    public Image scrollLightIValueImage;
    public Text scrollLightIValueText;

    public Text scrollDarkIDesText;
    public Image scrollDarkIValueImage;
    public Text scrollDarkIValueText;

    public Text scrollNoneDesText;
    public Image scrollNoneValueImage;
    public Text scrollNoneValueText;

    public Text scrollWindIIDesText;
    public Image scrollWindIIValueImage;
    public Text scrollWindIIValueText;

    public Text scrollFireIIDesText;
    public Image scrollFireIIValueImage;
    public Text scrollFireIIValueText;

    public Text scrollWaterIIDesText;
    public Image scrollWaterIIValueImage;
    public Text scrollWaterIIValueText;

    public Text scrollGroundIIDesText;
    public Image scrollGroundIIValueImage;
    public Text scrollGroundIIValueText;

    public Text scrollLightIIDesText;
    public Image scrollLightIIValueImage;
    public Text scrollLightIIValueText;

    public Text scrollDarkIIDesText;
    public Image scrollDarkIIValueImage;
    public Text scrollDarkIIValueText;

    public Button closeBtn;

    Color cGreen = new Color(72 / 255f, 161 / 255f, 79 / 255f, 188 / 255f);
    Color cRed = new Color(193 / 255f, 56 / 255f, 30 / 255f, 188 / 255f);

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }
    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    public void OnShow(short districtID, int x, int y)
    {

        UpdateAllInfo(districtID);
        SetAnchoredPosition(x, y);
        isShow = true;
    }
    public override void OnHide()
    {
        SetAnchoredPosition(0, 5000);
        isShow = false;
    }
    public void UpdateAllInfo(short districtID)
    {
        UpdateSingle(districtID, ItemTypeSmall.Sword);
        UpdateSingle(districtID, ItemTypeSmall.Axe);
        UpdateSingle(districtID, ItemTypeSmall.Spear);
        UpdateSingle(districtID, ItemTypeSmall.Hammer);
        UpdateSingle(districtID, ItemTypeSmall.Bow);
        UpdateSingle(districtID, ItemTypeSmall.Staff);
        UpdateSingle(districtID, ItemTypeSmall.Shield);
        UpdateSingle(districtID, ItemTypeSmall.Dorlach);
        UpdateSingle(districtID, ItemTypeSmall.Neck);
        UpdateSingle(districtID, ItemTypeSmall.Finger);
        UpdateSingle(districtID, ItemTypeSmall.HeadH);
        UpdateSingle(districtID, ItemTypeSmall.BodyH);
        UpdateSingle(districtID, ItemTypeSmall.HandH);
        UpdateSingle(districtID, ItemTypeSmall.BackH);
        UpdateSingle(districtID, ItemTypeSmall.FootH);
        UpdateSingle(districtID, ItemTypeSmall.HeadL);
        UpdateSingle(districtID, ItemTypeSmall.BodyL);
        UpdateSingle(districtID, ItemTypeSmall.HandL);
        UpdateSingle(districtID, ItemTypeSmall.BackL);
        UpdateSingle(districtID, ItemTypeSmall.FootL);

        UpdateSingle(districtID, new List<int> { 1});
        UpdateSingle(districtID, new List<int> { 2 });
        UpdateSingle(districtID, new List<int> { 3 });
        UpdateSingle(districtID, new List<int> { 4 });
        UpdateSingle(districtID, new List<int> {5 });
        UpdateSingle(districtID, new List<int> { 6 });
        UpdateSingle(districtID, new List<int> { 0 });
        UpdateSingle(districtID, new List<int> { 1,5 });
        UpdateSingle(districtID, new List<int> { 2 ,4});
        UpdateSingle(districtID, new List<int> { 3 ,6});
        UpdateSingle(districtID, new List<int> { 4 ,3});
        UpdateSingle(districtID, new List<int> { 5 ,1});
        UpdateSingle(districtID, new List<int> { 6 ,2});
    }


    void UpdateSingle(short districtID,ItemTypeSmall  itemTypeSmall)
    {
        string str = "";
        short value = 0;
        byte rank;
        int have= getHave(districtID, itemTypeSmall); ;
        int num = 0;


        string yearMonth = gc.timeYear + "/" + gc.timeMonth;
        switch (itemTypeSmall)
        {
            case ItemTypeSmall.Sword: value = gc.supplyAndDemand.weaponSwordValue[districtID]; rank = gc.supplyAndDemand.weaponSwordRank[districtID]; num = gc.salesRecordDic[yearMonth].weaponSwordNum[districtID]; break;
            case ItemTypeSmall.Axe: value = gc.supplyAndDemand.weaponAxeValue[districtID]; rank = gc.supplyAndDemand.weaponAxeRank[districtID]; num = gc.salesRecordDic[yearMonth].weaponAxeNum[districtID]; break;
            case ItemTypeSmall.Spear: value = gc.supplyAndDemand.weaponSpearValue[districtID]; rank = gc.supplyAndDemand.weaponSpearRank[districtID]; num = gc.salesRecordDic[yearMonth].weaponSpearNum[districtID]; break;
            case ItemTypeSmall.Hammer: value = gc.supplyAndDemand.weaponHammerValue[districtID]; rank = gc.supplyAndDemand.weaponHammerRank[districtID]; num = gc.salesRecordDic[yearMonth].weaponHammerNum[districtID]; break;
            case ItemTypeSmall.Bow: value = gc.supplyAndDemand.weaponBowValue[districtID]; rank = gc.supplyAndDemand.weaponBowRank[districtID]; num = gc.salesRecordDic[yearMonth].weaponBowNum[districtID]; break;
            case ItemTypeSmall.Staff: value = gc.supplyAndDemand.weaponStaffValue[districtID]; rank = gc.supplyAndDemand.weaponStaffRank[districtID]; num = gc.salesRecordDic[yearMonth].weaponStaffNum[districtID]; break;
            case ItemTypeSmall.Shield: value = gc.supplyAndDemand.subhandShieldValue[districtID]; rank = gc.supplyAndDemand.subhandShieldRank[districtID]; num = gc.salesRecordDic[yearMonth].subhandShieldNum[districtID]; break;
            case ItemTypeSmall.Dorlach: value = gc.supplyAndDemand.subhandDorlachValue[districtID]; rank = gc.supplyAndDemand.subhandDorlachRank[districtID]; num = gc.salesRecordDic[yearMonth].subhandDorlachNum[districtID]; break;
            case ItemTypeSmall.Neck: value = gc.supplyAndDemand.jewelryNeckValue[districtID]; rank = gc.supplyAndDemand.jewelryNeckRank[districtID]; num = gc.salesRecordDic[yearMonth].jewelryNeckNum[districtID]; break;
            case ItemTypeSmall.Finger: value = gc.supplyAndDemand.jewelryFingerValue[districtID]; rank = gc.supplyAndDemand.jewelryFingerRank[districtID]; num = gc.salesRecordDic[yearMonth].jewelryFingerNum[districtID]; break;
            case ItemTypeSmall.HeadH: value = gc.supplyAndDemand.armorHeadHValue[districtID]; rank = gc.supplyAndDemand.armorHeadHRank[districtID]; num = gc.salesRecordDic[yearMonth].armorHeadHNum[districtID]; break;
            case ItemTypeSmall.BodyH: value = gc.supplyAndDemand.armorBodyHValue[districtID]; rank = gc.supplyAndDemand.armorBodyHRank[districtID]; num = gc.salesRecordDic[yearMonth].armorBodyHNum[districtID]; break;
            case ItemTypeSmall.HandH: value = gc.supplyAndDemand.armorHandHValue[districtID]; rank = gc.supplyAndDemand.armorHandHRank[districtID]; num = gc.salesRecordDic[yearMonth].armorHandHNum[districtID]; break;
            case ItemTypeSmall.BackH: value = gc.supplyAndDemand.armorBackHValue[districtID]; rank = gc.supplyAndDemand.armorBackHRank[districtID]; num = gc.salesRecordDic[yearMonth].armorBackHNum[districtID]; break;
            case ItemTypeSmall.FootH: value = gc.supplyAndDemand.armorFootHValue[districtID]; rank = gc.supplyAndDemand.armorFootHRank[districtID]; num = gc.salesRecordDic[yearMonth].armorFootHNum[districtID]; break;
            case ItemTypeSmall.HeadL: value = gc.supplyAndDemand.armorHeadLValue[districtID]; rank = gc.supplyAndDemand.armorHeadLRank[districtID]; num = gc.salesRecordDic[yearMonth].armorHeadLNum[districtID]; break;
            case ItemTypeSmall.BodyL: value = gc.supplyAndDemand.armorBodyLValue[districtID]; rank = gc.supplyAndDemand.armorBodyLRank[districtID]; num = gc.salesRecordDic[yearMonth].armorBodyLNum[districtID]; break;
            case ItemTypeSmall.HandL: value = gc.supplyAndDemand.armorHandLValue[districtID]; rank = gc.supplyAndDemand.armorHandLRank[districtID]; num = gc.salesRecordDic[yearMonth].armorHandLNum[districtID]; break;
            case ItemTypeSmall.BackL: value = gc.supplyAndDemand.armorBackLValue[districtID]; rank = gc.supplyAndDemand.armorBackLRank[districtID]; num = gc.salesRecordDic[yearMonth].armorBackLNum[districtID]; break;
            case ItemTypeSmall.FootL: value = gc.supplyAndDemand.armorFootLValue[districtID]; rank = gc.supplyAndDemand.armorFootLRank[districtID]; num = gc.salesRecordDic[yearMonth].armorFootLNum[districtID]; break;
            default:
                rank = 0; break;
        }

        switch (rank)
        {
            case 0: str += "倾向 "; break;
            case 1: str += "倾向 一般"; break;
            case 2: str += "倾向 良好"; break;
            case 3: str += "倾向 高档"; break;
        }
        str += "\n销量" + num+"\n库存"+ have;

        switch (itemTypeSmall)
        {
            case ItemTypeSmall.Sword:
                weaponSwordDesText.text = str;
                weaponSwordValueImage.color = value > 0 ? cGreen : cRed;
                weaponSwordValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                weaponSwordValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                weaponSwordValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.Axe:
                weaponAxeDesText.text = str;
                weaponAxeValueImage.color = value > 0 ? cGreen : cRed;
                weaponAxeValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                weaponAxeValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                weaponAxeValueText.text = (value > 0 ? "+" : "") + value; 
                break;
            case ItemTypeSmall.Spear: 
                weaponSpearDesText.text = str;
                weaponSpearValueImage.color = value > 0 ? cGreen : cRed;
                weaponSpearValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                weaponSpearValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                weaponSpearValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.Hammer:
                weaponHammerDesText.text = str;
                weaponHammerValueImage.color = value > 0 ? cGreen : cRed;
                weaponHammerValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                weaponHammerValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                weaponHammerValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.Bow:
                weaponBowDesText.text = str;
                weaponBowValueImage.color = value > 0 ? cGreen : cRed;
                weaponBowValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                weaponBowValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                weaponBowValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.Staff:
                weaponStaffDesText.text = str;
                weaponStaffValueImage.color = value > 0 ? cGreen : cRed;
                weaponStaffValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                weaponStaffValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                weaponStaffValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.Shield:
                subhandShieldDesText.text = str;
                subhandShieldValueImage.color = value > 0 ? cGreen : cRed;
                subhandShieldValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                subhandShieldValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                subhandShieldValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.Dorlach:
                subhandDorlachDesText.text = str;
                subhandDorlachValueImage.color = value > 0 ? cGreen : cRed;
                subhandDorlachValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                subhandDorlachValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                subhandDorlachValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.Neck:
                jewelryNeckDesText.text = str;
                jewelryNeckValueImage.color = value > 0 ? cGreen : cRed;
                jewelryNeckValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                jewelryNeckValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                jewelryNeckValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.Finger:
                jewelryFingerDesText.text = str;
                jewelryFingerValueImage.color = value > 0 ? cGreen : cRed;
                jewelryFingerValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                jewelryFingerValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                jewelryFingerValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.HeadH:
                armorHeadHDesText.text = str;
                armorHeadHValueImage.color = value > 0 ? cGreen : cRed;
                armorHeadHValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorHeadHValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorHeadHValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.BodyH:
                armorBodyHDesText.text = str;
                armorBodyHValueImage.color = value > 0 ? cGreen : cRed;
                armorBodyHValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorBodyHValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorBodyHValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.HandH:
                armorHandHDesText.text = str;
                armorHandHValueImage.color = value > 0 ? cGreen : cRed;
                armorHandHValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorHandHValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorHandHValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.BackH:
                armorBackHDesText.text = str;
                armorBackHValueImage.color = value > 0 ? cGreen : cRed;
                armorBackHValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorBackHValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorBackHValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.FootH:
                armorFootHDesText.text = str;
                armorFootHValueImage.color = value > 0 ? cGreen : cRed;
                armorFootHValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorFootHValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorFootHValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.HeadL:
                armorHeadLDesText.text = str;
                armorHeadLValueImage.color = value > 0 ? cGreen : cRed;
                armorHeadLValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorHeadLValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorHeadLValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.BodyL:
                armorBodyLDesText.text = str;
                armorBodyLValueImage.color = value > 0 ? cGreen : cRed;
                armorBodyLValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorBodyLValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorBodyLValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.HandL:
                armorHandLDesText.text = str;
                armorHandLValueImage.color = value > 0 ? cGreen : cRed;
                armorHandLValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorHandLValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorHandLValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.BackL:
                armorBackLDesText.text = str;
                armorBackLValueImage.color = value > 0 ? cGreen : cRed;
                armorBackLValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorBackLValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorBackLValueText.text = (value > 0 ? "+" : "") + value;
                break;
            case ItemTypeSmall.FootL:
                armorFootLDesText.text = str;
                armorFootLValueImage.color = value > 0 ? cGreen : cRed;
                armorFootLValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
                armorFootLValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
                armorFootLValueText.text = (value > 0 ? "+" : "") + value;
                break;
            default: break;
        }

    }

    void UpdateSingle(short districtID,List<int> itemTypeSmall)
    {
        string str = "";
        short value = 0;
        byte rank=0;
        int have = getHave(districtID, itemTypeSmall); ;
        int num = 0;


        string yearMonth = gc.timeYear + "/" + gc.timeMonth;

        if (itemTypeSmall.Count==1&& itemTypeSmall.Contains(1))
        {
            value = gc.supplyAndDemand.scrollWindIValue[districtID]; 
            rank = gc.supplyAndDemand.scrollWindIRank[districtID]; 
            num = gc.salesRecordDic[yearMonth].scrollWindINum[districtID];
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(2))
        {
            value = gc.supplyAndDemand.scrollFireIValue[districtID];
            rank = gc.supplyAndDemand.scrollFireIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollFireINum[districtID];
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(3))
        {
            value = gc.supplyAndDemand.scrollWaterIValue[districtID];
            rank = gc.supplyAndDemand.scrollWaterIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollWaterINum[districtID];
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(4))
        {
            value = gc.supplyAndDemand.scrollGroundIValue[districtID];
            rank = gc.supplyAndDemand.scrollGroundIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollGroundINum[districtID];
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(5))
        {
            value = gc.supplyAndDemand.scrollLightIValue[districtID];
            rank = gc.supplyAndDemand.scrollLightIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollLightINum[districtID];
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(6))
        {
            value = gc.supplyAndDemand.scrollDarkIValue[districtID];
            rank = gc.supplyAndDemand.scrollDarkIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollDarkINum[districtID];
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(0))
        {
            value = gc.supplyAndDemand.scrollNoneValue[districtID];
            rank = gc.supplyAndDemand.scrollNoneRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollNoneNum[districtID];
        }
        else if (itemTypeSmall.Contains(5) && itemTypeSmall.Contains(1))
        {
            value = gc.supplyAndDemand.scrollWindIIValue[districtID];
            rank = gc.supplyAndDemand.scrollWindIIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollWindIINum[districtID];
        }
        else if (itemTypeSmall.Contains(4) && itemTypeSmall.Contains(2))
        {
            value = gc.supplyAndDemand.scrollFireIIValue[districtID];
            rank = gc.supplyAndDemand.scrollFireIIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollFireIINum[districtID];
        }
        else if (itemTypeSmall.Contains(5) && itemTypeSmall.Contains(3))
        {
            value = gc.supplyAndDemand.scrollWaterIIValue[districtID];
            rank = gc.supplyAndDemand.scrollWaterIIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollWaterIINum[districtID];
        }
        else if (itemTypeSmall.Contains(3) && itemTypeSmall.Contains(4))
        {
            value = gc.supplyAndDemand.scrollGroundIIValue[districtID];
            rank = gc.supplyAndDemand.scrollGroundIIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollGroundIINum[districtID];
        }
        else if (itemTypeSmall.Contains(1) && itemTypeSmall.Contains(5))
        {
            value = gc.supplyAndDemand.scrollLightIIValue[districtID];
            rank = gc.supplyAndDemand.scrollLightIIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollLightIINum[districtID];
        }
        else if (itemTypeSmall.Contains(2) && itemTypeSmall.Contains(6))
        {
            value = gc.supplyAndDemand.scrollDarkIIValue[districtID];
            rank = gc.supplyAndDemand.scrollDarkIIRank[districtID];
            num = gc.salesRecordDic[yearMonth].scrollDarkIINum[districtID];
        }

        switch (rank)
        {
            case 0: str += "倾向 "; break;
            case 1: str += "倾向 一般"; break;
            case 2: str += "倾向 良好"; break;
            case 3: str += "倾向 高档"; break;
        }
        str += "\n销量" + num + "\n库存" + have;


        if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(1))
        {
            scrollWindIDesText.text = str;
            scrollWindIValueImage.color = value > 0 ? cGreen : cRed;
            scrollWindIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollWindIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollWindIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(2))
        {
            scrollFireIDesText.text = str;
            scrollFireIValueImage.color = value > 0 ? cGreen : cRed;
            scrollFireIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollFireIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollFireIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(3))
        {
            scrollWaterIDesText.text = str;
            scrollWaterIValueImage.color = value > 0 ? cGreen : cRed;
            scrollWaterIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollWaterIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollWaterIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(4))
        {
            scrollGroundIDesText.text = str;
            scrollGroundIValueImage.color = value > 0 ? cGreen : cRed;
            scrollGroundIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollGroundIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollGroundIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(5))
        {
            scrollLightIDesText.text = str;
            scrollLightIValueImage.color = value > 0 ? cGreen : cRed;
            scrollLightIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollLightIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollLightIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(6))
        {
            scrollDarkIDesText.text = str;
            scrollDarkIValueImage.color = value > 0 ? cGreen : cRed;
            scrollDarkIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollDarkIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollDarkIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Count == 1 && itemTypeSmall.Contains(0))
        {
            scrollNoneDesText.text = str;
            scrollNoneValueImage.color = value > 0 ? cGreen : cRed;
            scrollNoneValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollNoneValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollNoneValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Contains(5) && itemTypeSmall.Contains(1))
        {
            scrollWindIIDesText.text = str;
            scrollWindIIValueImage.color = value > 0 ? cGreen : cRed;
            scrollWindIIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollWindIIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollWindIIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Contains(4) && itemTypeSmall.Contains(2))
        {
            scrollFireIIDesText.text = str;
            scrollFireIIValueImage.color = value > 0 ? cGreen : cRed;
            scrollFireIIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollFireIIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollFireIIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Contains(5) && itemTypeSmall.Contains(3))
        {
            scrollWaterIIDesText.text = str;
            scrollWaterIIValueImage.color = value > 0 ? cGreen : cRed;
            scrollWaterIIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollWaterIIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollWaterIIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Contains(3) && itemTypeSmall.Contains(4))
        {
            scrollGroundIIDesText.text = str;
            scrollGroundIIValueImage.color = value > 0 ? cGreen : cRed;
            scrollGroundIIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollGroundIIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollGroundIIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Contains(1) && itemTypeSmall.Contains(5))
        {
            scrollLightIIDesText.text = str;
            scrollLightIIValueImage.color = value > 0 ? cGreen : cRed;
            scrollLightIIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollLightIIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollLightIIValueText.text = (value > 0 ? "+" : "") + value;
        }
        else if (itemTypeSmall.Contains(2) && itemTypeSmall.Contains(6))
        {
            scrollDarkIIDesText.text = str;
            scrollDarkIIValueImage.color = value > 0 ? cGreen : cRed;
            scrollDarkIIValueImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, (value > 0 ? 0.0f : 180.0f), 0.0f);
            scrollDarkIIValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(System.Math.Abs(value) / 100f * 32, 16f);
            scrollDarkIIValueText.text = (value > 0 ? "+" : "") + value;
        }

    }

    int getHave(short districtID, ItemTypeSmall itemTypeSmall)
    {
        int have =0;
        foreach (KeyValuePair<int, ItemObject> kvp in gc.itemDic)
        {
            if (kvp.Value.districtID == districtID && kvp.Value.heroID == -1 && kvp.Value.isGoods == true && DataManager.mItemDict[kvp.Value.prototypeID].TypeSmall == itemTypeSmall)
            {
                have++;
            }
        }
        return have;
    }

    int getHave(short districtID, List<int> skillTypeSmall)
    {
        int have = 0;
        foreach (KeyValuePair<int, SkillObject> kvp in gc.skillDic)
        {
            if (kvp.Value.districtID == districtID && kvp.Value.heroID == -1 && kvp.Value.isGoods == true )
            {
                bool fh = false;
                if (DataManager.mSkillDict[kvp.Value.prototypeID].Element.Count == skillTypeSmall.Count)
                {
                    for (byte i = 0; i < DataManager.mSkillDict[kvp.Value.prototypeID].Element.Count; i++)
                    {
                        if (DataManager.mSkillDict[kvp.Value.prototypeID].Element[i] != skillTypeSmall[i])
                        {
                            fh = false; break;
                        }
                    }
                }

                if (fh)
                {
                    have++;
                }
              
            }
        }
        return have;
    }
}
