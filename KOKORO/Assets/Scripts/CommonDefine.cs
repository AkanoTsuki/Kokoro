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
    public int ID;
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

//英雄原型T
[System.Serializable]
public class CreateHeroType
{
    public int ID;
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

//英雄原型
[System.Serializable]
public class HeroPrototype
{
    public int ID;

    public string Name;
    public string PicMan;
    public string PicWoman;
    public int Hp;
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

    public int HpGD;
    public int MpGD;
    public int HpRenewGD;
    public int MpRenewGD;
    public int AtkMinGD;
    public int AtkMaxGD;
    public int MAtkMinGD;
    public int MAtkMaxGD;
    public int DefGD;
    public int MDefGD;
    public int HitGD;
    public int DodGD;
    public int CriRGD;
    public int CriDGD;

    public int HpGU;
    public int MpGU;
    public int HpRenewGU;
    public int MpRenewGU;
    public int AtkMinGU;
    public int AtkMaxGU;
    public int MAtkMinGU;
    public int MAtkMaxGU;
    public int DefGU;
    public int MDefGU;
    public int HitGU;
    public int DodGU;
    public int CriRGU;
    public int CriDGU;

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

//英雄实例
public class HeroObject
{
    
    private int ID;
    private string Name;
    private int Type;//来源类型“herotype表”
    private int Level;
    private int Exp;
    private int Sex;
    private string Pic;
    private int Hp;//级别 0 1 2
    private int Mp;
    private int HpRenew;
    private int MpRenew;
    private int AtkMin;
    private int AtkMax;
    private int MAtkMin;
    private int MAtkMax;
    private int Def;
    private int MDef;
    private int Hit;
    private int Dod;
    private int CriR;
    private int CriD;
    private int Spd;
    private int WindDam;
    private int FireDam;
    private int WaterDam;
    private int GroundDam;
    private int LightDam;
    private int DarkDam;
    private int WindRes;
    private int FireRes;
    private int WaterRes;
    private int GroundRes;
    private int LightRes;
    private int DarkRes;
    private int DizzyRes;
    private int ConfusionRes;
    private int PoisonRes;
    private int SleepRes;
    private int GoldGet;
    private int ExpGet;
    private int ItemGet;
    private int WorkPlanting;
    private int WorkFeeding;
    private int WorkFishing;
    private int WorkHunting;
    private int WorkMining;
    private int WorkQuarrying;
    private int WorkFelling;
    private int WorkBuild;
    private int WorkMakeWeapon;
    private int WorkMakeArmor;
    private int WorkMakeJewelry;
    private int WorkSundry;
    private int EquipWeapon;
    private int EquipHead;
    private int EquipBody;
    private int EquipHand;
    private int EquipLower;
    private int EquipFoot;
    private int EquipNeck;
    private int EquipFinger1;
    private int EquipFinger2;

    public HeroObject(int id, string name, int type,  int level, int exp,int sex,string pic,
        int hp, int mp, int hpRenew, int mpRenew,
        int atkMin, int atkMax, int mAtkMin, int mAtkMax, int def, int mDef,
        int hit, int dod, int criR, int criD, int spd,
        int windDam, int fireDam, int waterDam, int groundDam, int LightDam, int darkDam,
        int windRes, int fireRes, int waterRes, int groundRes, int LightRes, int darkRes,
        int dizzyRes, int confusionRes, int poisonRes, int sleepRes,
        int goldGet, int expGet, int itemGet,
        int workPlanting, int workFeeding, int workFishing, int workHunting, int workMining, int workQuarrying, int workFelling, int workBuild,
        int workMakeWeapon, int workMakeArmor, int workMakeJewelry,
        int workSundry,
        int equipWeapon, int equipHead, int equipBody, int equipHand, int equipLower, int equipFoot, int equipNeck, int equipFinger1, int equipFinger2)
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
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
        this.EquipWeapon = equipWeapon;
        this.EquipHead = equipHead;
        this.EquipBody = equipBody;
        this.EquipHand = equipHand;
        this.EquipLower = equipLower;
        this.EquipFoot = equipFoot;
        this.EquipNeck = equipNeck;
        this.EquipFinger1 = equipFinger1;
        this.EquipFinger2 = equipFinger2;

}
    public int id { get { return ID; } }
    public string name { get { return Name; } set { Name = value; } }
    public int type { get { return Type; } }
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
    public int equipWeapon { get { return EquipWeapon; } set { equipWeapon = value; } }
    public int equipHead { get { return EquipHead; } set { EquipHead = value; } }
    public int equipBody { get { return EquipBody; } set { EquipBody = value; } }
    public int equipHand { get { return EquipHand; } set { EquipHand = value; } }
    public int equipLower { get { return EquipLower; } set { EquipLower = value; } }
    public int equipFoot { get { return EquipFoot; } set { EquipFoot = value; } }
    public int equipNeck { get { return EquipNeck; } set { EquipNeck = value; } }
    public int equipFinger1 { get { return EquipFinger1; } set { EquipFinger1 = value; } }
    public int equipFinger2 { get { return EquipFinger2; } set { EquipFinger2 = value; } }
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
[System.Serializable]
public class DistrictPrototype
{
    public int ID;
    public string Name;
    public string Des;
    public List<int> Grass;
    public List<int> Wood;
    public List<int> Water;
    public List<int> Stone;
    public List<int> Metal;
    public int EWind;
    public int EFire;
    public int EWater;
    public int EGround;
    public int ELight;
    public int EDark;
}
[System.Serializable]
public class DistrictGridPrototype
{
    public string Name;
    public int DistrictID;
    public int Level;
    public int X;
    public int Y;
}

public class DistrictObject
{
    public int ID;
    public string Name;
    public string Des;
    public int[] TotalGrass;
    public int[] TotalWood;
    public int[] TotalWater;
    public int[] TotalStone;
    public int[] TotalMetal;
    public int UsedGrass;
    public int UsedWood;
    public int UsedWater;
    public int UsedStone;
    public int UsedMetal;
    public List<int> BuildingList;
    public int EWind;
    public int EFire;
    public int EWater;
    public int EGround;
    public int ELight;
    public int EDark;
}

public class DistrictGridObject
{
    public string Name;
    public int DistrictID;
    public int Level;
    public int BuildingID;//-2未开放 -1未使用
    public int X;
    public int Y;
}


//建筑物原型
[System.Serializable]
public class BuildingPrototype
{
    public int ID;
    public string Name;
    public string MainPic;
    public string MapPic;
    public int Level;
    public int NeedGold;
    public int NeedWood;
    public int NeedStone;
    public int NeedMetal;
    public int Expense;
    public int Grid;
    public int NatureGrass;
    public int NatureWood;
    public int NatureWater;
    public int NatureStone;
    public int NatureMetal;
    public int People;
    public int Worker;
    public int EWind;
    public int EFire;
    public int EWater;
    public int EGround;
    public int ELight;
    public int EDark;
}

//地牢原型
[System.Serializable]
public class DungeonPrototype
{
    public int ID;
    public string Name;
    public int Level;
    public string Des;
    public string Monster;
}

//怪物原型
[System.Serializable]
public class MonsterPrototype
{
    public int ID;
    public string Name;
    public int Level;
    public string Des;
}


public class CommonDefine : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {
        
    }


}
