using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameControlInNewGame : MonoBehaviour
{
    GameControl gc;

    //运行变量
    short temp_districtID = -1;
    byte temp_flag = 0;
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

        temp_districtID = 1;
        temp_leaderHeroSex = 0;
        SetLeaderHeroType(0);
        SetFlag(0);
        StartChoosePanel.Instance.UpdateFlag(0);
        RollMenberAll();
        HeroPanel.Instance.OnHide();
    }

    public void SetFlag(byte flag)
    {
        temp_flag = flag;
    }

    public void SetLeaderHeroSex(byte sex)
    {
        temp_leaderHeroSex = sex;
        temp_Leader.sex= sex;
        if (sex==0)
        {
            temp_Leader.pic = DataManager.mHeroDict[temp_leaderHeroType].PicMan[0];
        }
        else if (sex == 1)
        {
            temp_Leader.pic = DataManager.mHeroDict[temp_leaderHeroType].PicWoman[0];
        }

        StartChoosePanel.Instance.UpdateLeaderInfo(temp_leaderHeroType);
        HeroPanel.Instance.OnShow(temp_Leader, false,  374, -32);
    }

    public void SetLeaderHeroType(short typeID)
    {
        temp_leaderHeroType = typeID;
        temp_Leader= gc.GenerateHeroByRandom(0, typeID, temp_leaderHeroSex,1,0);
        StartChoosePanel.Instance.UpdateLeaderInfo(typeID);
        HeroPanel.Instance.OnShow(temp_Leader, false,  374, -32);
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
        temp_HeroList[index]=gc.GenerateHeroByRandom(index+1, (short)ran,(byte)Random.Range(0, 2),1,0);

        StartChoosePanel.Instance.UpdateMenberInfo(index);
        HeroPanel.Instance.UpdateAllInfo(temp_HeroList[index]);
    }

    //确认并正式开始游戏
    public void ConfirmAndStart()
    {
        //districtNum=1
        gc.forceDic.Add(0, new ForceObject(0, temp_flag, StartChoosePanel.Instance.leaderNameIf.text+ "封地", StartChoosePanel.Instance.leaderNameIf.text, 3, new List<byte> { }, new List<short> { 1 }, new Dictionary<byte, short> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } }, 3500, 0,0,0,0,0,0, 10000, 10000, 10000, 10000, 10000, 10000, 10000,0,0,0,0,0,0,100000,100000,200, 0, 20000));
        gc.forceDic.Add(1, new ForceObject(1, (byte)(temp_flag == 1 ? 0 : 1), "格兰蒂斯公国", "大公·洛兰特二世", 1, new List<byte> { }, new List<short> { 0, 8, 9 }, new Dictionary<byte, short> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } }, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 0, 100000));
        gc.forceDic.Add(2, new ForceObject(2, (byte)(temp_flag == 2 ? 0 : 2), "纳德森管理地", "村长·纳德森", 1, new List<byte> { }, new List<short> { 2 }, new Dictionary<byte, short> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } }, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 0, 20000));
        gc.forceDic.Add(3, new ForceObject(3, (byte)(temp_flag == 3 ? 0 : 3), "安尔加封地", "伯爵·安尔加", 1, new List<byte> { 0}, new List<short> { 5, 6 }, new Dictionary<byte, short> { { 0, 65 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } }, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 0, 20000));
        gc.forceDic.Add(4, new ForceObject(4, (byte)(temp_flag == 4 ? 0 : 4), "阿伦德尔封地", "伯爵·阿伦德尔", 1, new List<byte> { }, new List<short> { 7 }, new Dictionary<byte, short> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } }, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 0, 20000));
        gc.forceDic.Add(5, new ForceObject(5, (byte)(temp_flag == 5 ? 0 : 5), "朱利奥封地", "伯爵·朱利奥", 1, new List<byte> { }, new List<short> { 3, 4 }, new Dictionary<byte, short> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } }, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 0, 20000));
        gc.forceDic.Add(6, new ForceObject(6, (byte)(temp_flag == 6 ? 0 : 6), "北境", "骑士·特雷西亚", 1, new List<byte> { }, new List<short> { 10 }, new Dictionary<byte, short> { { 0, -17 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } }, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 0, 20000));



        // Debug.Log(" gc.districtDic.Length=" + gc.districtDic.Length);
        for (int i = 0; i < DataManager.mDistrictDict.Count; i++)
        {
            short force = -1;
            for (int j = 0; j < gc.forceDic.Count; j++)
            {
                if (gc.forceDic[j].districtID.Contains((short)i))
                {
                    force = (short)j;
                    break;
                }
            }

            gc.districtDic[i] = new DistrictObject((short)i, DataManager.mDistrictDict[i].Name, "", DataManager.mDistrictDict[i].Des, true, force, DataManager.mDistrictDict[i].InitLevel, DataManager.mDistrictDict[i].InitWallLevel,10, 20, 0,
             new List<int> { }, temp_districtID == i ? new List<int> { 0, 1, 2, 3, 4, 5 } : new List<int> { }, new List<int> { }, DataManager.mDistrictDict[i].EWind, DataManager.mDistrictDict[i].EFire, DataManager.mDistrictDict[i].EWater, DataManager.mDistrictDict[i].EGround, DataManager.mDistrictDict[i].ELight, DataManager.mDistrictDict[i].EDark,
            0, 0, 0, 0, 0, 0, 0, 0,  50,
            1000,1000,20,
            50, 100, 50, 50, 50, 50,
            50,
            50, 50, 50, 50,
            100,100,100,100,100,100,100,20,20,20,
            new List<DistrictFiscal> {  new DistrictFiscal(0,0,0, 0, 0, 0,0), new DistrictFiscal(0, 0, 0, 0, 0, 0, 0) });

            gc.districtGridDic.Add(new Dictionary<string, DistrictGridObject>());

            //设置默认满意度，繁荣值
            gc.DistrictSetSatisfactionInNewGame((short)i);
            gc.DistrictSetProsperousInNewGame((short)i);
        }
        for (int i = 0; i < DataManager.mDistrictGridDict.Count; i++)
        {
            
            gc.districtGridDic[DataManager.mDistrictGridDict[i].DistrictID].Add(DataManager.mDistrictGridDict[i].DistrictID+"_"+ DataManager.mDistrictGridDict[i].X+"," + DataManager.mDistrictGridDict[i].Y, new DistrictGridObject(DataManager.mDistrictGridDict[i].Level,-1));
        }

        for (int i = 0; i < DataManager.mTechnologyDict.Count; i++)
        {
            gc.technologyDic.Add(i, new TechnologyObject((short)i, (DataManager.mTechnologyDict[i].ParentID.Count == 0? TechnologyStage.Open:TechnologyStage.Close)));
   
        }
    


        gc.heroDic.Add(0, temp_Leader);
        gc.heroDic[0].name = StartChoosePanel.Instance.leaderNameIf.text;
        gc.heroDic[0].groupRate += 0.1f;//主角优待
        gc.heroDic[0].salary = 0;

        for (int i = 0; i < 5; i++)
        {
            gc.heroDic.Add(i + 1, temp_HeroList[i]);
        }
        gc.heroIndex = 6;
        

        gc.adventureTeamList.Add(new AdventureTeamObject(0, -1,-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new List<string> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, 0, 0, AdventureState.Free, AdventureAction.None, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new List<int> { }, 0, new List<string> { }, new List<AdventurePartObject> { }));
        gc.adventureTeamList.Add(new AdventureTeamObject(1, -1,-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new List<string> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, 0, 0, AdventureState.Free, AdventureAction.None, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new List<int> { }, 0, new List<string> { }, new List<AdventurePartObject> { }));

        //最多7个冒险队
        for (int i = 0; i < 7; i++)
        {
            gc.fightMenberObjectSS.Add(new List<FightMenberObject>());
        }


        for (short i = 0; i < DataManager.mDungeonDict.Count; i++)
        {
            gc.dungeonList.Add(new DungeonObject(i, DungeonStage.Open, new List<byte> { }, 1));
        }

        for (short i = 0; i < DataManager.mConsumableDict.Count; i++)
        {
            gc.consumableNum.Add(0);
        }

            gc.buildingUnlock = new bool[78];
        //gc.buildingUnlock[0]= true;
        //gc.buildingUnlock[3]= true;
        //gc.buildingUnlock[9]= true;
        //gc.buildingUnlock[10]= true;
        //gc.buildingUnlock[11]= true;
        //gc.buildingUnlock[12]= true;
        //gc.buildingUnlock[13]= true;
        //gc.buildingUnlock[14]= true;
        //gc.buildingUnlock[15]= true;
        //gc.buildingUnlock[16]= true;
        //gc.buildingUnlock[19]= true;
        //gc.buildingUnlock[22]= true;
        //gc.buildingUnlock[27]= true;
        //gc.buildingUnlock[32]= true;
        //gc.buildingUnlock[37]= true;
        //gc.buildingUnlock[42]= true;
        //gc.buildingUnlock[47]= true;
        //gc.buildingUnlock[48]= true;
        //gc.buildingUnlock[49]= true;
        //gc.buildingUnlock[59] = true;
        //gc.buildingUnlock[60] = true;
        //gc.buildingUnlock[61] = true;
        //gc.buildingUnlock[62] = true;
        //gc.buildingUnlock[63] = true;
        //gc.buildingUnlock[64] = true;
        //gc.buildingUnlock[65]= true;
        //gc.buildingUnlock[73] = true;

        //TODO:开发配置阶段 全部建筑都开启
        for (int i = 0; i < gc.buildingUnlock.Length; i++)
        {
            gc.buildingUnlock[i] = true;
        }


        gc.forgeAddUnlock.Add(StuffType.Wood, true);
        gc.forgeAddUnlock.Add(StuffType.Stone, true);
        gc.forgeAddUnlock.Add(StuffType.Metal, true);
        gc.forgeAddUnlock.Add(StuffType.Leather, true);
        gc.forgeAddUnlock.Add(StuffType.Cloth, true);
        gc.forgeAddUnlock.Add(StuffType.Twine, true);
        gc.forgeAddUnlock.Add(StuffType.Bone, true);
        gc.forgeAddUnlock.Add(StuffType.Wind, true);
        gc.forgeAddUnlock.Add(StuffType.Fire, true);
        gc.forgeAddUnlock.Add(StuffType.Water, true);
        gc.forgeAddUnlock.Add(StuffType.Ground, true);
        gc.forgeAddUnlock.Add(StuffType.Light, true);
        gc.forgeAddUnlock.Add(StuffType.Dark, true);

        gc.supplyAndDemand = new SupplyAndDemandObject(new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
             );




        foreach (KeyValuePair<int, SkillPrototype> kvp in DataManager.mSkillDict)
        {
            gc.GenerateSkillByOriginal((short)kvp.Key);
            gc.skillIndex++;
        }


        gc.CreateSalesRecord(gc.timeYear, gc.timeMonth);
        gc.CreateCustomerRecord(gc.timeYear, gc.timeMonth);

        //gc.gold = 50000;
        gc.nowCheckingDistrictID = temp_districtID;

        InitBuilding();


        gc.Save();
        SceneManager.LoadScene("A3_Play");
    }

    void InitBuilding()
    {
        //gc.buildingIndex = 1;
        DataManager.NewGameInit();
        foreach (KeyValuePair<int, DevelopInitBuilding> kvp in DataManager.dBuildingDict)
        {
   
            short pid = kvp.Value.PrototypeID;

            gc.buildingDic.Add(kvp.Value.ID, new BuildingObject(kvp.Value.ID, kvp.Value.PrototypeID, kvp.Value.DistrictID, DataManager.mBuildingDict[pid].Name, DataManager.mBuildingDict[pid].MainPic,kvp.Value.NpcList, kvp.Value.PositionX, kvp.Value.PositionY, kvp.Value.Layer, kvp.Value.PositionX > 64 ? AnimStatus.WalkLeft : AnimStatus.WalkRight, DataManager.mBuildingDict[pid].PanelType, DataManager.mBuildingDict[pid].Des, DataManager.mBuildingDict[pid].Level, DataManager.mBuildingDict[pid].Expense, DataManager.mBuildingDict[pid].UpgradeTo, false, false, kvp.Value.GridList, new List<int> { }, new List<int> { },
            DataManager.mBuildingDict[pid].People, DataManager.mBuildingDict[pid].Worker, 0,
            DataManager.mBuildingDict[pid].EWind, DataManager.mBuildingDict[pid].EFire, DataManager.mBuildingDict[pid].EWater, DataManager.mBuildingDict[pid].EGround, DataManager.mBuildingDict[pid].ELight, DataManager.mBuildingDict[pid].EDark,
            new List<BuildingTaskObject> { }, -1, 1));

            //Debug.Log("kvp.Value.ID="+ kvp.Value.ID);
            //gc.buildingIndex++;
        }
        gc.buildingIndex = DataManager.dBuildingDict.Count+ 1;

        foreach (KeyValuePair<string, DevelopInitGrid> kvp in DataManager.dGridDict)
        {
            gc.districtGridDic[kvp.Value.DistrictID][kvp.Value.ID].buildingID = kvp.Value.BuildingID;
        }

        foreach (KeyValuePair<int, DevelopInitDistrict> kvp in DataManager.dDistrictDict)
        {
            gc.districtDic[kvp.Value.ID].peopleLimit = kvp.Value.PeopleLimit;
            gc.districtDic[kvp.Value.ID].buildingList = kvp.Value.BuildingList;
            gc.districtDic[kvp.Value.ID].eWind = kvp.Value.EWind;
            gc.districtDic[kvp.Value.ID].eFire = kvp.Value.EFire;
            gc.districtDic[kvp.Value.ID].eWater = kvp.Value.EWater;
            gc.districtDic[kvp.Value.ID].eGround = kvp.Value.EGround;
            gc.districtDic[kvp.Value.ID].eLight = kvp.Value.ELight;
            gc.districtDic[kvp.Value.ID].eDark = kvp.Value.EDark;
            gc.districtDic[kvp.Value.ID].rProductLimit = kvp.Value.RProductLimit;
        }

    }
}
