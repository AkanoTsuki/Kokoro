using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlInNewGame : MonoBehaviour
{


    GameControl gc;
     int temp_districtID=-1;
    public int temp_leaderHeroType = -1;
    public HeroObject[] temp_HeroList = { null, null, null, null, null};

    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();

        UIManager.Instance.SceneUIInit();
        UIManager.Instance.InitPanel(UIPanelType.StartChoose);
        StartChoosePanel.Instance.SetAnchoredPosition(32, -32);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gc.Save();
        }
    }

    public void SetDistrict(int districtID)
    {
        temp_districtID = districtID;
    }


    public void SetPlayerName(string name)
    {      
        gc.playerName = name;
    }

    public void SetLeaderHeroType(int typeID)
    {
        temp_leaderHeroType = typeID;
    }

    public void RollMenberAll()
    {
        for (int i = 0; i < 5; i++)
        {
            RollMenber(i);
        }
    }

    public void RollMenber(int index)
    {
        int ran = Random.Range(0, 6);
        temp_HeroList[index]=gc.GenerateHero(index, ran, Random.Range(0, 2));
    }


}
