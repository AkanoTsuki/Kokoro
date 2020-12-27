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
    public int gold = 0;
    public short nowCheckingDistrictID = 0;
    public int standardTime = 0;//时间戳，基准时间单位：1/10小时（例如 1天为240单位 ）
    public List<ExecuteEventObject> executeEventList = new List<ExecuteEventObject>();
    public int nextExecuteEventEndTime = 0;
    public int heroIndex = 0;
    public int itemIndex = 0;
    public int skillIndex = 0;
    public int buildingIndex = 0;
    public bool[] buildingUnlock = new bool[73];
    public int logIndex = 0;
    public string playerName = "AAA";
    public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
    public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();
    public DistrictObject[] districtDic = new DistrictObject[7];
    public Dictionary<int, DistrictGridObject> districtGridDic = new Dictionary<int, DistrictGridObject>();
    public Dictionary<int, BuildingObject> buildingDic = new Dictionary<int, BuildingObject>();
    public Dictionary<int, LogObject> logDic = new Dictionary<int, LogObject>();
    public List<AdventureTeamObject> adventureTeamList = new List<AdventureTeamObject>();
    public List<DungeonObject> dungeonList = new List<DungeonObject>();
    public Dictionary<int, SkillObject> skillDic = new Dictionary<int, SkillObject>();
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
        public int gold = 0;
        public short nowCheckingDistrictID = 0;
        public int standardTime = 0;
        public List<ExecuteEventObject> executeEventList = new List<ExecuteEventObject>();
        public int nextExecuteEventEndTime = 0;
        public int heroIndex = 0;
        public int itemIndex = 0;
        public int skillIndex = 0;
        public int buildingIndex = 0;
        public bool[] buildingUnlock = new bool[73];
        public int logIndex = 0;
        public string playerName = "";
        public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
        public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();
        public DistrictObject[] districtDic = new DistrictObject[7];
        public Dictionary<int, DistrictGridObject> districtGridDic = new Dictionary<int, DistrictGridObject>();
        public Dictionary<int, BuildingObject> buildingDic = new Dictionary<int, BuildingObject>();
        public Dictionary<int, LogObject> logDic = new Dictionary<int, LogObject>();
        public List<AdventureTeamObject> adventureTeamList = new List<AdventureTeamObject>();
        public List<DungeonObject> dungeonList = new List<DungeonObject>();
        public Dictionary<int, SkillObject> skillDic = new Dictionary<int, SkillObject>();
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
        t.gold = this.gold;
        t.nowCheckingDistrictID = this.nowCheckingDistrictID;
        t.standardTime = this.standardTime;
        t.executeEventList = this.executeEventList;
        t.nextExecuteEventEndTime = this.nextExecuteEventEndTime;
        t.heroIndex = this.heroIndex;
        t.itemIndex = this.itemIndex;
        t.skillIndex = this.skillIndex;
        t.buildingIndex = this.buildingIndex;
        t.buildingUnlock = this.buildingUnlock;
        t.logIndex = this.logIndex;
        t.playerName = this.playerName;
        t.itemDic = this.itemDic;
        t.heroDic = this.heroDic;
        t.districtDic = this.districtDic;
        t.districtGridDic = this.districtGridDic;
        t.buildingDic = this.buildingDic;
        t.logDic = this.logDic;
        t.adventureTeamList = this.adventureTeamList;
        t.dungeonList = this.dungeonList;
        t.skillDic = this.skillDic;
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
            this.gold = t1.gold;
            this.nowCheckingDistrictID = t1.nowCheckingDistrictID;
            this.standardTime = t1.standardTime;
            this.executeEventList = t1.executeEventList;
            this.nextExecuteEventEndTime = t1.nextExecuteEventEndTime;
            this.heroIndex = t1.heroIndex;
            this.itemIndex = t1.itemIndex;
            this.skillIndex = t1.skillIndex;
            this.buildingIndex = t1.buildingIndex;
            this.buildingUnlock = t1.buildingUnlock;
            this.logIndex = t1.logIndex;
            this.playerName = t1.playerName;
            this.itemDic = t1.itemDic;
            this.heroDic = t1.heroDic;
            this.districtDic = t1.districtDic;
            this.districtGridDic = t1.districtGridDic;
            this.buildingDic = t1.buildingDic;
            this.logDic = t1.logDic;
            this.adventureTeamList = t1.adventureTeamList;
            this.dungeonList = t1.dungeonList;
            this.skillDic = t1.skillDic;
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

    #region 【通用方法】生成英雄、道具、技能
    public void CreateHero(short pid)
    {
        heroDic.Add(heroIndex, GenerateHeroByRandom(heroIndex, pid, (byte)Random.Range(0, 2)));
        heroIndex++;
    }

    public HeroObject GenerateHeroByRandom(int heroID, short heroTypeID, byte sexCode)
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
        float groupRate = Random.Range(DataManager.mHeroDict[heroTypeID].GroupRate - 0.2f, DataManager.mHeroDict[heroTypeID].GroupRate + 0.2f);

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
        byte workSundry = (byte)SetAttr(Attribute.WorkSundry, heroTypeID);


        return new HeroObject(heroID, name, heroTypeID, 1, 0, sexCode, pic, groupRate, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
          windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, goldGet, expGet, itemGet,
          workPlanting, workFeeding, workFishing, workHunting, workMining, workQuarrying, workFelling, workBuild, workMakeWeapon, workMakeArmor, workMakeJewelry, workSundry,
          -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, new List<int> { -1, -1, -1, -1 }, -1, -1);

    }

    //public HeroObject GenerateHeroByMould(int heroID, short heroTypeID, byte sexCode,string nameSet)
    //{

    //    string name = "";
    //    string pic = "";
    //    if (sexCode == 0)
    //    {
    //        name = DataManager.mNameMan[Random.Range(0, DataManager.mNameMan.Length)];
    //        pic = DataManager.mHeroDict[heroTypeID].PicMan;
    //    }
    //    else
    //    {
    //        name = DataManager.mNameWoman[Random.Range(0, DataManager.mNameWoman.Length)];
    //        pic = DataManager.mHeroDict[heroTypeID].PicWoman;
    //    }

    //    int hp = DataManager.mHeroDict[heroTypeID].Hp;
    //    int mp = DataManager.mHeroDict[heroTypeID].Mp;
    //    short hpRenew = DataManager.mHeroDict[heroTypeID].HpRenew;
    //    short mpRenew = DataManager.mHeroDict[heroTypeID].MpRenew;
    //    short atkMin = DataManager.mHeroDict[heroTypeID].AtkMin;
    //    short atkMax = DataManager.mHeroDict[heroTypeID].AtkMax;
    //    short mAtkMin = DataManager.mHeroDict[heroTypeID].MAtkMin;
    //    short mAtkMax = DataManager.mHeroDict[heroTypeID].MAtkMax;
    //    short def = DataManager.mHeroDict[heroTypeID].Def;
    //    short mDef = DataManager.mHeroDict[heroTypeID].MDef;
    //    short hit = DataManager.mHeroDict[heroTypeID].Hit;
    //    short dod = DataManager.mHeroDict[heroTypeID].Dod;
    //    short criR = DataManager.mHeroDict[heroTypeID].CriR;
    //    short criD = DataManager.mHeroDict[heroTypeID].CriD;
    //    short spd = DataManager.mHeroDict[heroTypeID].Spd;
    //    short windDam = DataManager.mHeroDict[heroTypeID].WindDam;
    //    short fireDam = DataManager.mHeroDict[heroTypeID].FireDam ;
    //    short waterDam = DataManager.mHeroDict[heroTypeID].WaterDam ;
    //    short groundDam = DataManager.mHeroDict[heroTypeID].GroundDam ;
    //    short lightDam = DataManager.mHeroDict[heroTypeID].LightDam ;
    //    short darkDam = DataManager.mHeroDict[heroTypeID].DarkDam ;
    //    short windRes = DataManager.mHeroDict[heroTypeID].WindRes ;
    //    short fireRes = DataManager.mHeroDict[heroTypeID].FireRes;
    //    short waterRes = DataManager.mHeroDict[heroTypeID].WaterRes;
    //    short groundRes = DataManager.mHeroDict[heroTypeID].GroundRes;
    //    short lightRes = DataManager.mHeroDict[heroTypeID].LightRes;
    //    short darkRes = DataManager.mHeroDict[heroTypeID].DarkRes;
    //    short dizzyRes = DataManager.mHeroDict[heroTypeID].DizzyRes;
    //    short confusionRes = DataManager.mHeroDict[heroTypeID].ConfusionRes;
    //    short poisonRes = DataManager.mHeroDict[heroTypeID].PoisonRes;
    //    short sleepRes = DataManager.mHeroDict[heroTypeID].SleepRes;
    //    byte goldGet = DataManager.mHeroDict[heroTypeID].GoldGet;
    //    byte expGet = DataManager.mHeroDict[heroTypeID].ExpGet;
    //    byte itemGet = DataManager.mHeroDict[heroTypeID].ItemGet;
    //    byte workPlanting = DataManager.mHeroDict[heroTypeID].WorkPlanting;
    //    byte workFeeding = DataManager.mHeroDict[heroTypeID].WorkFeeding;
    //    byte workFishing = DataManager.mHeroDict[heroTypeID].WorkFishing;
    //    byte workHunting = DataManager.mHeroDict[heroTypeID].WorkHunting;
    //    byte workMining = DataManager.mHeroDict[heroTypeID].WorkMining;
    //    byte workQuarrying = DataManager.mHeroDict[heroTypeID].WorkQuarrying;
    //    byte workFelling = DataManager.mHeroDict[heroTypeID].WorkFelling;
    //    byte workBuild = DataManager.mHeroDict[heroTypeID].WorkBuild;
    //    byte workMakeWeapon = DataManager.mHeroDict[heroTypeID].WorkMakeWeapon;
    //    byte workMakeArmor = DataManager.mHeroDict[heroTypeID].WorkMakeArmor;
    //    byte workMakeJewelry = DataManager.mHeroDict[heroTypeID].WorkMakeJewelry;
    //    byte workSundry = DataManager.mHeroDict[heroTypeID].WorkSundry;


    //    return new HeroObject(heroID,nameSet!=""?nameSet:name, heroTypeID, 1, 0, sexCode, pic, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
    //      windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, goldGet, expGet, itemGet,
    //      workPlanting, workFeeding, workFishing, workHunting, workMining, workQuarrying, workFelling, workBuild, workMakeWeapon, workMakeArmor, workMakeJewelry, workSundry,
    //      -1, -1, -1, -1,-1, -1, -1, -1, -1, -1, new List<int> { -1, -1, -1, -1 }, - 1,-1);

    //}

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
            case Attribute.WorkSundry: rank = DataManager.mHeroDict[heroTypeID].WorkSundry; break;
            default:
                rank = 999; break;
        }

        int probabilityCount = DataManager.cCreateHeroRankDict[attr].Probability.GetLength(1);

        int ran = Random.Range(0, 100);
        int lj = 0;
        for (int i = 0; i < probabilityCount; i++)
        {
            lj += DataManager.cCreateHeroRankDict[attr].Probability[rank, i];

            if (ran < lj)
            {
                return Random.Range(DataManager.cCreateHeroRankDict[attr].Value1[i], DataManager.cCreateHeroRankDict[attr].Value2[i]);
            }
        }

        return 0;
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
            DataManager.mItemDict[itemID].Des + ("于" + timeYear + "年" + timeMonth + "月" + (districtObject != null ? ("在" + districtObject.name + "制作") : "获得")), DataManager.mItemDict[itemID].Cost, districtObject != null ? districtObject.id : (short)-1, false, -1, EquipPart.None);
    }

    public SkillObject GenerateSkillByRandom(short skillID)
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

        return new SkillObject(skillIndex, name, skillID, RateModify, MpModify, ComboRate, ComboMax, Gold, 5000, -1, -1, 0);

    }
    #endregion

    #region 【方法】建筑物建设
    public void BuildDone(short buildingId)
    {
        buildingDic[buildingId].buildProgress = 1;

        //资源生产设施开始自动开工


        AreaMapPanel.Instance.AddIconByBuilding(buildingId);
        if (DistrictMainPanel.Instance.isShow && buildingDic[buildingId].districtID == nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateNatureInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateCultureInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateBuildingInfo(districtDic[nowCheckingDistrictID]);
        }


        //BuildPanel.Instance.UpdateAllInfo(this);
        if (BuildingSelectPanel.Instance.isShow)
        {
            BuildingSelectPanel.Instance.UpdateAllInfo(buildingDic[buildingId].districtID, BuildingSelectPanel.Instance.nowTypePanel, 2);
        }
        MessagePanel.Instance.AddMessage(districtDic[buildingDic[buildingId].districtID].name + "的" + buildingDic[buildingId].name + "建筑完成");

    }

    void StartBuild(int districtID, int buildingID, int needTime)
    {
        //value0:地区实例ID value1:建筑实例ID 
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.Build, standardTime, standardTime + needTime, new List<int> { districtID, buildingID }));
    }
    #endregion

    #region 【方法】建筑物配置生成资源/装备
    void StartProduceResource(int districtID, int buildingID, int needTime, StuffType stuffType, int value)
    {
        //value0:地区实例ID value1:建筑实例ID value2:资源类型 value3:资源数量
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.ProduceResource, standardTime, standardTime + needTime, new List<int> { districtID, buildingID, (int)stuffType, value }));
    }

    public void StopProduceResource(int buildingID)
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < executeEventList.Count; i++)
        {
            if (executeEventList[i].type == ExecuteEventType.ProduceResource && executeEventList[i].value[1] == buildingID)
            {
                tempList.Add(i);
            }
        }
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            executeEventList.RemoveAt(tempList[i]);
        }
        buildingDic[buildingID].produceEquipNow = -1;
        MessagePanel.Instance.AddMessage("接到停工命令，生产停止");
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
        }
    }

    void StartProduceItem(int districtID, int buildingID, int needTime, short produceEquipNow)
    {
        //value0:地区实例ID value1:建筑实例ID value2:装备模板原型ID
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.ProduceItem, standardTime, standardTime + needTime, new List<int> { districtID, buildingID, produceEquipNow }));
    }

    public void StopProduceItem(int buildingID)
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < executeEventList.Count; i++)
        {
            if (executeEventList[i].type == ExecuteEventType.ProduceItem && executeEventList[i].value[1] == buildingID)
            {
                tempList.Add(i);
            }
        }
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            executeEventList.RemoveAt(tempList[i]);
        }
        buildingDic[buildingID].produceEquipNow = -1;
        MessagePanel.Instance.AddMessage("接到停工命令，生产停止");
        if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
        {
            BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
        }
    }

    public void CreateBuildEvent(short BuildingPrototypeID)
    {

        if (DataManager.mBuildingDict[BuildingPrototypeID].NatureGrass > districtDic[nowCheckingDistrictID].totalGrass - districtDic[nowCheckingDistrictID].usedGrass)
        {
            return;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NatureWood > districtDic[nowCheckingDistrictID].totalGrass - districtDic[nowCheckingDistrictID].usedWood)
        {
            return;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NatureWater > districtDic[nowCheckingDistrictID].totalGrass - districtDic[nowCheckingDistrictID].usedWater)
        {
            return;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NatureStone > districtDic[nowCheckingDistrictID].totalStone - districtDic[nowCheckingDistrictID].usedStone)
        {
            return;
        }
        if (DataManager.mBuildingDict[BuildingPrototypeID].NatureMetal > districtDic[nowCheckingDistrictID].totalMetal - districtDic[nowCheckingDistrictID].usedMetal)
        {
            return;
        }

        short buildingId = BuildingPrototypeID;
        List<int> grid = new List<int> { };
        short count = DataManager.mBuildingDict[buildingId].Grid;


        foreach (KeyValuePair<int, DistrictGridObject> kvp in districtGridDic)
        {
            if (DataManager.mDistrictGridDict[kvp.Value.id].DistrictID == nowCheckingDistrictID &&
                DataManager.mDistrictGridDict[kvp.Value.id].Level <= districtDic[nowCheckingDistrictID].level &&
                 kvp.Value.buildingID == -1)
            {
                grid.Add(kvp.Value.id);
                districtGridDic[kvp.Value.id].buildingID = buildingIndex;
                districtGridDic[kvp.Value.id].pic = DataManager.mBuildingDict[buildingId].MapPic;

                count--;
            }
            if (count == 0)
            {
                break;
            }
        }

        for (int i = 0; i < grid.Count; i++)
        {
            districtGridDic[grid[i]].buildingID = buildingIndex;
        }


        districtDic[nowCheckingDistrictID].rStuffWood -= DataManager.mBuildingDict[buildingId].NeedWood;
        districtDic[nowCheckingDistrictID].rStuffStone -= DataManager.mBuildingDict[buildingId].NeedStone;
        districtDic[nowCheckingDistrictID].rStuffMetal -= DataManager.mBuildingDict[buildingId].NeedMetal;
        gold -= DataManager.mBuildingDict[buildingId].NeedGold;

        districtDic[nowCheckingDistrictID].usedGrass += DataManager.mBuildingDict[buildingId].NatureGrass;
        districtDic[nowCheckingDistrictID].usedWood += DataManager.mBuildingDict[buildingId].NatureWood;
        districtDic[nowCheckingDistrictID].usedWater += DataManager.mBuildingDict[buildingId].NatureWater;
        districtDic[nowCheckingDistrictID].usedStone += DataManager.mBuildingDict[buildingId].NatureStone;
        districtDic[nowCheckingDistrictID].usedMetal += DataManager.mBuildingDict[buildingId].NatureMetal;

        districtDic[nowCheckingDistrictID].eWind += DataManager.mBuildingDict[buildingId].EWind;
        districtDic[nowCheckingDistrictID].eFire += DataManager.mBuildingDict[buildingId].EFire;
        districtDic[nowCheckingDistrictID].eWater += DataManager.mBuildingDict[buildingId].EWater;
        districtDic[nowCheckingDistrictID].eGround += DataManager.mBuildingDict[buildingId].EGround;
        districtDic[nowCheckingDistrictID].eLight += DataManager.mBuildingDict[buildingId].ELight;
        districtDic[nowCheckingDistrictID].eDark += DataManager.mBuildingDict[buildingId].EDark;

        districtDic[nowCheckingDistrictID].peopleLimit += DataManager.mBuildingDict[buildingId].People;


        districtDic[nowCheckingDistrictID].gridEmpty -= count;
        districtDic[nowCheckingDistrictID].gridUsed += count;
        districtDic[nowCheckingDistrictID].buildingList.Add(buildingIndex);



        buildingDic.Add(buildingIndex, new BuildingObject(buildingIndex, buildingId, nowCheckingDistrictID, DataManager.mBuildingDict[buildingId].Name, DataManager.mBuildingDict[buildingId].MainPic, DataManager.mBuildingDict[buildingId].MapPic, DataManager.mBuildingDict[buildingId].PanelType, DataManager.mBuildingDict[buildingId].Des, DataManager.mBuildingDict[buildingId].Level, DataManager.mBuildingDict[buildingId].Expense, DataManager.mBuildingDict[buildingId].UpgradeTo, true, grid, new List<int> { },
            DataManager.mBuildingDict[buildingId].NatureGrass, DataManager.mBuildingDict[buildingId].NatureWood, DataManager.mBuildingDict[buildingId].NatureWater, DataManager.mBuildingDict[buildingId].NatureStone, DataManager.mBuildingDict[buildingId].NatureMetal,
            DataManager.mBuildingDict[buildingId].People, DataManager.mBuildingDict[buildingId].Worker, 0,
            DataManager.mBuildingDict[buildingId].EWind, DataManager.mBuildingDict[buildingId].EFire, DataManager.mBuildingDict[buildingId].EWater, DataManager.mBuildingDict[buildingId].EGround, DataManager.mBuildingDict[buildingId].ELight, DataManager.mBuildingDict[buildingId].EDark,
            -1, 0));



        int needTime = DataManager.mBuildingDict[BuildingPrototypeID].BuildTime * 10;
        StartBuild(nowCheckingDistrictID, buildingIndex, needTime);

        buildingIndex++;
        BuildPanel.Instance.UpdateAllInfo(BuildPanel.Instance.nowTypePanel);
        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateResourcesInfo(nowCheckingDistrictID);
    }

    public void CreateProduceItemEvent(int buildingID)
    {
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
            }
        }

        int needTime = (int)(24 * ((float)needLabor / (float)nowLabor));

        StartProduceItem(buildingDic[buildingID].districtID, buildingID, needTime, buildingDic[buildingID].produceEquipNow);
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
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Cereal, num1);
                break;
            case 10://菜田
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputVegetable * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Vegetable, num1);
                break;
            case 11://果园
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputFruit * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Fruit, num1);
                break;
            case 12://亚麻田
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputTwine * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Twine, num1);
                break;
            case 13://牛圈
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMeat * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Meat, num1);
                break;
            case 14://羊圈
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMeat * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Meat, num1);
                num2 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputCloth * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Cloth, num2);
                break;
            case 15://渔场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputFish * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Fish, num1);
                break;

            case 16://伐木场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputWood * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Wood, num1);
                break;
            case 17://伐木场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputWood * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Wood, num1);
                break;
            case 18://伐木场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputWood * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Wood, num1);
                break;

            case 19://矿场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMetal * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Metal, num1);
                break;
            case 20://矿场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMetal * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Metal, num1);
                break;
            case 21://矿场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputMetal * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Metal, num1);
                break;

            case 22://采石场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputStone * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Stone, num1);
                break;
            case 23://采石场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputStone * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Stone, num1);
                break;
            case 24://采石场
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputStone * (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Stone, num1);
                break;
        }
    }

    public void DeleteProduceResourceEvent(int buildingID)
    {
        buildingDic[buildingID].produceEquipNow = -1;
    }

    public bool DistrictItemAdd(short districtID, int buildingID)
    {

        if (GetDistrictProductAll(districtID) >= districtDic[districtID].rProductLimit)
        {
            MessagePanel.Instance.AddMessage("装备库房已满，生产停止");
            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
            }
            return false;
        }
        int moduleID = buildingDic[buildingID].produceEquipNow;

        if (DataManager.mProduceEquipDict[moduleID].InputWood > districtDic[districtID].rStuffWood ||
            DataManager.mProduceEquipDict[moduleID].InputStone > districtDic[districtID].rStuffStone ||
            DataManager.mProduceEquipDict[moduleID].InputMetal > districtDic[districtID].rStuffMetal ||
            DataManager.mProduceEquipDict[moduleID].InputLeather > districtDic[districtID].rStuffLeather ||
            DataManager.mProduceEquipDict[moduleID].InputCloth > districtDic[districtID].rStuffCloth ||
            DataManager.mProduceEquipDict[moduleID].InputTwine > districtDic[districtID].rStuffTwine ||
            DataManager.mProduceEquipDict[moduleID].InputBone > districtDic[districtID].rStuffBone)
        {
            MessagePanel.Instance.AddMessage("原材料不足，生产停止");
            if (BuildingPanel.Instance.isShow && BuildingPanel.Instance.nowCheckingBuildingID == buildingID)
            {
                BuildingPanel.Instance.UpdateOutputInfoPart(buildingDic[buildingID]);
                BuildingPanel.Instance.UpdateTotalSetButton(buildingDic[buildingID]);
            }
            return false;
        }



        //计算依据模板，确定道具原型ID
        int probabilityCount = DataManager.mProduceEquipDict[moduleID].OutputRate.Count;
        int ran = Random.Range(0, 100);
        int lj = 0;
        int itemID = -1;
        for (int i = 0; i < probabilityCount; i++)
        {
            lj += DataManager.mProduceEquipDict[moduleID].OutputRate[i];

            if (ran < lj)
            {
                itemID = DataManager.mProduceEquipDict[moduleID].OutputID[i];
                break;
            }
        }
        Debug.Log("DistrictItemAdd() 生产 " + DataManager.mItemDict[itemID].Name);


        districtDic[districtID].rStuffWood -= DataManager.mProduceEquipDict[moduleID].InputWood;
        districtDic[districtID].rStuffStone -= DataManager.mProduceEquipDict[moduleID].InputStone;
        districtDic[districtID].rStuffMetal -= DataManager.mProduceEquipDict[moduleID].InputMetal;
        districtDic[districtID].rStuffLeather -= DataManager.mProduceEquipDict[moduleID].InputLeather;
        districtDic[districtID].rStuffCloth -= DataManager.mProduceEquipDict[moduleID].InputCloth;
        districtDic[districtID].rStuffTwine -= DataManager.mProduceEquipDict[moduleID].InputTwine;
        districtDic[districtID].rStuffBone -= DataManager.mProduceEquipDict[moduleID].InputBone;


        itemDic.Add(itemIndex, GenerateItemByRandom(itemID, districtDic[districtID], buildingDic[buildingID].heroList));
        itemIndex++;

        switch (DataManager.mItemDict[itemID].TypeBig)
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

        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.UpdateList(districtID, 1);
        }
        if (districtID == nowCheckingDistrictID)
        {
            PlayMainPanel.Instance.UpdateResourcesInfo(districtID);
        }
        CreateLog(LogType.ProduceDone, "", new List<int> { districtID, buildingID, itemID });
        return true;
        // itemDic.Add(GenerateItemByRandom(, districtDic[districtID],));
    }

    public bool DistrictResourceAdd(short districtID, int buildingID, StuffType stuffType, int value)
    {

        Debug.Log("DistrictResourceAdd() " + districtID + " " + stuffType + " " + value);
        switch (stuffType)
        {
            case StuffType.Cereal:
            case StuffType.Vegetable:
            case StuffType.Fruit:
            case StuffType.Meat:
            case StuffType.Fish:
                if (GetDistrictFoodAll(districtID) >= districtDic[districtID].rFoodLimit)
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
                if (GetDistrictStuffAll(districtID) >= districtDic[districtID].rStuffLimit)
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


        switch (stuffType)
        {
            case StuffType.Cereal: districtDic[districtID].rFoodCereal += value; break;
            case StuffType.Vegetable: districtDic[districtID].rFoodVegetable += value; break;
            case StuffType.Fruit: districtDic[districtID].rFoodFruit += value; break;
            case StuffType.Meat: districtDic[districtID].rFoodMeat += value; break;
            case StuffType.Fish: districtDic[districtID].rFoodFish += value; break;
            case StuffType.Wood: districtDic[districtID].rStuffWood += value; break;
            case StuffType.Stone: districtDic[districtID].rStuffStone += value; break;
            case StuffType.Metal: districtDic[districtID].rStuffMetal += value; break;
            case StuffType.Leather: districtDic[districtID].rStuffLeather += value; break;
            case StuffType.Cloth: districtDic[districtID].rStuffCloth += value; break;
            case StuffType.Twine: districtDic[districtID].rStuffTwine += value; break;
            case StuffType.Bone: districtDic[districtID].rStuffBone += value; break;
        }

        Debug.Log("AAAAADistrictResourceAdd() ");
        if (DistrictMainPanel.Instance.isShow)
        {
            DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[districtID]);
        }
        if (districtID == nowCheckingDistrictID)
        {
            PlayMainPanel.Instance.UpdateResourcesInfo(districtID);
        }
        return true;
    }

    public void ChangeProduceEquipNow(int buildingID)
    {
        Debug.Log("ChangeProduceEquipNow() buildingID=" + buildingID + " setForgeType=" + BuildingPanel.Instance.setForgeType + " setForgeLevel" + BuildingPanel.Instance.setForgeLevel);
        bool needStart = (buildingDic[buildingID].produceEquipNow == -1);

        foreach (KeyValuePair<int, ProduceEquipPrototype> kvp in DataManager.mProduceEquipDict)
        {
            if (kvp.Value.MakePlace.Contains((byte)buildingDic[buildingID].prototypeID) && kvp.Value.OptionValue == BuildingPanel.Instance.setForgeType && kvp.Value.Level == (BuildingPanel.Instance.setForgeLevel + 1))
            {
                buildingDic[buildingID].produceEquipNow = kvp.Value.ID;
                Debug.Log("kvp.Value.ID=" + kvp.Value.ID);
                break;
            }
        }
        Debug.Log("buildingDic[buildingID].produceEquipNow=" + buildingDic[buildingID].produceEquipNow);
        if (needStart)
        {
            CreateProduceItemEvent(buildingID);
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

    #region 【方法】英雄装备/卸下，技能配置
    public void HeroEquipSet(int heroID, EquipPart equipPart, int itemID)
    {
        // Debug.Log("HeroEquipSet() heroID=" + heroID + " equipPart=" + equipPart + " itemID="+ itemID+ " heroDic[heroID].equipWeapon=" + heroDic[heroID].equipWeapon);

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
    }

    public void HeroEquipUnSet(int heroID, EquipPart equipPart)
    {
        // Debug.Log("HeroEquipUnSet() heroID=" + heroID + " equipPart=" + equipPart+ " heroDic[heroID].equipWeapon="+ heroDic[heroID].equipWeapon);

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

        HeroPanel.Instance.UpdateEquip(heroDic[heroID], equipPart);
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.UpdateAllInfoToEquip(equipPart);
        }
    }

    public void HeroSkillSet(int heroID, byte index, int skillID)
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

        HeroPanel.Instance.UpdateSkill(heroDic[heroID], index);
        SkillListAndInfoPanel.Instance.OnHide();
    }
    #endregion

    #region 【方法】鉴定库转收藏/放售
    public void ItemToCollectionAll(short districtID)
    {
        foreach (KeyValuePair<int, ItemObject> kvp in itemDic)
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
            }
        }
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnShow(districtID, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
    }

    public void ItemToCollection(int itemID)
    {
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
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnShow(districtID, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
    }

    public void ItemToGoodsAll(short districtID)
    {
        foreach (KeyValuePair<int, ItemObject> kvp in itemDic)
        {
            if (kvp.Value.districtID == districtID)
            {
                kvp.Value.isGoods = true;
            }
        }
        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnShow(districtID, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
    }

    public void ItemToGoods(int itemID)
    {
        itemDic[itemID].isGoods = true;

        if (ItemListAndInfoPanel.Instance.isShow)
        {
            ItemListAndInfoPanel.Instance.OnShow(itemDic[itemID].districtID, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.x, (int)ItemListAndInfoPanel.Instance.transform.GetComponent<RectTransform>().anchoredPosition.y, 1);
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

    public void AdventureTeamHeroMinus(byte teamID, int heroID)
    {
        if (!adventureTeamList[teamID].heroIDList.Contains(heroID))
        {
            return;
        }
        int index = adventureTeamList[teamID].heroIDList.FindIndex(item => item.Equals(heroID));

        adventureTeamList[teamID].heroIDList.RemoveAt(index);
        adventureTeamList[teamID].heroHpList.RemoveAt(index);
        adventureTeamList[teamID].heroMpList.RemoveAt(index);
        //adventureTeamList[teamID].heroIDList.
        heroDic[heroID].adventureInTeam = -1;
        AdventureMainPanel.Instance.UpdateTeamHero(teamID);
        AdventureMainPanel.Instance.TeamLogAdd(teamID, heroDic[heroID].name + "离开了队伍");
    }

    public void AdventureTeamHeroAdd(byte teamID, int heroID)
    {
        if (adventureTeamList[teamID].heroIDList.Count >= 3)
        {
            return;
        }
        if (heroDic[heroID].adventureInTeam != -1)
        {
            return;
        }
        adventureTeamList[teamID].heroIDList.Add(heroID);
        adventureTeamList[teamID].heroHpList.Add(GetHeroAttr(Attribute.Hp, heroID));
        adventureTeamList[teamID].heroMpList.Add(GetHeroAttr(Attribute.Mp, heroID));
        heroDic[heroID].adventureInTeam = teamID;
        AdventureMainPanel.Instance.UpdateTeamHero(teamID);
        AdventureMainPanel.Instance.TeamLogAdd(teamID, heroDic[heroID].name + "加入了队伍");
    }

    public void AdventureTeamSend(byte teamID)
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

        adventureTeamList[teamID].state = AdventureState.Doing;
        adventureTeamList[teamID].action = AdventureAction.Walk;
        AdventureMainPanel.Instance.UpdateTeam(teamID);
        AdventureMainPanel.Instance.UpdateSceneRole(teamID);

        AdventureEventHappen(teamID);
    }

    public void AdventureEventHappen(byte teamID)
    {
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.Adventure, standardTime, standardTime + 240, new List<int> { teamID }));
    }

    public void AdventureFight(byte teamID)
    {
        const int RoundLimit= 50;
        List<FightMenberObject> fightMenberObjects = new List<FightMenberObject>();
        //读取队伍成员和怪物列表，转化为战斗成员实例
        for (int i = 0; i < adventureTeamList[teamID].heroIDList.Count; i++)
        {
            int heroID = adventureTeamList[teamID].heroIDList[i];

            int id = i;
            int objectID = heroID;//对于己方，heroID
            byte side = 0;
            string name = heroDic[heroID].name;
            int hp = GetHeroAttr(Attribute.Hp, heroID);
            int mp = GetHeroAttr(Attribute.Mp, heroID);
            short hpRenew = (short)GetHeroAttr(Attribute.HpRenew, heroID);
            short mpRenew = (short)GetHeroAttr(Attribute.MpRenew, heroID);
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
            short actionBar = 0;
            byte skillIndex = 0;//当前招式位置
            int hpNow = adventureTeamList[teamID].heroHpList[i];
            int mpNow = adventureTeamList[teamID].heroMpList[i];
            List<FightBuff> buff = new List<FightBuff> { };
            fightMenberObjects.Add(new FightMenberObject(id, objectID, side, name, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
                                                            windDam, fireDam, waterDam, groundDam, lightDam, darkDam,windRes, fireRes, waterRes, groundRes, lightRes, darkRes,dizzyRes, confusionRes, poisonRes, sleepRes,
                                                            actionBar, skillIndex, hpNow, mpNow, buff));
        }

        //测试创建怪物ID列表
        List<int> enemyIDList = new List<int> { 0, 0, 0 };


        for (int i = 0; i < enemyIDList.Count; i++)
        {
            int monsterID = enemyIDList[i];

            int id = i+ adventureTeamList[teamID].heroIDList.Count;
            int objectID = monsterID;//对于敌方，原型
            byte side = 1;
            string name = DataManager.mMonsterDict[monsterID].Name;
            int hp = DataManager.mMonsterDict[monsterID].Hp;
            int mp = DataManager.mMonsterDict[monsterID].Mp;
            short hpRenew =  DataManager.mMonsterDict[monsterID].HpRenew;
            short mpRenew =  DataManager.mMonsterDict[monsterID].MpRenew;
            short atkMin =  DataManager.mMonsterDict[monsterID].AtkMin;
            short atkMax =  DataManager.mMonsterDict[monsterID].AtkMax;
            short mAtkMin =  DataManager.mMonsterDict[monsterID].MAtkMin;
            short mAtkMax =  DataManager.mMonsterDict[monsterID].MAtkMax;
            short def =  DataManager.mMonsterDict[monsterID].Def;
            short mDef =  DataManager.mMonsterDict[monsterID].MDef;
            short hit =  DataManager.mMonsterDict[monsterID].Hit;
            short dod =  DataManager.mMonsterDict[monsterID].Dod;
            short criR =  DataManager.mMonsterDict[monsterID].CriR;
            short criD =  DataManager.mMonsterDict[monsterID].CriD;
            short spd =  DataManager.mMonsterDict[monsterID].Spd;
            short windDam =  DataManager.mMonsterDict[monsterID].WindDam;
            short fireDam =  DataManager.mMonsterDict[monsterID].FireDam;
            short waterDam =  DataManager.mMonsterDict[monsterID].WaterDam;
            short groundDam =  DataManager.mMonsterDict[monsterID].GroundDam;
            short lightDam =  DataManager.mMonsterDict[monsterID].LightDam;
            short darkDam =  DataManager.mMonsterDict[monsterID].DarkDam;
            short windRes =  DataManager.mMonsterDict[monsterID].WindRes;
            short fireRes =  DataManager.mMonsterDict[monsterID].FireRes;
            short waterRes =  DataManager.mMonsterDict[monsterID].WaterRes;
            short groundRes =  DataManager.mMonsterDict[monsterID].GroundRes;
            short lightRes =  DataManager.mMonsterDict[monsterID].LightRes;
            short darkRes =  DataManager.mMonsterDict[monsterID].DarkRes;
            short dizzyRes =  DataManager.mMonsterDict[monsterID].DizzyRes;
            short confusionRes =  DataManager.mMonsterDict[monsterID].ConfusionRes;
            short poisonRes =  DataManager.mMonsterDict[monsterID].PoisonRes;
            short sleepRes =  DataManager.mMonsterDict[monsterID].SleepRes;
            short actionBar = 0;
            byte skillIndex = 0;//当前招式位置
            int hpNow = hp;
            int mpNow =mp;
            List<FightBuff> buff = new List<FightBuff> { };

            fightMenberObjects.Add(new FightMenberObject(id, objectID, side, name, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
                                                      windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes,
                                                      actionBar, skillIndex, hpNow, mpNow, buff));
        }


        int round = 0;

        List<FightMenberObject> actionMenber = new List<FightMenberObject>();

        while(round< RoundLimit)
        {
            //选取行动槽满的战斗成员
            while (actionMenber.Count == 0)
            {
                for (int i = 0; i < fightMenberObjects.Count; i++)
                {
                    if (fightMenberObjects[i].hpNow > 0)//眩晕与睡眠待写
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
                //buff计算待写（减掉回合数）


                if(actionMenber[i].hpNow>0)
                { 
                byte skillIndex = actionMenber[i].skillIndex;
                int skillID = heroDic[actionMenber[i].objectID].skill[skillIndex];
                SkillObject so = skillDic[skillID];
                SkillPrototype sp = DataManager.mSkillDict[so.prototypeID];
                if (skillID != -1)
                {
                    if (GetSkillMpCost(skillID) <= actionMenber[i].mpNow)
                    {
                        int ran = Random.Range(0, 100);
                        if (ran < GetSkillProbability(skillID))
                        {
                            //选取目标
                            List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], sp);


                            //对目标行动
                            for (int j = 0; j < targetMenber.Count; j++)
                            {

                                if (sp.FlagDamage)
                                {

                                    int hitRate = (int)((float)actionMenber[i].hit / (actionMenber[i].hit + targetMenber[j].dod) * 100);
                                    int ranHit = Random.Range(0, 100);
                                    if (ranHit < hitRate)
                                    {
                                        int damageMin = System.Math.Max(0, (int)(actionMenber[i].atkMin * (sp.Atk / 100f)) + (int)(actionMenber[i].mAtkMin * (sp.MAtk / 100f)) - targetMenber[j].def);
                                        int damageMax = System.Math.Max(0, (int)(actionMenber[i].atkMax * (sp.Atk / 100f)) + (int)(actionMenber[i].mAtkMax * (sp.MAtk / 100f)) - targetMenber[j].mDef);

                                        if (sp.Sword != 0 && heroDic[actionMenber[i].objectID].equipWeapon != -1)
                                        {
                                            if (DataManager.mItemDict[itemDic[heroDic[actionMenber[i].objectID].equipWeapon].prototypeID].TypeSmall == ItemTypeSmall.Sword)
                                            {
                                                damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                                                damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                                            }
                                        }
                                        if (sp.Axe != 0 && heroDic[actionMenber[i].objectID].equipWeapon != -1)
                                        {
                                            if (DataManager.mItemDict[itemDic[heroDic[actionMenber[i].objectID].equipWeapon].prototypeID].TypeSmall == ItemTypeSmall.Axe)
                                            {
                                                damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                                                damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                                            }
                                        }
                                        if (sp.Spear != 0 && heroDic[actionMenber[i].objectID].equipWeapon != -1)
                                        {
                                            if (DataManager.mItemDict[itemDic[heroDic[actionMenber[i].objectID].equipWeapon].prototypeID].TypeSmall == ItemTypeSmall.Spear)
                                            {
                                                damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                                                damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                                            }
                                        }
                                        if (sp.Hammer != 0 && heroDic[actionMenber[i].objectID].equipWeapon != -1)
                                        {
                                            if (DataManager.mItemDict[itemDic[heroDic[actionMenber[i].objectID].equipWeapon].prototypeID].TypeSmall == ItemTypeSmall.Hammer)
                                            {
                                                damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                                                damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                                            }
                                        }
                                        if (sp.Bow != 0 && heroDic[actionMenber[i].objectID].equipWeapon != -1)
                                        {
                                            if (DataManager.mItemDict[itemDic[heroDic[actionMenber[i].objectID].equipWeapon].prototypeID].TypeSmall == ItemTypeSmall.Bow)
                                            {
                                                damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                                                damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                                            }
                                        }
                                        if (sp.Staff != 0 && heroDic[actionMenber[i].objectID].equipWeapon != -1)
                                        {
                                            if (DataManager.mItemDict[itemDic[heroDic[actionMenber[i].objectID].equipWeapon].prototypeID].TypeSmall == ItemTypeSmall.Staff)
                                            {
                                                damageMin = (int)(damageMin * (1f + (sp.Sword / 100f)));
                                                damageMax = (int)(damageMax * (1f + (sp.Sword / 100f)));
                                            }
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
                                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].windDam + sp.Wind - targetMenber[j].windRes) / 100f)));
                                            }
                                            if (sp.Element.Contains(2))
                                            {
                                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].fireDam + sp.Fire - targetMenber[j].fireRes) / 100f)));
                                            }
                                            if (sp.Element.Contains(3))
                                            {
                                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].waterDam + sp.Water - targetMenber[j].waterRes) / 100f)));
                                            }
                                            if (sp.Element.Contains(4))
                                            {
                                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].groundDam + sp.Ground - targetMenber[j].groundRes) / 100f)));
                                            }
                                            if (sp.Element.Contains(5))
                                            {
                                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].lightDam + sp.Light - targetMenber[j].lightRes) / 100f)));
                                            }
                                            if (sp.Element.Contains(6))
                                            {
                                                damageWithElement += System.Math.Max(0, (int)(damage * (1f + (actionMenber[i].darkDam + sp.Dark - targetMenber[j].darkRes) / 100f)));
                                            }
                                        }

                                        int ranCri = Random.Range(0, 100);
                                        if (ranCri < actionMenber[i].criR)
                                        {
                                            damageWithElement = (int)(damageWithElement * (actionMenber[i].criD / 100f));
                                        }

                                        //造成伤害
                                        targetMenber[j].hp -= damageWithElement;
                                        if (targetMenber[j].hp < 0)
                                        {
                                            targetMenber[j].hp = 0;
                                        }
                                        AdventureMainPanel.Instance.TeamLogAdd(teamID, actionMenber[i].name + "使用" + sp.Name + "对" + targetMenber[j].name + "造成" + damageWithElement + "点伤害");
                                    }
                                    else//未命中
                                    {
                                        AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "避开了" + actionMenber[i].name + "的攻击");
                                    }
                                }

                                if (sp.FlagDebuff)
                                {
                                    if (sp.Dizzy != 0)
                                    {
                                        int hitRate = System.Math.Max(0, sp.Dizzy - targetMenber[j].dizzyRes);
                                        int ranHit = Random.Range(0, 100);
                                        if (ranHit < hitRate)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.Dizzy, 0, (byte)sp.DizzyValue));
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "眩晕了");
                                        }
                                        else
                                        {
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, "眩晕效果未生效");
                                        }
                                    }
                                    if (sp.Confusion != 0)
                                    {
                                        int hitRate = System.Math.Max(0, sp.Confusion - targetMenber[j].confusionRes);
                                        int ranHit = Random.Range(0, 100);
                                        if (ranHit < hitRate)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.Confusion, 0, (byte)sp.ConfusionValue));
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "混乱了");
                                        }
                                        else
                                        {
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, "混乱效果未生效");
                                        }
                                    }
                                    if (sp.Sleep != 0)
                                    {
                                        int hitRate = System.Math.Max(0, sp.Sleep - targetMenber[j].sleepRes);
                                        int ranHit = Random.Range(0, 100);
                                        if (ranHit < hitRate)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.Sleep, 0, (byte)sp.SleepValue));
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "睡眠了");
                                        }
                                        else
                                        {
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, "睡眠效果未生效");
                                        }
                                    }
                                    if (sp.Poison != 0)
                                    {
                                        int hitRate = System.Math.Max(0, sp.Poison - targetMenber[j].poisonRes);
                                        int ranHit = Random.Range(0, 100);
                                        if (ranHit < hitRate)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.Poison, 0, (byte)sp.PoisonValue));
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "中毒了");
                                        }
                                        else
                                        {
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, "中毒效果未生效");
                                        }
                                    }
                                }

                                if (sp.Cure != 0)
                                {
                                    targetMenber[j].hpNow += (int)(targetMenber[j].hp * (sp.Cure / 100f));
                                    if (targetMenber[j].hpNow > targetMenber[j].hp)
                                    {
                                        targetMenber[j].hpNow = targetMenber[j].hp;
                                    }
                                    AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "体力恢复了" + (int)(targetMenber[j].hp * (sp.Cure / 100f)));
                                }

                                if (sp.FlagBuff)
                                {
                                    if (sp.UpWindDam != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpWindDam) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWindDam, (byte)sp.UpWindDam, 2));
                                            targetMenber[j].windDam += sp.UpWindDam;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "风系伤害提升了");
                                        }
                                    }
                                    if (sp.UpFireDam != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpFireDam) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpFireDam, (byte)sp.UpFireDam, 2));
                                            targetMenber[j].fireDam += sp.UpFireDam;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "火系伤害提升了");
                                        }
                                    }
                                    if (sp.UpWaterDam != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpWaterDam) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWaterDam, (byte)sp.UpWaterDam, 2));
                                            targetMenber[j].waterDam += sp.UpWaterDam;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "水系伤害提升了");
                                        }
                                    }
                                    if (sp.UpGroundDam != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpGroundDam) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpGroundDam, (byte)sp.UpGroundDam, 2));
                                            targetMenber[j].groundDam += sp.UpGroundDam;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "地系伤害提升了");
                                        }
                                    }
                                    if (sp.UpLightDam != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpLightDam) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpLightDam, (byte)sp.UpLightDam, 2));
                                            targetMenber[j].lightDam += sp.UpLightDam;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "光系伤害提升了");
                                        }
                                    }
                                    if (sp.UpDarkDam != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpDarkDam) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDarkDam, (byte)sp.UpDarkDam, 2));
                                            targetMenber[j].darkDam += sp.UpDarkDam;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "暗系伤害提升了");
                                        }
                                    }

                                    if (sp.UpWindRes != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpWindRes) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWindRes, (byte)sp.UpWindRes, 2));
                                            targetMenber[j].windRes += sp.UpWindRes;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "风系抗性提升了");
                                        }
                                    }
                                    if (sp.UpFireRes != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpFireRes) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpFireRes, (byte)sp.UpFireRes, 2));
                                            targetMenber[j].fireRes += sp.UpFireRes;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "火系抗性提升了");
                                        }
                                    }
                                    if (sp.UpWaterRes != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpWaterRes) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpWaterRes, (byte)sp.UpWaterRes, 2));
                                            targetMenber[j].waterRes += sp.UpWaterRes;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "水系抗性提升了");
                                        }
                                    }
                                    if (sp.UpGroundRes != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpGroundRes) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpGroundRes, (byte)sp.UpGroundRes, 2));
                                            targetMenber[j].groundRes += sp.UpGroundRes;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "地系抗性提升了");
                                        }
                                    }
                                    if (sp.UpLightRes != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpLightRes) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpLightRes, (byte)sp.UpLightRes, 2));
                                            targetMenber[j].lightRes += sp.UpLightRes;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "光系抗性提升了");
                                        }
                                    }
                                    if (sp.UpDarkRes != 0)
                                    {
                                        bool buffExist = false;
                                        for (int k = 0; k < targetMenber[j].buff.Count; k++)
                                        {
                                            if (targetMenber[j].buff[k].type == FightBuffType.UpDarkRes) { buffExist = true; break; }
                                        }
                                        if (!buffExist)
                                        {
                                            targetMenber[j].buff.Add(new FightBuff(FightBuffType.UpDarkRes, (byte)sp.UpDarkRes, 2));
                                            targetMenber[j].darkRes += sp.UpDarkRes;
                                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[j].name + "暗系抗性提升了");
                                        }
                                    }


                                }

                            }
                        }
                        else//技能概率未触发，普通攻击
                        {
                            //选取目标
                            List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], null);
                            int hitRate = (int)((float)actionMenber[i].hit / (actionMenber[i].hit + targetMenber[0].dod) * 100);
                            int ranHit = Random.Range(0, 100);
                            if (ranHit < hitRate)
                            {
                                int damageMin = System.Math.Max(0, (int)(actionMenber[i].atkMin * (sp.Atk / 100f)) + (int)(actionMenber[i].mAtkMin * (sp.MAtk / 100f)) - targetMenber[0].def);
                                int damageMax = System.Math.Max(0, (int)(actionMenber[i].atkMax * (sp.Atk / 100f)) + (int)(actionMenber[i].mAtkMax * (sp.MAtk / 100f)) - targetMenber[0].mDef);
                                int damage = Random.Range(damageMin, damageMax + 1);
                                int ranCri = Random.Range(0, 100);
                                if (ranCri < actionMenber[i].criR)
                                {
                                    damage = (int)(damage * (actionMenber[i].criD / 100f));
                                }

                                //造成伤害
                                targetMenber[0].hp -= damage;
                                if (targetMenber[0].hp < 0)
                                {
                                    targetMenber[0].hp = 0;
                                }
                                AdventureMainPanel.Instance.TeamLogAdd(teamID, actionMenber[i].name + "普通攻击对" + targetMenber[0].name + "造成" + damage + "点伤害");
                            }
                            else//未命中
                            {
                                AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[0].name + "避开了" + actionMenber[i].name + "的攻击");
                            }
                        }

                    }
                    else//MP不足，普通攻击
                    {
                        //选取目标
                        List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], null);
                        int hitRate = (int)((float)actionMenber[i].hit / (actionMenber[i].hit + targetMenber[0].dod) * 100);
                        int ranHit = Random.Range(0, 100);
                        if (ranHit < hitRate)
                        {
                            int damageMin = System.Math.Max(0, (int)(actionMenber[i].atkMin * (sp.Atk / 100f)) + (int)(actionMenber[i].mAtkMin * (sp.MAtk / 100f)) - targetMenber[0].def);
                            int damageMax = System.Math.Max(0, (int)(actionMenber[i].atkMax * (sp.Atk / 100f)) + (int)(actionMenber[i].mAtkMax * (sp.MAtk / 100f)) - targetMenber[0].mDef);
                            int damage = Random.Range(damageMin, damageMax + 1);
                            int ranCri = Random.Range(0, 100);
                            if (ranCri < actionMenber[i].criR)
                            {
                                damage = (int)(damage * (actionMenber[i].criD / 100f));
                            }

                            //造成伤害
                            targetMenber[0].hp -= damage;
                            if (targetMenber[0].hp < 0)
                            {
                                targetMenber[0].hp = 0;
                            }
                            AdventureMainPanel.Instance.TeamLogAdd(teamID, actionMenber[i].name + "普通攻击对" + targetMenber[0].name + "造成" + damage + "点伤害");
                        }
                        else//未命中
                        {
                            AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[0].name + "避开了" + actionMenber[i].name + "的攻击");
                        }
                    }

                }
                else//普通攻击
                {
                    //选取目标
                    List<FightMenberObject> targetMenber = GetTargetManbers(fightMenberObjects, actionMenber[i], null);
                    int hitRate = (int)((float)actionMenber[i].hit / (actionMenber[i].hit + targetMenber[0].dod) * 100);
                    int ranHit = Random.Range(0, 100);
                    if (ranHit < hitRate)
                    {
                        int damageMin = System.Math.Max(0, (int)(actionMenber[i].atkMin * (sp.Atk / 100f)) + (int)(actionMenber[i].mAtkMin * (sp.MAtk / 100f)) - targetMenber[0].def);
                        int damageMax = System.Math.Max(0, (int)(actionMenber[i].atkMax * (sp.Atk / 100f)) + (int)(actionMenber[i].mAtkMax * (sp.MAtk / 100f)) - targetMenber[0].mDef);
                        int damage = Random.Range(damageMin, damageMax + 1);
                        int ranCri = Random.Range(0, 100);
                        if (ranCri < actionMenber[i].criR)
                        {
                            damage = (int)(damage * (actionMenber[i].criD / 100f));
                        }

                        //造成伤害
                        targetMenber[0].hp -= damage;
                        if (targetMenber[0].hp < 0)
                        {
                            targetMenber[0].hp = 0;
                        }
                        AdventureMainPanel.Instance.TeamLogAdd(teamID, actionMenber[i].name + "普通攻击对" + targetMenber[0].name + "造成" + damage + "点伤害");
                    }
                    else//未命中
                    {
                        AdventureMainPanel.Instance.TeamLogAdd(teamID, targetMenber[0].name + "避开了" + actionMenber[i].name + "的攻击");
                    }
                }
                    
                    round++;
                    
                }  
            }

            actionMenber.Clear();
            int CheckFightOverResult = CheckFightOver(fightMenberObjects);
            if (CheckFightOverResult != -1)
            {
                if (CheckFightOverResult == 0)
                {
                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "战斗胜利！");
                }
                else if (CheckFightOverResult == 1)
                {
                    AdventureMainPanel.Instance.TeamLogAdd(teamID, "被击败了！");
                }
                break;
            }
        }
        if (round > RoundLimit)
        {
            AdventureMainPanel.Instance.TeamLogAdd(teamID, "超过"+ RoundLimit + "回合,战斗失败！");
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
        List<FightMenberObject> targetMenber = new List<FightMenberObject>();
        int maxCount;
        if (sp != null)
        {
            maxCount = sp.Max;
            if (sp.FlagDamage || sp.FlagDebuff)
            {

                if (actionMenber.side == 0)
                {
                    for (int j = 0; j < fightMenberObjects.Count; j++)
                    {
                        if (fightMenberObjects[j].side == 1)
                        {
                            if (fightMenberObjects[j].hpNow > 0)
                            {
                                if (maxCount > 0)
                                {
                                    targetMenber.Add(fightMenberObjects[j]);
                                }
                                maxCount--;
                            }

                        }
                    }
                }
                else if (actionMenber.side == 1)
                {
                    for (int j = 0; j < fightMenberObjects.Count; j++)
                    {
                        if (fightMenberObjects[j].side == 0)
                        {
                            if (fightMenberObjects[j].hpNow > 0)
                            {
                                if (maxCount > 0)
                                {
                                    targetMenber.Add(fightMenberObjects[j]);
                                }
                                maxCount--;
                            }
                        }
                    }
                }
            }
            else if (sp.Cure != 0 || sp.FlagBuff)
            {
                if (actionMenber.side == 0)
                {
                    for (int j = 0; j < fightMenberObjects.Count; j++)
                    {
                        if (fightMenberObjects[j].side == 0)
                        {
                            if (fightMenberObjects[j].hpNow > 0)
                            {
                                if (maxCount > 0)
                                {
                                    targetMenber.Add(fightMenberObjects[j]);
                                }
                                maxCount--;
                            }

                        }
                    }
                }
                else if (actionMenber.side == 1)
                {
                    for (int j = 0; j < fightMenberObjects.Count; j++)
                    {
                        if (fightMenberObjects[j].side == 1)
                        {
                            if (fightMenberObjects[j].hpNow > 0)
                            {
                                if (maxCount > 0)
                                {
                                    targetMenber.Add(fightMenberObjects[j]);
                                }
                                maxCount--;
                            }
                        }
                    }
                }
            }

        }
        else
        {
            if (actionMenber.side == 0)
            {
                for (int j = 0; j < fightMenberObjects.Count; j++)
                {
                    if (fightMenberObjects[j].side == 1)
                    {
                        if (fightMenberObjects[j].hpNow > 0)
                        {
                            targetMenber.Add(fightMenberObjects[j]);
                            break;
                        }
                    }
                }
            }
            else if (actionMenber.side == 1)
            {
                for (int j = 0; j < fightMenberObjects.Count; j++)
                {
                    if (fightMenberObjects[j].side == 0)
                    {
                        if (fightMenberObjects[j].hpNow > 0)
                        {
                            targetMenber.Add(fightMenberObjects[j]);
                            break;
                        }
                    }
                }
            }
        }
        return targetMenber;
    }


    #endregion

    #region 【方法】日志与执行事件基础
    public void CreateLog(LogType logType,string text, List<int> value)
    {
        //LogType.ProduceDone(地区实例ID,建筑实例ID,物品原型ID)
        logDic.Add(logIndex, new LogObject(logIndex, logType,standardTime, text, value));
        logIndex++;
        //MessagePanel.Instance.AddMessage(logDic[logIndex - 1]);
    }

    //添加执行事件，遍历确定插入位置
    public void ExecuteEventAdd(ExecuteEventObject executeEventObject)
    {
        Debug.Log("ExecuteEventAdd() executeEventObject=" + executeEventObject.startTime);
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

    #region 【辅助方法集】获取值
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

    public int GetDistrictFoodAll(int districtID)
    {
        return districtDic[districtID].rFoodCereal + districtDic[districtID].rFoodVegetable + districtDic[districtID].rFoodFruit + districtDic[districtID].rFoodMeat + districtDic[districtID].rFoodFish;
    }

    public int GetDistrictStuffAll(int districtID)
    {
        return districtDic[districtID].rStuffWood + districtDic[districtID].rStuffStone + districtDic[districtID].rStuffMetal + districtDic[districtID].rStuffLeather + districtDic[districtID].rStuffCloth + districtDic[districtID].rStuffTwine + districtDic[districtID].rStuffBone;
    }
    
    public int GetDistrictProductAll(int districtID)
    {
        return districtDic[districtID].rProductWeapon + districtDic[districtID].rProductArmor + districtDic[districtID].rProductJewelry;
    }

    public float GetProduceResourceLaborRate(int buildingID)
    {
      return  Mathf.Pow(buildingDic[buildingID].workerNow, DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].LaborRate);
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
    public int GetHeroAttr(Attribute attribute,int heroID)
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
            case Attribute.Hp: return heroDic[heroID].hp+ equipAdd;
            case Attribute.Mp: return heroDic[heroID].mp+ equipAdd;
            case Attribute.HpRenew: return heroDic[heroID].hpRenew+ equipAdd;
            case Attribute.MpRenew: return heroDic[heroID].mpRenew+ equipAdd;
            case Attribute.AtkMin: return heroDic[heroID].atkMin+ equipAdd;
            case Attribute.AtkMax: return heroDic[heroID].atkMax+ equipAdd;
            case Attribute.MAtkMin: return heroDic[heroID].mAtkMin+ equipAdd;
            case Attribute.MAtkMax: return heroDic[heroID].mAtkMax+ equipAdd;
            case Attribute.Def: return heroDic[heroID].def+ equipAdd;
            case Attribute.MDef: return heroDic[heroID].mDef+ equipAdd;
            case Attribute.Hit: return heroDic[heroID].hit+ equipAdd;
            case Attribute.Dod: return heroDic[heroID].dod+ equipAdd;
            case Attribute.CriR: return heroDic[heroID].criR+ equipAdd;
            case Attribute.CriD: return heroDic[heroID].criD+ equipAdd;
            case Attribute.Spd: return heroDic[heroID].spd+ equipAdd;
            case Attribute.WindDam: return heroDic[heroID].windDam+ equipAdd;
            case Attribute.FireDam: return heroDic[heroID].fireDam+ equipAdd;
            case Attribute.WaterDam: return heroDic[heroID].waterDam+ equipAdd;
            case Attribute.GroundDam: return heroDic[heroID].groundDam+ equipAdd;
            case Attribute.LightDam: return heroDic[heroID].lightDam+ equipAdd;
            case Attribute.DarkDam: return heroDic[heroID].darkDam+ equipAdd;
            case Attribute.WindRes: return heroDic[heroID].windRes+ equipAdd;
            case Attribute.FireRes: return heroDic[heroID].fireRes+ equipAdd;
            case Attribute.WaterRes: return heroDic[heroID].waterRes+ equipAdd;
            case Attribute.GroundRes: return heroDic[heroID].groundRes+ equipAdd;
            case Attribute.LightRes: return heroDic[heroID].lightRes+ equipAdd;
            case Attribute.DarkRes: return heroDic[heroID].darkRes+ equipAdd;
            case Attribute.DizzyRes: return heroDic[heroID].dizzyRes+ equipAdd;
            case Attribute.ConfusionRes: return heroDic[heroID].confusionRes+ equipAdd;
            case Attribute.PoisonRes: return heroDic[heroID].poisonRes+ equipAdd;
            case Attribute.SleepRes: return heroDic[heroID].sleepRes+ equipAdd;
            case Attribute.GoldGet: return heroDic[heroID].goldGet+ equipAdd;
            case Attribute.ExpGet: return heroDic[heroID].expGet+ equipAdd;
            case Attribute.ItemGet: return heroDic[heroID].itemGet+ equipAdd;
            default: return 0;
        }

    }
    #endregion

    #region 【辅助方法集】输出字符
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
        int year=0, month = 0, day = 0, hour = 0, st = 0;

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
            case "Y年M月D日H时":return (year + 1) + ("年") + ((month + 1) > 12 ? 1 : (month + 1)) + ("月") + day + ("日") + hour + ("时");
            case "Y年M月D日": return (year + 1) + ("年") + ((month + 1) > 12 ? 1 : (month + 1)) + ("月") + day + ("日") ; 
            case "Y/M/D H": return (year + 1) + ("/") + ((month + 1) > 12 ? 1 : (month + 1)) + ("/") + day + (" ") + hour ;
            default:return "未知格式";
        }    

   
    }

    public string OutputWorkValueToRank(int value)
    {
        switch (value)
        {
            case 10:return "E  ";
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

    public string OutputSeasonStr(int month,bool color)
    {
        switch (month)
        {
            case 1:
            case 2:
            case 3:
                return (color ? "<color=#7BBD00>" : "" )+ "春" + (color ? "</color>" : "");
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
            default:return "错误的月份";
        }
    }
    
    public string OutputWeekStr(int week, bool color)
    {
        switch (week)
        {
            case 1:return (color ? "<color=#DA7CFF>" : "") + "星期一" + (color ? "</color>" : "");
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
            default:return "未定义类型";
        }
    }
    #endregion
}
