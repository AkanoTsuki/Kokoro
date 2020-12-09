using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType
{
    Warrior,
    Magician,
    Archer,
    Producer,
    Maker,
    Backman
}

public enum ItemTypeBig
{
    Weapon,
    Armor
}
public enum ItemTypeSmall
{
    Sword,
    Hammer,
    Spear,
    Axe,
    Bow, 
    Staff,
    HeadH,
    HeadL,
    BodyH,
    BodyL,
    HandH,
    HandL,
    LowerH,
    LowerL,
    FootH,
    FootL
}
public enum Attribute
{
    Hp,
    Mp,
    HpRenew,
    MpRenew,
    AtkMin,
    AtkMax,
    MAtkMin,
    MAtkMax,
    Def,
    MDef,
    Hit,
    Dod,
    CriR,
    CriD,
    Spd,
    WindDam,
    FireDam,
    WaterDam,
    GroundDam,
    LightDam,
    DarkDam,
    WindRes,
    FireRes,
    WaterRes,
    GroundRes,
    LightRes,
    DarkRes,
    DizzyRes,
    ConfusionRes,
    PoisonRes,
    SleepRes,
    GoldGet,
    ExpGet,
    ItemGet,
    WorkPlanting, 
    WorkFeeding, 
    WorkFishing,
    WorkHunting, 
    WorkMining, 
    WorkQuarrying, 
    WorkFelling, 
    WorkBuild, 
    WorkMakeWeapon, 
    WorkMakeArmor, 
    WorkMakeJewelry, 
    WorkSundry
}

public enum AttributeSource
{
    Basic,
    RandomAdd,
    LemmaAdd
}

public class ItemAttribute
{
    private Attribute Attr;
    private AttributeSource AttrS;
    private int Value;
    public ItemAttribute(Attribute attr, AttributeSource attrS, int value)
    {
        this.Attr = attr;
        this.AttrS = attrS;
        this.Value = value;
    }
    public Attribute attr { get { return Attr; } }
    public AttributeSource attrS { get { return AttrS; } }
    public int value { get { return Value; } }
}

//物品原型
[System.Serializable]
public class ItemPrototype
{
    public string PrototypeID;
    public string Name;
}

//物品实例
public class ItemObject
{
    private int ObjectID;
    private int PrototypeID;
    private string Name;
    private int Rank;
    private List<ItemAttribute> Attr;
    private string Des;
    private int Cost;
    public ItemObject(int objectID, int prototypeID, string name, int rank, List<ItemAttribute> attr, string des, int cost)
    {
        this.ObjectID = objectID;
        this.PrototypeID = prototypeID;
        this.Name = name;
        this.Rank = rank;
        this.Attr = attr;
        this.Des = des;
        this.Cost = cost;
    }
    public int objectID { get { return ObjectID; } }
    public int prototypeID { get { return PrototypeID; } }
    public string name { get { return Name; } }
    public int rank { get { return Rank; } }
    public List<ItemAttribute> attr { get { return Attr; } }
    public string des { get { return Des; } }
    public int cost { get { return Cost; } }
}

//英雄原型
[System.Serializable]
public class CreateHeroType
{
    public string PrototypeID;
    public string Name;
    public int Hp;//级别 0 1 2
    public int Mp;
    public int HpRenew;
    public int MpRenew;
    public int AtkMin;
    public int AtkMax;
    public int MAtkMin;
    public int MAtkMax;
    public int Def;
    public int MDef;
    public int Hit;
    public int Dod;
    public int CriR;
    public int CriD;
    public int Spd;
    public int WindDam;
    public int FireDam;
    public int WaterDam;
    public int GroundDam;
    public int LightDam;
    public int DarkDam;
    public int WindRes;
    public int FireRes;
    public int WaterRes;
    public int GroundRes;
    public int LightRes;
    public int DarkRes;
    public int DizzyRes;
    public int ConfusionRes;
    public int PoisonRes;
    public int SleepRes;
    public int GoldGet;
    public int ExpGet;
    public int ItemGet;
    public int WorkPlanting;
    public int WorkFeeding;
    public int WorkFishing;
    public int WorkHunting;
    public int WorkMining;
    public int WorkQuarrying;
    public int WorkFelling;
    public int WorkBuild;
    public int WorkMakeWeapon;
    public int WorkMakeArmor;
    public int WorkMakeJewelry;
    public int WorkSundry;

   


}

public class HeroObject
{
    public int ID;
    public string Name;
    public int Level;
    public int Exp;
    public int Sex;
    public string Pic;
    public int Hp;//级别 0 1 2
    public int Mp;
    public int HpRenew;
    public int MpRenew;
    public int AtkMin;
    public int AtkMax;
    public int MAtkMin;
    public int MAtkMax;
    public int Def;
    public int MDef;
    public int Hit;
    public int Dod;
    public int CriR;
    public int CriD;
    public int Spd;
    public int WindDam;
    public int FireDam;
    public int WaterDam;
    public int GroundDam;
    public int LightDam;
    public int DarkDam;
    public int WindRes;
    public int FireRes;
    public int WaterRes;
    public int GroundRes;
    public int LightRes;
    public int DarkRes;
    public int DizzyRes;
    public int ConfusionRes;
    public int PoisonRes;
    public int SleepRes;
    public int GoldGet;
    public int ExpGet;
    public int ItemGet;
    public int WorkPlanting;
    public int WorkFeeding;
    public int WorkFishing;
    public int WorkHunting;
    public int WorkMining;
    public int WorkQuarrying;
    public int WorkFelling;
    public int WorkBuild;
    public int WorkMakeWeapon;
    public int WorkMakeArmor;
    public int WorkMakeJewelry;
    public int WorkSundry;

    public HeroObject(int id, string name,int level,int exp,int sex,string pic,
        int hp, int mp, int hpRenew, int mpRenew,
        int atkMin, int atkMax, int mAtkMin, int mAtkMax, int def, int mDef,
        int hit, int dod, int criR, int criD, int spd,
        int windDam, int fireDam, int waterDam, int groundDam, int LightDam, int darkDam,
        int windRes, int fireRes, int waterRes, int groundRes, int LightRes, int darkRes,
        int dizzyRes, int confusionRes, int poisonRes, int sleepRes,
        int goldGet, int expGet, int itemGet,
        int workPlanting, int workFeeding, int workFishing, int workHunting, int workMining, int workQuarrying, int workFelling, int workBuild,
        int workMakeWeapon, int workMakeArmor, int workMakeJewelry,
        int workSundry)
    {
        this.ID = id;
        this.Name = name;
        this.Level = level;
        this.Exp = exp;
        this.Sex = sex;
        this.Pic = pic;
        this.Hp = hp;
        this.Mp = mp;
        this.HpRenew = hpRenew;
        this.MpRenew = mpRenew;
        this.AtkMin = atkMin;
        this.AtkMax = atkMax;
        this.MAtkMin = mAtkMin;
        this.MAtkMax = mAtkMax;
        this.Def = def;
        this.MDef = mDef;
        this.Hit = hit;
        this.Dod = dod;
        this.CriR = criR;
        this.CriD = criD;
        this.Spd = spd;
        this.WindDam = windDam;
        this.FireDam = fireDam;
        this.WaterDam = waterDam;
        this.GroundDam = groundDam;
        this.LightDam = LightDam;
        this.DarkDam = darkDam;
        this.WindRes = windRes;
        this.FireRes = fireRes;
        this.WaterRes = waterRes;
        this.GroundRes = groundRes;
        this.LightRes = LightRes;
        this.DarkRes = darkRes;
        this.DizzyRes = dizzyRes;
        this.ConfusionRes = confusionRes;
        this.PoisonRes = poisonRes;
        this.SleepRes = sleepRes;
        this.GoldGet = goldGet;
        this.ExpGet = expGet;
        this.ItemGet = itemGet;
        this.WorkPlanting = workPlanting;
        this.WorkFeeding = workFeeding;
        this.WorkFishing = workFishing;
        this.WorkHunting = workHunting;
        this.WorkMining = workMining;
        this.WorkQuarrying = workQuarrying;
        this.WorkFelling = workFelling;
        this.WorkBuild = workBuild;
        this.WorkMakeWeapon = workMakeWeapon;
        this.WorkMakeArmor = workMakeArmor;
        this.WorkMakeJewelry = workMakeJewelry;
        this.WorkSundry = workSundry;


    }
    public int id { get { return ID; } }
    public string name { get { return Name; } set { Name = value; } }
    public int level { get { return Level; } set { Level = value; } }
    public int exp { get { return Exp; } set { Exp = value; } }
    public int sex { get { return Sex; } set { Sex = value; } }
    public string pic { get { return Pic; } set { Pic = value; } }
    public int hp { get { return Hp; } set { Hp = value; } }
    public int mp { get { return Mp; } set { Mp = value; } }
    public int hpRenew { get { return HpRenew; } set { HpRenew = value; } }
    public int mpRenew { get { return MpRenew; } set { MpRenew = value; } }
    public int atkMin { get { return AtkMin; } set { AtkMin = value; } }
    public int atkMax { get { return AtkMax; } set { AtkMax = value; } }
    public int mAtkMin { get { return MAtkMin; } set { MAtkMin = value; } }
    public int mAtkMax { get { return MAtkMax; } set { MAtkMax = value; } }
    public int def { get { return Def; } set { Def = value; } }
    public int mDef { get { return MDef; } set { MDef = value; } }
    public int hit { get { return Hit; } set { Hit = value; } }
    public int dod { get { return Dod; } set { Dod = value; } }
    public int criR { get { return CriR; } set { CriR = value; } }
    public int criD { get { return CriD; } set { CriD = value; } }
    public int spd { get { return Spd; } set { Spd = value; } }
    public int windDam { get { return WindDam; } set { WindDam = value; } }
    public int fireDam { get { return FireDam; } set { FireDam = value; } }
    public int waterDam { get { return WaterDam; } set { WaterDam = value; } }
    public int groundDam { get { return GroundDam; } set { GroundDam = value; } }
    public int lightDam { get { return LightDam; } set { LightDam = value; } }
    public int darkDam { get { return DarkDam; } set { DarkDam = value; } }
    public int windRes { get { return WindRes; } set { WindRes = value; } }
    public int fireRes { get { return FireRes; } set { FireRes = value; } }
    public int waterRes { get { return WaterRes; } set { WaterRes = value; } }
    public int groundRes { get { return GroundRes; } set { GroundRes = value; } }
    public int lightRes { get { return LightRes; } set { LightRes = value; } }
    public int darkRes { get { return DarkRes; } set { DarkRes = value; } }
    public int dizzyRes { get { return DizzyRes; } set { DizzyRes = value; } }
    public int confusionRes { get { return ConfusionRes; } set { ConfusionRes = value; } }
    public int poisonRes { get { return PoisonRes; } set { PoisonRes = value; } }
    public int sleepRes { get { return SleepRes; } set { SleepRes = value; } }
    public int goldGet { get { return GoldGet; } set { GoldGet = value; } }
    public int expGet { get { return ExpGet; } set { ExpGet = value; } }
    public int itemGet { get { return ItemGet; } set { ItemGet = value; } }
    public int workPlanting { get { return WorkPlanting; } set { WorkPlanting = value; } }
    public int workFeeding { get { return WorkFeeding; } set { WorkFeeding = value; } }
    public int workFishing { get { return WorkFishing; } set { WorkFishing = value; } }
    public int workHunting { get { return WorkHunting; } set { WorkHunting = value; } }
    public int workMining { get { return WorkMining; } set { WorkMining = value; } }
    public int workQuarrying { get { return WorkQuarrying; } set { WorkQuarrying = value; } }
    public int workFelling { get { return WorkFelling; } set { WorkFelling = value; } }
    public int workBuild { get { return WorkBuild; } set { WorkBuild = value; } }
    public int workMakeWeapon { get { return WorkMakeWeapon; } set { WorkMakeWeapon = value; } }
    public int workMakeArmor { get { return WorkMakeArmor; } set { WorkMakeArmor = value; } }
    public int workMakeJewelry { get { return WorkMakeJewelry; } set { WorkMakeJewelry = value; } }
    public int workSundry { get { return WorkSundry; } set { WorkSundry = value; } }

}

public class CreateHeroRank
{
    public int[] Value1;
    public int[] Value2;
    public int[,] Probability;
    public CreateHeroRank(int[] value1,int[] value2,int[,] probability)
    {
        this.Value1 = value1;
        this.Value2 = value2;
        this.Probability = probability;
    }
    public int[] value1 { get { return Value1; } }
    public int[] value2 { get { return Value2; } }
    public int[,] probability { get { return Probability; } }
}


public class CommonDefine : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {
        
    }


}
