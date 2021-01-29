using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameControl : MonoBehaviour
{


    //no save
    public const float spacing = 4f;

    //save data
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
    public int nextExecuteEventEndTime = 0;
    public int heroIndex = 0;
    public int itemIndex = 0;
    public int skillIndex = 0;
    public int customerIndex = 0;
    public int buildingIndex = 0;
    public int travellerIndex = 0;
    public int forceIndex = 0;
    public bool[] buildingUnlock = new bool[78];
    public int logIndex = 0;
    //public byte forceFlag = 0;

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
        public int nextExecuteEventEndTime = 0;
        public int heroIndex = 0;
        public int itemIndex = 0;
        public int skillIndex = 0;
        public int customerIndex = 0;
        public int buildingIndex = 0;
        public int travellerIndex = 0;
        public int forceIndex = 0;
        public bool[] buildingUnlock = new bool[78];
        public int logIndex = 0;
        //public byte forceFlag = 0;
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
        t.nextExecuteEventEndTime = this.nextExecuteEventEndTime;
        t.heroIndex = this.heroIndex;
        t.itemIndex = this.itemIndex;
        t.skillIndex = this.skillIndex;
        t.customerIndex = this.customerIndex;
        t.buildingIndex = this.buildingIndex;
        t.travellerIndex = this.travellerIndex;
        t.forceIndex = this.forceIndex;
        t.buildingUnlock = this.buildingUnlock;
        t.logIndex = this.logIndex;
       // t.forceFlag = this.forceFlag;
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
            this.nextExecuteEventEndTime = t1.nextExecuteEventEndTime;
            this.heroIndex = t1.heroIndex;
            this.itemIndex = t1.itemIndex;
            this.skillIndex = t1.skillIndex;
            this.customerIndex = t1.customerIndex;
            this.buildingIndex = t1.buildingIndex;
            this.travellerIndex = t1.travellerIndex;
            this.forceIndex = t1.forceIndex;
            this.buildingUnlock = t1.buildingUnlock;
            this.logIndex = t1.logIndex;
           // this.forceFlag = t1.forceFlag;
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

    void Start()
    {
        //Debug.Log("1heroDic.Count=" + heroDic.Count);
        //CreateHero(1);
        //Debug.Log("2heroDic.Count=" + heroDic.Count);
    }

    #region 【通用方法】生成英雄、道具、技能,英雄升级,英雄改名
    public void HeroChangeName(int heroID, string newName)
    {
        heroDic[heroID].name = newName;
        HeroPanel.Instance.UpdateBasicInfo(heroDic[heroID]);
    }

    public void CreateHero(short pid, short districtID)
    {
        heroDic.Add(heroIndex, GenerateHeroByRandom(heroIndex, pid, (byte)Random.Range(0, 2), districtID));
        heroIndex++;
    }

    public HeroObject GenerateHeroByRandom(int heroID, short heroTypeID, byte sexCode, short districtID)
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
        short windDam = 0;
        short fireDam = 0;
        short waterDam = 0;
        short groundDam = 0;
        short lightDam = 0;
        short darkDam = 0;
        short windRes = 0;
        short fireRes = 0;
        short waterRes = 0;
        short groundRes = 0;
        short lightRes = 0;
        short darkRes = 0;
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

        return new HeroObject(heroID, name, heroTypeID, 1, 0, sexCode, pic, groupRate, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
            (short)hp, (short)mp, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR,
          windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, goldGet, expGet, itemGet,
          workPlanting, workFeeding, workFishing, workHunting, workMining, workQuarrying, workFelling, workBuild, workMakeWeapon, workMakeArmor, workMakeJewelry, workMakeScroll, workSundry,
          -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, new List<int> { -1, -1, -1, -1 }, -1, -1, districtID,
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new Dictionary<short, HeroSkill>(), new List<string> { });

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


    public ItemObject GenerateItemByRandom(int itemID, DistrictObject districtObject, List<int> heroObjectIDList)
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
        string name = DataManager.mItemDict[itemID].Name + (upLevel > 0 ? " +" + upLevel : "");


        List<ItemAttribute> attrList = new List<ItemAttribute> { };

        //模板基础属性及等级修正
        if (DataManager.mItemDict[itemID].Hp != 0) { attrList.Add(new ItemAttribute(Attribute.Hp, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Hp * upRate))); }
        if (DataManager.mItemDict[itemID].Mp != 0) { attrList.Add(new ItemAttribute(Attribute.Mp, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Mp * upRate))); }
        if (DataManager.mItemDict[itemID].HpRenew != 0) { attrList.Add(new ItemAttribute(Attribute.HpRenew, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].HpRenew * upRate))); }
        if (DataManager.mItemDict[itemID].MpRenew != 0) { attrList.Add(new ItemAttribute(Attribute.MpRenew, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].AtkMax * upRate))); }

        if (DataManager.mItemDict[itemID].AtkMax != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMax, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].AtkMax * upRate))); }
        if (DataManager.mItemDict[itemID].AtkMin != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMin, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].AtkMin * upRate))); }
        if (DataManager.mItemDict[itemID].MAtkMax != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMax, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].MAtkMax * upRate))); }
        if (DataManager.mItemDict[itemID].MAtkMin != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMin, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].MAtkMin * upRate))); }

        if (DataManager.mItemDict[itemID].Def != 0) { attrList.Add(new ItemAttribute(Attribute.Def, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Def * upRate))); }
        if (DataManager.mItemDict[itemID].MDef != 0) { attrList.Add(new ItemAttribute(Attribute.MDef, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].MDef * upRate))); }

        if (DataManager.mItemDict[itemID].Hit != 0) { attrList.Add(new ItemAttribute(Attribute.Hit, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Hit * upRate))); }
        if (DataManager.mItemDict[itemID].Dod != 0) { attrList.Add(new ItemAttribute(Attribute.Dod, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Dod * upRate))); }
        if (DataManager.mItemDict[itemID].CriR != 0) { attrList.Add(new ItemAttribute(Attribute.CriR, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].CriR * upRate))); }
        if (DataManager.mItemDict[itemID].CriD != 0) { attrList.Add(new ItemAttribute(Attribute.CriD, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].CriD * upRate))); }
        if (DataManager.mItemDict[itemID].Spd != 0) { attrList.Add(new ItemAttribute(Attribute.Spd, AttributeSource.Basic, DataManager.mItemDict[itemID].Spd)); }

        if (DataManager.mItemDict[itemID].WindDam != 0) { attrList.Add(new ItemAttribute(Attribute.WindDam, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].WindDam * upRate))); }
        if (DataManager.mItemDict[itemID].FireDam != 0) { attrList.Add(new ItemAttribute(Attribute.FireDam, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].FireDam * upRate))); }
        if (DataManager.mItemDict[itemID].WaterDam != 0) { attrList.Add(new ItemAttribute(Attribute.WaterDam, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].WaterDam * upRate))); }
        if (DataManager.mItemDict[itemID].GroundDam != 0) { attrList.Add(new ItemAttribute(Attribute.GroundDam, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].GroundDam * upRate))); }
        if (DataManager.mItemDict[itemID].LightDam != 0) { attrList.Add(new ItemAttribute(Attribute.LightDam, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].LightDam * upRate))); }
        if (DataManager.mItemDict[itemID].DarkDam != 0) { attrList.Add(new ItemAttribute(Attribute.DarkDam, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].DarkDam * upRate))); }

        if (DataManager.mItemDict[itemID].WindRes != 0) { attrList.Add(new ItemAttribute(Attribute.WindRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].WindRes * upRate))); }
        if (DataManager.mItemDict[itemID].FireRes != 0) { attrList.Add(new ItemAttribute(Attribute.FireRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].FireRes * upRate))); }
        if (DataManager.mItemDict[itemID].WaterRes != 0) { attrList.Add(new ItemAttribute(Attribute.WaterRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].WaterRes * upRate))); }
        if (DataManager.mItemDict[itemID].GroundRes != 0) { attrList.Add(new ItemAttribute(Attribute.GroundRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].GroundRes * upRate))); }
        if (DataManager.mItemDict[itemID].LightRes != 0) { attrList.Add(new ItemAttribute(Attribute.LightRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].LightRes * upRate))); }
        if (DataManager.mItemDict[itemID].DarkRes != 0) { attrList.Add(new ItemAttribute(Attribute.DarkRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].DarkRes * upRate))); }

        if (DataManager.mItemDict[itemID].DizzyRes != 0) { attrList.Add(new ItemAttribute(Attribute.DizzyRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].DizzyRes * upRate))); }
        if (DataManager.mItemDict[itemID].ConfusionRes != 0) { attrList.Add(new ItemAttribute(Attribute.ConfusionRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].ConfusionRes * upRate))); }
        if (DataManager.mItemDict[itemID].PoisonRes != 0) { attrList.Add(new ItemAttribute(Attribute.PoisonRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].PoisonRes * upRate))); }
        if (DataManager.mItemDict[itemID].SleepRes != 0) { attrList.Add(new ItemAttribute(Attribute.SleepRes, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].SleepRes * upRate))); }

        if (DataManager.mItemDict[itemID].ExpGet != 0) { attrList.Add(new ItemAttribute(Attribute.ExpGet, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].ExpGet * upRate))); }
        if (DataManager.mItemDict[itemID].GoldGet != 0) { attrList.Add(new ItemAttribute(Attribute.GoldGet, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].GoldGet * upRate))); }
        if (DataManager.mItemDict[itemID].ItemGet != 0) { attrList.Add(new ItemAttribute(Attribute.ItemGet, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].ItemGet * upRate))); }

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


            if (DataManager.mLemmaDict[lemmaID].Hp.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Hp, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Hp[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Mp.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Mp, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Mp[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].HpRenew.Count != 0) { attrList.Add(new ItemAttribute(Attribute.HpRenew, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].HpRenew[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MpRenew.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MpRenew, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].MpRenew[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].AtkMax.Count != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMax, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].AtkMax[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].AtkMin.Count != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMin, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].AtkMin[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MAtkMax.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMax, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].MAtkMax[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MAtkMin.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMin, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].MAtkMin[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].Def.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Def, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Def[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MDef.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MDef, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].MDef[rank] * upRate))); }


            if (DataManager.mLemmaDict[lemmaID].Hit.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Hit, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Hit[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Dod.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Dod, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Dod[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].CriR.Count != 0) { attrList.Add(new ItemAttribute(Attribute.CriR, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].CriR[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].CriD.Count != 0) { attrList.Add(new ItemAttribute(Attribute.CriD, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].CriD[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Spd.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Spd, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Spd[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].WindDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.WindDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].WindDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].FireDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.FireDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].FireDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].WaterDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.WaterDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].WaterDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GroundDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.GroundDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].GroundDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].LightDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.LightDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].LightDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].DarkDam.Count != 0) { attrList.Add(new ItemAttribute(Attribute.DarkDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].DarkDam[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].WindRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.WindRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].WindRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].FireRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.FireRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].FireRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].WaterRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.WaterRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].WaterRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GroundRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.GroundRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].GroundRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].LightRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.LightRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].LightRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].DarkRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.DarkRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].DarkRes[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].DizzyRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.DizzyRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].DizzyRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].ConfusionRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.ConfusionRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].ConfusionRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].PoisonRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.PoisonRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].PoisonRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].SleepRes.Count != 0) { attrList.Add(new ItemAttribute(Attribute.SleepRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].SleepRes[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].ExpGet.Count != 0) { attrList.Add(new ItemAttribute(Attribute.ExpGet, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].ExpGet[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GoldGet.Count != 0) { attrList.Add(new ItemAttribute(Attribute.GoldGet, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].GoldGet[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].ItemGet.Count != 0) { attrList.Add(new ItemAttribute(Attribute.ItemGet, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].ItemGet[rank] * upRate))); }

        }



        return new ItemObject(itemIndex, itemID, name, DataManager.mItemDict[itemID].Pic, DataManager.mItemDict[itemID].Rank, upLevel, attrList,
            DataManager.mItemDict[itemID].Des + (",于" + timeYear + "年" + timeMonth + "月" + (districtObject != null ? ("在" + districtObject.name + "制作") : "获得")), DataManager.mItemDict[itemID].Cost, districtObject != null ? districtObject.id : (short)-1, false, -1, EquipPart.None);
    }

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
            DistrictMainPanel.Instance.UpdateNatureInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateCultureInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateBuildingInfo(districtDic[nowCheckingDistrictID]);
        }
        if (DistrictMapPanel.Instance.isShow && buildingDic[buildingId].districtID == nowCheckingDistrictID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingId);
            DistrictMapPanel.Instance.UpdateBasicInfo();
            DistrictMapPanel.Instance.UpdateBaselineElementText(nowCheckingDistrictID);
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
            DistrictMainPanel.Instance.UpdateNatureInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateCultureInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateBuildingInfo(districtDic[nowCheckingDistrictID]);

        }
        if (DistrictMapPanel.Instance.isShow && buildingDic[buildingId].districtID == nowCheckingDistrictID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingId);
            DistrictMapPanel.Instance.UpdateBasicInfo();
            DistrictMapPanel.Instance.UpdateBaselineElementText(nowCheckingDistrictID);
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
    public void CreateBuildEvent(short BuildingPrototypeID, short posX, short posY, byte layer, List<int> xList, List<int> yList)
    {
        Debug.Log("CreateBuildEvent() BuildingPrototypeID=" + BuildingPrototypeID);
        //再次判断是应对面板打开的时候，相关数据已经产生变化
        if (DataManager.mBuildingDict[BuildingPrototypeID].NeedWood > forceDic[0].rStuffWood)
        {
            BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
            return;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NeedStone > forceDic[0].rStuffStone)
        {
            BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
            return;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NeedMetal > forceDic[0].rStuffMetal)
        {
            BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
            return;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NeedGold > forceDic[0].gold)
        {
            BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
            return;
        }

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

        buildingDic.Add(buildingIndex, new BuildingObject(buildingIndex, buildingId, nowCheckingDistrictID, DataManager.mBuildingDict[buildingId].Name, DataManager.mBuildingDict[buildingId].MainPic, posX, posY, layer, posX > 64 ? AnimStatus.WalkLeft : AnimStatus.WalkRight, DataManager.mBuildingDict[buildingId].PanelType, DataManager.mBuildingDict[buildingId].Des, DataManager.mBuildingDict[buildingId].Level, DataManager.mBuildingDict[buildingId].Expense, DataManager.mBuildingDict[buildingId].UpgradeTo, false, false, grid, new List<int> { }, new List<int> { },
            DataManager.mBuildingDict[buildingId].People, DataManager.mBuildingDict[buildingId].Worker, 0,
            DataManager.mBuildingDict[buildingId].EWind, DataManager.mBuildingDict[buildingId].EFire, DataManager.mBuildingDict[buildingId].EWater, DataManager.mBuildingDict[buildingId].EGround, DataManager.mBuildingDict[buildingId].ELight, DataManager.mBuildingDict[buildingId].EDark,
            -1, 0, new List<StuffType> { }));


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

        StopProduceResource(buildingID);
        StartBuildingUpgrade(nowCheckingDistrictID, buildingID, needTime);


        BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
        if (buildingDic[buildingID].districtID == nowCheckingDistrictID)
        {
       
            if (DistrictMapPanel.Instance.isShow)
            {
                DistrictMapPanel.Instance.UpdateBaselineResourcesText(nowCheckingDistrictID);
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

        if (buildingDic[buildingID].districtID == nowCheckingDistrictID)
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

        StopProduceResource(buildingID);
        buildingDic.Remove(buildingID);

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

    public void StopProduceResource(int buildingID)
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
            executeEventList.RemoveAt(tempList[i]);
        }
        buildingDic[buildingID].isOpen = false;
        MessagePanel.Instance.AddMessage("接到停工命令，生产停止");
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
            BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
        }
        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == buildingDic[buildingID].districtID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
        }
    }

    void StartProduceItem(int districtID, int buildingID, int needTime, short produceEquipNow)
    {
        //value0:地区实例ID value1:建筑实例ID value2:装备模板原型ID
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.ProduceItem, standardTime, standardTime + needTime, new List<List<int>> { new List<int> { districtID }, new List<int> { buildingID }, new List<int> { produceEquipNow } }));
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
            executeEventList.RemoveAt(tempList[i]);
        }
        buildingDic[buildingID].isOpen = false;
        MessagePanel.Instance.AddMessage("接到停工命令，生产停止");
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
            BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
        }
        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == buildingDic[buildingID].districtID)
        {
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
        }
    }


    public void CreateProduceItemEvent(int buildingID)
    {
        short districtID = buildingDic[buildingID].districtID;
        if (GetDistrictProductAll(districtID) >= districtDic[districtID].rProductLimit)
        {
            buildingDic[buildingID].isOpen = false;
            MessagePanel.Instance.AddMessage("制品库房已满，生产停止");
            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
            }
            if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
            {
                DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
            }
      
            return ;
        }

        int moduleID = buildingDic[buildingID].produceEquipNow;

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
                BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
            }
            if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
            {
                DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
            }
            return ;
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

        int needLabor = DataManager.mProduceEquipDict[buildingDic[buildingID].produceEquipNow].NeedLabor;
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

        StartProduceItem(buildingDic[buildingID].districtID, buildingID, needTime, buildingDic[buildingID].produceEquipNow);
        PlayMainPanel.Instance.UpdateResources();
        if (PlayMainPanel.Instance.IsShowResourcesBlock)
        {
            PlayMainPanel.Instance.UpdateResourcesBlock();
        }
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
    }

    public void DistrictItemOrSkillAdd(short districtID, int buildingID)
    {

        //if (GetDistrictProductAll(districtID) >= districtDic[districtID].rProductLimit)
        //{
        //    MessagePanel.Instance.AddMessage("制品库房已满，生产停止");
        //    if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        //    {
        //        BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
        //        BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
        //        BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
        //    }
        //    if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        //    {
        //        DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
        //    }
        //    return false;
        //}
        int moduleID = buildingDic[buildingID].produceEquipNow;

        //if (DataManager.mProduceEquipDict[moduleID].InputWood > forceDic[0].rStuffWood ||
        //    DataManager.mProduceEquipDict[moduleID].InputStone > forceDic[0].rStuffStone ||
        //    DataManager.mProduceEquipDict[moduleID].InputMetal > forceDic[0].rStuffMetal ||
        //    DataManager.mProduceEquipDict[moduleID].InputLeather > forceDic[0].rStuffLeather ||
        //    DataManager.mProduceEquipDict[moduleID].InputCloth > forceDic[0].rStuffCloth ||
        //    DataManager.mProduceEquipDict[moduleID].InputTwine > forceDic[0].rStuffTwine ||
        //    DataManager.mProduceEquipDict[moduleID].InputBone > forceDic[0].rStuffBone ||
        //    DataManager.mProduceEquipDict[moduleID].InputWind > forceDic[0].rStuffWind ||
        //    DataManager.mProduceEquipDict[moduleID].InputFire > forceDic[0].rStuffFire ||
        //    DataManager.mProduceEquipDict[moduleID].InputWater > forceDic[0].rStuffWater ||
        //    DataManager.mProduceEquipDict[moduleID].InputGround > forceDic[0].rStuffGround ||
        //    DataManager.mProduceEquipDict[moduleID].InputLight > forceDic[0].rStuffLight ||
        //    DataManager.mProduceEquipDict[moduleID].InputDark > forceDic[0].rStuffDark)
        //{
        //    MessagePanel.Instance.AddMessage("原材料不足，生产停止");
        //    if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        //    {
        //        BuildingPanel.Instance.UpdateBasicPart(buildingDic[buildingID]);
        //        BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
        //        BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
        //    }
        //    if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        //    {
        //        DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
        //    }
        //    return false;
        //}


     


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
                itemDic.Add(itemIndex, GenerateItemByRandom(itemOrSkillID, districtDic[districtID], buildingDic[buildingID].heroList));
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
                if (ItemListAndInfoPanel.Instance.isShow)
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
                if (SkillListAndInfoPanel.Instance.isShow)
                {
                    SkillListAndInfoPanel.Instance.UpdateList(districtID, null, -1, 0);
                }
                if (DistrictMapPanel.Instance.isShow)
                {
                    DistrictMapPanel.Instance.UpdateButtonScrollNum(districtID);
                }
                break;
        }




        //forceDic[0].rStuffWood -= DataManager.mProduceEquipDict[moduleID].InputWood;
        //forceDic[0].rStuffStone -= DataManager.mProduceEquipDict[moduleID].InputStone;
        //forceDic[0].rStuffMetal -= DataManager.mProduceEquipDict[moduleID].InputMetal;
        //forceDic[0].rStuffLeather -= DataManager.mProduceEquipDict[moduleID].InputLeather;
        //forceDic[0].rStuffCloth -= DataManager.mProduceEquipDict[moduleID].InputCloth;
        //forceDic[0].rStuffTwine -= DataManager.mProduceEquipDict[moduleID].InputTwine;
        //forceDic[0].rStuffBone -= DataManager.mProduceEquipDict[moduleID].InputBone;
        //forceDic[0].rStuffWind -= DataManager.mProduceEquipDict[moduleID].InputWind;
        //forceDic[0].rStuffFire -= DataManager.mProduceEquipDict[moduleID].InputFire;
        //forceDic[0].rStuffWater -= DataManager.mProduceEquipDict[moduleID].InputWater;
        //forceDic[0].rStuffGround -= DataManager.mProduceEquipDict[moduleID].InputGround;
        //forceDic[0].rStuffLight -= DataManager.mProduceEquipDict[moduleID].InputLight;
        //forceDic[0].rStuffDark -= DataManager.mProduceEquipDict[moduleID].InputDark;


        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateHistoryInfoPart(buildingDic[buildingID]);
        }
        if (DistrictMapPanel.Instance.isShow && nowCheckingDistrictID == districtID)
        {
            DistrictMapPanel.Instance.UpdateBaselineResourcesText(districtID);
            DistrictMapPanel.Instance.UpdateSingleBuilding(buildingID);
         

        }
       
        CreateLog(LogType.ProduceDone, "", new List<int> { districtID, buildingID, itemOrSkillID });
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
                            BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
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
                            BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
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
                BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
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

        if (DistrictMainPanel.Instance.isShow)
        {
            DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[districtID]);
        }

        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateHistoryInfoPart(buildingDic[buildingID]);
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

    public void ChangeProduceEquipNow(int buildingID)
    {
        //Debug.Log("ChangeProduceEquipNow() buildingID=" + buildingID + " setForgeType=" + BuildingPanel.Instance.setForgeType + " setForgeLevel" + BuildingPanel.Instance.setForgeLevel);
        //bool needStart = (buildingDic[buildingID].produceEquipNow == -1);

        foreach (KeyValuePair<int, ProduceEquipPrototype> kvp in DataManager.mProduceEquipDict)
        {
            if (kvp.Value.MakePlace.Contains((byte)buildingDic[buildingID].prototypeID) && kvp.Value.OptionValue == BuildingPanel.Instance.setForgeType && kvp.Value.Level == (BuildingPanel.Instance.setForgeLevel + 1))
            {
                buildingDic[buildingID].produceEquipNow = kvp.Value.ID;

                break;
            }
        }
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
        }

    }

    public void ChangeProduceEquipAddStuff(int buildingID, int addIndex, StuffType newStuff)
    {
        //  buildingDic[buildingID].forgeAddStuff
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
            executeEventList.RemoveAt(tempList[i]);
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
                    DistrictMapPanel.Instance.UpdateCustomerByStage(buildingDic[buildingID].customerList[i]);
                }

            }
            customerID = buildingDic[buildingID].customerList[0];
            if (customerDic[customerID].stage == CustomerStage.Wait)
            {

                CustomerCheckGoods(customerID);
                buildingDic[customerDic[customerID].buildingID].customerList.Remove(customerID);
                customerDic[customerID].stage = CustomerStage.IntoShop;

                DistrictMapPanel.Instance.UpdateCustomerByStage(customerID);
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
            DistrictMapPanel.Instance.UpdateCustomerByStage(buildingDic[buildingID].customerList[i]);
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

    #region 【方法】英雄装备/卸下，技能配置
    public void HeroEquipSet(int heroID, EquipPart equipPart, int itemID)
    {
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
                heroDic[heroID].equipWeapon = itemID;
                break;
            case EquipPart.Subhand:
                if (heroDic[heroID].equipSubhand != -1)
                {
                    itemDic[heroDic[heroID].equipSubhand].heroID = -1;
                    itemDic[heroDic[heroID].equipSubhand].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipSubhand = itemID;
                break;
            case EquipPart.Head:
                if (heroDic[heroID].equipHead != -1)
                {
                    itemDic[heroDic[heroID].equipHead].heroID = -1;
                    itemDic[heroDic[heroID].equipHead].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipHead = itemID;
                break;
            case EquipPart.Body:
                if (heroDic[heroID].equipBody != -1)
                {
                    itemDic[heroDic[heroID].equipBody].heroID = -1;
                    itemDic[heroDic[heroID].equipBody].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipBody = itemID;
                break;
            case EquipPart.Hand:
                if (heroDic[heroID].equipHand != -1)
                {
                    itemDic[heroDic[heroID].equipHand].heroID = -1;
                    itemDic[heroDic[heroID].equipHand].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipHand = itemID;
                break;
            case EquipPart.Back:
                if (heroDic[heroID].equipBack != -1)
                {
                    itemDic[heroDic[heroID].equipBack].heroID = -1;
                    itemDic[heroDic[heroID].equipBack].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipBack = itemID;
                break;
            case EquipPart.Foot:
                if (heroDic[heroID].equipFoot != -1)
                {
                    itemDic[heroDic[heroID].equipFoot].heroID = -1;
                    itemDic[heroDic[heroID].equipFoot].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipFoot = itemID; break;
            case EquipPart.Neck:
                if (heroDic[heroID].equipNeck != -1)
                {
                    itemDic[heroDic[heroID].equipNeck].heroID = -1;
                    itemDic[heroDic[heroID].equipNeck].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipNeck = itemID; break;
            case EquipPart.Finger1:
                if (heroDic[heroID].equipFinger1 != -1)
                {
                    itemDic[heroDic[heroID].equipFinger1].heroID = -1;
                    itemDic[heroDic[heroID].equipFinger1].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipFinger1 = itemID; break;
            case EquipPart.Finger2:
                if (heroDic[heroID].equipFinger2 != -1)
                {
                    itemDic[heroDic[heroID].equipFinger2].heroID = -1;
                    itemDic[heroDic[heroID].equipFinger2].heroPart = EquipPart.None;
                }
                heroDic[heroID].equipFinger2 = itemID; break;
            default: break;
        }
        itemDic[itemID].heroID = heroID;
        itemDic[itemID].heroPart = equipPart;

        HeroPanel.Instance.UpdateEquip(heroDic[heroID], equipPart);
        ItemListAndInfoPanel.Instance.OnHide();
        PlayMainPanel.Instance.UpdateButtonItemNum();
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
                heroDic[heroID].equipWeapon = -1; break;
            case EquipPart.Subhand:
                if (heroDic[heroID].equipSubhand == -1) { return; }
                itemDic[heroDic[heroID].equipSubhand].heroID = -1;
                itemDic[heroDic[heroID].equipSubhand].heroPart = EquipPart.None;
                heroDic[heroID].equipSubhand = -1; break;
            case EquipPart.Head:
                if (heroDic[heroID].equipHead == -1) { return; }
                itemDic[heroDic[heroID].equipHead].heroID = -1;
                itemDic[heroDic[heroID].equipHead].heroPart = EquipPart.None;
                heroDic[heroID].equipHead = -1; break;
            case EquipPart.Body:
                if (heroDic[heroID].equipBody == -1) { return; }
                itemDic[heroDic[heroID].equipBody].heroID = -1;
                itemDic[heroDic[heroID].equipBody].heroPart = EquipPart.None;
                heroDic[heroID].equipBody = -1; break;
            case EquipPart.Hand:
                if (heroDic[heroID].equipHand == -1) { return; }
                itemDic[heroDic[heroID].equipHand].heroID = -1;
                itemDic[heroDic[heroID].equipHand].heroPart = EquipPart.None;
                heroDic[heroID].equipHand = -1; break;
            case EquipPart.Back:
                if (heroDic[heroID].equipBack == -1) { return; }
                itemDic[heroDic[heroID].equipBack].heroID = -1;
                itemDic[heroDic[heroID].equipBack].heroPart = EquipPart.None;
                heroDic[heroID].equipBack = -1; break;
            case EquipPart.Foot:
                if (heroDic[heroID].equipFoot == -1) { return; }
                itemDic[heroDic[heroID].equipFoot].heroID = -1;
                itemDic[heroDic[heroID].equipFoot].heroPart = EquipPart.None;
                heroDic[heroID].equipFoot = -1; break;
            case EquipPart.Neck:
                if (heroDic[heroID].equipNeck == -1) { return; }
                itemDic[heroDic[heroID].equipNeck].heroID = -1;
                itemDic[heroDic[heroID].equipNeck].heroPart = EquipPart.None;
                heroDic[heroID].equipNeck = -1; break;
            case EquipPart.Finger1:
                if (heroDic[heroID].equipFinger1 == -1) { return; }
                itemDic[heroDic[heroID].equipFinger1].heroID = -1;
                itemDic[heroDic[heroID].equipFinger1].heroPart = EquipPart.None;
                heroDic[heroID].equipFinger1 = -1; break;
            case EquipPart.Finger2:
                if (heroDic[heroID].equipFinger2 == -1) { return; }
                itemDic[heroDic[heroID].equipFinger2].heroID = -1;
                itemDic[heroDic[heroID].equipFinger2].heroPart = EquipPart.None;
                heroDic[heroID].equipFinger2 = -1; break;
        }
        forceDic[0].rProductNow++;
        HeroPanel.Instance.UpdateEquip(heroDic[heroID], equipPart);
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.UpdateAllInfoToEquip(equipPart);
        }
        PlayMainPanel.Instance.UpdateButtonItemNum();
    }

    public void HeroSkillSet(int heroID, byte index, int skillID)
    {
        if (skillID == -1)
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

            heroDic[heroID].skill[index] = skillID;
            skillDic[skillID].heroID = heroID;
        }


        HeroPanel.Instance.UpdateSkill(heroDic[heroID], index);
        SkillListAndInfoPanel.Instance.OnHide();
        
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

        itemDic.Remove(itemID);
        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateButtonItemNum();
        ItemListAndInfoPanel.Instance.OnShow(-1, 64, -88);
    }
    public void Skillales(int skillID)
    {
        if (!skillDic.ContainsKey(skillID))
        {
            MessagePanel.Instance.AddMessage("无效的物品，请重新选择");
            return;
        }
        forceDic[0].gold += skillDic[skillID].cost / 2;

        skillDic.Remove(skillID);
        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateButtonSkillNum();
        SkillListAndInfoPanel.Instance.OnShow(-1,null, 76, -104);
    }
    #endregion

    #region 【方法】市集出售

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
        customerRecordDic.Add(year + "/" + month, new CustomerRecordObject(new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 },
            new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 },
            new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }, new List<short> { 0, 0, 0, 0, 0, 0, 0 }));

        if (customerRecordDic.Count > 12)
        {
            customerRecordDic.Remove((year - 1) + "/" + month);
        }
    }

    public void CustomerCome(short districtID)
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
           
       
        DistrictMapPanel.Instance.UpdateCustomerByStage(customerIndex);
        // DistrictMapPanel.Instance.CustomerCome(customerIndex);

        customerIndex++;


    }

    public void CustomerChooseShop(int customerID)
    {
        for (int i = 0; i < districtDic[customerDic[customerID].districtID].buildingList.Count; i++)
        {
            int buildingID = districtDic[customerDic[customerID].districtID].buildingList[i];
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


    public void CustomerGone(int customerID)
    {
        Debug.Log("customerID=" + customerID);
        Debug.Log("customerDic[customerID].districtID=" + customerDic[customerID].districtID);
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
        forceDic[0].gold += spend;
        PlayMainPanel.Instance.UpdateGold();
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

    #region 【方法】冒险
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



    //TODO
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
        for (int i = 0; i < heroIDList.Count; i++)
        {
            adventureTeamList[teamID].heroIDList.Add(heroIDList[i]);

            adventureTeamList[teamID].heroHpList.Add(GetHeroAttr(Attribute.Hp, heroIDList[i]));
            adventureTeamList[teamID].heroMpList.Add(GetHeroAttr(Attribute.Mp, heroIDList[i]));

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
            executeEventList.RemoveAt(tempList[i]);
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
            AdventureMainPanel.Instance.TeamLogAdd(teamID, "探险队完成任务回来了");
            MessagePanel.Instance.AddMessage("第" + (teamID + 1) + "探险队完成任务回来了");
        }
        else if (adventureState == AdventureState.Fail)
        {
            AdventureMainPanel.Instance.TeamLogAdd(teamID, "探险队全灭");
            MessagePanel.Instance.AddMessage("第" + (teamID + 1) + "探险队全灭");
        }
        else if (adventureState == AdventureState.Retreat)
        {
            AdventureMainPanel.Instance.TeamLogAdd(teamID, "探险队撤退回来了");
            MessagePanel.Instance.AddMessage("第" + (teamID + 1) + "探险队撤退回来了");
        }
        AdventureMainPanel.Instance.TeamLogAdd(teamID, "本次探险于" + OutputDateStr(adventureTeamList[teamID].standardTimeStart, "Y年M月D日") + "出发," + OutputDateStr(standardTime, "Y年M月D日") + "返回,耗时" + OutputUseDateStr(adventureTeamList[teamID].standardTimeStart, standardTime) + "天");
        PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
        if (AreaMapPanel.Instance.dungeonInfoBlockID == adventureTeamList[teamID].dungeonID)
        {
            AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
        }
        if (AdventureTeamPanel.Instance.isShow && AdventureTeamPanel.Instance.nowTeam == teamID)
        {
            AdventureTeamPanel.Instance.UpdateHero(teamID);
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
                itemDic.Add(itemIndex, GenerateItemByRandom(adventureTeamList[teamID].getItemList[i], null, null));
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

       
        adventureTeamList[teamID].state = AdventureState.NotSend;
        adventureTeamList[teamID].action = AdventureAction.None;

        if (AreaMapPanel.Instance.dungeonInfoBlockID == adventureTeamList[teamID].dungeonID)
        {
            AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
        }

        AdventureMainPanel.Instance.UpdateTeam(teamID);
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
            AdventureTeamPanel.Instance.UpdateHero(teamID);
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
        switch (adventureEvent)
        {
            case AdventureEvent.Gold:
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
        AdventureMainPanel.Instance.TeamLogAdd(teamID, log);


        yield return new WaitForSeconds(1f);

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
                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[触发陷阱]<color=#72FF53>" + heroDic[adventureTeamList[teamID].heroIDList[i]].name + "</color>损失10%体力");
                    AdventureMainPanel.Instance.ShowEffect(teamID, 0, i, "weapon_2", 1f);
                    AdventureMainPanel.Instance.ShowDamageText(teamID, 0, i, "<color=#F86A43>-" + (int)(GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]) * 0.1f) + "</color>");
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
                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[触发陷阱]<color=#72FF53>" + heroDic[adventureTeamList[teamID].heroIDList[i]].name + "</color>损失10%魔力");
                    AdventureMainPanel.Instance.ShowEffect(teamID, 0, i, "weapon_2", 1f);
                    AdventureMainPanel.Instance.ShowDamageText(teamID, 0, i, "<color=#D76FFA>-" + (int)(GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]) * 0.1f) + "</color>");
                }
                log = "探险队行进时触发陷阱，队伍成员的魔下降了";
                break;
            case AdventureEvent.SpringHp:
                for (byte i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
                {
                    adventureTeamList[teamID].heroHpList[i] += (int)(GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]) * 0.1f);
                    if (adventureTeamList[teamID].heroHpList[i] > GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]))
                    {
                        adventureTeamList[teamID].heroHpList[i] = GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]);
                    }
                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[生命之泉]<color=#72FF53>" + heroDic[adventureTeamList[teamID].heroIDList[i]].name + "</color>恢复10%体力");
                    AdventureMainPanel.Instance.ShowEffect(teamID, 0, i, "impact_6", 1f);
                    AdventureMainPanel.Instance.ShowDamageText(teamID, 0, i, "<color=#6EFB6F>+" + (int)(GetHeroAttr(Attribute.Hp, adventureTeamList[teamID].heroIDList[i]) * 0.1f) + "</color>");
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
                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[智慧之泉]<color=#72FF53>" + heroDic[adventureTeamList[teamID].heroIDList[i]].name + "</color>恢复10%魔力");
                    AdventureMainPanel.Instance.ShowEffect(teamID, 0, i, "impact_5", 1f);
                    AdventureMainPanel.Instance.ShowDamageText(teamID, 0, i, "<color=#6FAAFA>+" + (int)(GetHeroAttr(Attribute.Mp, adventureTeamList[teamID].heroIDList[i]) * 0.1f) + "</color>");
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


        const int RoundLimit = 200;
        int CheckFightOverResult = -1;


        List<FightMenberObject> fightMenberObjects = new List<FightMenberObject>();

        Debug.Log("fightMenberObjects.Count=" + fightMenberObjects.Count );

        if (adventureTeamList[teamID].action == AdventureAction.Fight)//继续之前的战斗
        {
            fightMenberObjects = fightMenberObjectSS[teamID];
            Debug.Log(" 继续之前的战斗 teamID=" + teamID + " fightMenberObjectSS.Count=" + fightMenberObjectSS.Count);
        }
        else //新开的战斗
        {

            Debug.Log(" 新开的战斗 teamID=" + teamID + " fightMenberObjectSS.Count=" + fightMenberObjectSS.Count);
            fightMenberObjects = fightMenberObjectSS[teamID];
            //if (teamID < fightMenberObjectSS.Count)
            //{
            //    fightMenberObjectSS[teamID] = new List<FightMenberObject>();
            //    fightMenberObjects = fightMenberObjectSS[teamID];
            //}
            //else
            //{
            //    fightMenberObjectSS.Add(fightMenberObjects);

            //}

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
                if (heroDic[heroID].equipWeapon != -1)
                {
                    weaponType = DataManager.mItemDict[itemDic[heroDic[heroID].equipWeapon].prototypeID].TypeSmall;
                }

                short actionBar = 0;
                byte skillIndex = 0;//当前招式位置
                int hpNow = adventureTeamList[teamID].heroHpList[i];
                int mpNow = adventureTeamList[teamID].heroMpList[i];
                List<FightBuff> buff = new List<FightBuff> { };
                fightMenberObjects.Add(new FightMenberObject(id, objectID, side, i, name, level, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
                                                                windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, weaponType,
                                                                actionBar, skillIndex, hpNow, mpNow, buff));
            }

            int heroCount = fightMenberObjects.Count;

            //创建怪物ID列表
            adventureTeamList[teamID].enemyIDList.Clear();
            List<int> enemyIDList = new List<int> { };
            List<int> enemyLevelList = new List<int> { };
            List<float> enemyRankList = new List<float> { };
            int enemyNum = Random.Range(1, 4);
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

            List<int> enemyIDTempList = new List<int> { };//用作记录同类怪物数
            for (byte i = 0; i < enemyIDList.Count; i++)
            {
                int monsterID = enemyIDList[i];
                int level = enemyLevelList[i];

                int id = i + adventureTeamList[teamID].heroIDList.Count;
                int objectID = monsterID;//对于敌方，原型
                byte side = 1;

                byte sameNameCount = 0;

                string name = DataManager.mMonsterDict[monsterID].Name;
                if (enemyRankList[i] == 2f)
                {
                    name += "首领";
                }
                else if (enemyRankList[i] == 1.3f)
                {
                    name += "精英";
                }

                string NameModifyStr = "";
                for (int j = 0; j < enemyIDTempList.Count; j++)
                {
                    // Debug.Log("enemyRankList[i]=" + enemyRankList[i] + " enemyRankList[j]=" + enemyRankList[j]);
                    if (enemyIDTempList[j] == monsterID && enemyRankList[i] == enemyRankList[j])
                    {
                        sameNameCount++;
                    }
                }

                if (sameNameCount == 1)
                {
                    fightMenberObjects[heroCount].name += "A";
                    NameModifyStr = "B";

                }
                else if (sameNameCount == 2)
                {
                    NameModifyStr = "C";
                }

                name += NameModifyStr;

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
                                                          actionBar, skillIndex, hpNow, mpNow, buff));

                enemyIDTempList.Add(monsterID);
            }
            string monsterNameList = "";
            for (int i = 0; i < fightMenberObjects.Count; i++)
            {
                if (fightMenberObjects[i].side == 1)
                {
                    monsterNameList += fightMenberObjects[i].name + "(Lv." + fightMenberObjects[i].level + ") ";
                }
            }
            Debug.Log("fightMenberObjects.Count=" + fightMenberObjects.Count + " ,enemyIDTempList.Count=" + enemyIDTempList.Count + " ,adventureTeamList[teamID].enemyIDList.Count=" + adventureTeamList[teamID].enemyIDList.Count);
            Debug.Log("遭遇了" + monsterNameList + ",开始战斗！");
            adventureTeamList[teamID].action = AdventureAction.Fight;
            adventureTeamList[teamID].fightRound = 1;



            AdventureMainPanel.Instance.TeamLogAdd(teamID, "遭遇了" + monsterNameList + ",开始战斗！");
            PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
            if (AreaMapPanel.Instance.dungeonInfoBlockID == adventureTeamList[teamID].dungeonID)
            {
                AreaMapPanel.Instance.UpdateDungeonInfoBlock(AreaMapPanel.Instance.dungeonInfoBlockID);
            }
        }

        Debug.Log("fightMenberObjects.Count="+fightMenberObjects.Count);
        AdventureMainPanel.Instance.UpdateSceneRoleFormations(teamID);
        AdventureMainPanel.Instance.UpdateSceneRole(teamID);

        AdventureMainPanel.Instance.UpdateSceneEnemy(teamID);
        AdventureMainPanel.Instance.HideSceneRoleHpMp(teamID);
        AdventureMainPanel.Instance.UpdateSceneRoleHpMp(teamID, fightMenberObjects);
        AdventureMainPanel.Instance.HideSceneRoleBuff(teamID);
        AdventureMainPanel.Instance.UpdateSceneRoleBuff(teamID, fightMenberObjects);
        AdventureMainPanel.Instance.HideElementPoint(teamID);
        AdventureMainPanel.Instance.UpdateElementPoint(teamID);

        if (AdventureTeamPanel.Instance.isShow && AdventureTeamPanel.Instance.nowTeam == teamID)
        {
            AdventureTeamPanel.Instance.UpdateNow(teamID);
        }

        yield return new WaitForSeconds(1f);





        List<FightMenberObject> actionMenber = new List<FightMenberObject>();

        while (adventureTeamList[teamID].fightRound <= RoundLimit)
        {

            //选取行动槽满的战斗成员
            while (actionMenber.Count == 0)
            {
                for (int i = 0; i < fightMenberObjects.Count; i++)
                {
                    if (fightMenberObjects[i].hpNow > 0)
                    {
                        fightMenberObjects[i].actionBar += fightMenberObjects[i].spd;
                        if (fightMenberObjects[i].actionBar >= 200)
                        {
                            actionMenber.Add(fightMenberObjects[i]);
                            fightMenberObjects[i].actionBar = (short)(fightMenberObjects[i].actionBar - 200);
                        }
                    }
                }
            }
            //行动成员行动
            for (int i = 0; i < actionMenber.Count; i++)
            {

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
                            AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(actionMenber[i]) + "受到" + (int)(actionMenber[i].hp * 0.05f) + "点中毒伤害");
                            actionMenber[i].hpNow -= (int)(actionMenber[i].hp * 0.05f);
                            if (actionMenber[i].hpNow < 0)
                            {
                                actionMenber[i].hpNow = 0;
                                AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(actionMenber[i]) + "被打倒了！");
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

                        //  if(heroDic[actionMenber[i].objectID].skillInfo.ContainsKey())
                    }
                    else if (actionMenber[i].side == 1)
                    {
                        skillID = DataManager.mMonsterDict[actionMenber[i].objectID].SkillID[skillIndex];
                    }


                    if (skillID != -1)
                    {
                        SkillObject so = skillDic[skillID];
                        SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];
                        float skillLevelUp = 0f;
                        if (actionMenber[i].side == 0)
                        {
                            if (heroDic[actionMenber[i].objectID].skillInfo.ContainsKey(so.prototypeID))
                            {
                                skillLevelUp = 0.1f * (heroDic[actionMenber[i].objectID].skillInfo[so.prototypeID].level - 1);
                            }
                        }
                        if (GetSkillMpCost(skillID) <= actionMenber[i].mpNow)
                        {
                            int ran = Random.Range(0, 100);
                            if (ran < GetSkillProbability(skillID))
                            {
                                Debug.Log("[" + adventureTeamList[teamID].fightRound + "]" + actionMenber[i].name + "发动技能");
                                StartCoroutine(Attack(teamID, actionMenber[i], sp));

                                actionMenber[i].mpNow -= GetSkillMpCost(skillID);
                                AdventureMainPanel.Instance.UpdateSceneRoleHpMpSingle(teamID, actionMenber[i]);
                                AdventureMainPanel.Instance.UpdateHeroHpMpSingle(teamID, actionMenber[i]);
                                //选取目标
                                List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], sp);

                                if (targetMenber.Count > 0)
                                {
                                    //对目标行动
                                    for (int j = 0; j < targetMenber.Count; j++)
                                    {

                                        if (sp.FlagDamage)
                                        {

                                            if (IsHit(actionMenber[i].hit, targetMenber[j].dod))
                                            {
                                                int damageMin = System.Math.Max(0, (int)(actionMenber[i].atkMin * (sp.Atk / 100f) - targetMenber[j].def)) + System.Math.Max(0, (int)(actionMenber[i].mAtkMin * (sp.MAtk / 100f) - targetMenber[j].mDef));
                                                int damageMax = System.Math.Max(0, (int)(actionMenber[i].atkMax * (sp.Atk / 100f) - targetMenber[j].def)) + System.Math.Max(0, (int)(actionMenber[i].mAtkMax * (sp.MAtk / 100f) - targetMenber[j].mDef));

                                                if (sp.Sword != 0 && actionMenber[i].weaponType == ItemTypeSmall.Sword)
                                                {
                                                    damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                                                    damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                                                }
                                                if (sp.Axe != 0 && actionMenber[i].weaponType == ItemTypeSmall.Axe)
                                                {
                                                    damageMin = (int)(damageMin * (1f + (sp.Axe / 100f)));
                                                    damageMax = (int)(damageMax * (1f + (sp.Axe / 100f)));
                                                }
                                                if (sp.Spear != 0 && actionMenber[i].weaponType == ItemTypeSmall.Spear)
                                                {
                                                    damageMin = (int)(damageMin * (1f + (sp.Spear / 100f)));
                                                    damageMax = (int)(damageMax * (1f + (sp.Spear / 100f)));
                                                }
                                                if (sp.Hammer != 0 && actionMenber[i].weaponType == ItemTypeSmall.Hammer)
                                                {
                                                    damageMin = (int)(damageMin * (1f + (sp.Hammer / 100f)));
                                                    damageMax = (int)(damageMax * (1f + (sp.Hammer / 100f)));
                                                }
                                                if (sp.Bow != 0 && actionMenber[i].weaponType == ItemTypeSmall.Bow)
                                                {
                                                    damageMin = (int)(damageMin * (1f + (sp.Bow / 100f)));
                                                    damageMax = (int)(damageMax * (1f + (sp.Bow / 100f)));
                                                }
                                                if (sp.Staff != 0 && actionMenber[i].weaponType == ItemTypeSmall.Staff)
                                                {
                                                    damageMin = (int)(damageMin * (1f + (sp.Staff / 100f)));
                                                    damageMax = (int)(damageMax * (1f + (sp.Staff / 100f)));
                                                }

                                                int damage = Random.Range(damageMin, damageMax + 1);

                                                int damageWithElement = 0;


                                                if (sp.Element.Contains(0))
                                                {
                                                    damageWithElement = damage;
                                                }
                                                else
                                                {
                                                    if (sp.Element.Contains(1))
                                                    {
                                                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].windDam + sp.Wind + adventureTeamList[teamID].dungeonEPWind * 20 - targetMenber[j].windRes) / 100f)));
                                                    }
                                                    if (sp.Element.Contains(2))
                                                    {
                                                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].fireDam + sp.Fire + adventureTeamList[teamID].dungeonEPFire * 20 - targetMenber[j].fireRes) / 100f)));
                                                    }
                                                    if (sp.Element.Contains(3))
                                                    {
                                                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].waterDam + sp.Water + adventureTeamList[teamID].dungeonEPWater * 20 - targetMenber[j].waterRes) / 100f)));
                                                    }
                                                    if (sp.Element.Contains(4))
                                                    {
                                                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].groundDam + sp.Ground + adventureTeamList[teamID].dungeonEPGround * 20 - targetMenber[j].groundRes) / 100f)));
                                                    }
                                                    if (sp.Element.Contains(5))
                                                    {
                                                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].lightDam + sp.Light + adventureTeamList[teamID].dungeonEPLight * 20 - targetMenber[j].lightRes) / 100f)));
                                                    }
                                                    if (sp.Element.Contains(6))
                                                    {
                                                        damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].darkDam + sp.Dark + adventureTeamList[teamID].dungeonEPDark * 20 - targetMenber[j].darkRes) / 100f)));
                                                    }
                                                }

                                                damageWithElement = (int)((1f + skillLevelUp) * damageWithElement);

                                                int ranCri = Random.Range(0, 100);
                                                if (ranCri < actionMenber[i].criR)
                                                {
                                                    damageWithElement = (int)(damageWithElement * (actionMenber[i].criD / 100f));
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

                                                StartCoroutine(TakeDamage(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[j], sp, damageWithElement, damageTimes, 1f + skillLevelUp));


                                            }
                                            else//未命中
                                            {
                                                Miss(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[j]);
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

                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "眩晕了");
                                                }
                                                else
                                                {
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "眩晕效果未生效");
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

                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "混乱了");
                                                }
                                                else
                                                {
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "混乱效果未生效");
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

                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "睡眠了");
                                                }
                                                else
                                                {
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "睡眠效果未生效");
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

                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "中毒了");
                                                }
                                                else
                                                {
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + "中毒效果未生效");
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
                                            StartCoroutine(TakeCure(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[j], sp, Attribute.Hp, cure, damageTimes));


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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "物理攻击提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "魔法攻击提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "物理防御提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "魔法防御提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "命中提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "闪避提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暴击伤害提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "风系伤害提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "火系伤害提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "水系伤害提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "地系伤害提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "光系伤害提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暗系伤害提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "风系抗性提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "火系抗性提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "水系抗性提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "地系抗性提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "光系抗性提升了");
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
                                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber[j]) + "暗系抗性提升了");
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
                                                AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(actionMenber[i]) + "从" + OutputNameWithColor(targetMenber[j]) + "夺得" + gold + "金币");
                                            }
                                        }
                                    }
                                }


                            }
                            else//技能概率未触发，普通攻击
                            {
                                Debug.Log("[" + adventureTeamList[teamID].fightRound + "]" + actionMenber[i].name + "技能概率未触发，普通攻击");
                                StartCoroutine(Attack(teamID, actionMenber[i], null));
                                //选取目标
                                List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], null);
                                if (targetMenber.Count > 0)
                                {
                                    if (IsHit(actionMenber[i].hit, targetMenber[0].dod))
                                    {
                                        int damageMin = System.Math.Max(0, actionMenber[i].atkMin - targetMenber[0].def);
                                        int damageMax = System.Math.Max(0, actionMenber[i].atkMax - targetMenber[0].def);
                                        int damage = Random.Range(damageMin, damageMax + 1);
                                        int ranCri = Random.Range(0, 100);
                                        if (ranCri < actionMenber[i].criR)
                                        {
                                            damage = (int)(damage * (actionMenber[i].criD / 100f));
                                        }
                                        StartCoroutine(TakeDamage(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[0], null, damage, 1, 1f));

                                    }
                                    else//未命中
                                    {
                                        Miss(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[0]);
                                    }
                                }

                            }

                        }
                        else//MP不足，普通攻击
                        {
                            Debug.Log("[" + adventureTeamList[teamID].fightRound + "]" + actionMenber[i].name + "MP不足，普通攻击");
                            StartCoroutine(Attack(teamID, actionMenber[i], null));
                            //选取目标
                            List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], null);
                            if (targetMenber.Count > 0)
                            {

                                if (IsHit(actionMenber[i].hit, targetMenber[0].dod))
                                {
                                    int damageMin = System.Math.Max(0, actionMenber[i].atkMin - targetMenber[0].def);
                                    int damageMax = System.Math.Max(0, actionMenber[i].atkMax - targetMenber[0].def);
                                    int damage = Random.Range(damageMin, damageMax + 1);
                                    int ranCri = Random.Range(0, 100);
                                    if (ranCri < actionMenber[i].criR)
                                    {
                                        damage = (int)(damage * (actionMenber[i].criD / 100f));
                                    }

                                    StartCoroutine(TakeDamage(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[0], null, damage, 1, 1f));

                                }
                                else//未命中
                                {
                                    Miss(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[0]);
                                }
                            }

                        }

                    }
                    else//普通攻击
                    {
                        Debug.Log("[" + adventureTeamList[teamID].fightRound + "]" + actionMenber[i].name + "发动设置的普通攻击");
                        StartCoroutine(Attack(teamID, actionMenber[i], null));
                        //选取目标
                        List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], null);
                        if (targetMenber.Count > 0)
                        {
                            if (IsHit(actionMenber[i].hit, targetMenber[0].dod))
                            {
                                int damageMin = System.Math.Max(0, actionMenber[i].atkMin - targetMenber[0].def);
                                int damageMax = System.Math.Max(0, actionMenber[i].atkMax - targetMenber[0].def);
                                int damage = Random.Range(damageMin, damageMax + 1);
                                int ranCri = Random.Range(0, 100);
                                if (ranCri < actionMenber[i].criR)
                                {
                                    damage = (int)(damage * (actionMenber[i].criD / 100f));
                                }

                                StartCoroutine(TakeDamage(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[0], null, damage, 1, 1f));

                            }
                            else//未命中
                            {
                                Miss(teamID, adventureTeamList[teamID].fightRound, actionMenber[i], targetMenber[0]);
                            }

                        }


                    }

                    adventureTeamList[teamID].fightRound++;

                    actionMenber[i].skillIndex++;
                    if (actionMenber[i].skillIndex >= 4)
                    {
                        actionMenber[i].skillIndex = 0;
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
            AdventureMainPanel.Instance.TeamLogAdd(teamID, "超过" + RoundLimit + "回合");
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
                            }
                        }
                    }


                }

            }

            AdventureMainPanel.Instance.TeamLogAdd(teamID, "战斗胜利！获得[经验值" + getExp + "][金币" + getGold + "] " + getStr);


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
            AdventureMainPanel.Instance.TeamLogAdd(teamID, "被击败了！");

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
                AdventureMainPanel.Instance.TeamLogAdd(teamID, "撤出战斗！");

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
    }

    void Miss(byte teamID, int round, FightMenberObject actionMenber, FightMenberObject targetMenber)
    {
        AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, "闪避");
        AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + adventureTeamList[teamID].fightRound + "]" + OutputNameWithColor(targetMenber) + "避开了" + OutputNameWithColor(actionMenber) + "的攻击");
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
            if (sp.Rank == 4)
            {
                AdventureMainPanel.Instance.ShowTalk(teamID, actionMenber.side, actionMenber.sideIndex, "绝技 <color=#698BFF>" + sp.Name + "</color>");
            }

            AdventureDungeonElementChange(teamID, sp.Element, (byte)(sp.Rank * 10));
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
            AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, "<b><color=#" + color + ">+" + cureSingle + "</color></b>");
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
        AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + round + "]" + (sp != null ? OutputNameWithColor(actionMenber) + "用<color=#4FA3C1>" + sp.Name + "</color>" : "") + "恢复" + OutputNameWithColor(targetMenber) + "<color=#86E3C9>" + cureTotalStr + "</color>点" + (attribute == Attribute.Hp ? "体力" : "魔力"));

    }

    IEnumerator TakeDamage(byte teamID, int round, FightMenberObject actionMenber, FightMenberObject targetMenber, SkillPrototype sp, int damage, byte times, float size)
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

        List<int> damageList = new List<int> { };
        int damageTotal = 0;
        string damageTotalStr = "";
        for (byte i = 0; i < times; i++)
        {
            int damageSingle = (i == 0 ? damage : (int)(Random.Range(0.5f, 0.9f) * damage));
            damageList.Add(damageSingle);
            damageTotal += damageSingle;


            AdventureMainPanel.Instance.ShowEffect(teamID, targetMenber.side, targetMenber.sideIndex, effectName, size);
            AdventureMainPanel.Instance.ShowDamageText(teamID, targetMenber.side, targetMenber.sideIndex, damage > 0 ? ("<b><color=#F86A43>-" + damageSingle + "</color></b>") : "格挡");
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

        if (targetMenber.hpNow < 0)
        {
            targetMenber.hpNow = 0;
        }
        AdventureMainPanel.Instance.UpdateSceneRoleHpMpSingle(teamID, targetMenber);
        AdventureMainPanel.Instance.UpdateHeroHpMpSingle(teamID, targetMenber);
        AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + round + "]" + OutputNameWithColor(actionMenber) + (sp != null ? "用<color=#4FA3C1>" + sp.Name + "</color>" : "普通攻击") + "对" + OutputNameWithColor(targetMenber) + "造成<color=#EC838F>" + damageTotalStr + "</color>点伤害");

        if (targetMenber.hpNow == 0)
        {
            AdventureMainPanel.Instance.SetAnim(teamID, targetMenber.side, targetMenber.sideIndex, AnimStatus.Death);
            AdventureMainPanel.Instance.TeamLogAdd(teamID, "[回合" + round + "]" + OutputNameWithColor(targetMenber) + "被打倒了！");

            if (targetMenber.side == 0)
            {
                heroDic[targetMenber.objectID].countDeath++;
            }
            else if (targetMenber.side == 1)
            {
                heroDic[actionMenber.objectID].countKill++;
            }
        }
    }

    void AdventureDungeonElementChange(byte teamID, List<int> addType, byte addValue)
    {
        short MinPoint = 100;

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
                executeEventList.Insert(i, executeEventObject);
                return;
            }
        }
        executeEventList.Add(executeEventObject);
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
        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, heroListTemp, endDistrictID, "District", -1,0,"冒险者", heroListTemp.Count));
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
        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, heroListTemp, endDungeonID, "Dungeon", teamID,0,"冒险者", heroListTemp.Count));
        AreaMapPanel.Instance.CreateTraveller(travellerIndex, pathList, 0, pic, heroListTemp);
        travellerIndex++;

        adventureTeamList[teamID].state = AdventureState.Sending;
        PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
        TransferPanel.Instance.OnHide();
        if (districtDic[startDistrictID].force != 0 && districtDic[startDistrictID].heroList.Count == 0)
        {
            DistrictMapPanel.Instance.OnHide();
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
        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, heroListTemp, adventureTeamList[teamID].districtID, "District", teamID,0,"冒险者", heroListTemp.Count));
        AreaMapPanel.Instance.CreateTraveller(travellerIndex, pathList, 0, pic, heroListTemp);
        travellerIndex++;

        adventureTeamList[teamID].state = AdventureState.Backing;
        PlayMainPanel.Instance.UpdateAdventureSingle(teamID);
        AreaMapPanel.Instance.UpdateDungeonInfoBlock(adventureTeamList[teamID].dungeonID);

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
    }

    public void AdventureBackDone(int travellerID)
    {
        adventureTeamList[travellerDic[travellerID].team].state = AdventureState.Free;


        adventureTeamList[travellerDic[travellerID].team].districtID = -1;
        adventureTeamList[travellerDic[travellerID].team].dungeonID = -1;
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

        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, new List<int> { }, (short)endDistrict, "District", -1, force, personType,Random.Range(1,4)));
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
        travellerDic.Add(travellerIndex, new TravellerObject(pic, pathList, 1, 0, 0, new List<int> { }, (short)endDungeon, "Dungeon", -1, force,"冒险者", Random.Range(1, 4)));
        AreaMapPanel.Instance.CreateTraveller(travellerIndex, pathList, 0, pic, new List<int> { });
        travellerIndex++;
    }

    public void CreateTravellerByHero(List<int> heroID, int startDistrict, int endDistrict)
    {
        string pic = heroDic[heroID[0]].pic;

        //AreaMapPanel.Instance.CreateTraveller(startDistrict, endDistrict, pic, heroID);
    }
    #endregion

    #region 【辅助方法集】获取值
    public bool IsHit(short hit, short dod)
    {
        int hitRate = System.Math.Max((int)((float)hit / (hit + dod) * 100), 90);
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
    //基础+装备加成
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


        switch (attribute)
        {
            case Attribute.Hp: return (int)heroDic[heroID].hp + equipAdd;
            case Attribute.Mp: return (int)heroDic[heroID].mp + equipAdd;
            case Attribute.HpRenew: return (int)heroDic[heroID].hpRenew + equipAdd;
            case Attribute.MpRenew: return (int)heroDic[heroID].mpRenew + equipAdd;
            case Attribute.AtkMin: return (int)heroDic[heroID].atkMin + equipAdd;
            case Attribute.AtkMax: return (int)heroDic[heroID].atkMax + equipAdd;
            case Attribute.MAtkMin: return (int)heroDic[heroID].mAtkMin + equipAdd;
            case Attribute.MAtkMax: return (int)heroDic[heroID].mAtkMax + equipAdd;
            case Attribute.Def: return (int)heroDic[heroID].def + equipAdd;
            case Attribute.MDef: return (int)heroDic[heroID].mDef + equipAdd;
            case Attribute.Hit: return (int)heroDic[heroID].hit + equipAdd;
            case Attribute.Dod: return (int)heroDic[heroID].dod + equipAdd;
            case Attribute.CriR: return (int)heroDic[heroID].criR + equipAdd;
            case Attribute.CriD: return (int)heroDic[heroID].criD + equipAdd;
            case Attribute.Spd: return (heroDic[heroID].equipWeapon == -1) ? (heroDic[heroID].spd + equipAdd) : equipAdd;
            case Attribute.WindDam: return heroDic[heroID].windDam + equipAdd;
            case Attribute.FireDam: return heroDic[heroID].fireDam + equipAdd;
            case Attribute.WaterDam: return heroDic[heroID].waterDam + equipAdd;
            case Attribute.GroundDam: return heroDic[heroID].groundDam + equipAdd;
            case Attribute.LightDam: return heroDic[heroID].lightDam + equipAdd;
            case Attribute.DarkDam: return heroDic[heroID].darkDam + equipAdd;
            case Attribute.WindRes: return heroDic[heroID].windRes + equipAdd;
            case Attribute.FireRes: return heroDic[heroID].fireRes + equipAdd;
            case Attribute.WaterRes: return heroDic[heroID].waterRes + equipAdd;
            case Attribute.GroundRes: return heroDic[heroID].groundRes + equipAdd;
            case Attribute.LightRes: return heroDic[heroID].lightRes + equipAdd;
            case Attribute.DarkRes: return heroDic[heroID].darkRes + equipAdd;
            case Attribute.DizzyRes: return heroDic[heroID].dizzyRes + equipAdd;
            case Attribute.ConfusionRes: return heroDic[heroID].confusionRes + equipAdd;
            case Attribute.PoisonRes: return heroDic[heroID].poisonRes + equipAdd;
            case Attribute.SleepRes: return heroDic[heroID].sleepRes + equipAdd;
            case Attribute.GoldGet: return heroDic[heroID].goldGet + equipAdd;
            case Attribute.ExpGet: return heroDic[heroID].expGet + equipAdd;
            case Attribute.ItemGet: return heroDic[heroID].itemGet + equipAdd;
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

        if (t >= 86400) //天,
        {
            year = System.Convert.ToInt16(t / 86400);
            month = System.Convert.ToInt16((t % 86400) / 7200);
            day = System.Convert.ToInt16((t % 86400 % 7200) / 240);
            hour = System.Convert.ToInt16((t % 86400 % 7200 % 240) / 10);
            st = System.Convert.ToInt16(t % 86400 % 7200 % 240 % 10);
        }
        else if (t >= 7200)//时,
        {
            month = System.Convert.ToInt16(t / 7200);
            day = System.Convert.ToInt16((t % 7200) / 240);
            hour = System.Convert.ToInt16((t % 7200 % 240) / 10);
            st = System.Convert.ToInt16(t % 7200 % 240 % 10);
        }
        else if (t >= 240)//分
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
