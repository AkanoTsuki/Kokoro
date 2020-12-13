using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayMainPanel : BasePanel
{
    public static PlayMainPanel Instance;
    GameControlInPlay gci;

    public Image top_weatherImage;
    public Image top_seasonImage;
    public Text top_dateText;
    public Text top_hourText;
    public List<RectTransform> top_timeBarRtList;


    public Button left_districtMainBtn;
    public Button left_inventoryMainBtn;
    public Button left_heroMainBtn;
    public Button left_adventureMainBtn;

    void Awake()
    {
        Instance = this;
         
    }

    void Start()
    {
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();
        left_districtMainBtn.onClick.AddListener(delegate () { gci.OpenDistrictMain(); });
        left_inventoryMainBtn.onClick.AddListener(delegate () {  });
        left_heroMainBtn.onClick.AddListener(delegate () { });
        left_adventureMainBtn.onClick.AddListener(delegate () { });
    }
    public override void OnShow()
    {

        SetAnchoredPosition(0, 0);
        //ShowByImmediately(true);
    }

}
