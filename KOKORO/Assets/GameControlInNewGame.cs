using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameControlInNewGame : MonoBehaviour
{


    GameControl gc;
    int temp_districtID = -1;
    public int temp_leaderHeroSex = 0;
    public int temp_leaderHeroType = -1;
    public HeroObject temp_Leader = null;
    public HeroObject[] temp_HeroList = { null, null, null, null, null };


    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();

        UIManager.Instance.SceneUIInit();
        UIManager.Instance.InitPanel(UIPanelType.StartChoose);
        StartChoosePanel.Instance.SetAnchoredPosition(32, -32);
        UIManager.Instance.InitPanel(UIPanelType.Hero);
        //HeroPanel.Instance.OnHide();

        //gc.Delete();

        temp_leaderHeroSex = 0;
        SetLeaderHeroType(0);
        SetDistrict(0);
        RollMenberAll();
        HeroPanel.Instance.OnHide();
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
        StartChoosePanel.Instance.UpdateDistrictInfo(districtID);
    }


    public void SetLeaderName(string name)
    {      
        gc.playerName = name;
    }

    public void SetLeaderHeroSex(int sex)
    {
        temp_leaderHeroSex = sex;
        temp_Leader.sex= sex;
        if (sex==0)
        {
            temp_Leader.pic = DataManager.mHeroDict[temp_leaderHeroType].PicMan;
        }
        else if (sex == 1)
        {
            temp_Leader.pic = DataManager.mHeroDict[temp_leaderHeroType].PicWoman;
        }
        Debug.Log("temp_Leader.sex="+temp_Leader.sex);
        StartChoosePanel.Instance.UpdateLeaderInfo(temp_leaderHeroType);
        HeroPanel.Instance.OnShow(temp_Leader, 374, -32, -234);
    }

    public void SetLeaderHeroType(int typeID)
    {
        temp_leaderHeroType = typeID;
        temp_Leader= gc.GenerateHeroByMould(0, typeID, temp_leaderHeroSex,"[主角]");
        StartChoosePanel.Instance.UpdateLeaderInfo(typeID);
        HeroPanel.Instance.OnShow(temp_Leader, 374, -32, -234);
    }

    public void RollMenberAll()
    {
        for (int i = 0; i < 5; i++)
        {
            RollMenber(i);
        }
        //StartChoosePanel.Instance.UpdateMenberAllInfo();
    }

    public void RollMenber(int index)
    {
        int ran = Random.Range(0, 6);
        temp_HeroList[index]=gc.GenerateHeroByRandom(index, ran, Random.Range(0, 2));

        StartChoosePanel.Instance.UpdateMenberInfo(index);
        HeroPanel.Instance.UpdateAllInfo(gc, temp_HeroList[index], -374);
    }

    //确认并正式开始游戏
    public void ConfirmAndStart()
    {
        for (int i = 0; i < DataManager.mDistrictGridDict.Count; i++)
        {
            gc.districtGridDic.Add(DataManager.mDistrictGridDict[i].ID, new DistrictGridObject(DataManager.mDistrictGridDict[i].ID, "", -1));
        }

        gc.heroDic.Add(0, temp_Leader);
        for (int i = 0; i < 5; i++)
        {
            gc.heroDic.Add(i + 1, temp_HeroList[i]);
        }
        gc.heroIndex = 6;
        for (int i = 0; i < 7; i++)
        {
            gc.districtDic[i] = new DistrictObject(i, DataManager.mDistrictDict[i].Name, "初始村", DataManager.mDistrictDict[i].Des, temp_districtID == i, 1, 10, 20, DataManager.mDistrictDict[i].StartGrid[0], 0,
                DataManager.mDistrictDict[i].Grass[0], DataManager.mDistrictDict[i].Wood[0], DataManager.mDistrictDict[i].Water[0], DataManager.mDistrictDict[i].Stone[0], DataManager.mDistrictDict[i].Metal[0],
                0, 0, 0, 0, 0, new List<int> { }, temp_districtID == i? new List<int> { 0,1,2,3,4,5}: new List<int> { }, DataManager.mDistrictDict[i].EWind, DataManager.mDistrictDict[i].EFire, DataManager.mDistrictDict[i].EWater, DataManager.mDistrictDict[i].EGround, DataManager.mDistrictDict[i].ELight, DataManager.mDistrictDict[i].EDark,
                0, 0, 0, 0, 0, 500, 500, 500, 500, 0, 0, 0, 0, 0, 0, 5000, 5000, 50);
        }
        gc.gold = 5000;
        gc.nowCheckingDistrictID = temp_districtID;
        gc.Save();
        SceneManager.LoadScene("A3_Play");
    }
}
