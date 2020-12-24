using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillListAndInfoPanel : BasePanel
{
    public static SkillListAndInfoPanel Instance;

    GameControl gc;

    public Text titleText;
    public Text numText;
    public RectTransform list_nowRt;
    public Image list_nowPicImage;
    public Text list_nowNameText;
    public Button list_nowEmptyBtn;

    public RectTransform list_filterRt;
    public Button list_filterWindBtn;
    public Button list_filterFireBtn;
    public Button list_filterWaterBtn;
    public Button list_filterGroundBtn;
    public Button list_filterLightBtn;
    public Button list_filterDarkBtn;
    public Button list_filterThunderBtn;
    public Button list_filterExplodeBtn;
    public Button list_filterIceBtn;
    public Button list_filterNaturalBtn;
    public Button list_filterSpaceBtn;
    public Button list_filterDeathBtn;

    public RectTransform list_scrollViewRt;
    public GameObject list_skillListGo;

    public Image info_picImage;
    public Text info_nameText;
    public Text info_desText;

    public Text tipText;
    public List<Button> funcBtn;

    public Button closeBtn;

    List<GameObject> skillGo = new List<GameObject>();

    public int nowSkillID = -1;

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(delegate () { OnHide(); });

    }
}
