using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureTeamPanel : BasePanel
{
    public static AdventureTeamPanel Instance;

    GameControl gc;
    public Text titleText;

    public Text dungeon_desText;

    public List<Image> hero_picImage;
    public List<Text> hero_nameText;
    public List<Text> hero_hpmpText;
    public Text hero_contentText;

    public GameObject partListGo;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
