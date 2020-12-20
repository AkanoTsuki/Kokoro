using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartChoosePanel : BasePanel
{
    public static StartChoosePanel Instance;

    GameControl gc;
    GameControlInNewGame gci;

    public RectTransform bigMapRt;
    public RectTransform bigMapDesRt;
    public Text bigMapDesText;

    public Text desText;
    public List<Button> districtBtnList;

    public InputField leaderNameIf;
    public List<Button> leaderBtnList;
    public Button leaderManBtn;
    public Button leaderWomanBtn;
    public Image leaderManImage;
    public Image leaderWomanImage;
    public List<Image> leaderTypeImageList;
    public List<Text> leaderTypeTextList;


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

    
        districtBtnList[0].onClick.AddListener(delegate () { gci.SetDistrict(0);  });
        districtBtnList[1].onClick.AddListener(delegate () { gci.SetDistrict(1);  });
        districtBtnList[2].onClick.AddListener(delegate () { gci.SetDistrict(2); });
        districtBtnList[3].onClick.AddListener(delegate () { gci.SetDistrict(3);  });
        districtBtnList[4].onClick.AddListener(delegate () { gci.SetDistrict(4);  });
        districtBtnList[5].onClick.AddListener(delegate () { gci.SetDistrict(5);  });
        districtBtnList[6].onClick.AddListener(delegate () { gci.SetDistrict(6); });

        leaderBtnList[0].onClick.AddListener(delegate () { gci.SetLeaderHeroType(0); });
        leaderBtnList[1].onClick.AddListener(delegate () { gci.SetLeaderHeroType(1); });
        leaderBtnList[2].onClick.AddListener(delegate () { gci.SetLeaderHeroType(2); });
        leaderBtnList[3].onClick.AddListener(delegate () { gci.SetLeaderHeroType(3); });
        leaderBtnList[4].onClick.AddListener(delegate () { gci.SetLeaderHeroType(4); });
        leaderBtnList[5].onClick.AddListener(delegate () { gci.SetLeaderHeroType(5); });

        leaderManBtn.onClick.AddListener(delegate () { gci.SetLeaderHeroSex(0); });
        leaderWomanBtn.onClick.AddListener(delegate () { gci.SetLeaderHeroSex(1); });

        menberBtnList[0].onClick.AddListener(delegate () { gci.RollMenber(0);  });
        menberBtnList[1].onClick.AddListener(delegate () { gci.RollMenber(1);});
        menberBtnList[2].onClick.AddListener(delegate () { gci.RollMenber(2);  });
        menberBtnList[3].onClick.AddListener(delegate () { gci.RollMenber(3); });
        menberBtnList[4].onClick.AddListener(delegate () { gci.RollMenber(4);  });


        menberAllBtn.onClick.AddListener(delegate () { gci.RollMenberAll();  });

        confirmBtn.onClick.AddListener(delegate () { gci.ConfirmAndStart(); });

    }

    public void UpdateDistrictInfo(int districtID)
    {
        //Debug.Log("districtID=" + districtID);
        desText.text = DataManager.mDistrictDict[districtID].Des;

        bigMapRt.anchoredPosition = new Vector2(DataManager.mDistrictDict[districtID].BigMapX, DataManager.mDistrictDict[districtID].BigMapY);
        bigMapDesRt.anchoredPosition = new Vector2(DataManager.mDistrictDict[districtID].BigMapDesX, DataManager.mDistrictDict[districtID].BigMapDesY);
        bigMapDesText.text = DataManager.mDistrictDict[districtID].Name +
            "\n<i><color=#73FB8F>草地</color>" + DataManager.mDistrictDict[districtID].Grass[0] + "   <color=#FBE757>□地块</color>" + DataManager.mDistrictDict[districtID].StartGrid[0]+
            "\n<color=#098522>林地</color> " + DataManager.mDistrictDict[districtID].Wood[0] +
            "\n<color=#39BAFF>水域</color> " + DataManager.mDistrictDict[districtID].Water[0] +
            "\n<color=#A2A2A2>石头矿藏</color> " + DataManager.mDistrictDict[districtID].Stone[0] +
            "\n<color=#B7611D>金属矿藏</color> " + DataManager.mDistrictDict[districtID].Metal[0] + "</i>";
    }

    //public void UpdateMenberAllInfo()
    //{
    //    for (int i= 0; i < 5; i++)
    //    {
    //        UpdateMenberInfo(i);
    //    }
    //}
    public void UpdateLeaderInfo(int index)
    {
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();
        if (gci.temp_Leader.sex == 0)
        {
            leaderManImage.color = new Color(1f, 1f, 1f, 1f);
            leaderWomanImage.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            leaderManImage.color = new Color(1f, 1f, 1f, 0.5f);
            leaderWomanImage.color = new Color(1f, 1f, 1f, 1f);
        }

        for (int i = 0; i < 6; i++)
        {
            leaderTypeImageList[i].color = new Color(1f, 1f, 1f, 0.5f);
            leaderTypeTextList[i].color= new Color(leaderTypeTextList[i].color.r, leaderTypeTextList[i].color.g, leaderTypeTextList[i].color.b, 0.5f);
        }
        leaderTypeImageList[index].color = new Color(1f, 1f, 1f, 1f);
        leaderTypeTextList[index].color = new Color(leaderTypeTextList[index].color.r, leaderTypeTextList[index].color.g, leaderTypeTextList[index].color.b, 1f);

        //Debug.Log(leaderTypeImageList[3].color);
    }


    public void UpdateMenberInfo( int index)
    {

        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();


        picImageList[index].overrideSprite= Resources.Load("Image/RolePic/" + gci.temp_HeroList[index].pic, typeof(Sprite)) as Sprite;
        nameTextList[index].text = gci.temp_HeroList[index].name;
        typeTextList[index].text = "<color=#"+DataManager.mCreateHeroTypeDict[ gci.temp_HeroList[index].type].Color+">"+ DataManager.mCreateHeroTypeDict[gci.temp_HeroList[index].type].Name+"</color>";
    }


}
