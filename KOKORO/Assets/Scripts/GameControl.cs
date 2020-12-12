using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameControl : MonoBehaviour
{


    //no save
    public bool IsNewGame = false;

    //save data
    public int gold=0;
    public int nowCheckingDistrictID = 0;
    public int countHour = 0;//时间戳，基准时间单位：小时
    public int heroIndex = 0;
    public int itemIndex = 0;
    public int buildingIndex = 0;
    public string playerName = "AAA";
    public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
    public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();
    public DistrictObject[] districtDic = new DistrictObject[7];
    public Dictionary<int, DistrictGridObject> districtGridDic = new Dictionary<int, DistrictGridObject>();
    public Dictionary<int, BuildingObject> buildingDic = new Dictionary<int, BuildingObject>();


    /// <summary>
    /// 用作存档的数据类
    /// </summary>
    [System.Serializable]
    public class DataSave
    {
        public int gold = 0;
        public int nowCheckingDistrictID = 0;
        public int countHour = 0;
        public int heroIndex = 0;
        public int itemIndex = 0;
        public int buildingIndex = 0;
        public string playerName = "";
        public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
        public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();
        public DistrictObject[] districtDic = new DistrictObject[7];
        public Dictionary<int, DistrictGridObject> districtGridDic = new Dictionary<int, DistrictGridObject>();
        public Dictionary<int, BuildingObject> buildingDic = new Dictionary<int, BuildingObject>();
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
        t.countHour = this.countHour;
        t.heroIndex = this.heroIndex;
        t.itemIndex = this.itemIndex;
        t.buildingIndex = this.buildingIndex;
        t.playerName = this.playerName;
        t.itemDic = this.itemDic;
        t.heroDic = this.heroDic;
        t.districtDic = this.districtDic;
        t.districtGridDic = this.districtGridDic;
        t.buildingDic = this.buildingDic;


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
            this.countHour = t1.countHour;
            this.heroIndex = t1.heroIndex;
            this.itemIndex = t1.itemIndex;
            this.buildingIndex = t1.buildingIndex;
            this.playerName = t1.playerName;
            this.itemDic = t1.itemDic;
            this.heroDic = t1.heroDic;
            this.districtDic = t1.districtDic;
            this.districtGridDic = t1.districtGridDic;
            this.buildingDic = t1.buildingDic;

            IsNewGame = false;
        }
        else
        {
            print("文件不存在." + filename);
            IsNewGame = true;
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
    public void CreateHero(int pid)
    {
        heroDic.Add(heroIndex,GenerateHeroByRandom(heroIndex, pid,Random.Range(0,2)));
        heroIndex++;
    }

    //pid:
    public HeroObject GenerateHeroByRandom(int heroID,int pid,int sexCode)
    {
        string name = "";
        string pic = "";
        if (sexCode == 0)
        {
            name = DataManager.mNameMan[Random.Range(0, DataManager.mNameMan.Length)];
            pic = DataManager.mHeroDict[pid].PicMan;
        }
        else
        {
            name = DataManager.mNameWoman[Random.Range(0, DataManager.mNameWoman.Length)];
            pic = DataManager.mHeroDict[pid].PicWoman;
        }

        int hp = SetAttr(Attribute.Hp, pid);
        int mp = SetAttr(Attribute.Mp, pid);
        int hpRenew = 0;
        int mpRenew = 0;
        int atkMax = SetAttr(Attribute.AtkMax, pid);
        int atkMin = atkMax - 2;
        int mAtkMax = SetAttr(Attribute.MAtkMax, pid);
        int mAtkMin = mAtkMax - 2;
        int def = SetAttr(Attribute.Def, pid);
        int mDef = SetAttr(Attribute.MDef, pid);
        int hit = SetAttr(Attribute.Hit, pid);
        int dod = SetAttr(Attribute.Dod, pid);
        int criR = SetAttr(Attribute.CriR, pid);
        int criD = 200;
        int spd = 80;
        int windDam = 100;
        int fireDam = 100;
        int waterDam = 100;
        int groundDam = 100;
        int lightDam = 100;
        int darkDam = 100;
        int windRes = 0;
        int fireRes = 0;
        int waterRes = 0;
        int groundRes = 0;
        int lightRes = 0;
        int darkRes = 0;
        int dizzyRes = 0;
        int confusionRes = 0;
        int poisonRes = 0;
        int sleepRes = 0;
        int goldGet = 0;
        int expGet = 0;
        int itemGet = 0;
        int workPlanting = SetAttr(Attribute.WorkPlanting, pid);
        int workFeeding = SetAttr(Attribute.WorkFeeding, pid);
        int workFishing = SetAttr(Attribute.WorkFishing, pid);
        int workHunting = SetAttr(Attribute.WorkHunting, pid);
        int workMining = SetAttr(Attribute.WorkMining, pid);
        int workQuarrying = SetAttr(Attribute.WorkQuarrying, pid);
        int workFelling = SetAttr(Attribute.WorkFelling, pid);
        int workBuild = SetAttr(Attribute.WorkBuild, pid);
        int workMakeWeapon = SetAttr(Attribute.WorkMakeWeapon, pid);
        int workMakeArmor = SetAttr(Attribute.WorkMakeArmor, pid);
        int workMakeJewelry = SetAttr(Attribute.WorkMakeJewelry, pid);
        int workSundry = SetAttr(Attribute.WorkSundry, pid);


       return new HeroObject(heroID, name, pid, 1,0, sexCode, pic, hp,mp,hpRenew,mpRenew,atkMin,atkMax,mAtkMin,mAtkMax,def,mDef,hit,dod,criR,criD,spd,
         windDam,fireDam,waterDam,groundDam,lightDam,darkDam,windRes,fireRes,waterRes,groundRes,lightRes,darkRes,dizzyRes,confusionRes,poisonRes,sleepRes,goldGet,expGet,itemGet,
         workPlanting,workFeeding,workFishing,workHunting,workMining,workQuarrying,workFelling,workBuild,workMakeWeapon,workMakeArmor,workMakeJewelry,workSundry,
         -1,-1,-1,-1,-1,-1,-1,-1,-1, -1);

}

    public HeroObject GenerateHeroByMould(int heroID, int pid, int sexCode,string nameSet)
    {

        string name = "";
        string pic = "";
        if (sexCode == 0)
        {
            name = DataManager.mNameMan[Random.Range(0, DataManager.mNameMan.Length)];
            pic = DataManager.mHeroDict[pid].PicMan;
        }
        else
        {
            name = DataManager.mNameWoman[Random.Range(0, DataManager.mNameWoman.Length)];
            pic = DataManager.mHeroDict[pid].PicWoman;
        }

        int hp = DataManager.mHeroDict[pid].Hp;
        int mp = DataManager.mHeroDict[pid].Mp;
        int hpRenew = DataManager.mHeroDict[pid].HpRenew;
        int mpRenew = DataManager.mHeroDict[pid].MpRenew;
        int atkMax = DataManager.mHeroDict[pid].AtkMin;
        int atkMin = DataManager.mHeroDict[pid].AtkMax;
        int mAtkMax = DataManager.mHeroDict[pid].MAtkMin;
        int mAtkMin = DataManager.mHeroDict[pid].MAtkMax;
        int def = DataManager.mHeroDict[pid].Def;
        int mDef = DataManager.mHeroDict[pid].MDef;
        int hit = DataManager.mHeroDict[pid].Hit;
        int dod = DataManager.mHeroDict[pid].Dod;
        int criR = DataManager.mHeroDict[pid].CriR;
        int criD = DataManager.mHeroDict[pid].CriD;
        int spd = DataManager.mHeroDict[pid].Spd;
        int windDam = DataManager.mHeroDict[pid].WindDam+100;
        int fireDam = DataManager.mHeroDict[pid].FireDam + 100;
        int waterDam = DataManager.mHeroDict[pid].WaterDam + 100;
        int groundDam = DataManager.mHeroDict[pid].GroundDam + 100;
        int lightDam = DataManager.mHeroDict[pid].LightDam + 100;
        int darkDam = DataManager.mHeroDict[pid].DarkDam + 100;
        int windRes = DataManager.mHeroDict[pid].WindRes + 100;
        int fireRes = DataManager.mHeroDict[pid].FireRes;
        int waterRes = DataManager.mHeroDict[pid].WaterRes;
        int groundRes = DataManager.mHeroDict[pid].GroundRes;
        int lightRes = DataManager.mHeroDict[pid].LightRes;
        int darkRes = DataManager.mHeroDict[pid].DarkRes;
        int dizzyRes = DataManager.mHeroDict[pid].DizzyRes;
        int confusionRes = DataManager.mHeroDict[pid].ConfusionRes;
        int poisonRes = DataManager.mHeroDict[pid].PoisonRes;
        int sleepRes = DataManager.mHeroDict[pid].SleepRes;
        int goldGet = DataManager.mHeroDict[pid].GoldGet;
        int expGet = DataManager.mHeroDict[pid].ExpGet;
        int itemGet = DataManager.mHeroDict[pid].ItemGet;
        int workPlanting = DataManager.mHeroDict[pid].WorkPlanting;
        int workFeeding = DataManager.mHeroDict[pid].WorkFeeding;
        int workFishing = DataManager.mHeroDict[pid].WorkFishing;
        int workHunting = DataManager.mHeroDict[pid].WorkHunting;
        int workMining = DataManager.mHeroDict[pid].WorkMining;
        int workQuarrying = DataManager.mHeroDict[pid].WorkQuarrying;
        int workFelling = DataManager.mHeroDict[pid].WorkFelling;
        int workBuild = DataManager.mHeroDict[pid].WorkBuild;
        int workMakeWeapon = DataManager.mHeroDict[pid].WorkMakeWeapon;
        int workMakeArmor = DataManager.mHeroDict[pid].WorkMakeArmor;
        int workMakeJewelry = DataManager.mHeroDict[pid].WorkMakeJewelry;
        int workSundry = DataManager.mHeroDict[pid].WorkSundry;


        return new HeroObject(heroID,nameSet!=""?nameSet:name, pid, 1, 0, sexCode, pic, hp, mp, hpRenew, mpRenew, atkMin, atkMax, mAtkMin, mAtkMax, def, mDef, hit, dod, criR, criD, spd,
          windDam, fireDam, waterDam, groundDam, lightDam, darkDam, windRes, fireRes, waterRes, groundRes, lightRes, darkRes, dizzyRes, confusionRes, poisonRes, sleepRes, goldGet, expGet, itemGet,
          workPlanting, workFeeding, workFishing, workHunting, workMining, workQuarrying, workFelling, workBuild, workMakeWeapon, workMakeArmor, workMakeJewelry, workSundry,
          -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);

    }

    public int SetAttr(Attribute attr, int pid)
    {
        int rank = 0;

        switch (attr)
        {
            case Attribute.Hp:rank = DataManager.mCreateHeroTypeDict[pid].Hp;break;
            case Attribute.Mp: rank = DataManager.mCreateHeroTypeDict[pid].Mp; break;

            case Attribute.AtkMax: rank = DataManager.mCreateHeroTypeDict[pid].AtkMax; break;
            case Attribute.MAtkMax: rank = DataManager.mCreateHeroTypeDict[pid].MAtkMax; break;

            case Attribute.Def: rank = DataManager.mCreateHeroTypeDict[pid].Def; break;
            case Attribute.MDef: rank = DataManager.mCreateHeroTypeDict[pid].MDef; break;

            case Attribute.Hit: rank = DataManager.mCreateHeroTypeDict[pid].Hit; break;
            case Attribute.Dod: rank = DataManager.mCreateHeroTypeDict[pid].Dod; break;
            case Attribute.CriR: rank = DataManager.mCreateHeroTypeDict[pid].CriR; break;


            case Attribute.WorkPlanting: rank = DataManager.mCreateHeroTypeDict[pid].WorkPlanting; break;
            case Attribute.WorkFeeding: rank = DataManager.mCreateHeroTypeDict[pid].WorkFeeding; break;
            case Attribute.WorkFishing: rank = DataManager.mCreateHeroTypeDict[pid].WorkFishing; break;
            case Attribute.WorkHunting: rank = DataManager.mCreateHeroTypeDict[pid].WorkHunting; break;
            case Attribute.WorkFelling: rank = DataManager.mCreateHeroTypeDict[pid].WorkFelling; break;
            case Attribute.WorkQuarrying: rank = DataManager.mCreateHeroTypeDict[pid].WorkQuarrying; break;
            case Attribute.WorkMining: rank = DataManager.mCreateHeroTypeDict[pid].WorkMining; break;
            case Attribute.WorkBuild: rank = DataManager.mCreateHeroTypeDict[pid].WorkBuild; break;
            case Attribute.WorkMakeWeapon: rank = DataManager.mCreateHeroTypeDict[pid].WorkMakeWeapon; break;
            case Attribute.WorkMakeArmor: rank = DataManager.mCreateHeroTypeDict[pid].WorkMakeArmor; break;
            case Attribute.WorkMakeJewelry: rank = DataManager.mCreateHeroTypeDict[pid].WorkMakeJewelry; break;
            case Attribute.WorkSundry: rank = DataManager.mCreateHeroTypeDict[pid].WorkSundry; break;
            default:
                rank = 999; break;
        }

        int probabilityCount = DataManager.cCreateHeroRankDict[attr].Probability.GetLength(1);

        //Debug.Log("  probabilityCount=" + probabilityCount );
        int ran = Random.Range(0, 100);
        int lj = 0;
        for (int i = 0; i < probabilityCount; i++)
        {
            //Debug.Log("pid=" + pid + "  i=" + i + "  probabilityCount=" + probabilityCount + "  attr=" + attr.ToString());
            // Debug.Log("DataManager.cCreateHeroRankDict[attr].Probability[pid, i]=" + DataManager.cCreateHeroRankDict[attr].Probability[pid, i]);
            //Debug.Log("DataManager.cCreateHeroRankDict[attr].Probability[pid, i - 1]=" + DataManager.cCreateHeroRankDict[attr].Probability[pid, i - 1]);
            lj += DataManager.cCreateHeroRankDict[attr].Probability[rank, i];

            if (ran < lj)
            {
                //Debug.Log("符合  DataManager.cCreateHeroRankDict[attr].Probability[rank, i]=" + DataManager.cCreateHeroRankDict[attr].Probability[rank, i]);
                //Debug.Log("符合   DataManager.cCreateHeroRankDict[attr].Value1[i]=" + DataManager.cCreateHeroRankDict[attr].Value1[i]);
                //Debug.Log("符合   DataManager.cCreateHeroRankDict[attr].Value2[i=" + DataManager.cCreateHeroRankDict[attr].Value2[i]);
                return Random.Range(DataManager.cCreateHeroRankDict[attr].Value1[i], DataManager.cCreateHeroRankDict[attr].Value2[i]);

            }
            else
            {
                //Debug.Log("rank=" + rank + "  i=" + i + "  attr=" + attr.ToString());
                //Debug.Log("不符合  Probability[rank, i]=" + DataManager.cCreateHeroRankDict[attr].Probability[rank, i]+ " Value1[i]=" + DataManager.cCreateHeroRankDict[attr].Value1[i]+ " Value2[i]=" + DataManager.cCreateHeroRankDict[attr].Value2[i]);

            }
        }
        Debug.Log("  ran=" + ran);
        return 0;
    }


    public void Build( int buildingId)
    {
        if (DataManager.mBuildingDict[buildingId].NeedWood > districtDic[nowCheckingDistrictID].rStuffWood)
        {
            return;
        }
        if (DataManager.mBuildingDict[buildingId].NeedStone > districtDic[nowCheckingDistrictID].rStuffStone)
        {
            return;
        }
        if (DataManager.mBuildingDict[buildingId].NeedMetal > districtDic[nowCheckingDistrictID].rStuffMetal)
        {
            return;
        }
        if (DataManager.mBuildingDict[buildingId].NeedGold > gold)
        {
            return;
        }
        if (DataManager.mBuildingDict[buildingId].Grid > districtDic[nowCheckingDistrictID].gridEmpty)
        {
            return;
        }
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
        int count = DataManager.mBuildingDict[buildingId].Grid;
        foreach (KeyValuePair<int, DistrictGridObject> kvp in districtGridDic)
        {
            if (DataManager.mDistrictGridDict[kvp.Value.id].DistrictID == nowCheckingDistrictID&& DataManager.mDistrictGridDict[kvp.Value.id].Level<= districtDic[nowCheckingDistrictID].level)
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

        districtDic[nowCheckingDistrictID].rStuffWood -= DataManager.mBuildingDict[buildingId].NeedWood;
        districtDic[nowCheckingDistrictID].rStuffStone -= DataManager.mBuildingDict[buildingId].NeedStone;
        districtDic[nowCheckingDistrictID].rStuffMetal -= DataManager.mBuildingDict[buildingId].NeedMetal;
        gold -= DataManager.mBuildingDict[buildingId].NeedGold;

        districtDic[nowCheckingDistrictID].usedGrass += DataManager.mBuildingDict[buildingId].NatureGrass;
        districtDic[nowCheckingDistrictID].usedWood += DataManager.mBuildingDict[buildingId].NatureWood;
        districtDic[nowCheckingDistrictID].usedWater += DataManager.mBuildingDict[buildingId].NatureWater;
        districtDic[nowCheckingDistrictID].usedStone += DataManager.mBuildingDict[buildingId].NatureStone;
        districtDic[nowCheckingDistrictID].usedMetal += DataManager.mBuildingDict[buildingId].NatureMetal;

        districtDic[nowCheckingDistrictID].gridEmpty -= count;
        districtDic[nowCheckingDistrictID].gridUsed+= count;


        buildingDic.Add(buildingIndex, new BuildingObject(buildingIndex, DataManager.mBuildingDict[buildingId].Name, DataManager.mBuildingDict[buildingId].MainPic, DataManager.mBuildingDict[buildingId].MapPic, DataManager.mBuildingDict[buildingId].Des, DataManager.mBuildingDict[buildingId].Level, DataManager.mBuildingDict[buildingId].Expense, DataManager.mBuildingDict[buildingId].UpgradeTo, true, grid, new List<int> { },
            DataManager.mBuildingDict[buildingId].NatureGrass, DataManager.mBuildingDict[buildingId].NatureWood, DataManager.mBuildingDict[buildingId].NatureWater, DataManager.mBuildingDict[buildingId].NatureStone, DataManager.mBuildingDict[buildingId].NatureMetal,
            DataManager.mBuildingDict[buildingId].People, DataManager.mBuildingDict[buildingId].Worker, 0,
            DataManager.mBuildingDict[buildingId].EWind, DataManager.mBuildingDict[buildingId].EFire, DataManager.mBuildingDict[buildingId].EWater, DataManager.mBuildingDict[buildingId].EGround, DataManager.mBuildingDict[buildingId].ELight, DataManager.mBuildingDict[buildingId].EDark));
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
