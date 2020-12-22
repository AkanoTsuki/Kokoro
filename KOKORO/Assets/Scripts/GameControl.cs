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
    public int gold=0;
    public short nowCheckingDistrictID = 0;
    public int standardTime = 0;//时间戳，基准时间单位：1/10小时
    public List<ExecuteEventObject> executeEventList = new List<ExecuteEventObject>();
    public int nextExecuteEventEndTime = 0;
    public int heroIndex = 0;
    public int itemIndex = 0;
    public int buildingIndex = 0;
    public bool[] buildingUnlock=new bool[73] ; 
    public int logIndex = 0;
    public string playerName = "AAA";
    public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
    public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();
    public DistrictObject[] districtDic = new DistrictObject[7];
    public Dictionary<int, DistrictGridObject> districtGridDic = new Dictionary<int, DistrictGridObject>();
    public Dictionary<int, BuildingObject> buildingDic = new Dictionary<int, BuildingObject>();
    public Dictionary<int, LogObject> logDic = new Dictionary<int, LogObject>();
    public List<AdventureTeamObject> adventureTeamList = new List<AdventureTeamObject>();

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

    #region 【通用方法】生成英雄、道具
    public void CreateHero(short pid)
    {
        heroDic.Add(heroIndex,GenerateHeroByRandom(heroIndex, pid,(byte)Random.Range(0,2)));
        heroIndex++;
    }

    public HeroObject GenerateHeroByRandom(int heroID,short heroTypeID,byte sexCode)
    {
        string name;
        string pic;
        if (sexCode == 0)
        {
            name = DataManager.mNameMan[Random.Range(0, DataManager.mNameMan.Length)];
            pic = DataManager.mHeroDict[heroTypeID].PicMan;
        }
        else
        {
            name = DataManager.mNameWoman[Random.Range(0, DataManager.mNameWoman.Length)];
            pic = DataManager.mHeroDict[heroTypeID].PicWoman;
        }

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


       return new HeroObject(heroID, name, heroTypeID, 1,0, sexCode, pic, hp,mp,hpRenew,mpRenew,atkMin,atkMax,mAtkMin,mAtkMax,def,mDef,hit,dod,criR,criD,spd,
         windDam,fireDam,waterDam,groundDam,lightDam,darkDam,windRes,fireRes,waterRes,groundRes,lightRes,darkRes,dizzyRes,confusionRes,poisonRes,sleepRes,goldGet,expGet,itemGet,
         workPlanting,workFeeding,workFishing,workHunting,workMining,workQuarrying,workFelling,workBuild,workMakeWeapon,workMakeArmor,workMakeJewelry,workSundry,
         -1,-1,-1,-1,-1,-1,-1,-1,-1,-1, -1,-1);

}

    public HeroObject GenerateHeroByMould(int heroID, short heroTypeID, byte sexCode,string nameSet)
    {

        string name = "";
        string pic = "";
        if (sexCode == 0)
        {
            name = DataManager.mNameMan[Random.Range(0, DataManager.mNameMan.Length)];
            pic = DataManager.mHeroDict[heroTypeID].PicMan;
        }
        else
        {
            name = DataManager.mNameWoman[Random.Range(0, DataManager.mNameWoman.Length)];
            pic = DataManager.mHeroDict[heroTypeID].PicWoman;
        }

        int hp = DataManager.mHeroDict[heroTypeID].Hp;
        int mp = DataManager.mHeroDict[heroTypeID].Mp;
        short hpRenew = DataManager.mHeroDict[heroTypeID].HpRenew;
        short mpRenew = DataManager.mHeroDict[heroTypeID].MpRenew;
        short atkMin = DataManager.mHeroDict[heroTypeID].AtkMin;
        short atkMax = DataManager.mHeroDict[heroTypeID].AtkMax;
        short mAtkMin = DataManager.mHeroDict[heroTypeID].MAtkMin;
        short mAtkMax = DataManager.mHeroDict[heroTypeID].MAtkMax;
        short def = DataManager.mHeroDict[heroTypeID].Def;
        short mDef = DataManager.mHeroDict[heroTypeID].MDef;
        short hit = DataManager.mHeroDict[heroTypeID].Hit;
        short dod = DataManager.mHeroDict[heroTypeID].Dod;
        short criR = DataManager.mHeroDict[heroTypeID].CriR;
        short criD = DataManager.mHeroDict[heroTypeID].CriD;
        short spd = DataManager.mHeroDict[heroTypeID].Spd;
        short windDam = DataManager.mHeroDict[heroTypeID].WindDam;
        short fireDam = DataManager.mHeroDict[heroTypeID].FireDam ;
        short waterDam = DataManager.mHeroDict[heroTypeID].WaterDam ;
        short groundDam = DataManager.mHeroDict[heroTypeID].GroundDam ;
        short lightDam = DataManager.mHeroDict[heroTypeID].LightDam ;
        short darkDam = DataManager.mHeroDict[heroTypeID].DarkDam ;
        short windRes = DataManager.mHeroDict[heroTypeID].WindRes ;
        short fireRes = DataManager.mHeroDict[heroTypeID].FireRes;
        short waterRes = DataManager.mHeroDict[heroTypeID].WaterRes;
        short groundRes = DataManager.mHeroDict[heroTypeID].GroundRes;
        short lightRes = DataManager.mHeroDict[heroTypeID].LightRes;
        short darkRes = DataManager.mHeroDict[heroTypeID].DarkRes;
        short dizzyRes = DataManager.mHeroDict[heroTypeID].DizzyRes;
        short confusionRes = DataManager.mHeroDict[heroTypeID].ConfusionRes;
        short poisonRes = DataManager.mHeroDict[heroTypeID].PoisonRes;
        short sleepRes = DataManager.mHeroDict[heroTypeID].SleepRes;
        byte goldGet = DataManager.mHeroDict[heroTypeID].GoldGet;
        byte expGet = DataManager.mHeroDict[heroTypeID].ExpGet;
        byte itemGet = DataManager.mHeroDict[heroTypeID].ItemGet;
        byte workPlanting = DataManager.mHeroDict[heroTypeID].WorkPlanting;
        byte workFeeding = DataManager.mHeroDict[heroTypeID].WorkFeeding;
        byte workFishing = DataManager.mHeroDict[heroTypeID].WorkFishing;
        byte workHunting = DataManager.mHeroDict[heroTypeID].WorkHunting;
        byte workMining = DataManager.mHeroDict[heroTypeID].WorkMining;
        byte workQuarrying = DataManager.mHeroDict[heroTypeID].WorkQuarrying;
        byte workFelling = DataManager.mHeroDict[heroTypeID].WorkFelling;
        byte workBuild = DataManager.mHeroDict[heroTypeID].WorkBuild;
        byte workMakeWeapon = DataManager.mHeroDict[heroTypeID].WorkMakeWeapon;
        byte workMakeArmor = DataManager.mHeroDict[heroTypeID].WorkMakeArmor;
        byte workMakeJewelry = DataManager.mHeroDict[heroTypeID].WorkMakeJewelry;
        byte workSundry = DataManager.mHeroDict[heroTypeID].WorkSundry;


        return new HeroObject(heroID,nameSet!=""?nameSet:name, heroTypeID, 1, 0, sexCode, pic, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
          windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, goldGet, expGet, itemGet,
          workPlanting, workFeeding, workFishing, workHunting, workMining, workQuarrying, workFelling, workBuild, workMakeWeapon, workMakeArmor, workMakeJewelry, workSundry,
          -1, -1, -1, -1,-1, -1, -1, -1, -1, -1, -1,-1);

    }

    public int SetAttr(Attribute attr, short heroTypeID)
    {
        int rank;

        switch (attr)
        {
            case Attribute.Hp:rank = DataManager.mCreateHeroTypeDict[heroTypeID].Hp;break;
            case Attribute.Mp: rank = DataManager.mCreateHeroTypeDict[heroTypeID].Mp; break;

            case Attribute.AtkMax: rank = DataManager.mCreateHeroTypeDict[heroTypeID].AtkMax; break;
            case Attribute.MAtkMax: rank = DataManager.mCreateHeroTypeDict[heroTypeID].MAtkMax; break;

            case Attribute.Def: rank = DataManager.mCreateHeroTypeDict[heroTypeID].Def; break;
            case Attribute.MDef: rank = DataManager.mCreateHeroTypeDict[heroTypeID].MDef; break;

            case Attribute.Hit: rank = DataManager.mCreateHeroTypeDict[heroTypeID].Hit; break;
            case Attribute.Dod: rank = DataManager.mCreateHeroTypeDict[heroTypeID].Dod; break;
            case Attribute.CriR: rank = DataManager.mCreateHeroTypeDict[heroTypeID].CriR; break;


            case Attribute.WorkPlanting: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkPlanting; break;
            case Attribute.WorkFeeding: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkFeeding; break;
            case Attribute.WorkFishing: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkFishing; break;
            case Attribute.WorkHunting: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkHunting; break;
            case Attribute.WorkFelling: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkFelling; break;
            case Attribute.WorkQuarrying: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkQuarrying; break;
            case Attribute.WorkMining: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkMining; break;
            case Attribute.WorkBuild: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkBuild; break;
            case Attribute.WorkMakeWeapon: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkMakeWeapon; break;
            case Attribute.WorkMakeArmor: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkMakeArmor; break;
            case Attribute.WorkMakeJewelry: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkMakeJewelry; break;
            case Attribute.WorkSundry: rank = DataManager.mCreateHeroTypeDict[heroTypeID].WorkSundry; break;
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

    public ItemObject GenerateItemByRandom(int itemID, DistrictObject districtObject,List<int> heroObjectIDList)
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

        if (DataManager.mItemDict[itemID].AtkMax != 0){attrList.Add(new ItemAttribute(Attribute.AtkMax, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].AtkMax * upRate))); }
        if (DataManager.mItemDict[itemID].AtkMin != 0){attrList.Add(new ItemAttribute(Attribute.AtkMin, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].AtkMin * upRate)));}
        if (DataManager.mItemDict[itemID].MAtkMax != 0){attrList.Add(new ItemAttribute(Attribute.MAtkMax, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].MAtkMax * upRate)));}
        if (DataManager.mItemDict[itemID].MAtkMin != 0){attrList.Add(new ItemAttribute(Attribute.MAtkMin, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].MAtkMin * upRate)));}

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
        byte rank =(byte) (DataManager.mItemDict[itemID].Rank-1);
        int lemmaCount = 0;
         ran = Random.Range(0, 99);
        upRate = 1f + Random.Range(0f,0.2f);
        if (ran <= 10)
        {
            lemmaCount = 2;
        }
        else if (ran > 10 && ran <= 30)
        {
            lemmaCount = 1;
        }
        for (int i = 0; i < lemmaCount; i++)
        {
            int lemmaID = Random.Range(0, DataManager.mLemmaDict.Count);
            name = DataManager.mLemmaDict[lemmaID].Name+"的 " + name;

           // Debug.Log("lemmaID=" + lemmaID+ " rank="+ rank);

            
            if (DataManager.mLemmaDict[lemmaID].Hp.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Hp, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Hp[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Mp.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Mp, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Mp[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].HpRenew.Count !=0) { attrList.Add(new ItemAttribute(Attribute.HpRenew, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].HpRenew[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MpRenew.Count !=0) { attrList.Add(new ItemAttribute(Attribute.MpRenew, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].MpRenew[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].AtkMax.Count != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMax, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].AtkMax[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].AtkMin.Count != 0) { attrList.Add(new ItemAttribute(Attribute.AtkMin, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].AtkMin[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MAtkMax.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMax, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].MAtkMax[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MAtkMin.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MAtkMin, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].MAtkMin[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].Def.Count != 0) { attrList.Add(new ItemAttribute(Attribute.Def, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Def[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].MDef.Count != 0) { attrList.Add(new ItemAttribute(Attribute.MDef, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].MDef[rank] * upRate))); }


            if (DataManager.mLemmaDict[lemmaID].Hit.Count !=0) { attrList.Add(new ItemAttribute(Attribute.Hit, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Hit[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Dod.Count !=0) { attrList.Add(new ItemAttribute(Attribute.Dod, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Dod[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].CriR.Count !=0) { attrList.Add(new ItemAttribute(Attribute.CriR, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].CriR[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].CriD.Count !=0) { attrList.Add(new ItemAttribute(Attribute.CriD, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].CriD[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].Spd.Count !=0) { attrList.Add(new ItemAttribute(Attribute.Spd, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].Spd[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].WindDam.Count !=0) { attrList.Add(new ItemAttribute(Attribute.WindDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].WindDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].FireDam.Count !=0) { attrList.Add(new ItemAttribute(Attribute.FireDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].FireDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].WaterDam.Count !=0) { attrList.Add(new ItemAttribute(Attribute.WaterDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].WaterDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GroundDam.Count !=0) { attrList.Add(new ItemAttribute(Attribute.GroundDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].GroundDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].LightDam.Count !=0) { attrList.Add(new ItemAttribute(Attribute.LightDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].LightDam[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].DarkDam.Count !=0) { attrList.Add(new ItemAttribute(Attribute.DarkDam, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].DarkDam[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].WindRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.WindRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].WindRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].FireRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.FireRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].FireRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].WaterRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.WaterRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].WaterRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GroundRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.GroundRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].GroundRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].LightRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.LightRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].LightRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].DarkRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.DarkRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].DarkRes[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].DizzyRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.DizzyRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].DizzyRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].ConfusionRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.ConfusionRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].ConfusionRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].PoisonRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.PoisonRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].PoisonRes[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].SleepRes.Count !=0) { attrList.Add(new ItemAttribute(Attribute.SleepRes, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].SleepRes[rank] * upRate))); }

            if (DataManager.mLemmaDict[lemmaID].ExpGet.Count !=0) { attrList.Add(new ItemAttribute(Attribute.ExpGet, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].ExpGet[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].GoldGet.Count !=0) { attrList.Add(new ItemAttribute(Attribute.GoldGet, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].GoldGet[rank] * upRate))); }
            if (DataManager.mLemmaDict[lemmaID].ItemGet.Count !=0) { attrList.Add(new ItemAttribute(Attribute.ItemGet, AttributeSource.LemmaAdd, (int)(DataManager.mLemmaDict[lemmaID].ItemGet[rank] * upRate))); }

        }



        return new ItemObject(itemIndex, itemID, name, DataManager.mItemDict[itemID].Pic, DataManager.mItemDict[itemID].Rank,upLevel,attrList, 
            DataManager.mItemDict[itemID].Des+("于"+timeYear+"年"+timeMonth+"月"+ (districtObject != null ? ("在"+districtObject.name + "制作") :"获得") ), DataManager.mItemDict[itemID].Cost, districtObject!=null? districtObject.id:(short)-1, false,-1, EquipPart.None);
    }
    #endregion

    #region 【方法】建筑物建设
    public void BuildDone( short buildingId)
    {
        buildingDic[buildingId].buildProgress = 1;
      
        //资源生产设施开始自动开工


        AreaMapPanel.Instance.AddIconByBuilding(buildingId);
        if(DistrictMainPanel.Instance.isShow&& buildingDic[buildingId].districtID== nowCheckingDistrictID)
        {
            DistrictMainPanel.Instance.UpdateNatureInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateCultureInfo(districtDic[nowCheckingDistrictID]);
            DistrictMainPanel.Instance.UpdateBuildingInfo(districtDic[nowCheckingDistrictID]);
        }


        //BuildPanel.Instance.UpdateAllInfo(this);
        if (BuildingSelectPanel.Instance.isShow)
        {
            BuildingSelectPanel.Instance.UpdateAllInfo(buildingDic[buildingId].districtID, 2);
        }
        MessagePanel.Instance.AddMessage(districtDic[buildingDic[buildingId].districtID].name+"的"+ buildingDic[buildingId].name+"建筑完成");
    
    }

    void StartBuild(int districtID,int buildingID, int needTime)
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
        BuildPanel.Instance.UpdateAllInfo(this);
        PlayMainPanel.Instance.UpdateGold();
        PlayMainPanel.Instance.UpdateResourcesInfo(nowCheckingDistrictID);
    }

    public void CreateProduceItemEvent(int buildingID)
    {
        int needLabor = DataManager.mProduceEquipDict[buildingDic[buildingID].produceEquipNow].NeedLabor;
        int nowLabor = 20+ buildingDic[buildingID].workerNow*20;
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
        int num1,num2 = 0;

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
                num1 = (int)(DataManager.mProduceResourceDict[buildingDic[buildingID].prototypeID].OutputStone* (1f + GetProduceResourceOutputUp(buildingID)) * laborRate);
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

        if (GetDistrictProductAll (districtID) >= districtDic[districtID].rProductLimit)
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

        if (DataManager.mProduceEquipDict[moduleID].InputWood> districtDic[districtID].rStuffWood||
            DataManager.mProduceEquipDict[moduleID].InputStone > districtDic[districtID].rStuffStone ||
            DataManager.mProduceEquipDict[moduleID].InputMetal > districtDic[districtID].rStuffMetal ||
            DataManager.mProduceEquipDict[moduleID].InputLeather > districtDic[districtID].rStuffLeather ||
            DataManager.mProduceEquipDict[moduleID].InputCloth > districtDic[districtID].rStuffCloth ||
            DataManager.mProduceEquipDict[moduleID].InputTwine > districtDic[districtID].rStuffTwine ||
            DataManager.mProduceEquipDict[moduleID].InputBone > districtDic[districtID].rStuffBone )
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


        itemDic.Add(itemIndex, GenerateItemByRandom(itemID, districtDic[districtID],buildingDic[buildingID].heroList));
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

    public bool DistrictResourceAdd(short districtID, int buildingID, StuffType stuffType,  int value)
    {

        Debug.Log("DistrictResourceAdd() "+ districtID+" "+ stuffType+" "+value);
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
                    if (BuildingPanel.Instance.isShow&& BuildingPanel.Instance.nowCheckingBuildingID== buildingID)
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
            case StuffType.Cereal:districtDic[districtID].rFoodCereal += value;break;
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
        Debug.Log("ChangeProduceEquipNow() buildingID=" + buildingID+ " setForgeType="+ BuildingPanel.Instance.setForgeType + " setForgeLevel" + BuildingPanel.Instance.setForgeLevel);
        bool needStart = (buildingDic[buildingID].produceEquipNow == -1);

        foreach (KeyValuePair<int, ProduceEquipPrototype> kvp in DataManager.mProduceEquipDict)
        {
            if (kvp.Value.MakePlace.Contains((byte)buildingDic[buildingID].prototypeID)&& kvp.Value.OptionValue == BuildingPanel.Instance.setForgeType && kvp.Value.Level ==( BuildingPanel.Instance.setForgeLevel+1))
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
        if ( buildingDic[buildingID].worker<=0)
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

    #region 【方法】英雄装备/卸下
    public void HeroEquipSet(int heroID,EquipPart equipPart, int itemID)
    {
        Debug.Log("HeroEquipSet() heroID=" + heroID + " equipPart=" + equipPart + " itemID="+ itemID+ " heroDic[heroID].equipWeapon=" + heroDic[heroID].equipWeapon);
        if (heroDic[heroID].equipWeapon != -1)
        {
            itemDic[heroDic[heroID].equipWeapon].heroID = -1;
            itemDic[heroDic[heroID].equipWeapon].heroPart = EquipPart.None;
        }
        switch (equipPart)
        {
            case EquipPart.Weapon:heroDic[heroID].equipWeapon = itemID;break;
            case EquipPart.Subhand: heroDic[heroID].equipSubhand = itemID; break;
            case EquipPart.Head: heroDic[heroID].equipHead = itemID; break;
            case EquipPart.Body: heroDic[heroID].equipBody = itemID; break;
            case EquipPart.Hand: heroDic[heroID].equipHand = itemID; break;
            case EquipPart.Back: heroDic[heroID].equipBack = itemID; break;
            case EquipPart.Foot: heroDic[heroID].equipFoot = itemID; break;
            case EquipPart.Neck: heroDic[heroID].equipNeck = itemID; break;
            case EquipPart.Finger1: heroDic[heroID].equipFinger1 = itemID; break;
            case EquipPart.Finger2: heroDic[heroID].equipFinger2 = itemID; break;
            default:break;
        }
        itemDic[itemID].heroID = heroID;
        itemDic[itemID].heroPart = equipPart;

        HeroPanel.Instance.UpdateEquip(heroDic[heroID], equipPart);
        ItemListAndInfoPanel.Instance.OnHide();
    }

    public void HeroEquipUnSet(int heroID, EquipPart equipPart)
    {
        if (heroDic[heroID].equipWeapon == -1)//原本就无装备
        {
            return;
        }
        itemDic[heroDic[heroID].equipWeapon].heroID = -1;
        itemDic[heroDic[heroID].equipWeapon].heroPart = EquipPart.None;
        switch (equipPart)
        {
            case EquipPart.Weapon:heroDic[heroID].equipWeapon = -1;break;
            case EquipPart.Subhand: heroDic[heroID].equipSubhand = -1; break;
            case EquipPart.Head: heroDic[heroID].equipHead = -1; break;
            case EquipPart.Body: heroDic[heroID].equipBody = -1; break;
            case EquipPart.Hand: heroDic[heroID].equipHand = -1; break;
            case EquipPart.Back: heroDic[heroID].equipBack = -1; break;
            case EquipPart.Foot: heroDic[heroID].equipFoot = -1; break;
            case EquipPart.Neck: heroDic[heroID].equipNeck = -1; break;
            case EquipPart.Finger1: heroDic[heroID].equipFinger1 = -1; break;
            case EquipPart.Finger2: heroDic[heroID].equipFinger2 = -1; break;
        }

        HeroPanel.Instance.UpdateEquip(heroDic[heroID], equipPart);
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

    public void AdventureTeamSetDungeon(byte teamID, short dungeonID)
    {
        adventureTeamList[teamID].dungeonID=  dungeonID;
    }

    public void AdventureTeamHeroMinus(byte teamID, int heroID)
    {
        if (!adventureTeamList[teamID].heroIDList.Contains(heroID))
        {
            return;
        }
        adventureTeamList[teamID].heroIDList.Remove(heroID);
        heroDic[heroID].adventureInTeam = -1;
        //todo UI操作
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
        adventureTeamList[teamID].heroIDList.Add(teamID);
        heroDic[heroID].adventureInTeam = teamID;
        //todo  UI操作
    }

    public void AdventureTeamSend(byte teamID)
    {
        
    }

    public void AdventureEventHappen()
    { 

    }

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
            case ItemTypeSmall.HeadH: return "头部装备（重型）";
            case ItemTypeSmall.HeadL: return "头部装备（轻型）";
            case ItemTypeSmall.BodyH: return "身体装备（重型）";
            case ItemTypeSmall.BodyL: return "身体装备（轻型）";
            case ItemTypeSmall.HandH: return "手部装备（重型）";
            case ItemTypeSmall.HandL: return "手部装备（轻型）";
            case ItemTypeSmall.BackH: return "背部装备（重型）";
            case ItemTypeSmall.BackL: return "背部装备（轻型）";
            case ItemTypeSmall.FootH: return "腿部装备（重型）";
            case ItemTypeSmall.FootL: return "腿部装备（轻型）";
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
