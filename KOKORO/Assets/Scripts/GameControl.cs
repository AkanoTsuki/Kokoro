using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameControl : MonoBehaviour
{

    //save data
    public int heroIndex = 0;
    public string playerName = "";
    public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
    public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();

    /// <summary>
    /// 用作存档的数据类
    /// </summary>
    [System.Serializable]
    public class DataSave
    {
        public string playerName = "";
        public Dictionary<int, ItemObject> itemDic = new Dictionary<int, ItemObject>();
        public Dictionary<int, HeroObject> heroDic = new Dictionary<int, HeroObject>();
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

        t.playerName = this.playerName;
        t.itemDic = this.itemDic;
        t.heroDic = this.heroDic;
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

            this.playerName = t1.playerName;
            this.itemDic = t1.itemDic;
            this.heroDic = t1.heroDic;
            //IsNewGame = false;
        }
        else
        {
            print("文件不存在." + filename);
            //IsNewGame = true;
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
        Debug.Log("1heroDic.Count=" + heroDic.Count);
        CreateHero(1);
        Debug.Log("2heroDic.Count=" + heroDic.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateHero(int pid)
    {

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


        heroDic.Add(heroIndex,new HeroObject(heroIndex,"jack",1,0,0,"",hp,mp,hpRenew,mpRenew,atkMin,atkMax,mAtkMin,mAtkMax,def,mDef,hit,dod,criR,criD,spd,
         windDam,fireDam,waterDam,groundDam,lightDam,darkDam,windRes,fireRes,waterRes,groundRes,lightRes,darkRes,dizzyRes,confusionRes,poisonRes,sleepRes,goldGet,expGet,itemGet,
         workPlanting,workFeeding,workFishing,workHunting,workMining,workQuarrying,workFelling,workBuild,workMakeWeapon,workMakeArmor,workMakeJewelry,workSundry));

}
    public int SetAttr(Attribute attr, int pid)
    {
        //Debug.Log("attr=" + attr);
        int probabilityCount = DataManager.cCreateHeroRankDict[attr].Probability.GetLength(1);
        int ran = Random.Range(0, 100);
        for (int i = 0; i < probabilityCount; i++)
        {
            if (ran < DataManager.cCreateHeroRankDict[attr].Probability[pid, i] + (i != 0 ? DataManager.cCreateHeroRankDict[attr].Probability[pid, i - 1] : 0))
            {
                return Random.Range(DataManager.cCreateHeroRankDict[attr].Value1[i], DataManager.cCreateHeroRankDict[attr].Value2[i]);
                
            }
        }
        return 0;
    }

}
