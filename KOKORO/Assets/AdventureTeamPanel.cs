using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureTeamPanel : BasePanel
{
    public static AdventureTeamPanel Instance;

    GameControl gc;
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
