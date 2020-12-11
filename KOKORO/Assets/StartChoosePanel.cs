using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartChoosePanel : BasePanel
{
    public static StartChoosePanel Instance;

    GameControl gc;
    GameControlInNewGame gci;
    public Text desText;
    public List<Button> districtBtnList;

    public List<Button> leaderBtnList;

    public List<Image> picImageList;
    public List<Text> nameTextList;
    public List<Text> typeTextList;
    public List<Button> menberBtnList;
    public Button menberAllBtn;


    public Button confirmBtn;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();

        int index = 0;
        districtBtnList[0].onClick.AddListener(delegate () { gci.SetDistrict(0); UpdateDesInfo(0); });
        districtBtnList[1].onClick.AddListener(delegate () { gci.SetDistrict(1); UpdateDesInfo(1); });
        districtBtnList[2].onClick.AddListener(delegate () { gci.SetDistrict(2); UpdateDesInfo(2); });
        districtBtnList[3].onClick.AddListener(delegate () { gci.SetDistrict(3); UpdateDesInfo(3); });
        districtBtnList[4].onClick.AddListener(delegate () { gci.SetDistrict(4); UpdateDesInfo(4); });
        districtBtnList[5].onClick.AddListener(delegate () { gci.SetDistrict(5); UpdateDesInfo(5); });
        districtBtnList[6].onClick.AddListener(delegate () { gci.SetDistrict(6); UpdateDesInfo(6); });

        leaderBtnList[0].onClick.AddListener(delegate () { gci.SetLeaderHeroType(0); });
        leaderBtnList[1].onClick.AddListener(delegate () { gci.SetLeaderHeroType(1); });
        leaderBtnList[2].onClick.AddListener(delegate () { gci.SetLeaderHeroType(2); });
        leaderBtnList[3].onClick.AddListener(delegate () { gci.SetLeaderHeroType(3); });
        leaderBtnList[4].onClick.AddListener(delegate () { gci.SetLeaderHeroType(4); });
        leaderBtnList[5].onClick.AddListener(delegate () { gci.SetLeaderHeroType(5); });

        for (int i = 0; i < menberBtnList.Count; i++)
        {
            index = i;
            menberBtnList[i].onClick.AddListener(delegate () { gci.RollMenber(index); UpdateMenberInfo(index); });
        }
        menberAllBtn.onClick.AddListener(delegate () { gci.RollMenberAll(); UpdateMenberAllInfo(); });

    }

    public void UpdateDesInfo( int districtID)
    {
        Debug.Log("districtID=" + districtID);
        desText.text = DataManager.mDistrictDict[districtID].Des;
    }

    public void UpdateMenberAllInfo()
    {
        for (int i= 0; i < 5; i++)
        {
            UpdateMenberInfo(i);
        }
    }
    public void UpdateMenberInfo( int index)
    {
        picImageList[index].overrideSprite= Resources.Load("Image/RolePic/" + gci.temp_HeroList[index].pic, typeof(Sprite)) as Sprite;
        nameTextList[index].text = gci.temp_HeroList[index].name;
        typeTextList[index].text = gci.temp_HeroList[index].type.ToString();
    }



}
