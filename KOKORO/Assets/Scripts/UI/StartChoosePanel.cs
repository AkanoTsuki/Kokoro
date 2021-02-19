using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartChoosePanel : BasePanel
{
    public static StartChoosePanel Instance;

    GameControlInNewGame gci;

    #region 【UI控件】
    public List<Button> flagBtnList;
    public List<RectTransform> flagSelectedRt;

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
    #endregion

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();

        flagBtnList[0].onClick.AddListener(delegate () { gci.SetFlag(0); UpdateFlag(0); });
        flagBtnList[1].onClick.AddListener(delegate () { gci.SetFlag(1); UpdateFlag(1); });
        flagBtnList[2].onClick.AddListener(delegate () { gci.SetFlag(2); UpdateFlag(2); });
        flagBtnList[3].onClick.AddListener(delegate () { gci.SetFlag(3); UpdateFlag(3); });
        flagBtnList[4].onClick.AddListener(delegate () { gci.SetFlag(4); UpdateFlag(4); });
        flagBtnList[5].onClick.AddListener(delegate () { gci.SetFlag(5); UpdateFlag(5); });
        flagBtnList[6].onClick.AddListener(delegate () { gci.SetFlag(6); UpdateFlag(6); });
        flagBtnList[7].onClick.AddListener(delegate () { gci.SetFlag(7); UpdateFlag(7); });

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

    //旗帜选择栏目-更新
    public void UpdateFlag(byte flag)
    {
        for (byte i = 0; i < 8; i++)
        {
            if (i == flag)
            {
                flagSelectedRt[i].localScale = Vector2.one;
            }
            else
            {
                flagSelectedRt[i].localScale = Vector2.zero;
            }
        }
    }

    //主角设置栏目-更新
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

    //随从设置栏目-更新
    public void UpdateMenberInfo( int index)
    {
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();

        picImageList[index].overrideSprite= Resources.Load("Image/RolePic/" + gci.temp_HeroList[index].pic+"/Pic", typeof(Sprite)) as Sprite;
        nameTextList[index].text = gci.temp_HeroList[index].name;
        typeTextList[index].text = "<color=#"+DataManager.mHeroDict[ gci.temp_HeroList[index].prototypeID].Color+">"+ DataManager.mHeroDict[gci.temp_HeroList[index].prototypeID].Name+"</color>";
    }

}
