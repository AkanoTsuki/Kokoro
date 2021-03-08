﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;
using System.Linq;
public class GameControl : MonoBehaviour
{


    //no save
    float roleHeight = 54f;//人物高度
    public const float spacing = 4f;

    //save data
    public byte volumeMusic = 1;
    public byte volumeSound = 1;
    public byte timeFlowSpeed = 1;//0,1,2
    public int timeS = 0;
    public int timeHour = 0;
    public int timeDay = 1;
    public int timeWeek = 1;
    public int timeMonth = 1;
    public int timeSeason = 1;
    public int timeYear = 1;
    //public int gold = 0;
    public short nowCheckingDistrictID = 0;
    public int standardTime = 0;//时间戳，基准时间单位：1/10小时（例如 1天为240单位 ）
    public List<ExecuteEventObject> executeEventList = new List<ExecuteEventObject>();
    public Dictionary<int, ExecuteEventObject> executeEventDic = new Dictionary<int, ExecuteEventObject>();
    public int eventIndex = 0;
    public int heroIndex = 0;
    public int itemIndex = 0;
    public int skillIndex = 0;
    public int customerIndex = 0;
    public int buildingIndex = 0;
    public int travellerIndex = 0;
    public int forceIndex = 0;
    public bool[] buildingUnlock = new bool[78];
    public Dictionary<StuffType, bool> forgeAddUnlock = new Dictionary<StuffType, bool>();
    public int logIndex = 0;
    //public byte forceFlag = 0;
    public List<int> consumableNum=new List<int>();

    public List<byte> itemPanel_rankSelected = new List<byte>();
    public List<byte> itemPanel_levelSelected = new List<byte>();
    public List<ItemTypeSmall> itemPanel_typeSelected = new List<ItemTypeSmall>();
    public List<byte> skillPanel_rankSelected = new List<byte>();
    public List<ItemTypeSmall> skillPanel_typeSelected = new List<ItemTypeSmall>();

    public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
    public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();
    public DistrictObject[] districtDic = new DistrictObject[11];
    public List<Dictionary<string, DistrictGridObject>> districtGridDic = new List<Dictionary<string, DistrictGridObject>>();
    public Dictionary<int, BuildingObject> buildingDic = new Dictionary<int, BuildingObject>();
    public Dictionary<int, LogObject> logDic = new Dictionary<int, LogObject>();
    public List<AdventureTeamObject> adventureTeamList = new List<AdventureTeamObject>();
    public List<DungeonObject> dungeonList = new List<DungeonObject>();
    public Dictionary<int, SkillObject> skillDic = new Dictionary<int, SkillObject>();
    public List<List<FightMenberObject>> fightMenberObjectSS = new List<List<FightMenberObject>>();
    public SupplyAndDemandObject supplyAndDemand;
    public Dictionary<string, SalesRecordObject> salesRecordDic = new Dictionary<string, SalesRecordObject>();
    public Dictionary<int, CustomerObject> customerDic = new Dictionary<int, CustomerObject>();
    public Dictionary<string, CustomerRecordObject> customerRecordDic = new Dictionary<string, CustomerRecordObject>();
    public Dictionary<int, TechnologyObject> technologyDic = new Dictionary<int, TechnologyObject>();
    public List<int> technologyResearchingList = new List<int>();
    public Dictionary<int, TravellerObject> travellerDic = new Dictionary<int, TravellerObject>();
    public Dictionary<int, ForceObject> forceDic = new Dictionary<int, ForceObject>();
    /// <summary>
    /// 用作存档的数据类
    /// </summary>
    [System.Serializable]
    public class DataSave
    {
        public byte volumeMusic = 1;
        public byte volumeSound = 1;
        public byte timeFlowSpeed = 1;
        public int timeS = 0;
        public int timeHour = 0;
        public int timeDay = 1;
        public int timeWeek = 1;
        public int timeMonth = 1;
        public int timeSeason = 1;
        public int timeYear = 1;
        //public int gold = 0;
        public short nowCheckingDistrictID = 0;
        public int standardTime = 0;
        public List<ExecuteEventObject> executeEventList = new List<ExecuteEventObject>();
        public Dictionary<int, ExecuteEventObject> executeEventDic = new Dictionary<int, ExecuteEventObject>();
        public int eventIndex = 0;
        public int heroIndex = 0;
        public int itemIndex = 0;
       
        public int skillIndex = 0;
        public int customerIndex = 0;
        public int buildingIndex = 0;
        public int travellerIndex = 0;
        public int forceIndex = 0;
        public bool[] buildingUnlock = new bool[78];
        public Dictionary<StuffType, bool> forgeAddUnlock = new Dictionary<StuffType, bool>();
        public int logIndex = 0;
        public List<int> consumableNum = new List<int>();
        //public byte forceFlag = 0;

        public List<byte> itemPanel_rankSelected = new List<byte>();
        public List<byte> itemPanel_levelSelected = new List<byte>();
        public List<ItemTypeSmall> itemPanel_typeSelected = new List<ItemTypeSmall>();
        public List<byte> skillPanel_rankSelected = new List<byte>();
        public List<ItemTypeSmall> skillPanel_typeSelected = new List<ItemTypeSmall>();

        public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
        public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();
        public DistrictObject[] districtDic = new DistrictObject[11];
        public List<Dictionary<string, DistrictGridObject>> districtGridDic = new List<Dictionary<string, DistrictGridObject>>();
        public Dictionary<int, BuildingObject> buildingDic = new Dictionary<int, BuildingObject>();
        public Dictionary<int, LogObject> logDic = new Dictionary<int, LogObject>();
        public List<AdventureTeamObject> adventureTeamList = new List<AdventureTeamObject>();
        public List<DungeonObject> dungeonList = new List<DungeonObject>();
        public Dictionary<int, SkillObject> skillDic = new Dictionary<int, SkillObject>();
        public List<List<FightMenberObject>> fightMenberObjectSS = new List<List<FightMenberObject>>();
        public SupplyAndDemandObject supplyAndDemand;
        public Dictionary<string, SalesRecordObject> salesRecordDic = new Dictionary<string, SalesRecordObject>();
        public Dictionary<int, CustomerObject> customerDic = new Dictionary<int, CustomerObject>();
        public Dictionary<string, CustomerRecordObject> customerRecordDic = new Dictionary<string, CustomerRecordObject>();
        public Dictionary<int, TechnologyObject> technologyDic = new Dictionary<int, TechnologyObject>();
        public List<int> technologyResearchingList = new List<int> ();
        public Dictionary<int, TravellerObject> travellerDic = new Dictionary<int, TravellerObject>();
        public Dictionary<int, ForceObject> forceDic = new Dictionary<int, ForceObject>();
    }


    public void Save()
    {
        print("Save");
        //定义存档路径
        string dirpath = Application.dataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";
        DataSave t = new DataSave();

        t.volumeMusic = this.volumeMusic;
        t.volumeSound = this.volumeSound;
        t.timeFlowSpeed = this.timeFlowSpeed;
        t.timeS = this.timeS;
        t.timeHour = this.timeHour;
        t.timeDay = this.timeDay;
        t.timeWeek = this.timeWeek;
        t.timeMonth = this.timeMonth;
        t.timeSeason = this.timeSeason;
        t.timeYear = this.timeYear;
       // t.gold = this.gold;
        t.nowCheckingDistrictID = this.nowCheckingDistrictID;
        t.standardTime = this.standardTime;
        t.executeEventList = this.executeEventList;
        t.executeEventDic = this.executeEventDic;
        t.eventIndex = this.eventIndex;
        t.heroIndex = this.heroIndex;
        t.itemIndex = this.itemIndex;

        t.skillIndex = this.skillIndex;
        t.customerIndex = this.customerIndex;
        t.buildingIndex = this.buildingIndex;
        t.travellerIndex = this.travellerIndex;
        t.forceIndex = this.forceIndex;
        t.buildingUnlock = this.buildingUnlock;
        t.forgeAddUnlock = this.forgeAddUnlock;
        t.logIndex = this.logIndex;
        t.consumableNum = this.consumableNum;
        // t.forceFlag = this.forceFlag;
        t.itemPanel_rankSelected = this.itemPanel_rankSelected;
        t.itemPanel_levelSelected = this.itemPanel_levelSelected;
        t.itemPanel_typeSelected = this.itemPanel_typeSelected;
        t.skillPanel_rankSelected = this.skillPanel_rankSelected;
        t.skillPanel_typeSelected = this.skillPanel_typeSelected;

        t.itemDic = this.itemDic;
        t.heroDic = this.heroDic;
        t.districtDic = this.districtDic;
        t.districtGridDic = this.districtGridDic;
        t.buildingDic = this.buildingDic;
        t.logDic = this.logDic;
        t.adventureTeamList = this.adventureTeamList;
        t.dungeonList = this.dungeonList;
        t.skillDic = this.skillDic;
        t.fightMenberObjectSS = this.fightMenberObjectSS;
        t.supplyAndDemand = this.supplyAndDemand;
        t.salesRecordDic = this.salesRecordDic;
        t.customerDic = this.customerDic;
        t.customerRecordDic = this.customerRecordDic;
        t.technologyDic = this.technologyDic;
        t.technologyResearchingList = this.technologyResearchingList;
        t.travellerDic = this.travellerDic;
        t.forceDic = this.forceDic;
        //保存数据
        IOHelper.SetData(filename, t);
    }

    public void Load()
    {
        print("Load");
        //定义存档路径
        string dirpath = Application.dataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";

        //读取数据
        if (File.Exists(@filename))
        {
            print("文件存在." + filename);
            DataSave t1 = (DataSave)IOHelper.GetData(filename, typeof(DataSave));

            this.volumeMusic = t1.volumeMusic;
            this.volumeSound = t1.volumeSound;
            this.timeFlowSpeed = t1.timeFlowSpeed;
            this.timeS = t1.timeS;
            this.timeHour = t1.timeHour;
            this.timeDay = t1.timeDay;
            this.timeWeek = t1.timeWeek;
            this.timeMonth = t1.timeMonth;
            this.timeSeason = t1.timeSeason;
            this.timeYear = t1.timeYear;
           // this.gold = t1.gold;
            this.nowCheckingDistrictID = t1.nowCheckingDistrictID;
            this.standardTime = t1.standardTime;
            this.executeEventList = t1.executeEventList;
            this.executeEventDic = t1.executeEventDic;
            this.eventIndex = t1.eventIndex;
            this.heroIndex = t1.heroIndex;
            this.itemIndex = t1.itemIndex;

            this.skillIndex = t1.skillIndex;
            this.customerIndex = t1.customerIndex;
            this.buildingIndex = t1.buildingIndex;
            this.travellerIndex = t1.travellerIndex;
            this.forceIndex = t1.forceIndex;
            this.buildingUnlock = t1.buildingUnlock;
            this.forgeAddUnlock = t1.forgeAddUnlock;
            this.logIndex = t1.logIndex;
            this.consumableNum = t1.consumableNum;
            // this.forceFlag = t1.forceFlag;
            this.itemPanel_rankSelected = t1.itemPanel_rankSelected;
            this.itemPanel_levelSelected = t1.itemPanel_levelSelected;
            this.itemPanel_typeSelected = t1.itemPanel_typeSelected;
            this.skillPanel_rankSelected = t1.skillPanel_rankSelected;
            this.skillPanel_typeSelected = t1.skillPanel_typeSelected;

            this.itemDic = t1.itemDic;

            this.heroDic = t1.heroDic;
            this.districtDic = t1.districtDic;
            this.districtGridDic = t1.districtGridDic;
            this.buildingDic = t1.buildingDic;
            this.logDic = t1.logDic;
            this.adventureTeamList = t1.adventureTeamList;
            this.dungeonList = t1.dungeonList;
            this.skillDic = t1.skillDic;
            this.fightMenberObjectSS = t1.fightMenberObjectSS;
            this.supplyAndDemand = t1.supplyAndDemand;
            this.salesRecordDic = t1.salesRecordDic;
            this.customerDic = t1.customerDic;
            this.customerRecordDic = t1.customerRecordDic;
            this.technologyDic = t1.technologyDic;
            this.technologyResearchingList = t1.technologyResearchingList;
            this.travellerDic = t1.travellerDic;
            this.forceDic = t1.forceDic;
        }
        else
        {
            print("文件不存在." + filename);

        }

    }

    public bool CheckSaveFile()
    {
        //定义存档路径
        string dirpath = Application.dataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";

        //读取数据
        if (File.Exists(@filename))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Delete()
    {
        print("Delete");
        //定义存档路径
        string dirpath = Application.dataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";
        //读取数据
        if (File.Exists(@filename))
        {
            File.Delete(filename);
            print("文件存在." + filename);
        }
        else
        {
            print("文件不存在." + filename);
        }
    }

    #region 【方法】生成英雄,英雄升级,英雄改名,招募，解雇,发工资
    public void HeroChangeName(int heroID, string newName)
    {
        heroDic[heroID].name = newName;
        HeroPanel.Instance.UpdateBasicInfo(heroDic[heroID]);
    }

    public void CreateHero(short pid, short districtID, short force)
    {
        heroDic.Add(heroIndex, GenerateHeroByRandom(heroIndex, pid, (byte)Random.Range(0, 2), districtID,force));
        heroIndex++;
    }

    public HeroObject GenerateHeroByRandom(int heroID, short heroTypeID, byte sexCode, short districtID, short force)
    {
        string name;
        string pic;
        if (sexCode == 0)
        {
            name = DataManager.mNameMan[Random.Range(0, DataManager.mNameMan.Length)];
            pic = DataManager.mHeroDict[heroTypeID].PicMan[Random.Range(0, DataManager.mHeroDict[heroTypeID].PicMan.Count)];
        }
        else
        {
            name = DataManager.mNameWoman[Random.Range(0, DataManager.mNameWoman.Length)];
            pic = DataManager.mHeroDict[heroTypeID].PicWoman[Random.Range(0, DataManager.mHeroDict[heroTypeID].PicWoman.Count)];
        }
        float groupRate = Random.Range(DataManager.mHeroDict[heroTypeID].GroupRate - 0.1f, DataManager.mHeroDict[heroTypeID].GroupRate + 0.1f);

        int hp = SetAttr(Attribute.Hp, heroTypeID);
        int mp = SetAttr(Attribute.Mp, heroTypeID);
        short hpRenew = 0;
        short mpRenew = 0;
        short atkMax = (short)SetAttr(Attribute.AtkMax, heroTypeID);
        short atkMin = (short)(atkMax - 2);
        short mAtkMax = (short)SetAttr(Attribute.MAtkMax, heroTypeID);
        short mAtkMin = (short)(mAtkMax - 2);
        short def = (short)SetAttr(Attribute.Def, heroTypeID);
        short mDef = (short)SetAttr(Attribute.MDef, heroTypeID);
        short hit = (short)SetAttr(Attribute.Hit, heroTypeID);
        short dod = (short)SetAttr(Attribute.Dod, heroTypeID);
        short criR = (short)SetAttr(Attribute.CriR, heroTypeID);
        short criD = 200;
        short spd = 80;
        short windDam = (short)SetAttr(Attribute.WindDam, heroTypeID);
        short fireDam = (short)SetAttr(Attribute.FireDam, heroTypeID);
        short waterDam = (short)SetAttr(Attribute.WaterDam, heroTypeID);
        short groundDam = (short)SetAttr(Attribute.GroundDam, heroTypeID);
        short lightDam = (short)SetAttr(Attribute.LightDam, heroTypeID);
        short darkDam = (short)SetAttr(Attribute.DarkDam, heroTypeID);
        short windRes = (short)SetAttr(Attribute.WindRes, heroTypeID);
        short fireRes = (short)SetAttr(Attribute.FireRes, heroTypeID);
        short waterRes = (short)SetAttr(Attribute.WaterRes, heroTypeID);
        short groundRes = (short)SetAttr(Attribute.GroundRes, heroTypeID);
        short lightRes = (short)SetAttr(Attribute.LightRes, heroTypeID);
        short darkRes = (short)SetAttr(Attribute.DarkRes, heroTypeID);

        short dizzyRes = 0;
        short confusionRes = 0;
        short poisonRes = 0;
        short sleepRes = 0;
        byte goldGet = 0;
        byte expGet = 0;
        byte itemGet = 0;
        byte workPlanting = (byte)SetAttr(Attribute.WorkPlanting, heroTypeID);
        byte workFeeding = (byte)SetAttr(Attribute.WorkFeeding, heroTypeID);
        byte workFishing = (byte)SetAttr(Attribute.WorkFishing, heroTypeID);
        byte workHunting = (byte)SetAttr(Attribute.WorkHunting, heroTypeID);
        byte workMining = (byte)SetAttr(Attribute.WorkMining, heroTypeID);
        byte workQuarrying = (byte)SetAttr(Attribute.WorkQuarrying, heroTypeID);
        byte workFelling = (byte)SetAttr(Attribute.WorkFelling, heroTypeID);
        byte workBuild = (byte)SetAttr(Attribute.WorkBuild, heroTypeID);
        byte workMakeWeapon = (byte)SetAttr(Attribute.WorkMakeWeapon, heroTypeID);
        byte workMakeArmor = (byte)SetAttr(Attribute.WorkMakeArmor, heroTypeID);
        byte workMakeJewelry = (byte)SetAttr(Attribute.WorkMakeJewelry, heroTypeID);
        byte workMakeScroll = (byte)SetAttr(Attribute.WorkMakeScroll, heroTypeID);
        byte workSundry = (byte)SetAttr(Attribute.WorkSundry, heroTypeID);

        List<short> characteristicList = new List<short>();
        for (int i = 0; i < 2; i++)
        {
            int ran = Random.Range(0, 100);
            if (ran < DataManager.mCharacteristicDict.Count && (!characteristicList.Contains((short)ran)))
            {
                characteristicList.Add((short)ran);
            }
        }

        short salary = (short)(Random.Range(50, 150) * groupRate);

        //TODO测试 好看的halo
        List<short> testHalo = new List<short> { 0,2,3,4,5,6,7,8,9,15,16,17,29,30,31,32,37,38,39,40,48,52};
        short halo = (short)Random.Range(0, testHalo.Count - 1);
        halo = 15;

        return new HeroObject(heroID, name, heroTypeID, 1, 0, sexCode, pic, salary,groupRate, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
            (short)hp, (short)mp, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR,
          windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, goldGet, expGet, itemGet,
          workPlanting, workFeeding, workFishing, workHunting, workMining, workQuarrying, workFelling, workBuild, workMakeWeapon, workMakeArmor, workMakeJewelry, workMakeScroll, workSundry,
          -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, new List<short> { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, new List<int> { -1, -1, -1, -1 }, -1, -1, districtID, force,
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new Dictionary<short, HeroSkill>(), characteristicList, new List<string> { }, halo,false,-1);

    }


    public int SetAttr(Attribute attr, short heroTypeID)
    {
        int rank;

        switch (attr)
        {
            case Attribute.Hp: rank = DataManager.mHeroDict[heroTypeID].Hp; break;
            case Attribute.Mp: rank = DataManager.mHeroDict[heroTypeID].Mp; break;

            case Attribute.AtkMax: rank = DataManager.mHeroDict[heroTypeID].AtkMax; break;
            case Attribute.MAtkMax: rank = DataManager.mHeroDict[heroTypeID].MAtkMax; break;

            case Attribute.Def: rank = DataManager.mHeroDict[heroTypeID].Def; break;
            case Attribute.MDef: rank = DataManager.mHeroDict[heroTypeID].MDef; break;

            case Attribute.Hit: rank = DataManager.mHeroDict[heroTypeID].Hit; break;
            case Attribute.Dod: rank = DataManager.mHeroDict[heroTypeID].Dod; break;
            case Attribute.CriR: rank = DataManager.mHeroDict[heroTypeID].CriR; break;


            case Attribute.WorkPlanting: rank = DataManager.mHeroDict[heroTypeID].WorkPlanting; break;
            case Attribute.WorkFeeding: rank = DataManager.mHeroDict[heroTypeID].WorkFeeding; break;
            case Attribute.WorkFishing: rank = DataManager.mHeroDict[heroTypeID].WorkFishing; break;
            case Attribute.WorkHunting: rank = DataManager.mHeroDict[heroTypeID].WorkHunting; break;
            case Attribute.WorkFelling: rank = DataManager.mHeroDict[heroTypeID].WorkFelling; break;
            case Attribute.WorkQuarrying: rank = DataManager.mHeroDict[heroTypeID].WorkQuarrying; break;
            case Attribute.WorkMining: rank = DataManager.mHeroDict[heroTypeID].WorkMining; break;
            case Attribute.WorkBuild: rank = DataManager.mHeroDict[heroTypeID].WorkBuild; break;
            case Attribute.WorkMakeWeapon: rank = DataManager.mHeroDict[heroTypeID].WorkMakeWeapon; break;
            case Attribute.WorkMakeArmor: rank = DataManager.mHeroDict[heroTypeID].WorkMakeArmor; break;
            case Attribute.WorkMakeJewelry: rank = DataManager.mHeroDict[heroTypeID].WorkMakeJewelry; break;
            case Attribute.WorkMakeScroll: rank = DataManager.mHeroDict[heroTypeID].WorkMakeScroll; break;
            case Attribute.WorkSundry: rank = DataManager.mHeroDict[heroTypeID].WorkSundry; break;

            case Attribute.WindDam: rank = DataManager.mHeroDict[heroTypeID].WindDam; break;
            case Attribute.FireDam: rank = DataManager.mHeroDict[heroTypeID].FireDam; break;
            case Attribute.WaterDam: rank = DataManager.mHeroDict[heroTypeID].WaterDam; break;
            case Attribute.GroundDam: rank = DataManager.mHeroDict[heroTypeID].GroundDam; break;
            case Attribute.LightDam: rank = DataManager.mHeroDict[heroTypeID].LightDam; break;
            case Attribute.DarkDam: rank = DataManager.mHeroDict[heroTypeID].DarkDam; break;

            case Attribute.WindRes: rank = DataManager.mHeroDict[heroTypeID].WindRes; break;
            case Attribute.FireRes: rank = DataManager.mHeroDict[heroTypeID].FireRes; break;
            case Attribute.WaterRes: rank = DataManager.mHeroDict[heroTypeID].WaterRes; break;
            case Attribute.GroundRes: rank = DataManager.mHeroDict[heroTypeID].GroundRes; break;
            case Attribute.LightRes: rank = DataManager.mHeroDict[heroTypeID].LightRes; break;
            case Attribute.DarkRes: rank = DataManager.mHeroDict[heroTypeID].DarkRes; break;

            default:
                rank = 999; break;
        }

        int probabilityCount = DataManager.cCreateHeroRankDict[attr].probability.GetLength(1);

        int ran = Random.Range(0, 100);
        int lj = 0;
        for (int i = 0; i < probabilityCount; i++)
        {
            lj += DataManager.cCreateHeroRankDict[attr].probability[rank, i];

            if (ran < lj)
            {
                return Random.Range(DataManager.cCreateHeroRankDict[attr].value1[i], DataManager.cCreateHeroRankDict[attr].value2[i]);
            }
        }

        return 0;
    }

    void HeroGetExp(int heroID, int exp)
    {
        heroDic[heroID].exp += exp;

        int levelupNeedExp = (int)System.Math.Pow(1.05f, heroDic[heroID].level) * 200;
        while (heroDic[heroID].exp >= levelupNeedExp)
        {
            heroDic[heroID].exp -= levelupNeedExp;
            heroDic[heroID].level++;
            HeroLevelUp(heroID);
            levelupNeedExp = (int)System.Math.Pow(1.05f, heroDic[heroID].level) * 200;
        }
    }
    void HeroLevelUp(int heroID)
    {
        float groupRate = heroDic[heroID].groupRate - 1f;

        heroDic[heroID].hp += heroDic[heroID].baseHp * groupRate;
        heroDic[heroID].mp += heroDic[heroID].baseMp * groupRate;
        heroDic[heroID].atkMin += heroDic[heroID].baseAtkMin * groupRate;
        heroDic[heroID].atkMax += heroDic[heroID].baseAtkMax * groupRate;
        heroDic[heroID].mAtkMin += heroDic[heroID].baseMAtkMin * groupRate;
        heroDic[heroID].mAtkMax += heroDic[heroID].baseMAtkMax * groupRate;
        heroDic[heroID].def += heroDic[heroID].baseDef * groupRate;
        heroDic[heroID].mDef += heroDic[heroID].baseMDef * groupRate;
        heroDic[heroID].hit += heroDic[heroID].baseHit * groupRate;
        heroDic[heroID].dod += heroDic[heroID].baseDod * groupRate;
        heroDic[heroID].criR += heroDic[heroID].baseCriR * groupRate;

        MessagePanel.Instance.AddMessage(heroDic[heroID].name + "等级升级到Lv." + heroDic[heroID].level + "，能力值提升了");
    }
    void HeroSkillGetExp(int heroID, short spid, int exp)
    {


        if (!heroDic[heroID].skillInfo.ContainsKey(spid))
        {
            heroDic[heroID].skillInfo.Add(spid, new HeroSkill(spid, 1, 0));
        }

        if (heroDic[heroID].skillInfo[spid].level >= 10)
        {
            return;
        }
        heroDic[heroID].skillInfo[spid].exp += exp;
        int levelupNeedExp = heroDic[heroID].skillInfo[spid].level * 200;
        while (heroDic[heroID].skillInfo[spid].exp >= levelupNeedExp && heroDic[heroID].skillInfo[spid].level < 10)
        {
            heroDic[heroID].skillInfo[spid].exp -= levelupNeedExp;
            heroDic[heroID].skillInfo[spid].level++;
            MessagePanel.Instance.AddMessage(heroDic[heroID].name + "的技能 " + DataManager.mSkillDict[spid].Name + " 升级到Lv." + heroDic[heroID].skillInfo[spid].level);
            levelupNeedExp = heroDic[heroID].skillInfo[spid].level * 200;
        }

    }

    public void HeroRecruit(int buildingID,int heroID,short forceID)
    {
        if (heroDic[heroID].salary > forceDic[forceID].gold)
        {
            MessagePanel.Instance.AddMessage("金币不足，无法招募/解雇。（需要："+ heroDic[heroID].salary +" 当前：" + forceDic[forceID].gold + "）");
            return;
        }


        short oldForceID = heroDic[heroID].force;

        heroDic[heroID].force = forceID;

        districtDic[buildingDic[buildingID].districtID].recruitList.Remove(heroID);
        districtDic[buildingDic[buildingID].districtID].heroList.Add(heroID);

        if (oldForceID == 0)
        {
            MessagePanel.Instance.AddMessage("解雇了"+ heroDic[heroID].name);

        }
        else if(forceID == 0)
        {
            MessagePanel.Instance.AddMessage("招募了" + heroDic[heroID].name);
            if (BuildingPanel.Instance.isShow&& BuildingPanel.Instance.IsShowRecruitPart && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                BuildingPanel.Instance.UpdateSceneRolePic(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateRecruitPart(buildingDic[buildingID]);
            }

            AreaMapPanel.Instance.UpdateDistrictSingle(buildingDic[buildingID].districtID);

            PlayMainPanel.Instance.UpdateGold();
        }
       


        PlayMainPanel.Instance.UpdateButtonHeroNum();
        HeroPanel.Instance.OnHide();
    }

    //结算英雄工资（全部势力）
    public void PayHeroSalary()
    {
        int count = 0;
        int salary;
        foreach (KeyValuePair<int, HeroObject> kvp in heroDic)
        {
            if (kvp.Value.force != -1)
            {
                salary = kvp.Value.salary;
                if (forceDic[kvp.Value.force].gold < salary)
                {
                    salary = forceDic[kvp.Value.force].gold;
                }
                forceDic[kvp.Value.force].gold -= salary;

                if (kvp.Value.force == 0)
                {
                    count += salary;
                }
                
            }
        }

        PlayMainPanel.Instance.UpdateGold();
        MessagePanel.Instance.AddMessage("支付部属英雄的薪金合计" + count + "金币");
    }


    #endregion

    #region 【方法】生成装备物品，镶嵌，强化
    public ItemObject GenerateItemByRandom(int itemID, DistrictObject districtObject, int buildingID, List<int> heroObjectIDList)
    {
        //随机提升等级，每个等级提升基础数据5%，上限5
        byte upLevel = 0;
        int ran;

        for (int i = 0; i < 5; i++)
        {
            ran = Random.Range(0, 100);
            if (ran < 20)
            {
                upLevel++;
            }
            else
            {
                break;
            }
        }
        float upRate = 1f + upLevel * 0.05f;
        string name = DataManager.mItemDict[itemID].Name/* + (upLevel > 0 ? " +" + upLevel : "")*/;


        List<ItemAttribute> attrList = new List<ItemAttribute> { };

        //模板基础属性及等级修正
        if (DataManager.mItemDict[itemID].Hp != 0) { attrList.Add(new ItemAttribute(Attribute.Hp, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].Hp * upRate))); }
        if (DataManager.mItemDict[itemID].Mp != 0) { attrList.Add(new ItemAttribute(Attribute.Mp, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].Mp * upRate))); }
        if (DataManager.mItemDict[itemID].HpRenew != 0) { attrList.Add(new ItemAttribute(Attribute.HpRenew, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].HpRenew * upRate))); }
        if (DataManager.mItemDict[itemID].MpRenew != 0) { attrList.Add(new ItemAttribute(Attribute.MpRenew, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].AtkMax * upRate))); }

        if (DataManager.mItemDict[itemID].AtkMax != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMax, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].AtkMax * upRate))); }
        if (DataManager.mItemDict[itemID].AtkMin != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMin, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].AtkMin * upRate))); }
        if (DataManager.mItemDict[itemID].MAtkMax != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMax, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].MAtkMax * upRate))); }
        if (DataManager.mItemDict[itemID].MAtkMin != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMin, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].MAtkMin * upRate))); }

        if (DataManager.mItemDict[itemID].Def != 0) { attrList.Add(new ItemAttribute(Attribute.Def, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].Def * upRate))); }
        if (DataManager.mItemDict[itemID].MDef != 0) { attrList.Add(new ItemAttribute(Attribute.MDef, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].MDef * upRate))); }

        if (DataManager.mItemDict[itemID].Hit != 0) { attrList.Add(new ItemAttribute(Attribute.Hit, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].Hit * upRate))); }
        if (DataManager.mItemDict[itemID].Dod != 0) { attrList.Add(new ItemAttribute(Attribute.Dod, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].Dod * upRate))); }
        if (DataManager.mItemDict[itemID].CriR != 0) { attrList.Add(new ItemAttribute(Attribute.CriR, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].CriR * upRate))); }
        if (DataManager.mItemDict[itemID].CriD != 0) { attrList.Add(new ItemAttribute(Attribute.CriD, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].CriD * upRate))); }
        if (DataManager.mItemDict[itemID].Spd != 0) { attrList.Add(new ItemAttribute(Attribute.Spd, AttributeSource.Basic, 0, -1, AttributeSkill.None, DataManager.mItemDict[itemID].Spd)); }

        if (DataManager.mItemDict[itemID].WindDam != 0) { attrList.Add(new ItemAttribute(Attribute.WindDam, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].WindDam * upRate))); }
        if (DataManager.mItemDict[itemID].FireDam != 0) { attrList.Add(new ItemAttribute(Attribute.FireDam, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].FireDam * upRate))); }
        if (DataManager.mItemDict[itemID].WaterDam != 0) { attrList.Add(new ItemAttribute(Attribute.WaterDam, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].WaterDam * upRate))); }
        if (DataManager.mItemDict[itemID].GroundDam != 0) { attrList.Add(new ItemAttribute(Attribute.GroundDam, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].GroundDam * upRate))); }
        if (DataManager.mItemDict[itemID].LightDam != 0) { attrList.Add(new ItemAttribute(Attribute.LightDam, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].LightDam * upRate))); }
        if (DataManager.mItemDict[itemID].DarkDam != 0) { attrList.Add(new ItemAttribute(Attribute.DarkDam, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].DarkDam * upRate))); }

        if (DataManager.mItemDict[itemID].WindRes != 0) { attrList.Add(new ItemAttribute(Attribute.WindRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].WindRes * upRate))); }
        if (DataManager.mItemDict[itemID].FireRes != 0) { attrList.Add(new ItemAttribute(Attribute.FireRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].FireRes * upRate))); }
        if (DataManager.mItemDict[itemID].WaterRes != 0) { attrList.Add(new ItemAttribute(Attribute.WaterRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].WaterRes * upRate))); }
        if (DataManager.mItemDict[itemID].GroundRes != 0) { attrList.Add(new ItemAttribute(Attribute.GroundRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].GroundRes * upRate))); }
        if (DataManager.mItemDict[itemID].LightRes != 0) { attrList.Add(new ItemAttribute(Attribute.LightRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].LightRes * upRate))); }
        if (DataManager.mItemDict[itemID].DarkRes != 0) { attrList.Add(new ItemAttribute(Attribute.DarkRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].DarkRes * upRate))); }

        if (DataManager.mItemDict[itemID].DizzyRes != 0) { attrList.Add(new ItemAttribute(Attribute.DizzyRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].DizzyRes * upRate))); }
        if (DataManager.mItemDict[itemID].ConfusionRes != 0) { attrList.Add(new ItemAttribute(Attribute.ConfusionRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].ConfusionRes * upRate))); }
        if (DataManager.mItemDict[itemID].PoisonRes != 0) { attrList.Add(new ItemAttribute(Attribute.PoisonRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].PoisonRes * upRate))); }
        if (DataManager.mItemDict[itemID].SleepRes != 0) { attrList.Add(new ItemAttribute(Attribute.SleepRes, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].SleepRes * upRate))); }

        if (DataManager.mItemDict[itemID].ExpGet != 0) { attrList.Add(new ItemAttribute(Attribute.ExpGet, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].ExpGet * upRate))); }
        if (DataManager.mItemDict[itemID].GoldGet != 0) { attrList.Add(new ItemAttribute(Attribute.GoldGet, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].GoldGet * upRate))); }
        if (DataManager.mItemDict[itemID].ItemGet != 0) { attrList.Add(new ItemAttribute(Attribute.ItemGet, AttributeSource.Basic, 0, -1, AttributeSkill.None, (int)(DataManager.mItemDict[itemID].ItemGet * upRate))); }

        //追加词条
        byte rank = (byte)(DataManager.mItemDict[itemID].Rank - 1);
        int lemmaCount = 0;
        ran = Random.Range(0, 100);
        upRate = 1f + Random.Range(0f, 0.2f);
        if (ran < 10)//10%概率2词条
        {
            lemmaCount = 2;
        }
        else if (ran >= 10 && ran < 30)//20%概率1词条
        {
            lemmaCount = 1;
        }
        for (int i = 0; i < lemmaCount; i++)
        {
            int lemmaID = Random.Range(0, DataManager.mLemmaDict.Count);
            name = DataManager.mLemmaDict[lemmaID].Name + "的 " + name;

            // Debug.Log("lemmaID=" + lemmaID+ " rank="+ rank);


            if (DataManager.mLemmaDict[lemmaID].Hp.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Hp, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].Hp[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Mp.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Mp, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].Mp[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].HpRenew.Count != 0) { attrList.Add(new ItemAttribute(Attribute.HpRenew, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].HpRenew[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MpRenew.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MpRenew, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].MpRenew[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].AtkMax.Count != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMax, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].AtkMax[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].AtkMin.Count != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMin, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].AtkMin[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MAtkMax.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMax, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].MAtkMax[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MAtkMin.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMin, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].MAtkMin[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].Def.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Def, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].Def[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MDef.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MDef, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].MDef[rank] * upRate))); }


            if (DataManager.mLemmaDict[lemmaID].Hit.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Hit, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].Hit[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Dod.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Dod, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].Dod[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].CriR.Count != 0) { attrList.Add(new ItemAttribute(Attribute.CriR, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].CriR[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].CriD.Count != 0) { attrList.Add(new ItemAttribute(Attribute.CriD, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].CriD[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Spd.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Spd, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].Spd[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].WindDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.WindDam, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].WindDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].FireDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.FireDam, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].FireDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].WaterDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.WaterDam, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].WaterDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GroundDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.GroundDam, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].GroundDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].LightDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.LightDam, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].LightDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].DarkDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.DarkDam, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].DarkDam[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].WindRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.WindRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].WindRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].FireRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.FireRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].FireRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].WaterRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.WaterRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].WaterRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GroundRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.GroundRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].GroundRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].LightRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.LightRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].LightRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].DarkRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.DarkRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].DarkRes[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].DizzyRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.DizzyRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].DizzyRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].ConfusionRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.ConfusionRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].ConfusionRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].PoisonRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.PoisonRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].PoisonRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].SleepRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.SleepRes, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].SleepRes[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].ExpGet.Count != 0) { attrList.Add(new ItemAttribute(Attribute.ExpGet, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].ExpGet[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GoldGet.Count != 0) { attrList.Add(new ItemAttribute(Attribute.GoldGet, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].GoldGet[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].ItemGet.Count != 0) { attrList.Add(new ItemAttribute(Attribute.ItemGet, AttributeSource.LemmaAdd, 0, -1, AttributeSkill.None, (int)(DataManager.mLemmaDict[lemmaID].ItemGet[rank] * upRate))); }

        }

        //技能强化词条
        ran = Random.Range(0, 100);
        if (ran < 10)//10%概率
        {

            short skillID = (short)Random.Range(0, DataManager.mSkillDict.Count);
            AttributeSkill skillAddType;
            int value;
            ran = Random.Range(0, 100);

            if (DataManager.mSkillDict[skillID].FlagDamage)
            {
                if (ran < 20)
                {
                    skillAddType = AttributeSkill.Damage;
                    value = Random.Range(10, 20);
                }
                else if (ran >= 20 && ran < 35)
                {
                    skillAddType = AttributeSkill.CriDamage;
                    value = Random.Range(20, 50);
                }
                else if (ran >= 35 && ran < 45)
                {
                    skillAddType = AttributeSkill.Probability;
                    value = Random.Range(5, 10);
                }
                else if (ran >= 45 && ran < 55)
                {
                    skillAddType = AttributeSkill.CostMp;
                    value = Random.Range(10, 30);
                }
                else if (ran >= 55 && ran < 60)
                {
                    skillAddType = AttributeSkill.IgnoreDef;
                    value = Random.Range(15, 30);
                }
                else if (ran >= 60 && ran < 75)
                {
                    skillAddType = AttributeSkill.IgnoreMDef;
                    value = Random.Range(15, 30);
                }
                else if (ran >= 75 && ran < 85)
                {
                    skillAddType = AttributeSkill.SuckHp;
                    value = Random.Range(10, 30);
                }
                else if (ran >= 85 && ran < 90)
                {
                    skillAddType = AttributeSkill.SuckMp;
                    value = Random.Range(1, 5);
                }
                else if (ran >= 90 && ran < 95)
                {
                    skillAddType = AttributeSkill.TargetNum;
                    value = Random.Range(1, 5);
                }
                else
                {
                    skillAddType = AttributeSkill.Invincible;
                    value = Random.Range(10, 15);
                }
            }
            else if (DataManager.mSkillDict[skillID].Cure > 0)
            {
                if (ran < 60)
                {
                    skillAddType = AttributeSkill.Damage;
                    value = Random.Range(10, 20);
                }
                else if (ran >= 60 && ran < 70)
                {
                    skillAddType = AttributeSkill.Probability;
                    value = Random.Range(5, 10);
                }
                else if (ran >= 70 && ran < 85)
                {
                    skillAddType = AttributeSkill.CostMp;
                    value = Random.Range(10, 30);
                }
                else if (ran >= 85 && ran < 95)
                {
                    skillAddType = AttributeSkill.Ap;
                    value = 50;
                }
                else
                {
                    skillAddType = AttributeSkill.Invincible;
                    value = Random.Range(10, 15);
                }
            }
            else
            {
                if (ran < 70)
                {
                    skillAddType = AttributeSkill.Probability;
                    value = Random.Range(5, 10);
                }
                else if (ran >= 70 && ran < 95)
                {
                    skillAddType = AttributeSkill.CostMp;
                    value = Random.Range(10, 30);
                }
                else
                {
                    skillAddType = AttributeSkill.Invincible;
                    value = Random.Range(10, 15);
                }
            }

            attrList.Add(new ItemAttribute(Attribute.Skill, AttributeSource.LemmaAdd, 0, skillID, skillAddType, value));
        }

        //插孔 最多3
        List<byte> slotLevel = new List<byte> { };
        List<short> slotItemID = new List<short> { };
        ran = Random.Range(0, 100);
        if (ran < 20)//10%概率
        {
            slotLevel.Add((byte)Random.Range(1, 5));
            slotItemID.Add(-1);
        }
        else if (ran >= 20 && ran < 50)
        {
            slotLevel.Add((byte)Random.Range(1, 5));
            slotItemID.Add(-1);
            slotLevel.Add((byte)Random.Range(1, 5));
            slotItemID.Add(-1);
        }
        else if (ran >= 50 && ran < 95)
        {
            slotLevel.Add((byte)Random.Range(1, 5));
            slotItemID.Add(-1);
            slotLevel.Add((byte)Random.Range(1, 5));
            slotItemID.Add(-1);
            slotLevel.Add((byte)Random.Range(1, 5));
            slotItemID.Add(-1);
        }

        Debug.Log("slotLevel.Count=" + slotLevel.Count);
        //TODO:依据建筑附加材料生成属性



        return new ItemObject(itemIndex, itemID, name, DataManager.mItemDict[itemID].Pic, DataManager.mItemDict[itemID].Rank, upLevel, attrList, slotLevel, slotItemID,
            DataManager.mItemDict[itemID].Des + (",于" + timeYear + "年" + timeMonth + "月" + (districtObject != null ? ("在" + districtObject.name + "制作") : "获得")), DataManager.mItemDict[itemID].Cost, districtObject != null ? districtObject.id : (short)-1, false, -1, EquipPart.None);
    }

    public void EquipmentStrengthen(int itemID,short consumableID)
    {
        if (consumableID == -1)
        {
            MessagePanel.Instance.AddMessage("未选择强化材料");
            return;
        }
        if (consumableNum[consumableID]<=0)
        {
            MessagePanel.Instance.AddMessage("强化材料不足");
            return;
        }
        if (forceDic[0].gold< (itemDic[itemID].level+1)*100)
        {
            MessagePanel.Instance.AddMessage("金钱不足");
            return;
        }

        int successRate = DataManager.mConsumableDict[consumableID].Value[itemDic[itemID].level];
        int ran = Random.Range(0, 100);
        if (ran < successRate)
        {
            itemDic[itemID].level++;
            float upRate = 1f + itemDic[itemID].level * 0.05f;

            for (int i = 0; i < itemDic[itemID].attr.Count; i++)
            {
                if (itemDic[itemID].attr[i].attrS == AttributeSource.Basic)
                {
                    switch (itemDic[itemID].attr[i].attr)
                    {
                        case Attribute.Hp: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].Hp * upRate); break;
                        case Attribute.Mp: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].Mp * upRate); break;
                        case Attribute.HpRenew: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].HpRenew * upRate); break;
                        case Attribute.MpRenew: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].MpRenew * upRate); break;
                        case Attribute.AtkMax: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].AtkMax * upRate); break;
                        case Attribute.AtkMin: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].AtkMin * upRate); break;
                        case Attribute.MAtkMax: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].MAtkMax * upRate); break;
                        case Attribute.MAtkMin: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].MAtkMin * upRate); break;
                        case Attribute.Def: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].Def * upRate); break;
                        case Attribute.MDef: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].MDef * upRate); break;
                        case Attribute.Hit: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].Hit * upRate); break;
                        case Attribute.Dod: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].Dod * upRate); break;
                        case Attribute.CriR: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].CriR * upRate); break;
                        case Attribute.CriD: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].CriD * upRate); break;
                        case Attribute.WindDam: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].WindDam * upRate); break;
                        case Attribute.FireDam: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].FireDam * upRate); break;
                        case Attribute.WaterDam: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].WaterDam * upRate); break;
                        case Attribute.GroundDam: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].GroundDam * upRate); break;
                        case Attribute.LightDam: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].LightDam * upRate); break;
                        case Attribute.DarkDam: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].DarkDam * upRate); break;
                        case Attribute.WindRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].WindRes * upRate); break;
                        case Attribute.FireRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].FireRes * upRate); break;
                        case Attribute.WaterRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].WaterRes * upRate); break;
                        case Attribute.GroundRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].GroundRes * upRate); break;
                        case Attribute.LightRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].LightRes * upRate); break;
                        case Attribute.DarkRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].DarkRes * upRate); break;
                        case Attribute.DizzyRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].DizzyRes * upRate); break;
                        case Attribute.ConfusionRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].ConfusionRes * upRate); break;
                        case Attribute.PoisonRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].PoisonRes * upRate); break;
                        case Attribute.SleepRes: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].SleepRes * upRate); break;
                        case Attribute.ExpGet: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].ExpGet * upRate); break;
                        case Attribute.GoldGet: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].GoldGet * upRate); break;
                        case Attribute.ItemGet: itemDic[itemID].attr[i].value = (int)(DataManager.mItemDict[itemDic[itemID].prototypeID].ItemGet * upRate); break;
                    }
                }
            }

            BuildingPanel.Instance.ShowEffect(BuildingPanel.Instance.strengthen_targetEffectGo, "FX003");
            MessagePanel.Instance.AddMessage(itemDic[itemID].name+"强化成功(+"+(itemDic[itemID].level-1)+"→+"+ itemDic[itemID].level+")");
            AudioControl.Instance.PlaySound("system_success");
        }
        else
        {
            BuildingPanel.Instance.ShowEffect(BuildingPanel.Instance.strengthen_targetEffectGo, "FX001");
            MessagePanel.Instance.AddMessage(itemDic[itemID].name + "强化失败");
            AudioControl.Instance.PlaySound("system_fail");
        }

        ConsumableChange(consumableID, -1);

        forceDic[0].gold -= (itemDic[itemID].level + 1) * 100;
        PlayMainPanel.Instance.UpdateGold();

        BuildingPanel.Instance.UpdateStrengthenPart(buildingDic[BuildingPanel.Instance.nowCheckingBuildingID]);
    }


    public void EquipmentInlay(int itemID,byte slotIndex, short consumableID)
    {
        if (itemDic[itemID].slotItemID[slotIndex]!=-1)
        {
            MessagePanel.Instance.AddMessage("孔槽已嵌有晶石，请移除后在进行操作");
            return;
        }
        if (itemDic[itemID].slotLevel[slotIndex] < DataManager.mConsumableDict[consumableID].SlotLevel)
        {
            MessagePanel.Instance.AddMessage("孔槽无法嵌入高级晶石");
            return;
        }
        if (forceDic[0].gold < 100)
        {
            MessagePanel.Instance.AddMessage("金钱不足");
            return;
        }

        itemDic[itemID].slotItemID[slotIndex] = consumableID;
        for (int i = 0; i < DataManager.mConsumableDict[consumableID].AttributeType.Count; i++)
        {
            itemDic[itemID].attr.Add(new ItemAttribute(DataManager.mConsumableDict[consumableID].AttributeType[i], AttributeSource.SlotAdd, slotIndex, DataManager.mConsumableDict[consumableID].SkillID[i], DataManager.mConsumableDict[consumableID].SkillAddType[i], DataManager.mConsumableDict[consumableID].Value[i]));
        }


        ConsumableChange(consumableID, -1);
        forceDic[0].gold -= 100;
        PlayMainPanel.Instance.UpdateGold();
        //BuildingPanel.Instance.inlayItemID[slotIndex] = -1;
        BuildingPanel.Instance.UpdateInlayPart(buildingDic[BuildingPanel.Instance.nowCheckingBuildingID]);
        AudioControl.Instance.PlaySound("system_inlay");
    }

    public void EquipmentUnlay(int itemID, byte slotIndex)
    {
        Debug.Log("EquipmentUnlay() itemID=" + itemID + " slotIndex=" + slotIndex);
        if (itemDic[itemID].slotItemID[slotIndex] == -1)
        {
            MessagePanel.Instance.AddMessage("孔槽为空，卸下失败");
            return;
        }
        if (forceDic[0].gold < 100)
        {
            MessagePanel.Instance.AddMessage("金钱不足");
            return;
        }

        ConsumableChange(itemDic[itemID].slotItemID[slotIndex], 1);

        for (int i = itemDic[itemID].attr.Count-1; i >=0 ; i--)
        {
            if (itemDic[itemID].attr[i].attrS == AttributeSource.SlotAdd && itemDic[itemID].attr[i].slotIndex == slotIndex)
            {
                itemDic[itemID].attr.RemoveAt(i);
            }
        }

        itemDic[itemID].slotItemID[slotIndex] = -1;

        forceDic[0].gold -= 100;
        PlayMainPanel.Instance.UpdateGold();

        BuildingPanel.Instance.inlayItemID[slotIndex] = -1;
        BuildingPanel.Instance.UpdateInlayPart(buildingDic[BuildingPanel.Instance.nowCheckingBuildingID]);
        AudioControl.Instance.PlaySound("system_inlay");
    }
    #endregion

    #region 【方法】消耗品道具
    public void ConsumableChange(short consumableID, int num)
    {
        if (consumableNum[consumableID] + num < 0)
        {
            MessagePanel.Instance.AddMessage("数量不足");
            return;
        }
        consumableNum[consumableID] += num;

        if (ConsumableListAndInfoPanel.Instance.isShow)
        {
            ConsumableListAndInfoPanel.Instance.UpdateList( ConsumableType.None,-1);
            ConsumableListAndInfoPanel.Instance.UpdateInfo(ConsumableListAndInfoPanel.Instance.nowItemID);
        }
        PlayMainPanel.Instance.UpdateButtonConsumableNum();
    }
    #endregion

    #region 【方法】生成技能
    public SkillObject GenerateSkillByRandom(short skillID, short districtID)
    {
        string name = DataManager.mSkillDict[skillID].Name;
        short RateModify = 0;
        short MpModify = 0;
        byte ComboRate = 0;
        byte ComboMax = 0;
        byte Gold = 0;


        int lemmaCount = 0;
        int ran = Random.Range(0, 100);
        if (ran <= 10)//10%概率2词条
        {
            lemmaCount = 2;
        }
        else if (ran > 10 && ran <= 30)//20%概率1词条
        {
            lemmaCount = 1;
        }

        List<int> typePool = new List<int> { 0, 1, 2, 3 };

        for (int i = 0; i < lemmaCount; i++)
        {
            int ranType = Random.Range(0, typePool.Count);
            int type = typePool[ranType];
            typePool.Remove(ranType);
            switch (type)
            {
                case 0://发动几率修正

                    while (RateModify == 0)
                    {
                        RateModify = (short)Random.Range(-10, 11);
                    }
                    if (RateModify > 0)
                    {
                        name = "积极 " + name;
                    }
                    else
                    {
                        name = "消极 " + name;
                    }

                    break;
                case 1://发耗蓝修正 对原本mp的%修正
                    while (MpModify == 0)
                    {
                        MpModify = (short)Random.Range(-30, 30);
                    }
                    if (MpModify > 0)
                    {
                        name = "奢侈 " + name;
                    }
                    else
                    {
                        name = "节约 " + name;
                    }

                    break;
                case 2://追击
                    ComboRate = (byte)Random.Range(1, 21);
                    ComboMax = (byte)Random.Range(1, 5);

                    if (ComboRate < 5)
                    {
                        name = "快速 " + name;
                    }
                    else if (ComboRate >= 5 && ComboRate < 10)
                    {
                        name = "迅速 " + name;
                    }
                    else if (ComboRate >= 10 && ComboRate < 15)
                    {
                        name = "急速 " + name;
                    }
                    else
                    {
                        name = "极速 " + name;
                    }
                    switch (ComboMax)
                    {
                        case 1: name = "二连" + name; break;
                        case 2: name = "三连" + name; break;
                        case 3: name = "四连" + name; break;
                        case 4: name = "五连" + name; break;
                    }


                    break;
                case 3://攻击得金

                    Gold = (byte)Random.Range(1, 21);
                    name = "夺金 " + name;
                    break;
            }
        }

        return new SkillObject(skillIndex, name, skillID, RateModify, MpModify, ComboRate, ComboMax, Gold, 5000, districtID, false, -1, 0);

    }
    public SkillObject GenerateSkillByOriginal(short skillID)
    {
        return new SkillObject(skillIndex, DataManager.mSkillDict[skillID].Name, skillID, 0, 0, 0, 0, 0, 5000, -2, false, -1, 0);
    }
    #endregion

    #region 【方法】建筑物建设
    public void BuildDone(short buildingId)
    {
        buildingDic[buildingId].buildProgress = 1;
        if (buildingDic[buildingId].panelType == "Resource")
        {
            buildingDic[buildingId].produceEquipNow = buildingDic[buildingId].prototypeID;
        }

        short prototypeID = buildingDic[buildingId].prototypeID;
        districtDic[nowCheckingDistrictID].eWind += DataManager.mBuildingDict[prototypeID].EWind;
        districtDic[nowCheckingDistrictID].eFire += DataManager.mBuildingDict[prototypeID].EFire;
        districtDic[nowCheckingDistrictID].eWater += DataManager.mBuildingDict[prototypeID].EWater;
        districtDic[nowCheckingDistrictID].eGround += DataManager.mBuildingDict[prototypeID].EGround;
        districtDic[nowCheckingDistrictID].eLight += DataManager.mBuildingDict[prototypeID].ELight;
        districtDic[nowCheckingDistrictID].eDark += DataManager.mBuildingDict[prototypeID].EDark;

        districtDic[nowCheckingDistrictID].peopleLimit += DataManager.mBuildingDict[prototypeID].People;

        if (prototypeID == 47)
        {
            forceDic[0].rFoodLimit += 1000;
        }
        else if (prototypeID == 48)
        {
            forceDic[0].rStuffLimit += 1000;
        }
        else if (prototypeID == 49)
        {
            districtDic[nowCheckingDistrictID].rProductLimit += 200;
        }


        //AreaMapPanel.Instance.AddIconByBuilding(buildingId);
        if (DistrictMainPanel.Instance.isShow && buildingDic[buildingId].districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateBasicInfo(districtDic[nowCheckingDistrictID]);
            //DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdatePeopleInfo(districtDic[nowCheckingDistrictID]);
            //DistrictMainPanel.Instance.UpdateBuildingInfo(districtDic[nowCheckingDistrictID]);
        }
        if (DistrictMapPanel.Instance.isShow && buildingDic[buildingId].districtID == nowCheckingDistrictID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingId);
            DistrictMapPanel.Instance.UpdateBasicInfo();
            DistrictMapPanel.Instance.UpdateBaselineElementText(nowCheckingDistrictID);
        }
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingId)
        {
            BuildingPanel.Instance.OnShow(buildingDic[buildingId]);
        }
        PlayMainPanel.Instance.UpdateResources();

        MessagePanel.Instance.AddMessage(districtDic[buildingDic[buildingId].districtID].name + "的" + buildingDic[buildingId].name + "建筑完成");

    }

    public void BuildingUpgradeDone(short buildingId)
    {
        buildingDic[buildingId].buildProgress = 1;
        if (buildingDic[buildingId].panelType == "Resource")
        {
            buildingDic[buildingId].produceEquipNow = buildingDic[buildingId].prototypeID;
        }

        short prototypeID = buildingDic[buildingId].prototypeID;
        districtDic[nowCheckingDistrictID].eWind += DataManager.mBuildingDict[prototypeID].EWind;
        districtDic[nowCheckingDistrictID].eFire += DataManager.mBuildingDict[prototypeID].EFire;
        districtDic[nowCheckingDistrictID].eWater += DataManager.mBuildingDict[prototypeID].EWater;
        districtDic[nowCheckingDistrictID].eGround += DataManager.mBuildingDict[prototypeID].EGround;
        districtDic[nowCheckingDistrictID].eLight += DataManager.mBuildingDict[prototypeID].ELight;
        districtDic[nowCheckingDistrictID].eDark += DataManager.mBuildingDict[prototypeID].EDark;

        districtDic[nowCheckingDistrictID].peopleLimit += DataManager.mBuildingDict[prototypeID].People;
        //AreaMapPanel.Instance.RemoveIconByBuilding(buildingId);
        //AreaMapPanel.Instance.AddIconByBuilding(buildingId);
        if (DistrictMainPanel.Instance.isShow && buildingDic[buildingId].districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateBasicInfo(districtDic[nowCheckingDistrictID]);
            //DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdatePeopleInfo(districtDic[nowCheckingDistrictID]);
            //DistrictMainPanel.Instance.UpdateBuildingInfo(districtDic[nowCheckingDistrictID]);

        }
        if (DistrictMapPanel.Instance.isShow && buildingDic[buildingId].districtID == nowCheckingDistrictID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingId);
            DistrictMapPanel.Instance.UpdateBasicInfo();
            DistrictMapPanel.Instance.UpdateBaselineElementText(nowCheckingDistrictID);
        }

        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingId)
        {
            BuildingPanel.Instance.OnShow(buildingDic[buildingId]);
        }

        MessagePanel.Instance.AddMessage(districtDic[buildingDic[buildingId].districtID].name + "的" + buildingDic[buildingId].name + "升级完成");

    }

    void StartBuild(int districtID, int buildingID, int needTime)
    {
        //value0:地区实例ID value1:建筑实例ID 
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.Build, standardTime, standardTime + needTime, new List<List<int>> { new List<int> { districtID }, new List<int> { buildingID } }));
       
    }
    void StartBuildingUpgrade(int districtID, int buildingID, int needTime)
    {
        //value0:地区实例ID value1:建筑实例ID 
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.BuildingUpgrade, standardTime, standardTime + needTime, new List<List<int>> { new List<int> { districtID }, new List<int> { buildingID } }));
    }
    public bool CreateBuildEvent(short BuildingPrototypeID, short posX, short posY, byte layer, List<int> xList, List<int> yList)
    {
        //Debug.Log("CreateBuildEvent() BuildingPrototypeID=" + BuildingPrototypeID);
        Debug.Log("CreateBuildEvent()  初始");
        //再次判断是应对面板打开的时候，相关数据已经产生变化
        if (DataManager.mBuildingDict[BuildingPrototypeID].NeedWood > forceDic[0].rStuffWood)
        {
            BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
            return false;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NeedStone > forceDic[0].rStuffStone)
        {
            BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
            return false;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NeedMetal > forceDic[0].rStuffMetal)
        {
            BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
            return false;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NeedGold > forceDic[0].gold)
        {
            BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
            return false;
        }
        Debug.Log("CreateBuildEvent()  符合条件");
        short buildingId = BuildingPrototypeID;

        List<string> grid = new List<string> { };
        for (int i = 0; i < xList.Count; i++)
        {
            districtGridDic[nowCheckingDistrictID][nowCheckingDistrictID + "_" + xList[i] + "," + yList[i]].buildingID = buildingIndex;
            grid.Add(nowCheckingDistrictID + "_" + xList[i] + "," + yList[i]);
        }


        forceDic[0].rStuffWood -= DataManager.mBuildingDict[buildingId].NeedWood;
        forceDic[0].rStuffStone -= DataManager.mBuildingDict[buildingId].NeedStone;
        forceDic[0].rStuffMetal -= DataManager.mBuildingDict[buildingId].NeedMetal;
        forceDic[0].gold -= DataManager.mBuildingDict[buildingId].NeedGold;

        districtDic[nowCheckingDistrictID].buildingList.Add(buildingIndex);

        //List<string> npcList;
        List<string> npcPicList = new List<string>();
        switch (DataManager.mBuildingDict[buildingId].BgPic)
        {
            case "DBG_HouseSmall_1":
            case "DBG_HouseSmall_2":
            case "DBG_HouseSmall_3":
                for (int i = 0; i < 4; i++)
                {
                    npcPicList.Add(DataManager.mNpcName[Random.Range(0, DataManager.mNpcName.Length - 1)]);
                }
                break;
            case "DBG_HouseMiddle_1":
            case "DBG_HouseMiddle_2":
            case "DBG_HouseMiddle_3":
            case "DBG_HouseBig_1":
            case "DBG_HouseBig_2":
            case "DBG_HouseBig_3":
                for (int i = 0; i < 6; i++)
                {
                    npcPicList.Add(DataManager.mNpcName[Random.Range(0, DataManager.mNpcName.Length - 1)]);
                }
                break;
            case "DBG_WheatField":
            case "DBG_VegetableField":
            case "DBG_Orchard":
            case "DBG_FlaxField":
                for (int i = 0; i < 6; i++)
                {
                    npcPicList.Add("npc_farmer1");
                }
                break;
            case "DBG_Lair":
                for (int i = 0; i < 3; i++)
                {
                    npcPicList.Add("npc_animal_" + (DataManager.mBuildingDict[buildingId].MainPic == "Lair" ? "cow" : "sheep") + Random.Range(1, 3));
                }
                for (int i = 3; i < 6; i++)
                {
                    npcPicList.Add("npc_farmer1");
                }
                break;
            case "DBG_Fishpond":
                for (int i = 0; i < 6; i++)
                {
                    npcPicList.Add("npc_other1_17");
                }
                break;
            case "DBG_Outside":
                for (int i = 0; i < 6; i++)
                {
                    npcPicList.Add("npc_other1_01");
                }
                break;
            case "DBG_IronMine":
            case "DBG_Quarry":
                for (int i = 0; i < 6; i++)
                {
                    npcPicList.Add("npc_other2_04");
                }
                break;

            case "DBG_Base1":
            case "DBG_Base2":
            case "DBG_Base3":
            case "DBG_Base4":
            case "DBG_Base5":
                npcPicList.Add("npc_knight" + forceDic[districtDic[nowCheckingDistrictID].force].flagIndex);
                npcPicList.Add("npc_knight" + forceDic[districtDic[nowCheckingDistrictID].force].flagIndex);
                break;
            case "DBG_WeaponShop":
                npcPicList.Add("npc_other1_24");
                for (int i = 0; i < 4; i++)
                {
                    npcPicList.Add("npc_blacksmith");
                }
                break;
            case "DBG_ArmorShop":
                npcPicList.Add("npc_other1_30");
                break;
            case "DBG_JewelryShop":
                npcPicList.Add("npc_other2_13");
                break;
            case "DBG_Warehouse":
                npcPicList.Add("npc_other2_10");
                break;
            case "DBG_Arena":
                npcPicList.Add("npc_other2_10");
                npcPicList.Add("npc_other1_07");
                npcPicList.Add("npc_other2_21");
                npcPicList.Add("npc_other2_17");
                npcPicList.Add("npc_other2_06");
                npcPicList.Add("npc_other2_02");
                npcPicList.Add("npc_other2_01");
                break;
            case "DBG_Monastery":
                npcPicList.Add("npc_other2_12");
                npcPicList.Add("npc_other2_15");
                for (int i = 0; i < 5; i++)
                {
                    npcPicList.Add(DataManager.mNpcName[Random.Range(0, DataManager.mNpcName.Length - 1)]);
                }
                break;
            case "DBG_Inn":
                npcPicList.Add("npc_other1_02");
                npcPicList.Add("npc_other1_23");
                break;
            case "DBG_ScrollShop":
                npcPicList.Add("npc_other2_08");
                break;
        }


        buildingDic.Add(buildingIndex, new BuildingObject(buildingIndex, buildingId, nowCheckingDistrictID, DataManager.mBuildingDict[buildingId].Name, DataManager.mBuildingDict[buildingId].MainPic, npcPicList, posX, posY, layer, posX > 64 ? AnimStatus.WalkLeft : AnimStatus.WalkRight, DataManager.mBuildingDict[buildingId].PanelType, DataManager.mBuildingDict[buildingId].Des, DataManager.mBuildingDict[buildingId].Level, DataManager.mBuildingDict[buildingId].Expense, DataManager.mBuildingDict[buildingId].UpgradeTo, false, false, grid, new List<int> { }, new List<int> { }, 
            DataManager.mBuildingDict[buildingId].People, DataManager.mBuildingDict[buildingId].Worker, 0,
            DataManager.mBuildingDict[buildingId].EWind, DataManager.mBuildingDict[buildingId].EFire, DataManager.mBuildingDict[buildingId].EWater, DataManager.mBuildingDict[buildingId].EGround, DataManager.mBuildingDict[buildingId].ELight, DataManager.mBuildingDict[buildingId].EDark,
            new List<BuildingTaskObject> { },-1, 0));


        int needTime = DataManager.mBuildingDict[BuildingPrototypeID].BuildTime * 10;
        StartBuild(nowCheckingDistrictID, buildingIndex, needTime);

        buildingIndex++;
        BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);



        if (PlayMainPanel.Instance.IsShowResourcesBlock)
        {
            PlayMainPanel.Instance.UpdateResourcesBlock();
        }

        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateResources();
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateBaselineResourcesText(nowCheckingDistrictID);
        }

        return true;
    }

    public void CreateBuildingUpgradeEvent(int buildingID)
    {
        short nowPrototypeID = buildingDic[buildingID].prototypeID;
        short newPrototypeID = DataManager.mBuildingDict[nowPrototypeID].UpgradeTo;


        forceDic[0].rStuffWood -= DataManager.mBuildingDict[newPrototypeID].NeedWood;
        forceDic[0].rStuffStone -= DataManager.mBuildingDict[newPrototypeID].NeedStone;
        forceDic[0].rStuffMetal -= DataManager.mBuildingDict[newPrototypeID].NeedMetal;
        forceDic[0].gold -= DataManager.mBuildingDict[newPrototypeID].NeedGold;



        districtDic[nowCheckingDistrictID].eWind -= DataManager.mBuildingDict[nowPrototypeID].EWind;
        districtDic[nowCheckingDistrictID].eFire -= DataManager.mBuildingDict[nowPrototypeID].EFire;
        districtDic[nowCheckingDistrictID].eWater -= DataManager.mBuildingDict[nowPrototypeID].EWater;
        districtDic[nowCheckingDistrictID].eGround -= DataManager.mBuildingDict[nowPrototypeID].EGround;
        districtDic[nowCheckingDistrictID].eLight -= DataManager.mBuildingDict[nowPrototypeID].ELight;
        districtDic[nowCheckingDistrictID].eDark -= DataManager.mBuildingDict[nowPrototypeID].EDark;

        districtDic[nowCheckingDistrictID].peopleLimit -= DataManager.mBuildingDict[nowPrototypeID].People;


        buildingDic[buildingID].prototypeID = newPrototypeID;
        buildingDic[buildingID].name = DataManager.mBuildingDict[newPrototypeID].Name;
        buildingDic[buildingID].mainPic = DataManager.mBuildingDict[newPrototypeID].MainPic;
        buildingDic[buildingID].panelType = DataManager.mBuildingDict[newPrototypeID].PanelType;
        buildingDic[buildingID].des = DataManager.mBuildingDict[newPrototypeID].Des;
        buildingDic[buildingID].level = DataManager.mBuildingDict[newPrototypeID].Level;
        buildingDic[buildingID].expense = DataManager.mBuildingDict[newPrototypeID].Expense;
        buildingDic[buildingID].upgradeTo = DataManager.mBuildingDict[newPrototypeID].UpgradeTo;
        buildingDic[buildingID].isOpen = false;
        buildingDic[buildingID].isSale = false;
        buildingDic[buildingID].people = DataManager.mBuildingDict[newPrototypeID].People;
        buildingDic[buildingID].worker = DataManager.mBuildingDict[newPrototypeID].Worker;
        buildingDic[buildingID].eWind = DataManager.mBuildingDict[newPrototypeID].EWind;
        buildingDic[buildingID].eFire = DataManager.mBuildingDict[newPrototypeID].EFire;
        buildingDic[buildingID].eWater = DataManager.mBuildingDict[newPrototypeID].EWater;
        buildingDic[buildingID].eGround = DataManager.mBuildingDict[newPrototypeID].EGround;
        buildingDic[buildingID].eLight = DataManager.mBuildingDict[newPrototypeID].ELight;
        buildingDic[buildingID].eDark = DataManager.mBuildingDict[newPrototypeID].EDark;
        // buildingDic[buildingID].produceEquipNow = -1;
        buildingDic[buildingID].buildProgress = 2;




        int needTime = DataManager.mBuildingDict[newPrototypeID].BuildTime * 10;

        if (buildingDic[buildingID].isOpen && buildingDic[buildingID].panelType == "Resource")
        {
            StopProduceResource("Upgrade",buildingID);
        }
   
        StartBuildingUpgrade(nowCheckingDistrictID, buildingID, needTime);


        BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
        if (buildingDic[buildingID].districtID == nowCheckingDistrictID)
        {
       
            if (DistrictMapPanel.Instance.isShow)
            {
                DistrictMapPanel.Instance.UpdateBaselineResourcesText(nowCheckingDistrictID);
                DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
            }

            if (buildingDic[buildingID].panelType == "House")
            {
                if (DistrictMapPanel.Instance.isShow)
                {
                    DistrictMapPanel.Instance.UpdateBasicInfo();
                }

            }

            
        }
        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateResources();
        if (PlayMainPanel.Instance.IsShowResourcesBlock)
        {
            PlayMainPanel.Instance.UpdateResourcesBlock();
        }
    }

    public void BuildingPullDown(int buildingID)
    {
        int prototypeID = buildingDic[buildingID].prototypeID;


        districtDic[nowCheckingDistrictID].eWind -= DataManager.mBuildingDict[prototypeID].EWind;
        districtDic[nowCheckingDistrictID].eFire -= DataManager.mBuildingDict[prototypeID].EFire;
        districtDic[nowCheckingDistrictID].eWater -= DataManager.mBuildingDict[prototypeID].EWater;
        districtDic[nowCheckingDistrictID].eGround -= DataManager.mBuildingDict[prototypeID].EGround;
        districtDic[nowCheckingDistrictID].eLight -= DataManager.mBuildingDict[prototypeID].ELight;
        districtDic[nowCheckingDistrictID].eDark -= DataManager.mBuildingDict[prototypeID].EDark;

        districtDic[nowCheckingDistrictID].peopleLimit -= DataManager.mBuildingDict[prototypeID].People;

        districtDic[nowCheckingDistrictID].worker -= buildingDic[buildingID].workerNow;
        if (prototypeID == 47)
        {
            forceDic[0].rFoodLimit -= 1000;
        }
        else if (prototypeID == 48)
        {
            forceDic[0].rStuffLimit -= 1000;
        }
        else if (prototypeID == 49)
        {
            districtDic[nowCheckingDistrictID].rProductLimit -= 200;
        }

        //districtDic[nowCheckingDistrictID].gridEmpty += DataManager.mBuildingDict[prototypeID].Grid;
        //districtDic[nowCheckingDistrictID].gridUsed -= DataManager.mBuildingDict[prototypeID].Grid;

        // AreaMapPanel.Instance.RemoveIconByBuilding(buildingID);

        for (int i = 0; i < buildingDic[buildingID].gridList.Count; i++)
        {
            districtGridDic[nowCheckingDistrictID][buildingDic[buildingID].gridList[i]].buildingID = -1;
            //districtGridDic[].buildingID = -1;
            //districtGridDic[buildingDic[buildingID].gridList[i]].pic = "";//暂无必要，取消以节省性能
        }

        MessagePanel.Instance.AddMessage(districtDic[buildingDic[buildingID].districtID].name + "的" + buildingDic[buildingID].name + "已拆除");

        short buildingDistrictID = buildingDic[buildingID].districtID;
        if (buildingDic[buildingID].isOpen && buildingDic[buildingID].panelType == "Resource")
        {
            StopProduceResource("PullDown", buildingID);
        }
        districtDic[nowCheckingDistrictID].buildingList.Remove(buildingID);
        buildingDic.Remove(buildingID);

        if (buildingDistrictID == nowCheckingDistrictID)
        {
            if (prototypeID == 47 || prototypeID == 48 || prototypeID == 49)
            {
                if (DistrictMapPanel.Instance.isShow)
                {
                    DistrictMapPanel.Instance.UpdateBaselineResourcesText(nowCheckingDistrictID);
                }
            }

            if (DistrictMapPanel.Instance.isShow)
            {
                DistrictMapPanel.Instance.DeleteBuilding(buildingID);
                DistrictMapPanel.Instance.UpdateBasicInfo();
            }
        }
        PlayMainPanel.Instance.UpdateResources();
    }
    #endregion

    #region 【方法】建筑物配置生成资源/装备
    void StartProduceResource(int districtID, int buildingID, int needTime, List<StuffType> stuffType, List<int> value)
    {
        //value0:地区实例ID value1:建筑实例ID value2:资源类型 value3:资源数量
        List<int> stuffTypeInt = new List<int>();
        for (int i = 0; i < stuffType.Count; i++)
        {
            stuffTypeInt.Add((int)stuffType[i]);
        }

        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.ProduceResource, standardTime, standardTime + needTime, new List<List<int>> { new List<int> { districtID }, new List<int> { buildingID }, stuffTypeInt, value }));
        buildingDic[buildingID].isOpen = true;
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
        }
        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
        }
    }

    public void StopProduceResource(string type,int buildingID)
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < executeEventList.Count; i++)
        {
            if (executeEventList[i].type == ExecuteEventType.ProduceResource && executeEventList[i].value[1][0] == buildingID)
            {
                tempList.Add(i);
            }
        }
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            ExecuteEventDelete(tempList[i]);

        }
        if (type == "Stop")
        {
            buildingDic[buildingID].isOpen = false;

            MessagePanel.Instance.AddMessage("接到停工命令，生产停止");
            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
                if (BuildingPanel.Instance.IsShowOutputInfoPart)
                {
                    BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                }

            }
            if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == buildingDic[buildingID].districtID)
            {
                DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
            }
        }
        else if (type == "Upgrade")
        {
            MessagePanel.Instance.AddMessage("建筑升级中，生产停止");
        }
        else if (type == "PullDown")
        {
            MessagePanel.Instance.AddMessage("建筑拆除，生产停止");
        }
    }

    public void AddProduceItemTask(int buildingID, List<StuffType> addStuff,short num)
    {


        if (buildingDic[buildingID].taskList.Count >= 3)
        {
            MessagePanel.Instance.AddMessage("任务队列已满，添加失败");
            return;
        }
        short itemID=-1;
        foreach (KeyValuePair<int, ProduceEquipPrototype> kvp in DataManager.mProduceEquipDict)
        {
            if (kvp.Value.MakePlace.Contains((byte)buildingDic[buildingID].prototypeID) && kvp.Value.OptionValue == BuildingPanel.Instance.setForgeType && kvp.Value.Level == (BuildingPanel.Instance.setForgeLevel + 1))
            {
                itemID = kvp.Value.ID;

                break;
            }
        }


        buildingDic[buildingID].taskList.Add(new BuildingTaskObject(itemID, addStuff, num));

        BuildingPanel.Instance.UpdateOutputInfoPartTask(buildingDic[buildingID]);
    }
    public void DeleteProduceItemTask(int buildingID, byte index)
    {
        buildingDic[buildingID].taskList.RemoveAt(index);
        if (buildingDic[buildingID].taskList.Count == 0)
        {
            StopProduceItem(buildingID);
        }
        BuildingPanel.Instance.UpdateOutputInfoPartTask(buildingDic[buildingID]);
    }


    void StartProduceItem(int districtID, int buildingID, int needTime, short produceEquipNow)
    {
        //value0:地区实例ID value1:建筑实例ID value2:装备模板原型ID
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.ProduceItem, standardTime, standardTime + needTime, new List<List<int>> { new List<int> { districtID }, new List<int> { buildingID }, new List<int> { produceEquipNow } }));
        buildingDic[buildingID].isOpen = true;
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
            BuildingPanel.Instance.UpdateSceneRolePic(buildingDic[buildingID]);
        }
        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
        }
    }

    public void StopProduceItem(int buildingID)
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < executeEventList.Count; i++)
        {
            if (executeEventList[i].type == ExecuteEventType.ProduceItem && executeEventList[i].value[1][0] == buildingID)
            {
                tempList.Add(i);
            }
        }
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            ExecuteEventDelete(tempList[i]);
        }
        buildingDic[buildingID].isOpen = false;
        MessagePanel.Instance.AddMessage("接到停工命令或订单取消，生产停止");
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
            BuildingPanel.Instance.UpdateSceneRolePic(buildingDic[buildingID]);
            if (BuildingPanel.Instance.IsShowOutputInfoPart)
            {
                BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
            }
               
        }
        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == buildingDic[buildingID].districtID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
        }
    }


    public void CreateProduceItemEvent(int buildingID)
    {
        short districtID = buildingDic[buildingID].districtID;
        if (buildingDic[buildingID].taskList.Count == 0)
        {
            buildingDic[buildingID].isOpen = false;
       
            MessagePanel.Instance.AddMessage("全部生产任务都完成了，生产停止");
            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateSceneRolePic(buildingDic[buildingID]);
                if (BuildingPanel.Instance.IsShowOutputInfoPart)
                {
                    BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                }
                BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
            }
            if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
            {
                DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
            }
            return;
        }

        if (GetDistrictProductAll(districtID) >= districtDic[districtID].rProductLimit)
        {
            buildingDic[buildingID].isOpen = false;
            MessagePanel.Instance.AddMessage("制品库房已满，生产停止");
            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateSceneRolePic(buildingDic[buildingID]);
                if (BuildingPanel.Instance.IsShowOutputInfoPart)
                {
                    BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                }
                BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
            }
            if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
            {
                DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
            }
      
            return ;
        }

       // int moduleID = buildingDic[buildingID].produceEquipNow;
        int moduleID = buildingDic[buildingID].taskList[0].produceEquipNow;

        if (DataManager.mProduceEquipDict[moduleID].InputWood > forceDic[0].rStuffWood ||
            DataManager.mProduceEquipDict[moduleID].InputStone > forceDic[0].rStuffStone ||
            DataManager.mProduceEquipDict[moduleID].InputMetal > forceDic[0].rStuffMetal ||
            DataManager.mProduceEquipDict[moduleID].InputLeather > forceDic[0].rStuffLeather ||
            DataManager.mProduceEquipDict[moduleID].InputCloth > forceDic[0].rStuffCloth ||
            DataManager.mProduceEquipDict[moduleID].InputTwine > forceDic[0].rStuffTwine ||
            DataManager.mProduceEquipDict[moduleID].InputBone > forceDic[0].rStuffBone ||
            DataManager.mProduceEquipDict[moduleID].InputWind > forceDic[0].rStuffWind ||
            DataManager.mProduceEquipDict[moduleID].InputFire > forceDic[0].rStuffFire ||
            DataManager.mProduceEquipDict[moduleID].InputWater > forceDic[0].rStuffWater ||
            DataManager.mProduceEquipDict[moduleID].InputGround > forceDic[0].rStuffGround ||
            DataManager.mProduceEquipDict[moduleID].InputLight > forceDic[0].rStuffLight ||
            DataManager.mProduceEquipDict[moduleID].InputDark > forceDic[0].rStuffDark)
        {
            buildingDic[buildingID].isOpen = false;
            MessagePanel.Instance.AddMessage("原材料不足，生产停止");
            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateSceneRolePic(buildingDic[buildingID]);
                if (BuildingPanel.Instance.IsShowOutputInfoPart)
                {
                    BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                }
                BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
            }
            if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
            {
                DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
            }
            return ;
        }

        bool addOK = true;
        for (int i = 0; i < 3; i++)
        {
            if (buildingDic[buildingID].taskList[0].forgeAddStuff[i] != StuffType.None)
            {
                switch (buildingDic[buildingID].taskList[0].forgeAddStuff[i])
                {
                    case StuffType.Wood: if (forceDic[0].rStuffWood < (DataManager.mProduceEquipDict[moduleID].InputWood+ GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break; 
                    case StuffType.Stone: if (forceDic[0].rStuffStone < (DataManager.mProduceEquipDict[moduleID].InputStone + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Metal: if (forceDic[0].rStuffMetal < (DataManager.mProduceEquipDict[moduleID].InputMetal + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Leather: if (forceDic[0].rStuffLeather < (DataManager.mProduceEquipDict[moduleID].InputLeather + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Cloth: if (forceDic[0].rStuffCloth < (DataManager.mProduceEquipDict[moduleID].InputCloth + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Twine: if (forceDic[0].rStuffTwine < (DataManager.mProduceEquipDict[moduleID].InputTwine + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Bone: if (forceDic[0].rStuffBone < (DataManager.mProduceEquipDict[moduleID].InputBone + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Wind: if (forceDic[0].rStuffWind < (DataManager.mProduceEquipDict[moduleID].InputWind + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Fire: if (forceDic[0].rStuffFire < (DataManager.mProduceEquipDict[moduleID].InputFire + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Water: if (forceDic[0].rStuffWater < (DataManager.mProduceEquipDict[moduleID].InputWater + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Ground: if (forceDic[0].rStuffGround < (DataManager.mProduceEquipDict[moduleID].InputGround + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Light: if (forceDic[0].rStuffLight < (DataManager.mProduceEquipDict[moduleID].InputLight + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                    case StuffType.Dark: if (forceDic[0].rStuffDark < (DataManager.mProduceEquipDict[moduleID].InputDark + GetForgeTypeCount(buildingDic[buildingID].taskList[0].forgeAddStuff[i], buildingDic[buildingID].taskList[0].forgeAddStuff) * 10)) { addOK = false; } break;
                }
                if (addOK == false)
                {
                    buildingDic[buildingID].isOpen = false;
                    MessagePanel.Instance.AddMessage("附加材料不足，生产停止");
                    if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
                    {
                        BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
                        BuildingPanel.Instance.UpdateSceneRolePic(buildingDic[buildingID]);
                        if (BuildingPanel.Instance.IsShowOutputInfoPart)
                        {
                            BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                        }
                        BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
                    }
                    if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
                    {
                        DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
                    }
                    return;
                }
            }
        }


        forceDic[0].rStuffWood -= DataManager.mProduceEquipDict[moduleID].InputWood;
        forceDic[0].rStuffStone -= DataManager.mProduceEquipDict[moduleID].InputStone;
        forceDic[0].rStuffMetal -= DataManager.mProduceEquipDict[moduleID].InputMetal;
        forceDic[0].rStuffLeather -= DataManager.mProduceEquipDict[moduleID].InputLeather;
        forceDic[0].rStuffCloth -= DataManager.mProduceEquipDict[moduleID].InputCloth;
        forceDic[0].rStuffTwine -= DataManager.mProduceEquipDict[moduleID].InputTwine;
        forceDic[0].rStuffBone -= DataManager.mProduceEquipDict[moduleID].InputBone;
        forceDic[0].rStuffWind -= DataManager.mProduceEquipDict[moduleID].InputWind;
        forceDic[0].rStuffFire -= DataManager.mProduceEquipDict[moduleID].InputFire;
        forceDic[0].rStuffWater -= DataManager.mProduceEquipDict[moduleID].InputWater;
        forceDic[0].rStuffGround -= DataManager.mProduceEquipDict[moduleID].InputGround;
        forceDic[0].rStuffLight -= DataManager.mProduceEquipDict[moduleID].InputLight;
        forceDic[0].rStuffDark -= DataManager.mProduceEquipDict[moduleID].InputDark;

        for (int i = 0; i < 3; i++)
        {
            if (buildingDic[buildingID].taskList[0].forgeAddStuff[i] != StuffType.None)
            {
                switch (buildingDic[buildingID].taskList[0].forgeAddStuff[i])
                {
                    case StuffType.Wood: forceDic[0].rStuffWood -= 10;  break;
                    case StuffType.Stone: forceDic[0].rStuffStone -= 10; break;
                    case StuffType.Metal: forceDic[0].rStuffMetal -= 10; break;
                    case StuffType.Leather: forceDic[0].rStuffLeather -= 10; break;
                    case StuffType.Cloth: forceDic[0].rStuffCloth -= 10; break;
                    case StuffType.Twine: forceDic[0].rStuffTwine -= 10; break;
                    case StuffType.Bone: forceDic[0].rStuffBone -= 10; break;
                    case StuffType.Wind: forceDic[0].rStuffWind -= 10; break;
                    case StuffType.Fire: forceDic[0].rStuffFire -= 10; break;
                    case StuffType.Water: forceDic[0].rStuffWater -= 10; break;
                    case StuffType.Ground: forceDic[0].rStuffGround -= 10; break;
                    case StuffType.Light: forceDic[0].rStuffLight -= 10; break;
                    case StuffType.Dark: forceDic[0].rStuffDark -= 10; break;
                }
            }

        }


        int needLabor = DataManager.mProduceEquipDict[buildingDic[buildingID].taskList[0].produceEquipNow].NeedLabor;
        int nowLabor = 20 + buildingDic[buildingID].workerNow * 20;
        for (int i = 1; i < buildingDic[buildingID].heroList.Count; i++)
        {
            switch (buildingDic[buildingID].id)
            {
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                    nowLabor += heroDic[buildingDic[buildingID].heroList[i]].workMakeWeapon;
                    break;
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                    nowLabor += heroDic[buildingDic[buildingID].heroList[i]].workMakeArmor;
                    break;
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                    nowLabor += heroDic[buildingDic[buildingID].heroList[i]].workMakeJewelry;
                    break;
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                    nowLabor += heroDic[buildingDic[buildingID].heroList[i]].workMakeScroll;
                    break;
            }
        }

        int needTime = (int)(24 * ((float)needLabor / (float)nowLabor));

        //if (buildingDic[buildingID].taskList[0].num != -1)
        //{
        //    buildingDic[buildingID].taskList[0].num--;
        //    if (buildingDic[buildingID].taskList[0].num == 0)
        //    {
        //        buildingDic[buildingID].taskList.RemoveAt(0);
        //    }
        //}
        

        StartProduceItem(buildingDic[buildingID].districtID, buildingID, needTime, buildingDic[buildingID].taskList[0].produceEquipNow);
        PlayMainPanel.Instance.UpdateResources();
        if (PlayMainPanel.Instance.IsShowResourcesBlock)
        {
            PlayMainPanel.Instance.UpdateResourcesBlock();
        }
    }

    int GetForgeTypeCount(StuffType stuffType, List<StuffType> stuffTypes)
    {
        int count = 0;
        for (int i = 0; i < stuffTypes.Count; i++)
        {
            if (stuffTypes[i] == stuffType)
            {
                count++;
            }
        }
        return count;
    }

    public void CreateProduceResourceEvent(int buildingID)
    {
        Debug.Log("CreateProduceResourceEvent() buildingID=" + buildingID);


        buildingDic[buildingID].produceEquipNow = buildingDic[buildingID].prototypeID;
        int needTime = 24 * DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].TimeInterval;
        float laborRate = GetProduceResourceLaborRate(buildingID);
        Debug.Log("laborRate=" + laborRate);
        int num1, num2 = 0;

        switch (buildingDic[buildingID].prototypeID)
        {
            case 9://麦田
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputCereal * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Cereal }, new List<int> { num1 });
                break;
            case 10://菜田
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputVegetable * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Vegetable }, new List<int> { num1 });
                break;
            case 11://果园
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputFruit * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Fruit }, new List<int> { num1 });
                break;
            case 12://亚麻田
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputTwine * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Twine }, new List<int> { num1 });
                break;
            case 13://牛圈
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMeat * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Meat }, new List<int> { num1 });
                break;
            case 14://羊圈
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMeat * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                num2 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputCloth * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Meat, StuffType.Cloth }, new List<int> { num1, num2 });
                break;
            case 15://渔场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputFish * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Fish }, new List<int> { num1 });
                break;

            case 25://啤酒厂
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputBeer * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Beer }, new List<int> { num1 });
                break;
            case 26://红酒厂
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputWine * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Wine }, new List<int> { num1 });
                break;

            case 16://伐木场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputWood * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Wood }, new List<int> { num1 });
                break;
            case 17://伐木场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputWood * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Wood }, new List<int> { num1 });
                break;
            case 18://伐木场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputWood * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Wood }, new List<int> { num1 });
                break;

            case 19://矿场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMetal * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Metal }, new List<int> { num1 });
                break;
            case 20://矿场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMetal * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Metal }, new List<int> { num1 });
                break;
            case 21://矿场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMetal * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Metal }, new List<int> { num1 });
                break;

            case 22://采石场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputStone * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Stone }, new List<int> { num1 });
                break;
            case 23://采石场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputStone * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Stone }, new List<int> { num1 });
                break;
            case 24://采石场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputStone * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, new List<StuffType> { StuffType.Stone }, new List<int> { num1 });
                break;
        }
    }

    public void DeleteProduceResourceEvent(int buildingID)
    {
        buildingDic[buildingID].isOpen = false;
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateSceneRolePic(buildingDic[buildingID]);
        }          
    }

    public void DistrictItemOrSkillAdd(short districtID, int buildingID)
    {
        int moduleID = buildingDic[buildingID].taskList[0].produceEquipNow;

        //计算依据模板，确定道具原型ID
        int probabilityCount = DataManager.mProduceEquipDict[moduleID].OutputRate.Count;
        int ran = Random.Range(0, 100);
        int lj = 0;
        int itemOrSkillID = -1;
        for (int i = 0; i < probabilityCount; i++)
        {
            lj += DataManager.mProduceEquipDict[moduleID].OutputRate[i];

            if (ran < lj)
            {
                itemOrSkillID = DataManager.mProduceEquipDict[moduleID].OutputID[i];
                break;
            }
        }
        switch (DataManager.mProduceEquipDict[moduleID].Type)
        {
            case ItemTypeSmall.Sword:
            case ItemTypeSmall.Axe:
            case ItemTypeSmall.Spear:
            case ItemTypeSmall.Hammer:
            case ItemTypeSmall.Bow:
            case ItemTypeSmall.Staff:
            case ItemTypeSmall.Shield:
            case ItemTypeSmall.Dorlach:
            case ItemTypeSmall.Neck:
            case ItemTypeSmall.Finger:
            case ItemTypeSmall.HeadH:
            case ItemTypeSmall.BodyH:
            case ItemTypeSmall.HandH:
            case ItemTypeSmall.BackH:
            case ItemTypeSmall.FootH:
            case ItemTypeSmall.HeadL:
            case ItemTypeSmall.BodyL:
            case ItemTypeSmall.HandL:
            case ItemTypeSmall.BackL:
            case ItemTypeSmall.FootL:
                itemDic.Add(itemIndex, GenerateItemByRandom(itemOrSkillID, districtDic[districtID], buildingID,buildingDic[buildingID].heroList));
                itemIndex++;
                switch (DataManager.mItemDict[itemOrSkillID].TypeBig)
                {
                    case ItemTypeBig.Weapon:
                    case ItemTypeBig.Subhand:
                        districtDic[districtID].rProductWeapon++;
                        break;
                    case ItemTypeBig.Armor:
                        districtDic[districtID].rProductArmor++;
                        break;
                    case ItemTypeBig.Jewelry:
                        districtDic[districtID].rProductJewelry++;
                        break;
                }
                //增加管理人员的数据
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    switch (DataManager.mItemDict[itemOrSkillID].TypeBig)
                    {
                        case ItemTypeBig.Weapon:
                        case ItemTypeBig.Subhand:
                            heroDic[buildingDic[buildingID].heroList[i]].countMakeWeapon++;
                            break;
                        case ItemTypeBig.Armor:
                            heroDic[buildingDic[buildingID].heroList[i]].countMakeArmor++;
                            break;
                        case ItemTypeBig.Jewelry:
                            heroDic[buildingDic[buildingID].heroList[i]].countMakeJewelry++;
                            break;

                    }
                }
                if (ItemListAndInfoPanel.Instance.isShow&& ItemListAndInfoPanel.Instance.nowDistrictID == districtID)
                {
                    ItemListAndInfoPanel.Instance.UpdateList(districtID, 1);
                }
                if (DistrictMapPanel.Instance.isShow)
                {
                    DistrictMapPanel.Instance.UpdateButtonItemNum(districtID);
                }
                Debug.Log("DistrictItemAdd() 生产 " + DataManager.mItemDict[itemOrSkillID].Name);
                break;
            case ItemTypeSmall.ScrollWindI:
            case ItemTypeSmall.ScrollFireI:
            case ItemTypeSmall.ScrollWaterI:
            case ItemTypeSmall.ScrollGroundI:
            case ItemTypeSmall.ScrollLightI:
            case ItemTypeSmall.ScrollDarkI:
            case ItemTypeSmall.ScrollNone:
            case ItemTypeSmall.ScrollWindII:
            case ItemTypeSmall.ScrollFireII:
            case ItemTypeSmall.ScrollWaterII:
            case ItemTypeSmall.ScrollGroundII:
            case ItemTypeSmall.ScrollLightII:
            case ItemTypeSmall.ScrollDarkII:
                skillDic.Add(skillIndex, GenerateSkillByRandom((short)itemOrSkillID, districtID));
                skillIndex++;
                Debug.Log("DistrictItemAdd() 生产 " + DataManager.mSkillDict[itemOrSkillID].Name);
                districtDic[districtID].rProductScroll++;
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    heroDic[buildingDic[buildingID].heroList[i]].countMakeScroll++;
                }
                if (SkillListAndInfoPanel.Instance.isShow && SkillListAndInfoPanel.Instance.nowDistrictID == districtID)
                {
                    SkillListAndInfoPanel.Instance.UpdateList(districtID, null, -1, 0);
                }
                if (DistrictMapPanel.Instance.isShow)
                {
                    DistrictMapPanel.Instance.UpdateButtonScrollNum(districtID);
                }
                break;
        }



        CreateLog(LogType.ProduceDone, "", new List<int> { districtID, buildingID, itemOrSkillID });

        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            if (BuildingPanel.Instance.IsShowHistoryInfoPart)
            {
                BuildingPanel.Instance.UpdateHistoryInfoPart(buildingDic[buildingID]);
            }
        }
        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        {
            DistrictMapPanel.Instance.UpdateBaselineResourcesText(districtID);
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
         

        }

        if (buildingDic[buildingID].taskList[0].num != -1)
        {
            buildingDic[buildingID].taskList[0].num--;
            if (buildingDic[buildingID].taskList[0].num == 0)
            {
                buildingDic[buildingID].taskList.RemoveAt(0);
            }

            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                if (BuildingPanel.Instance.IsShowOutputInfoPartTask)
                {
                    BuildingPanel.Instance.UpdateOutputInfoPartTask(buildingDic[buildingID]);
                }
            }
        }
       
        //return true;
        // itemDic.Add(GenerateItemByRandom(, districtDic[districtID],));
    }

    public bool DistrictResourceAdd(short districtID, int buildingID, List<StuffType> stuffType, List<int> value)
    {

        Debug.Log("DistrictResourceAdd() " + districtID + " " + stuffType + " " + value);

        for (int i = 0; i < stuffType.Count; i++)
        {
            switch (stuffType[i])
            {
                case StuffType.Cereal:
                case StuffType.Vegetable:
                case StuffType.Fruit:
                case StuffType.Meat:
                case StuffType.Fish:
                case StuffType.Beer:
                case StuffType.Wine:
                    if (GetForceFoodAll(0) >= forceDic[0].rFoodLimit)
                    {
                        MessagePanel.Instance.AddMessage("食物库房已满，生产停止");
                        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
                        {
                            if (BuildingPanel.Instance.IsShowOutputInfoPart)
                            {
                                BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                            }
                            BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
                        }
                        return false;
                    }
                    break;
                case StuffType.Wood:
                case StuffType.Stone:
                case StuffType.Metal:
                case StuffType.Leather:
                case StuffType.Cloth:
                case StuffType.Twine:
                case StuffType.Bone:
                case StuffType.Wind:
                case StuffType.Fire:
                case StuffType.Water:
                case StuffType.Ground:
                case StuffType.Light:
                case StuffType.Dark:
                    if (GetForceStuffAll(0) >= forceDic[0].rStuffLimit)
                    {
                        MessagePanel.Instance.AddMessage("材料库房已满，生产停止");
                        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
                        {
                            if (BuildingPanel.Instance.IsShowOutputInfoPart)
                            {
                                BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                            }
                            BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
                        }
                        return false;
                    }
                    break;
            }
        }


        int moduleID = buildingDic[buildingID].prototypeID;

        if (DataManager.mProduceResourceDict[moduleID].InputWood > forceDic[0].rStuffWood ||
            DataManager.mProduceResourceDict[moduleID].InputStone > forceDic[0].rStuffStone ||
            DataManager.mProduceResourceDict[moduleID].InputMetal > forceDic[0].rStuffMetal ||
            DataManager.mProduceResourceDict[moduleID].InputLeather > forceDic[0].rStuffLeather ||
            DataManager.mProduceResourceDict[moduleID].InputCloth > forceDic[0].rStuffCloth ||
            DataManager.mProduceResourceDict[moduleID].InputTwine > forceDic[0].rStuffTwine ||
            DataManager.mProduceResourceDict[moduleID].InputBone > forceDic[0].rStuffBone ||
            DataManager.mProduceResourceDict[moduleID].InputWind > forceDic[0].rStuffWind ||
            DataManager.mProduceResourceDict[moduleID].InputFire > forceDic[0].rStuffFire ||
            DataManager.mProduceResourceDict[moduleID].InputWater > forceDic[0].rStuffWater ||
            DataManager.mProduceResourceDict[moduleID].InputGround > forceDic[0].rStuffGround ||
            DataManager.mProduceResourceDict[moduleID].InputLight > forceDic[0].rStuffLight ||
            DataManager.mProduceResourceDict[moduleID].InputDark > forceDic[0].rStuffDark)
        {
            MessagePanel.Instance.AddMessage("原材料不足，生产停止");
            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                if (BuildingPanel.Instance.IsShowOutputInfoPart)
                {
                    BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                }
                BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
            }
            return false;
        }

        for (int i = 0; i < stuffType.Count; i++)
        {
            switch (stuffType[i])
            {
                case StuffType.Cereal: forceDic[0].rFoodCereal += value[i]; break;
                case StuffType.Vegetable: forceDic[0].rFoodVegetable += value[i]; break;
                case StuffType.Fruit: forceDic[0].rFoodFruit += value[i]; break;
                case StuffType.Meat: forceDic[0].rFoodMeat += value[i]; break;
                case StuffType.Fish: forceDic[0].rFoodFish += value[i]; break;
                case StuffType.Beer: forceDic[0].rFoodBeer += value[i]; break;
                case StuffType.Wine: forceDic[0].rFoodWine += value[i]; break;
                case StuffType.Wood: forceDic[0].rStuffWood += value[i]; break;
                case StuffType.Stone: forceDic[0].rStuffStone += value[i]; break;
                case StuffType.Metal: forceDic[0].rStuffMetal += value[i]; break;
                case StuffType.Leather: forceDic[0].rStuffLeather += value[i]; break;
                case StuffType.Cloth: forceDic[0].rStuffCloth += value[i]; break;
                case StuffType.Twine: forceDic[0].rStuffTwine += value[i]; break;
                case StuffType.Bone: forceDic[0].rStuffBone += value[i]; break;
                case StuffType.Wind: forceDic[0].rStuffWind += value[i]; break;
                case StuffType.Fire: forceDic[0].rStuffFire += value[i]; break;
                case StuffType.Water: forceDic[0].rStuffWater += value[i]; break;
                case StuffType.Ground: forceDic[0].rStuffGround += value[i]; break;
                case StuffType.Light: forceDic[0].rStuffLight += value[i]; break;
                case StuffType.Dark: forceDic[0].rStuffDark += value[i]; break;
            }
        }



        forceDic[0].rStuffWood -= DataManager.mProduceResourceDict[moduleID].InputWood;
        forceDic[0].rStuffStone -= DataManager.mProduceResourceDict[moduleID].InputStone;
        forceDic[0].rStuffMetal -= DataManager.mProduceResourceDict[moduleID].InputMetal;
        forceDic[0].rStuffLeather -= DataManager.mProduceResourceDict[moduleID].InputLeather;
        forceDic[0].rStuffCloth -= DataManager.mProduceResourceDict[moduleID].InputCloth;
        forceDic[0].rStuffTwine -= DataManager.mProduceResourceDict[moduleID].InputTwine;
        forceDic[0].rStuffBone -= DataManager.mProduceResourceDict[moduleID].InputBone;
        forceDic[0].rStuffWind -= DataManager.mProduceResourceDict[moduleID].InputWind;
        forceDic[0].rStuffFire -= DataManager.mProduceResourceDict[moduleID].InputFire;
        forceDic[0].rStuffWater -= DataManager.mProduceResourceDict[moduleID].InputWater;
        forceDic[0].rStuffGround -= DataManager.mProduceResourceDict[moduleID].InputGround;
        forceDic[0].rStuffLight -= DataManager.mProduceResourceDict[moduleID].InputLight;
        forceDic[0].rStuffDark -= DataManager.mProduceResourceDict[moduleID].InputDark;

        //if (DistrictMainPanel.Instance.isShow)
        //{
        //    DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[districtID]);
        //}

        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            if (BuildingPanel.Instance.IsShowHistoryInfoPart)
            {
                BuildingPanel.Instance.UpdateHistoryInfoPart(buildingDic[buildingID]);
            }
        }
        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
            DistrictMapPanel.Instance.UpdateBaselineResourcesText(districtID);
            //if (DistrictMapPanel.Instance.IsShowResourcesBlock)
            //{
            //    DistrictMapPanel.Instance.UpdateResourcesBlock(districtID);
            //}
        }
        PlayMainPanel.Instance.UpdateResources();
        return true;
    }


    public void ChangeProduceEquipAddStuff(int buildingID, int addIndex, StuffType newStuff)
    {
        BuildingPanel.Instance.forgeAddStuff[addIndex] = newStuff;
        BuildingPanel.Instance.HideSetForgeAddBlock();
        BuildingPanel.Instance.UpdateSetForgePart(buildingDic[buildingID]);
    }
    public void SetProduceEquipNum(int buildingID, short num)
    {
        BuildingPanel.Instance.setForgePartNum  = num;

        BuildingPanel.Instance.UpdateSetForgePartNum();
    }
    public void ChangeProduceEquipNum(int buildingID, short num)
    {
        if (BuildingPanel.Instance.setForgePartNum == -1)
        {
            BuildingPanel.Instance.setForgePartNum = 0;
        }

        BuildingPanel.Instance.setForgePartNum += num;
        if (BuildingPanel.Instance.setForgePartNum < 0)
        {
            BuildingPanel.Instance.setForgePartNum = 0;
        }
        if (BuildingPanel.Instance.setForgePartNum > 100)
        {
            BuildingPanel.Instance.setForgePartNum = 100;
        }
        BuildingPanel.Instance.UpdateSetForgePartNum();
    }


    public void CreateBuildingSaleEvent(int buildingID)
    {
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.BuildingSale, standardTime, standardTime + 80, new List<List<int>> { new List<int> { buildingDic[buildingID].districtID }, new List<int> { buildingID } }));
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
        }
    }

    public void DeleteBuildingSaleEvent(int buildingID)
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < executeEventList.Count; i++)
        {
            if (executeEventList[i].type == ExecuteEventType.BuildingSale && executeEventList[i].value[1][0] == buildingID)
            {
                tempList.Add(i);
            }
        }
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            ExecuteEventDelete(tempList[i]);
        }
    }

    public void BuildingSale(int buildingID)
    {
        int customerID;
        if (buildingDic[buildingID].customerList.Count > 0)
        {
            //Debug.Log("buildingDic[buildingID].customerList.Count=" + buildingDic[buildingID].customerList.Count);

            for (int i = 1; i < buildingDic[buildingID].customerList.Count; i++)
            {
                //Debug.Log("i=" + i + " customerID=" + buildingDic[buildingID].customerList[i]);
                customerID = buildingDic[buildingID].customerList[i];
                if (customerDic[customerID].stage == CustomerStage.Wait)
                {
                    UpdateCustomerByStage(buildingDic[buildingID].customerList[i]);
                }

            }
            customerID = buildingDic[buildingID].customerList[0];
            if (customerDic[customerID].stage == CustomerStage.Wait)
            {

                CustomerCheckGoods(customerID);
                buildingDic[customerDic[customerID].buildingID].customerList.Remove(customerID);
                customerDic[customerID].stage = CustomerStage.IntoShop;

                UpdateCustomerByStage(customerID);
            }

        }

    }

    public void BuildingStopSale(int buildingID)
    {
        DeleteBuildingSaleEvent(buildingID);
        for (int i = 0; i < buildingDic[buildingID].customerList.Count; i++)
        {
            customerRecordDic[timeYear + "/" + timeMonth].backNum[buildingDic[buildingID].districtID]++;

            if (customerDic[buildingDic[buildingID].customerList[i]].stage == CustomerStage.Wait)
            {
                customerDic[buildingDic[buildingID].customerList[i]].stage = CustomerStage.Gone;
            }
            else if (customerDic[buildingDic[buildingID].customerList[i]].stage == CustomerStage.Come)
            {
                customerDic[buildingDic[buildingID].customerList[i]].stage = CustomerStage.Observe;
            }
            UpdateCustomerByStage(buildingDic[buildingID].customerList[i]);
        }
        buildingDic[buildingID].customerList.Clear();
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
        }
    }
    #endregion

    #region 【方法】建筑物配置管理者、工人
    public void BuildingManagerMinus(int buildingID, int heroID)
    {
        if (!buildingDic[buildingID].heroList.Contains(heroID))
        {
            return;
        }
        buildingDic[buildingID].heroList.Remove(heroID);
        heroDic[heroID].workerInBuilding = -1;
        BuildingPanel.Instance.UpdateSetManagerPart(buildingDic[buildingID]);
    }

    public void BuildingManagerAdd(int buildingID, int heroID)
    {
        if (buildingDic[buildingID].heroList.Count >= 4)
        {
            return;
        }
        if (heroDic[heroID].workerInBuilding != -1)
        {
            return;
        }
        buildingDic[buildingID].heroList.Add(heroID);
        heroDic[heroID].workerInBuilding = buildingID;
        BuildingPanel.Instance.UpdateSetManagerPart(buildingDic[buildingID]);
    }

    public void BuildingWorkerMinus(int buildingID)
    {
        if (buildingDic[buildingID].worker <= 0)
        {
            return;
        }
        buildingDic[buildingID].workerNow--;
        districtDic[buildingDic[buildingID].districtID].worker--;
        BuildingPanel.Instance.UpdateSetWorkerPart(buildingDic[buildingID]);
        DistrictMapPanel.Instance.UpdateBasicInfo();
    }

    public void BuildingWorkerAdd(int buildingID)
    {
        if (buildingDic[buildingID].workerNow >= buildingDic[buildingID].worker)
        {
            return;
        }
        if (districtDic[buildingDic[buildingID].districtID].people - districtDic[buildingDic[buildingID].districtID].worker <= 0)
        {
            return;
        }
        buildingDic[buildingID].workerNow++;
        districtDic[buildingDic[buildingID].districtID].worker++;
        BuildingPanel.Instance.UpdateSetWorkerPart(buildingDic[buildingID]);
        DistrictMapPanel.Instance.UpdateBasicInfo();
    }
    #endregion

    #region 【方法】科技研究
    public void CreateTechnologyResearchEvent(int districtID, int technologyID)
    {
        for (int i = 0; i < DataManager.mTechnologyDict[technologyID].ParentID.Count; i++)
        {
            if (technologyDic[DataManager.mTechnologyDict[technologyID].ParentID[i]].stage != TechnologyStage.Done)
            {
                TechnologyPanel.Instance.UpdateInfo(technologyID);
                return;
            }
        }

        if (DataManager.mTechnologyDict[technologyID].NeedBuilding.Count != 0)
        {
            bool ok = false;
            foreach (KeyValuePair<int, BuildingObject> kvp in buildingDic)
            {
                if (DataManager.mTechnologyDict[technologyID].NeedBuilding.Contains(kvp.Value.prototypeID) && kvp.Value.districtID == districtID)
                {
                    ok = true;
                    break;
                }
            }
            if (!ok)
            {
                TechnologyPanel.Instance.UpdateInfo(technologyID);
                return;
            }
        }

        for (int i = 0; i < DataManager.mTechnologyDict[technologyID].NeedStuff.Count; i++)
        {
            switch (DataManager.mTechnologyDict[technologyID].NeedStuff[i])
            {
                case StuffType.Wood:
                    if (forceDic[0].rStuffWood < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Stone:
                    if (forceDic[0].rStuffStone < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Metal:
                    if (forceDic[0].rStuffMetal < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Leather:
                    if (forceDic[0].rStuffLeather < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Cloth:
                    if (forceDic[0].rStuffCloth < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Twine:
                    if (forceDic[0].rStuffTwine < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Bone:
                    if (forceDic[0].rStuffBone < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Wind:
                    if (forceDic[0].rStuffWind < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Fire:
                    if (forceDic[0].rStuffFire < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Water:
                    if (forceDic[0].rStuffWater < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Ground:
                    if (forceDic[0].rStuffGround < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Light:
                    if (forceDic[0].rStuffLight < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;
                case StuffType.Dark:
                    if (forceDic[0].rStuffDark < DataManager.mTechnologyDict[technologyID].NeedStuffValue[i])
                    {
                        TechnologyPanel.Instance.UpdateInfo(technologyID);
                        return;
                    }
                    break;

            }
        }
        if (DataManager.mTechnologyDict[technologyID].NeedGold != 0)
        {
            if (forceDic[0].gold < DataManager.mTechnologyDict[technologyID].NeedGold)
            {
                TechnologyPanel.Instance.UpdateInfo(technologyID);
                return;
            }
        }

        for (int i = 0; i < DataManager.mTechnologyDict[technologyID].NeedStuff.Count; i++)
        {
            switch (DataManager.mTechnologyDict[technologyID].NeedStuff[i])
            {
                case StuffType.Wood:
                    forceDic[0].rStuffWood -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Stone:
                    forceDic[0].rStuffStone -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Metal:
                    forceDic[0].rStuffMetal -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Leather:
                    forceDic[0].rStuffLeather -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Cloth:
                    forceDic[0].rStuffCloth -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Twine:
                    forceDic[0].rStuffTwine -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Bone:
                    forceDic[0].rStuffBone -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Wind:
                    forceDic[0].rStuffWind -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Fire:
                    forceDic[0].rStuffFire -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Water:
                    forceDic[0].rStuffWater -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Ground:
                    forceDic[0].rStuffGround -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Light:
                    forceDic[0].rStuffLight -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;
                case StuffType.Dark:
                    forceDic[0].rStuffDark -= DataManager.mTechnologyDict[technologyID].NeedStuffValue[i];
                    break;

            }
        }
        forceDic[0].gold -= DataManager.mTechnologyDict[technologyID].NeedGold;
        technologyDic[technologyID].stage = TechnologyStage.Research;

        int needTime = DataManager.mTechnologyDict[technologyID].NeedTime * 240;
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.TechnologyResearch, standardTime, standardTime + needTime, new List<List<int>> { new List<int> { districtID }, new List<int> { technologyID } }));

        technologyResearchingList.Add(technologyID);
        PlayMainPanel.Instance.UpdateResources();
        PlayMainPanel.Instance.UpdateGold();
        TechnologyPanel.Instance.UpdateInfo(technologyID);
    }
    public void TechnologyResearchDone(int technologyID)
    {
        technologyResearchingList.Remove(technologyID);
        technologyDic[technologyID].stage = TechnologyStage.Done;
       
        for (int i = 0; i < DataManager.mTechnologyDict[technologyID].ChildrenID.Count; i++)
        {
            if (technologyDic[DataManager.mTechnologyDict[technologyID].ChildrenID[i]].stage == TechnologyStage.Close)
            {
                technologyDic[DataManager.mTechnologyDict[technologyID].ChildrenID[i]].stage = TechnologyStage.Open;
            }

        }

        if (TechnologyPanel.Instance.isShow)
        {
            TechnologyPanel.Instance.UpdateList("done");
            TechnologyPanel.Instance.UpdateList("none");
            if (TechnologyPanel.Instance.nowCheckingTechnology == technologyID)
            {
                TechnologyPanel.Instance.UpdateInfo(technologyID);
            }
        }
        MessagePanel.Instance.AddMessage(DataManager.mTechnologyDict[technologyID].Name + "的研究已经完成");
    }

    #endregion

    #region 【方法】地区设置,更新财政报表,收税
    public void DistrictSetRation(short districtID, StuffType stuffType,int value)
    {
        switch (stuffType)
        {
            case StuffType.Cereal: districtDic[districtID].rationCereal = (byte)(value * 50);break;
            case StuffType.Vegetable: districtDic[districtID].rationVegetable = (byte)(value * 50); break;
            case StuffType.Meat: districtDic[districtID].rationMeat = (byte)(value * 50); break;
            case StuffType.Fish: districtDic[districtID].rationFish = (byte)(value * 50); break;
            case StuffType.Fruit: districtDic[districtID].rationFruit = (byte)(value * 50); break;
            case StuffType.Beer: districtDic[districtID].rationBeer = (byte)(value * 50); break;
            case StuffType.Wine: districtDic[districtID].rationWine = (byte)(value * 50); break;
        }      
    }
    public void DistrictSetTaxPeople(short districtID, int value)
    {
        districtDic[districtID].taxPeople = (byte)(value * 10);
        
    }
    public void DistrictSetTaxPass(short districtID, int value)
    {
        districtDic[districtID].taxPass = (byte)(value * 10);
    }
    public void DistrictSetTaxGoods(short districtID, int value)
    {
        districtDic[districtID].taxGoods = (byte)(value * 10);
    }

    public void DistrictCreateFiscalAll()
    {
        for (short i = 0; i < districtDic.Length; i++)
        {
            DistrictCreateFiscal(i);
        }
    }

    public void DistrictCreateFiscal(short districtID)
    {
        districtDic[districtID].fiscals.RemoveAt(0);
        districtDic[districtID].fiscals.Add(new DistrictFiscal(0, 0, 0, 0, 0, 0, 0));

        if (DistrictMainPanel.Instance.isShow&& districtID ==nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateFiscal0Info(districtDic[districtID]);
            DistrictMainPanel.Instance.UpdateFiscal1Info(districtDic[districtID]);
        }
    }

    //居民税 每周 基数（10）*人口*地区等级*税率
    public void DistrictGetTaxPeopleAll()
    {
        for (short i = 0; i < districtDic.Length; i++)
        {
            DistrictGetTaxPeople(i);
        }

    }

    public void DistrictGetTaxPeople(short districtID)
    {
        int count = (int)(10*districtDic[districtID].people * districtDic[districtID].level * districtDic[districtID].taxPeople / 100f);
        districtDic[districtID].fiscals[1].incomeTaxPeople += count;

        forceDic[districtDic[districtID].force].gold += count;

        DistrictSetSatisfactionByPeopleTax(districtID, (30 - districtDic[districtID].taxPeople) / 10);
        DistrictSetSatisfaction(districtID);

        if (districtDic[districtID].force == 0)
        {
            PlayMainPanel.Instance.UpdateGold();
            MessagePanel.Instance.AddMessage("<color=#F3DE60>" + districtDic[districtID].name + "</color>收取了居民税" + count + "金币");
        }
        if (DistrictMainPanel.Instance.isShow && districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateFiscal1Info(districtDic[districtID]);
        }
        AreaMapPanel.Instance.ShowNumText(districtID, "<color=#F8B666><b>金币+" + count + "</b></color>");
    }

    //通行税 每次 人口*地区等级*税率
    public void DistrictGetTaxPass(short districtID,int num)
    {
        int count = (int)(10 * districtDic[districtID].level * num* districtDic[districtID].taxPass / 100f);
        districtDic[districtID].fiscals[1].incomeTaxPass += count;

        forceDic[districtDic[districtID].force].gold += count;

        DistrictSetProsperousByPassTax(districtID, (30 - districtDic[districtID].taxPass) / 10);
        DistrictSetProsperous(districtID);

        if (districtDic[districtID].force == 0)
        {
            PlayMainPanel.Instance.UpdateGold();

        }
        if (DistrictMainPanel.Instance.isShow && districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateFiscal1Info(districtDic[districtID]);
        }

        AreaMapPanel.Instance.ShowNumText(districtID, "<color=#F8B666><b>金币+" + count + "</b></color>");
    }

    //后勤服务收入 每次
    public void DistrictGetLogistics(short districtID, int num)
    {
        int count = (int)(10 * districtDic[districtID].level * num);
        districtDic[districtID].fiscals[1].incomeLogistics += count;

        forceDic[districtDic[districtID].force].gold += count;

   

        if (districtDic[districtID].force == 0)
        {
            PlayMainPanel.Instance.UpdateGold();
        }
        if (DistrictMainPanel.Instance.isShow && districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateFiscal1Info(districtDic[districtID]);
        }
        AreaMapPanel.Instance.ShowNumText(districtID, "<color=#F8B666><b>金币+" + count + "</b></color>");
    }

    //结算维护费 每月
    public void DistrictBuildingExpenseAll()
    {
        for (short i = 0; i < districtDic.Length; i++)
        {
            DistrictBuildingExpense(i);
        }
    }

    public void DistrictBuildingExpense(short districtID)
    {
        int count = 0;
        for (int i = 0; i < districtDic[districtID].buildingList.Count; i++)
        {
            count += buildingDic[districtDic[districtID].buildingList[i]].expense;
        }

        if (count > forceDic[districtDic[districtID].force].gold)
        {
            count = forceDic[districtDic[districtID].force].gold;
        }

        districtDic[districtID].fiscals[1].expendMaintenance += count;
        forceDic[districtDic[districtID].force].gold -= count;


        if (districtDic[districtID].force == 0)
        {
            PlayMainPanel.Instance.UpdateGold();
            MessagePanel.Instance.AddMessage("<color=#F3DE60>" + districtDic[districtID].name + "</color>的建筑物维护花费了" + count + "金币");

        }
        if (DistrictMainPanel.Instance.isShow && districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateFiscal1Info(districtDic[districtID]);
        }
        AreaMapPanel.Instance.ShowNumText(districtID, "<color=#F86F67><b>金币-" + count + "</b></color>");

    }

    //结算居民食物补给 每周
    public void DistrictPeopleFoodExpenseAll()
    {
        for (short i = 0; i < districtDic.Length; i++)
        {
            DistrictPeopleFoodExpense(i);
        }
    }

    public void DistrictPeopleFoodExpense(short districtID)
    {
        int spendCereal = (int)(Random.Range(10, 13) * districtDic[districtID].people * districtDic[districtID].rationCereal / 100f);
        if (spendCereal > forceDic[districtDic[districtID].force].rFoodCereal)
        {
            spendCereal = forceDic[districtDic[districtID].force].rFoodCereal;
            DistrictSetSatisfactionByFood(districtID, -10);
        }
        forceDic[districtDic[districtID].force].rFoodCereal -= spendCereal;
        switch (districtDic[districtID].rationCereal)
        {
            case 0: DistrictSetSatisfactionByFood(districtID, Random.Range(-11, -9)); break;
            case 50: DistrictSetSatisfactionByFood(districtID, Random.Range(-6, -4)); break;
            case 150: DistrictSetSatisfactionByFood(districtID, Random.Range(4, 5)); break;
            case 200: DistrictSetSatisfactionByFood(districtID, Random.Range(8, 10)); break;
        }


        int spendVegetable = (int)(Random.Range(8, 10) * districtDic[districtID].people * districtDic[districtID].rationVegetable / 100f);
        if (spendVegetable > forceDic[districtDic[districtID].force].rFoodVegetable)
        {
            spendVegetable = forceDic[districtDic[districtID].force].rFoodVegetable;
            DistrictSetSatisfactionByFood(districtID, -5);
        }
        forceDic[districtDic[districtID].force].rFoodVegetable -= spendVegetable;
        switch (districtDic[districtID].rationVegetable)
        {
            case 0: DistrictSetSatisfactionByFood(districtID, Random.Range(-11, -9)); break;
            case 50: DistrictSetSatisfactionByFood(districtID, Random.Range(-6, -4)); break;
            case 150: DistrictSetSatisfactionByFood(districtID, Random.Range(4, 5)); break;
            case 200: DistrictSetSatisfactionByFood(districtID, Random.Range(8, 10)); break;
        }

        int spendMeat = (int)(Random.Range(5, 7) * districtDic[districtID].people * districtDic[districtID].rationMeat / 100f);
        if (spendMeat > forceDic[districtDic[districtID].force].rFoodMeat)
        {
            spendMeat = forceDic[districtDic[districtID].force].rFoodMeat;
            DistrictSetSatisfactionByFood(districtID, -5);
        }
        forceDic[districtDic[districtID].force].rFoodMeat -= spendMeat;
        switch (districtDic[districtID].rationMeat)
        {
            case 0: DistrictSetSatisfactionByFood(districtID, Random.Range(-8, -6)); break;
            case 50: DistrictSetSatisfactionByFood(districtID, Random.Range(-5, -3)); break;
            case 150: DistrictSetSatisfactionByFood(districtID, Random.Range(4, 6)); break;
            case 200: DistrictSetSatisfactionByFood(districtID, Random.Range(8, 11)); break;
        }

        int spendFish = (int)(Random.Range(5, 7) * districtDic[districtID].people * districtDic[districtID].rationFish / 100f);
        if (spendFish > forceDic[districtDic[districtID].force].rFoodFish)
        {
            spendFish = forceDic[districtDic[districtID].force].rFoodFish;
            DistrictSetSatisfactionByFood(districtID, -5);
        }
        forceDic[districtDic[districtID].force].rFoodFish -= spendFish;
        switch (districtDic[districtID].rationFish)
        {
            case 0: DistrictSetSatisfactionByFood(districtID, Random.Range(-5, -3)); break;
            case 50: DistrictSetSatisfactionByFood(districtID, Random.Range(-3, -2)); break;
            case 150: DistrictSetSatisfactionByFood(districtID, Random.Range(3, 5)); break;
            case 200: DistrictSetSatisfactionByFood(districtID, Random.Range(7, 9)); break;
        }

        int spendFruit = (int)(Random.Range(5, 7) * districtDic[districtID].people * districtDic[districtID].rationFruit / 100f);
        if (spendFruit > forceDic[districtDic[districtID].force].rFoodFruit)
        {
            spendFruit = forceDic[districtDic[districtID].force].rFoodFruit;
            DistrictSetSatisfactionByFood(districtID, -5);
        }
        forceDic[districtDic[districtID].force].rFoodFruit -= spendFruit;
        switch (districtDic[districtID].rationFruit)
        {
            case 0: DistrictSetSatisfactionByFood(districtID, Random.Range(-8, -6)); break;
            case 50: DistrictSetSatisfactionByFood(districtID, Random.Range(-4, -2)); break;
            case 150: DistrictSetSatisfactionByFood(districtID, Random.Range(3, 5)); break;
            case 200: DistrictSetSatisfactionByFood(districtID, Random.Range(7, 9)); break;
        }

        int spendBeer = (int)(Random.Range(4, 6) * districtDic[districtID].people * districtDic[districtID].rationBeer / 100f);
        if (spendBeer > forceDic[districtDic[districtID].force].rFoodBeer)
        {
            spendBeer = forceDic[districtDic[districtID].force].rFoodBeer;
            DistrictSetSatisfactionByFood(districtID, Random.Range(-2, 1));
        }
        forceDic[districtDic[districtID].force].rFoodBeer -= spendBeer;
        switch (districtDic[districtID].rationBeer)
        {
            case 0: DistrictSetSatisfactionByFood(districtID, Random.Range(-4, -2)); break;
            case 50: DistrictSetSatisfactionByFood(districtID, Random.Range(-2, 1)); break;
            case 150: DistrictSetSatisfactionByFood(districtID, Random.Range(3, 5)); break;
            case 200: DistrictSetSatisfactionByFood(districtID, Random.Range(7, 9)); break;
        }

        int spendWine = (int)(Random.Range(3, 5) * districtDic[districtID].people * districtDic[districtID].rationWine / 100f);
        if (spendWine > forceDic[districtDic[districtID].force].rFoodWine)
        {
            spendWine = forceDic[districtDic[districtID].force].rFoodWine;
            //DistrictSetSatisfactionByFood(districtID, -1);
        }
        forceDic[districtDic[districtID].force].rFoodWine -= spendWine;
        switch (districtDic[districtID].rationWine)
        {
            //case 0: DistrictSetSatisfactionByFood(districtID, -2); break;
            //case 50: DistrictSetSatisfactionByFood(districtID, -1); break;
            case 150: DistrictSetSatisfactionByFood(districtID, 1); break;
            case 200: DistrictSetSatisfactionByFood(districtID, 2); break;
        }

        DistrictSetSatisfaction(districtID);

        if (districtDic[districtID].force == 0)
        {
            PlayMainPanel.Instance.UpdateResources();
            if (PlayMainPanel.Instance.IsShowResourcesBlock)
            {
                PlayMainPanel.Instance.UpdateResourcesBlock();
            }

            MessagePanel.Instance.AddMessage("<color=#F3DE60>" + districtDic[districtID].name+ "</color>的居民领取了食物补给 "+ (spendCereal>0?("谷物*" + spendCereal):"")+ (spendVegetable > 0 ? ("蔬菜*" +spendVegetable) : "") + (spendMeat > 0 ? ("肉类*" + spendMeat) : "") + (spendFish > 0 ? ("鱼类*" + spendFish) : "") + (spendFruit > 0 ? ("水果*" + spendFruit) : "") + (spendBeer > 0 ? ("啤酒*" + spendBeer) : "") + (spendWine > 0 ? ("红酒*" + spendWine) : ""));
        }

       
    }

    #endregion

    #region 【方法】居民满意度,经济繁荣度
    //居民满意度
    public void DistrictSetSatisfactionInNewGame(short districtID)
    {
        districtDic[districtID].satisfaction = (short)((districtDic[districtID].satisfactionByFood +
            districtDic[districtID].satisfactionByLive +
            districtDic[districtID].satisfactionByPeopleTax +
             districtDic[districtID].satisfactionByWork +
              districtDic[districtID].satisfactionByEvent) * (1f + (float)districtDic[districtID].hpNow / districtDic[districtID].hp * 0.1f));

        districtDic[districtID].satisfaction = (short)Mathf.Clamp(districtDic[districtID].satisfaction, 0, 1000);

    }

    public void DistrictSetSatisfaction(short districtID)
    {
        districtDic[districtID].satisfaction =(short)(( districtDic[districtID].satisfactionByFood +
            districtDic[districtID].satisfactionByLive +
            districtDic[districtID].satisfactionByPeopleTax +
             districtDic[districtID].satisfactionByWork +
              districtDic[districtID].satisfactionByEvent)* (1f+(float)districtDic[districtID].hpNow / districtDic[districtID].hp*0.1f));
        
        districtDic[districtID].satisfaction = (short)Mathf.Clamp(districtDic[districtID].satisfaction, 0, 1000);

        if (DistrictMainPanel.Instance.isShow && districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdatePeopleInfo(districtDic[districtID]);
        }
    }

    public void DistrictSetSatisfactionByFood(short districtID, int value)
    {
        districtDic[districtID].satisfactionByFood += (short)value;
        districtDic[districtID].satisfactionByFood = (short)Mathf.Clamp(districtDic[districtID].satisfactionByFood, -300, 200);
    }
    public void DistrictSetSatisfactionByLive(short districtID, int value)
    {
        districtDic[districtID].satisfactionByLive += (short)value;
        districtDic[districtID].satisfactionByLive = (short)Mathf.Clamp(districtDic[districtID].satisfactionByLive, 0, 150);
    }
    public void DistrictSetSatisfactionByPeopleTax(short districtID, int value)
    {
        districtDic[districtID].satisfactionByPeopleTax += (short)value;
        districtDic[districtID].satisfactionByPeopleTax = (short)Mathf.Clamp(districtDic[districtID].satisfactionByPeopleTax, 0, 150);
    }
    public void DistrictSetSatisfactionByWork(short districtID, int value)
    {
        districtDic[districtID].satisfactionByWork += (short)value;
        districtDic[districtID].satisfactionByWork = (short)Mathf.Clamp(districtDic[districtID].satisfactionByWork, 0, 150);
    }
    public void DistrictSetSatisfactionByEvent(short districtID, int value)
    {
        districtDic[districtID].satisfactionByEvent += (short)value;
        districtDic[districtID].satisfactionByEvent = (short)Mathf.Clamp(districtDic[districtID].satisfactionByEvent, -1000, 1000);
    }


    //经济繁荣度
    public void DistrictSetProsperousInNewGame(short districtID)
    {
        districtDic[districtID].prosperous = (short)((districtDic[districtID].prosperousByGoodsTax +
              districtDic[districtID].prosperousByPassTax +
              districtDic[districtID].level * 50 +
              districtDic[districtID].worker +
              districtDic[districtID].buildingList.Count * 2
              ) *
              (1f + (float)districtDic[districtID].hpNow / districtDic[districtID].hp * 0.1f)
              );

    }
    public void DistrictSetProsperous(short districtID)
    {
        districtDic[districtID].prosperous= (short)((districtDic[districtID].prosperousByGoodsTax +
              districtDic[districtID].prosperousByPassTax+
              districtDic[districtID].level*50+
              districtDic[districtID].worker+
              districtDic[districtID].buildingList.Count*2
              ) * 
              (1f + (float)districtDic[districtID].hpNow / districtDic[districtID].hp * 0.1f)
              );

        if (DistrictMainPanel.Instance.isShow && districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateBasicInfo(districtDic[districtID]);
        }
    }

    public void DistrictSetProsperousByGoodsTax(short districtID, int value)
    {
        districtDic[districtID].prosperousByGoodsTax += (short)value;
        districtDic[districtID].prosperousByGoodsTax = (short)Mathf.Clamp(districtDic[districtID].prosperousByGoodsTax, 0, 150);
    }
    public void DistrictSetProsperousByPassTax(short districtID, int value)
    {
        districtDic[districtID].prosperousByPassTax += (short)value;
        districtDic[districtID].prosperousByPassTax = (short)Mathf.Clamp(districtDic[districtID].prosperousByPassTax, 0, 150);
    }
    #endregion

    #region 【方法】英雄装备/卸下，技能配置
    public void HeroEquipSet(int heroID, EquipPart equipPart, int itemID)
    {
        if (itemID == -1)
        {
            MessagePanel.Instance.AddMessage("未选择物品");
            return;
        }

        // Debug.Log("HeroEquipSet() heroID=" + heroID + " equipPart=" + equipPart + " itemID="+ itemID+ " heroDic[heroID].equipWeapon=" + heroDic[heroID].equipWeapon);
        if (itemDic[itemID].heroID != -1)
        {
            MessagePanel.Instance.AddMessage("当前指向的物品已被装备，请重新选择");
            return;
        }

        switch (equipPart)
        {
            case EquipPart.Weapon:
                if (heroDic[heroID].equipWeapon != -1)
                {
                    itemDic[heroDic[heroID].equipWeapon].heroID = -1;
                    itemDic[heroDic[heroID].equipWeapon].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipWeapon = itemID;
                heroDic[heroID].equipSuitePart[0] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID;
                break;
            case EquipPart.Subhand:
                if (heroDic[heroID].equipSubhand != -1)
                {
                    itemDic[heroDic[heroID].equipSubhand].heroID = -1;
                    itemDic[heroDic[heroID].equipSubhand].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipSubhand = itemID;
                heroDic[heroID].equipSuitePart[1] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID;
                break;
            case EquipPart.Head:
                if (heroDic[heroID].equipHead != -1)
                {
                    itemDic[heroDic[heroID].equipHead].heroID = -1;
                    itemDic[heroDic[heroID].equipHead].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipHead = itemID;
                heroDic[heroID].equipSuitePart[2] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID;
                break;
            case EquipPart.Body:
                if (heroDic[heroID].equipBody != -1)
                {
                    itemDic[heroDic[heroID].equipBody].heroID = -1;
                    itemDic[heroDic[heroID].equipBody].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipBody = itemID;
                heroDic[heroID].equipSuitePart[3] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID;
                break;
            case EquipPart.Hand:
                if (heroDic[heroID].equipHand != -1)
                {
                    itemDic[heroDic[heroID].equipHand].heroID = -1;
                    itemDic[heroDic[heroID].equipHand].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipHand = itemID;
                heroDic[heroID].equipSuitePart[4] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID;
                break;
            case EquipPart.Back:
                if (heroDic[heroID].equipBack != -1)
                {
                    itemDic[heroDic[heroID].equipBack].heroID = -1;
                    itemDic[heroDic[heroID].equipBack].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipBack = itemID;
                heroDic[heroID].equipSuitePart[5] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID;
                break;
            case EquipPart.Foot:
                if (heroDic[heroID].equipFoot != -1)
                {
                    itemDic[heroDic[heroID].equipFoot].heroID = -1;
                    itemDic[heroDic[heroID].equipFoot].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipFoot = itemID;
                heroDic[heroID].equipSuitePart[6] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID; 
                break;
            case EquipPart.Neck:
                if (heroDic[heroID].equipNeck != -1)
                {
                    itemDic[heroDic[heroID].equipNeck].heroID = -1;
                    itemDic[heroDic[heroID].equipNeck].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipNeck = itemID;
                heroDic[heroID].equipSuitePart[7] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID; 
                break;
            case EquipPart.Finger1:
                if (heroDic[heroID].equipFinger1 != -1)
                {
                    itemDic[heroDic[heroID].equipFinger1].heroID = -1;
                    itemDic[heroDic[heroID].equipFinger1].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipFinger1 = itemID; 
                heroDic[heroID].equipSuitePart[8] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID; 
                break;
            case EquipPart.Finger2:
                if (heroDic[heroID].equipFinger2 != -1)
                {
                    itemDic[heroDic[heroID].equipFinger2].heroID = -1;
                    itemDic[heroDic[heroID].equipFinger2].heroPart = EquipPart.None;
                }
                else
                {
                    forceDic[0].rProductNow--;
                    PlayMainPanel.Instance.UpdateInventoryNum();
                }
                heroDic[heroID].equipFinger2 = itemID;
                heroDic[heroID].equipSuitePart[9] = DataManager.mItemDict[itemDic[itemID].prototypeID].SuiteID;
                break;
            default: break;
        }
        itemDic[itemID].heroID = heroID;
        itemDic[itemID].heroPart = equipPart;
        PlayMainPanel.Instance.UpdateButtonItemNum();
        HeroPanel.Instance.UpdateFightInfo(heroDic[heroID], EquipPart.None, null, 1);
        HeroPanel.Instance.UpdateEquip(heroDic[heroID], equipPart);
        ItemListAndInfoPanel.Instance.OnHide();


        AudioControl.Instance.PlaySound("system_equip");
    }

    public void HeroEquipUnSet(int heroID, EquipPart equipPart)
    {
        // Debug.Log("HeroEquipUnSet() heroID=" + heroID + " equipPart=" + equipPart+ " heroDic[heroID].equipWeapon="+ heroDic[heroID].equipWeapon);
        if (forceDic[0].rProductNow >= forceDic[0].rProductLimit)
        {
            MessagePanel.Instance.AddMessage("收藏库已满");
            return;
        }

        switch (equipPart)
        {
            case EquipPart.Weapon:
                if (heroDic[heroID].equipWeapon == -1) { return; }
                itemDic[heroDic[heroID].equipWeapon].heroID = -1;
                itemDic[heroDic[heroID].equipWeapon].heroPart = EquipPart.None;
                heroDic[heroID].equipWeapon = -1;
                heroDic[heroID].equipSuitePart[0] = -1;
                break;
            case EquipPart.Subhand:
                if (heroDic[heroID].equipSubhand == -1) { return; }
                itemDic[heroDic[heroID].equipSubhand].heroID = -1;
                itemDic[heroDic[heroID].equipSubhand].heroPart = EquipPart.None;
                heroDic[heroID].equipSubhand = -1;
                heroDic[heroID].equipSuitePart[1] = -1; break;
            case EquipPart.Head:
                if (heroDic[heroID].equipHead == -1) { return; }
                itemDic[heroDic[heroID].equipHead].heroID = -1;
                itemDic[heroDic[heroID].equipHead].heroPart = EquipPart.None;
                heroDic[heroID].equipHead = -1;
                heroDic[heroID].equipSuitePart[2] = -1; break;
            case EquipPart.Body:
                if (heroDic[heroID].equipBody == -1) { return; }
                itemDic[heroDic[heroID].equipBody].heroID = -1;
                itemDic[heroDic[heroID].equipBody].heroPart = EquipPart.None;
                heroDic[heroID].equipBody = -1;
                heroDic[heroID].equipSuitePart[3] = -1; break;
            case EquipPart.Hand:
                if (heroDic[heroID].equipHand == -1) { return; }
                itemDic[heroDic[heroID].equipHand].heroID = -1;
                itemDic[heroDic[heroID].equipHand].heroPart = EquipPart.None;
                heroDic[heroID].equipHand = -1;
                heroDic[heroID].equipSuitePart[4] = -1; break;
            case EquipPart.Back:
                if (heroDic[heroID].equipBack == -1) { return; }
                itemDic[heroDic[heroID].equipBack].heroID = -1;
                itemDic[heroDic[heroID].equipBack].heroPart = EquipPart.None;
                heroDic[heroID].equipBack = -1;
                heroDic[heroID].equipSuitePart[5] = -1; break;
            case EquipPart.Foot:
                if (heroDic[heroID].equipFoot == -1) { return; }
                itemDic[heroDic[heroID].equipFoot].heroID = -1;
                itemDic[heroDic[heroID].equipFoot].heroPart = EquipPart.None;
                heroDic[heroID].equipFoot = -1;
                heroDic[heroID].equipSuitePart[6] = -1; break;
            case EquipPart.Neck:
                if (heroDic[heroID].equipNeck == -1) { return; }
                itemDic[heroDic[heroID].equipNeck].heroID = -1;
                itemDic[heroDic[heroID].equipNeck].heroPart = EquipPart.None;
                heroDic[heroID].equipNeck = -1;
                heroDic[heroID].equipSuitePart[7] = -1; break;
            case EquipPart.Finger1:
                if (heroDic[heroID].equipFinger1 == -1) { return; }
                itemDic[heroDic[heroID].equipFinger1].heroID = -1;
                itemDic[heroDic[heroID].equipFinger1].heroPart = EquipPart.None;
                heroDic[heroID].equipFinger1 = -1;
                heroDic[heroID].equipSuitePart[8] = -1; break;
            case EquipPart.Finger2:
                if (heroDic[heroID].equipFinger2 == -1) { return; }
                itemDic[heroDic[heroID].equipFinger2].heroID = -1;
                itemDic[heroDic[heroID].equipFinger2].heroPart = EquipPart.None;
                heroDic[heroID].equipFinger2 = -1;
                heroDic[heroID].equipSuitePart[9] = -1; break;
        }
        forceDic[0].rProductNow++;
        HeroPanel.Instance.UpdateEquip(heroDic[heroID], equipPart);

        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.UpdateAllInfoToEquip(equipPart);
        }
        PlayMainPanel.Instance.UpdateButtonItemNum();
        PlayMainPanel.Instance.UpdateInventoryNum();

        AudioControl.Instance.PlaySound("system_equip");
    }

    public void HeroSkillSet(int heroID, byte index, int skillID)
    {
        if (skillID == -1)//卸下
        {
            if (forceDic[0].rProductNow >= forceDic[0].rProductLimit)
            {
                MessagePanel.Instance.AddMessage("收藏库已满");
                return;
            }

            if (heroDic[heroID].skill[index] != -1)
            {
                skillDic[heroDic[heroID].skill[index]].heroID = -1;
            }
            heroDic[heroID].skill[index] = skillID;
            forceDic[0].rProductNow++;
            PlayMainPanel.Instance.UpdateButtonSkillNum();
            PlayMainPanel.Instance.UpdateInventoryNum();
        }
        else
        {
            if (skillDic[skillID].heroID != -1)
            {
                Debug.Log("技能已被使用 skillID=" + skillID);
                return;
            }


            if (heroDic[heroID].skill[index] != -1)
            {
                skillDic[heroDic[heroID].skill[index]].heroID = -1;
            }
            else
            {
                forceDic[0].rProductNow--;
                PlayMainPanel.Instance.UpdateButtonSkillNum();
                PlayMainPanel.Instance.UpdateInventoryNum();
            }
            heroDic[heroID].skill[index] = skillID;
            skillDic[skillID].heroID = heroID;
        }

        HeroPanel.Instance.UpdateFightInfo(heroDic[heroID], EquipPart.None, null, 1);
        HeroPanel.Instance.UpdateSkill(heroDic[heroID], index);
        SkillListAndInfoPanel.Instance.OnHide();
        AudioControl.Instance.PlaySound("system_equip");
    }
    #endregion

    #region 【方法】鉴定库转收藏/放售
    public void ItemToCollectionAll(short districtID)
    {
        int freeCount = forceDic[0].rProductLimit - forceDic[0].rProductNow;
        foreach (KeyValuePair<int, ItemObject> kvp in itemDic)
        {
            if (freeCount > 0)
            {


                if (kvp.Value.districtID == districtID)
                {


                    if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == ItemTypeBig.Weapon)
                    {
                        districtDic[districtID].rProductWeapon--;
                    }
                    else if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == ItemTypeBig.Subhand)
                    {
                        districtDic[districtID].rProductWeapon--;
                    }
                    else if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == ItemTypeBig.Armor)
                    {
                        districtDic[districtID].rProductArmor--;
                    }
                    else if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == ItemTypeBig.Jewelry)
                    {
                        districtDic[districtID].rProductJewelry--;
                    }

                    itemDic[kvp.Key].districtID = -1;
                   
                    forceDic[0].rProductNow++;
                    freeCount--;
                }
            }
            else
            {
                MessagePanel.Instance.AddMessage("收藏库已满");
                break;
            }
        }
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnShow(districtID, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateButtonItemNum(districtID);
        }
        PlayMainPanel.Instance.UpdateButtonItemNum();
        PlayMainPanel.Instance.UpdateInventoryNum();
    }

    public void ItemToCollection(int itemID)
    {
        if (forceDic[0].rProductNow >= forceDic[0].rProductLimit)
        {
            MessagePanel.Instance.AddMessage("收藏库已满");
            return;
        }

        short districtID = itemDic[itemID].districtID;

        if (DataManager.mItemDict[itemDic[itemID].prototypeID].TypeBig == ItemTypeBig.Weapon)
        {
            districtDic[itemDic[itemID].districtID].rProductWeapon--;
        }
        else if (DataManager.mItemDict[itemDic[itemID].prototypeID].TypeBig == ItemTypeBig.Subhand)
        {
            districtDic[itemDic[itemID].districtID].rProductWeapon--;
        }
        else if (DataManager.mItemDict[itemDic[itemID].prototypeID].TypeBig == ItemTypeBig.Armor)
        {
            districtDic[itemDic[itemID].districtID].rProductArmor--;
        }
        else if (DataManager.mItemDict[itemDic[itemID].prototypeID].TypeBig == ItemTypeBig.Jewelry)
        {
            districtDic[itemDic[itemID].districtID].rProductJewelry--;
        }

        itemDic[itemID].districtID = -1;
        forceDic[0].rProductNow++;
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnShow(districtID, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateButtonItemNum(districtID);
        }
        PlayMainPanel.Instance.UpdateButtonItemNum();
        PlayMainPanel.Instance.UpdateInventoryNum();
    }

    public void ItemToGoodsAll(short districtID)
    {
        int j = 0;
        foreach (KeyValuePair<int, ItemObject> kvp in itemDic)
        {

            if (kvp.Value.districtID == districtID && kvp.Value.isGoods == false)
            {
                j++;
                if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == ItemTypeBig.Weapon)
                {
                    districtDic[districtID].rProductGoodWeapon++;
                }
                else if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == ItemTypeBig.Subhand)
                {
                    districtDic[districtID].rProductGoodWeapon++;
                }
                else if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == ItemTypeBig.Armor)
                {
                    districtDic[districtID].rProductGoodArmor++;
                }
                else if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == ItemTypeBig.Jewelry)
                {
                    districtDic[districtID].rProductGoodJewelry++;
                }
                kvp.Value.isGoods = true;
            }
        }

        Debug.Log(j);
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnShow(districtID, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
        DistrictMapPanel.Instance.UpdateBaselineResourcesText(districtID);
        if (PlayMainPanel.Instance.IsShowResourcesBlock)
        {
            PlayMainPanel.Instance.UpdateResourcesBlock();
        }
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateButtonItemNum(districtID);
        }
    }

    public void ItemToGoods(int itemID)
    {
        if (itemDic[itemID].isGoods == true)
        {
            return;
        }

        if (DataManager.mItemDict[itemDic[itemID].prototypeID].TypeBig == ItemTypeBig.Weapon)
        {
            districtDic[itemDic[itemID].districtID].rProductGoodWeapon++;
        }
        else if (DataManager.mItemDict[itemDic[itemID].prototypeID].TypeBig == ItemTypeBig.Subhand)
        {
            districtDic[itemDic[itemID].districtID].rProductGoodWeapon++;
        }
        else if (DataManager.mItemDict[itemDic[itemID].prototypeID].TypeBig == ItemTypeBig.Armor)
        {
            districtDic[itemDic[itemID].districtID].rProductGoodArmor++;
        }
        else if (DataManager.mItemDict[itemDic[itemID].prototypeID].TypeBig == ItemTypeBig.Jewelry)
        {
            districtDic[itemDic[itemID].districtID].rProductGoodJewelry++;
        }
        itemDic[itemID].isGoods = true;

        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnShow(itemDic[itemID].districtID, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
        DistrictMapPanel.Instance.UpdateBaselineResourcesText(itemDic[itemID].districtID);
        if (PlayMainPanel.Instance.IsShowResourcesBlock)
        {
            PlayMainPanel.Instance.UpdateResourcesBlock();
        }
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateButtonItemNum(itemDic[itemID].districtID);
        }
    }

    public void SkillToCollectionAll(short districtID)
    {
        int freeCount = forceDic[0].rProductLimit - forceDic[0].rProductNow;

        foreach (KeyValuePair<int, SkillObject> kvp in skillDic)
        {
            if (freeCount > 0)
            {
                if (kvp.Value.districtID == districtID)
                {


                    districtDic[districtID].rProductScroll--;

                    skillDic[kvp.Key].districtID = -1;
                    forceDic[0].rProductNow++;
                    freeCount--;
                }
            }
            else
            {
                MessagePanel.Instance.AddMessage("收藏库已满");
                break;
            }
        }
        if (SkillListAndInfoPanel.Instance.isShow)
        {
            SkillListAndInfoPanel.Instance.OnShow(districtID, null, (int)SkillListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)SkillListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y);
        }
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateButtonScrollNum(districtID);
        }
        PlayMainPanel.Instance.UpdateButtonSkillNum();
        PlayMainPanel.Instance.UpdateInventoryNum();
    }

    public void SkillToCollection(int skillID)
    {
        if (forceDic[0].rProductNow >= forceDic[0].rProductLimit)
        {
            MessagePanel.Instance.AddMessage("收藏库已满");
            return;
        }

        short districtID = skillDic[skillID].districtID;

        districtDic[skillDic[skillID].districtID].rProductScroll--;

        skillDic[skillID].districtID = -1;
        forceDic[0].rProductNow++;
        if (SkillListAndInfoPanel.Instance.isShow)
        {
            SkillListAndInfoPanel.Instance.OnShow(districtID, null, (int)SkillListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)SkillListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y);
        }
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateButtonScrollNum(districtID);
        }
        PlayMainPanel.Instance.UpdateButtonSkillNum();
        PlayMainPanel.Instance.UpdateInventoryNum();
    }

    public void SkillToGoodsAll(short districtID)
    {
        foreach (KeyValuePair<int, SkillObject> kvp in skillDic)
        {
            if (kvp.Value.districtID == districtID && kvp.Value.isGoods == false)
            {
                districtDic[districtID].rProductGoodScroll++;
                kvp.Value.isGoods = true;
            }
        }
        if (SkillListAndInfoPanel.Instance.isShow)
        {
            SkillListAndInfoPanel.Instance.OnShow(districtID, null, (int)SkillListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)SkillListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y);
        }
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateButtonScrollNum(districtID);
        }
    }

    public void SkillToGoods(int skillID)
    {
        if (skillID == -1)
        {
            return;
        }

        if (skillDic[skillID].isGoods == true)
        {
            return;
        }
        districtDic[skillDic[skillID].districtID].rProductGoodScroll++;
        skillDic[skillID].isGoods = true;

        if (SkillListAndInfoPanel.Instance.isShow)
        {
            SkillListAndInfoPanel.Instance.OnShow(skillDic[skillID].districtID, null, (int)SkillListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)SkillListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y);
        }
        if (DistrictMapPanel.Instance.isShow)
        {
            DistrictMapPanel.Instance.UpdateButtonScrollNum(skillDic[skillID].districtID);
        }
    }
    #endregion

    #region 【方法】直接出售
    public void ItemSales(int itemID)
    {
        if (!itemDic.ContainsKey(itemID))
        {
            MessagePanel.Instance.AddMessage("无效的物品，请重新选择");
            return;
        }

        forceDic[0].gold += itemDic[itemID].cost / 2;
        forceDic[0].rProductNow--;

        itemDic.Remove(itemID);
        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateButtonItemNum();
        PlayMainPanel.Instance.UpdateInventoryNum();
        ItemListAndInfoPanel.Instance.OnShow(-1, 76, -104,2);
    }
    
    public void ItemSalesBatch()
    {
  
        int gold = 0;
        List<int> itemObjects = new List<int>();
        foreach (KeyValuePair<int, ItemObject> kvp in itemDic)
        {
            if (kvp.Value.districtID == -1 && itemPanel_rankSelected.Contains(kvp.Value.rank) && itemPanel_levelSelected.Contains(kvp.Value.level) && itemPanel_typeSelected.Contains(DataManager.mItemDict[kvp.Value.prototypeID].TypeSmall))
            {
                gold += itemDic[kvp.Key].cost / 2;

                itemObjects.Add(kvp.Key);
                
 
            }
        }
        for (int i = 0; i < itemObjects.Count; i++)
        {
            itemDic.Remove(itemObjects[i]);
        }

        if (itemObjects.Count > 0)
        {
            forceDic[0].gold += gold;
            forceDic[0].rProductNow -= itemObjects.Count;
            MessagePanel.Instance.AddMessage("出售了符合要求的" + itemObjects.Count + "件装备,共获得金币" + gold);
            PlayMainPanel.Instance.UpdateGold();
            PlayMainPanel.Instance.UpdateButtonItemNum();
            PlayMainPanel.Instance.UpdateInventoryNum();
        }
        else
        {
            MessagePanel.Instance.AddMessage("没有符合要求的装备");
        }
        //ItemListAndInfoPanel.Instance.HideBatch();
        ItemListAndInfoPanel.Instance.OnShow(-1, 76, -104, 2);
    }

    public void ItemPanelSetRank(byte rank)
    {
        if (itemPanel_rankSelected.Contains(rank))
        {
            itemPanel_rankSelected.Remove(rank);
        }
        else
        {
            itemPanel_rankSelected.Add(rank);
        }
        ItemListAndInfoPanel.Instance.UpdateBatchRank();
    }

    public void ItemPanelSetRankAll()
    {
        if (itemPanel_rankSelected.Count == 10)
        {
            itemPanel_rankSelected.Clear();
        }
        else
        {
            itemPanel_rankSelected = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
        ItemListAndInfoPanel.Instance.UpdateBatchRank();
    }

    public void ItemPanelSetLevel(byte level)
    {
        if (itemPanel_levelSelected.Contains(level))
        {
            itemPanel_levelSelected.Remove(level);
        }
        else
        {
            itemPanel_levelSelected.Add(level);
        }
        ItemListAndInfoPanel.Instance.UpdateBatchLevel();
    }
    
    public void ItemPanelSetLevelAll()
    {
        if (itemPanel_levelSelected.Count == 11)
        {
            itemPanel_levelSelected.Clear();
        }
        else
        {
            itemPanel_levelSelected = new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
        ItemListAndInfoPanel.Instance.UpdateBatchLevel();
    }

    public void ItemPanelSetType(ItemTypeSmall type)
    {
        if (itemPanel_typeSelected.Contains(type))
        {
            itemPanel_typeSelected.Remove(type);
        }
        else
        {
            itemPanel_typeSelected.Add(type);
        }

        if (type == ItemTypeSmall.Sword ||
            type == ItemTypeSmall.Axe ||
            type == ItemTypeSmall.Spear ||
            type == ItemTypeSmall.Hammer ||
            type == ItemTypeSmall.Bow ||
            type == ItemTypeSmall.Staff)
        {
            ItemListAndInfoPanel.Instance.UpdateBatchWeapon();
        }
        else if (type == ItemTypeSmall.HeadH ||
           type == ItemTypeSmall.BodyH ||
           type == ItemTypeSmall.HandH ||
           type == ItemTypeSmall.BackH ||
           type == ItemTypeSmall.FootH ||
           type == ItemTypeSmall.HeadL ||
           type == ItemTypeSmall.BodyL ||
           type == ItemTypeSmall.HandL ||
           type == ItemTypeSmall.BackL ||
           type == ItemTypeSmall.FootL )
        {
            ItemListAndInfoPanel.Instance.UpdateBatchArmor();
        }
        else if (type == ItemTypeSmall.Shield ||
        type == ItemTypeSmall.Dorlach)
        {
            ItemListAndInfoPanel.Instance.UpdateBatchSubhand();
        }
        else if (type == ItemTypeSmall.Neck ||
        type == ItemTypeSmall.Finger)
        {
            ItemListAndInfoPanel.Instance.UpdateBatchJewelry();
        }
    }

    public void ItemPanelSetTypeWeaponAll()
    {
        if (itemPanel_typeSelected.Contains(ItemTypeSmall.Sword) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.Axe) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.Spear) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.Hammer) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.Bow) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.Staff))
        {
            itemPanel_typeSelected.Remove(ItemTypeSmall.Sword);
            itemPanel_typeSelected.Remove(ItemTypeSmall.Axe);
            itemPanel_typeSelected.Remove(ItemTypeSmall.Spear);
            itemPanel_typeSelected.Remove(ItemTypeSmall.Hammer);
            itemPanel_typeSelected.Remove(ItemTypeSmall.Bow);
            itemPanel_typeSelected.Remove(ItemTypeSmall.Staff);
        }
        else
        {
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Sword))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Sword);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Axe))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Axe);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Spear))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Spear);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Hammer))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Hammer);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Bow))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Bow);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Staff))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Staff);
            }
        }

        ItemListAndInfoPanel.Instance.UpdateBatchWeapon();
    }

    public void ItemPanelSetTypeArmorAll()
    {
        if (itemPanel_typeSelected.Contains(ItemTypeSmall.HeadH) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.BodyH) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.HandH) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.BackH) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.FootH) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.HeadL) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.BodyL) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.HandL) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.BackL) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.FootL))
        {
            itemPanel_typeSelected.Remove(ItemTypeSmall.HeadH);
            itemPanel_typeSelected.Remove(ItemTypeSmall.BodyH);
            itemPanel_typeSelected.Remove(ItemTypeSmall.HandH);
            itemPanel_typeSelected.Remove(ItemTypeSmall.BackH);
            itemPanel_typeSelected.Remove(ItemTypeSmall.FootH);
            itemPanel_typeSelected.Remove(ItemTypeSmall.HeadL);
            itemPanel_typeSelected.Remove(ItemTypeSmall.BodyL);
            itemPanel_typeSelected.Remove(ItemTypeSmall.HandL);
            itemPanel_typeSelected.Remove(ItemTypeSmall.BackL);
            itemPanel_typeSelected.Remove(ItemTypeSmall.FootL);
        }
        else
        {
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.HeadH))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.HeadH);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.BodyH))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.BodyH);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.HandH))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.HandH);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.BackH))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.BackH);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.FootH))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.FootH);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.HeadL))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.HeadL);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.BodyL))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.BodyL);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.HandL))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.HandL);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.BackL))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.BackL);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.FootL))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.FootL);
            }
        }

        ItemListAndInfoPanel.Instance.UpdateBatchArmor();
    }

    public void ItemPanelSetTypeSubhandAll()
    {
        if (itemPanel_typeSelected.Contains(ItemTypeSmall.Shield) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.Dorlach))
        {
            itemPanel_typeSelected.Remove(ItemTypeSmall.Shield);
            itemPanel_typeSelected.Remove(ItemTypeSmall.Dorlach);
        }
        else
        {
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Shield))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Shield);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Dorlach))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Dorlach);
            }

        }
        ItemListAndInfoPanel.Instance.UpdateBatchSubhand();
    }

    public void ItemPanelSetTypeJewelryAll()
    {
        if (itemPanel_typeSelected.Contains(ItemTypeSmall.Neck) &&
            itemPanel_typeSelected.Contains(ItemTypeSmall.Finger))
        {
            itemPanel_typeSelected.Remove(ItemTypeSmall.Neck);
            itemPanel_typeSelected.Remove(ItemTypeSmall.Finger);
        }
        else
        {
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Neck))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Neck);
            }
            if (!itemPanel_typeSelected.Contains(ItemTypeSmall.Finger))
            {
                itemPanel_typeSelected.Add(ItemTypeSmall.Finger);
            }
        }

        ItemListAndInfoPanel.Instance.UpdateBatchJewelry();
    }


    public void SkillSales(int skillID)
    {
        if (!skillDic.ContainsKey(skillID))
        {
            MessagePanel.Instance.AddMessage("无效的物品，请重新选择");
            return;
        }
        forceDic[0].gold += skillDic[skillID].cost / 2;
        forceDic[0].rProductNow --;
        skillDic.Remove(skillID);
        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateButtonSkillNum();
        PlayMainPanel.Instance.UpdateInventoryNum();
        SkillListAndInfoPanel.Instance.OnShow(-1, null, 76, -104);
    }

    public void SkillSalesBatch()
    {

        int gold = 0;
        List<int> itemObjects = new List<int>();
        foreach (KeyValuePair<int, SkillObject> kvp in skillDic)
        {
            if (kvp.Value.districtID == -1 && skillPanel_rankSelected.Contains(DataManager.mSkillDict[kvp.Value.prototypeID].Rank) && skillPanel_typeSelected.Contains(DataManager.mSkillDict[kvp.Value.prototypeID].TypeSmall))
            {
                gold += skillDic[kvp.Key].cost / 2;

                itemObjects.Add(kvp.Key);


            }
        }
        for (int i = 0; i < itemObjects.Count; i++)
        {
            skillDic.Remove(itemObjects[i]);
        }

        if (itemObjects.Count > 0)
        {
            forceDic[0].gold += gold;
            forceDic[0].rProductNow -= itemObjects.Count;
            MessagePanel.Instance.AddMessage("出售了符合要求的" + itemObjects.Count + "件卷轴,共获得金币" + gold);
            PlayMainPanel.Instance.UpdateGold();
            PlayMainPanel.Instance.UpdateButtonSkillNum();
            PlayMainPanel.Instance.UpdateInventoryNum();
        }
        else
        {
            MessagePanel.Instance.AddMessage("没有符合要求的卷轴");
        }
        //ItemListAndInfoPanel.Instance.HideBatch();
        SkillListAndInfoPanel.Instance.OnShow(-1, null, 76, -104);
    }

    public void SkillPanelSetRank(byte rank)
    {
        if (skillPanel_rankSelected.Contains(rank))
        {
            skillPanel_rankSelected.Remove(rank);
        }
        else
        {
            skillPanel_rankSelected.Add(rank);
        }
        SkillListAndInfoPanel.Instance.UpdateBatchRank();
    }

    public void SkillPanelSetRankAll()
    {
        if (skillPanel_rankSelected.Count == 4)
        {
            skillPanel_rankSelected.Clear();
        }
        else
        {
            skillPanel_rankSelected = new List<byte> { 1, 2, 3, 4 };
        }
        SkillListAndInfoPanel.Instance.UpdateBatchRank();
    }

    public void SkillPanelSetType(ItemTypeSmall type)
    {
        if (skillPanel_typeSelected.Contains(type))
        {
            skillPanel_typeSelected.Remove(type);
        }
        else
        {
            skillPanel_typeSelected.Add(type);
        }

        SkillListAndInfoPanel.Instance.UpdateBatchType();
    }
    
    public void SkillPanelSetTypeAll()
    {
        if (skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollNone) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollWindI) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollFireI) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollWaterI) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollGroundI) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollLightI) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollDarkI) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollWindII) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollFireII) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollWaterII) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollGroundII) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollLightII) &&
            skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollDarkII))
        {
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollNone);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollWindI);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollFireI);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollWaterI);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollGroundI);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollLightI);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollDarkI);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollWindII);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollFireII);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollWaterII);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollGroundII);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollLightII);
            skillPanel_typeSelected.Remove(ItemTypeSmall.ScrollDarkII);
        }
        else
        {
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollNone))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollNone);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollWindI))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollWindI);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollFireI))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollFireI);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollWaterI))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollWaterI);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollGroundI))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollGroundI);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollLightI))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollLightI);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollDarkI))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollDarkI);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollWindII))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollWindII);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollFireII))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollFireII);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollWaterII))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollWaterII);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollGroundII))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollGroundII);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollLightII))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollLightII);
            }
            if (!skillPanel_typeSelected.Contains(ItemTypeSmall.ScrollDarkII))
            {
                skillPanel_typeSelected.Add(ItemTypeSmall.ScrollDarkII);
            }
        }

        SkillListAndInfoPanel.Instance.UpdateBatchType();
    }

    #endregion

    #region 【方法】市集出售，顾客访客

    public void CreateSalesRecord(int year, int month)
    {
        salesRecordDic.Add(year + "/" + month, new SalesRecordObject(new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
             new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
             ));

        if (salesRecordDic.Count > 12)
        {
            salesRecordDic.Remove((year - 1) + "/" + month);
        }
    }

    public void CreateCustomerRecord(int year, int month)
    {
        customerRecordDic.Add(year + "/" + month, new CustomerRecordObject(new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));

        if (customerRecordDic.Count > 12)
        {
            customerRecordDic.Remove((year - 1) + "/" + month);
        }
    }

    public void CreateCustomer(short districtID)
    {
        string name;
        string pic;
        int sexCode = Random.Range(0, 2);
        int heroType = Random.Range(0, DataManager.mHeroDict.Count);
        if (sexCode == 0)
        {
            name = DataManager.mNameMan[Random.Range(0, DataManager.mNameMan.Length)];
            pic = DataManager.mHeroDict[heroType].PicMan[Random.Range(0, DataManager.mHeroDict[heroType].PicMan.Count)];
        }
        else
        {
            name = DataManager.mNameWoman[Random.Range(0, DataManager.mNameWoman.Length)];
            pic = DataManager.mHeroDict[heroType].PicWoman[Random.Range(0, DataManager.mHeroDict[heroType].PicWoman.Count)];
        }


        ShopType shopType = ShopType.None;
        ItemTypeBig wantBuyTypeBig = ItemTypeBig.None;
        ItemTypeSmall wantBuyTypeSmall = DataManager.mHeroDict[heroType].WantBuy[Random.Range(0, DataManager.mHeroDict[heroType].WantBuy.Count)];
        bool hesitate = true;//在犹豫中
        byte thinkTime = 3;
        short pdz = -10;
        while (hesitate && thinkTime > 0)
        {
            wantBuyTypeSmall = DataManager.mHeroDict[heroType].WantBuy[Random.Range(0, DataManager.mHeroDict[heroType].WantBuy.Count)];
            switch (wantBuyTypeSmall)
            {
                case ItemTypeSmall.Sword: if (supplyAndDemand.weaponSwordValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Hammer: if (supplyAndDemand.weaponHammerValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Spear: if (supplyAndDemand.weaponSpearValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Axe: if (supplyAndDemand.weaponAxeValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Bow: if (supplyAndDemand.weaponBowValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Staff: if (supplyAndDemand.weaponStaffValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.HeadH: if (supplyAndDemand.armorHeadHValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.HeadL: if (supplyAndDemand.armorHeadLValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.BodyH: if (supplyAndDemand.armorBodyHValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.BodyL: if (supplyAndDemand.armorBodyLValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.HandH: if (supplyAndDemand.armorHandHValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.HandL: if (supplyAndDemand.armorHandLValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.BackH: if (supplyAndDemand.armorBackHValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.BackL: if (supplyAndDemand.armorBackLValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.FootH: if (supplyAndDemand.armorFootHValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.FootL: if (supplyAndDemand.armorFootLValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Neck: if (supplyAndDemand.jewelryNeckValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Finger: if (supplyAndDemand.jewelryFingerValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Shield: if (supplyAndDemand.subhandShieldValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.Dorlach: if (supplyAndDemand.subhandDorlachValue[districtID] > pdz) { hesitate = false; } break;

                case ItemTypeSmall.ScrollWindI: if (supplyAndDemand.scrollWindIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollFireI: if (supplyAndDemand.scrollFireIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollWaterI: if (supplyAndDemand.scrollWaterIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollGroundI: if (supplyAndDemand.scrollGroundIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollLightI: if (supplyAndDemand.scrollLightIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollDarkI: if (supplyAndDemand.scrollDarkIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollNone: if (supplyAndDemand.scrollNoneValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollWindII: if (supplyAndDemand.scrollWindIIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollFireII: if (supplyAndDemand.scrollFireIIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollWaterII: if (supplyAndDemand.scrollWaterIIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollGroundII: if (supplyAndDemand.scrollGroundIIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollLightII: if (supplyAndDemand.scrollLightIIValue[districtID] > pdz) { hesitate = false; } break;
                case ItemTypeSmall.ScrollDarkII: if (supplyAndDemand.scrollDarkIIValue[districtID] > pdz) { hesitate = false; } break;

            }
            thinkTime--;
        }

        switch (wantBuyTypeSmall)
        {
            case ItemTypeSmall.Sword:
            case ItemTypeSmall.Hammer:
            case ItemTypeSmall.Spear:
            case ItemTypeSmall.Axe:
            case ItemTypeSmall.Bow:
            case ItemTypeSmall.Staff: shopType = ShopType.WeaponAndSubhand; wantBuyTypeBig = ItemTypeBig.Weapon; break;
            case ItemTypeSmall.HeadH:
            case ItemTypeSmall.HeadL:
            case ItemTypeSmall.BodyH:
            case ItemTypeSmall.BodyL:
            case ItemTypeSmall.HandH:
            case ItemTypeSmall.HandL:
            case ItemTypeSmall.BackH:
            case ItemTypeSmall.BackL:
            case ItemTypeSmall.FootH:
            case ItemTypeSmall.FootL: shopType = ShopType.Armor; wantBuyTypeBig = ItemTypeBig.Armor; break;
            case ItemTypeSmall.Neck:
            case ItemTypeSmall.Finger: shopType = ShopType.Jewelry; wantBuyTypeBig = ItemTypeBig.Jewelry; break;
            case ItemTypeSmall.Shield:
            case ItemTypeSmall.Dorlach: shopType = ShopType.WeaponAndSubhand; wantBuyTypeBig = ItemTypeBig.Subhand; break;

            case ItemTypeSmall.ScrollWindI:
            case ItemTypeSmall.ScrollFireI:
            case ItemTypeSmall.ScrollWaterI:
            case ItemTypeSmall.ScrollGroundI:
            case ItemTypeSmall.ScrollLightI:
            case ItemTypeSmall.ScrollDarkI:
            case ItemTypeSmall.ScrollNone:
            case ItemTypeSmall.ScrollWindII:
            case ItemTypeSmall.ScrollFireII:
            case ItemTypeSmall.ScrollWaterII:
            case ItemTypeSmall.ScrollGroundII:
            case ItemTypeSmall.ScrollLightII:
            case ItemTypeSmall.ScrollDarkII: shopType = ShopType.Scroll; wantBuyTypeBig = ItemTypeBig.SkillRoll; break;

        }
        BucketList bucketList = new BucketList(wantBuyTypeBig, wantBuyTypeSmall, -1, (short)Random.Range(1, 3), 0);

        int gold = Random.Range(50, 1100);
        if (gold < 200)
        {
            customerRecordDic[timeYear + "/" + timeMonth].goldPoorNum[districtID]++;
        }
        else if (gold >= 200 && gold < 500)
        {
            customerRecordDic[timeYear + "/" + timeMonth].goldNormalNum[districtID]++;
        }
        else if (gold >= 500 && gold < 1000)
        {
            customerRecordDic[timeYear + "/" + timeMonth].goldRichNum[districtID]++;
        }
        else
        {
            customerRecordDic[timeYear + "/" + timeMonth].goldVeryRichNum[districtID]++;
        }
        customerRecordDic[timeYear + "/" + timeMonth].comeNum[districtID]++;

        customerDic.Add(customerIndex, new CustomerObject(customerIndex, name, (short)heroType, pic, gold, districtID, shopType, -1, bucketList, CustomerStage.Come, 0, 50));
        CustomerChooseShop(customerIndex);

       
            DistrictMapPanel.Instance.SetCustomer(customerIndex);
           
       
        UpdateCustomerByStage(customerIndex);
        // DistrictMapPanel.Instance.CustomerCome(customerIndex);

        customerIndex++;


    }

    public void CustomerChooseShop(int customerID)
    {
        for (int i = 0; i < districtDic[customerDic[customerID].districtID].buildingList.Count; i++)
        {
            int buildingID = districtDic[customerDic[customerID].districtID].buildingList[i];
            //Debug.Log("buildingID=" + buildingID + " customerID=" + customerID);
            if (DataManager.mBuildingDict[buildingDic[buildingID].prototypeID].ShopType == customerDic[customerID].shopType)
            {
                if (buildingDic[buildingID].isSale)
                {
                    customerDic[customerID].buildingID = buildingID;
                    buildingDic[buildingID].customerList.Add(customerID);
                }

            }
        }
    }

    public void DeleteCustomer(int customerID)
    {
        //Debug.Log("customerID=" + customerID);
        //Debug.Log("customerDic[customerID].districtID=" + customerDic[customerID].districtID);
        // Debug.Log("satisfaction=" + customerRecordDic[timeYear + "/" + timeMonth].satisfaction[customerDic[customerID].districtID]);
        float nowAllSat = (float)(customerRecordDic[timeYear + "/" + timeMonth].satisfaction[customerDic[customerID].districtID] * customerRecordDic[timeYear + "/" + timeMonth].comeNum[customerDic[customerID].districtID] + customerDic[customerID].satisfaction);


        //TODO：报错
        customerRecordDic[timeYear + "/" + timeMonth].satisfaction[customerDic[customerID].districtID] = (short)(nowAllSat / (customerRecordDic[timeYear + "/" + timeMonth].comeNum[customerDic[customerID].districtID] + 1));
        if (MarketPanel.Instance.isShow && nowCheckingDistrictID == customerDic[customerID].districtID)
        {
            MarketPanel.Instance.UpdateInfo(customerDic[customerID].districtID);
        }
        customerDic.Remove(customerID);
    }
    //TODO
    public void CustomerCheckGoods(int customerID)
    {
        short districtID = customerDic[customerID].districtID;
        List<int> buyItemList = new List<int>();//实例ID
        List<int> buySkillList = new List<int>();
        int spend = 0;

        if (customerDic[customerID].shopType == ShopType.WeaponAndSubhand
            || customerDic[customerID].shopType == ShopType.Armor
            || customerDic[customerID].shopType == ShopType.Jewelry)
        {

            foreach (KeyValuePair<int, ItemObject> kvp in itemDic)
            {
                if (kvp.Value.isGoods == true && kvp.Value.districtID == districtID && kvp.Value.heroID == -1)
                {
                    //  Debug.Log("有商品");
                    if (customerDic[customerID].bucketList.prototypeID != -1)
                    {
                        if (kvp.Value.prototypeID == customerDic[customerID].bucketList.prototypeID)
                        {
                            if (customerDic[customerID].gold >= kvp.Value.cost)
                            {
                                buyItemList.Add(kvp.Value.objectID);
                                customerDic[customerID].gold -= kvp.Value.cost;
                                spend += kvp.Value.cost;
                            }

                        }
                    }
                    else if (customerDic[customerID].bucketList.typeSmall != ItemTypeSmall.None)
                    {
                        //  Debug.Log("指定了小类");
                        if (DataManager.mItemDict[kvp.Value.prototypeID].TypeSmall == customerDic[customerID].bucketList.typeSmall)
                        {
                            //  Debug.Log("符合小类");
                            if (customerDic[customerID].gold >= kvp.Value.cost)
                            {
                                buyItemList.Add(kvp.Value.objectID);
                                customerDic[customerID].gold -= kvp.Value.cost;
                                spend += kvp.Value.cost;
                            }

                        }
                    }
                    else if (customerDic[customerID].bucketList.typeBig != ItemTypeBig.None)
                    {
                        if (DataManager.mItemDict[kvp.Value.prototypeID].TypeBig == customerDic[customerID].bucketList.typeBig)
                        {
                            if (customerDic[customerID].gold >= kvp.Value.cost)
                            {
                                buyItemList.Add(kvp.Value.objectID);
                                customerDic[customerID].gold -= kvp.Value.cost;
                                spend += kvp.Value.cost;
                            }

                        }
                    }
                }
            }


        }
        else if (customerDic[customerID].shopType == ShopType.Scroll)
        {

            foreach (KeyValuePair<int, SkillObject> kvp in skillDic)
            {
                if (kvp.Value.isGoods == true && kvp.Value.districtID == districtID && kvp.Value.heroID == -1)
                {
                    if (customerDic[customerID].bucketList.prototypeID != -1)
                    {
                        if (kvp.Value.prototypeID == customerDic[customerID].bucketList.prototypeID)
                        {
                            if (customerDic[customerID].gold >= kvp.Value.cost)
                            {
                                buySkillList.Add(kvp.Value.id);
                                customerDic[customerID].gold -= kvp.Value.cost;
                                spend += kvp.Value.cost;
                            }

                        }
                    }
                    else if (customerDic[customerID].bucketList.typeSmall != ItemTypeSmall.None)
                    {
                        if (DataManager.mSkillDict[kvp.Value.prototypeID].TypeSmall == customerDic[customerID].bucketList.typeSmall)
                        {
                            if (customerDic[customerID].gold >= kvp.Value.cost)
                            {
                                buySkillList.Add(kvp.Value.id);
                                customerDic[customerID].gold -= kvp.Value.cost;
                                spend += kvp.Value.cost;
                            }

                        }
                    }

                }
            }

        }

        for (int i = 0; i < buyItemList.Count; i++)
        {
            customerDic[customerID].satisfaction += 5;
            Debug.Log("出售了" + itemDic[buyItemList[i]].name);
            if (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeBig == ItemTypeBig.Weapon)
            {
                districtDic[itemDic[buyItemList[i]].districtID].rProductWeapon--;
                districtDic[itemDic[buyItemList[i]].districtID].rProductGoodWeapon--;
                switch (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeSmall)
                {
                    case ItemTypeSmall.Sword: salesRecordDic[timeYear + "/" + timeMonth].weaponSwordNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].weaponSwordGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.Axe: salesRecordDic[timeYear + "/" + timeMonth].weaponAxeNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].weaponAxeGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.Spear: salesRecordDic[timeYear + "/" + timeMonth].weaponSpearNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].weaponSpearGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.Hammer: salesRecordDic[timeYear + "/" + timeMonth].weaponHammerNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].weaponHammerGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.Bow: salesRecordDic[timeYear + "/" + timeMonth].weaponBowNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].weaponBowGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.Staff: salesRecordDic[timeYear + "/" + timeMonth].weaponStaffNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].weaponStaffGold[districtID] += itemDic[buyItemList[i]].cost; break;
                }
            }
            else if (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeBig == ItemTypeBig.Subhand)
            {
                districtDic[itemDic[buyItemList[i]].districtID].rProductWeapon--;
                districtDic[itemDic[buyItemList[i]].districtID].rProductGoodWeapon--;
                switch (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeSmall)
                {
                    case ItemTypeSmall.Shield: salesRecordDic[timeYear + "/" + timeMonth].subhandShieldNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].subhandShieldGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.Dorlach: salesRecordDic[timeYear + "/" + timeMonth].subhandDorlachNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].subhandDorlachGold[districtID] += itemDic[buyItemList[i]].cost; break;
                }
            }
            else if (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeBig == ItemTypeBig.Armor)
            {
                districtDic[itemDic[buyItemList[i]].districtID].rProductArmor--;
                districtDic[itemDic[buyItemList[i]].districtID].rProductGoodArmor--;
                switch (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeSmall)
                {
                    case ItemTypeSmall.HeadH: salesRecordDic[timeYear + "/" + timeMonth].armorHeadHNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorHeadHGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.BodyH: salesRecordDic[timeYear + "/" + timeMonth].armorBodyHNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorBodyHGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.HandH: salesRecordDic[timeYear + "/" + timeMonth].armorHandHNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorHandHGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.BackH: salesRecordDic[timeYear + "/" + timeMonth].armorBackHNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorBackHGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.FootH: salesRecordDic[timeYear + "/" + timeMonth].armorFootHNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorFootHGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.HeadL: salesRecordDic[timeYear + "/" + timeMonth].armorHeadLNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorHeadLGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.BodyL: salesRecordDic[timeYear + "/" + timeMonth].armorBodyLNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorBodyLGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.HandL: salesRecordDic[timeYear + "/" + timeMonth].armorHandLNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorHandLGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.BackL: salesRecordDic[timeYear + "/" + timeMonth].armorBackLNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorBackLGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.FootL: salesRecordDic[timeYear + "/" + timeMonth].armorFootLNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].armorFootLGold[districtID] += itemDic[buyItemList[i]].cost; break;
                }
            }
            else if (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeBig == ItemTypeBig.Jewelry)
            {
                districtDic[itemDic[buyItemList[i]].districtID].rProductJewelry--;
                districtDic[itemDic[buyItemList[i]].districtID].rProductGoodJewelry--;
                switch (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeSmall)
                {
                    case ItemTypeSmall.Neck: salesRecordDic[timeYear + "/" + timeMonth].jewelryNeckNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].jewelryNeckGold[districtID] += itemDic[buyItemList[i]].cost; break;
                    case ItemTypeSmall.Finger: salesRecordDic[timeYear + "/" + timeMonth].jewelryFingerNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].jewelryFingerGold[districtID] += itemDic[buyItemList[i]].cost; break;
                }
            }
            SupplyAndDemandChange(districtID, DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeSmall, Random.Range(0, 3) * -1);
            if (SupplyAndDemandPanel.Instance.isShow && nowCheckingDistrictID == districtID)
            {
                SupplyAndDemandPanel.Instance.UpdateSingle(districtID, DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeSmall);
            }
            itemDic.Remove(buyItemList[i]);
        }
        for (int i = 0; i < buySkillList.Count; i++)
        {
            customerDic[customerID].satisfaction += 5;
            //Debug.Log("出售了" + skillDic[buySkillList[i]].name);
            districtDic[skillDic[buySkillList[i]].districtID].rProductScroll--;
            districtDic[skillDic[buySkillList[i]].districtID].rProductGoodScroll--;
            switch (DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeSmall)
            {
                case ItemTypeSmall.ScrollWindI: salesRecordDic[timeYear + "/" + timeMonth].scrollWindINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollWindIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollFireI: salesRecordDic[timeYear + "/" + timeMonth].scrollFireINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollFireIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollWaterI: salesRecordDic[timeYear + "/" + timeMonth].scrollWaterINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollWaterIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollGroundI: salesRecordDic[timeYear + "/" + timeMonth].scrollGroundINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollGroundIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollLightI: salesRecordDic[timeYear + "/" + timeMonth].scrollLightINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollLightIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollDarkI: salesRecordDic[timeYear + "/" + timeMonth].scrollDarkINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollDarkIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollNone: salesRecordDic[timeYear + "/" + timeMonth].scrollNoneNum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollNoneGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollWindII: salesRecordDic[timeYear + "/" + timeMonth].scrollWindIINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollWindIIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollFireII: salesRecordDic[timeYear + "/" + timeMonth].scrollFireIINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollFireIIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollWaterII: salesRecordDic[timeYear + "/" + timeMonth].scrollWaterIINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollWaterIIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollGroundII: salesRecordDic[timeYear + "/" + timeMonth].scrollGroundIINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollGroundIIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollLightII: salesRecordDic[timeYear + "/" + timeMonth].scrollLightIINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollLightIIGold[districtID] += itemDic[buyItemList[i]].cost; break;
                case ItemTypeSmall.ScrollDarkII: salesRecordDic[timeYear + "/" + timeMonth].scrollDarkIINum[districtID]++; salesRecordDic[timeYear + "/" + timeMonth].scrollDarkIIGold[districtID] += itemDic[buyItemList[i]].cost; break;

            }
            SupplyAndDemandChange(districtID, DataManager.mSkillDict[skillDic[buySkillList[i]].prototypeID].TypeSmall, Random.Range(0, 3) * -1);
            if (SupplyAndDemandPanel.Instance.isShow && nowCheckingDistrictID == districtID)
            {
                SupplyAndDemandPanel.Instance.UpdateSingle(districtID, DataManager.mItemDict[itemDic[buyItemList[i]].prototypeID].TypeSmall);
            }
            skillDic.Remove(buySkillList[i]);
        }

        DistrictSetProsperousByGoodsTax(districtID, (20 - districtDic[districtID].taxGoods) / 10);
        DistrictSetProsperous(districtID);

        forceDic[districtDic[districtID].force].gold += (int)(spend * (1f + districtDic[districtID].taxGoods / 100f));
        if (districtDic[districtID].force == 0)
        {
            PlayMainPanel.Instance.UpdateGold();
        }

        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        {
            DistrictMapPanel.Instance.UpdateBaselineResourcesText(districtID);
           
        }
        if (MarketPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        {
            MarketPanel.Instance.UpdateAllInfo(districtID, MarketPanel.Instance.itemTypeBig, MarketPanel.Instance.itemTypeSmall);
        }
        if (PlayMainPanel.Instance.IsShowResourcesBlock)
        {
            PlayMainPanel.Instance.UpdateResourcesBlock();
        }

        if ((buyItemList.Count + buySkillList.Count) == 0)
        {
            customerDic[customerID].satisfaction -= 20;
            //Debug.Log(customerDic[customerID].name + "啥都没买回去了");
            switch (customerDic[customerID].bucketList.typeSmall)
            {
                case ItemTypeSmall.Sword: supplyAndDemand.weaponSwordValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Hammer: supplyAndDemand.weaponHammerValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Spear: supplyAndDemand.weaponSpearValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Axe: supplyAndDemand.weaponAxeValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Bow: supplyAndDemand.weaponBowValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Staff: supplyAndDemand.weaponStaffValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.HeadH: supplyAndDemand.armorHeadHValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.HeadL: supplyAndDemand.armorHeadLValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.BodyH: supplyAndDemand.armorBodyHValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.BodyL: supplyAndDemand.armorBodyLValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.HandH: supplyAndDemand.armorHandHValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.HandL: supplyAndDemand.armorHandLValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.BackH: supplyAndDemand.armorBackHValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.BackL: supplyAndDemand.armorBackLValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.FootH: supplyAndDemand.armorFootHValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.FootL: supplyAndDemand.armorFootLValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Neck: supplyAndDemand.jewelryNeckValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Finger: supplyAndDemand.jewelryFingerValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Shield: supplyAndDemand.subhandShieldValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.Dorlach: supplyAndDemand.subhandDorlachValue[districtID] += (short)Random.Range(0, 2); break;

                case ItemTypeSmall.ScrollWindI: supplyAndDemand.scrollWindIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollFireI: supplyAndDemand.scrollFireIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollWaterI: supplyAndDemand.scrollWaterIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollGroundI: supplyAndDemand.scrollGroundIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollLightI: supplyAndDemand.scrollLightIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollDarkI: supplyAndDemand.scrollDarkIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollNone: supplyAndDemand.scrollNoneValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollWindII: supplyAndDemand.scrollWindIIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollFireII: supplyAndDemand.scrollFireIIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollWaterII: supplyAndDemand.scrollWaterIIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollGroundII: supplyAndDemand.scrollGroundIIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollLightII: supplyAndDemand.scrollLightIIValue[districtID] += (short)Random.Range(0, 2); break;
                case ItemTypeSmall.ScrollDarkII: supplyAndDemand.scrollDarkIIValue[districtID] += (short)Random.Range(0, 2); break;

            }


        }
        else
        {

            switch (DataManager.mBuildingDict[buildingDic[customerDic[customerID].buildingID].prototypeID].ShopType)
            {
                case ShopType.WeaponAndSubhand: customerRecordDic[timeYear + "/" + timeMonth].buyWeaponNum[customerDic[customerID].districtID]++; break;
                case ShopType.Armor: customerRecordDic[timeYear + "/" + timeMonth].buyArmorNum[customerDic[customerID].districtID]++; break;
                case ShopType.Jewelry: customerRecordDic[timeYear + "/" + timeMonth].buyJewelryNum[customerDic[customerID].districtID]++; break;
                case ShopType.Scroll: customerRecordDic[timeYear + "/" + timeMonth].buyScrollNum[customerDic[customerID].districtID]++; break;
            }
            customerRecordDic[timeYear + "/" + timeMonth].buyNum[customerDic[customerID].districtID]++;
        }


        customerRecordDic[timeYear + "/" + timeMonth].goToShopNum[customerDic[customerID].districtID]++;

        switch (DataManager.mBuildingDict[buildingDic[customerDic[customerID].buildingID].prototypeID].ShopType)
        {
            case ShopType.WeaponAndSubhand: customerRecordDic[timeYear + "/" + timeMonth].goToShopWeaponNum[customerDic[customerID].districtID]++; break;
            case ShopType.Armor: customerRecordDic[timeYear + "/" + timeMonth].goToShopArmorNum[customerDic[customerID].districtID]++; break;
            case ShopType.Jewelry: customerRecordDic[timeYear + "/" + timeMonth].goToShopJewelryNum[customerDic[customerID].districtID]++; break;
            case ShopType.Scroll: customerRecordDic[timeYear + "/" + timeMonth].goToShopScrollNum[customerDic[customerID].districtID]++; break;
        }

        string str = "";
        if ((buyItemList.Count + buySkillList.Count) == 0)
        {
            str = OutputRandomStr(new List<string> { "没有合适的", "空手而回", "", "" });
        }
        else
        {
            int ran2 = Random.Range(0, 3);
            if ((buyItemList.Count + buySkillList.Count) == 1)
            {
                ran2 = 1;
            }
            if (ran2 == 0)
            {
                str = OutputRandomStr(new List<string> { "这次买了" + (buyItemList.Count + buySkillList.Count) + "件", "满载而归", "钱花光了", "买买买", "" });
            }
            else
            {
                switch (customerDic[customerID].shopType)
                {
                    case ShopType.WeaponAndSubhand:
                        if (customerDic[customerID].bucketList.prototypeID != -1)
                        {
                            int ran = Random.Range(0, 2);
                            if (ran == 0)
                            {
                                str = OutputRandomStr(new List<string> { "买下了" + DataManager.mItemDict[customerDic[customerID].bucketList.prototypeID].Name, "", "" });
                            }
                            else
                            {
                                switch (customerDic[customerID].bucketList.typeSmall)
                                {
                                    case ItemTypeSmall.Sword: str = OutputRandomStr(new List<string> { "好剑", "好剑好剑", "", "" }); break;
                                    case ItemTypeSmall.Axe: str = OutputRandomStr(new List<string> { "这斧头很锋利", "哈", "", "" }); break;
                                    case ItemTypeSmall.Spear: str = OutputRandomStr(new List<string> { "好像是刚锻造的", "突刺", "", "" }); break;
                                    case ItemTypeSmall.Hammer: str = OutputRandomStr(new List<string> { "好重", "给你一棒槌", "", "" }); break;
                                    case ItemTypeSmall.Bow: str = OutputRandomStr(new List<string> { "狩猎利器", "这弓似乎有灵性", "", "" }); break;
                                    case ItemTypeSmall.Staff: str = OutputRandomStr(new List<string> { "法力无边", "走上魔法使之路", "", "" }); break;
                                    case ItemTypeSmall.Shield: str = OutputRandomStr(new List<string> { "这下安心多了", "坚固的盾", "", "" }); break;
                                    case ItemTypeSmall.Dorlach: str = OutputRandomStr(new List<string> { "可以多带几支箭了", "哈", "", "" }); break;
                                }
                            }
                        }
                        else
                        {
                            switch (customerDic[customerID].bucketList.typeSmall)
                            {
                                case ItemTypeSmall.Sword: str = OutputRandomStr(new List<string> { "好剑", "好剑好剑", "", "" }); break;
                                case ItemTypeSmall.Axe: str = OutputRandomStr(new List<string> { "这斧头很锋利", "哈", "", "" }); break;
                                case ItemTypeSmall.Spear: str = OutputRandomStr(new List<string> { "好像是刚锻造的", "突刺", "", "" }); break;
                                case ItemTypeSmall.Hammer: str = OutputRandomStr(new List<string> { "好重", "给你一棒槌", "", "" }); break;
                                case ItemTypeSmall.Bow: str = OutputRandomStr(new List<string> { "狩猎利器", "这弓似乎有灵性", "", "" }); break;
                                case ItemTypeSmall.Staff: str = OutputRandomStr(new List<string> { "法力无边", "走上魔法使之路", "", "" }); break;
                                case ItemTypeSmall.Shield: str = OutputRandomStr(new List<string> { "这下安心多了", "坚固的盾", "", "" }); break;
                                case ItemTypeSmall.Dorlach: str = OutputRandomStr(new List<string> { "可以多带几支箭了", "哈", "", "" }); break;
                            }
                        }
                        break;
                    case ShopType.Armor:
                        if (customerDic[customerID].bucketList.prototypeID != -1)
                        {
                            int ran = Random.Range(0, 2);
                            if (ran == 0)
                            {
                                str = OutputRandomStr(new List<string> { "买下了" + DataManager.mItemDict[customerDic[customerID].bucketList.prototypeID].Name, "", "" });
                            }
                            else
                            {
                                switch (customerDic[customerID].bucketList.typeSmall)
                                {
                                    case ItemTypeSmall.HeadH: str = OutputRandomStr(new List<string> { "好重的头盔啊", "变得更耐打了", "", "" }); break;
                                    case ItemTypeSmall.BodyH: str = OutputRandomStr(new List<string> { "厚实的护甲", "变得更耐打了", "", "" }); break;
                                    case ItemTypeSmall.HandH: str = OutputRandomStr(new List<string> { "坚固的护腕", "变得更耐打了", "", "" }); break;
                                    case ItemTypeSmall.BackH: str = OutputRandomStr(new List<string> { "很拉风的披风", "变得更耐打了", "", "" }); break;
                                    case ItemTypeSmall.FootH: str = OutputRandomStr(new List<string> { "坚固的战靴", "变得更耐打了", "", "" }); break;
                                    case ItemTypeSmall.HeadL: str = OutputRandomStr(new List<string> { "轻巧的帽子", "魔法防御加强了", "", "" }); break;
                                    case ItemTypeSmall.BodyL: str = OutputRandomStr(new List<string> { "不错的衣服", "魔法防御加强了", "", "" }); break;
                                    case ItemTypeSmall.HandL: str = OutputRandomStr(new List<string> { "很暖和的手套", "魔法防御加强了", "", "" }); break;
                                    case ItemTypeSmall.BackL: str = OutputRandomStr(new List<string> { "这斗篷很好", "魔法防御加强了", "", "" }); break;
                                    case ItemTypeSmall.FootL: str = OutputRandomStr(new List<string> { "很合适的鞋子", "魔法防御加强了", "", "" }); break;
                                }
                            }
                        }
                        else
                        {
                            switch (customerDic[customerID].bucketList.typeSmall)
                            {
                                case ItemTypeSmall.HeadH: str = OutputRandomStr(new List<string> { "好重的头盔啊", "变得更耐打了", "", "" }); break;
                                case ItemTypeSmall.BodyH: str = OutputRandomStr(new List<string> { "厚实的护甲", "变得更耐打了", "", "" }); break;
                                case ItemTypeSmall.HandH: str = OutputRandomStr(new List<string> { "坚固的护腕", "变得更耐打了", "", "" }); break;
                                case ItemTypeSmall.BackH: str = OutputRandomStr(new List<string> { "很拉风的披风", "变得更耐打了", "", "" }); break;
                                case ItemTypeSmall.FootH: str = OutputRandomStr(new List<string> { "坚固的战靴", "变得更耐打了", "", "" }); break;
                                case ItemTypeSmall.HeadL: str = OutputRandomStr(new List<string> { "轻巧的帽子", "魔法防御加强了", "", "" }); break;
                                case ItemTypeSmall.BodyL: str = OutputRandomStr(new List<string> { "不错的衣服", "魔法防御加强了", "", "" }); break;
                                case ItemTypeSmall.HandL: str = OutputRandomStr(new List<string> { "很暖和的手套", "魔法防御加强了", "", "" }); break;
                                case ItemTypeSmall.BackL: str = OutputRandomStr(new List<string> { "这斗篷很好", "魔法防御加强了", "", "" }); break;
                                case ItemTypeSmall.FootL: str = OutputRandomStr(new List<string> { "很合适的鞋子", "魔法防御加强了", "", "" }); break;
                            }
                        }
                        break;
                    case ShopType.Jewelry:
                        if (customerDic[customerID].bucketList.prototypeID != -1)
                        {
                            int ran = Random.Range(0, 2);
                            if (ran == 0)
                            {
                                str = OutputRandomStr(new List<string> { "买下了" + DataManager.mItemDict[customerDic[customerID].bucketList.prototypeID].Name, "", "" });
                            }
                            else
                            {
                                switch (customerDic[customerID].bucketList.typeSmall)
                                {
                                    case ItemTypeSmall.Neck: str = OutputRandomStr(new List<string> { "漂亮的项链", "漂亮的勋章", "", "" }); break;
                                    case ItemTypeSmall.Finger: str = OutputRandomStr(new List<string> { "闪亮闪亮的", "不错的指环", "", "" }); break;
                                }
                            }
                        }
                        else
                        {
                            switch (customerDic[customerID].bucketList.typeSmall)
                            {
                                case ItemTypeSmall.Neck: str = OutputRandomStr(new List<string> { "漂亮的项链", "漂亮的勋章", "", "" }); break;
                                case ItemTypeSmall.Finger: str = OutputRandomStr(new List<string> { "闪亮闪亮的", "不错的指环", "", "" }); break;

                            }
                        }
                        break;


                    case ShopType.Scroll: str = "感受到了魔力"; break;
                }
            }

        }

        StartCoroutine(DistrictMapPanel.Instance.CustomerTalk(customerID, str, 1.8f));


        //StartCoroutine(DistrictMapPanel.Instance.CustomerGoToBuilding(customerID, str));
    }

    //迁移的部分
    public void UpdateCustomerByStageUI(int customerID)
    {
        // GameObject go = GameObject.Find("Canvas/DistrictMapPanel/Parts/Viewport/Content/" + gc.customerDic[customerID].layer + "/Customer_" + customerID);
        GameObject go =DistrictMapPanel.Instance.customerGoDic[customerID];
        switch (customerDic[customerID].stage)
        {
            case CustomerStage.Come:
                StartCoroutine(CustomerComeUI(customerID, go));
                break;
            case CustomerStage.Observe:
                StartCoroutine(CustomerObserveUI(customerID, go));
                break;
            case CustomerStage.Wait:
                StartCoroutine(CustomerWaitUI(customerID, go));
                break;
            case CustomerStage.IntoShop:
                StartCoroutine(CustomerIntoShopUI(customerID, go));
                break;
            case CustomerStage.Gone:
                StartCoroutine(CustomerGoneUI(customerID, go));
                break;
        }
    }

    public void UpdateCustomerByStage(int customerID)
    {
        // Debug.Log("[" + customerID + ":" + gc.customerDic[customerID].name + "]" + gc.customerDic[customerID].stage + "[" + DataManager.mDistrictDict[gc.customerDic[customerID].districtID].Name + "]");

        switch (customerDic[customerID].stage)
        {
            case CustomerStage.Come:
                StartCoroutine(CustomerCome(customerID));
                break;
            case CustomerStage.Observe:
                StartCoroutine(CustomerObserve(customerID));
                break;
            case CustomerStage.Wait:
                StartCoroutine(CustomerWait(customerID));
                break;
            case CustomerStage.IntoShop:
                StartCoroutine(CustomerIntoShop(customerID));
                break;
            case CustomerStage.Gone:
                StartCoroutine(CustomerGone(customerID));
                break;
        }
    }

    IEnumerator CustomerComeUI(int customerID, GameObject go)
    {

        Vector2 startPos = go.transform.localPosition;
        Vector2 targetPos;

        if (customerDic[customerID].buildingID != -1)
        {
            int buildingID = customerDic[customerID].buildingID;

            targetPos = new Vector2((buildingDic[buildingID].positionX + DataManager.mBuildingDict[buildingDic[buildingID].prototypeID].DoorPosition + (buildingDic[buildingID].positionY < 64 ? (1 + buildingDic[buildingID].customerList.Count) * -1 : buildingDic[buildingID].customerList.Count)) * 16f, customerDic[customerID].layer * -16f + roleHeight);

            go.GetComponent<AnimatiorControlByCustomer>().SetAnim((startPos.x > targetPos.x) ? AnimStatus.WalkLeft : AnimStatus.WalkRight);
            go.transform.DOLocalMove(targetPos, 5f);

            yield return new WaitForSeconds(5f);
            go.transform.DOComplete();

            go.GetComponent<AnimatiorControlByCustomer>().SetAnim(buildingDic[buildingID].doorInLine);
            go.GetComponent<AnimatiorControlByCustomer>().Stop();

        }
        else
        {
            targetPos = new Vector2(Random.Range(54, 74) * 16f, customerDic[customerID].layer * -16f + roleHeight);

            go.GetComponent<RectTransform>().anchoredPosition = startPos;

            go.GetComponent<AnimatiorControlByCustomer>().SetAnim((startPos.x > targetPos.x) ? AnimStatus.WalkLeft : AnimStatus.WalkRight);
            go.transform.DOLocalMove(targetPos, 5f);

            yield return new WaitForSeconds(5f);
            go.transform.DOComplete();
        }
    }

    IEnumerator CustomerCome(int customerID)
    {
        UpdateCustomerByStageUI(customerID);

        if (customerDic[customerID].buildingID != -1)
        {
            yield return new WaitForSeconds(5f);
            customerDic[customerID].stage = CustomerStage.Wait;
            UpdateCustomerByStage(customerID);
        }
        else
        {
            yield return new WaitForSeconds(5f);
            customerDic[customerID].stage = CustomerStage.Observe;
            UpdateCustomerByStage(customerID);
        }
    }

    IEnumerator CustomerWaitUI(int customerID, GameObject go)
    {
        go.transform.DOComplete();
        int buildingID =customerDic[customerID].buildingID;
        int waitIndex = buildingDic[buildingID].customerList.IndexOf(customerID);
        Vector2 startPos = go.transform.localPosition;
        Vector2 targetPos = new Vector2((buildingDic[buildingID].positionX + DataManager.mBuildingDict[buildingDic[buildingID].prototypeID].DoorPosition + (buildingDic[buildingID].positionY < 64 ? (1 + waitIndex) * -1 : waitIndex)) * 16f, customerDic[customerID].layer * -16f + roleHeight);

        go.GetComponent<AnimatiorControlByCustomer>().SetAnim((startPos.x > targetPos.x) ? AnimStatus.WalkLeft : AnimStatus.WalkRight);
        //go.GetComponent<AnimatiorControlByNPC>().Play();
        go.transform.DOLocalMove(targetPos, 1f);
        yield return new WaitForSeconds(1f);
        go.transform.DOComplete();
        go.GetComponent<AnimatiorControlByCustomer>().Stop();
    }

    IEnumerator CustomerWait(int customerID)
    {
        UpdateCustomerByStageUI(customerID);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator CustomerObserveUI(int customerID, GameObject go)
    {
        customerRecordDic[timeYear + "/" + timeMonth].backNum[customerDic[customerID].districtID]++;

        go.transform.DOComplete();
        go.GetComponent<AnimatiorControlByCustomer>().Stop();


        yield return new WaitForSeconds(2.0f);

        string str = "";
        switch (customerDic[customerID].shopType)
        {
            case ShopType.WeaponAndSubhand: str = "这里没有武器店吗？"; break;
            case ShopType.Armor: str = "这里没有防具店吗？"; break;
            case ShopType.Jewelry: str = "这里没有饰品店吗？"; break;
            case ShopType.Scroll: str = "这里没有卷轴店吗？"; break;
        }
        StartCoroutine(DistrictMapPanel.Instance.CustomerTalk(customerID, OutputRandomStr(new List<string> { "没有合适的店啊", "icon_talk_sad", "白来一趟了", "好荒凉啊", str }), 0f));
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator CustomerObserve(int customerID)
    {
        UpdateCustomerByStageUI(customerID);
        yield return new WaitForSeconds(2.5f);

        customerDic[customerID].stage = CustomerStage.Gone;
        UpdateCustomerByStage(customerID);
    }

    IEnumerator CustomerIntoShopUI(int customerID, GameObject go)
    {
        go.transform.DOComplete();
        int buildingID = customerDic[customerID].buildingID;
        float doorPosX = (buildingDic[buildingID].positionX + DataManager.mBuildingDict[buildingDic[buildingID].prototypeID].DoorPosition) * 16f;

        if (go.transform.localPosition.x > doorPosX)
        {
            go.GetComponent<AnimatiorControlByCustomer>().SetAnim(AnimStatus.WalkLeft);
            go.transform.DOLocalMove(go.transform.localPosition + Vector3.left * (go.transform.localPosition.x - doorPosX), 1f);
        }
        else
        {
            go.GetComponent<AnimatiorControlByCustomer>().SetAnim(AnimStatus.WalkRight);
            go.transform.DOLocalMove(go.transform.localPosition + Vector3.right * (doorPosX - go.transform.localPosition.x - 8f), 1f);
        }
        yield return new WaitForSeconds(1f);

        go.GetComponent<AnimatiorControlByCustomer>().SetAnim(AnimStatus.WalkUp);
        go.transform.DOLocalMove(go.transform.localPosition + Vector3.up * 10f, 1f);
        yield return new WaitForSeconds(1f);
        go.GetComponent<AnimatiorControlByCustomer>().SetAnim(AnimStatus.WalkDown);
        go.transform.DOLocalMove(go.transform.localPosition + Vector3.down * 10f, 1f);

        yield return new WaitForSeconds(1f);
    }

    IEnumerator CustomerIntoShop(int customerID)
    {
        UpdateCustomerByStageUI(customerID);
        yield return new WaitForSeconds(3f);
        customerDic[customerID].stage = CustomerStage.Gone;
        UpdateCustomerByStage(customerID);
    }

    IEnumerator CustomerGoneUI(int customerID, GameObject go)
    {
        go.transform.DOComplete();
        int ran = Random.Range(0, 2);

        go.GetComponent<AnimatiorControlByCustomer>().SetAnim(ran == 0 ? AnimStatus.WalkLeft : AnimStatus.WalkRight);
        go.transform.DOLocalMove(new Vector2(ran == 0 ? 0 : 2008, go.transform.localPosition.y), 10f);
        yield return new WaitForSeconds(10f);
        go.transform.DOComplete();
        go.GetComponent<AnimatiorControlByCustomer>().Stop();
        go.GetComponent<AnimatiorControlByCustomer>().enabled = false;
        DistrictMapPanel.Instance.customerGoPool.Add(go);
        DistrictMapPanel.Instance.customerGoDic.Remove(customerID);
    }

    IEnumerator CustomerGone(int customerID)
    {
        UpdateCustomerByStageUI(customerID);
        yield return new WaitForSeconds(10f);
        DeleteCustomer(customerID);
    }





    public void SupplyAndDemandChangeRegular(short districtID)
    {


        supplyAndDemand.weaponSwordValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponSwordValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.weaponHammerValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponHammerValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.weaponSpearValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponSpearValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.weaponAxeValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponAxeValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.weaponBowValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponBowValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.weaponStaffValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponStaffValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorHeadHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorHeadHValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorHeadLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorHeadLValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorBodyHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorBodyHValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorBodyLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorBodyLValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorHandHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorHandHValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorHandLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorHandLValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorBackHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorBackHValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorBackLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorBackLValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorFootHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorFootHValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.armorFootLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorFootLValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.jewelryNeckValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.jewelryNeckValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.jewelryFingerValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.jewelryFingerValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.subhandShieldValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.subhandShieldValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.subhandDorlachValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.subhandDorlachValue[districtID] += (short)Random.Range(-4, 7)));

        supplyAndDemand.scrollWindIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollWindIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollFireIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollFireIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollWaterIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollWaterIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollGroundIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollGroundIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollLightIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollLightIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollDarkIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollDarkIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollNoneValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollNoneValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollWindIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollWindIIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollFireIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollFireIIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollWaterIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollWaterIIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollGroundIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollGroundIIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollLightIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollLightIIValue[districtID] += (short)Random.Range(-4, 7)));
        supplyAndDemand.scrollDarkIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollDarkIIValue[districtID] += (short)Random.Range(-4, 7)));

    }

    void SupplyAndDemandChange(short districtID, ItemTypeSmall itemTypeSmall, int value)
    {
        switch (itemTypeSmall)
        {
            case ItemTypeSmall.Sword: supplyAndDemand.weaponSwordValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponSwordValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Hammer: supplyAndDemand.weaponHammerValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponHammerValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Spear: supplyAndDemand.weaponSpearValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponSpearValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Axe: supplyAndDemand.weaponAxeValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponAxeValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Bow: supplyAndDemand.weaponBowValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponBowValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Staff: supplyAndDemand.weaponStaffValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.weaponStaffValue[districtID] += (short)value)); break;
            case ItemTypeSmall.HeadH: supplyAndDemand.armorHeadHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorHeadHValue[districtID] += (short)value)); break;
            case ItemTypeSmall.HeadL: supplyAndDemand.armorHeadLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorHeadLValue[districtID] += (short)value)); break;
            case ItemTypeSmall.BodyH: supplyAndDemand.armorBodyHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorBodyHValue[districtID] += (short)value)); break;
            case ItemTypeSmall.BodyL: supplyAndDemand.armorBodyLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorBodyLValue[districtID] += (short)value)); break;
            case ItemTypeSmall.HandH: supplyAndDemand.armorHandHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorHandHValue[districtID] += (short)value)); break;
            case ItemTypeSmall.HandL: supplyAndDemand.armorHandLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorHandLValue[districtID] += (short)value)); break;
            case ItemTypeSmall.BackH: supplyAndDemand.armorBackHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorBackHValue[districtID] += (short)value)); break;
            case ItemTypeSmall.BackL: supplyAndDemand.armorBackLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorBackLValue[districtID] += (short)value)); break;
            case ItemTypeSmall.FootH: supplyAndDemand.armorFootHValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorFootHValue[districtID] += (short)value)); break;
            case ItemTypeSmall.FootL: supplyAndDemand.armorFootLValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.armorFootLValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Neck: supplyAndDemand.jewelryNeckValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.jewelryNeckValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Finger: supplyAndDemand.jewelryFingerValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.jewelryFingerValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Shield: supplyAndDemand.subhandShieldValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.subhandShieldValue[districtID] += (short)value)); break;
            case ItemTypeSmall.Dorlach: supplyAndDemand.subhandDorlachValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.subhandDorlachValue[districtID] += (short)value)); break;

            case ItemTypeSmall.ScrollWindI: supplyAndDemand.scrollWindIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollWindIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollFireI: supplyAndDemand.scrollFireIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollFireIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollWaterI: supplyAndDemand.scrollWaterIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollWaterIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollGroundI: supplyAndDemand.scrollGroundIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollGroundIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollLightI: supplyAndDemand.scrollLightIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollLightIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollDarkI: supplyAndDemand.scrollDarkIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollDarkIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollNone: supplyAndDemand.scrollNoneValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollNoneValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollWindII: supplyAndDemand.scrollWindIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollWindIIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollFireII: supplyAndDemand.scrollFireIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollFireIIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollWaterII: supplyAndDemand.scrollWaterIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollWaterIIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollGroundII: supplyAndDemand.scrollGroundIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollGroundIIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollLightII: supplyAndDemand.scrollLightIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollLightIIValue[districtID] += (short)value)); break;
            case ItemTypeSmall.ScrollDarkII: supplyAndDemand.scrollDarkIIValue[districtID] = System.Math.Min((short)100, System.Math.Max((short)-100, supplyAndDemand.scrollDarkIIValue[districtID] += (short)value)); break;

        }

    }
    #endregion

    #region 【方法】英雄访客
    public void CreateRecruiter(short districtID)
    {
        if (districtDic[districtID].recruitList.Count < 5)
        {
            int heroID = heroIndex;
            CreateHero((short)Random.Range(0, DataManager.mHeroDict.Count), districtID, -1);
            districtDic[districtID].recruitList.Add(heroID);
        }
  
    }
    #endregion

    #region 【方法】冒险
    void AdventureTeamLogAdd(byte teamID, string str)
    {
        adventureTeamList[teamID].log.Add(str);
        AdventureMainPanel.Instance.UpdateLogContent(teamID);
    }


    public void AdventureTeamSetDungeon(byte teamID, short dungeonID)
    {

        adventureTeamList[teamID].dungeonID = dungeonID;
        adventureTeamList[teamID].scenePicList.Clear();
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                adventureTeamList[teamID].scenePicList.Add(DataManager.mDungeonDict[dungeonID].ScenePic[0]);
            }
            else
            {
                adventureTeamList[teamID].scenePicList.Add(DataManager.mDungeonDict[dungeonID].ScenePic[Random.Range(0, DataManager.mDungeonDict[dungeonID].ScenePic.Count - 1)]);
            }

        }
    }

    public void AdventureTeamSetHero(int heroID)
    {


        if (AdventureSendPanel.Instance.selectedHeroID.Contains(heroID))//已在队里，卸下
        {
            AdventureSendPanel.Instance.selectedHeroID.Remove(heroID);
            AdventureSendPanel.Instance.UpdateHeroListSingle(heroID);

        }
        else//未在队里，添加
        {
            if (AdventureSendPanel.Instance.selectedHeroID.Count == 3)
            {
                MessagePanel.Instance.AddMessage("队伍已满");
                return;
            }
            AdventureSendPanel.Instance.selectedHeroID.Add(heroID);
        }
        for (int i = 0; i < AdventureSendPanel.Instance.selectedHeroID.Count; i++)
        {
            AdventureSendPanel.Instance.UpdateHeroListSingle(AdventureSendPanel.Instance.selectedHeroID[i]);
        }
        AdventureSendPanel.Instance.UpdateHeroNum();

    }

    //从据点向地牢派出
    public void AdventureTeamSend(short districtID, short dungeonID, byte teamID, List<int> heroIDList)
    {
        if (districtID == -1)
        {
            MessagePanel.Instance.AddMessage("据点不能为空");
            return;
        }
        if (dungeonID == -1)
        {
            MessagePanel.Instance.AddMessage("未选择目的地");
            return;
        }
        if (heroIDList.Count == 0)
        {
            MessagePanel.Instance.AddMessage("队伍无探险者");
            return;
        }
        adventureTeamList[teamID].districtID = districtID;


        AdventureTeamSetDungeon(teamID, dungeonID);

        adventureTeamList[teamID].heroIDList.Clear();
        adventureTeamList[teamID].heroHpList.Clear();
        adventureTeamList[teamID].heroMpList.Clear();
        adventureTeamList[teamID].heroDamageList.Clear();
        for (int i = 0; i < heroIDList.Count; i++)
        {
            adventureTeamList[teamID].heroIDList.Add(heroIDList[i]);

            adventureTeamList[teamID].heroHpList.Add(GetHeroAttr(Attribute.Hp, heroIDList[i]));
            adventureTeamList[teamID].heroMpList.Add(GetHeroAttr(Attribute.Mp, heroIDList[i]));
            adventureTeamList[teamID].heroDamageList.Add(0);
            districtDic[heroDic[heroIDList[i]].inDistrict].heroList.Remove(heroIDList[i]);
            heroDic[heroIDList[i]].inDistrict = -1;
            heroDic[heroIDList[i]].adventureInTeam = teamID;

        }


        Debug.Log("adventureTeamList[teamID].heroIDList.Count=" + adventureTeamList[teamID].heroIDList.Count);
        AdventureSendPanel.Instance.OnHide();

        //地图上移动
        AdventureSend(districtID, dungeonID, heroIDList, teamID);

    }
    public void AdventureTeamBack(byte teamID)
    {
        Debug.Log(" teamID=" + teamID);
        AdventureBack(teamID);
    }


    public void AdventureTeamStart(byte teamID)
    {
        if (adventureTeamList[teamID].dungeonID == -1)
        {
            MessagePanel.Instance.AddMessage("未选择目的地");
            return;
        }
        if (adventureTeamList[teamID].heroIDList.Count == 0)
        {
            MessagePanel.Instance.AddMessage("队伍无探险者");
            return;
        }
        //复位数据


        for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
        {
            adventureTeamList[teamID].heroHpList[i] = GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]);
            adventureTeamList[teamID].heroMpList[i] = GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]);
            adventureTeamList[teamID].heroDamageList[i] = 0;
        }
        adventureTeamList[teamID].enemyIDList.Clear();

        //地牢未设计，初始化先为0
        adventureTeamList[teamID].dungeonEVWind = 0;
        adventureTeamList[teamID].dungeonEVFire = 0;
        adventureTeamList[teamID].dungeonEVWater = 0;
        adventureTeamList[teamID].dungeonEVGround = 0;
        adventureTeamList[teamID].dungeonEVLight = 0;
        adventureTeamList[teamID].dungeonEVDark = 0;
        adventureTeamList[teamID].dungeonEPWind = 0;
        adventureTeamList[teamID].dungeonEPFire = 0;
        adventureTeamList[teamID].dungeonEPWater = 0;
        adventureTeamList[teamID].dungeonEPGround = 0;
        adventureTeamList[teamID].dungeonEPLight = 0;
        adventureTeamList[teamID].dungeonEPDark = 0;

        adventureTeamList[teamID].nowDay = 0;
        adventureTeamList[teamID].standardTimeStart = standardTime;
        adventureTeamList[teamID].fightRound = 0;
        adventureTeamList[teamID].getExp = 0;
        adventureTeamList[teamID].getGold = 0;
        adventureTeamList[teamID].getCereal = 0;
        adventureTeamList[teamID].getVegetable = 0;
        adventureTeamList[teamID].getFruit = 0;
        adventureTeamList[teamID].getMeat = 0;
        adventureTeamList[teamID].getFish = 0;
        adventureTeamList[teamID].getWood = 0;
        adventureTeamList[teamID].getMetal = 0;
        adventureTeamList[teamID].getStone = 0;
        adventureTeamList[teamID].getLeather = 0;
        adventureTeamList[teamID].getCloth = 0;
        adventureTeamList[teamID].getTwine = 0;
        adventureTeamList[teamID].getBone = 0;
        adventureTeamList[teamID].getWind = 0;
        adventureTeamList[teamID].getFire = 0;
        adventureTeamList[teamID].getWater = 0;
        adventureTeamList[teamID].getGround = 0;
        adventureTeamList[teamID].getLight = 0;
        adventureTeamList[teamID].getDark = 0;
        adventureTeamList[teamID].getItemList.Clear();
        adventureTeamList[teamID].killNum = 0;
        adventureTeamList[teamID].log.Clear();//?
        adventureTeamList[teamID].part.Clear();

        adventureTeamList[teamID].state = AdventureState.Doing;
        adventureTeamList[teamID].action = AdventureAction.Walk;



        //List<short> needUpdateDistrict = new List<short>();
        //for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
        //{
        //    if (!needUpdateDistrict.Contains(heroDic[adventureTeamList[teamID].heroIDList[i]].inDistrict))
        //    {
        //        needUpdateDistrict.Add(heroDic[adventureTeamList[teamID].heroIDList[i]].inDistrict);
        //    }
        //}
        //for (int i = 0; i < needUpdateDistrict.Count; i++)
        //{
        //    AreaMapPanel.Instance.UpdateDistrictSingle(needUpdateDistrict[i]);
        //}

        AdventureMainPanel.Instance.UpdateSceneRole(teamID);//下面代码包括了
        AdventureMainPanel.Instance.UpdateTeam(teamID);

        PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
        if (AreaMapPanel.Instance.dungeonInfoBlockID == adventureTeamList[teamID].dungeonID)
        {
            AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
        }
        if (AdventureTeamPanel.Instance.isShow && AdventureTeamPanel.Instance.nowTeam == teamID)
        {
            // AdventureTeamPanel.Instance.UpdateHero(teamID);
            AdventureTeamPanel.Instance.UpdatePart(teamID);
            AdventureTeamPanel.Instance.UpdateLast(teamID);
            AdventureTeamPanel.Instance.UpdateNow(teamID);
        }

        CreateAdventureEvent(teamID);
    }

    public void AdventureTeamEnd(byte teamID, AdventureState adventureState)
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < executeEventList.Count; i++)
        {
            if (executeEventList[i].type == ExecuteEventType.Adventure && executeEventList[i].value[0][0] == teamID)
            {
                tempList.Add(i);
            }
        }
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            ExecuteEventDelete(tempList[i]);
        }



        adventureTeamList[teamID].state = adventureState;
        adventureTeamList[teamID].action = AdventureAction.None;



        AdventureMainPanel.Instance.UpdateTeam(teamID);
        for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
        {
            heroDic[adventureTeamList[teamID].heroIDList[i]].countAdventure++;
        }
        if (adventureState == AdventureState.Done)
        {
            for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
            {
                heroDic[adventureTeamList[teamID].heroIDList[i]].countAdventureDone++;
            }
            AdventureTeamLogAdd(teamID, "探险队完成任务回来了");
            MessagePanel.Instance.AddMessage("第" + (teamID + 1) + "探险队完成任务回来了");
        }
        else if (adventureState == AdventureState.Fail)
        {
            AdventureTeamLogAdd(teamID, "探险队全灭");
            MessagePanel.Instance.AddMessage("第" + (teamID + 1) + "探险队全灭");
        }
        else if (adventureState == AdventureState.Retreat)
        {
            AdventureTeamLogAdd(teamID, "探险队撤退回来了");
            MessagePanel.Instance.AddMessage("第" + (teamID + 1) + "探险队撤退回来了");
        }
        AdventureTeamLogAdd(teamID, "本次探险于" + OutputDateStr(adventureTeamList[teamID].standardTimeStart, "Y年M月D日") + "出发," + OutputDateStr(standardTime, "Y年M月D日") + "返回,耗时" + OutputUseDateStr(adventureTeamList[teamID].standardTimeStart, standardTime) + "天");
        PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
        if (AreaMapPanel.Instance.dungeonInfoBlockID == adventureTeamList[teamID].dungeonID)
        {
            AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
        }
        if (AdventureTeamPanel.Instance.isShow && AdventureTeamPanel.Instance.nowTeam == teamID)
        {
           // AdventureTeamPanel.Instance.UpdateHero(teamID);
            AdventureTeamPanel.Instance.UpdatePart(teamID);
            AdventureTeamPanel.Instance.UpdateLast(teamID);
            AdventureTeamPanel.Instance.UpdateNow(teamID);
        }
    }



    public void AdventureTakeGets(byte teamID)
    {

        for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
        {
            HeroGetExp(adventureTeamList[teamID].heroIDList[i], (int)(adventureTeamList[teamID].getExp / 3f * (1f + heroDic[adventureTeamList[teamID].heroIDList[i]].expGet / 100f)));
        }

        forceDic[0].gold += adventureTeamList[teamID].getGold;

        int freeCount = forceDic[0].rFoodLimit - GetForceFoodAll(0);
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getCereal > 0)
            {
                forceDic[0].rFoodCereal += System.Math.Min(freeCount, adventureTeamList[teamID].getCereal);
                freeCount = forceDic[0].rFoodLimit - GetForceFoodAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getVegetable > 0)
            {
                forceDic[0].rFoodVegetable += System.Math.Min(freeCount, adventureTeamList[teamID].getVegetable);
                freeCount = forceDic[0].rFoodLimit - GetForceFoodAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getFruit > 0)
            {
                forceDic[0].rFoodFruit += System.Math.Min(freeCount, adventureTeamList[teamID].getFruit);
                freeCount = forceDic[0].rFoodLimit - GetForceFoodAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getMeat > 0)
            {
                forceDic[0].rFoodMeat += System.Math.Min(freeCount, adventureTeamList[teamID].getMeat);
                freeCount = forceDic[0].rFoodLimit - GetForceFoodAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getFish > 0)
            {
                forceDic[0].rFoodFish += System.Math.Min(freeCount, adventureTeamList[teamID].getFish);
                freeCount = forceDic[0].rFoodLimit - GetForceFoodAll(0);
            }
        }
        if (freeCount <= 0)
        {
            MessagePanel.Instance.AddMessage("食品库房已满");
        }

        freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getWood > 0)
            {
                forceDic[0].rStuffWood += System.Math.Min(freeCount, adventureTeamList[teamID].getWood);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getMetal > 0)
            {
                forceDic[0].rStuffMetal += System.Math.Min(freeCount, adventureTeamList[teamID].getMetal);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getStone > 0)
            {
                forceDic[0].rStuffStone += System.Math.Min(freeCount, adventureTeamList[teamID].getStone);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getLeather > 0)
            {
                forceDic[0].rStuffLeather += System.Math.Min(freeCount, adventureTeamList[teamID].getLeather);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getCloth > 0)
            {
                forceDic[0].rStuffCloth += System.Math.Min(freeCount, adventureTeamList[teamID].getCloth);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getTwine > 0)
            {
                forceDic[0].rStuffTwine += System.Math.Min(freeCount, adventureTeamList[teamID].getTwine);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getBone > 0)
            {
                forceDic[0].rStuffBone += System.Math.Min(freeCount, adventureTeamList[teamID].getBone);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getWind > 0)
            {
                forceDic[0].rStuffWind += System.Math.Min(freeCount, adventureTeamList[teamID].getWind);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getFire > 0)
            {
                forceDic[0].rStuffFire += System.Math.Min(freeCount, adventureTeamList[teamID].getFire);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getWater > 0)
            {
                forceDic[0].rStuffWater += System.Math.Min(freeCount, adventureTeamList[teamID].getWater);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getGround > 0)
            {
                forceDic[0].rStuffGround += System.Math.Min(freeCount, adventureTeamList[teamID].getGround);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getLight > 0)
            {
                forceDic[0].rStuffLight += System.Math.Min(freeCount, adventureTeamList[teamID].getLight);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount > 0)
        {
            if (adventureTeamList[teamID].getDark > 0)
            {
                forceDic[0].rStuffDark += System.Math.Min(freeCount, adventureTeamList[teamID].getDark);
                freeCount = forceDic[0].rStuffLimit - GetForceStuffAll(0);
            }
        }
        if (freeCount <= 0)
        {
            MessagePanel.Instance.AddMessage("材料库房已满");
        }

        Debug.Log("forceDic[0].rProductNow=" + forceDic[0].rProductNow + " forceDic[0].rProductLimit=" + forceDic[0].rProductLimit);

        for (int i = 0; i < adventureTeamList[teamID].getItemList.Count; i++)
        {
            if (forceDic[0].rProductNow < forceDic[0].rProductLimit)
            {
                itemDic.Add(itemIndex, GenerateItemByRandom(adventureTeamList[teamID].getItemList[i], null,-1, null));
                itemIndex++;
                forceDic[0].rProductNow++;
            }
            else
            {
                MessagePanel.Instance.AddMessage("收藏库房已满");
                break;
            }
        }


        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateResources();
    

        if (PlayMainPanel.Instance.IsShowResourcesBlock)
        {
            PlayMainPanel.Instance.UpdateResourcesBlock();
        }
        if (adventureTeamList[teamID].getItemList.Count > 0)
        {
            PlayMainPanel.Instance.UpdateButtonItemNum();
        }


        AdventureState endAdventureState = adventureTeamList[teamID].state;

        //可能是多余的清零，开始时会重置，这里是为了getsicon的显示
        adventureTeamList[teamID].getExp = 0;
        adventureTeamList[teamID].getGold = 0;
        adventureTeamList[teamID].getCereal = 0;
        adventureTeamList[teamID].getVegetable = 0;
        adventureTeamList[teamID].getFruit = 0;
        adventureTeamList[teamID].getMeat = 0;
        adventureTeamList[teamID].getFish = 0;
        adventureTeamList[teamID].getWood = 0;
        adventureTeamList[teamID].getMetal = 0;
        adventureTeamList[teamID].getStone = 0;
        adventureTeamList[teamID].getLeather = 0;
        adventureTeamList[teamID].getCloth = 0;
        adventureTeamList[teamID].getTwine = 0;
        adventureTeamList[teamID].getBone = 0;
        adventureTeamList[teamID].getWind = 0;
        adventureTeamList[teamID].getFire = 0;
        adventureTeamList[teamID].getWater = 0;
        adventureTeamList[teamID].getGround = 0;
        adventureTeamList[teamID].getLight = 0;
        adventureTeamList[teamID].getDark = 0;
        adventureTeamList[teamID].getItemList.Clear();

        adventureTeamList[teamID].state = AdventureState.NotSend;
        adventureTeamList[teamID].action = AdventureAction.None;

        if (AreaMapPanel.Instance.dungeonInfoBlockID == adventureTeamList[teamID].dungeonID)
        {
            AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
        }

        AdventureMainPanel.Instance.UpdateTeam(teamID);
        AdventureMainPanel.Instance.SetAllRoleAnimInEnd(teamID, endAdventureState);
    }

    public void CreateAdventureEvent(byte teamID)
    {
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.Adventure, standardTime, standardTime + 160, new List<List<int>> { new List<int> { teamID } }));
    }

    void CreateAdventureEventPartLog(byte teamID, AdventureEvent adventureEvent, bool isPass, string log)
    {
        Debug.Log("CreateAdventureEventPartLog() adventureEvent=" + adventureEvent);

        List<int> hpList = new List<int> { };
        List<int> mpList = new List<int> { };
        for (byte i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
        {
            hpList.Add(adventureTeamList[teamID].heroHpList[i]);
            mpList.Add(adventureTeamList[teamID].heroMpList[i]);
        }

        PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
        if (AreaMapPanel.Instance.dungeonInfoBlockID == adventureTeamList[teamID].dungeonID)
        {
            AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
        }

        adventureTeamList[teamID].part.Add(new AdventurePartObject(adventureEvent, isPass, hpList, mpList, new List<byte> { adventureTeamList[teamID].dungeonEPWind, adventureTeamList[teamID].dungeonEPFire, adventureTeamList[teamID].dungeonEPWater, adventureTeamList[teamID].dungeonEPGround, adventureTeamList[teamID].dungeonEPLight, adventureTeamList[teamID].dungeonEPDark }, log));

        if (AdventureTeamPanel.Instance.isShow && AdventureTeamPanel.Instance.nowTeam == teamID)
        {
            //AdventureTeamPanel.Instance.UpdateHero(teamID);
            AdventureTeamPanel.Instance.UpdatePart(teamID);
            AdventureTeamPanel.Instance.UpdateLast(teamID);
            AdventureTeamPanel.Instance.UpdateNow(teamID);
        }
    }

    public void AdventureEventHappen(byte teamID)
    {
        AdventureEvent adventureEvent;

        short dungeonID = adventureTeamList[teamID].dungeonID;

        int ran = Random.Range(0, 100);


        if (ran < DataManager.mDungeonDict[dungeonID].RandomMonster)
        {
            adventureEvent = AdventureEvent.Monster;
        }
        else if (ran < DataManager.mDungeonDict[dungeonID].RandomMonster +
            DataManager.mDungeonDict[dungeonID].RandomTrapHp)
        {
            adventureEvent = AdventureEvent.TrapHp;
        }
        else if (ran < DataManager.mDungeonDict[dungeonID].RandomMonster +
            DataManager.mDungeonDict[dungeonID].RandomTrapHp +
            DataManager.mDungeonDict[dungeonID].RandomTrapMp)
        {
            adventureEvent = AdventureEvent.TrapMp;
        }
        else if (ran < DataManager.mDungeonDict[dungeonID].RandomMonster +
            DataManager.mDungeonDict[dungeonID].RandomTrapHp +
            DataManager.mDungeonDict[dungeonID].RandomTrapMp +
            DataManager.mDungeonDict[dungeonID].RandomSpringHp)
        {
            adventureEvent = AdventureEvent.SpringHp;
        }
        else if (ran < DataManager.mDungeonDict[dungeonID].RandomMonster +
            DataManager.mDungeonDict[dungeonID].RandomTrapHp +
            DataManager.mDungeonDict[dungeonID].RandomTrapMp +
            DataManager.mDungeonDict[dungeonID].RandomSpringHp +
            DataManager.mDungeonDict[dungeonID].RandomSpringMp)
        {
            adventureEvent = AdventureEvent.SpringMp;
        }
        else if (ran < DataManager.mDungeonDict[dungeonID].RandomMonster +
            DataManager.mDungeonDict[dungeonID].RandomTrapHp +
            DataManager.mDungeonDict[dungeonID].RandomTrapMp +
            DataManager.mDungeonDict[dungeonID].RandomSpringHp +
            DataManager.mDungeonDict[dungeonID].RandomSpringMp +
            DataManager.mDungeonDict[dungeonID].RandomGold)
        {
            adventureEvent = AdventureEvent.Gold;
        }
        else if (ran < DataManager.mDungeonDict[dungeonID].RandomMonster +
            DataManager.mDungeonDict[dungeonID].RandomTrapHp +
            DataManager.mDungeonDict[dungeonID].RandomTrapMp +
            DataManager.mDungeonDict[dungeonID].RandomSpringHp +
            DataManager.mDungeonDict[dungeonID].RandomSpringMp +
            DataManager.mDungeonDict[dungeonID].RandomGold +
            DataManager.mDungeonDict[dungeonID].RandomItem)
        {
            adventureEvent = AdventureEvent.Item;
        }
        else if (ran < DataManager.mDungeonDict[dungeonID].RandomMonster +
            DataManager.mDungeonDict[dungeonID].RandomTrapHp +
            DataManager.mDungeonDict[dungeonID].RandomTrapMp +
            DataManager.mDungeonDict[dungeonID].RandomSpringHp +
            DataManager.mDungeonDict[dungeonID].RandomSpringMp +
            DataManager.mDungeonDict[dungeonID].RandomGold +
            DataManager.mDungeonDict[dungeonID].RandomItem +
            DataManager.mDungeonDict[dungeonID].RandomResource)
        {
            adventureEvent = AdventureEvent.Resource;
        }
        else
        {
            adventureEvent = AdventureEvent.None;
        }

        //Debug.Log("teamID=" + teamID+ "  ran="+ ran + "  adventureEvent="+ adventureEvent);

        switch (adventureEvent)
        {
            case AdventureEvent.Monster:
                StartCoroutine(AdventureFight(teamID));
                break;
            case AdventureEvent.TrapHp:
                StartCoroutine(AdventureTrapOrSpring(teamID, adventureEvent));
                break;
            case AdventureEvent.TrapMp:
                StartCoroutine(AdventureTrapOrSpring(teamID, adventureEvent));
                break;
            case AdventureEvent.SpringHp:
                StartCoroutine(AdventureTrapOrSpring(teamID, adventureEvent));
                break;
            case AdventureEvent.SpringMp:
                StartCoroutine(AdventureTrapOrSpring(teamID, adventureEvent));
                break;
            case AdventureEvent.Gold:
                StartCoroutine(AdventureGetSomething(teamID, adventureEvent));
                break;
            case AdventureEvent.Item:
                break;
            case AdventureEvent.Resource:
                StartCoroutine(AdventureGetSomething(teamID, adventureEvent));
                break;
            case AdventureEvent.None:
                AdventureNoneHappen(teamID);
                break;
        }

    }

    void AdventureNoneHappen(byte teamID)
    {
        if (adventureTeamList[teamID].state == AdventureState.Retreat)
        {
            AdventureTeamEnd(teamID, AdventureState.Retreat);
        }
        else
        {
            CreateAdventureEvent(teamID);
        }

        CreateAdventureEventPartLog(teamID, AdventureEvent.None, true, "");

    }

    IEnumerator AdventureGetSomething(byte teamID, AdventureEvent adventureEvent)
    {
        if (adventureTeamList[teamID].state == AdventureState.Retreat)
        {
            AdventureTeamEnd(teamID, AdventureState.Retreat);
            yield break;
        }

        CreateAdventureEvent(teamID);

        adventureTeamList[teamID].action = AdventureAction.GetSomething;
        AdventureMainPanel.Instance.UpdateSceneRoleFormations(teamID);
        AdventureMainPanel.Instance.UpdateSceneEnemy(teamID);

        if (AdventureTeamPanel.Instance.isShow && AdventureTeamPanel.Instance.nowTeam == teamID)
        {
            AdventureTeamPanel.Instance.UpdateNow(teamID);
        }

        yield return new WaitForSeconds(1f);
        short getNum = 0;
        string log = "";
        string type = "";
        switch (adventureEvent)
        {
            case AdventureEvent.Gold:
                type = "Gold";
                getNum = (short)Random.Range(100, 500);
                adventureTeamList[teamID].getGold += getNum;
                log = "[获得金币]金币 " + getNum;
                break;
            case AdventureEvent.Item:
                break;
            case AdventureEvent.Resource:
               
                int ran = Random.Range(0, DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].ResourceType.Count);

                StuffType resourceType = DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].ResourceType[ran];
                int resourceValue = DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].ResourceValue[ran];
                type = resourceType.ToString();
               //Debug.Log("ran=" + ran + " resourceType=" + resourceType + " resourceValue");
                getNum = (short)(resourceValue * Random.Range(0.5f, 1.5f));
                switch (resourceType)
                {
                    case StuffType.Cereal:
                        adventureTeamList[teamID].getCereal += getNum;
                        log = "[获得资源]谷物*" + getNum;
                        break;
                    case StuffType.Vegetable:
                        adventureTeamList[teamID].getVegetable += getNum;
                        log = "[获得资源]蔬菜*" + getNum;
                        break;
                    case StuffType.Fruit:
                        adventureTeamList[teamID].getFruit += getNum;
                        log = "[获得资源]水果*" + getNum;
                        break;
                    case StuffType.Meat:
                        adventureTeamList[teamID].getMeat += getNum;
                        log = "[获得资源]肉类*" + getNum;
                        break;
                    case StuffType.Fish:
                        adventureTeamList[teamID].getFish += getNum;
                        log = "[获得资源]鱼类*" + getNum;
                        break;
                    case StuffType.Wood:
                        adventureTeamList[teamID].getWood += getNum;
                        log = "[获得资源]木材*" + getNum;
                        break;
                    case StuffType.Stone:
                        adventureTeamList[teamID].getStone += getNum;
                        log = "[获得资源]石料*" + getNum;
                        break;
                    case StuffType.Metal:
                        adventureTeamList[teamID].getMetal += getNum;
                        log = "[获得资源]金属*" + getNum;
                        break;
                    case StuffType.Leather:
                        adventureTeamList[teamID].getLeather += getNum;
                        log = "[获得资源]皮革*" + getNum;
                        break;
                    case StuffType.Cloth:
                        adventureTeamList[teamID].getCloth += getNum;
                        log = "[获得资源]布料*" + getNum;
                        break;
                    case StuffType.Twine:
                        adventureTeamList[teamID].getTwine += getNum;
                        log = "[获得资源]麻绳*" + getNum;
                        break;
                    case StuffType.Bone:
                        adventureTeamList[teamID].getBone += getNum;
                        log = "[获得资源]骨块*" + getNum;
                        break;
                    case StuffType.Wind:
                        adventureTeamList[teamID].getWind += getNum;
                        log = "[获得资源]风粉尘*" + getNum;
                        break;
                    case StuffType.Fire:
                        adventureTeamList[teamID].getFire += getNum;
                        log = "[获得资源]火粉尘*" + getNum;
                        break;
                    case StuffType.Water:
                        adventureTeamList[teamID].getWater += getNum;
                        log = "[获得资源]水粉尘*" + getNum;
                        break;
                    case StuffType.Ground:
                        adventureTeamList[teamID].getGround += getNum;
                        log = "[获得资源]地粉尘*" + getNum;
                        break;
                    case StuffType.Light:
                        adventureTeamList[teamID].getLight += getNum;
                        log = "[获得资源]光粉尘*" + getNum;
                        break;
                    case StuffType.Dark:
                        adventureTeamList[teamID].getDark += getNum;
                        log = "[获得资源]暗粉尘*" + getNum;
                        break;
                }

                break;
        }
        AdventureTeamLogAdd(teamID, log);


        yield return new WaitForSeconds(1f);

        AdventureMainPanel.Instance.ShowDropsIcon(teamID, new List<string> { type } , new List<short> { }, new Vector2(320, -135));

        CreateAdventureEventPartLog(teamID, adventureEvent, true, "探险队发现了一些东西\n" + log);

        //if (adventureTeamList[teamID].state == AdventureState.Retreat)
        //{
        //    AdventureTeamBack(teamID, AdventureState.Retreat);
        //}
        //else
        //{



        //}
        adventureTeamList[teamID].action = AdventureAction.Walk;
        AdventureMainPanel.Instance.UpdateSceneRoleFormations(teamID);
        AdventureMainPanel.Instance.UpdateSceneRole(teamID);
        AdventureMainPanel.Instance.UpdateTeam(teamID);

    }

    IEnumerator AdventureTrapOrSpring(byte teamID, AdventureEvent adventureEvent)
    {
        if (adventureTeamList[teamID].state == AdventureState.Retreat)
        {
            AdventureTeamEnd(teamID, AdventureState.Retreat);
            yield break;
        }
        CreateAdventureEvent(teamID);
        switch (adventureEvent)
        {
            case AdventureEvent.TrapHp:
                adventureTeamList[teamID].action = AdventureAction.TrapHp;
                break;
            case AdventureEvent.TrapMp:
                adventureTeamList[teamID].action = AdventureAction.TrapMp;
                break;
            case AdventureEvent.SpringHp:
                adventureTeamList[teamID].action = AdventureAction.SpringHp;
                break;
            case AdventureEvent.SpringMp:
                adventureTeamList[teamID].action = AdventureAction.SpringMp;
                break;
        }


        AdventureMainPanel.Instance.UpdateSceneRoleFormations(teamID);
        AdventureMainPanel.Instance.UpdateSceneEnemy(teamID);

        if (AdventureTeamPanel.Instance.isShow && AdventureTeamPanel.Instance.nowTeam == teamID)
        {
            AdventureTeamPanel.Instance.UpdateNow(teamID);
        }

        yield return new WaitForSeconds(0.5f);

        string log = "";
        switch (adventureEvent)
        {
            case AdventureEvent.TrapHp:
                for (byte i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
                {
                    adventureTeamList[teamID].heroHpList[i] -= (int)(GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]) * 0.1f);
                    if (adventureTeamList[teamID].heroHpList[i] <= 0)
                    {
                        adventureTeamList[teamID].heroHpList[i] = 1;
                    }
                    AdventureTeamLogAdd(teamID, "[触发陷阱]<color=#72FF53>" + heroDic[adventureTeamList[teamID].heroIDList[i]].name + "</color>损失10%体力");
                    AdventureMainPanel.Instance.ShowEffect(teamID, 0, i, "weapon_2", 1f);
                    AdventureMainPanel.Instance.ShowDamageText(teamID, 0, i, "<color=#F86A43>-" + (int)(GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]) * 0.1f) + "</color>",false);
                }
                log = "探险队行进时触发陷阱，队伍成员的体力下降了";
                break;
            case AdventureEvent.TrapMp:
                for (byte i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
                {
                    adventureTeamList[teamID].heroMpList[i] -= (int)(GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]) * 0.1f);
                    if (adventureTeamList[teamID].heroMpList[i] <= 0)
                    {
                        adventureTeamList[teamID].heroMpList[i] = 1;
                    }
                    AdventureTeamLogAdd(teamID, "[触发陷阱]<color=#72FF53>" + heroDic[adventureTeamList[teamID].heroIDList[i]].name + "</color>损失10%魔力");
                    AdventureMainPanel.Instance.ShowEffect(teamID, 0, i, "weapon_2", 1f);
                    AdventureMainPanel.Instance.ShowDamageText(teamID, 0, i, "<color=#D76FFA>-" + (int)(GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]) * 0.1f) + "</color>", false);
                }
                log = "探险队行进时触发陷阱，队伍成员的魔力下降了";
                break;
            case AdventureEvent.SpringHp:
                for (byte i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
                {
                    adventureTeamList[teamID].heroHpList[i] += (int)(GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]) * 0.1f);
                    if (adventureTeamList[teamID].heroHpList[i] > GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]))
                    {
                        adventureTeamList[teamID].heroHpList[i] = GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]);
                    }
                    AdventureTeamLogAdd(teamID, "[生命之泉]<color=#72FF53>" + heroDic[adventureTeamList[teamID].heroIDList[i]].name + "</color>恢复10%体力");
                    AdventureMainPanel.Instance.ShowEffect(teamID, 0, i, "impact_6", 1f);
                    AdventureMainPanel.Instance.ShowDamageText(teamID, 0, i, "<color=#6EFB6F>+" + (int)(GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]) * 0.1f) + "</color>", false);
                }
                log = "探险队行进时发现生命之泉，队伍成员的体力恢复了";
                break;
            case AdventureEvent.SpringMp:
                for (byte i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
                {
                    adventureTeamList[teamID].heroMpList[i] += (int)(GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]) * 0.1f);
                    if (adventureTeamList[teamID].heroMpList[i] > GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]))
                    {
                        adventureTeamList[teamID].heroMpList[i] = GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]);
                    }
                    AdventureTeamLogAdd(teamID, "[智慧之泉]<color=#72FF53>" + heroDic[adventureTeamList[teamID].heroIDList[i]].name + "</color>恢复10%魔力");
                    AdventureMainPanel.Instance.ShowEffect(teamID, 0, i, "impact_5", 1f);
                    AdventureMainPanel.Instance.ShowDamageText(teamID, 0, i, "<color=#6FAAFA>+" + (int)(GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]) * 0.1f) + "</color>", false);
                }
                log = "探险队行进时发现智慧之泉，队伍成员的魔力恢复了";
                break;
        }


        yield return new WaitForSeconds(2f);

        CreateAdventureEventPartLog(teamID, adventureEvent, true, log);


        //else
        //{
        adventureTeamList[teamID].action = AdventureAction.Walk;
        AdventureMainPanel.Instance.UpdateSceneRoleFormations(teamID);
        AdventureMainPanel.Instance.UpdateSceneRole(teamID);
        AdventureMainPanel.Instance.UpdateTeam(teamID);



        // }

    }

    public  IEnumerator AdventureFight(byte teamID)
    {
        if (AdventureMainPanel.Instance.isShow && AdventureMainPanel.Instance.nowCheckingTeamID== teamID)
        {
            AudioControl.Instance.PlayMusic("08Thebattle");
        }

        const int RoundLimit = 200;
        int CheckFightOverResult = -1;


        List<FightMenberObject> fightMenberObjects = new List<FightMenberObject>();

        //Debug.Log("fightMenberObjects.Count=" + fightMenberObjects.Count );

        if (adventureTeamList[teamID].action == AdventureAction.Fight)//继续之前的战斗
        {
            fightMenberObjects = fightMenberObjectSS[teamID];
            Debug.Log(" 继续之前的战斗 teamID=" + teamID + " fightMenberObjectSS.Count=" + fightMenberObjectSS.Count);
        }
        else //新开的战斗
        {

            Debug.Log(" 新开的战斗 teamID=" + teamID + " fightMenberObjectSS.Count=" + fightMenberObjectSS.Count);
            fightMenberObjects = fightMenberObjectSS[teamID];


            //读取队伍成员和怪物列表，转化为战斗成员实例
            for (byte i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
            {
                int heroID = adventureTeamList[teamID].heroIDList[i];

                int id = i;
                int objectID = heroID;//对于己方，heroID
                byte side = 0;
                string name = heroDic[heroID].name;
                short level = heroDic[heroID].level;
                int hp = GetHeroAttr(Attribute.Hp, heroID);
                int mp = GetHeroAttr(Attribute.Mp, heroID);
                short hpRenew = (short)GetHeroAttr(Attribute.HpRenew, heroID);
                short mpRenew = (short)(GetHeroAttr(Attribute.MpRenew, heroID) + 10);//测试 每回合回10%
                short atkMin = (short)GetHeroAttr(Attribute.AtkMin, heroID);
                short atkMax = (short)GetHeroAttr(Attribute.AtkMax, heroID);
                short mAtkMin = (short)GetHeroAttr(Attribute.MAtkMin, heroID);
                short mAtkMax = (short)GetHeroAttr(Attribute.MAtkMax, heroID);
                short def = (short)GetHeroAttr(Attribute.Def, heroID);
                short mDef = (short)GetHeroAttr(Attribute.MDef, heroID);
                short hit = (short)GetHeroAttr(Attribute.Hit, heroID);
                short dod = (short)GetHeroAttr(Attribute.Dod, heroID);
                short criR = (short)GetHeroAttr(Attribute.CriR, heroID);
                short criD = (short)GetHeroAttr(Attribute.CriD, heroID);
                short spd = (short)GetHeroAttr(Attribute.Spd, heroID);
                spd += (short)Random.Range(-4, 4);
                short windDam = (short)GetHeroAttr(Attribute.WindDam, heroID);
                short fireDam = (short)GetHeroAttr(Attribute.FireDam, heroID);
                short waterDam = (short)GetHeroAttr(Attribute.WaterDam, heroID);
                short groundDam = (short)GetHeroAttr(Attribute.GroundDam, heroID);
                short lightDam = (short)GetHeroAttr(Attribute.LightDam, heroID);
                short darkDam = (short)GetHeroAttr(Attribute.DarkDam, heroID);
                short windRes = (short)GetHeroAttr(Attribute.WindRes, heroID);
                short fireRes = (short)GetHeroAttr(Attribute.FireRes, heroID);
                short waterRes = (short)GetHeroAttr(Attribute.WaterRes, heroID);
                short groundRes = (short)GetHeroAttr(Attribute.GroundRes, heroID);
                short lightRes = (short)GetHeroAttr(Attribute.LightRes, heroID);
                short darkRes = (short)GetHeroAttr(Attribute.DarkRes, heroID);
                short dizzyRes = (short)GetHeroAttr(Attribute.DizzyRes, heroID);
                short confusionRes = (short)GetHeroAttr(Attribute.ConfusionRes, heroID);
                short poisonRes = (short)GetHeroAttr(Attribute.PoisonRes, heroID);
                short sleepRes = (short)GetHeroAttr(Attribute.SleepRes, heroID);

                ItemTypeSmall weaponType = ItemTypeSmall.None;
                short weaponPID = -1;
                byte sharpnessNow = 0;
                if (heroDic[heroID].equipWeapon != -1)
                {
                    weaponPID = (short)itemDic[heroDic[heroID].equipWeapon].prototypeID;
                    weaponType = DataManager.mItemDict[weaponPID].TypeSmall;
                    //byte sharpnessTotal = 0;
                    for (byte j = 0; j < DataManager.mItemDict[weaponPID].Sharpness.Count; j++)
                    {
                        sharpnessNow += DataManager.mItemDict[weaponPID].Sharpness[j];
                    }
                }

                short actionBar = 0;
                byte skillIndex = 0;//当前招式位置
                int hpNow = adventureTeamList[teamID].heroHpList[i];
                int mpNow = adventureTeamList[teamID].heroMpList[i];


                List<FightBuff> buff = new List<FightBuff> { };
                fightMenberObjects.Add(new FightMenberObject(id, objectID, side, i, name, level, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
                                                                windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, weaponType,
                                                                actionBar, skillIndex, hpNow, mpNow, sharpnessNow, weaponPID,buff,false));
            }

            //int heroCount = fightMenberObjects.Count;

            //创建怪物ID列表
            adventureTeamList[teamID].enemyIDList.Clear();
            List<int> enemyIDList = new List<int> { };
            List<int> enemyLevelList = new List<int> { };
            List<float> enemyRankList = new List<float> { };
            int enemyNum = Random.Range(1, 7);
            for (int i = 0; i < enemyNum; i++)
            {
                int probabilityCount = DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterID.Count;
                int ranEnemy = Random.Range(0, 100);
                byte lj = 0;
                for (int j = 0; j < probabilityCount; j++)
                {
                    lj += DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterRate[j];

                    if (ranEnemy < lj)
                    {
                        enemyIDList.Add(DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterID[j]);
                        adventureTeamList[teamID].enemyIDList.Add(DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterID[j]);
                        enemyLevelList.Add(Random.Range(DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterLevelMin[j], DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterLevelMax[j]));

                        int ranRank = Random.Range(0, 100);
                        if (ranRank < DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterLeaderRate[j])
                        {
                            enemyRankList.Add(2f);
                        }
                        else if (ranRank < DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterLeaderRate[j] + DataManager.mDungeonDict[adventureTeamList[teamID].dungeonID].MonsterEliteRate[j])
                        {
                            enemyRankList.Add(1.3f);
                        }
                        else
                        {
                            enemyRankList.Add(1f);
                        }
                        break;
                    }
                }
            }

            //List<int> enemyIDTempList = new List<int> { };//用作记录同类怪物数
            for (byte i = 0; i < enemyIDList.Count; i++)
            {
                int monsterID = enemyIDList[i];
                int level = enemyLevelList[i];

                int id = i + adventureTeamList[teamID].heroIDList.Count;
                int objectID = monsterID;//对于敌方，原型
                byte side = 1;

                //byte sameNameCount = 0;

                string name = DataManager.mMonsterDict[monsterID].Name;
                if (enemyRankList[i] == 2f)
                {
                    name += "首领";
                }
                else if (enemyRankList[i] == 1.3f)
                {
                    name += "精英";
                }


                float levelModify = level * DataManager.mMonsterDict[monsterID].GroupRate;


                int hp = (int)(DataManager.mMonsterDict[monsterID].Hp * (1f + levelModify) * enemyRankList[i]);
                int mp = (int)(DataManager.mMonsterDict[monsterID].Mp * (1f + levelModify) * enemyRankList[i]);
                short hpRenew = DataManager.mMonsterDict[monsterID].HpRenew;
                short mpRenew = DataManager.mMonsterDict[monsterID].MpRenew;
                short atkMin = (short)(DataManager.mMonsterDict[monsterID].AtkMin * (1f + levelModify) * enemyRankList[i]);
                short atkMax = (short)(DataManager.mMonsterDict[monsterID].AtkMax * (1f + levelModify) * enemyRankList[i]);
                short mAtkMin = (short)(DataManager.mMonsterDict[monsterID].MAtkMin * (1f + levelModify) * enemyRankList[i]);
                short mAtkMax = (short)(DataManager.mMonsterDict[monsterID].MAtkMax * (1f + levelModify) * enemyRankList[i]);
                short def = (short)(DataManager.mMonsterDict[monsterID].Def * (1f + levelModify) * enemyRankList[i]);
                short mDef = (short)(DataManager.mMonsterDict[monsterID].MDef * (1f + levelModify) * enemyRankList[i]);
                short hit = (short)(DataManager.mMonsterDict[monsterID].Hit * (1f + levelModify) * enemyRankList[i]);
                short dod = (short)(DataManager.mMonsterDict[monsterID].Dod * (1f + levelModify) * enemyRankList[i]);
                short criR = DataManager.mMonsterDict[monsterID].CriR;
                short criD = DataManager.mMonsterDict[monsterID].CriD;
                short spd = DataManager.mMonsterDict[monsterID].Spd;
                spd += (short)Random.Range(-4, 4);
                short windDam = DataManager.mMonsterDict[monsterID].WindDam;
                short fireDam = DataManager.mMonsterDict[monsterID].FireDam;
                short waterDam = DataManager.mMonsterDict[monsterID].WaterDam;
                short groundDam = DataManager.mMonsterDict[monsterID].GroundDam;
                short lightDam = DataManager.mMonsterDict[monsterID].LightDam;
                short darkDam = DataManager.mMonsterDict[monsterID].DarkDam;
                short windRes = DataManager.mMonsterDict[monsterID].WindRes;
                short fireRes = DataManager.mMonsterDict[monsterID].FireRes;
                short waterRes = DataManager.mMonsterDict[monsterID].WaterRes;
                short groundRes = DataManager.mMonsterDict[monsterID].GroundRes;
                short lightRes = DataManager.mMonsterDict[monsterID].LightRes;
                short darkRes = DataManager.mMonsterDict[monsterID].DarkRes;
                short dizzyRes = DataManager.mMonsterDict[monsterID].DizzyRes;
                short confusionRes = DataManager.mMonsterDict[monsterID].ConfusionRes;
                short poisonRes = DataManager.mMonsterDict[monsterID].PoisonRes;
                short sleepRes = DataManager.mMonsterDict[monsterID].SleepRes;
                short actionBar = 0;
                byte skillIndex = 0;//当前招式位置
                int hpNow = hp;
                int mpNow = mp;

                List<FightBuff> buff = new List<FightBuff> { };

                fightMenberObjects.Add(new FightMenberObject(id, objectID, side, i, name, (short)level, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
                                                          windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, ItemTypeSmall.None,
                                                          actionBar, skillIndex, hpNow, mpNow,255,-1, buff,false));

                //enemyIDTempList.Add(monsterID);
            }
            string monsterNameList = "";
            for (int i = 0; i < fightMenberObjects.Count; i++)
            {
                if (fightMenberObjects[i].side == 1)
                {
                    monsterNameList += fightMenberObjects[i].name + "(Lv." + fightMenberObjects[i].level + ") ";
                }
            }
            //Debug.Log("fightMenberObjects.Count=" + fightMenberObjects.Count  + " ,adventureTeamList[teamID].enemyIDList.Count=" + adventureTeamList[teamID].enemyIDList.Count);
            Debug.Log("遭遇了" + monsterNameList + ",开始战斗！");
            adventureTeamList[teamID].action = AdventureAction.Fight;
            adventureTeamList[teamID].fightRound = 1;



            AdventureTeamLogAdd(teamID, "遭遇了" + monsterNameList + ",开始战斗！");
            PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
            if (AreaMapPanel.Instance.dungeonInfoBlockID == adventureTeamList[teamID].dungeonID)
            {
                AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
            }
        }

        //Debug.Log("fightMenberObjects.Count="+fightMenberObjects.Count);
        AdventureMainPanel.Instance.UpdateSceneRoleFormations(teamID);
        AdventureMainPanel.Instance.UpdateSceneRole(teamID);

        AdventureMainPanel.Instance.UpdateSceneEnemy(teamID);
        AdventureMainPanel.Instance.HideSceneRoleHpMp(teamID);
        AdventureMainPanel.Instance.UpdateSceneRoleHpMp(teamID, fightMenberObjects);
        AdventureMainPanel.Instance.ShowSceneRoleAp(teamID, fightMenberObjects);
        AdventureMainPanel.Instance.UpdateSceneRoleAp(teamID, fightMenberObjects);
        AdventureMainPanel.Instance.UpdateSceneRoleSharpness(teamID, fightMenberObjects);
        AdventureMainPanel.Instance.UpdateSceneRoleApText(teamID, fightMenberObjects);
        AdventureMainPanel.Instance.HideSceneRoleBuff(teamID);
        AdventureMainPanel.Instance.UpdateSceneRoleBuff(teamID, fightMenberObjects);
        AdventureMainPanel.Instance.HideElementPoint(teamID);
        AdventureMainPanel.Instance.UpdateElementPoint(teamID);
        AdventureDungeonCheckHalo(teamID);
        AdventureMainPanel.Instance.UpdateSceneRoleHalo(teamID, fightMenberObjects);

        if (AdventureTeamPanel.Instance.isShow && AdventureTeamPanel.Instance.nowTeam == teamID)
        {
            AdventureTeamPanel.Instance.UpdateNow(teamID);
        }

        yield return new WaitForSeconds(0.5f);





        List<FightMenberObject> actionMenber = new List<FightMenberObject>();

        while (adventureTeamList[teamID].fightRound <= RoundLimit)
        {

            //选取行动槽满(8000)的战斗成员
            while (actionMenber.Count == 0)
            {
                yield return new WaitForSeconds(0.02f);
                for (int i = 0; i < fightMenberObjects.Count; i++)
                {
                    if (fightMenberObjects[i].hpNow > 0)
                    {
                        fightMenberObjects[i].actionBar += fightMenberObjects[i].spd;
                        if (fightMenberObjects[i].actionBar >= 8000)
                        {
                            actionMenber.Add(fightMenberObjects[i]);
                            fightMenberObjects[i].actionBar = (short)(fightMenberObjects[i].actionBar - 8000);
                        }
                        AdventureMainPanel.Instance.UpdateSceneRoleApSingle(teamID, fightMenberObjects[i]);    
                    }
                }
            }
            //行动成员行动
            for (int i = 0; i < actionMenber.Count; i++)
            {
                if (CheckFightOver(fightMenberObjects) != -1)
                {
                    break;
                }

                //行动前执行BUFF效果（同时减掉回合数）
                bool canAction = true;
                for (int j = 0; j < actionMenber[i].buff.Count; j++)
                {
                    switch (actionMenber[i].buff[j].type)
                    {
                        case FightBuffType.Dizzy:
                            canAction = false;
                            break;
                        case FightBuffType.Confusion:

                            break;
                        case FightBuffType.Poison://每回合5%伤害
                            //造成伤害
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(actionMenber[i]) + "受到" + (int)(actionMenber[i].hp * 0.05f) + "点中毒伤害");
                            actionMenber[i].hpNow -= (int)(actionMenber[i].hp * 0.05f);
                            if (actionMenber[i].hpNow < 0)
                            {
                                actionMenber[i].hpNow = 0;
                                AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(actionMenber[i]) + "被打倒了！");
                            }
                            break;
                        case FightBuffType.Sleep:
                            canAction = false;
                            break;
                        default:
                            break;
                    }
                    actionMenber[i].buff[j].round--;
                }
                AdventureMainPanel.Instance.UpdateSceneRoleBuffSingle(teamID, actionMenber[i]);

                //行动前执行回合HPMP固定恢复
                if (actionMenber[i].hpNow > 0)
                {
                    if (actionMenber[i].hpRenew > 0)
                    {
                        StartCoroutine(TakeCure(teamID, adventureTeamList[teamID].fightRound, null, actionMenber[i], null, Attribute.Hp, (int)(actionMenber[i].hp * (actionMenber[i].hpRenew / 100f)), 1));
                    }
                    if (actionMenber[i].mpRenew > 0)
                    {
                        StartCoroutine(TakeCure(teamID, adventureTeamList[teamID].fightRound, null, actionMenber[i], null, Attribute.Mp, (int)(actionMenber[i].mp * (actionMenber[i].mpRenew / 100f)), 1));
                    }
                }


                if (actionMenber[i].hpNow > 0 && canAction)
                {
                    byte skillIndex = actionMenber[i].skillIndex;
                    int skillID = -1;

                    if (actionMenber[i].side == 0)
                    {
                        skillID = heroDic[actionMenber[i].objectID].skill[skillIndex];
                    }
                    else if (actionMenber[i].side == 1)
                    {
                        skillID = DataManager.mMonsterDict[actionMenber[i].objectID].SkillID[skillIndex];
                    }


                    if (skillID != -1)
                    {
                        //SkillObject so = skillDic[skillID];
                        //SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];
                        //float skillLevelUp = 0f;
                        //if (actionMenber[i].side == 0)
                        //{
                        //    if (heroDic[actionMenber[i].objectID].skillInfo.ContainsKey(so.prototypeID))
                        //    {
                        //        skillLevelUp = 0.1f * (heroDic[actionMenber[i].objectID].skillInfo[so.prototypeID].level - 1);
                        //    }
                        //}
                        if (GetSkillMpCost(skillID) <= actionMenber[i].mpNow)
                        {
                            //int ran = Random.Range(0, 100);
                            if (Random.Range(0, 100) < GetSkillProbability(skillID))
                            {
                                Debug.Log("[" + adventureTeamList[teamID].fightRound + "]" + actionMenber[i].name + "发动技能");
                                SkillAttack(teamID, actionMenber[i], fightMenberObjects, skillID);
                                //StartCoroutine(Attack(teamID, actionMenber[i], sp));

                                //actionMenber[i].mpNow -= GetSkillMpCost(skillID);
                                //AdventureMainPanel.Instance.UpdateSceneRoleHpMpSingle(teamID, actionMenber[i]);
                                //AdventureMainPanel.Instance.UpdateHeroHpMpSingle(teamID, actionMenber[i]);
                                ////选取目标
                                //List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], sp);

                                //if (targetMenber.Count > 0)
                                //{
                                //    //对目标行动
                                //    for (int j = 0; j < targetMenber.Count; j++)
                                //    {
                                //        if (sp.FlagDamage)
                                //        {
                                           
                                //            if (IsHit(actionMenber[i], targetMenber[j]))
                                //            {
                                //                int damageMin = System.Math.Max(0, (int)(actionMenber[i].atkMin * (sp.Atk / 100f) - targetMenber[j].def)) + System.Math.Max(0, (int)(actionMenber[i].mAtkMin * (sp.MAtk / 100f) - targetMenber[j].mDef));
                                //                int damageMax = System.Math.Max(0, (int)(actionMenber[i].atkMax * (sp.Atk / 100f) - targetMenber[j].def)) + System.Math.Max(0, (int)(actionMenber[i].mAtkMax * (sp.MAtk / 100f) - targetMenber[j].mDef));

                                //                if (sp.Sword != 0 && actionMenber[i].weaponType == ItemTypeSmall.Sword)
                                //                {
                                //                    damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                                //                    damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                                //                }
                                //                if (sp.Axe != 0 && actionMenber[i].weaponType == ItemTypeSmall.Axe)
                                //                {
                                //                    damageMin = (int)(damageMin * (1f + (sp.Axe / 100f)));
                                //                    damageMax = (int)(damageMax * (1f + (sp.Axe / 100f)));
                                //                }
                                //                if (sp.Spear != 0 && actionMenber[i].weaponType == ItemTypeSmall.Spear)
                                //                {
                                //                    damageMin = (int)(damageMin * (1f + (sp.Spear / 100f)));
                                //                    damageMax = (int)(damageMax * (1f + (sp.Spear / 100f)));
                                //                }
                                //                if (sp.Hammer != 0 && actionMenber[i].weaponType == ItemTypeSmall.Hammer)
                                //                {
                                //                    damageMin = (int)(damageMin * (1f + (sp.Hammer / 100f)));
                                //                    damageMax = (int)(damageMax * (1f + (sp.Hammer / 100f)));
                                //                }
                                //                if (sp.Bow != 0 && actionMenber[i].weaponType == ItemTypeSmall.Bow)
                                //                {
                                //                    damageMin = (int)(damageMin * (1f + (sp.Bow / 100f)));
                                //                    damageMax = (int)(damageMax * (1f + (sp.Bow / 100f)));
                                //                }
                                //                if (sp.Staff != 0 && actionMenber[i].weaponType == ItemTypeSmall.Staff)
                                //                {
                                //                    damageMin = (int)(damageMin * (1f + (sp.Staff / 100f)));
                                //                    damageMax = (int)(damageMax * (1f + (sp.Staff / 100f)));
                                //                }

                                //                int damage = Random.Range(damageMin, damageMax + 1);

                                //                float haloDamageUp = 0f;
                                //                if (actionMenber[i].haloStatus)
                                //                {
                                //                    haloDamageUp += DataManager.mHaloDict[heroDic[actionMenber[i].objectID].halo].DamageUp / 100f;
                                //                }
                                //                if (targetMenber[j].haloStatus)
                                //                {
                                //                    haloDamageUp -= DataManager.mHaloDict[heroDic[actionMenber[i].objectID].halo].Offset / 100f;
                                //                }

                                //                //斩味修正伤害
                                //                damage = (int)(damage*(1f+ haloDamageUp) * GetSharpnessModifyDamage(GetSharpnessLevel(actionMenber[i])));
                             

                                //                int damageWithElement = 0;


                                //                if (sp.Element.Contains(0))
                                //                {
                                //                    damageWithElement = damage;
                                //                }
                                //                else
                                //                {
                                //                    if (sp.Element.Contains(1))
                                //                    {
                                //                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].windDam + sp.Wind + adventureTeamList[teamID].dungeonEPWind * 20 - targetMenber[j].windRes) / 100f)));
                                //                    }
                                //                    if (sp.Element.Contains(2))
                                //                    {
                                //                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].fireDam + sp.Fire + adventureTeamList[teamID].dungeonEPFire * 20 - targetMenber[j].fireRes) / 100f)));
                                //                    }
                                //                    if (sp.Element.Contains(3))
                                //                    {
                                //                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].waterDam + sp.Water + adventureTeamList[teamID].dungeonEPWater * 20 - targetMenber[j].waterRes) / 100f)));
                                //                    }
                                //                    if (sp.Element.Contains(4))
                                //                    {
                                //                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].groundDam + sp.Ground + adventureTeamList[teamID].dungeonEPGround * 20 - targetMenber[j].groundRes) / 100f)));
                                //                    }
                                //                    if (sp.Element.Contains(5))
                                //                    {
                                //                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].lightDam + sp.Light + adventureTeamList[teamID].dungeonEPLight * 20 - targetMenber[j].lightRes) / 100f)));
                                //                    }
                                //                    if (sp.Element.Contains(6))
                                //                    {
                                //                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].darkDam + sp.Dark + adventureTeamList[teamID].dungeonEPDark * 20 - targetMenber[j].darkRes) / 100f)));
                                //                    }
                                //                }

                                //                float haloElementDamageUp = 0f;
                                //                if (actionMenber[i].haloStatus)
                                //                {
                                //                    haloElementDamageUp += DataManager.mHaloDict[heroDic[actionMenber[i].objectID].halo].EDamageUp/100f;
                                //                }
                                //                if (targetMenber[j].haloStatus)
                                //                {
                                //                    haloElementDamageUp -= DataManager.mHaloDict[heroDic[actionMenber[i].objectID].halo].EOffset / 100f;
                                //                }

                                //                damageWithElement = (int)((1f + skillLevelUp+ haloElementDamageUp) * damageWithElement);



                                             
                                //                if (IsCri(actionMenber[i]))
                                //                {
                                //                    byte criDUp = 0;
                                //                    if (actionMenber[i].haloStatus)
                                //                    {
                                //                        criDUp= DataManager.mHaloDict[heroDic[actionMenber[i].objectID].halo].CriDUp;
                                //                    }

                                //                    damageWithElement = (int)(damageWithElement * ((actionMenber[i].criD+ criDUp) / 100f));
                                //                }


                                //                byte damageTimes = 1;
                                //                if (so.comboMax != 0)
                                //                {
                                //                    for (byte k = 0; k < so.comboMax; k++)
                                //                    {
                                //                        int ranCombo = Random.Range(0, 100);
                                //                        if (ranCombo < so.comboRate)
                                //                        {
                                //                            damageTimes++;
                                //                        }
                                //                        else
                                //                        {
                                //                            break;
                                //                        }
                                //                    }
                                //                }

                                //                StartCoroutine(TakeDamage(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[j], sp, damageWithElement, damageTimes, 1f + skillLevelUp));


                                //            }
                                //            else//未命中
                                //            {
                                //                Miss(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[j]);
                                //            }


                                //        }

                                //        if (sp.FlagDebuff)
                                //        {
                                //            if (sp.Dizzy != 0)
                                //            {
                                //                int hitRate = System.Math.Max(0, (int)(sp.Dizzy * (1f + skillLevelUp)) - targetMenber[j].dizzyRes);
                                //                int ranHit = Random.Range(0, 100);
                                //                if (ranHit < hitRate)
                                //                {
                                //                    bool buffExist = false;
                                //                    for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                    {
                                //                        if (targetMenber[j].buff[k].type == FightBuffType.Dizzy)
                                //                        {
                                //                            targetMenber[j].buff[k].round = (byte)sp.DizzyValue;
                                //                            buffExist = true;
                                //                            break;
                                //                        }
                                //                    }
                                //                    if (!buffExist)
                                //                    {
                                //                        targetMenber[j].buff.Add(new FightBuff(FightBuffType.Dizzy, 0, (byte)sp.DizzyValue));
                                //                    }

                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "眩晕了");
                                //                }
                                //                else
                                //                {
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "眩晕效果未生效");
                                //                }
                                //            }
                                //            if (sp.Confusion != 0)
                                //            {
                                //                int hitRate = System.Math.Max(0, (int)(sp.Confusion * (1f + skillLevelUp)) - targetMenber[j].confusionRes);
                                //                int ranHit = Random.Range(0, 100);
                                //                if (ranHit < hitRate)
                                //                {
                                //                    bool buffExist = false;
                                //                    for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                    {
                                //                        if (targetMenber[j].buff[k].type == FightBuffType.Confusion)
                                //                        {
                                //                            targetMenber[j].buff[k].round = (byte)sp.ConfusionValue;
                                //                            buffExist = true;
                                //                            break;
                                //                        }
                                //                    }
                                //                    if (!buffExist)
                                //                    {
                                //                        targetMenber[j].buff.Add(new FightBuff(FightBuffType.Confusion, 0, (byte)sp.DizzyValue));
                                //                    }

                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "混乱了");
                                //                }
                                //                else
                                //                {
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "混乱效果未生效");
                                //                }
                                //            }
                                //            if (sp.Sleep != 0)
                                //            {
                                //                int hitRate = System.Math.Max(0, (int)(sp.Sleep * (1f + skillLevelUp)) - targetMenber[j].sleepRes);
                                //                int ranHit = Random.Range(0, 100);
                                //                if (ranHit < hitRate)
                                //                {
                                //                    bool buffExist = false;
                                //                    for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                    {
                                //                        if (targetMenber[j].buff[k].type == FightBuffType.Sleep)
                                //                        {
                                //                            targetMenber[j].buff[k].round = (byte)sp.ConfusionValue;
                                //                            buffExist = true;
                                //                            break;
                                //                        }
                                //                    }
                                //                    if (!buffExist)
                                //                    {
                                //                        targetMenber[j].buff.Add(new FightBuff(FightBuffType.Sleep, 0, (byte)sp.SleepValue));
                                //                    }

                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "睡眠了");
                                //                }
                                //                else
                                //                {
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "睡眠效果未生效");
                                //                }
                                //            }
                                //            if (sp.Poison != 0)
                                //            {
                                //                int hitRate = System.Math.Max(0, (int)(sp.Poison * (1f + skillLevelUp)) - targetMenber[j].poisonRes);
                                //                int ranHit = Random.Range(0, 100);
                                //                if (ranHit < hitRate)
                                //                {
                                //                    bool buffExist = false;
                                //                    for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                    {
                                //                        if (targetMenber[j].buff[k].type == FightBuffType.Poison)
                                //                        {
                                //                            targetMenber[j].buff[k].round = (byte)sp.ConfusionValue;
                                //                            buffExist = true;
                                //                            break;
                                //                        }
                                //                    }
                                //                    if (!buffExist)
                                //                    {
                                //                        targetMenber[j].buff.Add(new FightBuff(FightBuffType.Poison, 0, (byte)sp.PoisonValue));
                                //                    }

                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "中毒了");
                                //                }
                                //                else
                                //                {
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "中毒效果未生效");
                                //                }
                                //            }
                                //            AdventureMainPanel.Instance.UpdateSceneRoleBuffSingle(teamID, targetMenber[j]);
                                //        }

                                //        if (sp.Cure != 0)
                                //        {
                                //            int cure = (int)(targetMenber[j].hp * (sp.Cure / 100f));
                                //            cure = (int)(cure * (1f + skillLevelUp));

                                //            byte damageTimes = 1;
                                //            if (so.comboMax != 0)
                                //            {
                                //                for (byte k = 0; k < so.comboMax; k++)
                                //                {
                                //                    int ranCombo = Random.Range(0, 100);
                                //                    if (ranCombo < so.comboRate)
                                //                    {
                                //                        damageTimes++;
                                //                    }
                                //                    else
                                //                    {
                                //                        break;
                                //                    }
                                //                }
                                //            }
                                //            StartCoroutine(TakeCure(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[j], sp, Attribute.Hp, cure, damageTimes));


                                //        }

                                //        if (sp.FlagBuff)
                                //        {
                                //            if (sp.UpAtk != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpAtk)
                                //                    {
                                //                        targetMenber[j].buff[k].round = 2;
                                //                        buffExist = true;
                                //                        break;
                                //                    }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpAtk, (byte)(sp.UpAtk * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].atkMin += (short)(targetMenber[j].atkMin * (sp.UpAtk * (1f + skillLevelUp) / 100f));
                                //                    targetMenber[j].atkMax += (short)(targetMenber[j].atkMax * (sp.UpAtk * (1f + skillLevelUp) / 100f));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "物理攻击提升了");
                                //                }
                                //            }
                                //            if (sp.UpMAtk != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpMAtk) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpMAtk, (byte)(sp.UpMAtk * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].mAtkMin += (short)(targetMenber[j].mAtkMin * (sp.UpMAtk * (1f + skillLevelUp) / 100f));
                                //                    targetMenber[j].mAtkMax += (short)(targetMenber[j].mAtkMax * (sp.UpMAtk * (1f + skillLevelUp) / 100f));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "魔法攻击提升了");
                                //                }
                                //            }
                                //            if (sp.UpDef != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpDef) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDef, (byte)(sp.UpDef * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].def += (short)(targetMenber[j].def * (sp.UpDef * (1f + skillLevelUp) / 100f));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "物理防御提升了");
                                //                }
                                //            }
                                //            if (sp.UpMDef != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpMDef) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpMDef, (byte)(sp.UpMDef * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].mDef += (short)(targetMenber[j].mDef * (sp.UpMDef * (1f + skillLevelUp) / 100f));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "魔法防御提升了");
                                //                }
                                //            }
                                //            if (sp.UpHit != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpHit) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpHit, (byte)(sp.UpHit * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].hit += (short)(targetMenber[j].hit * (sp.UpHit * (1f + skillLevelUp) / 100f));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "命中提升了");
                                //                }
                                //            }
                                //            if (sp.UpDod != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpDod) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDod, (byte)(sp.UpDod * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].dod += (short)(targetMenber[j].dod * (sp.UpDod * (1f + skillLevelUp) / 100f));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "闪避提升了");
                                //                }
                                //            }
                                //            if (sp.UpCriD != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpCriD) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpCriD, (byte)(sp.UpCriD * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].criD += (short)(targetMenber[j].criD * (sp.UpCriD * (1f + skillLevelUp) / 100f));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暴击伤害提升了");
                                //                }
                                //            }

                                //            if (sp.UpWindDam != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpWindDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWindDam, (byte)(sp.UpWindDam * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].windDam += (short)(sp.UpWindDam * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "风系伤害提升了");
                                //                }
                                //            }
                                //            if (sp.UpFireDam != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpFireDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpFireDam, (byte)(sp.UpFireDam * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].fireDam += (short)(sp.UpFireDam * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "火系伤害提升了");
                                //                }
                                //            }
                                //            if (sp.UpWaterDam != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpWaterDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWaterDam, (byte)(sp.UpWaterDam * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].waterDam += (short)(sp.UpWaterDam * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "水系伤害提升了");
                                //                }
                                //            }
                                //            if (sp.UpGroundDam != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpGroundDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpGroundDam, (byte)(sp.UpGroundDam * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].groundDam += (short)(sp.UpGroundDam * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "地系伤害提升了");
                                //                }
                                //            }
                                //            if (sp.UpLightDam != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpLightDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpLightDam, (byte)(sp.UpLightDam * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].lightDam += (short)(sp.UpLightDam * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "光系伤害提升了");
                                //                }
                                //            }
                                //            if (sp.UpDarkDam != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpDarkDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDarkDam, (byte)(sp.UpDarkDam * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].darkDam += (short)(sp.UpDarkDam * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暗系伤害提升了");
                                //                }
                                //            }

                                //            if (sp.UpWindRes != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpWindRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWindRes, (byte)(sp.UpWindRes * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].windRes += (short)(sp.UpWindRes * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "风系抗性提升了");
                                //                }
                                //            }
                                //            if (sp.UpFireRes != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpFireRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpFireRes, (byte)(sp.UpFireRes * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].fireRes += (short)(sp.UpFireRes * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "火系抗性提升了");
                                //                }
                                //            }
                                //            if (sp.UpWaterRes != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpWaterRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWaterRes, (byte)(sp.UpWaterRes * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].waterRes += (short)(sp.UpWaterRes * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "水系抗性提升了");
                                //                }
                                //            }
                                //            if (sp.UpGroundRes != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpGroundRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpGroundRes, (byte)(sp.UpGroundRes * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].groundRes += (short)(sp.UpGroundRes * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "地系抗性提升了");
                                //                }
                                //            }
                                //            if (sp.UpLightRes != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpLightRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpLightRes, (byte)(sp.UpLightRes * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].lightRes += (short)(sp.UpLightRes * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "光系抗性提升了");
                                //                }
                                //            }
                                //            if (sp.UpDarkRes != 0)
                                //            {
                                //                bool buffExist = false;
                                //                for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                //                {
                                //                    if (targetMenber[j].buff[k].type == FightBuffType.UpDarkRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                                //                }
                                //                if (!buffExist)
                                //                {
                                //                    targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDarkRes, (byte)(sp.UpDarkRes * (1f + skillLevelUp)), 2));
                                //                    targetMenber[j].darkRes += (short)(sp.UpDarkRes * (1f + skillLevelUp));
                                //                    AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暗系抗性提升了");
                                //                }
                                //            }

                                //            AdventureMainPanel.Instance.UpdateSceneRoleBuffSingle(teamID, targetMenber[j]);
                                //        }

                                //        //TODO:[待优化设计]夺金设计概率
                                //        if (so.gold != 0 && targetMenber[j].side == 1)
                                //        {
                                //            int ranGold = Random.Range(0, 100);
                                //            if (ranGold < 30)
                                //            {
                                //                short gold = (short)(DataManager.mMonsterDict[targetMenber[j].objectID].GoldDrop * (so.gold / 100f));
                                //                adventureTeamList[teamID].getGold += gold;
                                //                AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(actionMenber[i]) + "从" + OutputNameWithColor(targetMenber[j]) + "夺得" + gold + "金币");
                                //            }
                                //        }
                                //    }
                                //}
                            }
                            else//技能概率未触发，普通攻击
                            {
                                Debug.Log("[" + adventureTeamList[teamID].fightRound + "]" + actionMenber[i].name + "技能概率未触发，普通攻击");
                                NormalAttack(teamID, actionMenber[i], fightMenberObjects);
                            }
                        }
                        else//MP不足，普通攻击
                        {
                            Debug.Log("[" + adventureTeamList[teamID].fightRound + "]" + actionMenber[i].name + "MP不足，普通攻击");
                            NormalAttack(teamID, actionMenber[i], fightMenberObjects);
                        }
                    }
                    else//普通攻击
                    {
                        Debug.Log("[" + adventureTeamList[teamID].fightRound + "]" + actionMenber[i].name + "发动设置的普通攻击");
                        NormalAttack(teamID, actionMenber[i], fightMenberObjects);
                    }

                    adventureTeamList[teamID].fightRound++;

                    actionMenber[i].skillIndex++;
                    if (actionMenber[i].skillIndex >= 4)
                    {
                        actionMenber[i].skillIndex = 0;
                    }

                    if (CheckFightOver(fightMenberObjects) != -1)
                    {
                        break;
                    }
                }

                yield return new WaitForSeconds(1f);//单个行动角色行动完后等1s
                //移除失效的buff
                for (int j = actionMenber[i].buff.Count - 1; j >= 0; j--)
                {
                    if (actionMenber[i].buff[j].round == 0)
                    {
                        switch (actionMenber[i].buff[j].type)
                        {
                            case FightBuffType.UpAtk:
                                actionMenber[i].atkMin = (short)(actionMenber[i].atkMin / (1f + actionMenber[i].buff[j].value / 100f));
                                actionMenber[i].atkMax = (short)(actionMenber[i].atkMax / (1f + actionMenber[i].buff[j].value / 100f));
                                break;
                            case FightBuffType.UpMAtk:
                                actionMenber[i].mAtkMin = (short)(actionMenber[i].mAtkMin / (1f + actionMenber[i].buff[j].value / 100f));
                                actionMenber[i].mAtkMax = (short)(actionMenber[i].mAtkMax / (1f + actionMenber[i].buff[j].value / 100f));
                                break;
                            case FightBuffType.UpDef:
                                actionMenber[i].def = (short)(actionMenber[i].def / (1f + actionMenber[i].buff[j].value / 100f));
                                break;
                            case FightBuffType.UpMDef:
                                actionMenber[i].mDef = (short)(actionMenber[i].mDef / (1f + actionMenber[i].buff[j].value / 100f));
                                break;
                            case FightBuffType.UpHit:
                                actionMenber[i].hit = (short)(actionMenber[i].hit / (1f + actionMenber[i].buff[j].value / 100f));
                                break;
                            case FightBuffType.UpDod:
                                actionMenber[i].dod = (short)(actionMenber[i].dod / (1f + actionMenber[i].buff[j].value / 100f));
                                break;
                            case FightBuffType.UpCriD:
                                actionMenber[i].criD = (short)(actionMenber[i].criD / (1f + actionMenber[i].buff[j].value / 100f));
                                break;
                            case FightBuffType.UpWindDam:
                                actionMenber[i].windDam -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpFireDam:
                                actionMenber[i].fireDam -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpWaterDam:
                                actionMenber[i].waterDam -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpGroundDam:
                                actionMenber[i].groundDam -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpLightDam:
                                actionMenber[i].lightDam -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpDarkDam:
                                actionMenber[i].darkDam -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpWindRes:
                                actionMenber[i].windRes -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpFireRes:
                                actionMenber[i].fireRes -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpWaterRes:
                                actionMenber[i].waterRes -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpGroundRes:
                                actionMenber[i].groundRes -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpLightRes:
                                actionMenber[i].lightRes -= actionMenber[i].buff[j].value;
                                break;
                            case FightBuffType.UpDarkRes:
                                actionMenber[i].darkRes -= actionMenber[i].buff[j].value;
                                break;
                        }

                        actionMenber[i].buff.Remove(actionMenber[i].buff[j]);
                    }
                }
                AdventureMainPanel.Instance.UpdateSceneRoleBuffSingle(teamID, actionMenber[i]);
            }

            actionMenber.Clear();
            CheckFightOverResult = CheckFightOver(fightMenberObjects);
            if (CheckFightOverResult != -1)
            {
                break;
            }
            if (adventureTeamList[teamID].state == AdventureState.Retreat)
            {
                break;
            }
        }
        if (adventureTeamList[teamID].fightRound > RoundLimit)
        {
            AdventureTeamLogAdd(teamID, "超过" + RoundLimit + "回合");
            CheckFightOverResult = 1;
        }

        yield return new WaitForSeconds(1.5f);

        string log = "行进过程中遇到了敌人:";

        for (byte i = 0; i < fightMenberObjects.Count; i++)
        {
            if (fightMenberObjects[i].side == 1)
            {
                log += fightMenberObjects[i].name + "(Lv." + fightMenberObjects[i].level + ")";
            }
        }

        if (CheckFightOverResult == 0)
        {

            string getStr = "";
            short getExp = 0;
            short getGold = 0;
            List<short> getItemList = new List<short>();
            for (int i = 0; i < fightMenberObjects.Count; i++)
            {
                if (fightMenberObjects[i].side == 1)
                {
                    adventureTeamList[teamID].killNum++;
                    adventureTeamList[teamID].getExp += DataManager.mMonsterDict[fightMenberObjects[i].objectID].ExpDrop;
                    getExp += DataManager.mMonsterDict[fightMenberObjects[i].objectID].ExpDrop;
                    adventureTeamList[teamID].getGold += DataManager.mMonsterDict[fightMenberObjects[i].objectID].GoldDrop;
                    getGold += DataManager.mMonsterDict[fightMenberObjects[i].objectID].GoldDrop;
                    for (int j = 0; j < DataManager.mMonsterDict[fightMenberObjects[i].objectID].ItemDrop.Count; j++)
                    {
                        int ran = Random.Range(0, 100);
                        if (ran < DataManager.mMonsterDict[fightMenberObjects[i].objectID].ItemDropRate[j])
                        {
                            for (int k = 0; k < Random.Range(DataManager.mMonsterDict[fightMenberObjects[i].objectID].ItemDropNumMin[j], DataManager.mMonsterDict[fightMenberObjects[i].objectID].ItemDropNumMin[j] + 1); k++)
                            {
                                adventureTeamList[teamID].getItemList.Add(DataManager.mMonsterDict[fightMenberObjects[i].objectID].ItemDrop[j]);
                                getStr += "[" + DataManager.mItemDict[DataManager.mMonsterDict[fightMenberObjects[i].objectID].ItemDrop[j]].Name + "] ";
                                getItemList.Add(DataManager.mMonsterDict[fightMenberObjects[i].objectID].ItemDrop[j]);
                            }
                        }
                    }
                }
            }

            AdventureMainPanel.Instance.ShowDropsIcon(teamID, (getGold != 0 ? new List<string> { "Gold" } : new List<string> { }), getItemList ,Vector2.zero);

            AdventureTeamLogAdd(teamID, "战斗胜利！获得[经验值" + getExp + "][金币" + getGold + "] " + getStr);


            //Debug.Log("adventureTeamList[teamID]" + adventureTeamList[teamID].state + " adventureTeamList[teamID].action=" + adventureTeamList[teamID].action);
            adventureTeamList[teamID].action = AdventureAction.Walk;
            AdventureMainPanel.Instance.UpdateSceneRoleFormations(teamID);
            AdventureMainPanel.Instance.UpdateSceneRole(teamID);
            AdventureMainPanel.Instance.UpdateTeam(teamID);

            //结算
            for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
            {
                adventureTeamList[teamID].heroHpList[i] = System.Math.Max(1, fightMenberObjects[i].hpNow);
                adventureTeamList[teamID].heroMpList[i] = fightMenberObjects[i].mpNow;
            }
            CreateAdventureEventPartLog(teamID, AdventureEvent.Monster, true, log + "\n经过战斗全灭了敌人");
            CreateAdventureEvent(teamID);
        }
        else if (CheckFightOverResult == 1)
        {
            AdventureTeamLogAdd(teamID, "被击败了！");

            for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
            {
                adventureTeamList[teamID].heroHpList[i] = fightMenberObjects[i].hpNow;
                adventureTeamList[teamID].heroMpList[i] = fightMenberObjects[i].mpNow;
            }

            CreateAdventureEventPartLog(teamID, AdventureEvent.Monster, false, log + "\n在战斗中不敌对手,全军覆没");
            AdventureTeamEnd(teamID, AdventureState.Fail);
        }
        else
        {
            if (adventureTeamList[teamID].state == AdventureState.Retreat)
            {
                AdventureTeamLogAdd(teamID, "撤出战斗！");

                for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
                {
                    adventureTeamList[teamID].heroHpList[i] = fightMenberObjects[i].hpNow;
                    adventureTeamList[teamID].heroMpList[i] = fightMenberObjects[i].mpNow;
                }

                CreateAdventureEventPartLog(teamID, AdventureEvent.Monster, false, log + "\n全队撤出了战斗,返回据点");
                AdventureTeamEnd(teamID, AdventureState.Retreat);
            }
        }
        fightMenberObjectSS[teamID].Clear();


        AdventureMainPanel.Instance.UpdateTeamHero(teamID);

        if (AdventureMainPanel.Instance.isShow && AdventureMainPanel.Instance.nowCheckingTeamID == teamID)
        {
            AudioControl.Instance.PlayMusic(AudioControl.Instance.nowMusic);
        }

    }
    void SkillAttack(byte teamID, FightMenberObject actionMenber, List<FightMenberObject> fightMenberObjects,int skillID)
    {
        SkillObject so = skillDic[skillID];
        SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];
        float skillLevelUp = 0f;
        if (actionMenber.side == 0)
        {
            if (heroDic[actionMenber.objectID].skillInfo.ContainsKey(so.prototypeID))
            {
                skillLevelUp = 0.1f * (heroDic[actionMenber.objectID].skillInfo[so.prototypeID].level - 1);
            }
        }
        StartCoroutine(Attack(teamID, actionMenber, sp));

        actionMenber.mpNow -= GetSkillMpCost(skillID);
        AdventureMainPanel.Instance.UpdateSceneRoleHpMpSingle(teamID, actionMenber);
        AdventureMainPanel.Instance.UpdateHeroHpMpSingle(teamID, actionMenber);
        //选取目标
        List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber, sp);

        if (targetMenber.Count > 0)
        {
            //对目标行动
            for (int j = 0; j < targetMenber.Count; j++)
            {
                if (sp.FlagDamage)
                {

                    if (IsHit(actionMenber, targetMenber[j]))
                    {
                        int damageMin = System.Math.Max(0, (int)(actionMenber.atkMin * (sp.Atk / 100f) - targetMenber[j].def)) + System.Math.Max(0, (int)(actionMenber.mAtkMin * (sp.MAtk / 100f) - targetMenber[j].mDef));
                        int damageMax = System.Math.Max(0, (int)(actionMenber.atkMax * (sp.Atk / 100f) - targetMenber[j].def)) + System.Math.Max(0, (int)(actionMenber.mAtkMax * (sp.MAtk / 100f) - targetMenber[j].mDef));

                        if (sp.Sword != 0 && actionMenber.weaponType == ItemTypeSmall.Sword)
                        {
                            damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                            damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                        }
                        if (sp.Axe != 0 && actionMenber.weaponType == ItemTypeSmall.Axe)
                        {
                            damageMin = (int)(damageMin * (1f + (sp.Axe / 100f)));
                            damageMax = (int)(damageMax * (1f + (sp.Axe / 100f)));
                        }
                        if (sp.Spear != 0 && actionMenber.weaponType == ItemTypeSmall.Spear)
                        {
                            damageMin = (int)(damageMin * (1f + (sp.Spear / 100f)));
                            damageMax = (int)(damageMax * (1f + (sp.Spear / 100f)));
                        }
                        if (sp.Hammer != 0 && actionMenber.weaponType == ItemTypeSmall.Hammer)
                        {
                            damageMin = (int)(damageMin * (1f + (sp.Hammer / 100f)));
                            damageMax = (int)(damageMax * (1f + (sp.Hammer / 100f)));
                        }
                        if (sp.Bow != 0 && actionMenber.weaponType == ItemTypeSmall.Bow)
                        {
                            damageMin = (int)(damageMin * (1f + (sp.Bow / 100f)));
                            damageMax = (int)(damageMax * (1f + (sp.Bow / 100f)));
                        }
                        if (sp.Staff != 0 && actionMenber.weaponType == ItemTypeSmall.Staff)
                        {
                            damageMin = (int)(damageMin * (1f + (sp.Staff / 100f)));
                            damageMax = (int)(damageMax * (1f + (sp.Staff / 100f)));
                        }

                        int damage = Random.Range(damageMin, damageMax + 1);

                        float haloDamageUp = 0f;
                        if (actionMenber.haloStatus)
                        {
                            haloDamageUp += DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].DamageUp / 100f;
                        }
                        if (targetMenber[j].haloStatus)
                        {
                            haloDamageUp -= DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].Offset / 100f;
                        }

                        //斩味修正伤害
                        damage = (int)(damage * (1f + haloDamageUp) * GetSharpnessModifyDamage(GetSharpnessLevel(actionMenber)));


                        int damageWithElement = 0;


                        if (sp.Element.Contains(0))
                        {
                            damageWithElement = damage;
                        }
                        else
                        {
                            if (sp.Element.Contains(1))
                            {
                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber.windDam + sp.Wind + adventureTeamList[teamID].dungeonEPWind * 20 - targetMenber[j].windRes) / 100f)));
                            }
                            if (sp.Element.Contains(2))
                            {
                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber.fireDam + sp.Fire + adventureTeamList[teamID].dungeonEPFire * 20 - targetMenber[j].fireRes) / 100f)));
                            }
                            if (sp.Element.Contains(3))
                            {
                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber.waterDam + sp.Water + adventureTeamList[teamID].dungeonEPWater * 20 - targetMenber[j].waterRes) / 100f)));
                            }
                            if (sp.Element.Contains(4))
                            {
                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber.groundDam + sp.Ground + adventureTeamList[teamID].dungeonEPGround * 20 - targetMenber[j].groundRes) / 100f)));
                            }
                            if (sp.Element.Contains(5))
                            {
                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber.lightDam + sp.Light + adventureTeamList[teamID].dungeonEPLight * 20 - targetMenber[j].lightRes) / 100f)));
                            }
                            if (sp.Element.Contains(6))
                            {
                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber.darkDam + sp.Dark + adventureTeamList[teamID].dungeonEPDark * 20 - targetMenber[j].darkRes) / 100f)));
                            }
                        }

                        float haloElementDamageUp = 0f;
                        if (actionMenber.haloStatus)
                        {
                            haloElementDamageUp += DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].EDamageUp / 100f;
                        }
                        if (targetMenber[j].haloStatus)
                        {
                            haloElementDamageUp -= DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].EOffset / 100f;
                        }

                        damageWithElement = (int)((1f + skillLevelUp + haloElementDamageUp) * damageWithElement);



                        bool isCri = IsCri(actionMenber);
                        if (isCri)
                        {
                            byte criDUp = 0;
                            if (actionMenber.haloStatus)
                            {
                                criDUp = DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].CriDUp;
                            }

                            damageWithElement = (int)(damageWithElement * ((actionMenber.criD + criDUp) / 100f));
                        }


                        byte damageTimes = 1;
                        if (so.comboMax != 0)
                        {
                            for (byte k = 0; k < so.comboMax; k++)
                            {
                                int ranCombo = Random.Range(0, 100);
                                if (ranCombo < so.comboRate)
                                {
                                    damageTimes++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        StartCoroutine(TakeDamage(teamID, adventureTeamList[teamID].fightRound, actionMenber, targetMenber[j], sp, damageWithElement, damageTimes, 1f + skillLevelUp, isCri));


                    }
                    else//未命中
                    {
                        Miss(teamID, adventureTeamList[teamID].fightRound, actionMenber, targetMenber[j]);
                    }


                }

                if (sp.FlagDebuff)
                {
                    if (sp.Dizzy != 0)
                    {
                        int hitRate = System.Math.Max(0, (int)(sp.Dizzy * (1f + skillLevelUp)) - targetMenber[j].dizzyRes);
                        int ranHit = Random.Range(0, 100);
                        if (ranHit < hitRate)
                        {
                            bool buffExist = false;
                            for (int k = 0; k < targetMenber[j].buff.Count; k++)
                            {
                                if (targetMenber[j].buff[k].type == FightBuffType.Dizzy)
                                {
                                    targetMenber[j].buff[k].round = (byte)sp.DizzyValue;
                                    buffExist = true;
                                    break;
                                }
                            }
                            if (!buffExist)
                            {
                                targetMenber[j].buff.Add(new FightBuff(FightBuffType.Dizzy, 0, (byte)sp.DizzyValue));
                            }

                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "眩晕了");
                        }
                        else
                        {
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "眩晕效果未生效");
                        }
                    }
                    if (sp.Confusion != 0)
                    {
                        int hitRate = System.Math.Max(0, (int)(sp.Confusion * (1f + skillLevelUp)) - targetMenber[j].confusionRes);
                        int ranHit = Random.Range(0, 100);
                        if (ranHit < hitRate)
                        {
                            bool buffExist = false;
                            for (int k = 0; k < targetMenber[j].buff.Count; k++)
                            {
                                if (targetMenber[j].buff[k].type == FightBuffType.Confusion)
                                {
                                    targetMenber[j].buff[k].round = (byte)sp.ConfusionValue;
                                    buffExist = true;
                                    break;
                                }
                            }
                            if (!buffExist)
                            {
                                targetMenber[j].buff.Add(new FightBuff(FightBuffType.Confusion, 0, (byte)sp.DizzyValue));
                            }

                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "混乱了");
                        }
                        else
                        {
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "混乱效果未生效");
                        }
                    }
                    if (sp.Sleep != 0)
                    {
                        int hitRate = System.Math.Max(0, (int)(sp.Sleep * (1f + skillLevelUp)) - targetMenber[j].sleepRes);
                        int ranHit = Random.Range(0, 100);
                        if (ranHit < hitRate)
                        {
                            bool buffExist = false;
                            for (int k = 0; k < targetMenber[j].buff.Count; k++)
                            {
                                if (targetMenber[j].buff[k].type == FightBuffType.Sleep)
                                {
                                    targetMenber[j].buff[k].round = (byte)sp.ConfusionValue;
                                    buffExist = true;
                                    break;
                                }
                            }
                            if (!buffExist)
                            {
                                targetMenber[j].buff.Add(new FightBuff(FightBuffType.Sleep, 0, (byte)sp.SleepValue));
                            }

                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "睡眠了");
                        }
                        else
                        {
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "睡眠效果未生效");
                        }
                    }
                    if (sp.Poison != 0)
                    {
                        int hitRate = System.Math.Max(0, (int)(sp.Poison * (1f + skillLevelUp)) - targetMenber[j].poisonRes);
                        int ranHit = Random.Range(0, 100);
                        if (ranHit < hitRate)
                        {
                            bool buffExist = false;
                            for (int k = 0; k < targetMenber[j].buff.Count; k++)
                            {
                                if (targetMenber[j].buff[k].type == FightBuffType.Poison)
                                {
                                    targetMenber[j].buff[k].round = (byte)sp.ConfusionValue;
                                    buffExist = true;
                                    break;
                                }
                            }
                            if (!buffExist)
                            {
                                targetMenber[j].buff.Add(new FightBuff(FightBuffType.Poison, 0, (byte)sp.PoisonValue));
                            }

                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "中毒了");
                        }
                        else
                        {
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "中毒效果未生效");
                        }
                    }
                    AdventureMainPanel.Instance.UpdateSceneRoleBuffSingle(teamID, targetMenber[j]);
                }

                if (sp.Cure != 0)
                {
                    int cure = (int)(targetMenber[j].hp * (sp.Cure / 100f));
                    cure = (int)(cure * (1f + skillLevelUp));

                    byte damageTimes = 1;
                    if (so.comboMax != 0)
                    {
                        for (byte k = 0; k < so.comboMax; k++)
                        {
                            int ranCombo = Random.Range(0, 100);
                            if (ranCombo < so.comboRate)
                            {
                                damageTimes++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    StartCoroutine(TakeCure(teamID, adventureTeamList[teamID].fightRound, actionMenber, targetMenber[j], sp, Attribute.Hp, cure, damageTimes));


                }

                if (sp.FlagBuff)
                {
                    if (sp.UpAtk != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpAtk)
                            {
                                targetMenber[j].buff[k].round = 2;
                                buffExist = true;
                                break;
                            }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpAtk, (byte)(sp.UpAtk * (1f + skillLevelUp)), 2));
                            targetMenber[j].atkMin += (short)(targetMenber[j].atkMin * (sp.UpAtk * (1f + skillLevelUp) / 100f));
                            targetMenber[j].atkMax += (short)(targetMenber[j].atkMax * (sp.UpAtk * (1f + skillLevelUp) / 100f));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "物理攻击提升了");
                        }
                    }
                    if (sp.UpMAtk != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpMAtk) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpMAtk, (byte)(sp.UpMAtk * (1f + skillLevelUp)), 2));
                            targetMenber[j].mAtkMin += (short)(targetMenber[j].mAtkMin * (sp.UpMAtk * (1f + skillLevelUp) / 100f));
                            targetMenber[j].mAtkMax += (short)(targetMenber[j].mAtkMax * (sp.UpMAtk * (1f + skillLevelUp) / 100f));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "魔法攻击提升了");
                        }
                    }
                    if (sp.UpDef != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpDef) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDef, (byte)(sp.UpDef * (1f + skillLevelUp)), 2));
                            targetMenber[j].def += (short)(targetMenber[j].def * (sp.UpDef * (1f + skillLevelUp) / 100f));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "物理防御提升了");
                        }
                    }
                    if (sp.UpMDef != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpMDef) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpMDef, (byte)(sp.UpMDef * (1f + skillLevelUp)), 2));
                            targetMenber[j].mDef += (short)(targetMenber[j].mDef * (sp.UpMDef * (1f + skillLevelUp) / 100f));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "魔法防御提升了");
                        }
                    }
                    if (sp.UpHit != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpHit) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpHit, (byte)(sp.UpHit * (1f + skillLevelUp)), 2));
                            targetMenber[j].hit += (short)(targetMenber[j].hit * (sp.UpHit * (1f + skillLevelUp) / 100f));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "命中提升了");
                        }
                    }
                    if (sp.UpDod != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpDod) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDod, (byte)(sp.UpDod * (1f + skillLevelUp)), 2));
                            targetMenber[j].dod += (short)(targetMenber[j].dod * (sp.UpDod * (1f + skillLevelUp) / 100f));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "闪避提升了");
                        }
                    }
                    if (sp.UpCriD != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpCriD) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpCriD, (byte)(sp.UpCriD * (1f + skillLevelUp)), 2));
                            targetMenber[j].criD += (short)(targetMenber[j].criD * (sp.UpCriD * (1f + skillLevelUp) / 100f));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暴击伤害提升了");
                        }
                    }

                    if (sp.UpWindDam != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpWindDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWindDam, (byte)(sp.UpWindDam * (1f + skillLevelUp)), 2));
                            targetMenber[j].windDam += (short)(sp.UpWindDam * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "风系伤害提升了");
                        }
                    }
                    if (sp.UpFireDam != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpFireDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpFireDam, (byte)(sp.UpFireDam * (1f + skillLevelUp)), 2));
                            targetMenber[j].fireDam += (short)(sp.UpFireDam * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "火系伤害提升了");
                        }
                    }
                    if (sp.UpWaterDam != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpWaterDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWaterDam, (byte)(sp.UpWaterDam * (1f + skillLevelUp)), 2));
                            targetMenber[j].waterDam += (short)(sp.UpWaterDam * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "水系伤害提升了");
                        }
                    }
                    if (sp.UpGroundDam != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpGroundDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpGroundDam, (byte)(sp.UpGroundDam * (1f + skillLevelUp)), 2));
                            targetMenber[j].groundDam += (short)(sp.UpGroundDam * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "地系伤害提升了");
                        }
                    }
                    if (sp.UpLightDam != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpLightDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpLightDam, (byte)(sp.UpLightDam * (1f + skillLevelUp)), 2));
                            targetMenber[j].lightDam += (short)(sp.UpLightDam * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "光系伤害提升了");
                        }
                    }
                    if (sp.UpDarkDam != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpDarkDam) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDarkDam, (byte)(sp.UpDarkDam * (1f + skillLevelUp)), 2));
                            targetMenber[j].darkDam += (short)(sp.UpDarkDam * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暗系伤害提升了");
                        }
                    }

                    if (sp.UpWindRes != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpWindRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWindRes, (byte)(sp.UpWindRes * (1f + skillLevelUp)), 2));
                            targetMenber[j].windRes += (short)(sp.UpWindRes * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "风系抗性提升了");
                        }
                    }
                    if (sp.UpFireRes != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpFireRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpFireRes, (byte)(sp.UpFireRes * (1f + skillLevelUp)), 2));
                            targetMenber[j].fireRes += (short)(sp.UpFireRes * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "火系抗性提升了");
                        }
                    }
                    if (sp.UpWaterRes != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpWaterRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWaterRes, (byte)(sp.UpWaterRes * (1f + skillLevelUp)), 2));
                            targetMenber[j].waterRes += (short)(sp.UpWaterRes * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "水系抗性提升了");
                        }
                    }
                    if (sp.UpGroundRes != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpGroundRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpGroundRes, (byte)(sp.UpGroundRes * (1f + skillLevelUp)), 2));
                            targetMenber[j].groundRes += (short)(sp.UpGroundRes * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "地系抗性提升了");
                        }
                    }
                    if (sp.UpLightRes != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpLightRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpLightRes, (byte)(sp.UpLightRes * (1f + skillLevelUp)), 2));
                            targetMenber[j].lightRes += (short)(sp.UpLightRes * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "光系抗性提升了");
                        }
                    }
                    if (sp.UpDarkRes != 0)
                    {
                        bool buffExist = false;
                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                        {
                            if (targetMenber[j].buff[k].type == FightBuffType.UpDarkRes) { targetMenber[j].buff[k].round = 2; buffExist = true; break; }
                        }
                        if (!buffExist)
                        {
                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDarkRes, (byte)(sp.UpDarkRes * (1f + skillLevelUp)), 2));
                            targetMenber[j].darkRes += (short)(sp.UpDarkRes * (1f + skillLevelUp));
                            AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暗系抗性提升了");
                        }
                    }

                    AdventureMainPanel.Instance.UpdateSceneRoleBuffSingle(teamID, targetMenber[j]);
                }

                //TODO:[待优化设计]夺金设计概率
                if (so.gold != 0 && targetMenber[j].side == 1)
                {
                    int ranGold = Random.Range(0, 100);
                    if (ranGold < 30)
                    {
                        short gold = (short)(DataManager.mMonsterDict[targetMenber[j].objectID].GoldDrop * (so.gold / 100f));
                        adventureTeamList[teamID].getGold += gold;
                        AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(actionMenber) + "从" + OutputNameWithColor(targetMenber[j]) + "夺得" + gold + "金币");
                    }
                }
            }
        }
    }

    void NormalAttack(byte teamID,FightMenberObject actionMenber, List<FightMenberObject> fightMenberObjects)
    {
        StartCoroutine(Attack(teamID, actionMenber, null));
        //选取目标
        List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber, null);
        if (targetMenber.Count > 0)
        {
            if (IsHit(actionMenber, targetMenber[0]))
            {
                int damageMin = System.Math.Max(0, actionMenber.atkMin - targetMenber[0].def);
                int damageMax = System.Math.Max(0, actionMenber.atkMax - targetMenber[0].def);
                int damage = Random.Range(damageMin, damageMax + 1);
                //斩味修正伤害
                damage = (int)(damage * GetSharpnessModifyDamage(GetSharpnessLevel(actionMenber)));
                bool isCri = IsCri(actionMenber);
                if (isCri)
                {
                    byte criDUp = 0;
                    if (actionMenber.haloStatus)
                    {
                        criDUp = DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].CriDUp;
                    }

                    damage = (int)(damage * ((actionMenber.criD + criDUp) / 100f));
                }

                StartCoroutine(TakeDamage(teamID, adventureTeamList[teamID].fightRound, actionMenber, targetMenber[0], null, damage, 1, 1f, isCri));

            }
            else//未命中
            {
                Miss(teamID, adventureTeamList[teamID].fightRound, actionMenber, targetMenber[0]);
            }

        }
    }

    void Miss(byte teamID, int round, FightMenberObject actionMenber, FightMenberObject targetMenber)
    {
        AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, "闪避", false);
        AdventureTeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber) + "避开了" + OutputNameWithColor(actionMenber) + "的攻击");
    }

    IEnumerator Attack(byte teamID, FightMenberObject actionMenber, SkillPrototype sp)
    {
        if (actionMenber.side == 0 && sp != null)
        {
            if (sp.Element.Contains(0)) { heroDic[actionMenber.objectID].countUseNone++; }
            if (sp.Element.Contains(1)) { heroDic[actionMenber.objectID].countUseWind++; }
            if (sp.Element.Contains(2)) { heroDic[actionMenber.objectID].countUseFire++; }
            if (sp.Element.Contains(3)) { heroDic[actionMenber.objectID].countUseWater++; }
            if (sp.Element.Contains(4)) { heroDic[actionMenber.objectID].countUseGround++; }
            if (sp.Element.Contains(5)) { heroDic[actionMenber.objectID].countUseLight++; }
            if (sp.Element.Contains(6)) { heroDic[actionMenber.objectID].countUseDark++; }

            HeroSkillGetExp(actionMenber.objectID, sp.ID, 30);
        }

       
       

        if (sp != null)
        {
            //消耗锋利度(技能)
            if (actionMenber.side == 0 && sp.FlagDamage)
            {
                if (Random.Range(0, 100) < 50)
                {
                    switch (sp.Rank)
                    {
                        case 1: actionMenber.sharpnessNow -= 2; break;
                        case 2: actionMenber.sharpnessNow -= 2; break;
                        case 3: actionMenber.sharpnessNow -= 3; break;
                        case 4: actionMenber.sharpnessNow -= 5; break;
                    }

                    if (actionMenber.sharpnessNow < 0)
                    {
                        actionMenber.sharpnessNow = 0;
                    }
                }
                AdventureMainPanel.Instance.UpdateSceneRoleSharpnessSingle(teamID, actionMenber);
            }
           

            if (sp.Rank == 4)
            {
                AdventureMainPanel.Instance.ShowTalk(teamID, actionMenber.side, actionMenber.sideIndex, "绝技 <color=#698BFF>" + sp.Name + "</color>");
            }

            if (AdventureMainPanel.Instance.nowCheckingTeamID == teamID)
            {
                AudioControl.Instance.PlaySound(sp.Sound);
            }
          

            AdventureDungeonElementChange(teamID, sp.Element, (byte)(sp.Rank * 10));
        }
        else
        {
            //消耗锋利度(普通攻击)
            if (actionMenber.side == 0)
            {
                if (Random.Range(0, 100) < 70)
                {
                    actionMenber.sharpnessNow -= 5;
                    if (actionMenber.sharpnessNow < 0)
                    {
                        actionMenber.sharpnessNow = 0;
                    }
                    AdventureMainPanel.Instance.UpdateSceneRoleSharpnessSingle(teamID, actionMenber);
                }
            }

            if (AdventureMainPanel.Instance.nowCheckingTeamID == teamID)
            {
                AudioControl.Instance.PlaySound("attack_weapon_7");
            }
           
           
        }

        AdventureMainPanel.Instance.SetAnim(teamID, actionMenber.side, actionMenber.sideIndex, sp != null ? sp.ActionAnim : AnimStatus.Attack);
        yield return new WaitForSeconds(0.5f);
        // AdventureMainPanel.Instance.SetAnim(teamID, actionMenber[i].side, actionMenber[i].sideIndex, AnimStatus.Magic);
    }

    IEnumerator TakeCure(byte teamID, int round, FightMenberObject actionMenber, FightMenberObject targetMenber, SkillPrototype sp, Attribute attribute, int cure, byte times)
    {
        float size = 1f;
        //Debug.Log("TakeCure() teamID=" + teamID);
        string effectName = "impact_6";
        string color = "6EFB6F";
        if (sp != null)
        {
            effectName = sp.Effect;


        }
        else
        {

            if (attribute == Attribute.Hp)
            {
                effectName = "impact_6";
            }
            else if (attribute == Attribute.Mp)
            {
                effectName = "impact_5";
            }
        }

        if (attribute == Attribute.Hp)
        {
            color = "6EFB6F";
            targetMenber.hpNow += cure;
            if (targetMenber.hpNow > targetMenber.hp)
            {
                targetMenber.hpNow = targetMenber.hp;
            }

        }
        else if (attribute == Attribute.Mp)
        {
            color = "6FAAFA";
            targetMenber.mpNow += cure;
            if (targetMenber.mpNow > targetMenber.mp)
            {
                targetMenber.mpNow = targetMenber.mp;
            }

        }


        int cureTotal = 0;
        string cureTotalStr = "";
        for (byte i = 0; i < times; i++)
        {
            int cureSingle = (i == 0 ? cure : (int)(Random.Range(0.5f, 0.9f) * cure));

            cureTotal += cureSingle;


            AdventureMainPanel.Instance.ShowEffect(teamID, targetMenber.side, targetMenber.sideIndex, effectName, size);
            AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, "<b><color=#" + color + ">+" + cureSingle + "</color></b>", false);
            yield return new WaitForSeconds(0.5f);
            cureTotalStr += cureSingle + "/";
        }
        cureTotalStr = cureTotalStr.TrimEnd('/');



        if (actionMenber != null)
        {
            if (actionMenber != targetMenber)
            {
                int ran = Random.Range(0, 100);
                if (ran < 30)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "谢谢你");
                }
                else if (ran >= 30 && ran < 60)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "好奶!");
                }
                else if (ran >= 60 && ran < 90)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "icon_talk_love");
                }
            }
            else
            {
                int ran = Random.Range(0, 100);
                if (ran < 60)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "来喝奶!");
                }
                else if (ran >= 60 && ran < 90)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "我用治疗术啦");
                }
            }

        }

        AdventureMainPanel.Instance.UpdateSceneRoleHpMpSingle(teamID, targetMenber);
        AdventureMainPanel.Instance.UpdateHeroHpMpSingle(teamID, targetMenber);
        AdventureTeamLogAdd(teamID, "[回合" + round + "]" + (sp != null ? OutputNameWithColor(actionMenber) + "用<color=#4FA3C1>" + sp.Name + "</color>" : "") + "恢复" + OutputNameWithColor(targetMenber) + "<color=#86E3C9>" + cureTotalStr + "</color>点" + (attribute == Attribute.Hp ? "体力" : "魔力"));

    }

    IEnumerator TakeDamage(byte teamID, int round, FightMenberObject actionMenber, FightMenberObject targetMenber, SkillPrototype sp, int damage, byte times, float size,bool isCri)
    {
        string effectName;
        // float size = 1f;
        if (sp != null)
        {
            effectName = sp.Effect;
        }
        else
        {
            effectName = "sword_1";
        }
        if (AdventureMainPanel.Instance.nowCheckingTeamID == teamID)
        {
            AudioControl.Instance.PlaySound("damage01");
        }
       
        //Debug.Log("damage01");
        List<int> damageList = new List<int> { };
        int damageTotal = 0;
        string damageTotalStr = "";
        for (byte i = 0; i < times; i++)
        {
            int damageSingle = (i == 0 ? damage : (int)(Random.Range(0.5f, 0.9f) * damage));
            damageList.Add(damageSingle);
            damageTotal += damageSingle;


            AdventureMainPanel.Instance.ShowEffect(teamID, targetMenber.side, targetMenber.sideIndex, effectName, size);
            if (damage > 0)
            {
                if (isCri)
                {
                    AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, "<color=#FF9D13>暴击</color>", true);

                }

                //todo:测试看效果，实际上应该只有1条
                AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, "<b><color=#F86A43>-" + damageSingle + "</color></b>", true);
                yield return new WaitForSeconds(0.2f);
                AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, "<b><color=#F86A43>-" + damageSingle + "</color></b>", true);
                yield return new WaitForSeconds(0.2f);
                AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, "<b><color=#F86A43>-" + damageSingle + "</color></b>", true);
            }
            else
            {
                AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex,"格挡",false);
            }
            AdventureMainPanel.Instance.SetAnim(teamID, targetMenber.side, targetMenber.sideIndex, AnimStatus.Hit);
            yield return new WaitForSeconds(0.5f);
            damageTotalStr += damageSingle + "/";
        }
        damageTotalStr = damageTotalStr.TrimEnd('/');




        if (damageTotal >= (int)(targetMenber.hp * 0.2f))
        {

            if (targetMenber.side == 0)
            {
                int ran = Random.Range(0, 100);
                if (ran < 30)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "好疼!");
                }
                else if (ran >= 30 && ran < 60)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "icon_talk_sad");
                }

            }
            else if (targetMenber.side == 1)
            {
                int ran = Random.Range(0, 100);
                if (ran < 50)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "T﹏T");
                }

            }
        }

        //造成伤害
      
        if (damageTotal > targetMenber.hpNow)
        {
            damageTotal = targetMenber.hpNow;
        }
        targetMenber.hpNow -= damageTotal;

        if (targetMenber.hpNow <= (int)(targetMenber.hp * 0.2f))
        {
            if (targetMenber.side == 0)
            {
                int ran = Random.Range(0, 100);
                if (ran < 30)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "危险了啊");
                }
                else if (ran >= 30 && ran < 60)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "我快不行了");
                }

            }
            else if (targetMenber.side == 1)
            {
                int ran = Random.Range(0, 100);
                if (ran < 20)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "@%#^*!");
                }
                else if (ran >= 20 && ran < 60)
                {
                    AdventureMainPanel.Instance.ShowTalk(teamID, targetMenber.side, targetMenber.sideIndex, "X﹏X");
                }

            }
        }

        if (actionMenber != null)
        {
            if (actionMenber.side==0)
            {
                adventureTeamList[teamID].heroDamageList[actionMenber.sideIndex] += damageTotal;
                AdventureMainPanel.Instance.UpdateDamageData(teamID, actionMenber.sideIndex);
            }
        }

        if (actionMenber!=null&& targetMenber!=null)
        {
            //halo吸血
            if (actionMenber.haloStatus)
            {
                if (DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].SuckBlood != 0)
                {
                    TakeCure(teamID, round, actionMenber, null, null, Attribute.Hp, (int)(damageTotal * (DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].SuckBlood / 100f)), 1);
                }
            }
            //halo反伤
            if (targetMenber.haloStatus)
            {
                if (DataManager.mHaloDict[heroDic[targetMenber.objectID].halo].DamageReflection != 0)
                {
                    StartCoroutine(TakeDamage(teamID, round, null, actionMenber, null, (int)(damageTotal * (DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].DamageReflection / 100f)), 1, 1f,false));
                }
            }
        }    
       


        if (targetMenber.hpNow <= 0)
        {
            //halo自爆
            if (targetMenber.haloStatus)
            {
                if (DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].Detonate != 0)
                {
                    for (int i = 0; i < fightMenberObjectSS[teamID].Count; i++)
                    {
                        if (fightMenberObjectSS[teamID][i].side != targetMenber.side && fightMenberObjectSS[teamID][i].hpNow > 0)
                        {
                            Debug.Log("actionMenber.objectID=" + actionMenber.objectID);
                            Debug.Log("heroDic[actionMenber.objectID].halo=" + heroDic[actionMenber.objectID].halo);
                            Debug.Log("i=" + i);
                            StartCoroutine(TakeDamage(teamID, round, null, fightMenberObjectSS[teamID][i], null, (int)(targetMenber.hp * (DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].Detonate / 100f)), 1, 1f,false));
                        }
                    }
                }
            }

            //targetMenber.hpNow = 0;
            AdventureMainPanel.Instance.UpdateSceneRoleApTextSingle(teamID, targetMenber);
        }
        AdventureMainPanel.Instance.UpdateSceneRoleHpMpSingle(teamID, targetMenber);
        AdventureMainPanel.Instance.UpdateHeroHpMpSingle(teamID, targetMenber);
        if (actionMenber != null)
        {
            AdventureTeamLogAdd(teamID, "[回合" + round + "]" + OutputNameWithColor(actionMenber) + (sp != null ? "用<color=#4FA3C1>" + sp.Name + "</color>" : "普通攻击") + "对" + OutputNameWithColor(targetMenber) + "造成<color=#EC838F>" + damageTotalStr + "</color>点伤害");
        }
        else
        {
            AdventureTeamLogAdd(teamID, "[回合" + round + "]"  + OutputNameWithColor(targetMenber) + "受到了<color=#EC838F>" + damageTotalStr + "</color>点伤害");
        }

        if (targetMenber.hpNow == 0)
        {
            AdventureMainPanel.Instance.SetAnim(teamID, targetMenber.side, targetMenber.sideIndex, AnimStatus.Death);
            AdventureTeamLogAdd(teamID, "[回合" + round + "]" + OutputNameWithColor(targetMenber) + "被打倒了！");

            if (targetMenber.side == 0)
            {
                heroDic[targetMenber.objectID].countDeath++;
                targetMenber.haloStatus = false;
                AdventureMainPanel.Instance.UpdateSceneRoleHaloSingle(teamID, targetMenber);
            }
            else if (targetMenber.side == 1)
            {
                heroDic[actionMenber.objectID].countKill++;
            }
        }
    }

    void AdventureDungeonElementChange(byte teamID, List<int> addType, byte addValue)
    {
        short MinPoint = 80;

        List<byte> oldEPList = new List<byte> { adventureTeamList[teamID].dungeonEPWind, adventureTeamList[teamID].dungeonEPFire, adventureTeamList[teamID].dungeonEPWater, adventureTeamList[teamID].dungeonEPGround, adventureTeamList[teamID].dungeonEPLight, adventureTeamList[teamID].dungeonEPDark };


        if (addType.Contains(1)) { adventureTeamList[teamID].dungeonEVWind += addValue; adventureTeamList[teamID].dungeonEVGround -= (byte)(addValue / 2f); }
        if (addType.Contains(2)) { adventureTeamList[teamID].dungeonEVFire += addValue; adventureTeamList[teamID].dungeonEVWater -= (byte)(addValue / 2f); }
        if (addType.Contains(3)) { adventureTeamList[teamID].dungeonEVWater += addValue; adventureTeamList[teamID].dungeonEVFire -= (byte)(addValue / 2f); }
        if (addType.Contains(4)) { adventureTeamList[teamID].dungeonEVGround += addValue; adventureTeamList[teamID].dungeonEVWind -= (byte)(addValue / 2f); }
        if (addType.Contains(5)) { adventureTeamList[teamID].dungeonEVLight += addValue; adventureTeamList[teamID].dungeonEVDark -= (byte)(addValue / 2f); }
        if (addType.Contains(6)) { adventureTeamList[teamID].dungeonEVDark += addValue; adventureTeamList[teamID].dungeonEVLight -= (byte)(addValue / 2f); }

        short dungeonEVTotal = (short)(System.Math.Max(adventureTeamList[teamID].dungeonEVWind, (short)0) +
            System.Math.Max(adventureTeamList[teamID].dungeonEVFire, (short)0) +
            System.Math.Max(adventureTeamList[teamID].dungeonEVWater, (short)0) +
           System.Math.Max(adventureTeamList[teamID].dungeonEVGround, (short)0) +
           System.Math.Max(adventureTeamList[teamID].dungeonEVLight, (short)0) +
           System.Math.Max(adventureTeamList[teamID].dungeonEVDark, (short)0));


        adventureTeamList[teamID].dungeonEPWind = System.Math.Min((adventureTeamList[teamID].dungeonEVWind >= MinPoint) ? (byte)(((float)adventureTeamList[teamID].dungeonEVWind / dungeonEVTotal) * 5) : (byte)0, (byte)(adventureTeamList[teamID].dungeonEVWind / MinPoint));
        adventureTeamList[teamID].dungeonEPFire = System.Math.Min((adventureTeamList[teamID].dungeonEVFire >= MinPoint) ? (byte)(((float)adventureTeamList[teamID].dungeonEVFire / dungeonEVTotal) * 5) : (byte)0, (byte)(adventureTeamList[teamID].dungeonEVWind / MinPoint));
        adventureTeamList[teamID].dungeonEPWater = System.Math.Min((adventureTeamList[teamID].dungeonEVWater >= MinPoint) ? (byte)(((float)adventureTeamList[teamID].dungeonEVWater / dungeonEVTotal) * 5) : (byte)0, (byte)(adventureTeamList[teamID].dungeonEVWind / MinPoint));
        adventureTeamList[teamID].dungeonEPGround = System.Math.Min((adventureTeamList[teamID].dungeonEVGround >= MinPoint) ? (byte)(((float)adventureTeamList[teamID].dungeonEVGround / dungeonEVTotal) * 5) : (byte)0, (byte)(adventureTeamList[teamID].dungeonEVWind / MinPoint));
        adventureTeamList[teamID].dungeonEPLight = System.Math.Min((adventureTeamList[teamID].dungeonEVLight >= MinPoint) ? (byte)(((float)adventureTeamList[teamID].dungeonEVLight / dungeonEVTotal) * 5) : (byte)0, (byte)(adventureTeamList[teamID].dungeonEVWind / MinPoint));
        adventureTeamList[teamID].dungeonEPDark = System.Math.Min((adventureTeamList[teamID].dungeonEVDark >= MinPoint) ? (byte)(((float)adventureTeamList[teamID].dungeonEVDark / dungeonEVTotal) * 5) : (byte)0, (byte)(adventureTeamList[teamID].dungeonEVWind / MinPoint));

        //Debug.Log("风"+ adventureTeamList[teamID].dungeonEVWind+ " 火" + adventureTeamList[teamID].dungeonEVFire + " 水" + adventureTeamList[teamID].dungeonEVWater + " 地" + adventureTeamList[teamID].dungeonEVGround + " 光" + adventureTeamList[teamID].dungeonEVLight + " 暗" + adventureTeamList[teamID].dungeonEVDark);
        //Debug.Log("风" + adventureTeamList[teamID].dungeonEPWind + " 火" + adventureTeamList[teamID].dungeonEPFire + " 水" + adventureTeamList[teamID].dungeonEPWater + " 地" + adventureTeamList[teamID].dungeonEPGround + " 光" + adventureTeamList[teamID].dungeonEPLight + " 暗" + adventureTeamList[teamID].dungeonEPDark);

        if (oldEPList[0] != adventureTeamList[teamID].dungeonEPWind ||
           oldEPList[1] != adventureTeamList[teamID].dungeonEPFire ||
           oldEPList[2] != adventureTeamList[teamID].dungeonEPWater ||
           oldEPList[3] != adventureTeamList[teamID].dungeonEPGround ||
           oldEPList[4] != adventureTeamList[teamID].dungeonEPLight ||
           oldEPList[5] != adventureTeamList[teamID].dungeonEPDark)
        {
            Debug.Log("更新地图元素");
            AdventureMainPanel.Instance.UpdateElementPoint(teamID);

            //检测光环是否触发
            AdventureDungeonCheckHalo(teamID);
        }

    }

    void AdventureDungeonCheckHalo(byte teamID)
    {
        for (int i = 0; i < fightMenberObjectSS[teamID].Count; i++)
        {
            if (fightMenberObjectSS[teamID][i].side == 0 && fightMenberObjectSS[teamID][i].hpNow > 0)
            {
                Debug.Log(" heroDic[fightMenberObjectSS[teamID][i].objectID].halo=" + heroDic[fightMenberObjectSS[teamID][i].objectID].halo);
                short haloID = heroDic[fightMenberObjectSS[teamID][i].objectID].halo;
                if (haloID != -1)
                {
                    bool can = true;
                    for (int j = 0; j < DataManager.mHaloDict[haloID].NeedElementType.Count; j++)
                    {
                        Debug.Log("DataManager.mHaloDict[haloID].NeedElementType[j]=" + DataManager.mHaloDict[haloID].NeedElementType[j]);
                        Debug.Log("DataManager.mHaloDict[haloID].NeedElementPoint[j]=" + DataManager.mHaloDict[haloID].NeedElementPoint[j]);
                        switch (DataManager.mHaloDict[haloID].NeedElementType[j])
                        {
                            case Element.Wind: if (adventureTeamList[teamID].dungeonEPWind < DataManager.mHaloDict[haloID].NeedElementPoint[j]) { can = false; } break;
                            case Element.Fire: if (adventureTeamList[teamID].dungeonEPFire < DataManager.mHaloDict[haloID].NeedElementPoint[j]) { can = false; } break;
                            case Element.Water: if (adventureTeamList[teamID].dungeonEPWater < DataManager.mHaloDict[haloID].NeedElementPoint[j]) { can = false; } break;
                            case Element.Ground: if (adventureTeamList[teamID].dungeonEPGround < DataManager.mHaloDict[haloID].NeedElementPoint[j]) { can = false; } break;
                            case Element.Light: if (adventureTeamList[teamID].dungeonEPLight < DataManager.mHaloDict[haloID].NeedElementPoint[j]) { can = false; } break;
                            case Element.Dark: if (adventureTeamList[teamID].dungeonEPDark < DataManager.mHaloDict[haloID].NeedElementPoint[j]) { can = false; } break;
                        }
                    }
                    Debug.Log("can=" + can);
                    Debug.Log(DataManager.mHaloDict[haloID].NeedHpNow);
                    if (DataManager.mHaloDict[haloID].NeedHpNow != -1)
                    {
                        if (fightMenberObjectSS[teamID][i].hpNow >= DataManager.mHaloDict[haloID].NeedHpNow)
                        {
                            can = false;
                        }
                    }
                    Debug.Log(fightMenberObjectSS[teamID][i].name + " fightMenberObjectSS[teamID][i].haloStatus=" + fightMenberObjectSS[teamID][i].haloStatus);
                    fightMenberObjectSS[teamID][i].haloStatus = can;
                    AdventureMainPanel.Instance.UpdateSceneRoleHaloSingle(teamID, fightMenberObjectSS[teamID][i]);
                }


            }
        }
    }

    int CheckFightOver(List<FightMenberObject> fightMenberObjects)
    {
        int Side0SurviveNum = 0;
        int Side1SurviveNum = 0;
        for (int i = 0; i < fightMenberObjects.Count; i++)
        {
            if (fightMenberObjects[i].hpNow > 0)
            {
                if (fightMenberObjects[i].side == 0)
                {
                    Side0SurviveNum++;
                }
                else if (fightMenberObjects[i].side == 1)
                {
                    Side1SurviveNum++;
                }
            }
        }

        if (Side0SurviveNum > 0 && Side1SurviveNum == 0)
        {
            return 0;
        }
        else if (Side1SurviveNum > 0 && Side0SurviveNum == 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    List<FightMenberObject> GetTargetManbers(List<FightMenberObject> fightMenberObjects, FightMenberObject actionMenber, SkillPrototype sp)
    {
        //string str= "打乱前：";
        //for (int i=0; i < fightMenberObjects.Count; i++)
        //{
        //    str += fightMenberObjects[i].name + " ";
        //}


        //TODO:混乱选取目标情况待写，策略选取待写（AI）
        List<FightMenberObject> temp_fightMenberObjects = GetRandomList<FightMenberObject>(fightMenberObjects);
        //str += "   打乱后：";
        //for (int i = 0; i < temp_fightMenberObjects.Count; i++)
        //{
        //    str += temp_fightMenberObjects[i].name + " ";
        //}
        //Debug.Log(str);

        List<FightMenberObject> targetMenber = new List<FightMenberObject>();
        int maxCount;
        if (sp != null)
        {
            maxCount = sp.Max;
            if (sp.FlagDamage || sp.FlagDebuff)
            {
                for (int j = 0; j < temp_fightMenberObjects.Count; j++)
                {
                    if (temp_fightMenberObjects[j].side != actionMenber.side && temp_fightMenberObjects[j].hpNow > 0 && maxCount > 0)
                    {
                        targetMenber.Add(temp_fightMenberObjects[j]);
                        maxCount--;

                    }
                }
            }
            else if (sp.Cure != 0 || sp.FlagBuff)
            {
                for (int j = 0; j < temp_fightMenberObjects.Count; j++)
                {
                    if (temp_fightMenberObjects[j].side == actionMenber.side && temp_fightMenberObjects[j].hpNow > 0 && maxCount > 0)
                    {
                        targetMenber.Add(temp_fightMenberObjects[j]);
                        maxCount--;

                    }
                }
            }

        }
        else
        {
            for (int j = 0; j < temp_fightMenberObjects.Count; j++)
            {
                if (temp_fightMenberObjects[j].side != actionMenber.side && temp_fightMenberObjects[j].hpNow > 0)
                {
                    targetMenber.Add(temp_fightMenberObjects[j]);
                    break;
                }
            }
        }
        //string str = "选中的目标：";
        //for (int i = 0; i < targetMenber.Count; i++)
        //{
        //    str += targetMenber[i].name + " ";
        //}

        //Debug.Log("targetMenber" + targetMenber.Count+ "  "+ str);
        return targetMenber;
    }

    string OutputNameWithColor(FightMenberObject fightMenberObject)
    {
        if (fightMenberObject.side == 0)
        {
            return "<color=#72FF53>" + fightMenberObject.name + "</color>";
        }
        else
        {
            return "<color=#FF5954>" + fightMenberObject.name + "</color>";
        }
    }
    #endregion

    #region 【方法】日志与执行事件基础
    public void CreateLog(LogType logType, string text, List<int> value)
    {
        //LogType.ProduceDone(地区实例ID,建筑实例ID,物品原型ID)
        logDic.Add(logIndex, new LogObject(logIndex, logType, standardTime, text, value));
        logIndex++;
        //MessagePanel.Instance.AddMessage(logDic[logIndex - 1]);
    }

    //添加执行事件，遍历确定插入位置
    public void ExecuteEventAdd(ExecuteEventObject executeEventObject)
    {
        //Debug.Log("ExecuteEventAdd() executeEventObject=" + executeEventObject.startTime);
        for (int i = 0; i < executeEventList.Count; i++)
        {
            if (executeEventObject.endTime < executeEventList[i].endTime)
            {
               
                executeEventObject.id = eventIndex;
                eventIndex++;
                executeEventList.Insert(i, executeEventObject);
                executeEventDic.Add(executeEventObject.id, executeEventObject);

                if (executeEventObject.type == ExecuteEventType.Build || executeEventObject.type == ExecuteEventType.BuildingUpgrade || executeEventObject.type == ExecuteEventType.TechnologyResearch)
                {
                    ProgressPanel.Instance.AddSingle(executeEventObject.id);
                }
                return;
            }
        }
        executeEventObject.id = eventIndex;
        eventIndex++;
        executeEventList.Add(executeEventObject);
        executeEventDic.Add(executeEventObject.id ,executeEventObject);

        if (executeEventObject.type == ExecuteEventType.Build || executeEventObject.type == ExecuteEventType.BuildingUpgrade || executeEventObject.type == ExecuteEventType.TechnologyResearch)
        {
            ProgressPanel.Instance.AddSingle(executeEventObject.id);
        }
    }
    public void ExecuteEventDelete(int index)
    {
        if (executeEventList[index].type == ExecuteEventType.Build || executeEventList[index].type == ExecuteEventType.BuildingUpgrade || executeEventList[index].type == ExecuteEventType.TechnologyResearch)
        {
            StartCoroutine( ProgressPanel.Instance.DeleteSingle(executeEventList[index].id));
        }
        executeEventDic.Remove(executeEventList[index].id);
        executeEventList.RemoveAt(index);
    }

    #endregion

    #region 【方法】大地图旅人、移动
    public void SetTransferHero(int heroID)
    {
        if (TransferPanel.Instance.selectedHeroID.Contains(heroID))
        {
            TransferPanel.Instance.selectedHeroID.Remove(heroID);
        }
        else
        {
            TransferPanel.Instance.selectedHeroID.Add(heroID);
        }
        TransferPanel.Instance.UpdateHeroNum();
        TransferPanel.Instance.UpdateHeroListSingle(heroID);
    }

    public void SetTransferDistrict(string type, short districtID)
    {
        short old = TransferPanel.Instance.selectedDistrict;
        TransferPanel.Instance.selectedDistrict = districtID;

        if (old != -1)
        {
            TransferPanel.Instance.UpdateDistrictListSingle(type, old);
        }

        TransferPanel.Instance.UpdateDistrictListSingle(type, districtID);
    }


    public void Transfer(short startDistrictID, short endDistrictID, List<int> heroList)
    {
        if (endDistrictID == -1)
        {
            MessagePanel.Instance.AddMessage("未指定目的地");
            return;
        }
        if (heroList.Count == 0)
        {
            MessagePanel.Instance.AddMessage("未选择移动的角色");
            return;
        }

        for (int i = 0; i < heroList.Count; i++)
        {
            districtDic[startDistrictID].heroList.Remove(heroList[i]);
            heroDic[heroList[i]].inDistrict = -1;

            if (heroDic[heroList[i]].workerInBuilding != -1)
            {
                buildingDic[heroDic[heroList[i]].workerInBuilding].heroList.Remove(heroList[i]);
                heroDic[heroList[i]].workerInBuilding = -1;
            }
        }
        AreaMapPanel.Instance.UpdateDistrictSingle(startDistrictID);

        string pic = heroDic[heroList[0]].pic;

        List<int> heroListTemp = new List<int>();
        for (int i = 0; i < heroList.Count; i++)
        {
            heroListTemp.Add(heroList[i]);
        }

        List<int> pathList = DataManager.mAreaPathDict["A-" + startDistrictID + "-" + endDistrictID].Path;
        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, heroListTemp, startDistrictID, endDistrictID, "District", -1,0,"冒险者", heroListTemp.Count));
        AreaMapPanel.Instance.CreateTraveller(travellerIndex, pathList, 0, pic, heroListTemp);
        travellerIndex++;

        TransferPanel.Instance.OnHide();
        if (districtDic[startDistrictID].force != 0 && districtDic[startDistrictID].heroList.Count == 0)
        {
            DistrictMapPanel.Instance.OnHide();
        }
    }

    public void AdventureSend(short startDistrictID, short endDungeonID, List<int> heroList, byte teamID)
    {
        if (endDungeonID == -1)
        {
            MessagePanel.Instance.AddMessage("未指定目的地");
            return;
        }
        if (heroList.Count == 0)
        {
            MessagePanel.Instance.AddMessage("未选择移动的角色");
            return;
        }


        AreaMapPanel.Instance.UpdateDistrictSingle(startDistrictID);

        string pic = heroDic[heroList[0]].pic;
        List<int> heroListTemp = new List<int>();
        for (int i = 0; i < heroList.Count; i++)
        {
            heroListTemp.Add(heroList[i]);
        }

        List<int> pathList = DataManager.mAreaPathDict["B-" + startDistrictID + "-" + endDungeonID].Path;
        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, heroListTemp, startDistrictID, endDungeonID, "Dungeon", teamID,0,"冒险者", heroListTemp.Count));
        AreaMapPanel.Instance.CreateTraveller(travellerIndex, pathList, 0, pic, heroListTemp);
        travellerIndex++;

        adventureTeamList[teamID].state = AdventureState.Sending;
        PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
        TransferPanel.Instance.OnHide();
        if (districtDic[startDistrictID].force != 0 && districtDic[startDistrictID].heroList.Count == 0)
        {
            DistrictMapPanel.Instance.OnHide();
        }
        if (AdventureMainPanel.Instance.isShow && AdventureMainPanel.Instance.nowCheckingTeamID == teamID)
        {
            AdventureMainPanel.Instance.UpdateTeam(teamID);
        }
    }

    public void AdventureBack(byte teamID)
    {
        // Debug.Log("AdventureBack()adventureTeamList[teamID].heroIDList.Count=" + adventureTeamList[teamID].heroIDList.Count); 
        dungeonList[adventureTeamList[teamID].dungeonID].teamList.Remove(teamID);
        AreaMapPanel.Instance.UpdateDungeonSingle(adventureTeamList[teamID].dungeonID);

        

        string pic = heroDic[adventureTeamList[teamID].heroIDList[0]].pic;

        List<int> heroListTemp = new List<int>();
        for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
        {
            heroListTemp.Add(adventureTeamList[teamID].heroIDList[i]);
        }
        List<int> pathList = new List<int>();
        for (int i = DataManager.mAreaPathDict["B-" + adventureTeamList[teamID].districtID + "-" + adventureTeamList[teamID].dungeonID].Path.Count - 1; i >= 0; i--)
        {
            pathList.Add(DataManager.mAreaPathDict["B-" + adventureTeamList[teamID].districtID + "-" + adventureTeamList[teamID].dungeonID].Path[i]);
        }
        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, heroListTemp, adventureTeamList[teamID].dungeonID, adventureTeamList[teamID].districtID, "District", teamID,0,"冒险者", heroListTemp.Count));
        AreaMapPanel.Instance.CreateTraveller(travellerIndex, pathList, 0, pic, heroListTemp);
        travellerIndex++;

        adventureTeamList[teamID].state = AdventureState.Backing;
        PlayMainPanel.Instance.UpdateAdventureSingle(teamID);

        if (dungeonList[adventureTeamList[teamID].dungeonID].teamList.Count > 0)
        {
            AreaMapPanel.Instance.UpdateDungeonInfoBlock(adventureTeamList[teamID].dungeonID);
        }
        else
        {
            AreaMapPanel.Instance.HideDungeonInfoBlock();
        }
      



        if (AdventureMainPanel.Instance.isShow && AdventureMainPanel.Instance.nowCheckingTeamID == teamID)
        {
            AdventureMainPanel.Instance.UpdateTeam(teamID);
        }
    }

    public void TransferDone(int travellerID)
    {
        

        Debug.Log("TransferDone() travellerDic[travellerID].endDistrictOrDungeonID=" + travellerDic[travellerID].endDistrictOrDungeonID);
        for (int i = 0; i < travellerDic[travellerID].heroList.Count; i++)
        {
            districtDic[travellerDic[travellerID].endDistrictOrDungeonID].heroList.Add(travellerDic[travellerID].heroList[i]);
            heroDic[travellerDic[travellerID].heroList[i]].inDistrict = travellerDic[travellerID].endDistrictOrDungeonID;
        }
        AreaMapPanel.Instance.UpdateDistrictSingle(travellerDic[travellerID].endDistrictOrDungeonID);

  
    }

    public void AdventureSendDone(int travellerID)
    {
        adventureTeamList[travellerDic[travellerID].team].state = AdventureState.NotSend;
        dungeonList[adventureTeamList[travellerDic[travellerID].team].dungeonID].teamList.Add((byte)travellerDic[travellerID].team);

        AreaMapPanel.Instance.UpdateDungeonSingle(travellerDic[travellerID].endDistrictOrDungeonID);
        PlayMainPanel.Instance.UpdateAdventureSingle((byte)travellerDic[travellerID].team);
        if (AreaMapPanel.Instance.dungeonInfoBlockID == travellerDic[travellerID].endDistrictOrDungeonID)
        {
            AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
        }
        if (AdventureMainPanel.Instance.isShow && AdventureMainPanel.Instance.nowCheckingTeamID == travellerDic[travellerID].team)
        {
            AdventureMainPanel.Instance.UpdateTeam((byte)travellerDic[travellerID].team);
        }
    }

    public void AdventureBackDone(int travellerID)
    {
        adventureTeamList[travellerDic[travellerID].team].state = AdventureState.Free;


        adventureTeamList[travellerDic[travellerID].team].districtID = -1;
        adventureTeamList[travellerDic[travellerID].team].dungeonID = -1;
        adventureTeamList[travellerDic[travellerID].team].heroIDList.Clear();
        Debug.Log("AdventureBackDone() travellerDic[travellerID].heroList.Count=" + travellerDic[travellerID].heroList.Count);
        for (int i = 0; i < travellerDic[travellerID].heroList.Count; i++)
        {
            districtDic[travellerDic[travellerID].endDistrictOrDungeonID].heroList.Add(travellerDic[travellerID].heroList[i]);
            heroDic[travellerDic[travellerID].heroList[i]].inDistrict = travellerDic[travellerID].endDistrictOrDungeonID;

            heroDic[travellerDic[travellerID].heroList[i]].adventureInTeam = -1;
        }
        AreaMapPanel.Instance.UpdateDistrictSingle(travellerDic[travellerID].endDistrictOrDungeonID);
        PlayMainPanel.Instance.UpdateAdventureSingle((byte)travellerDic[travellerID].team);
        //if (AreaMapPanel.Instance.districtInfoBlockID == travellerDic[travellerID].endDistrictOrDungeonID)
        //{
        //    AreaMapPanel.Instance.ShowDistrictInfoBlock(AreaMapPanel.Instance.districtInfoBlockID, (int)AreaMapPanel.Instance.districtInfoBlockRt.anchoredPosition.x, (int)AreaMapPanel.Instance.districtInfoBlockRt.anchoredPosition.y);
        //}
        if (AdventureMainPanel.Instance.isShow && AdventureMainPanel.Instance.nowCheckingTeamID == travellerDic[travellerID].team)
        {
            Debug.Log("AdventureBackDone() team="+ travellerDic[travellerID].team);
            //AdventureMainPanel.Instance.UpdateTeam((byte)travellerDic[travellerID].team);
            AdventureMainPanel.Instance.OnHide();
        }
    }

    public void CreateTravellerByRandom()
    {
        int heroType = Random.Range(0, DataManager.mHeroDict.Count);
        string pic;
        int sexCode = Random.Range(0, 2);
        if (sexCode == 0)
        {
            pic = DataManager.mHeroDict[heroType].PicMan[Random.Range(0, DataManager.mHeroDict[heroType].PicMan.Count)];
        }
        else
        {
            pic = DataManager.mHeroDict[heroType].PicWoman[Random.Range(0, DataManager.mHeroDict[heroType].PicWoman.Count)];
        }

        int startDistrict = Random.Range(0, 11);
        int endDistrict = Random.Range(0, 11);
        while (startDistrict == endDistrict)
        {
            endDistrict = Random.Range(0, 11);
        }
        List<int> pathList = DataManager.mAreaPathDict["A-" + startDistrict + "-" + endDistrict].Path;

       short force = -1;
        int ran = Random.Range(0, 100);
        if (ran < 20)
        {
            force = districtDic[startDistrict].force;
        }
        else if (ran >= 20 & ran < 40)
        {
            force =(short) Random.Range(1, forceDic.Count);
        }
        string personType = "";
        switch (Random.Range(0, 5))
        {
            case 0: personType = "平民"; break;
            case 1: personType = "商人"; break;
            case 2: personType = "冒险者"; break;
            case 3: personType = "游吟诗人"; break;
            case 4: personType = (force != -1? "士兵":"民兵"); break;
        }

        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, new List<int> { }, (short)startDistrict,(short)endDistrict, "District", -1, force, personType,Random.Range(1,4)));
        AreaMapPanel.Instance.CreateTraveller(travellerIndex, pathList, 0, pic, new List<int> { });
        travellerIndex++;
    }

    public void CreateAdventureTravellerByRandom()
    {
        int heroType = Random.Range(0, DataManager.mHeroDict.Count);
        string pic;
        int sexCode = Random.Range(0, 2);
        if (sexCode == 0)
        {
            pic = DataManager.mHeroDict[heroType].PicMan[Random.Range(0, DataManager.mHeroDict[heroType].PicMan.Count)];
        }
        else
        {
            pic = DataManager.mHeroDict[heroType].PicWoman[Random.Range(0, DataManager.mHeroDict[heroType].PicWoman.Count)];
        }

        int startDistrict = Random.Range(0, 11);
        int endDungeon =DataManager.mDistrictDict[startDistrict].DungeonList[Random.Range(0, DataManager.mDistrictDict[startDistrict].DungeonList.Count)];

        List<int> pathList = DataManager.mAreaPathDict["B-" + startDistrict + "-" + endDungeon].Path;

        short force = -1;
        int ran = Random.Range(0, 100);
        if (ran < 50)
        {
            force = districtDic[startDistrict].force;
        }
        else if (ran >= 50 & ran < 80)
        {
            force = (short)Random.Range(1, forceDic.Count);
        }
        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, new List<int> { }, (short)startDistrict,(short)endDungeon, "Dungeon", -1, force,"冒险者", Random.Range(1, 4)));
        AreaMapPanel.Instance.CreateTraveller(travellerIndex, pathList, 0, pic, new List<int> { });
        travellerIndex++;
    }

    public void CreateTravellerByHero(List<int> heroID, int startDistrict, int endDistrict)
    {
        string pic = heroDic[heroID[0]].pic;

        //AreaMapPanel.Instance.CreateTraveller(startDistrict, endDistrict, pic, heroID);
    }
    #endregion

    #region 【方法】系统设置
    public void SetVolumeMusic(byte value)
    {
        volumeMusic = value;
        AudioControl.Instance.MusicPlayer.volume = volumeMusic==0?0:( volumeMusic /5f);
    }
    public void SetVolumeSound(byte value)
    {
        volumeSound = value;
        AudioControl.Instance.SoundPlayer.volume = volumeSound == 0 ? 0 : (volumeSound / 5f);
    }

    #endregion

    #region 【辅助方法集】获取值

   public byte GetSharpnessLevel(FightMenberObject fightMenberObject)
    {
        if (fightMenberObject.side == 1)
        {
            return 3;
        }

        if (heroDic[fightMenberObject.objectID].equipWeapon == -1)
        {
            return 3;
        }
        //Debug.Log("GetSharpnessLevel() fightMenberObject.sharpnessNow=" + fightMenberObject.sharpnessNow);
        //Debug.Log("GetSharpnessLevel() weaponname=" + itemDic[heroDic[fightMenberObject.objectID].equipWeapon].name);
        byte total = 0;
        //string teststr = "";
        for (byte i = 0; i < DataManager.mItemDict[fightMenberObject.weaponPID].Sharpness.Count; i++)
        {
            total += DataManager.mItemDict[fightMenberObject.weaponPID].Sharpness[i];

            //Debug.Log("GetSharpnessLevel() total=" + total+" list:"+ DataManager.mItemDict[fightMenberObject.weaponPID].Sharpness[i]);
            if (fightMenberObject.sharpnessNow <= total)
            {
             
                return i;
            }
        }
        return 6;
    }

    float GetSharpnessModifyDamage(int level)
    {
        switch (level)
        {
            case 0:return 0.7f;
            case 1: return 0.85f;
            case 2: return 0.9f;
            case 3: return 1f;
            case 4: return 1.05f;
            case 5: return 1.1f;
            case 6: return 1.25f;
            default:return 1f;
        }
    }


    public int GetHeroNum(short forceID)
    {
        return heroDic.Count(p => p.Value.force == forceID);

    }

    public bool IsHit(FightMenberObject actionMenber, FightMenberObject targetMenber )
    {
        float hitUp = 0f;
        float dodUp = 0f;
        if (actionMenber.haloStatus)
        {
            hitUp = DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].HitUp / 100f;
        }
        if (targetMenber.haloStatus)
        {
            dodUp = DataManager.mHaloDict[heroDic[targetMenber.objectID].halo].DodUp / 100f;
        }


        int hitRate = System.Math.Max((int)(((float)actionMenber.hit*(1f+ hitUp)) / (actionMenber.hit * (1f + hitUp) + targetMenber.dod * (1f + dodUp)) * 100), 90);
        int ranHit = Random.Range(0, 100);
        if (ranHit < hitRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsCri(FightMenberObject actionMenber)
    {
        float criRUp = 0f;
        if (actionMenber.haloStatus)
        {
            criRUp = DataManager.mHaloDict[heroDic[actionMenber.objectID].halo].CriRUp / 100f;
        }
        int ranCri = Random.Range(0, 100);
        if (ranCri < (int)(actionMenber.criR*(1f+ criRUp)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetSkillMpCost(int skillID)
    {
        SkillObject so = skillDic[skillID];
        SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];
        int mp = sp.Mp;
        if (so.mpModify != 0)
        {
            mp = (int)(sp.Mp * (1f + so.mpModify / 100f));
        }
        return mp;
    }

    public int GetSkillProbability(int skillID)
    {
        SkillObject so = skillDic[skillID];
        SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];
        int probability = sp.Probability;
        if (so.rateModify != 0)
        {
            probability += so.rateModify;
            if (probability < 0)
            {
                probability = 0;
            }
            else if (probability > 0)
            {
                probability = 100;
            }
        }
        return probability;
    }

    public int GetForceFoodAll(int forceID)
    {
        return forceDic[forceID].rFoodCereal + forceDic[forceID].rFoodVegetable + forceDic[forceID].rFoodFruit + forceDic[forceID].rFoodMeat + forceDic[forceID].rFoodFish + forceDic[forceID].rFoodBeer + forceDic[forceID].rFoodWine;
    }

    public int GetForceStuffAll(int forceID)
    {
        return forceDic[forceID].rStuffWood + forceDic[forceID].rStuffStone + forceDic[forceID].rStuffMetal + forceDic[forceID].rStuffLeather + forceDic[forceID].rStuffCloth + forceDic[forceID].rStuffTwine + forceDic[forceID].rStuffBone +
            forceDic[forceID].rStuffWind + forceDic[forceID].rStuffFire + forceDic[forceID].rStuffWater + forceDic[forceID].rStuffGround + forceDic[forceID].rStuffLight + forceDic[forceID].rStuffDark;
    }

    public int GetDistrictProductAll(int districtID)
    {
        return districtDic[districtID].rProductWeapon + districtDic[districtID].rProductArmor + districtDic[districtID].rProductJewelry + districtDic[districtID].rProductScroll;
    }

    public int GetDistrictProductGoodsAll(int districtID)
    {
        return districtDic[districtID].rProductGoodWeapon + districtDic[districtID].rProductGoodArmor + districtDic[districtID].rProductGoodJewelry + districtDic[districtID].rProductGoodScroll;
    }


    public float GetProduceResourceLaborRate(int buildingID)
    {
        return Mathf.Pow(buildingDic[buildingID].workerNow, DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].LaborRate);
    }

    public float GetProduceResourceOutputUp(int buildingID)
    {
        float outputUp = 0f;
        switch (buildingDic[buildingID].prototypeID)
        {
            case 9://麦田
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workPlanting / 100f;
                }
                break;
            case 10://菜田
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workPlanting / 100f;
                }
                break;
            case 11://果园
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workPlanting / 100f;
                }
                break;
            case 12://亚麻田
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workPlanting / 100f;
                }
                break;
            case 13://牛圈
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workFeeding / 100f;
                }
                break;
            case 14://羊圈
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workFeeding / 100f;
                }
                break;
            case 15://渔场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workFishing / 100f;
                }
                break;

            case 25://啤酒厂
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workSundry / 100f;
                }
                break;

            case 26://红酒厂
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workSundry / 100f;
                }
                break;

            case 16://伐木场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workFelling / 100f;
                }
                break;
            case 17://伐木场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workFelling / 100f;
                }
                break;
            case 18://伐木场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workFelling / 100f;
                }
                break;

            case 19://矿场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workQuarrying / 100f;
                }
                break;
            case 20://矿场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workQuarrying / 100f;
                }
                break;
            case 21://矿场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workQuarrying / 100f;
                }
                break;

            case 22://采石场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workMining / 100f;
                }
                break;
            case 23://采石场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workMining / 100f;
                }
                break;
            case 24://采石场
                for (int i = 0; i < buildingDic[buildingID].heroList.Count; i++)
                {
                    outputUp += heroDic[buildingDic[buildingID].heroList[i]].workMining / 100f;
                }
                break;
        }
        return outputUp;
    }
    //基础+装备加成+套装
    public int GetHeroAttr(Attribute attribute, int heroID)
    {
        int equipAdd = 0;

        if (heroDic[heroID].equipWeapon != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipWeapon].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipWeapon].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipWeapon].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipSubhand != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipSubhand].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipSubhand].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipSubhand].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipHead != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipHead].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipHead].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipHead].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipBody != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipBody].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipBody].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipBody].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipHand != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipHand].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipHand].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipHand].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipBack != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipBack].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipBack].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipBack].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipFoot != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipFoot].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipFoot].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipFoot].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipNeck != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipNeck].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipNeck].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipNeck].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipFinger1 != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipFinger1].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipFinger1].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipFinger1].attr[i].value;
                }
            }
        }
        if (heroDic[heroID].equipFinger2 != -1)
        {
            for (int i = 0; i < itemDic[heroDic[heroID].equipFinger2].attr.Count; i++)
            {
                if (itemDic[heroDic[heroID].equipFinger2].attr[i].attr == attribute)
                {
                    equipAdd += itemDic[heroDic[heroID].equipFinger2].attr[i].value;
                }
            }
        }

        int suiteAdd = 0;
        List<short> suiteIDList = new List<short> { };
        List<byte> suiteNumList = new List<byte> { };
        for (int i = 0; i < heroDic[heroID].equipSuitePart.Count; i++)
        {
            if (heroDic[heroID].equipSuitePart[i] != -1)
            {
                if (!suiteIDList.Contains(heroDic[heroID].equipSuitePart[i]))
                {
                    suiteIDList.Add(heroDic[heroID].equipSuitePart[i]);
                    suiteNumList.Add(1);
                }
                else
                {
                    suiteNumList[suiteIDList.IndexOf(heroDic[heroID].equipSuitePart[i])]++;
                }
            }         
        }

        for (int i = 0; i < suiteIDList.Count; i++)
        {
            for (int j = 0; j < DataManager.mItemSuiteDict[suiteIDList[i]].NeedPart.Count; j++)
            {
                if (suiteNumList[i] >= DataManager.mItemSuiteDict[suiteIDList[i]].NeedPart[j])
                {
                    if (DataManager.mItemSuiteDict[suiteIDList[i]].AttributeType[j] == attribute)
                    {
                        suiteAdd += DataManager.mItemSuiteDict[suiteIDList[i]].Value[j];
                    }
                }
            }
        }


        switch (attribute)
        {
            case Attribute.Hp: return (int)heroDic[heroID].hp + equipAdd+suiteAdd;
            case Attribute.Mp: return (int)heroDic[heroID].mp + equipAdd+suiteAdd;
            case Attribute.HpRenew: return (int)heroDic[heroID].hpRenew + equipAdd+suiteAdd;
            case Attribute.MpRenew: return (int)heroDic[heroID].mpRenew + equipAdd+suiteAdd;
            case Attribute.AtkMin: return (int)heroDic[heroID].atkMin + equipAdd+suiteAdd;
            case Attribute.AtkMax: return (int)heroDic[heroID].atkMax + equipAdd+suiteAdd;
            case Attribute.MAtkMin: return (int)heroDic[heroID].mAtkMin + equipAdd+suiteAdd;
            case Attribute.MAtkMax: return (int)heroDic[heroID].mAtkMax + equipAdd+suiteAdd;
            case Attribute.Def: return (int)heroDic[heroID].def + equipAdd+suiteAdd;
            case Attribute.MDef: return (int)heroDic[heroID].mDef + equipAdd+suiteAdd;
            case Attribute.Hit: return (int)heroDic[heroID].hit + equipAdd+suiteAdd;
            case Attribute.Dod: return (int)heroDic[heroID].dod + equipAdd+suiteAdd;
            case Attribute.CriR: return (int)heroDic[heroID].criR + equipAdd+suiteAdd;
            case Attribute.CriD: return (int)heroDic[heroID].criD + equipAdd+suiteAdd;
            case Attribute.Spd: return (heroDic[heroID].equipWeapon == -1) ? (heroDic[heroID].spd + equipAdd + suiteAdd) : (equipAdd + suiteAdd);
            case Attribute.WindDam: return heroDic[heroID].windDam + equipAdd+suiteAdd;
            case Attribute.FireDam: return heroDic[heroID].fireDam + equipAdd+suiteAdd;
            case Attribute.WaterDam: return heroDic[heroID].waterDam + equipAdd+suiteAdd;
            case Attribute.GroundDam: return heroDic[heroID].groundDam + equipAdd+suiteAdd;
            case Attribute.LightDam: return heroDic[heroID].lightDam + equipAdd+suiteAdd;
            case Attribute.DarkDam: return heroDic[heroID].darkDam + equipAdd+suiteAdd;
            case Attribute.WindRes: return heroDic[heroID].windRes + equipAdd+suiteAdd;
            case Attribute.FireRes: return heroDic[heroID].fireRes + equipAdd+suiteAdd;
            case Attribute.WaterRes: return heroDic[heroID].waterRes + equipAdd+suiteAdd;
            case Attribute.GroundRes: return heroDic[heroID].groundRes + equipAdd+suiteAdd;
            case Attribute.LightRes: return heroDic[heroID].lightRes + equipAdd+suiteAdd;
            case Attribute.DarkRes: return heroDic[heroID].darkRes + equipAdd+suiteAdd;
            case Attribute.DizzyRes: return heroDic[heroID].dizzyRes + equipAdd+suiteAdd;
            case Attribute.ConfusionRes: return heroDic[heroID].confusionRes + equipAdd+suiteAdd;
            case Attribute.PoisonRes: return heroDic[heroID].poisonRes + equipAdd+suiteAdd;
            case Attribute.SleepRes: return heroDic[heroID].sleepRes + equipAdd+suiteAdd;
            case Attribute.GoldGet: return heroDic[heroID].goldGet + equipAdd+suiteAdd;
            case Attribute.ExpGet: return heroDic[heroID].expGet + equipAdd+suiteAdd;
            case Attribute.ItemGet: return heroDic[heroID].itemGet + equipAdd+suiteAdd;
            default: return 0;
        }

    }
    #endregion

    #region 【辅助方法集】输出字符
    public string OutputRandomStr(List<string> strs)
    {
        return strs[Random.Range(0, strs.Count)];
    }

    public string OutputItemTypeSmallStr(ItemTypeSmall itemTypeSmall)
    {
        switch (itemTypeSmall)
        {
            case ItemTypeSmall.Sword: return "剑";
            case ItemTypeSmall.Hammer: return "锤、棍棒";
            case ItemTypeSmall.Spear: return "枪、矛";
            case ItemTypeSmall.Axe: return "斧、镰刀";
            case ItemTypeSmall.Bow: return "弓";
            case ItemTypeSmall.Staff: return "杖";
            case ItemTypeSmall.HeadH: return "重型头部装备";
            case ItemTypeSmall.HeadL: return "轻型头部装备";
            case ItemTypeSmall.BodyH: return "重型身体装备";
            case ItemTypeSmall.BodyL: return "轻型身体装备";
            case ItemTypeSmall.HandH: return "重型手部装备";
            case ItemTypeSmall.HandL: return "轻型手部装备";
            case ItemTypeSmall.BackH: return "重型背部装备";
            case ItemTypeSmall.BackL: return "轻型背部装备";
            case ItemTypeSmall.FootH: return "重型腿部装备";
            case ItemTypeSmall.FootL: return "轻型腿部装备";
            case ItemTypeSmall.Neck: return "项链";
            case ItemTypeSmall.Finger: return "戒指";
            case ItemTypeSmall.Shield: return "副手装备（盾）";
            case ItemTypeSmall.Dorlach: return "副手装备（箭袋）";
            default: return "未定义类型";
        }
    }

    public string OutputDateStr(int t, string format)
    {
        // int t = st / 10;
        //string r = "";
        t += 240;
        int year = 0, month = 0, day = 0, hour = 0, st = 0;

        if (t >= 86400) 
        {
            year = System.Convert.ToInt16(t / 86400);
            month = System.Convert.ToInt16((t % 86400) / 7200);
            day = System.Convert.ToInt16((t % 86400 % 7200) / 240);
            hour = System.Convert.ToInt16((t % 86400 % 7200 % 240) / 10);
            st = System.Convert.ToInt16(t % 86400 % 7200 % 240 % 10);
        }
        else if (t >= 7200)
        {
            month = System.Convert.ToInt16(t / 7200);
            day = System.Convert.ToInt16((t % 7200) / 240);
            hour = System.Convert.ToInt16((t % 7200 % 240) / 10);
            st = System.Convert.ToInt16(t % 7200 % 240 % 10);
        }
        else if (t >= 240)
        {
            day = System.Convert.ToInt16(t / 240);
            hour = System.Convert.ToInt16((t % 240) / 10);
            st = System.Convert.ToInt16(t % 240 % 10);
        }
        else if (t >= 10)
        {
            hour = System.Convert.ToInt16(t / 10);
            st = System.Convert.ToInt16(t % 10);
        }
        else
        {
            st = System.Convert.ToInt16(t);
        }

        switch (format)
        {
            //TODO:bug临时表面处理
            case "Y年M月D日H时": return (year + 1) + ("年") + ((month + 1) > 12 ? 1 : (month + 1)) + ("月") + (day != 0 ? day : 1) + ("日") + hour + ("时");
            case "Y年M月D日": return (year + 1) + ("年") + ((month + 1) > 12 ? 1 : (month + 1)) + ("月") + (day != 0 ? day : 1) + ("日");
            case "Y/M/D H": return (year + 1) + ("/") + ((month + 1) > 12 ? 1 : (month + 1)) + ("/") + (day != 0 ? day : 1) + (" ") + hour;
            default: return "未知格式";
        }


    }

    public string OutputUseDateStr(int start, int end)
    {
        int cj = end - start;

        int day = cj / 240;
        return day.ToString();
    }

    public string OutputWorkValueToRank(int value)
    {
        switch (value)
        {
            case 10: return "E  ";
            case 20: return "D  ";
            case 30: return "C  ";
            case 50: return "B  ";
            case 70: return "A  ";
            case 90: return "S  ";
            case 120: return "SS ";
            case 150: return "SSS";
            default:
                return "null";
        }
    }

    public string OutputSignStr(string str, int count)
    {
        string tempStr = "";
        for (int i = 0; i < count; i++)
        {
            tempStr += str;
        }
        return tempStr;
    }

    public string OutputItemRankColorString(byte rank)
    {
        switch (rank)
        {
            case 1: return "B3B3B3";
            case 2: return "CDC9C9";
            case 3: return "32CD32";
            case 4: return "5CACEE";
            case 5: return "436EEE";
            case 6: return "CD00CD";
            case 7: return "FF8C00";
            case 8: return "EE2C2C";
            case 9: return "EEC900";
            case 10: return "EEAD0E";
            default: return "";
        }
    }

    public Color OutputItemRankColor(byte rank)
    {
        switch (rank)
        {
            case 1: return new Color(179 / 255f, 179 / 255f, 179 / 255f,1f);
            case 2: return new Color(205 / 255f, 201 / 255f, 201 / 255f, 1f);
            case 3: return new Color(50 / 255f, 205 / 255f, 50 / 255f, 1f) ;
            case 4: return new Color(92 / 255f, 172 / 255f, 238 / 255f, 1f);
            case 5: return new Color(67 / 255f, 110 / 255f, 238 / 255f, 1f);
            case 6: return new Color(205 / 255f, 0 / 255f, 205 / 255f, 1f);
            case 7: return new Color(255 / 255f, 140 / 255f, 0 / 255f, 1f);
            case 8: return new Color(238 / 255f, 44 / 255f, 44 / 255f, 1f);
            case 9: return new Color(238 / 255f, 201 / 255f, 0 / 255f, 1f);
            case 10: return new Color(238 / 255f, 173 / 255f, 14 / 255f, 1f);
            default: return Color.white;
        }
    }


    public string OutputSeasonStr(int month, bool color)
    {
        switch (month)
        {
            case 1:
            case 2:
            case 3:
                return (color ? "<color=#7BBD00>" : "") + "春" + (color ? "</color>" : "");
            case 4:
            case 5:
            case 6:
                return (color ? "<color=#DCAF00>" : "") + "夏" + (color ? "</color>" : "");
            case 7:
            case 8:
            case 9:
                return (color ? "<color=#895110>" : "") + "秋" + (color ? "</color>" : "");
            case 10:
            case 11:
            case 12:
                return (color ? "<color=#0079C6>" : "") + "冬" + (color ? "</color>" : "");
            default: return "错误的月份";
        }
    }

    public string OutputWeekStr(int week, bool color)
    {
        switch (week)
        {
            case 1: return (color ? "<color=#DA7CFF>" : "") + "星期一" + (color ? "</color>" : "");
            case 2: return (color ? "<color=#E74624>" : "") + "星期二" + (color ? "</color>" : "");
            case 3: return (color ? "<color=#24CDE7>" : "") + "星期三" + (color ? "</color>" : "");
            case 4: return (color ? "<color=#26F39A>" : "") + "星期四" + (color ? "</color>" : "");
            case 5: return (color ? "<color=#FFFFFF>" : "") + "星期五" + (color ? "</color>" : "");
            case 6: return (color ? "<color=#C08342>" : "") + "星期六" + (color ? "</color>" : "");
            case 7: return (color ? "<color=#E0DE60>" : "") + "星期日" + (color ? "</color>" : "");
            default: return "错误的星期";
        }
    }

    public string OutputAttrName(Attribute attribute)
    {
        switch (attribute)
        {

            case Attribute.Hp: return "体力上限";
            case Attribute.Mp: return "魔力上限";
            case Attribute.HpRenew: return "体力恢复";
            case Attribute.MpRenew: return "魔力恢复";
            case Attribute.AtkMin: return "最小物攻";
            case Attribute.AtkMax: return "最大物攻";
            case Attribute.MAtkMin: return "最小魔攻";
            case Attribute.MAtkMax: return "最大魔攻";
            case Attribute.Def: return "物防";
            case Attribute.MDef: return "魔防";
            case Attribute.Hit: return "命中";
            case Attribute.Dod: return "闪避";
            case Attribute.CriR: return "暴击";
            case Attribute.CriD: return "暴击伤害";
            case Attribute.Spd: return "速度";
            case Attribute.WindDam: return "风系伤害";
            case Attribute.FireDam: return "火系伤害";
            case Attribute.WaterDam: return "水系伤害";
            case Attribute.GroundDam: return "地系伤害";
            case Attribute.LightDam: return "光系伤害";
            case Attribute.DarkDam: return "暗系伤害";
            case Attribute.WindRes: return "风系抗性";
            case Attribute.FireRes: return "火系抗性";
            case Attribute.WaterRes: return "水系抗性";
            case Attribute.GroundRes: return "地系抗性";
            case Attribute.LightRes: return "光系抗性";
            case Attribute.DarkRes: return "暗系抗性";
            case Attribute.DizzyRes: return "眩晕抗性";
            case Attribute.ConfusionRes: return "混乱抗性";
            case Attribute.PoisonRes: return "中毒抗性";
            case Attribute.SleepRes: return "睡眠抗性";
            case Attribute.GoldGet: return "金币加成";
            case Attribute.ExpGet: return "经验值加成";
            case Attribute.ItemGet: return "稀有掉落加成";
            case Attribute.WorkPlanting: return "种植能力";
            case Attribute.WorkFeeding: return "饲养能力";
            case Attribute.WorkFishing: return "钓鱼能力";
            case Attribute.WorkHunting: return "打猎能力";
            case Attribute.WorkMining: return "采石能力";
            case Attribute.WorkQuarrying: return "挖矿能力";
            case Attribute.WorkFelling: return "伐木能力";
            case Attribute.WorkBuild: return "建筑能力";
            case Attribute.WorkMakeWeapon: return "武器锻造能力";
            case Attribute.WorkMakeArmor: return "防具制作能力";
            case Attribute.WorkMakeJewelry: return "饰品制作能力";
            case Attribute.WorkSundry: return "打杂能力";
            default: return "未定义类型";
        }
    }

    //物品详情-更新（辅助方法：输出属性行）
    public string OutputAttrLineStr(ItemAttribute itemAttribute)
    {
        string str;
        string strValue;
        if (itemAttribute.value > 0)
        {
            strValue = " +" + itemAttribute.value;
        }
        else
        {
            strValue = " " + itemAttribute.value;
        }

        switch (itemAttribute.attr)
        {
            case Attribute.Hp: str = "体力上限" + strValue; break;
            case Attribute.Mp: str = "魔力上限" + strValue; break;
            case Attribute.HpRenew: str = "体力恢复" + strValue + "%"; break;
            case Attribute.MpRenew: str = "魔力恢复" + strValue + "%"; break;
            case Attribute.AtkMin: str = "最小物攻" + strValue; break;
            case Attribute.AtkMax: str = "最大物攻" + strValue; break;
            case Attribute.MAtkMin: str = "最小魔攻" + strValue; break;
            case Attribute.MAtkMax: str = "最大魔攻" + strValue; break;
            case Attribute.Def: str = "物防" + strValue; break;
            case Attribute.MDef: str = "魔防" + strValue; break;
            case Attribute.Hit: str = "命中" + strValue; break;
            case Attribute.Dod: str = "闪避" + strValue; break;
            case Attribute.CriR: str = "暴击" + strValue; break;
            case Attribute.CriD: str = "暴击伤害" + strValue + "%"; break;
            case Attribute.Spd: str = "速度" + strValue; break;
            case Attribute.WindDam: str = "风系伤害" + strValue + "%"; break;
            case Attribute.FireDam: str = "火系伤害" + strValue + "%"; break;
            case Attribute.WaterDam: str = "水系伤害" + strValue + "%"; break;
            case Attribute.GroundDam: str = "地系伤害" + strValue + "%"; break;
            case Attribute.LightDam: str = "光系伤害" + strValue + "%"; break;
            case Attribute.DarkDam: str = "暗系伤害" + strValue + "%"; break;
            case Attribute.WindRes: str = "风系抗性" + strValue + "%"; break;
            case Attribute.FireRes: str = "火系抗性" + strValue + "%"; break;
            case Attribute.WaterRes: str = "水系抗性" + strValue + "%"; break;
            case Attribute.GroundRes: str = "地系抗性" + strValue + "%"; break;
            case Attribute.LightRes: str = "光系抗性" + strValue + "%"; break;
            case Attribute.DarkRes: str = "暗系抗性" + strValue + "%"; break;
            case Attribute.DizzyRes: str = "眩晕抗性" + strValue + "%"; break;
            case Attribute.ConfusionRes: str = "混乱抗性" + strValue + "%"; break;
            case Attribute.PoisonRes: str = "中毒抗性" + strValue + "%"; break;
            case Attribute.SleepRes: str = "睡眠抗性" + strValue + "%"; break;
            case Attribute.GoldGet: str = "金币加成" + strValue + "%"; break;
            case Attribute.ExpGet: str = "经验值加成" + strValue + "%"; break;
            case Attribute.ItemGet: str = "稀有掉落加成" + strValue + "%"; break;
            case Attribute.WorkPlanting: str = "种植能力" + strValue; break;
            case Attribute.WorkFeeding: str = "饲养能力" + strValue; break;
            case Attribute.WorkFishing: str = "钓鱼能力" + strValue; break;
            case Attribute.WorkHunting: str = "打猎能力" + strValue; break;
            case Attribute.WorkMining: str = "采石能力" + strValue; break;
            case Attribute.WorkQuarrying: str = "挖矿能力" + strValue; break;
            case Attribute.WorkFelling: str = "伐木能力" + strValue; break;
            case Attribute.WorkBuild: str = "建筑能力" + strValue; break;
            case Attribute.WorkMakeWeapon: str = "武器锻造能力" + strValue; break;
            case Attribute.WorkMakeArmor: str = "防具制作能力" + strValue; break;
            case Attribute.WorkMakeJewelry: str = "饰品制作能力" + strValue; break;
            case Attribute.WorkSundry: str = "打杂能力" + strValue; break;
            case Attribute.Skill:

                if (DataManager.mSkillDict[itemAttribute.skillID].FlagDamage)
                {
                    switch (itemAttribute.skillAddType)
                    {
                        case AttributeSkill.Damage: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的伤害提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.CriDamage: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>造成暴击时伤害提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.Probability: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的触发几率提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.CostMp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>消耗MP减少" + itemAttribute.value + "%"; break;
                        case AttributeSkill.IgnoreDef: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>" + itemAttribute.value + "%几率无视目标物防"; break;
                        case AttributeSkill.IgnoreMDef: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>" + itemAttribute.value + "%几率无视目标魔防"; break;
                        case AttributeSkill.SuckHp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>伤害" + itemAttribute.value + "%转化为体力"; break;
                        case AttributeSkill.SuckMp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>伤害" + itemAttribute.value + "%转化为魔力"; break;
                        case AttributeSkill.TargetNum: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的影响目标数量增加" + itemAttribute.value; break;
                        case AttributeSkill.Invincible: str = "使用<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>后有" + itemAttribute.value + "%几率在下一回合无敌"; break;
                        default: str = "未定义技能类型"; break;
                    }
                }
                else if (DataManager.mSkillDict[itemAttribute.skillID].Cure > 0)
                {
                    switch (itemAttribute.skillAddType)
                    {
                        case AttributeSkill.Damage: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的回复量提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.Probability: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的触发几率提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.CostMp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>消耗MP减少" + itemAttribute.value + "%"; break;
                        case AttributeSkill.Ap: str = "使用<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>后加快行动"; break;
                        case AttributeSkill.Invincible: str = "使用<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>后有" + itemAttribute.value + "%几率在下一回合无敌"; break;
                        default: str = "未定义技能类型"; break;
                    }
                }
                else
                {
                    switch (itemAttribute.skillAddType)
                    {
                        case AttributeSkill.Probability: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>的触发几率提升" + itemAttribute.value + "%"; break;
                        case AttributeSkill.CostMp: str = "<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>消耗MP减少" + itemAttribute.value + "%"; break;
                        case AttributeSkill.Invincible: str = "使用<color=#EDBDFF>[" + DataManager.mSkillDict[itemAttribute.skillID].Name + "]</color>后有" + itemAttribute.value + "%几率在下一回合无敌"; break;
                        default: str = "未定义技能类型"; break;
                    }
                }
                break;

            default: str = "未定义类型"; break;
        }
        if (itemAttribute.value < 0)
        {
            str = "<color=#FF554F>" + str + "</color>";
        }
        return str;
    }
    #endregion



    public static List<T> GetRandomList<T>(List<T> inputList)
    {
        //Copy to a array
        T[] copyArray = new T[inputList.Count];
        inputList.CopyTo(copyArray);

        //Add range
        List<T> copyList = new List<T>();
        copyList.AddRange(copyArray);

        //Set outputList and random
        List<T> outputList = new List<T>();
        System.Random rd = new System.Random(System.DateTime.Now.Millisecond);

        while (copyList.Count > 0)
        {
            //Select an index and item
            int rdIndex = rd.Next(0, copyList.Count);
            T remove = copyList[rdIndex];

            //remove it from copyList and add it to output
            copyList.Remove(remove);
            outputList.Add(remove);
        }
        return outputList;
    }


}
