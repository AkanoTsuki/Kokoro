using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlInPlay : MonoBehaviour
{
    GameControl gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();

        UIManager.Instance.SceneUIInit();
        UIManager.Instance.InitPanel(UIPanelType.DistrictMain);
        UIManager.Instance.InitPanel(UIPanelType.Hero);
        UIManager.Instance.InitPanel(UIPanelType.Build);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDistrictMain()
    {

        DistrictMainPanel.Instance.OnShow(gc.districtDic[gc.nowCheckingDistrictID],84,-88);
    }

    public void OpenBuild()
    {

        BuildPanel.Instance.OnShow( 688, -88);
    }
}
