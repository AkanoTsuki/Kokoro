using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameControlInNewGame : MonoBehaviour
{


    GameControl gc;
    short temp_districtID = -1;
    public byte temp_leaderHeroSex = 0;
    public short temp_leaderHeroType = -1;
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
        
    }

    public void SetDistrict(short districtID)
    {
        temp_districtID = districtID;
        StartChoosePanel.Instance.UpdateDistrictInfo(districtID);
    }


    public void SetLeaderName(string name)
    {      
        gc.playerName = name;
    }

    public void SetLeaderHeroSex(byte sex)
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
        HeroPanel.Instance.OnShow(temp_Leader, false, false, 374, -32, -234);
    }

    public void SetLeaderHeroType(short typeID)
    {
        temp_leaderHeroType = typeID;
        temp_Leader= gc.GenerateHeroByMould(0, typeID, temp_leaderHeroSex,"[主角]");
        StartChoosePanel.Instance.UpdateLeaderInfo(typeID);
        HeroPanel.Instance.OnShow(temp_Leader, false, false, 374, -32, -234);
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
        temp_HeroList[index]=gc.GenerateHeroByRandom(index+1, (short)ran,(byte)Random.Range(0, 2));

        StartChoosePanel.Instance.UpdateMenberInfo(index);
        HeroPanel.Instance.UpdateAllInfo(temp_HeroList[index], -374);
    }

    //确认并正式开始游戏
    public void ConfirmAndStart()
    {
        for (int i = 0; i < DataManager.mDistrictGridDict.Count; i++)
        {
            gc.districtGridDic.Add(DataManager.mDistrictGridDict[i].ID, new DistrictGridObject(DataManager.mDistrictGridDict[i].ID, "", -1));
        }

        gc.heroDic.Add(0, temp_Leader);
        gc.heroDic[0].name = StartChoosePanel.Instance.leaderNameIf.text;
        for (int i = 0; i < 5; i++)
        {
            gc.heroDic.Add(i + 1, temp_HeroList[i]);
        }
        gc.heroIndex = 6;
        for (int i = 0; i < 7; i++)
        {
            gc.districtDic[i] = new DistrictObject((short)i, DataManager.mDistrictDict[i].Name, "初始村", DataManager.mDistrictDict[i].Des, temp_districtID == i, 1, 10, 20,0, DataManager.mDistrictDict[i].StartGrid[0], 0,
                DataManager.mDistrictDict[i].Grass[0], DataManager.mDistrictDict[i].Wood[0], DataManager.mDistrictDict[i].Water[0], DataManager.mDistrictDict[i].Stone[0], DataManager.mDistrictDict[i].Metal[0],
                0, 0, 0, 0, 0, new List<int> { }, temp_districtID == i? new List<int> { 0,1,2,3,4,5}: new List<int> { }, DataManager.mDistrictDict[i].EWind, DataManager.mDistrictDict[i].EFire, DataManager.mDistrictDict[i].EWater, DataManager.mDistrictDict[i].EGround, DataManager.mDistrictDict[i].ELight, DataManager.mDistrictDict[i].EDark,
                0, 0, 0, 0, 0, 1000, 1000, 1000, 500, 0, 0, 0, 0, 0, 0, 5000, 5000, 50);
        }

        gc.adventureTeamList.Add(new AdventureTeamObject(0, -1, new List<int> { }, new List<int> { }, new List<int> { }, 0, AdventureState.NotSend, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new List<int> { }, 0, "", new List<AdventurePartObject> { }));
        gc.adventureTeamList.Add(new AdventureTeamObject(1, -1, new List<int> { }, new List<int> { }, new List<int> { }, 0, AdventureState.NotSend, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new List<int> { }, 0, "", new List<AdventurePartObject> { }));

        gc.dungeonList.Add(new DungeonObject(0, true));
        gc.dungeonList.Add(new DungeonObject(1, true));
        gc.dungeonList.Add(new DungeonObject(2, true));
        gc.dungeonList.Add(new DungeonObject(3, true));
        gc.dungeonList.Add(new DungeonObject(4, true));
        gc.dungeonList.Add(new DungeonObject(5, true));
        gc.dungeonList.Add(new DungeonObject(6, true));

        gc.buildingUnlock[0]= true;
        gc.buildingUnlock[3]= true;
        gc.buildingUnlock[9]= true;
        gc.buildingUnlock[10]= true;
        gc.buildingUnlock[11]= true;
        gc.buildingUnlock[12]= true;
        gc.buildingUnlock[13]= true;
        gc.buildingUnlock[14]= true;
        gc.buildingUnlock[15]= true;
        gc.buildingUnlock[16]= true;
        gc.buildingUnlock[19]= true;
        gc.buildingUnlock[22]= true;
        gc.buildingUnlock[27]= true;
        gc.buildingUnlock[32]= true;
        gc.buildingUnlock[37]= true;
        gc.buildingUnlock[42]= true;
        gc.buildingUnlock[47]= true;
        gc.buildingUnlock[48]= true;
        gc.buildingUnlock[49]= true;
        gc.buildingUnlock[65]= true;

        gc.gold = 5000;
        gc.nowCheckingDistrictID = temp_districtID;
        gc.Save();
        SceneManager.LoadScene("A3_Play");
    }
}
