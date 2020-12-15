using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//库存资源类型
public enum StuffType
{
Cereal,
Vegetable,
Fruit,
Meat,
Fish,
Wood,
Stone,
Metal,
Leather,
Cloth,
Twine,
Bone,
}

//交互小标签类型
public enum LabelType
{
    NewGameLeader,
    NewGameHero,
    Item,
    BuildingInDistrictMain,
    HeroInDis,
    BuildingInBuild,
    Message
}

public enum Element
{
    None,
    Wind,
    Fire,
    Water,
    Ground,
    Light,
    Dark
}

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


public enum LogType
{
    Info,
    BuildDone,
    ProduceDone
}

public enum ExecuteEventType
{
    Build,
    ProduceItem,
    ProduceResource,
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
    public string Pic;
    public string TypeBig;
    public string TypeSmall;
    public string Des;
    public int Cost;
    public int Rank;
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
}

//物品实例
public class ItemObject
{
    private int ObjectID;
    private int PrototypeID;
    private string Name;
    private string Pic;
    private int Rank;
    private int Level;
    private List<ItemAttribute> Attr;
    private string Des;
    private int Cost;
    public ItemObject(int objectID, int prototypeID, string name, string pic, int rank, int level, List<ItemAttribute> attr, string des, int cost)
    {
        this.ObjectID = objectID;
        this.PrototypeID = prototypeID;
        this.Name = name;
        this.Pic = pic;
        this.Rank = rank;
        this.Level = level;
        this.Attr = attr;
        this.Des = des;
        this.Cost = cost;
    }
    public int objectID { get { return ObjectID; } }
    public int prototypeID { get { return PrototypeID; } }
    public string name { get { return Name; } }
    public string pic { get { return Pic; } }
    public int rank { get { return Rank; } }
    public int level { get { return Level; } set { Level = value; } }
    public List<ItemAttribute> attr { get { return Attr; } }
    public string des { get { return Des; } }
    public int cost { get { return Cost; } }
}


//技能原型
[System.Serializable]
public class SkillPrototype
{
    public int ID;
    public string Name;
    public string Pic;
    public List<int> Element;
    public string Des;
    public int Mp;
    public int Probability;
    public int Max;
    public bool FlagDamage;
    public int Atk;
    public int MAtk;
    public int Sword;
    public int Axe;
    public int Spear;
    public int Hammer;
    public int Bow;
    public int Staff;
    public int Wind;
    public int Fire;
    public int Water;
    public int Ground;
    public int Light;
    public int Dark;
    public bool FlagDebuff;
    public int Dizzy;
    public int DizzyValue;
    public int Confusion;
    public int ConfusionValue;
    public int Poison;
    public int PoisonValue;
    public int Sleep;
    public int SleepValue;
    public int Cure;
    public bool FlagBuff;
    public int UpAtk;
    public int UpMAtk;
    public int UpDef;
    public int UpMDef;
    public int UpHit;
    public int UpDod;
    public int UpCriD;
    public int UpWindDam;
    public int UpFireDam;
    public int UpWaterDam;
    public int UpGroundDam;
    public int UpLightDam;
    public int UpDarkDam;
    public int UpWindRes;
    public int UpFireRes;
    public int UpWaterRes;
    public int UpGroundRes;
    public int UpLightRes;
    public int UpDarkRes;
}

//英雄原型T
[System.Serializable]
public class CreateHeroType
{
    public int ID;
    public string Name;
    public string Color;
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
    private int WorkerInBuilding;
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
        int equipWeapon, int equipHead, int equipBody, int equipHand, int equipLower, int equipFoot, int equipNeck, int equipFinger1, int equipFinger2,int workerInBuilding)
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
        this.WorkerInBuilding = workerInBuilding;
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
    public int workerInBuilding { get { return WorkerInBuilding; } set { WorkerInBuilding = value; } }
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
    public int BigMapX;
    public int BigMapY;
    public int BigMapDesX;
    public int BigMapDesY;
    public List<int> StartGrid;
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
    public int ID;
    public string Name;
    public int DistrictID;
    public int Level;
    public int X;
    public int Y;
}

public class DistrictObject
{
    private int ID;
    private string Name;
    private string BaseName;
    private string Des;
    private bool IsOpen;
    private int Level;
    private int People;
    private int PeopleLimit;
    private int Worker;
    private int GridEmpty;
    private int GridUsed;
    private int TotalGrass;
    private int TotalWood;
    private int TotalWater;
    private int TotalStone;
    private int TotalMetal;
    private int UsedGrass;
    private int UsedWood;
    private int UsedWater;
    private int UsedStone;
    private int UsedMetal;
    private List<int> BuildingList;
    private List<int> HeroList;
    private int EWind;
    private int EFire;
    private int EWater;
    private int EGround;
    private int ELight;
    private int EDark;
    private int RFoodCereal;//现有库存
    private int RFoodVegetable;
    private int RFoodFruit;
    private int RFoodMeat;
    private int RFoodFish;
    private int RStuffWood;
    private int RStuffMetal;
    private int RStuffStone;
    private int RStuffLeather;
    private int RStuffTwine;
    private int RStuffCloth;
    private int RStuffBone;
    private int RProductWeapon;
    private int RProductArmor;
    private int RProductJewelry;
    private int RFoodLimit;//库存上限
    private int RStuffLimit;
    private int RProductLimit;

    public DistrictObject(int id, string name, string baseName, string des, bool isOpen, int level, int people, int peopleLimit, int worker, int gridEmpty, int gridUsed,
        int totalGrass, int totalWood, int totalWater, int totalStone, int totalMetal, int usedGrass, int usedWood, int usedWater, int usedStone, int usedMetal, List<int> buildingList, List<int> heroList,
        int eWind, int eFire, int eWater, int eGround, int eLight, int eDark,
        int rFoodCereal, int rFoodVegetable, int rFoodFruit, int rFoodMeat, int rFoodFish,
        int rStuffWood, int rStuffMetal, int rStuffStone, int rStuffLeather, int rStuffTwine, int rStuffCloth, int rStuffBone,
        int rProductWeapon, int rProductArmor, int rProductJewelry,
        int rFoodLimit, int rStuffLimit, int rProductLimit)
    {
        this.ID = id;
        this.Name = name;
        this.BaseName = baseName;
        this.Des = des;
        this.IsOpen = isOpen;
        this.Level = level;
        this.People = people;
        this.PeopleLimit = peopleLimit;
        this.Worker = worker;
        this.GridEmpty = gridEmpty;
        this.GridUsed = gridUsed;
        this.TotalGrass = totalGrass;
        this.TotalWood = totalWood;
        this.TotalWater = totalWater;
        this.TotalStone = totalStone;
        this.TotalMetal = totalMetal;
        this.UsedGrass = usedGrass;
        this.UsedWood = usedWood;
        this.UsedWater = usedWater;
        this.UsedStone = usedStone;
        this.UsedMetal = usedMetal;
        this.BuildingList = buildingList;
        this.HeroList = heroList;
        this.EWind = eWind;
        this.EFire = eFire;
        this.EWater = eWater;
        this.EGround = eGround;
        this.ELight = eLight;
        this.EDark = eDark;
        this.RFoodCereal = rFoodCereal;//现有库存
        this.RFoodVegetable = rFoodVegetable;
        this.RFoodFruit = rFoodFruit;
        this.RFoodMeat = rFoodMeat;
        this.RFoodFish = rFoodFish;
        this.RStuffWood = rStuffWood;
        this.RStuffMetal = rStuffMetal;
        this.RStuffStone = rStuffStone;
        this.RStuffLeather = rStuffLeather;
        this.RStuffTwine = rStuffTwine;
        this.RStuffCloth = rStuffCloth;
        this.RStuffBone = rStuffBone;
        this.RProductWeapon = rProductWeapon;
        this.RProductArmor = rProductArmor;
        this.RProductJewelry = rProductJewelry;
        this.RFoodLimit = rFoodLimit;//库存上限
        this.RStuffLimit = rStuffLimit;
        this.RProductLimit = rProductLimit;
    }
    public int id { get { return ID; } }
    public string name { get { return Name; }  }
    public string baseName { get { return BaseName; } set { BaseName = value; } }
    public string des { get { return Des; } set { Des = value; } }
    public bool isOpen { get { return IsOpen; } set { IsOpen = value; } }
    public int level { get { return Level; } set { Level = value; } }
    public int people { get { return People; } set { People = value; } }
    public int peopleLimit { get { return PeopleLimit; } set { PeopleLimit = value; } }
    public int worker { get { return Worker; } set { Worker = value; } }
    public int gridEmpty { get { return GridEmpty; } set { GridEmpty = value; } }
    public int gridUsed { get { return GridUsed; } set { GridUsed = value; } }
    public int totalGrass { get { return TotalGrass; } set { TotalGrass = value; } }
    public int totalWood { get { return TotalWood; } set { TotalWood = value; } }
    public int totalWater { get { return TotalWater; } set { TotalWater = value; } }
    public int totalStone { get { return TotalStone; } set { TotalStone = value; } }
    public int totalMetal { get { return TotalMetal; } set { TotalMetal = value; } }
    public int usedGrass { get { return UsedGrass; } set { UsedGrass = value; } }
    public int usedWood { get { return UsedWood; } set { UsedWood = value; } }
    public int usedWater { get { return UsedWater; } set { UsedWater = value; } }
    public int usedStone { get { return UsedStone; } set { UsedStone = value; } }
    public int usedMetal { get { return UsedMetal; } set { UsedMetal = value; } }
    public List<int> buildingList { get { return BuildingList; } set { BuildingList = value; } }
    public List<int> heroList { get { return HeroList; } set { HeroList = value; } }
    public int eWind { get { return EWind; } set { EWind = value; } }
    public int eFire { get { return EFire; } set { EFire = value; } }
    public int eWater { get { return EWater; } set { EWater = value; } }
    public int eGround { get { return EGround; } set { EGround = value; } }
    public int eLight { get { return ELight; } set { ELight = value; } }
    public int eDark { get { return EDark; } set { EDark = value; } }
    public int rFoodCereal { get { return RFoodCereal; } set { RFoodCereal = value; } }
    public int rFoodVegetable { get { return RFoodVegetable; } set { RFoodVegetable = value; } }
    public int rFoodFruit { get { return RFoodFruit; } set { RFoodFruit = value; } }
    public int rFoodMeat { get { return RFoodMeat; } set { RFoodMeat = value; } }
    public int rFoodFish { get { return RFoodFish; } set { RFoodFish = value; } }
    public int rStuffWood { get { return RStuffWood; } set { RStuffWood = value; } }
    public int rStuffMetal { get { return RStuffMetal; } set { RStuffMetal = value; } }
    public int rStuffStone { get { return RStuffStone; } set { RStuffStone = value; } }
    public int rStuffLeather { get { return RStuffLeather; } set { RStuffLeather = value; } }
    public int rStuffTwine { get { return RStuffTwine; } set { RStuffTwine = value; } }
    public int rStuffCloth { get { return RStuffCloth; } set { RStuffCloth = value; } }
    public int rStuffBone { get { return RStuffBone; } set { RStuffBone = value; } }
    public int rProductWeapon { get { return RProductWeapon; } set { RProductWeapon = value; } }
    public int rProductArmor { get { return RProductArmor; } set { RProductArmor = value; } }
    public int rProductJewelry { get { return RProductJewelry; } set { RProductJewelry = value; } }
    public int rFoodLimit { get { return RFoodLimit; } set { RFoodLimit = value; } }
    public int rStuffLimit { get { return RStuffLimit; } set { RStuffLimit = value; } }
    public int rProductLimit { get { return RProductLimit; } set { RProductLimit = value; } }

}

public class DistrictGridObject
{
    private int ID;
    private string Pic;
    private int BuildingID;//-2未开放 -1未使用

    public DistrictGridObject(int id, string pic, int buildingID)
    {
        this.ID = id;
        this.Pic = pic;
        this.BuildingID = buildingID;
    }
    public int id { get { return ID; } }
    public string pic { get { return Pic; } set { Pic = value; } }
    public int buildingID { get { return BuildingID; } set { BuildingID = value; } }

}


//建筑物原型
[System.Serializable]
public class BuildingPrototype
{
    public int ID;
    public string Name;
    public string MainPic;
    public string MapPic;
    public string PanelType;
    public string Des;
    public int Level;
    public int NeedGold;
    public int NeedWood;
    public int NeedStone;
    public int NeedMetal;
    public int Expense;
    public int UpgradeTo;
    public int Grid;
    public int NatureGrass;
    public int NatureWood;
    public int NatureWater;
    public int NatureStone;
    public int NatureMetal;
    public int People;//提供人口
    public int Worker;//工人上限
    public int EWind;
    public int EFire;
    public int EWater;
    public int EGround;
    public int ELight;
    public int EDark;
}

//建筑实例
public class BuildingObject
{
    private int ID;
    private int DistrictID;
    private int PrototypeID;
    private string Name;
    private string MainPic;
    private string MapPic;
    private string PanelType;
    private string Des;
    private int Level;
    private int Expense;
    private int UpgradeTo;//升级后的建筑原型ID
    private bool IsOpen;
    private List<int> GridList;//占用格子ID
    private List<int> HeroList;
    private int NatureGrass;
    private int NatureWood;
    private int NatureWater;
    private int NatureStone;
    private int NatureMetal;
    private int People;//提供人口
    private int Worker;//工人上限
    private int WorkerNow;
    private int EWind;
    private int EFire;
    private int EWater;
    private int EGround;
    private int ELight;
    private int EDark;
    public BuildingObject(int id, int prototypeID, int districtID,string name, string mainPic, string mapPic, string panelType, string des, int level, int expense, int upgradeTo, bool isOpen, List<int> gridList, List<int> heroList,
        int natureGrass, int natureWood, int natureWater, int natureStone, int natureMetal, int people, int worker, int workerNow,
        int eWind, int eFire, int eWater, int eGround, int eLight, int eDark)
    {
        this.ID = id;
        this.PrototypeID = prototypeID;
        this.DistrictID = districtID;
        this.Name = name;
    this.MainPic = mainPic;
   this.MapPic = mapPic;
        this.PanelType = panelType;
        this.Des = des;
        this.Level = level;
    this.Expense = expense;
    this.UpgradeTo = upgradeTo;
    this.IsOpen = isOpen;
    this.GridList = gridList;
   this.HeroList = heroList;
    this.NatureGrass = natureGrass;
    this.NatureWood = natureWood;
    this.NatureWater = natureWater;
    this.NatureStone = natureStone;
    this.NatureMetal = natureMetal;
    this.People = people;
    this.Worker = worker;
    this.WorkerNow = workerNow;
    this.EWind = eWind;
    this.EFire = eFire;
    this.EWater = eWater;
    this.EGround = eGround;
    this.ELight = eLight;
    this.EDark = eDark;
}
    public int id{ get { return ID; } }
    public int prototypeID { get { return PrototypeID; } }
    public int districtID { get { return DistrictID; } }
    public string name{ get { return Name; } }
    public string mainPic { get { return MainPic; } }
    public string mapPic { get { return MapPic; } }
    public string panelType { get { return PanelType; } }
    public string des { get { return Des; } }
    public int level { get { return Level; } }
    public int expense { get { return Expense; } }
    public int upgradeTo { get { return UpgradeTo; } }
    public bool isOpen { get { return IsOpen; } set { IsOpen = value; } }
    public List<int> gridList { get { return GridList; } set { GridList = value; } }
    public List<int> heroList { get { return HeroList; } set { HeroList = value; } }
    public int natureGrass { get { return NatureGrass; } }
    public int natureWood { get { return NatureWood; } }
    public int natureWater { get { return NatureWater; } }
    public int natureStone { get { return NatureStone; } }
    public int natureMetal { get { return NatureMetal; } }
    public int people { get { return People; } }
    public int worker { get { return Worker; } }
    public int workerNow { get { return WorkerNow; } set { WorkerNow = value; } }
    public int eWind { get { return EWind; } }
    public int eFire { get { return EFire; } }
    public int eWater { get { return EWater; } }
    public int eGround { get { return EGround; } }
    public int eLight { get { return ELight; } }
    public int eDark { get { return EDark; } }

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

//日志消息实例
public class LogObject
{
    private int ID;
    private LogType Type;
    private int StandardTime;
    private string Text;
    private int Value1;
    private int Value2;
    private int Value3;

    public LogObject(int id, LogType type, int standardTime, string text, int value1, int value2, int value3)
    {
        this.ID = id;
        this.Type = type;
        this.StandardTime = standardTime;
        this.Text = text;
        this.Value1 = value1;
        this.Value2 = value2;
        this.Value3 = value3;

    }
    public int id { get { return ID; } }
    public LogType type { get { return Type; } }
    public int standardTime { get { return StandardTime; } }
    public string text { get { return Text; } }
    public int value1 { get { return Value1; } }
    public int value2 { get { return Value2; } }
    public int value3 { get { return Value3; } }

}


//装备词条原型
[System.Serializable]
public class LemmaPrototype
{
    public short ID;
    public string Name;
    public string Des;
    public List<short> Hp;//长度10，下标对应装备的rank确定增加的基本值
    public List<short> Mp;
    public List<short> HpRenew;
    public List<short> MpRenew;
    public List<short> AtkMin;
    public List<short> AtkMax;
    public List<short> MAtkMin;
    public List<short> MAtkMax;
    public List<short> Def;
    public List<short> MDef;
    public List<short> Hit;
    public List<short> Dod;
    public List<short> CriR;
    public List<short> CriD;
    public List<short> Spd;
    public List<short> WindDam;
    public List<short> FireDam;
    public List<short> WaterDam;
    public List<short> GroundDam;
    public List<short> LightDam;
    public List<short> DarkDam;
    public List<short> WindRes;
    public List<short> FireRes;
    public List<short> WaterRes;
    public List<short> GroundRes;
    public List<short> LightRes;
    public List<short> DarkRes;
    public List<short> DizzyRes;
    public List<short> ConfusionRes;
    public List<short> PoisonRes;
    public List<short> SleepRes;
    public List<short> GoldGet;
    public List<short> ExpGet;
    public List<short> ItemGet;
}

//装备生产关系原型
[System.Serializable]
public class ProduceEquipPrototype
{
    public short ID;
    public string Type;
    public byte Level;
    public byte InputWood;
    public byte InputStone;
    public byte InputMetal;
    public byte InputLeather;
    public byte InputCloth;
    public byte InputTwine;
    public byte InputBone;
    public List<short> OutputID;
    public List<short> OutputRate;

}

public class ExecuteEventObject
{

    private ExecuteEventType Type;
    private int StartTime;
    private int EndTime;


    private int Value1;
    private int Value2;
    private int Value3;
    private int Value4;
    private int Value5;
    public ExecuteEventObject( ExecuteEventType type, int startTime, int endTime, int value1, int value2, int value3, int value4, int value5)
    {

        this.Type = type;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Value1 = value1;
        this.Value2 = value2;
        this.Value3 = value3;
        this.Value4 = value4;
        this.Value3 = value5;
    }

    public ExecuteEventType type { get { return Type; } }
    public int startTime { get { return StartTime; } }
    public int endTime { get { return EndTime; } }
    public int value1 { get { return Value1; } }
    public int value2 { get { return Value2; } }
    public int value3 { get { return Value3; } }
    public int value4 { get { return Value4; } }
    public int value5 { get { return Value5; } }
}

public class CommonDefine : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {
        
    }


}
