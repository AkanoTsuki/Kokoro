using System.Collections;
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

    public Text scrollGrondIDesText;
    public Image scrollGrondIValueImage;
    public Text scrollGrondIValueText;

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

    public Text scrollGrondIIDesText;
    public Image scrollGrondIIValueImage;
    public Text scrollGrondIIValueText;

    public Text scrollLightIIDesText;
    public Image scrollLightIIValueImage;
    public Text scrollLightIIValueText;

    public Text scrollDarkIIDesText;
    public Image scrollDarkIIValueImage;
    public Text scrollDarkIIValueText;

    public Button closeBtn;
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
    
    }
}
