using System.Collections.Generic;
using UnityEngine;

using System;
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
    Bone
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
    Message,
    HeroInSelect,
    HeroInSelectToCheck,
    EquipmentLook,
    EquipmentSet,
    ItemToSet,
    DungeonInAdventure,
    AdventureTeam,
    AdventurePart,
    Skill
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
    Armor,
    Jewelry,
    Subhand,
    SkillRoll,
    None
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
    BackH,
    BackL,
    FootH,
    FootL,
    Neck,
    Finger,
    Shield,
    Dorlach,
    None
}

public enum EquipPart
{ 
    None,
    Weapon,
    Subhand,
    Head,
    Body,
    Hand,
    Back,
    Foot,
    Neck,
    Finger1,
    Finger2
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
    GoldGet,//skill 作为 击杀获取金币 单位%
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

public enum AdventureState
{
    NotSend,
    Doing,
    Done,
    Fail,
    Retreat
}

public enum AdventureAction
{
    None,
    Walk,
    Fight,
    GetSomething
}

public enum AdventureEvent
{
    None,
    Monster,
    TrapHp,
    TrapMp,
    SpringHp,
    SpringMp,
    Gold,
    Item,
    Resource
}

public enum FightBuffType
{
    UpAtk,
    UpMAtk,
    UpDef,
    UpMDef,
    UpHit,
    UpDod,
    UpCriR,
    UpCriD,
    UpSpd,
    UpWindDam,
    UpFireDam,
    UpWaterDam,
    UpGroundDam,
    UpLightDam,
    UpDarkDam,
    UpWindRes,
    UpFireRes,
    UpWaterRes,
    UpGroundRes,
    UpLightRes,
    UpDarkRes,
    Dizzy,
    Confusion,
    Poison,
    Sleep
}

public enum AnimStatus
{
    Front,//正面静止
    Idle,
    WalkLeft,
    WalkRight,
    Attack,
    Bow,
    Magic,
    Hit,
    Death
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
    Adventure
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
public class ItemPrototype: ISerializationCallbackReceiver
{
    public int ID;
    public string Name;
    public string Pic;
    public ItemTypeBig TypeBig;
    public ItemTypeSmall TypeSmall;
    public string TypeBigStr;
    public string TypeSmallStr;
    public string Des;
    public int Cost;
    public byte Rank;
    public int Hp;
    public int Mp;
    public short HpRenew;
    public short MpRenew;
    public short AtkMin;
    public short AtkMax;
    public short MAtkMin;
    public short MAtkMax;
    public short Def;
    public short MDef;
    public short Hit;
    public short Dod;
    public short CriR;
    public short CriD;
    public short Spd;
    public short WindDam;
    public short FireDam;
    public short WaterDam;
    public short GroundDam;
    public short LightDam;
    public short DarkDam;
    public short WindRes;
    public short FireRes;
    public short WaterRes;
    public short GroundRes;
    public short LightRes;
    public short DarkRes;
    public short DizzyRes;
    public short ConfusionRes;
    public short PoisonRes;
    public short SleepRes;
    public byte GoldGet;
    public byte ExpGet;
    public byte ItemGet;
    public void OnAfterDeserialize()
    {
        ItemTypeSmall type = (ItemTypeSmall)Enum.Parse(typeof(ItemTypeSmall), TypeSmallStr);
        TypeSmall = type;
        ItemTypeBig type2 = (ItemTypeBig)Enum.Parse(typeof(ItemTypeBig), TypeBigStr);
        TypeBig = type2;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}

//物品实例
public class ItemObject
{
    private int ObjectID;
    private int PrototypeID;
    private string Name;
    private string Pic;
    private byte Rank;
    private byte Level;
    private List<ItemAttribute> Attr;
    private string Des;
    private int Cost;
    private short DistrictID;//如果是装备，存储在哪个地区的制品仓库，-1为整体收藏库
    private bool IsGoods;//是否商品（DistrictID！=-1）
    private int HeroID;//装备在哪个英雄身上，-1为未装备
    private EquipPart HeroPart;//（HeroID！=-1）
    public ItemObject(int objectID, int prototypeID, string name, string pic, byte rank, byte level, List<ItemAttribute> attr, string des, int cost, short districtID, bool isGoods, int heroID, EquipPart heroPart)
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
        this.DistrictID = districtID;
        this.IsGoods = isGoods;
        this.HeroID = heroID;
        this.HeroPart = heroPart;
    }
    public int objectID { get { return ObjectID; } }
    public int prototypeID { get { return PrototypeID; } }
    public string name { get { return Name; } }
    public string pic { get { return Pic; } }
    public byte rank { get { return Rank; } }
    public byte level { get { return Level; } set { Level = value; } }
    public List<ItemAttribute> attr { get { return Attr; } }
    public string des { get { return Des; } }
    public int cost { get { return Cost; } }
    public short districtID { get { return DistrictID; } set { DistrictID = value; } }
    public bool isGoods { get { return IsGoods; } set { IsGoods = value; } }
    public int heroID { get { return HeroID; } set { HeroID = value; } }
    public EquipPart heroPart { get { return HeroPart; } set { HeroPart = value; } }
}


//技能原型
[System.Serializable]
public class SkillPrototype: ISerializationCallbackReceiver
{
    public short ID;
    public string Name;
    public string Pic;
    public string Effect;
    public AnimStatus ActionAnim;
    public string ActionAnimStr;
    public List<int> Element;
    public string Des;
    public short Mp;//消耗魔法
    public byte Probability;
    public byte Max;
    public bool FlagDamage;
    public short Atk;
    public short MAtk;
    public short Sword;
    public short Axe;
    public short Spear;
    public short Hammer;
    public short Bow;
    public short Staff;
    public short Wind;
    public short Fire;
    public short Water;
    public short Ground;
    public short Light;
    public short Dark;
    public bool FlagDebuff;
    public short Dizzy;
    public short DizzyValue;
    public short Confusion;
    public short ConfusionValue;
    public short Poison;
    public short PoisonValue;
    public short Sleep;
    public short SleepValue;
    public short Cure;
    public bool FlagBuff;
    public short UpAtk;
    public short UpMAtk;
    public short UpDef;
    public short UpMDef;
    public short UpHit;
    public short UpDod;
    public short UpCriD;
    public short UpWindDam;
    public short UpFireDam;
    public short UpWaterDam;
    public short UpGroundDam;
    public short UpLightDam;
    public short UpDarkDam;
    public short UpWindRes;
    public short UpFireRes;
    public short UpWaterRes;
    public short UpGroundRes;
    public short UpLightRes;
    public short UpDarkRes;
    public void OnAfterDeserialize()
    {
        AnimStatus type = (AnimStatus)Enum.Parse(typeof(AnimStatus), ActionAnimStr);
        ActionAnim = type;

    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}

public class SkillObject
{
    private int ID;
    private string Name;
    private short PrototypeID;
    private short RateModify;
    private short MpModify;
    private byte ComboRate;
    private byte ComboMax;
    private byte Gold;
    private short Cost;
    private short DistrictID;
    private int HeroID;
    private int UseCount;
    public SkillObject(int id, string name, short prototypeID, short rateModify, short mpModify, byte comboRate, byte comboMax, byte gold, short cost, short districtID,int heroID, int useCount)
    {
        this.ID= id;
        this.Name= name;
        this.PrototypeID= prototypeID;
        this.RateModify = rateModify;
        this.MpModify= mpModify;
        this.ComboRate= comboRate;
        this.ComboMax= comboMax;
        this.Gold= gold;
        this.Cost= cost;
        this.DistrictID = districtID;
        this.HeroID= heroID;
        this.UseCount= useCount;
}
    public int id { get { return ID; }  }
    public string name { get { return Name; } set { Name = value; } }
    public short prototypeID { get { return PrototypeID; }  }
    public short rateModify { get { return RateModify; } set { RateModify = value; } }
    public short mpModify { get { return MpModify; } set { MpModify = value; } }
    public byte comboRate { get { return ComboRate; } set { ComboRate = value; } }
    public byte comboMax { get { return ComboMax; } set { ComboMax = value; } }
    public byte gold { get { return Gold; } set { Gold = value; } }
    public short cost { get { return Cost; } set { Cost = value; } }
    public short districtID { get { return DistrictID; } set { DistrictID = value; } }
    public int heroID { get { return HeroID; } set { HeroID = value; } }
    public int useCount { get { return UseCount; } set { UseCount = value; } }
}


//英雄原型T
[System.Serializable]
public class HeroPrototype
{
    public short ID;
    public string Name;
    public string Des;
    public string Color;
    public List<string> PicMan;
    public List<string> PicWoman;
    public float GroupRate;
    public byte Hp;//级别 0 1 2
    public byte Mp;
    public byte HpRenew;
    public byte MpRenew;
    public byte AtkMin;
    public byte AtkMax;
    public byte MAtkMin;
    public byte MAtkMax;
    public byte Def;
    public byte MDef;
    public byte Hit;
    public byte Dod;
    public byte CriR;
    public byte CriD;
    public byte Spd;
    public byte WindDam;
    public byte FireDam;
    public byte WaterDam;
    public byte GroundDam;
    public byte LightDam;
    public byte DarkDam;
    public byte WindRes;
    public byte FireRes;
    public byte WaterRes;
    public byte GroundRes;
    public byte LightRes;
    public byte DarkRes;
    public byte DizzyRes;
    public byte ConfusionRes;
    public byte PoisonRes;
    public byte SleepRes;
    public byte GoldGet;
    public byte ExpGet;
    public byte ItemGet;
    public byte WorkPlanting;
    public byte WorkFeeding;
    public byte WorkFishing;
    public byte WorkHunting;
    public byte WorkMining;
    public byte WorkQuarrying;
    public byte WorkFelling;
    public byte WorkBuild;
    public byte WorkMakeWeapon;
    public byte WorkMakeArmor;
    public byte WorkMakeJewelry;
    public byte WorkSundry;
}


public class HeroObject
{
    
    private int ID;
    private string Name;
    private short PrototypeID;
    private short Level;
    private int Exp;
    private byte Sex;
    private string Pic;
    private float GroupRate;
    private int Hp;
    private int Mp;
    private short HpRenew;
    private short MpRenew;
    private short AtkMin;
    private short AtkMax;
    private short MAtkMin;
    private short MAtkMax;
    private short Def;
    private short MDef;
    private short Hit;
    private short Dod;
    private short CriR;
    private short CriD;
    private short Spd;
    private short WindDam;
    private short FireDam;
    private short WaterDam;
    private short GroundDam;
    private short LightDam;
    private short DarkDam;
    private short WindRes;
    private short FireRes;
    private short WaterRes;
    private short GroundRes;
    private short LightRes;
    private short DarkRes;
    private short DizzyRes;
    private short ConfusionRes;
    private short PoisonRes;
    private short SleepRes;
    private byte GoldGet;
    private byte ExpGet;
    private byte ItemGet;
    private byte WorkPlanting;
    private byte WorkFeeding;
    private byte WorkFishing;
    private byte WorkHunting;
    private byte WorkMining;
    private byte WorkQuarrying;
    private byte WorkFelling;
    private byte WorkBuild;
    private byte WorkMakeWeapon;
    private byte WorkMakeArmor;
    private byte WorkMakeJewelry;
    private byte WorkSundry;
    private int EquipWeapon;
    private int EquipSubhand;
    private int EquipHead;
    private int EquipBody;
    private int EquipHand;
    private int EquipBack;
    private int EquipFoot;
    private int EquipNeck;
    private int EquipFinger1;
    private int EquipFinger2;
    private List<int> Skill;
    private int WorkerInBuilding;
    private short AdventureInTeam;
    public HeroObject(int id, string name, short prototypeID, short level, int exp,byte sex,string pic, float groupRate,
        int hp, int mp, short hpRenew, short mpRenew,
        short atkMin, short atkMax, short mAtkMin, short mAtkMax, short def, short mDef,
        short hit, short dod, short criR, short criD, short spd,
        short windDam, short fireDam, short waterDam, short groundDam, short lightDam, short darkDam,
        short windRes, short fireRes, short waterRes, short groundRes, short lightRes, short darkRes,
        short dizzyRes, short confusionRes, short poisonRes, short sleepRes,
        byte goldGet, byte expGet, byte itemGet,
        byte workPlanting, byte workFeeding, byte workFishing, byte workHunting, byte workMining, byte workQuarrying, byte workFelling, byte workBuild,
        byte workMakeWeapon, byte workMakeArmor, byte workMakeJewelry,
        byte workSundry,
        int equipWeapon, int equipSubhand, int equipHead, int equipBody, int equipHand, int equipBack, int equipFoot, int equipNeck, int equipFinger1, int equipFinger2, List<int> skill,int workerInBuilding, short adventureInTeam)
    {
        this.ID = id;
        this.Name = name;
        this.PrototypeID = prototypeID;
        this.Level = level;
        this.Exp = exp;
        this.Sex = sex;
        this.Pic = pic;
        this.GroupRate = groupRate;
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
        this.LightDam = lightDam;
        this.DarkDam = darkDam;
        this.WindRes = windRes;
        this.FireRes = fireRes;
        this.WaterRes = waterRes;
        this.GroundRes = groundRes;
        this.LightRes = lightRes;
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
        this.EquipSubhand = equipSubhand;
        this.EquipHead = equipHead;
        this.EquipBody = equipBody;
        this.EquipHand = equipHand;
        this.EquipBack = equipBack;
        this.EquipFoot = equipFoot;
        this.EquipNeck = equipNeck;
        this.EquipFinger1 = equipFinger1;
        this.EquipFinger2 = equipFinger2;
        this.Skill = skill;
        this.WorkerInBuilding = workerInBuilding;
        this.AdventureInTeam = adventureInTeam;
    }
    public int id { get { return ID; } }
    public string name { get { return Name; } set { Name = value; } }
    public short prototypeID { get { return PrototypeID; } }
    public short level { get { return Level; } set { Level = value; } }
    public int exp { get { return Exp; } set { Exp = value; } }
    public byte sex { get { return Sex; } set { Sex = value; } }
    public string pic { get { return Pic; } set { Pic = value; } }
    public float groupRate { get { return GroupRate; } set { GroupRate = value; } }
    public int hp { get { return Hp; } set { Hp = value; } }
    public int mp { get { return Mp; } set { Mp = value; } }
    public short hpRenew { get { return HpRenew; } set { HpRenew = value; } }
    public short mpRenew { get { return MpRenew; } set { MpRenew = value; } }
    public short atkMin { get { return AtkMin; } set { AtkMin = value; } }
    public short atkMax { get { return AtkMax; } set { AtkMax = value; } }
    public short mAtkMin { get { return MAtkMin; } set { MAtkMin = value; } }
    public short mAtkMax { get { return MAtkMax; } set { MAtkMax = value; } }
    public short def { get { return Def; } set { Def = value; } }
    public short mDef { get { return MDef; } set { MDef = value; } }
    public short hit { get { return Hit; } set { Hit = value; } }
    public short dod { get { return Dod; } set { Dod = value; } }
    public short criR { get { return CriR; } set { CriR = value; } }
    public short criD { get { return CriD; } set { CriD = value; } }
    public short spd { get { return Spd; } set { Spd = value; } }
    public short windDam { get { return WindDam; } set { WindDam = value; } }
    public short fireDam { get { return FireDam; } set { FireDam = value; } }
    public short waterDam { get { return WaterDam; } set { WaterDam = value; } }
    public short groundDam { get { return GroundDam; } set { GroundDam = value; } }
    public short lightDam { get { return LightDam; } set { LightDam = value; } }
    public short darkDam { get { return DarkDam; } set { DarkDam = value; } }
    public short windRes { get { return WindRes; } set { WindRes = value; } }
    public short fireRes { get { return FireRes; } set { FireRes = value; } }
    public short waterRes { get { return WaterRes; } set { WaterRes = value; } }
    public short groundRes { get { return GroundRes; } set { GroundRes = value; } }
    public short lightRes { get { return LightRes; } set { LightRes = value; } }
    public short darkRes { get { return DarkRes; } set { DarkRes = value; } }
    public short dizzyRes { get { return DizzyRes; } set { DizzyRes = value; } }
    public short confusionRes { get { return ConfusionRes; } set { ConfusionRes = value; } }
    public short poisonRes { get { return PoisonRes; } set { PoisonRes = value; } }
    public short sleepRes { get { return SleepRes; } set { SleepRes = value; } }
    public byte goldGet { get { return GoldGet; } set { GoldGet = value; } }
    public byte expGet { get { return ExpGet; } set { ExpGet = value; } }
    public byte itemGet { get { return ItemGet; } set { ItemGet = value; } }
    public byte workPlanting { get { return WorkPlanting; } set { WorkPlanting = value; } }
    public byte workFeeding { get { return WorkFeeding; } set { WorkFeeding = value; } }
    public byte workFishing { get { return WorkFishing; } set { WorkFishing = value; } }
    public byte workHunting { get { return WorkHunting; } set { WorkHunting = value; } }
    public byte workMining { get { return WorkMining; } set { WorkMining = value; } }
    public byte workQuarrying { get { return WorkQuarrying; } set { WorkQuarrying = value; } }
    public byte workFelling { get { return WorkFelling; } set { WorkFelling = value; } }
    public byte workBuild { get { return WorkBuild; } set { WorkBuild = value; } }
    public byte workMakeWeapon { get { return WorkMakeWeapon; } set { WorkMakeWeapon = value; } }
    public byte workMakeArmor { get { return WorkMakeArmor; } set { WorkMakeArmor = value; } }
    public byte workMakeJewelry { get { return WorkMakeJewelry; } set { WorkMakeJewelry = value; } }
    public byte workSundry { get { return WorkSundry; } set { WorkSundry = value; } }
    public int equipWeapon { get { return EquipWeapon; } set { EquipWeapon = value; } }
    public int equipSubhand { get { return EquipSubhand; } set { EquipSubhand = value; } }
    public int equipHead { get { return EquipHead; } set { EquipHead = value; } }
    public int equipBody { get { return EquipBody; } set { EquipBody = value; } }
    public int equipHand { get { return EquipHand; } set { EquipHand = value; } }
    public int equipBack { get { return EquipBack; } set { EquipBack = value; } }
    public int equipFoot { get { return EquipFoot; } set { EquipFoot = value; } }
    public int equipNeck { get { return EquipNeck; } set { EquipNeck = value; } }
    public int equipFinger1 { get { return EquipFinger1; } set { EquipFinger1 = value; } }
    public int equipFinger2 { get { return EquipFinger2; } set { EquipFinger2 = value; } }
    public List<int> skill { get { return Skill; } set { Skill = value; } }
    public int workerInBuilding { get { return WorkerInBuilding; } set { WorkerInBuilding = value; } }
    public short adventureInTeam { get { return AdventureInTeam; } set { AdventureInTeam = value; } }
}


public class CreateHeroRank
{
    public int[] Value1;
    public int[] Value2;
    public byte[,] Probability;
    public CreateHeroRank(int[] value1,int[] value2, byte[,] probability)
    {
        this.Value1 = value1;
        this.Value2 = value2;
        this.Probability = probability;
    }
    public int[] value1 { get { return Value1; } }
    public int[] value2 { get { return Value2; } }
    public byte[,] probability { get { return Probability; } }
}

[System.Serializable]
public class DistrictPrototype
{
    public short ID;
    public string Name;
    public string Des;
    public short BigMapX;
    public short BigMapY;
    public short BigMapDesX;
    public short BigMapDesY;
    public List<short> StartGrid;
    public List<short> Grass;
    public List<short> Wood;
    public List<short> Water;
    public List<short> Stone;
    public List<short> Metal;
    public short EWind;
    public short EFire;
    public short EWater;
    public short EGround;
    public short ELight;
    public short EDark;
}

[System.Serializable]
public class DistrictGridPrototype
{
    public int ID;
    public string Name;
    public short DistrictID;
    public byte Level;
    public short X;
    public short Y;
}

public class DistrictObject
{
    private short ID;
    private string Name;
    private string BaseName;
    private string Des;
    private bool IsOpen;
    private byte Level;
    private short People;//当前人口
    private short PeopleLimit;//人口上限
    private short Worker;//正在工作
    private short GridEmpty;
    private short GridUsed;
    private short TotalGrass;
    private short TotalWood;
    private short TotalWater;
    private short TotalStone;
    private short TotalMetal;
    private short UsedGrass;
    private short UsedWood;
    private short UsedWater;
    private short UsedStone;
    private short UsedMetal;
    private List<int> BuildingList;
    private List<int> HeroList;
    private short EWind;
    private short EFire;
    private short EWater;
    private short EGround;
    private short ELight;
    private short EDark;
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
    private int RRollLimit;
    public DistrictObject(short id, string name, string baseName, string des, bool isOpen, byte level, short people, short peopleLimit, short worker, short gridEmpty, short gridUsed,
        short totalGrass, short totalWood, short totalWater, short totalStone, short totalMetal, short usedGrass, short usedWood, short usedWater, short usedStone, short usedMetal, List<int> buildingList, List<int> heroList,
        short eWind, short eFire, short eWater, short eGround, short eLight, short eDark,
        int rFoodCereal, int rFoodVegetable, int rFoodFruit, int rFoodMeat, int rFoodFish,
        int rStuffWood, int rStuffMetal, int rStuffStone, int rStuffLeather, int rStuffTwine, int rStuffCloth, int rStuffBone,
        int rProductWeapon, int rProductArmor, int rProductJewelry,
        int rFoodLimit, int rStuffLimit, int rProductLimit, int rRollLimit)
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
        this.RRollLimit = rRollLimit;
    }
    public short id { get { return ID; } }
    public string name { get { return Name; }  }
    public string baseName { get { return BaseName; } set { BaseName = value; } }
    public string des { get { return Des; } set { Des = value; } }
    public bool isOpen { get { return IsOpen; } set { IsOpen = value; } }
    public byte level { get { return Level; } set { Level = value; } }
    public short people { get { return People; } set { People = value; } }
    public short peopleLimit { get { return PeopleLimit; } set { PeopleLimit = value; } }
    public short worker { get { return Worker; } set { Worker = value; } }
    public short gridEmpty { get { return GridEmpty; } set { GridEmpty = value; } }
    public short gridUsed { get { return GridUsed; } set { GridUsed = value; } }
    public short totalGrass { get { return TotalGrass; } set { TotalGrass = value; } }
    public short totalWood { get { return TotalWood; } set { TotalWood = value; } }
    public short totalWater { get { return TotalWater; } set { TotalWater = value; } }
    public short totalStone { get { return TotalStone; } set { TotalStone = value; } }
    public short totalMetal { get { return TotalMetal; } set { TotalMetal = value; } }
    public short usedGrass { get { return UsedGrass; } set { UsedGrass = value; } }
    public short usedWood { get { return UsedWood; } set { UsedWood = value; } }
    public short usedWater { get { return UsedWater; } set { UsedWater = value; } }
    public short usedStone { get { return UsedStone; } set { UsedStone = value; } }
    public short usedMetal { get { return UsedMetal; } set { UsedMetal = value; } }
    public List<int> buildingList { get { return BuildingList; } set { BuildingList = value; } }
    public List<int> heroList { get { return HeroList; } set { HeroList = value; } }
    public short eWind { get { return EWind; } set { EWind = value; } }
    public short eFire { get { return EFire; } set { EFire = value; } }
    public short eWater { get { return EWater; } set { EWater = value; } }
    public short eGround { get { return EGround; } set { EGround = value; } }
    public short eLight { get { return ELight; } set { ELight = value; } }
    public short eDark { get { return EDark; } set { EDark = value; } }
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
    public int rRollLimit { get { return RRollLimit; } set { RRollLimit = value; } }
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
    public short ID;
    public string Name;
    public string MainPic;
    public string MapPic;
    public string PanelType;
    public string Des;
    public byte Level;
    public short BuildTime;//建造时间 单位小时
    public short NeedGold;
    public short NeedWood;
    public short NeedStone;
    public short NeedMetal;
    public int Expense;
    public short UpgradeTo;
    public byte Grid;
    public byte NatureGrass;
    public byte NatureWood;
    public byte NatureWater;
    public byte NatureStone;
    public byte NatureMetal;
    public short People;//提供人口
    public short Worker;//工人上限
    public byte EWind;
    public byte EFire;
    public byte EWater;
    public byte EGround;
    public byte ELight;
    public byte EDark;
}

//建筑实例
public class BuildingObject
{
    private int ID;
    private short DistrictID;
    private short PrototypeID;
    private string Name;
    private string MainPic;
    private string MapPic;
    private string PanelType;
    private string Des;
    private byte Level;
    private int Expense;
    private short UpgradeTo;//升级后的建筑原型ID
    private bool IsOpen;
    private List<int> GridList;//占用格子ID
    private List<int> HeroList;
    private byte NatureGrass;
    private byte NatureWood;
    private byte NatureWater;
    private byte NatureStone;
    private byte NatureMetal;
    private short People;//提供人口
    private short Worker;//工人上限
    private short WorkerNow;
    private byte EWind;
    private byte EFire;
    private byte EWater;
    private byte EGround;
    private byte ELight;
    private byte EDark;
    private short ProduceEquipNow;//当前生产的装备模板原型ID 如果是资源类则对应资源生产关系表
    private byte BuildProgress;//0建设中 1已完成
    public BuildingObject(int id, short prototypeID, short districtID,string name, string mainPic, string mapPic, string panelType, string des, byte level, int expense, short upgradeTo, bool isOpen, List<int> gridList, List<int> heroList,
        byte natureGrass, byte natureWood, byte natureWater, byte natureStone, byte natureMetal, short people, short worker, short workerNow,
        byte eWind, byte eFire, byte eWater, byte eGround, byte eLight, byte eDark,
        short produceEquipNow, byte buildProgress)
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
        this.ProduceEquipNow = produceEquipNow;
        this.BuildProgress = buildProgress;
    }
    public int id{ get { return ID; } }
    public short prototypeID { get { return PrototypeID; } set { PrototypeID = value; } }
    public short districtID { get { return DistrictID; } }
    public string name{ get { return Name; } set { Name = value; } }
    public string mainPic { get { return MainPic; } set { MainPic = value; } }
    public string mapPic { get { return MapPic; } set { MapPic = value; } }
    public string panelType { get { return PanelType; } set { PanelType = value; } }
    public string des { get { return Des; } set { Des = value; } }
    public byte level { get { return Level; } set { Level = value; } }
    public int expense { get { return Expense; } set { Expense = value; } }
    public short upgradeTo { get { return UpgradeTo; } set { UpgradeTo = value; } }
    public bool isOpen { get { return IsOpen; } set { IsOpen = value; } }
    public List<int> gridList { get { return GridList; } set { GridList = value; } }
    public List<int> heroList { get { return HeroList; } set { HeroList = value; } }
    public byte natureGrass { get { return NatureGrass; } set { NatureGrass = value; } }
    public byte natureWood { get { return NatureWood; } set { NatureWood = value; } }
    public byte natureWater { get { return NatureWater; } set { NatureWater = value; } }
    public byte natureStone { get { return NatureStone; } set { NatureStone = value; } }
    public byte natureMetal { get { return NatureMetal; } set { NatureMetal = value; } }
    public short people { get { return People; } set { People = value; } }
    public short worker { get { return Worker; } set { Worker = value; } }
    public short workerNow { get { return WorkerNow; } set { WorkerNow = value; } }
    public byte eWind { get { return EWind; } set { EWind = value; } }
    public byte eFire { get { return EFire; } set { EFire = value; } }
    public byte eWater { get { return EWater; } set { EWater = value; } }
    public byte eGround { get { return EGround; } set { EGround = value; } }
    public byte eLight { get { return ELight; } set { ELight = value; } }
    public byte eDark { get { return EDark; } set { EDark = value; } }
    public short produceEquipNow { get { return ProduceEquipNow; } set { ProduceEquipNow = value; } }
    public byte buildProgress { get { return BuildProgress; } set { BuildProgress = value; } }
}


//地牢原型
[System.Serializable]
public class DungeonPrototype : ISerializationCallbackReceiver
{
    public short ID;
    public string Name;
    public byte Level;
    public List<string> ScenePic;
    public string Des;
    public byte PartNum;
    public List<byte> FixPart;
    public List<AdventureEvent> FixEvent=new List<AdventureEvent>();
    public List<string> FixEventStr;
    public List<int> FixValue;
    public byte RandomMonster;
    public byte RandomTrapHp;
    public byte RandomTrapMp;
    public byte RandomSpringHp;
    public byte RandomSpringMp;
    public byte RandomGold;
    public byte RandomItem;
    public byte RandomResource;

    public List<StuffType> ResourceType=new List<StuffType>();
    public List<string> ResourceTypeStr;
    public List<int> ResourceValue;
    public List<int> MonsterID;
    public List<byte> MonsterRate;
    public List<byte> MonsterLevelMin;
    public List<byte> MonsterLevelMax;
    public List<byte> MonsterEliteRate;
    public List<byte> MonsterLeaderRate;
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < FixEventStr.Count; i++)
        {
            AdventureEvent fixPart = (AdventureEvent)Enum.Parse(typeof(AdventureEvent), FixEventStr[i]);
            FixEvent.Add(fixPart);
        }
        for (int i = 0; i < ResourceTypeStr.Count; i++)
        {
            StuffType resourceType = (StuffType)Enum.Parse(typeof(StuffType), ResourceTypeStr[i]);
            ResourceType.Add( resourceType);
        }
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}

//地牢实例
public class DungeonObject
{
    private short ID;
    private bool Unlock;
    public DungeonObject(short id, bool unlock)
    {
        this.ID = id;
        this.Unlock = unlock;
    }
    public short id { get { return ID; }  }
    public bool unlock { get { return Unlock; } set { Unlock = value; } }
}

//怪物原型
[System.Serializable]
public class MonsterPrototype
{
    public short ID;
    public string Name;
    public string Pic;
    public byte Level;
    public float GroupRate;
    public string Des;
    public List<short> SkillID;
    public int Hp;
    public int Mp;
    public short HpRenew;
    public short MpRenew;
    public short AtkMin;
    public short AtkMax;
    public short MAtkMin;
    public short MAtkMax;
    public short Def;
    public short MDef;
    public short Hit;
    public short Dod;
    public short CriR;
    public short CriD;
    public short Spd;
    public short WindDam;
    public short FireDam;
    public short WaterDam;
    public short GroundDam;
    public short LightDam;
    public short DarkDam;
    public short WindRes;
    public short FireRes;
    public short WaterRes;
    public short GroundRes;
    public short LightRes;
    public short DarkRes;
    public short DizzyRes;
    public short ConfusionRes;
    public short PoisonRes;
    public short SleepRes;
    public short ExpDrop;
    public short GoldDrop;
    public List<int> ItemDrop;
}

//探险队实例
public class AdventureTeamObject
{
    private byte ID;
    private short DungeonID;
    private List<string> ScenePicList;
    private List<int> HeroIDList;
    private List<int> HeroHpList;
    private List<int> HeroMpList;
    private List<int> EnemyIDList;
    private byte NowDay;
    private AdventureState State;
    private AdventureAction Action;
    private short GetExp;
    private short GetGold;
    private short GetCereal;
    private short GetVegetable;
    private short GetFruit;
    private short GetMeat;
    private short GetFish;
    private short GetWood;
    private short GetMetal;
    private short GetStone;
    private short GetLeather;
    private short GetCloth;
    private short GetTwine;
    private short GetBone;
    private List<int> GetItemList;
    private short KillNum;
    private List<string> Log;
    private List<AdventurePartObject> Part;
    public AdventureTeamObject(byte id,short dungeonID, List<string> scenePicList, List<int> heroIDList, List<int> heroHpList, List<int> heroMpList, List<int> enemyIDList, byte nowDay, AdventureState state, AdventureAction action,
        short getExp, short getGold, short getCereal, short getVegetable, short getFruit, short getMeat, short getFish, short getWood, short getMetal, short getStone, short getLeather, short getCloth,short getTwine, short getBone,
        List<int> getItemList, short killNum, List<string> log, List<AdventurePartObject> part)
    {
        this.ID = id;
        this.DungeonID = dungeonID;
        this.ScenePicList = scenePicList;
        this.HeroIDList = heroIDList;
        this.HeroHpList = heroHpList;
        this.HeroMpList = heroMpList;
        this.EnemyIDList = enemyIDList;
        this.NowDay = nowDay;
        this.State = state;
        this.Action = action;
        this.GetExp = getExp;
        this.GetGold = getGold;
        this.GetCereal = getCereal;
        this.GetVegetable = getVegetable;
        this.GetFruit = getFruit;
        this.GetMeat = getMeat;
        this.GetFish = getFish;
        this.GetWood = getWood;
        this.GetMetal = getMetal;
        this.GetStone = getStone;
        this.GetLeather = getLeather;
        this.GetCloth = getCloth;
        this.GetTwine = getTwine;
        this.GetBone = getBone;
        this.GetItemList = getItemList;
        this.KillNum = killNum;
        this.Log = log;
        this.Part = part;
    }
    public byte id { get { return ID; } }
    public short dungeonID { get { return DungeonID; } set { DungeonID = value; } }
    public List<string> scenePicList { get { return ScenePicList; } set { ScenePicList = value; } }
    public List<int> heroIDList { get { return HeroIDList; } set { HeroIDList = value; } }
    public List<int> heroHpList { get { return HeroHpList; } set { HeroHpList = value; } }
    public List<int> heroMpList { get { return HeroMpList; } set { HeroMpList = value; } }
    public List<int> enemyIDList { get { return EnemyIDList; } set { EnemyIDList = value; } }
    public byte nowDay { get { return NowDay; } set { NowDay = value; } }
    public AdventureState state { get { return State; } set { State = value; } }
    public AdventureAction action { get { return Action; } set { Action = value; } }
    public short getExp { get { return GetExp; } set { GetExp = value; } }
    public short getGold { get { return GetGold; } set { GetGold = value; } }
    public short getCereal { get { return GetCereal; } set { GetCereal = value; } }
    public short getVegetable { get { return GetVegetable; } set { GetVegetable = value; } }
    public short getFruit { get { return GetFruit; } set { GetFruit = value; } }
    public short getMeat { get { return GetMeat; } set { GetMeat = value; } }
    public short getFish { get { return GetFish; } set { GetFish = value; } }
    public short getWood { get { return GetWood; } set { GetWood = value; } }
    public short getMetal { get { return GetMetal; } set { GetMetal = value; } }
    public short getStone { get { return GetStone; } set { GetStone = value; } }
    public short getLeather { get { return GetLeather; } set { GetLeather = value; } }
    public short getCloth { get { return GetCloth; } set { GetCloth = value; } }
    public short getTwine { get { return GetTwine; } set { GetTwine = value; } }
    public short getBone { get { return GetBone; } set { GetBone = value; } }
    public List<int> getItemList { get { return GetItemList; } set { GetItemList = value; } }
    public short killNum { get { return KillNum; } set { KillNum = value; } }
    public List<string> log { get { return Log; } set { Log = value; } }
    public List<AdventurePartObject> part { get { return Part; } set { Part = value; } }
}

//探险队部分（暂定1部分=1天）实例
public class AdventurePartObject
{
    private byte ID;
    private AdventureEvent EventType;
    private bool IsPass;
    private List<int> HeroHpList;//结束后
    private List<int> HeroMpList;
    private string Log;
    public AdventurePartObject(byte id, AdventureEvent eventType, bool isPass, List<int> heroHpList, List<int> heroMpList, string log)
    {
        this.ID = id;
        this.EventType = eventType;
        this.IsPass = isPass;
        this.HeroHpList = heroHpList;
        this.HeroMpList = heroMpList;
        this.Log = log;
    }

    public byte id { get { return ID; } }
    public AdventureEvent eventType { get { return EventType; } set { EventType = value; } }
    public bool isPass { get { return IsPass; } set { IsPass = value; } }
    public List<int> heroHpList { get { return HeroHpList; } set { HeroHpList = value; } }
    public List<int> heroMpList { get { return HeroMpList; } set { HeroMpList = value; } }
    public string log { get { return Log; } set { Log = value; } }
}

public class FightObject
{
    private List<FightMenberObject> MenberList;
    private byte Round;
    private byte WinSide;//是否左边赢
    public FightObject(List<FightMenberObject> menberList,  byte round, byte winSide)
    {
        this.MenberList = menberList;
        this.Round = round;
        this.WinSide = winSide;
    }
    public List<FightMenberObject> menberList { get { return MenberList; } set { MenberList = value; } }
    public byte round { get { return Round; } set { Round = value; } }
    public byte winSide { get { return WinSide; } set { WinSide = value; } }
}

public class FightMenberObject
{
    private int ID;
    private int ObjectID;//对于己方hero实例ID,对于怪物为怪物原型ID
    private byte Side;
    private byte SideIndex;
    private string Name;
    private int Hp;
    private int Mp;
    private short HpRenew;
    private short MpRenew;
    private short AtkMin;
    private short AtkMax;
    private short MAtkMin;
    private short MAtkMax;
    private short Def;
    private short MDef;
    private short Hit;
    private short Dod;
    private short CriR;
    private short CriD;
    private short Spd;
    private short WindDam;
    private short FireDam;
    private short WaterDam;
    private short GroundDam;
    private short LightDam;
    private short DarkDam;
    private short WindRes;
    private short FireRes;
    private short WaterRes;
    private short GroundRes;
    private short LightRes;
    private short DarkRes;
    private short DizzyRes;
    private short ConfusionRes;
    private short PoisonRes;
    private short SleepRes;
    private ItemTypeSmall WeaponType;
    private short ActionBar;
    private byte SkillIndex;//当前招式位置
    private int HpNow;
    private int MpNow;
    private List<FightBuff> Buff;


    public FightMenberObject(int id, int objectID, byte side, byte sideIndex, string name,
         int hp, int mp, short hpRenew, short mpRenew,
        short atkMin, short atkMax, short mAtkMin, short mAtkMax, short def, short mDef,
        short hit, short dod, short criR, short criD, short spd,
        short windDam, short fireDam, short waterDam, short groundDam, short lightDam, short darkDam,
        short windRes, short fireRes, short waterRes, short groundRes, short lightRes, short darkRes,
        short dizzyRes, short confusionRes, short poisonRes, short sleepRes, ItemTypeSmall weaponType,
       short actionBar, byte skillIndex,int hpNow, int mpNow,  List<FightBuff> buff)
    {
        this.ID = id;
        this.ObjectID = objectID;
        this.Side = side;
        this.SideIndex = sideIndex;
        this.Name = name;
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
        this.LightDam = lightDam;
        this.DarkDam = darkDam;
        this.WindRes = windRes;
        this.FireRes = fireRes;
        this.WaterRes = waterRes;
        this.GroundRes = groundRes;
        this.LightRes = lightRes;
        this.DarkRes = darkRes;
        this.DizzyRes = dizzyRes;
        this.ConfusionRes = confusionRes;
        this.PoisonRes = poisonRes;
        this.SleepRes = sleepRes;
        this.WeaponType = weaponType;
        this.ActionBar = actionBar;
        this.HpNow = hpNow;
        this.MpNow = mpNow;
        this.Buff = buff;

}
    public int id { get { return ID; } }
    public int objectID { get { return ObjectID; } }
    public byte side { get { return Side; } }
    public byte sideIndex { get { return SideIndex; } }
    public string name { get { return Name; } set { Name = value; } }
    public int hp { get { return Hp; } set { Hp = value; } }
    public int mp { get { return Mp; } set { Mp = value; } }
    public short hpRenew { get { return HpRenew; } set { HpRenew = value; } }
    public short mpRenew { get { return MpRenew; } set { MpRenew = value; } }
    public short atkMin { get { return AtkMin; } set { AtkMin = value; } }
    public short atkMax { get { return AtkMax; } set { AtkMax = value; } }
    public short mAtkMin { get { return MAtkMin; } set { MAtkMin = value; } }
    public short mAtkMax { get { return MAtkMax; } set { MAtkMax = value; } }
    public short def { get { return Def; } set { Def = value; } }
    public short mDef { get { return MDef; } set { MDef = value; } }
    public short hit { get { return Hit; } set { Hit = value; } }
    public short dod { get { return Dod; } set { Dod = value; } }
    public short criR { get { return CriR; } set { CriR = value; } }
    public short criD { get { return CriD; } set { CriD = value; } }
    public short spd { get { return Spd; } set { Spd = value; } }
    public short windDam { get { return WindDam; } set { WindDam = value; } }
    public short fireDam { get { return FireDam; } set { FireDam = value; } }
    public short waterDam { get { return WaterDam; } set { WaterDam = value; } }
    public short groundDam { get { return GroundDam; } set { GroundDam = value; } }
    public short lightDam { get { return LightDam; } set { LightDam = value; } }
    public short darkDam { get { return DarkDam; } set { DarkDam = value; } }
    public short windRes { get { return WindRes; } set { WindRes = value; } }
    public short fireRes { get { return FireRes; } set { FireRes = value; } }
    public short waterRes { get { return WaterRes; } set { WaterRes = value; } }
    public short groundRes { get { return GroundRes; } set { GroundRes = value; } }
    public short lightRes { get { return LightRes; } set { LightRes = value; } }
    public short darkRes { get { return DarkRes; } set { DarkRes = value; } }
    public short dizzyRes { get { return DizzyRes; } set { DizzyRes = value; } }
    public short confusionRes { get { return ConfusionRes; } set { ConfusionRes = value; } }
    public short poisonRes { get { return PoisonRes; } set { PoisonRes = value; } }
    public short sleepRes { get { return SleepRes; } set { SleepRes = value; } }
    public ItemTypeSmall weaponType { get { return WeaponType; } }
    public short actionBar { get { return ActionBar; } set { ActionBar = value; } }
    public byte skillIndex { get { return SkillIndex; } set { SkillIndex = value; } }
    public int hpNow { get { return HpNow; } set { HpNow = value; } }
    public int mpNow { get { return MpNow; } set { MpNow = value; } }
    public List<FightBuff> buff { get { return Buff; } set { Buff = value; } }
}

public class FightBuff
{
    private FightBuffType Type;
    private byte Value;
    private byte Round;
    public FightBuff(FightBuffType type, byte value, byte round)
    {
        this.Type = type;
        this.Value = value;
        this.Round = round;
    }
    public FightBuffType type { get { return Type; } }
    public byte value { get { return Value; } }
    public byte round { get { return Round; } set { Round = value; } }
}

//日志消息实例
public class LogObject
{
    private int ID;
    private LogType Type;
    private int StandardTime;
    private string Text;
    private List<int> Value;
    public LogObject(int id, LogType type, int standardTime, string text, List<int> value)
    {
        this.ID = id;
        this.Type = type;
        this.StandardTime = standardTime;
        this.Text = text;
        this.Value = value;
    }
    public int id { get { return ID; } }
    public LogType type { get { return Type; } }
    public int standardTime { get { return StandardTime; } }
    public string text { get { return Text; } }
    public List<int> value { get { return Value; } }
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
public class ProduceEquipPrototype: ISerializationCallbackReceiver
{
    public short ID;
    public ItemTypeSmall Type;//smalltype
    public string TypeStr;
    public List<byte> MakePlace;
    public string OutputPic;
    public byte OptionValue;
    public byte Level;
    public short NeedLabor;
    public byte InputWood;
    public byte InputStone;
    public byte InputMetal;
    public byte InputLeather;
    public byte InputCloth;
    public byte InputTwine;
    public byte InputBone;
    public List<short> OutputID;
    public List<short> OutputRate;

    public void OnAfterDeserialize()
    {
        ItemTypeSmall type = (ItemTypeSmall)Enum.Parse(typeof(ItemTypeSmall), TypeStr);
        Type = type;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}

//资源生产关系原型
[System.Serializable]
public class ProduceResourcePrototype
{
    public short ID;//对应建筑原型ID
    public string Action;
    public List<string> OutputPic;
    public short TimeInterval;
    public float LaborRate;
    public short InputCereal;
    public short InputVegetable;
    public short InputFruit;
    public short InputMeat;
    public short InputFish;
    public short InputWood;
    public short InputStone;
    public short InputMetal;
    public short InputLeather;
    public short InputCloth;
    public short InputTwine;
    public short InputBone;
    public short OutputCereal;
    public short OutputVegetable;
    public short OutputFruit;
    public short OutputMeat;
    public short OutputFish;
    public short OutputWood;
    public short OutputStone;
    public short OutputMetal;
    public short OutputLeather;
    public short OutputCloth;
    public short OutputTwine;
    public short OutputBone;
}

public class ExecuteEventObject
{
    private ExecuteEventType Type;
    private int StartTime;
    private int EndTime;
    private List<int> Value;
    public ExecuteEventObject( ExecuteEventType type, int startTime, int endTime, List<int> value)
    {
        this.Type = type;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Value = value;
    }
    public ExecuteEventType type { get { return Type; } }
    public int startTime { get { return StartTime; } }
    public int endTime { get { return EndTime; } }
    public List<int> value { get { return Value; } }

}

