﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameControl : MonoBehaviour
{


    //no save
    public int timeS = 0;
    public int timeHour = 0;
    public int timeDay = 1;
    public int timeWeek = 1;
    public int timeMonth = 1;
    public int timeSeason = 1;
    public int timeYear = 1;

    //save data
    public int gold=0;
    public short nowCheckingDistrictID = 0;
    public int standardTime = 0;//时间戳，基准时间单位：小时
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

    /// <summary>
    /// 用作存档的数据类
    /// </summary>
    [System.Serializable]
    public class DataSave
    {
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
        t.logDic= this.logDic;

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
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("1heroDic.Count=" + heroDic.Count);
        //CreateHero(1);
        //Debug.Log("2heroDic.Count=" + heroDic.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateHero(short pid)
    {
        heroDic.Add(heroIndex,GenerateHeroByRandom(heroIndex, pid,(byte)Random.Range(0,2)));
        heroIndex++;
    }

    //pid:
    public HeroObject GenerateHeroByRandom(int heroID,short heroTypeID,byte sexCode)
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
        short windDam = 100;
        short fireDam = 100;
        short waterDam = 100;
        short groundDam = 100;
        short lightDam = 100;
        short darkDam = 100;
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
         -1,-1,-1,-1,-1,-1,-1,-1,-1,-1, -1);

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
        short atkMax = DataManager.mHeroDict[heroTypeID].AtkMin;
        short atkMin = DataManager.mHeroDict[heroTypeID].AtkMax;
        short mAtkMax = DataManager.mHeroDict[heroTypeID].MAtkMin;
        short mAtkMin = DataManager.mHeroDict[heroTypeID].MAtkMax;
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
          -1, -1, -1, -1,-1, -1, -1, -1, -1, -1, -1);

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

    public ItemObject GenerateItemByRandom(int itemID)
    {
        //随机提升等级，每个等级提升基础数据5%，上限5
        byte upLevel = 0;
        for (int i = 0; i < 5; i++)
        {
            int ran = Random.Range(0, 100);
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



        List<ItemAttribute> attrList = new List<ItemAttribute> { };

        if (DataManager.mItemDict[itemID].AtkMax != 0){attrList.Add(new ItemAttribute(Attribute.AtkMax, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].AtkMax * upRate))); }
        if (DataManager.mItemDict[itemID].AtkMin != 0){attrList.Add(new ItemAttribute(Attribute.AtkMin, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].AtkMin * upRate)));}
        if (DataManager.mItemDict[itemID].MAtkMax != 0){attrList.Add(new ItemAttribute(Attribute.MAtkMax, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].MAtkMax * upRate)));}
        if (DataManager.mItemDict[itemID].MAtkMin != 0){attrList.Add(new ItemAttribute(Attribute.MAtkMin, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].MAtkMin * upRate)));}
        
        if (DataManager.mItemDict[itemID].Hp != 0) { attrList.Add(new ItemAttribute(Attribute.Hp, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Hp * upRate))); }
        if (DataManager.mItemDict[itemID].Mp != 0) { attrList.Add(new ItemAttribute(Attribute.Mp, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Mp * upRate))); }
        if (DataManager.mItemDict[itemID].HpRenew != 0) { attrList.Add(new ItemAttribute(Attribute.HpRenew, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].HpRenew * upRate))); }
        if (DataManager.mItemDict[itemID].MpRenew != 0) { attrList.Add(new ItemAttribute(Attribute.MpRenew, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].AtkMax * upRate))); }

        if (DataManager.mItemDict[itemID].Hit != 0) { attrList.Add(new ItemAttribute(Attribute.Hit, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Hit * upRate))); }
        if (DataManager.mItemDict[itemID].Dod != 0) { attrList.Add(new ItemAttribute(Attribute.Dod, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Dod * upRate))); }
        if (DataManager.mItemDict[itemID].CriR != 0) { attrList.Add(new ItemAttribute(Attribute.CriR, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].CriR * upRate))); }
        if (DataManager.mItemDict[itemID].CriD != 0) { attrList.Add(new ItemAttribute(Attribute.CriD, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].CriD * upRate))); }
        if (DataManager.mItemDict[itemID].Spd != 0) { attrList.Add(new ItemAttribute(Attribute.Spd, AttributeSource.Basic, (int)(DataManager.mItemDict[itemID].Spd * upRate))); }

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



        return new ItemObject(itemIndex, itemID, DataManager.mItemDict[itemID].Name+(upLevel>0?" +"+ upLevel : ""), DataManager.mItemDict[itemID].Pic, DataManager.mItemDict[itemID].Rank,upLevel,attrList, DataManager.mItemDict[itemID].Des+(standardTime+"制作"), DataManager.mItemDict[itemID].Cost);
    }



    public void Build( short buildingId)
    {
       // Debug.Log(" Build（） buildingId=" + buildingId);


        if (DataManager.mBuildingDict[buildingId].NatureGrass > districtDic[nowCheckingDistrictID].totalGrass- districtDic[nowCheckingDistrictID].usedGrass)
        {
            return;
        }
        if (DataManager.mBuildingDict[buildingId].NatureWood > districtDic[nowCheckingDistrictID].totalGrass - districtDic[nowCheckingDistrictID].usedWood)
        {
            return;
        }
        if (DataManager.mBuildingDict[buildingId].NatureWater > districtDic[nowCheckingDistrictID].totalGrass - districtDic[nowCheckingDistrictID].usedWater)
        {
            return;
        }
        if (DataManager.mBuildingDict[buildingId].NatureStone > districtDic[nowCheckingDistrictID].totalStone - districtDic[nowCheckingDistrictID].usedStone)
        {
            return;
        }
        if (DataManager.mBuildingDict[buildingId].NatureMetal > districtDic[nowCheckingDistrictID].totalMetal - districtDic[nowCheckingDistrictID].usedMetal)
        {
            return;
        }



        List<int> grid = new List<int> { };
        short count = DataManager.mBuildingDict[buildingId].Grid;


        foreach (KeyValuePair<int, DistrictGridObject> kvp in districtGridDic)
        {
            if (DataManager.mDistrictGridDict[kvp.Value.id].DistrictID == nowCheckingDistrictID&& 
                DataManager.mDistrictGridDict[kvp.Value.id].Level<= districtDic[nowCheckingDistrictID].level&&
                 kvp.Value.buildingID==-1)
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
        Debug.Log("grid.count=" + grid.Count );
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
        districtDic[nowCheckingDistrictID].gridUsed+= count;
        districtDic[nowCheckingDistrictID].buildingList.Add(buildingIndex);



        Debug.Log("buildingDic.count="+ buildingDic.Count+"  buildingIndex=" + buildingIndex);
        buildingDic.Add(buildingIndex, new BuildingObject(buildingIndex, buildingId, nowCheckingDistrictID, DataManager.mBuildingDict[buildingId].Name, DataManager.mBuildingDict[buildingId].MainPic, DataManager.mBuildingDict[buildingId].MapPic, DataManager.mBuildingDict[buildingId].PanelType, DataManager.mBuildingDict[buildingId].Des, DataManager.mBuildingDict[buildingId].Level, DataManager.mBuildingDict[buildingId].Expense, DataManager.mBuildingDict[buildingId].UpgradeTo, true, grid, new List<int> { },
            DataManager.mBuildingDict[buildingId].NatureGrass, DataManager.mBuildingDict[buildingId].NatureWood, DataManager.mBuildingDict[buildingId].NatureWater, DataManager.mBuildingDict[buildingId].NatureStone, DataManager.mBuildingDict[buildingId].NatureMetal,
            DataManager.mBuildingDict[buildingId].People, DataManager.mBuildingDict[buildingId].Worker, 0,
            DataManager.mBuildingDict[buildingId].EWind, DataManager.mBuildingDict[buildingId].EFire, DataManager.mBuildingDict[buildingId].EWater, DataManager.mBuildingDict[buildingId].EGround, DataManager.mBuildingDict[buildingId].ELight, DataManager.mBuildingDict[buildingId].EDark));

        //资源生产设施开始自动开工
        
        


        AreaMapPanel.Instance.AddIconByBuilding(buildingIndex);

        DistrictMainPanel.Instance.UpdateNatureInfo(districtDic[nowCheckingDistrictID]);
        DistrictMainPanel.Instance.UpdateOutputInfo(districtDic[nowCheckingDistrictID]);
        DistrictMainPanel.Instance.UpdateCultureInfo(districtDic[nowCheckingDistrictID]);
        DistrictMainPanel.Instance.UpdateBuildingInfo(districtDic[nowCheckingDistrictID]);

        BuildPanel.Instance.UpdateAllInfo(this);
        
        buildingIndex++;
    }

     void StartProduceResource(int districtID, int buildingID,int needTime, StuffType stuffType, int value)
    {
        //value1:地区实例ID value2:建筑实例ID value3:资源类型
        ExecuteEventAdd(new ExecuteEventObject(ExecuteEventType.ProduceResource,standardTime, standardTime + needTime, districtID, buildingID, (int)stuffType, value, -1));

    }


    public void CreateProduceEvent(int buildingID)
    {


        int needTime = 0;
        int num = 0;
        
        switch (buildingDic[buildingID].id)
        {
            case 9://麦田
                needTime = 24 * 30;
                 num = buildingDic[buildingID].workerNow * 100;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Cereal, num);
                break;
            case 10://菜田
                needTime = 24 * 30;
                num = buildingDic[buildingID].workerNow * 100;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Vegetable, num);
                break;
            case 11://果园
                needTime = 24 * 30;
                num = buildingDic[buildingID].workerNow * 100;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Fruit, num);
                break;
            case 12://亚麻田
                needTime = 24 * 30;
                num = buildingDic[buildingID].workerNow * 100;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Twine, num);
                break;
            case 13://牛圈
                needTime = 24 * 30;
                num = buildingDic[buildingID].workerNow * 100;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Meat, num);
                break;
            case 14://羊圈
                needTime = 24 * 30;
                num = buildingDic[buildingID].workerNow * 100;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Meat, num);
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Cloth, num);
                break;
            case 15://渔场
                needTime = 24 * 30;
                num = buildingDic[buildingID].workerNow * 100;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Fish, num);
                break;

            case 16://伐木场
                needTime =(int)( (24) *(1f- buildingDic[buildingID].workerNow*0.05f));
                num = 200;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Wood, num);
                break;
            case 17://伐木场
                needTime = (int)((24) * (1f - buildingDic[buildingID].workerNow * 0.05f));
                num = 300;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Wood, num);
                break;
            case 18://伐木场
                needTime = (int)((24) * (1f - buildingDic[buildingID].workerNow * 0.05f));
                num = 500;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Wood, num);
                break;

            case 19://矿场
                needTime = (int)((24) * (1f - buildingDic[buildingID].workerNow * 0.05f));
                num = 200;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Metal, num);
                break;
            case 20://矿场
                needTime = (int)((24) * (1f - buildingDic[buildingID].workerNow * 0.05f));
                num = 300;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Metal, num);
                break;
            case 21://矿场
                needTime = (int)((24) * (1f - buildingDic[buildingID].workerNow * 0.05f));
                num = 500;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Metal, num);
                break;

            case 22://采石场
                needTime = (int)((24) * (1f - buildingDic[buildingID].workerNow * 0.05f));
                num = 200;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Stone, num);
                break;
            case 23://采石场
                needTime = (int)((24) * (1f - buildingDic[buildingID].workerNow * 0.05f));
                num = 300;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Stone, num);
                break;
            case 24://采石场
                needTime = (int)((24) * (1f - buildingDic[buildingID].workerNow * 0.05f));
                num = 500;
                StartProduceResource(buildingDic[buildingID].districtID, buildingID, needTime, StuffType.Stone, num);
                break;

        }


    }

    public void ResourceAdd(int districtID, StuffType stuffType,  int value)
    {
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
    }

    public void CreateLog(LogType logType,string text, int value1, int value2, int value3)
    {
        logDic.Add(logIndex, new LogObject(logIndex, logType,standardTime, text, value1, value2, value3));
        logIndex++;
        MessagePanel.Instance.AddMessage(logDic[logIndex - 1]);
    }

    //添加执行事件，遍历确定插入位置
    public void ExecuteEventAdd(ExecuteEventObject executeEventObject)
    {
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

    public string ValueToRank(int value)
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


}
