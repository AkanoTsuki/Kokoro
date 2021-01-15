using System.Collections.Generic;
using UnityEngine;

using System;

//访客阶段
public enum CustomerStage
{ 
    Come,
    Observe,
    Wait,
    IntoShop,
    Gone
}

//库存资源类型
public enum StuffType
{
    Cereal,
    Vegetable,
    Fruit,
    Meat,
    Fish,
    Beer,
    Wine,
    Wood,
    Stone,
    Metal,
    Leather,
    Cloth,
    Twine,
    Bone,
    Wind,
    Fire,
    Water,
    Ground,
    Light,
    Dark
}

//顾客访问的商店类型
public enum ShopType
{
    WeaponAndSubhand,
    Armor,
    Jewelry,
    Scroll,
    None
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
    ScrollNone,
    ScrollWindI,
    ScrollFireI,
    ScrollWaterI,
    ScrollGroundI,
    ScrollLightI,
    ScrollDarkI,
    ScrollWindII,
    ScrollFireII,
    ScrollWaterII,
    ScrollGroundII,
    ScrollLightII,
    ScrollDarkII,
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
    WorkMakeScroll,
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
    GetSomething,
    TrapHp,
    TrapMp,
    SpringHp,
    SpringMp
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
    Death,
    SpringAppear,
    ChestOpen,
    AttackLoop,
    WalkUp,
    WalkDown
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
    Adventure,
    BuildingUpgrade,
    BuildingSale
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
    public byte Rank;
    public AnimStatus ActionAnim;
    public string ActionAnimStr;
    public List<int> Element;
    public ItemTypeSmall TypeSmall;
    public string TypeSmallStr;
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

        ItemTypeSmall type2 = (ItemTypeSmall)Enum.Parse(typeof(ItemTypeSmall), TypeSmallStr);
        TypeSmall = type2;
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
    private short DistrictID;//-1 为整体收藏库 -2为初始化原始技能
    private bool IsGoods;//是否商品（DistrictID！=-1）
    private int HeroID;
    private int UseCount;
    public SkillObject(int id, string name, short prototypeID, short rateModify, short mpModify, byte comboRate, byte comboMax, byte gold, short cost, short districtID, bool isGoods, int heroID, int useCount)
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
        this.IsGoods = isGoods;
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
    public bool isGoods { get { return IsGoods; } set { IsGoods = value; } }
    public int heroID { get { return HeroID; } set { HeroID = value; } }
    public int useCount { get { return UseCount; } set { UseCount = value; } }
}


//英雄原型T
[System.Serializable]
public class HeroPrototype : ISerializationCallbackReceiver
{
    public short ID;
    public string Name;
    public string Des;
    public string Color;
    public List<string> PicMan;
    public List<string> PicWoman;
    public List<ItemTypeSmall> WantBuy = new List<ItemTypeSmall>();
    public List<string> WantBuyStr;
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
    public byte WorkMakeScroll;
    public byte WorkSundry;
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < WantBuyStr.Count; i++)
        {
            ItemTypeSmall fixPart = (ItemTypeSmall)Enum.Parse(typeof(ItemTypeSmall), WantBuyStr[i]);
            WantBuy.Add(fixPart);
        }
 
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
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
    private float Hp;
    private float Mp;
    private float HpRenew;
    private float MpRenew;
    private float AtkMin;
    private float AtkMax;
    private float MAtkMin;
    private float MAtkMax;
    private float Def;
    private float MDef;
    private float Hit;
    private float Dod;
    private float CriR;
    private float CriD;
    private short Spd;
    private short BaseHp;
    private short BaseMp;
    private short BaseAtkMin;
    private short BaseAtkMax;
    private short BaseMAtkMin;
    private short BaseMAtkMax;
    private short BaseDef;
    private short BaseMDef;
    private short BaseHit;
    private short BaseDod;
    private short BaseCriR;
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
    private byte WorkMakeScroll;
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
    private int CountMakeWeapon;
    private int CountMakeArmor;
    private int CountMakeJewelry;
    private int CountMakeScroll;
    private int CountKill;
    private int CountDeath;
    private int CountAdventure;
    private int CountAdventureDone;
    private int CountUseWind;
    private int CountUseFire;
    private int CountUseWater;
    private int CountUseGround;
    private int CountUseLight;
    private int CountUseDark;
    private int CountUseNone;
    private Dictionary<short,HeroSkill> SkillInfo;
    private List<string> Log;
    public HeroObject(int id, string name, short prototypeID, short level, int exp,byte sex,string pic, float groupRate,
        float hp, float mp, float hpRenew, float mpRenew,
        float atkMin, float atkMax, float mAtkMin, float mAtkMax, float def, float mDef,
        float hit, float dod, float criR, float criD, short spd,
        short baseHp, short baseMp, short baseAtkMin, short baseAtkMax, short baseMAtkMin, short baseMAtkMax, short baseDef, short baseMDef, short baseHit, short baseDod, short baseCriR,
        short windDam, short fireDam, short waterDam, short groundDam, short lightDam, short darkDam,
        short windRes, short fireRes, short waterRes, short groundRes, short lightRes, short darkRes,
        short dizzyRes, short confusionRes, short poisonRes, short sleepRes,
        byte goldGet, byte expGet, byte itemGet,
        byte workPlanting, byte workFeeding, byte workFishing, byte workHunting, byte workMining, byte workQuarrying, byte workFelling, byte workBuild,
        byte workMakeWeapon, byte workMakeArmor, byte workMakeJewelry, byte workMakeScroll,
        byte workSundry,
        int equipWeapon, int equipSubhand, int equipHead, int equipBody, int equipHand, int equipBack, int equipFoot, int equipNeck, int equipFinger1, int equipFinger2, List<int> skill,
        int workerInBuilding, short adventureInTeam,
        int countMakeWeapon, int countMakeArmor, int countMakeJewelry, int countMakeScroll, int countKill, int countDeath, int countAdventure, int countAdventureDone,
        int countUseWind, int countUseFire, int countUseWater, int countUseGround, int countUseLight, int countUseDark, int countUseNone, Dictionary<short, HeroSkill> skillInfo, List<string> log
        )
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
        this.BaseHp = baseHp;
        this.BaseMp = baseMp;
        this.BaseAtkMin = baseAtkMin;
        this.BaseAtkMax = baseAtkMax;
        this.BaseMAtkMin = baseMAtkMin;
        this.BaseMAtkMax = baseMAtkMax;
        this.BaseDef = baseDef;
        this.BaseMDef = baseMDef;
        this.BaseHit = baseHit;
        this.BaseDod = baseDod;
        this.BaseCriR = baseCriR;

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
        this.WorkMakeScroll = workMakeScroll;
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
        this.CountMakeWeapon = countMakeWeapon;
        this.CountMakeArmor = countMakeArmor;
        this.CountMakeJewelry = countMakeJewelry;
        this.CountMakeScroll = countMakeScroll;
        this.CountKill = countKill;
        this.CountDeath = countDeath;
        this.CountAdventure = countAdventure;
        this.CountAdventureDone = countAdventureDone;
        this.CountUseWind = countUseWind;
        this.CountUseFire = countUseFire;
        this.CountUseWater = countUseWater;
        this.CountUseGround = countUseGround;
        this.CountUseLight = countUseLight;
        this.CountUseDark = countUseDark;
        this.CountUseNone = countUseNone;
        this.SkillInfo = skillInfo;
        this.Log = log;
    }
    public int id { get { return ID; } }
    public string name { get { return Name; } set { Name = value; } }
    public short prototypeID { get { return PrototypeID; } }
    public short level { get { return Level; } set { Level = value; } }
    public int exp { get { return Exp; } set { Exp = value; } }
    public byte sex { get { return Sex; } set { Sex = value; } }
    public string pic { get { return Pic; } set { Pic = value; } }
    public float groupRate { get { return GroupRate; } set { GroupRate = value; } }
    public float hp { get { return Hp; } set { Hp = value; } }
    public float mp { get { return Mp; } set { Mp = value; } }
    public float hpRenew { get { return HpRenew; } set { HpRenew = value; } }
    public float mpRenew { get { return MpRenew; } set { MpRenew = value; } }
    public float atkMin { get { return AtkMin; } set { AtkMin = value; } }
    public float atkMax { get { return AtkMax; } set { AtkMax = value; } }
    public float mAtkMin { get { return MAtkMin; } set { MAtkMin = value; } }
    public float mAtkMax { get { return MAtkMax; } set { MAtkMax = value; } }
    public float def { get { return Def; } set { Def = value; } }
    public float mDef { get { return MDef; } set { MDef = value; } }
    public float hit { get { return Hit; } set { Hit = value; } }
    public float dod { get { return Dod; } set { Dod = value; } }
    public float criR { get { return CriR; } set { CriR = value; } }
    public float criD { get { return CriD; } set { CriD = value; } }
    public short spd { get { return Spd; } set { Spd = value; } }

    public short baseHp { get { return BaseHp; } }
    public short baseMp { get { return BaseMp; } }
    public short baseAtkMin { get { return BaseAtkMin; } }
    public short baseAtkMax { get { return BaseAtkMax; } }
    public short baseMAtkMin { get { return BaseMAtkMin; } }
    public short baseMAtkMax { get { return BaseMAtkMax; } }
    public short baseDef { get { return BaseDef; } }
    public short baseMDef { get { return BaseMDef; } }
    public short baseHit { get { return BaseHit; } }
    public short baseDod { get { return BaseDod; } }
    public short baseCriR { get { return BaseCriR; } }

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
    public byte workMakeScroll { get { return WorkMakeScroll; } set { WorkMakeScroll = value; } }
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

    public int countMakeWeapon { get { return CountMakeWeapon; } set { CountMakeWeapon = value; } }
    public int countMakeArmor { get { return CountMakeArmor; } set { CountMakeArmor = value; } }
    public int countMakeJewelry { get { return CountMakeJewelry; } set { CountMakeJewelry = value; } }
    public int countMakeScroll { get { return CountMakeScroll; } set { CountMakeScroll = value; } }
    public int countKill { get { return CountKill; } set { CountKill = value; } }
    public int countDeath { get { return CountDeath; } set { CountDeath = value; } }
    public int countAdventure { get { return CountAdventure; } set { CountAdventure = value; } }
    public int countAdventureDone { get { return CountAdventureDone; } set { CountAdventureDone = value; } }
    public int countUseWind { get { return CountUseWind; } set { CountUseWind = value; } }
    public int countUseFire { get { return CountUseFire; } set { CountUseFire = value; } }
    public int countUseWater { get { return CountUseWater; } set { CountUseWater = value; } }
    public int countUseGround { get { return CountUseGround; } set { CountUseGround = value; } }
    public int countUseLight { get { return CountUseLight; } set { CountUseLight = value; } }
    public int countUseDark { get { return CountUseDark; } set { CountUseDark = value; } }
    public int countUseNone { get { return CountUseNone; } set { CountUseNone = value; } }
    public Dictionary<short, HeroSkill> skillInfo  { get { return SkillInfo; } set { SkillInfo = value; } }
    public List<string> log { get { return Log; } set { Log = value; } }
}

public class HeroSkill
{
    private short SkillID;//模板ID
    private byte Level;
    private int Exp;
    public HeroSkill(short skillID, byte level, int exp)
    {
        this.SkillID = skillID;
        this.Level = level;
        this.Exp = exp;
    }
    public short skillID { get { return SkillID; } }
    public byte level { get { return Level; } set { Level = value; } }
    public int exp { get { return Exp; } set { Exp = value; } }
}

public class CreateHeroRank
{
    private int[] Value1;
    private int[] Value2;
    private byte[,] Probability;
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
    //public string Name;
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
    private int RFoodBeer;
    private int RFoodWine;
    private int RStuffWood;
    private int RStuffMetal;
    private int RStuffStone;
    private int RStuffLeather;
    private int RStuffTwine;
    private int RStuffCloth;
    private int RStuffBone;
    private int RStuffWind;
    private int RStuffFire;
    private int RStuffWater;
    private int RStuffGround;
    private int RStuffLight;
    private int RStuffDark;
    private int RProductWeapon;
    private int RProductArmor;
    private int RProductJewelry;
    private int RProductScroll;
    private int RProductGoodWeapon;
    private int RProductGoodArmor;
    private int RProductGoodJewelry;
    private int RProductGoodScroll;
    private int RFoodLimit;//库存上限
    private int RStuffLimit;
    private int RProductLimit;
    private int RScrollLimit;
    public DistrictObject(short id, string name, string baseName, string des, bool isOpen, byte level, short people, short peopleLimit, short worker, 
         List<int> buildingList, List<int> heroList,
        short eWind, short eFire, short eWater, short eGround, short eLight, short eDark,
        int rFoodCereal, int rFoodVegetable, int rFoodFruit, int rFoodMeat, int rFoodFish, int rFoodBeer, int rFoodWine,
        int rStuffWood, int rStuffMetal, int rStuffStone, int rStuffLeather, int rStuffTwine, int rStuffCloth, int rStuffBone,
        int rStuffWind, int rStuffFire, int rStuffWater, int rStuffGround, int rStuffLight, int rStuffDark,
        int rProductWeapon, int rProductArmor, int rProductJewelry, int rProductScroll, int rProductGoodWeapon, int rProductGoodArmor, int rProductGoodJewelry, int rProductGoodScroll,
        int rFoodLimit, int rStuffLimit, int rProductLimit, int rScrollLimit)
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
        this.RFoodBeer = rFoodBeer;
        this.RFoodWine = rFoodWine;
        this.RStuffWood = rStuffWood;
        this.RStuffMetal = rStuffMetal;
        this.RStuffStone = rStuffStone;
        this.RStuffLeather = rStuffLeather;
        this.RStuffTwine = rStuffTwine;
        this.RStuffCloth = rStuffCloth;
        this.RStuffBone = rStuffBone;
        this.RStuffWind = rStuffWind;
        this.RStuffFire = rStuffFire;
        this.RStuffWater = rStuffWater;
        this.RStuffGround = rStuffGround;
        this.RStuffLight = rStuffLight;
        this.RStuffDark = rStuffDark;
        this.RProductWeapon = rProductWeapon;
        this.RProductArmor = rProductArmor;
        this.RProductJewelry = rProductJewelry;
        this.RProductScroll = rProductScroll;
        this.RProductGoodWeapon = rProductGoodWeapon;
        this.RProductGoodArmor = rProductGoodArmor;
        this.RProductGoodJewelry = rProductGoodJewelry;
        this.RProductGoodScroll = rProductGoodScroll;
        this.RFoodLimit = rFoodLimit;//库存上限
        this.RStuffLimit = rStuffLimit;
        this.RProductLimit = rProductLimit;
        this.RScrollLimit = rScrollLimit;
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
    public int rFoodBeer { get { return RFoodBeer; } set { RFoodBeer = value; } }
    public int rFoodWine { get { return RFoodWine; } set { RFoodWine = value; } }
    public int rStuffWood { get { return RStuffWood; } set { RStuffWood = value; } }
    public int rStuffMetal { get { return RStuffMetal; } set { RStuffMetal = value; } }
    public int rStuffStone { get { return RStuffStone; } set { RStuffStone = value; } }
    public int rStuffLeather { get { return RStuffLeather; } set { RStuffLeather = value; } }
    public int rStuffTwine { get { return RStuffTwine; } set { RStuffTwine = value; } }
    public int rStuffCloth { get { return RStuffCloth; } set { RStuffCloth = value; } }
    public int rStuffBone { get { return RStuffBone; } set { RStuffBone = value; } }
    public int rStuffWind { get { return RStuffWind; } set { RStuffWind = value; } }
    public int rStuffFire { get { return RStuffFire; } set { RStuffFire = value; } }
    public int rStuffWater { get { return RStuffWater; } set { RStuffWater = value; } }
    public int rStuffGround { get { return RStuffGround; } set { RStuffGround = value; } }
    public int rStuffLight { get { return RStuffLight; } set { RStuffLight = value; } }
    public int rStuffDark { get { return RStuffDark; } set { RStuffDark = value; } }
    public int rProductWeapon { get { return RProductWeapon; } set { RProductWeapon = value; } }
    public int rProductArmor { get { return RProductArmor; } set { RProductArmor = value; } }
    public int rProductJewelry { get { return RProductJewelry; } set { RProductJewelry = value; } }
    public int rProductScroll { get { return RProductScroll; } set { RProductScroll = value; } }
    public int rProductGoodWeapon { get { return RProductGoodWeapon; } set { RProductGoodWeapon = value; } }
    public int rProductGoodArmor { get { return RProductGoodArmor; } set { RProductGoodArmor = value; } }
    public int rProductGoodJewelry { get { return RProductGoodJewelry; } set { RProductGoodJewelry = value; } }
    public int rProductGoodScroll { get { return RProductGoodScroll; } set { RProductGoodScroll = value; } }
    public int rFoodLimit { get { return RFoodLimit; } set { RFoodLimit = value; } }
    public int rStuffLimit { get { return RStuffLimit; } set { RStuffLimit = value; } }
    public int rProductLimit { get { return RProductLimit; } set { RProductLimit = value; } }
    public int rScrollLimit { get { return RScrollLimit; } set { RScrollLimit = value; } }
}

public class DistrictGridObject
{

    private byte Level;
    private int BuildingID;//-2未开放 -1未使用
    public DistrictGridObject(byte level, int buildingID)
    {
        this.Level = level;
        this.BuildingID = buildingID;
    }
    public byte level { get { return Level; } }
    public int buildingID { get { return BuildingID; } set { BuildingID = value; } }
}


//建筑物原型
[System.Serializable]
public class BuildingPrototype : ISerializationCallbackReceiver
{
    public short ID;
    public string Name;
    public string MainPic;
   // public string MapPic;
    public byte SizeX;
    public byte SizeY;
    public byte SizeYBase;
    public byte DoorPosition;
    public string PanelType;
    public ShopType ShopType;
    public string ShopTypeStr;
    public string Des;
    public byte Level;
    public short BuildTime;//建造时间 单位小时
    public short NeedGold;
    public short NeedWood;
    public short NeedStone;
    public short NeedMetal;
    public int Expense;
    public short UpgradeTo;
   // public byte Grid;

    public short People;//提供人口
    public short Worker;//工人上限
    public byte EWind;
    public byte EFire;
    public byte EWater;
    public byte EGround;
    public byte ELight;
    public byte EDark;
    public void OnAfterDeserialize()
    {
        ShopType type = (ShopType)Enum.Parse(typeof(ShopType), ShopTypeStr);
        ShopType = type;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}

//建筑实例
public class BuildingObject
{
    private int ID;
    private short DistrictID;
    private short PrototypeID;
    private string Name;
    private string MainPic;
    private short PositionX; //基准点，没包括16f
    private short PositionY;
    private byte Layer;
    private AnimStatus DoorInLine;//门在队伍的左还是右
    // private string MapPic;
    private string PanelType;
    private string Des;
    private byte Level;
    private int Expense;
    private short UpgradeTo;//升级后的建筑原型ID
    private bool IsOpen;
    private bool IsSale;
    private List<string> GridList;//占用格子ID
    private List<int> HeroList;
    private List<int> CustomerList;
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
    private byte BuildProgress;//0建设中 1已完成 2升级中
    public BuildingObject(int id, short prototypeID, short districtID,string name, string mainPic, short positionX,  short positionY, byte layer, AnimStatus doorInLine, string panelType, string des, byte level, int expense, short upgradeTo, bool isOpen, bool isSale, List<string> gridList, List<int> heroList, List<int> customerList,
         short people, short worker, short workerNow,
        byte eWind, byte eFire, byte eWater, byte eGround, byte eLight, byte eDark,
        short produceEquipNow, byte buildProgress)
    {
        this.ID = id;
        this.PrototypeID = prototypeID;
        this.DistrictID = districtID;
        this.Name = name;
        this.MainPic = mainPic;
        this.PositionX = positionX;
        this.PositionY = positionY;
        this.Layer = layer;
        this.DoorInLine = doorInLine;
        //   this.MapPic = mapPic;
        this.PanelType = panelType;
        this.Des = des;
        this.Level = level;
        this.Expense = expense;
        this.UpgradeTo = upgradeTo;
        this.IsOpen = isOpen;
        this.IsSale = isSale;
        this.GridList = gridList;
        this.HeroList = heroList;
        this.CustomerList = customerList;
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
    public short positionX { get { return PositionX; } }
    public short positionY { get { return PositionY; } }
    public byte layer { get { return Layer; } }
    public AnimStatus doorInLine { get { return DoorInLine; } }
    //   public string mapPic { get { return MapPic; } set { MapPic = value; } }
    public string panelType { get { return PanelType; } set { PanelType = value; } }
    public string des { get { return Des; } set { Des = value; } }
    public byte level { get { return Level; } set { Level = value; } }
    public int expense { get { return Expense; } set { Expense = value; } }
    public short upgradeTo { get { return UpgradeTo; } set { UpgradeTo = value; } }
    public bool isOpen { get { return IsOpen; } set { IsOpen = value; } }
    public bool isSale { get { return IsSale; } set { IsSale = value; } }
    public List<string> gridList { get { return GridList; } set { GridList = value; } }
    public List<int> heroList { get { return HeroList; } set { HeroList = value; } }
    public List<int> customerList { get { return CustomerList; } set { CustomerList = value; } }
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
    public List<short> ItemDrop;
    public List<byte> ItemDropRate;
    public List<byte> ItemDropNumMin;
    public List<byte> ItemDropNumMax;
}

//探险队实例
public class AdventureTeamObject
{
    private byte ID;
    private short DungeonID;
    private short DungeonEVWind;
    private short DungeonEVFire;
    private short DungeonEVWater;
    private short DungeonEVGround;
    private short DungeonEVLight;
    private short DungeonEVDark;
    private byte DungeonEPWind;
    private byte DungeonEPFire;
    private byte DungeonEPWater;
    private byte DungeonEPGround;
    private byte DungeonEPLight;
    private byte DungeonEPDark;
    private List<string> ScenePicList;
    private List<int> HeroIDList;
    private List<int> HeroHpList;
    private List<int> HeroMpList;
    private List<int> EnemyIDList;
    private byte NowDay;
    private int StandardTimeStart;//本次冒险开始时间
    private AdventureState State;
    private AdventureAction Action;
    private int FightRound;
    private short GetExp;//战利品（开始）
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
    private short GetWind;
    private short GetFire;
    private short GetWater;
    private short GetGround;
    private short GetLight;
    private short GetDark;
    private List<int> GetItemList;//战利品（结束），物品原型ID，结算时才生成实例
    private short KillNum;
    private List<string> Log;
    private List<AdventurePartObject> Part;
    public AdventureTeamObject(byte id,short dungeonID, short dungeonEVWind, short dungeonEVFire, short dungeonEVWater, short dungeonEVGround, short dungeonEVLight, short dungeonEVDark, byte dungeonEPWind, byte dungeonEPFire, byte dungeonEPWater, byte dungeonEPGround, byte dungeonEPLight, byte dungeonEPDark,
        List<string> scenePicList, List<int> heroIDList, List<int> heroHpList, List<int> heroMpList, List<int> enemyIDList, byte nowDay, int standardTimeStart, AdventureState state, AdventureAction action, int fightRound,
        short getExp, short getGold, short getCereal, short getVegetable, short getFruit, short getMeat, short getFish, short getWood, short getMetal, short getStone, short getLeather, short getCloth,short getTwine, short getBone,
        short getWind, short getFire, short getWater, short getGround, short getLight, short getDark,
        List<int> getItemList, short killNum, List<string> log, List<AdventurePartObject> part)
    {
        this.ID = id;
        this.DungeonID = dungeonID;
        this.DungeonEVWind = dungeonEVWind;
        this.DungeonEVFire = dungeonEVFire;
        this.DungeonEVWater = dungeonEVWater;
        this.DungeonEVGround = dungeonEVGround;
        this.DungeonEVLight = dungeonEVLight;
        this.DungeonEVDark = dungeonEVDark;
        this.DungeonEPWind = dungeonEPWind;
        this.DungeonEPFire = dungeonEPFire;
        this.DungeonEPWater = dungeonEPWater;
        this.DungeonEPGround = dungeonEPGround;
        this.DungeonEPLight = dungeonEPLight;
        this.DungeonEPDark = dungeonEPDark;
        this.ScenePicList = scenePicList;
        this.HeroIDList = heroIDList;
        this.HeroHpList = heroHpList;
        this.HeroMpList = heroMpList;
        this.EnemyIDList = enemyIDList;
        this.NowDay = nowDay;
        this.StandardTimeStart = standardTimeStart;
        this.State = state;
        this.Action = action;
        this.FightRound = fightRound;
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
        this.GetWind = getWind;
        this.GetFire = getFire;
        this.GetWater = getWater;
        this.GetGround = getGround;
        this.GetLight = getLight;
        this.GetDark = getDark;
        this.GetItemList = getItemList;
        this.KillNum = killNum;
        this.Log = log;
        this.Part = part;
    }
    public byte id { get { return ID; } }
    public short dungeonID { get { return DungeonID; } set { DungeonID = value; } }
    public short dungeonEVWind { get { return DungeonEVWind; } set { DungeonEVWind = value; } }
    public short dungeonEVFire { get { return DungeonEVFire; } set { DungeonEVFire = value; } }
    public short dungeonEVWater { get { return DungeonEVWater; } set { DungeonEVWater = value; } }
    public short dungeonEVGround { get { return DungeonEVGround; } set { DungeonEVGround = value; } }
    public short dungeonEVLight { get { return DungeonEVLight; } set { DungeonEVLight = value; } }
    public short dungeonEVDark { get { return DungeonEVDark; } set { DungeonEVDark = value; } }
    public byte dungeonEPWind { get { return DungeonEPWind; } set { DungeonEPWind = value; } }
    public byte dungeonEPFire { get { return DungeonEPFire; } set { DungeonEPFire = value; } }
    public byte dungeonEPWater { get { return DungeonEPWater; } set { DungeonEPWater = value; } }
    public byte dungeonEPGround { get { return DungeonEPGround; } set { DungeonEPGround = value; } }
    public byte dungeonEPLight { get { return DungeonEPLight; } set { DungeonEPLight = value; } }
    public byte dungeonEPDark { get { return DungeonEPDark; } set { DungeonEPDark = value; } }
    public List<string> scenePicList { get { return ScenePicList; } set { ScenePicList = value; } }
    public List<int> heroIDList { get { return HeroIDList; } set { HeroIDList = value; } }
    public List<int> heroHpList { get { return HeroHpList; } set { HeroHpList = value; } }
    public List<int> heroMpList { get { return HeroMpList; } set { HeroMpList = value; } }
    public List<int> enemyIDList { get { return EnemyIDList; } set { EnemyIDList = value; } }
    public byte nowDay { get { return NowDay; } set { NowDay = value; } }
    public int standardTimeStart { get { return StandardTimeStart; } set { StandardTimeStart = value; } }
    public AdventureState state { get { return State; } set { State = value; } }
    public AdventureAction action { get { return Action; } set { Action = value; } }
    public int fightRound { get { return FightRound; } set { FightRound = value; } }
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
    public short getWind { get { return GetWind; } set { GetWind = value; } }
    public short getFire { get { return GetFire; } set { GetFire = value; } }
    public short getWater { get { return GetWater; } set { GetWater = value; } }
    public short getGround { get { return GetGround; } set { GetGround = value; } }
    public short getLight { get { return GetLight; } set { GetLight = value; } }
    public short getDark { get { return GetDark; } set { GetDark = value; } }
    public List<int> getItemList { get { return GetItemList; } set { GetItemList = value; } }
    public short killNum { get { return KillNum; } set { KillNum = value; } }
    public List<string> log { get { return Log; } set { Log = value; } }
    public List<AdventurePartObject> part { get { return Part; } set { Part = value; } }
}

//探险队部分（暂定1部分=1天）实例
public class AdventurePartObject
{

    private AdventureEvent EventType;
    private bool IsPass;
    private List<int> HeroHpList;//结束后
    private List<int> HeroMpList;
    private List<byte> ElementPointList;
    private string Log;
    public AdventurePartObject( AdventureEvent eventType, bool isPass, List<int> heroHpList, List<int> heroMpList, List<byte> elementPointList, string log)
    {

        this.EventType = eventType;
        this.IsPass = isPass;
        this.HeroHpList = heroHpList;
        this.HeroMpList = heroMpList;
        this.ElementPointList = elementPointList;
        this.Log = log;
    }


    public AdventureEvent eventType { get { return EventType; } }
    public bool isPass { get { return IsPass; }  }
    public List<int> heroHpList { get { return HeroHpList; }  }
    public List<int> heroMpList { get { return HeroMpList; }  }
    public List<byte> elementPointList { get { return ElementPointList; }  }
    public string log { get { return Log; } }
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
    private short Level;
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


    public FightMenberObject(int id, int objectID, byte side, byte sideIndex, string name, short level,
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
        this.Level = level;
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
    public short level { get { return Level; } set { Level = value; } }
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

//供需表实例
public class SupplyAndDemandObject
{
    private List<short> WeaponSwordValue;
    private List<short> WeaponAxeValue;
    private List<short> WeaponSpearValue;
    private List<short> WeaponHammerValue;
    private List<short> WeaponBowValue;
    private List<short> WeaponStaffValue;
    private List<short> SubhandShieldValue;
    private List<short> SubhandDorlachValue;
    private List<short> JewelryNeckValue;
    private List<short> JewelryFingerValue;
    private List<short> ArmorHeadHValue;
    private List<short> ArmorBodyHValue;
    private List<short> ArmorHandHValue;
    private List<short> ArmorBackHValue;
    private List<short> ArmorFootHValue;
    private List<short> ArmorHeadLValue;
    private List<short> ArmorBodyLValue;
    private List<short> ArmorHandLValue;
    private List<short> ArmorBackLValue;
    private List<short> ArmorFootLValue;
    private List<short> ScrollWindIValue;
    private List<short> ScrollFireIValue;
    private List<short> ScrollWaterIValue;
    private List<short> ScrollGroundIValue;
    private List<short> ScrollLightIValue;
    private List<short> ScrollDarkIValue;
    private List<short> ScrollNoneValue;
    private List<short> ScrollWindIIValue;
    private List<short> ScrollFireIIValue;
    private List<short> ScrollWaterIIValue;
    private List<short> ScrollGroundIIValue;
    private List<short> ScrollLightIIValue;
    private List<short> ScrollDarkIIValue;

    private List<byte> WeaponSwordRank;
    private List<byte> WeaponAxeRank;
    private List<byte> WeaponSpearRank;
    private List<byte> WeaponHammerRank;
    private List<byte> WeaponBowRank;
    private List<byte> WeaponStaffRank;
    private List<byte> SubhandShieldRank;
    private List<byte> SubhandDorlachRank;
    private List<byte> JewelryNeckRank;
    private List<byte> JewelryFingerRank;
    private List<byte> ArmorHeadHRank;
    private List<byte> ArmorBodyHRank;
    private List<byte> ArmorHandHRank;
    private List<byte> ArmorBackHRank;
    private List<byte> ArmorFootHRank;
    private List<byte> ArmorHeadLRank;
    private List<byte> ArmorBodyLRank;
    private List<byte> ArmorHandLRank;
    private List<byte> ArmorBackLRank;
    private List<byte> ArmorFootLRank;
    private List<byte> ScrollWindIRank;
    private List<byte> ScrollFireIRank;
    private List<byte> ScrollWaterIRank;
    private List<byte> ScrollGroundIRank;
    private List<byte> ScrollLightIRank;
    private List<byte> ScrollDarkIRank;
    private List<byte> ScrollNoneRank;
    private List<byte> ScrollWindIIRank;
    private List<byte> ScrollFireIIRank;
    private List<byte> ScrollWaterIIRank;
    private List<byte> ScrollGroundIIRank;
    private List<byte> ScrollLightIIRank;
    private List<byte> ScrollDarkIIRank;

    public SupplyAndDemandObject(List<short> weaponSwordValue, List<short> weaponAxeValue, List<short> weaponSpearValue, List<short> weaponHammerValue, List<short> weaponBowValue, List<short> weaponStaffValue,
                List<short> subhandShieldValue, List<short> subhandDorlachValue, List<short> jewelryNeckValue, List<short> jewelryFingerValue,
                List<short> armorHeadHValue, List<short> armorBodyHValue, List<short> armorHandHValue, List<short> armorBackHValue, List<short> armorFootHValue,
                List<short> armorHeadLValue, List<short> armorBodyLValue, List<short> armorHandLValue, List<short> armorBackLValue, List<short> armorFootLValue,
                List<short> scrollWindIValue, List<short> scrollFireIValue, List<short> scrollWaterIValue, List<short> scrollGroundIValue, List<short> scrollLightIValue, List<short> scrollDarkIValue, List<short> scrollNoneValue,
                List<short> scrollWindIIValue, List<short> scrollFireIIValue, List<short> scrollWaterIIValue, List<short> scrollGroundIIValue, List<short> scrollLightIIValue, List<short> scrollDarkIIValue,
                List<byte> weaponSwordRank, List<byte> weaponAxeRank, List<byte> weaponSpearRank, List<byte> weaponHammerRank, List<byte> weaponBowRank, List<byte> weaponStaffRank,
                List<byte> subhandShieldRank, List<byte> subhandDorlachRank, List<byte> jewelryNeckRank, List<byte> jewelryFingerRank,
                List<byte> armorHeadHRank, List<byte> armorBodyHRank, List<byte> armorHandHRank, List<byte> armorBackHRank, List<byte> armorFootHRank,
                List<byte> armorHeadLRank, List<byte> armorBodyLRank, List<byte> armorHandLRank, List<byte> armorBackLRank, List<byte> armorFootLRank,
                List<byte> scrollWindIRank, List<byte> scrollFireIRank, List<byte> scrollWaterIRank, List<byte> scrollGroundIRank, List<byte> scrollLightIRank, List<byte> scrollDarkIRank, List<byte> scrollNoneRank,
                List<byte> scrollWindIIRank, List<byte> scrollFireIIRank, List<byte> scrollWaterIIRank, List<byte> scrollGroundIIRank, List<byte> scrollLightIIRank, List<byte> scrollDarkIIRank
         )
    {
        this.WeaponSwordValue = weaponSwordValue;
        this.WeaponAxeValue = weaponAxeValue;
        this.WeaponSpearValue = weaponSpearValue;
        this.WeaponHammerValue = weaponHammerValue;
        this.WeaponBowValue = weaponBowValue;
        this.WeaponStaffValue = weaponStaffValue;
        this.SubhandShieldValue = subhandShieldValue;
        this.SubhandDorlachValue = subhandDorlachValue;
        this.JewelryNeckValue = jewelryNeckValue;
        this.JewelryFingerValue = jewelryFingerValue;
        this.ArmorHeadHValue = armorHeadHValue;
        this.ArmorBodyHValue = armorBodyHValue;
        this.ArmorHandHValue = armorHandHValue;
        this.ArmorBackHValue = armorBackHValue;
        this.ArmorFootHValue = armorFootHValue;
        this.ArmorHeadLValue = armorHeadLValue;
        this.ArmorBodyLValue = armorBodyLValue;
        this.ArmorHandLValue = armorHandLValue;
        this.ArmorBackLValue = armorBackLValue;
        this.ArmorFootLValue = armorFootLValue;
        this.ScrollWindIValue = scrollWindIValue;
        this.ScrollFireIValue = scrollFireIValue;
        this.ScrollWaterIValue = scrollWaterIValue;
        this.ScrollGroundIValue = scrollGroundIValue;
        this.ScrollLightIValue = scrollLightIValue;
        this.ScrollDarkIValue = scrollDarkIValue;
        this.ScrollNoneValue = scrollNoneValue;
        this.ScrollWindIIValue = scrollWindIIValue;
        this.ScrollFireIIValue = scrollFireIIValue;
        this.ScrollWaterIIValue = scrollWaterIIValue;
        this.ScrollGroundIIValue = scrollGroundIIValue;
        this.ScrollLightIIValue = scrollLightIIValue;
        this.ScrollDarkIIValue = scrollDarkIIValue;

        this.WeaponSwordRank = weaponSwordRank;
        this.WeaponAxeRank = weaponAxeRank;
        this.WeaponSpearRank = weaponSpearRank;
        this.WeaponHammerRank = weaponHammerRank;
        this.WeaponBowRank = weaponBowRank;
        this.WeaponStaffRank = weaponStaffRank;
        this.SubhandShieldRank = subhandShieldRank;
        this.SubhandDorlachRank = subhandDorlachRank;
        this.JewelryNeckRank = jewelryNeckRank;
        this.JewelryFingerRank = jewelryFingerRank;
        this.ArmorHeadHRank = armorHeadHRank;
        this.ArmorBodyHRank = armorBodyHRank;
        this.ArmorHandHRank = armorHandHRank;
        this.ArmorBackHRank = armorBackHRank;
        this.ArmorFootHRank = armorFootHRank;
        this.ArmorHeadLRank = armorHeadLRank;
        this.ArmorBodyLRank = armorBodyLRank;
        this.ArmorHandLRank = armorHandLRank;
        this.ArmorBackLRank = armorBackLRank;
        this.ArmorFootLRank = armorFootLRank;
        this.ScrollWindIRank = scrollWindIRank;
        this.ScrollFireIRank = scrollFireIRank;
        this.ScrollWaterIRank = scrollWaterIRank;
        this.ScrollGroundIRank = scrollGroundIRank;
        this.ScrollLightIRank = scrollLightIRank;
        this.ScrollDarkIRank = scrollDarkIRank;
        this.ScrollNoneRank = scrollNoneRank;
        this.ScrollWindIIRank = scrollWindIIRank;
        this.ScrollFireIIRank = scrollFireIIRank;
        this.ScrollWaterIIRank = scrollWaterIIRank;
        this.ScrollGroundIIRank = scrollGroundIIRank;
        this.ScrollLightIIRank = scrollLightIIRank;
        this.ScrollDarkIIRank = scrollDarkIIRank;
    }
    public List<short> weaponSwordValue { get { return WeaponSwordValue; } set { WeaponSwordValue = value; } }
    public List<short> weaponAxeValue { get { return WeaponAxeValue; } set { WeaponAxeValue = value; } }
    public List<short> weaponSpearValue { get { return WeaponSpearValue; } set { WeaponSpearValue = value; } }
    public List<short> weaponHammerValue { get { return WeaponHammerValue; } set { WeaponHammerValue = value; } }
    public List<short> weaponBowValue { get { return WeaponBowValue; } set { WeaponBowValue = value; } }
    public List<short> weaponStaffValue { get { return WeaponStaffValue; } set { WeaponStaffValue = value; } }
    public List<short> subhandShieldValue { get { return SubhandShieldValue; } set { SubhandShieldValue = value; } }
    public List<short> subhandDorlachValue { get { return SubhandDorlachValue; } set { SubhandDorlachValue = value; } }
    public List<short> jewelryNeckValue { get { return JewelryNeckValue; } set { JewelryNeckValue = value; } }
    public List<short> jewelryFingerValue { get { return JewelryFingerValue; } set { JewelryFingerValue = value; } }
    public List<short> armorHeadHValue { get { return ArmorHeadHValue; } set { ArmorHeadHValue = value; } }
    public List<short> armorBodyHValue { get { return ArmorBodyHValue; } set { ArmorBodyHValue = value; } }
    public List<short> armorHandHValue { get { return ArmorHandHValue; } set { ArmorHandHValue = value; } }
    public List<short> armorBackHValue { get { return ArmorBackHValue; } set { ArmorBackHValue = value; } }
    public List<short> armorFootHValue { get { return ArmorFootHValue; } set { ArmorFootHValue = value; } }
    public List<short> armorHeadLValue { get { return ArmorHeadLValue; } set { ArmorHeadLValue = value; } }
    public List<short> armorBodyLValue { get { return ArmorBodyLValue; } set { ArmorBodyLValue = value; } }
    public List<short> armorHandLValue { get { return ArmorHandLValue; } set { ArmorHandLValue = value; } }
    public List<short> armorBackLValue { get { return ArmorBackLValue; } set { ArmorBackLValue = value; } }
    public List<short> armorFootLValue { get { return ArmorFootLValue; } set { ArmorFootLValue = value; } }
    public List<short> scrollWindIValue { get { return ScrollWindIValue; } set { ScrollWindIValue = value; } }
    public List<short> scrollFireIValue { get { return ScrollFireIValue; } set { ScrollFireIValue = value; } }
    public List<short> scrollWaterIValue { get { return ScrollWaterIValue; } set { ScrollWaterIValue = value; } }
    public List<short> scrollGroundIValue { get { return ScrollGroundIValue; } set { ScrollGroundIValue = value; } }
    public List<short> scrollLightIValue { get { return ScrollLightIValue; } set { ScrollLightIValue = value; } }
    public List<short> scrollDarkIValue { get { return ScrollDarkIValue; } set { ScrollDarkIValue = value; } }
    public List<short> scrollNoneValue { get { return ScrollNoneValue; } set { ScrollNoneValue = value; } }
    public List<short> scrollWindIIValue { get { return ScrollWindIIValue; } set { ScrollWindIIValue = value; } }
    public List<short> scrollFireIIValue { get { return ScrollFireIIValue; } set { ScrollFireIIValue = value; } }
    public List<short> scrollWaterIIValue { get { return ScrollWaterIIValue; } set { ScrollWaterIIValue = value; } }
    public List<short> scrollGroundIIValue { get { return ScrollGroundIIValue; } set { ScrollGroundIIValue = value; } }
    public List<short> scrollLightIIValue { get { return ScrollLightIIValue; } set { ScrollLightIIValue = value; } }
    public List<short> scrollDarkIIValue { get { return ScrollDarkIIValue; } set { ScrollDarkIIValue = value; } }

    public List<byte> weaponSwordRank { get { return WeaponSwordRank; } set { WeaponSwordRank = value; } }
    public List<byte> weaponAxeRank { get { return WeaponAxeRank; } set { WeaponAxeRank = value; } }
    public List<byte> weaponSpearRank { get { return WeaponSpearRank; } set { WeaponSpearRank = value; } }
    public List<byte> weaponHammerRank { get { return WeaponHammerRank; } set { WeaponHammerRank = value; } }
    public List<byte> weaponBowRank { get { return WeaponBowRank; } set { WeaponBowRank = value; } }
    public List<byte> weaponStaffRank { get { return WeaponStaffRank; } set { WeaponStaffRank = value; } }
    public List<byte> subhandShieldRank { get { return SubhandShieldRank; } set { SubhandShieldRank = value; } }
    public List<byte> subhandDorlachRank { get { return SubhandDorlachRank; } set { SubhandDorlachRank = value; } }
    public List<byte> jewelryNeckRank { get { return JewelryNeckRank; } set { JewelryNeckRank = value; } }
    public List<byte> jewelryFingerRank { get { return JewelryFingerRank; } set { JewelryFingerRank = value; } }
    public List<byte> armorHeadHRank { get { return ArmorHeadHRank; } set { ArmorHeadHRank = value; } }
    public List<byte> armorBodyHRank { get { return ArmorBodyHRank; } set { ArmorBodyHRank = value; } }
    public List<byte> armorHandHRank { get { return ArmorHandHRank; } set { ArmorHandHRank = value; } }
    public List<byte> armorBackHRank { get { return ArmorBackHRank; } set { ArmorBackHRank = value; } }
    public List<byte> armorFootHRank { get { return ArmorFootHRank; } set { ArmorFootHRank = value; } }
    public List<byte> armorHeadLRank { get { return ArmorHeadLRank; } set { ArmorHeadLRank = value; } }
    public List<byte> armorBodyLRank { get { return ArmorBodyLRank; } set { ArmorBodyLRank = value; } }
    public List<byte> armorHandLRank { get { return ArmorHandLRank; } set { ArmorHandLRank = value; } }
    public List<byte> armorBackLRank { get { return ArmorBackLRank; } set { ArmorBackLRank = value; } }
    public List<byte> armorFootLRank { get { return ArmorFootLRank; } set { ArmorFootLRank = value; } }
    public List<byte> scrollWindIRank { get { return ScrollWindIRank; } set { ScrollWindIRank = value; } }
    public List<byte> scrollFireIRank { get { return ScrollFireIRank; } set { ScrollFireIRank = value; } }
    public List<byte> scrollWaterIRank { get { return ScrollWaterIRank; } set { ScrollWaterIRank = value; } }
    public List<byte> scrollGroundIRank { get { return ScrollGroundIRank; } set { ScrollGroundIRank = value; } }
    public List<byte> scrollLightIRank { get { return ScrollLightIRank; } set { ScrollLightIRank = value; } }
    public List<byte> scrollDarkIRank { get { return ScrollDarkIRank; } set { ScrollDarkIRank = value; } }
    public List<byte> scrollNoneRank { get { return ScrollNoneRank; } set { ScrollNoneRank = value; } }
    public List<byte> scrollWindIIRank { get { return ScrollWindIIRank; } set { ScrollWindIIRank = value; } }
    public List<byte> scrollFireIIRank { get { return ScrollFireIIRank; } set { ScrollFireIIRank = value; } }
    public List<byte> scrollWaterIIRank { get { return ScrollWaterIIRank; } set { ScrollWaterIIRank = value; } }
    public List<byte> scrollGroundIIRank { get { return ScrollGroundIIRank; } set { ScrollGroundIIRank = value; } }
    public List<byte> scrollLightIIRank { get { return ScrollLightIIRank; } set { ScrollLightIIRank = value; } }
    public List<byte> scrollDarkIIRank { get { return ScrollDarkIIRank; } set { ScrollDarkIIRank = value; } }
}

//销售记录实例
public class SalesRecordObject
{
    private List<short> WeaponSwordNum;
    private List<short> WeaponAxeNum;
    private List<short> WeaponSpearNum;
    private List<short> WeaponHammerNum;
    private List<short> WeaponBowNum;
    private List<short> WeaponStaffNum;
    private List<short> SubhandShieldNum;
    private List<short> SubhandDorlachNum;
    private List<short> JewelryNeckNum;
    private List<short> JewelryFingerNum;
    private List<short> ArmorHeadHNum;
    private List<short> ArmorBodyHNum;
    private List<short> ArmorHandHNum;
    private List<short> ArmorBackHNum;
    private List<short> ArmorFootHNum;
    private List<short> ArmorHeadLNum;
    private List<short> ArmorBodyLNum;
    private List<short> ArmorHandLNum;
    private List<short> ArmorBackLNum;
    private List<short> ArmorFootLNum;
    private List<short> ScrollWindINum;
    private List<short> ScrollFireINum;
    private List<short> ScrollWaterINum;
    private List<short> ScrollGroundINum;
    private List<short> ScrollLightINum;
    private List<short> ScrollDarkINum;
    private List<short> ScrollNoneNum;
    private List<short> ScrollWindIINum;
    private List<short> ScrollFireIINum;
    private List<short> ScrollWaterIINum;
    private List<short> ScrollGroundIINum;
    private List<short> ScrollLightIINum;
    private List<short> ScrollDarkIINum;

    private List<int> WeaponSwordGold;
    private List<int> WeaponAxeGold;
    private List<int> WeaponSpearGold;
    private List<int> WeaponHammerGold;
    private List<int> WeaponBowGold;
    private List<int> WeaponStaffGold;
    private List<int> SubhandShieldGold;
    private List<int> SubhandDorlachGold;
    private List<int> JewelryNeckGold;
    private List<int> JewelryFingerGold;
    private List<int> ArmorHeadHGold;
    private List<int> ArmorBodyHGold;
    private List<int> ArmorHandHGold;
    private List<int> ArmorBackHGold;
    private List<int> ArmorFootHGold;
    private List<int> ArmorHeadLGold;
    private List<int> ArmorBodyLGold;
    private List<int> ArmorHandLGold;
    private List<int> ArmorBackLGold;
    private List<int> ArmorFootLGold;
    private List<int> ScrollWindIGold;
    private List<int> ScrollFireIGold;
    private List<int> ScrollWaterIGold;
    private List<int> ScrollGroundIGold;
    private List<int> ScrollLightIGold;
    private List<int> ScrollDarkIGold;
    private List<int> ScrollNoneGold;
    private List<int> ScrollWindIIGold;
    private List<int> ScrollFireIIGold;
    private List<int> ScrollWaterIIGold;
    private List<int> ScrollGroundIIGold;
    private List<int> ScrollLightIIGold;
    private List<int> ScrollDarkIIGold;

    public SalesRecordObject(List<short> weaponSwordNum, List<short> weaponAxeNum, List<short> weaponSpearNum, List<short> weaponHammerNum, List<short> weaponBowNum, List<short> weaponStaffNum,
            List<short> subhandShieldNum, List<short> subhandDorlachNum, List<short> jewelryNeckNum, List<short> jewelryFingerNum,
            List<short> armorHeadHNum, List<short> armorBodyHNum, List<short> armorHandHNum, List<short> armorBackHNum, List<short> armorFootHNum,
            List<short> armorHeadLNum, List<short> armorBodyLNum, List<short> armorHandLNum, List<short> armorBackLNum, List<short> armorFootLNum,
            List<short> scrollWindINum, List<short> scrollFireINum, List<short> scrollWaterINum, List<short> scrollGroundINum, List<short> scrollLightINum, List<short> scrollDarkINum, List<short> scrollNoneNum,
            List<short> scrollWindIINum, List<short> scrollFireIINum, List<short> scrollWaterIINum, List<short> scrollGroundIINum, List<short> scrollLightIINum, List<short> scrollDarkIINum,
            List<int> weaponSwordGold, List<int> weaponAxeGold, List<int> weaponSpearGold, List<int> weaponHammerGold, List<int> weaponBowGold, List<int> weaponStaffGold,
            List<int> subhandShieldGold, List<int> subhandDorlachGold, List<int> jewelryNeckGold, List<int> jewelryFingerGold,
            List<int> armorHeadHGold, List<int> armorBodyHGold, List<int> armorHandHGold, List<int> armorBackHGold, List<int> armorFootHGold,
            List<int> armorHeadLGold, List<int> armorBodyLGold, List<int> armorHandLGold, List<int> armorBackLGold, List<int> armorFootLGold,
            List<int> scrollWindIGold, List<int> scrollFireIGold, List<int> scrollWaterIGold, List<int> scrollGroundIGold, List<int> scrollLightIGold, List<int> scrollDarkIGold, List<int> scrollNoneGold,
            List<int> scrollWindIIGold, List<int> scrollFireIIGold, List<int> scrollWaterIIGold, List<int> scrollGroundIIGold, List<int> scrollLightIIGold, List<int> scrollDarkIIGold
     )
    {
        this.WeaponSwordNum = weaponSwordNum;
        this.WeaponAxeNum = weaponAxeNum;
        this.WeaponSpearNum = weaponSpearNum;
        this.WeaponHammerNum = weaponHammerNum;
        this.WeaponBowNum = weaponBowNum;
        this.WeaponStaffNum = weaponStaffNum;
        this.SubhandShieldNum = subhandShieldNum;
        this.SubhandDorlachNum = subhandDorlachNum;
        this.JewelryNeckNum = jewelryNeckNum;
        this.JewelryFingerNum = jewelryFingerNum;
        this.ArmorHeadHNum = armorHeadHNum;
        this.ArmorBodyHNum = armorBodyHNum;
        this.ArmorHandHNum = armorHandHNum;
        this.ArmorBackHNum = armorBackHNum;
        this.ArmorFootHNum = armorFootHNum;
        this.ArmorHeadLNum = armorHeadLNum;
        this.ArmorBodyLNum = armorBodyLNum;
        this.ArmorHandLNum = armorHandLNum;
        this.ArmorBackLNum = armorBackLNum;
        this.ArmorFootLNum = armorFootLNum;
        this.ScrollWindINum = scrollWindINum;
        this.ScrollFireINum = scrollFireINum;
        this.ScrollWaterINum = scrollWaterINum;
        this.ScrollGroundINum = scrollGroundINum;
        this.ScrollLightINum = scrollLightINum;
        this.ScrollDarkINum = scrollDarkINum;
        this.ScrollNoneNum = scrollNoneNum;
        this.ScrollWindIINum = scrollWindIINum;
        this.ScrollFireIINum = scrollFireIINum;
        this.ScrollWaterIINum = scrollWaterIINum;
        this.ScrollGroundIINum = scrollGroundIINum;
        this.ScrollLightIINum = scrollLightIINum;
        this.ScrollDarkIINum = scrollDarkIINum;

        this.WeaponSwordGold = weaponSwordGold;
        this.WeaponAxeGold = weaponAxeGold;
        this.WeaponSpearGold = weaponSpearGold;
        this.WeaponHammerGold = weaponHammerGold;
        this.WeaponBowGold = weaponBowGold;
        this.WeaponStaffGold = weaponStaffGold;
        this.SubhandShieldGold = subhandShieldGold;
        this.SubhandDorlachGold = subhandDorlachGold;
        this.JewelryNeckGold = jewelryNeckGold;
        this.JewelryFingerGold = jewelryFingerGold;
        this.ArmorHeadHGold = armorHeadHGold;
        this.ArmorBodyHGold = armorBodyHGold;
        this.ArmorHandHGold = armorHandHGold;
        this.ArmorBackHGold = armorBackHGold;
        this.ArmorFootHGold = armorFootHGold;
        this.ArmorHeadLGold = armorHeadLGold;
        this.ArmorBodyLGold = armorBodyLGold;
        this.ArmorHandLGold = armorHandLGold;
        this.ArmorBackLGold = armorBackLGold;
        this.ArmorFootLGold = armorFootLGold;
        this.ScrollWindIGold = scrollWindIGold;
        this.ScrollFireIGold = scrollFireIGold;
        this.ScrollWaterIGold = scrollWaterIGold;
        this.ScrollGroundIGold = scrollGroundIGold;
        this.ScrollLightIGold = scrollLightIGold;
        this.ScrollDarkIGold = scrollDarkIGold;
        this.ScrollNoneGold = scrollNoneGold;
        this.ScrollWindIIGold = scrollWindIIGold;
        this.ScrollFireIIGold = scrollFireIIGold;
        this.ScrollWaterIIGold = scrollWaterIIGold;
        this.ScrollGroundIIGold = scrollGroundIIGold;
        this.ScrollLightIIGold = scrollLightIIGold;
        this.ScrollDarkIIGold = scrollDarkIIGold;
    }
    public List<short> weaponSwordNum { get { return WeaponSwordNum; } set { WeaponSwordNum = value; } }
    public List<short> weaponAxeNum { get { return WeaponAxeNum; } set { WeaponAxeNum = value; } }
    public List<short> weaponSpearNum { get { return WeaponSpearNum; } set { WeaponSpearNum = value; } }
    public List<short> weaponHammerNum { get { return WeaponHammerNum; } set { WeaponHammerNum = value; } }
    public List<short> weaponBowNum { get { return WeaponBowNum; } set { WeaponBowNum = value; } }
    public List<short> weaponStaffNum { get { return WeaponStaffNum; } set { WeaponStaffNum = value; } }
    public List<short> subhandShieldNum { get { return SubhandShieldNum; } set { SubhandShieldNum = value; } }
    public List<short> subhandDorlachNum { get { return SubhandDorlachNum; } set { SubhandDorlachNum = value; } }
    public List<short> jewelryNeckNum { get { return JewelryNeckNum; } set { JewelryNeckNum = value; } }
    public List<short> jewelryFingerNum { get { return JewelryFingerNum; } set { JewelryFingerNum = value; } }
    public List<short> armorHeadHNum { get { return ArmorHeadHNum; } set { ArmorHeadHNum = value; } }
    public List<short> armorBodyHNum { get { return ArmorBodyHNum; } set { ArmorBodyHNum = value; } }
    public List<short> armorHandHNum { get { return ArmorHandHNum; } set { ArmorHandHNum = value; } }
    public List<short> armorBackHNum { get { return ArmorBackHNum; } set { ArmorBackHNum = value; } }
    public List<short> armorFootHNum { get { return ArmorFootHNum; } set { ArmorFootHNum = value; } }
    public List<short> armorHeadLNum { get { return ArmorHeadLNum; } set { ArmorHeadLNum = value; } }
    public List<short> armorBodyLNum { get { return ArmorBodyLNum; } set { ArmorBodyLNum = value; } }
    public List<short> armorHandLNum { get { return ArmorHandLNum; } set { ArmorHandLNum = value; } }
    public List<short> armorBackLNum { get { return ArmorBackLNum; } set { ArmorBackLNum = value; } }
    public List<short> armorFootLNum { get { return ArmorFootLNum; } set { ArmorFootLNum = value; } }
    public List<short> scrollWindINum { get { return ScrollWindINum; } set { ScrollWindINum = value; } }
    public List<short> scrollFireINum { get { return ScrollFireINum; } set { ScrollFireINum = value; } }
    public List<short> scrollWaterINum { get { return ScrollWaterINum; } set { ScrollWaterINum = value; } }
    public List<short> scrollGroundINum { get { return ScrollGroundINum; } set { ScrollGroundINum = value; } }
    public List<short> scrollLightINum { get { return ScrollLightINum; } set { ScrollLightINum = value; } }
    public List<short> scrollDarkINum { get { return ScrollDarkINum; } set { ScrollDarkINum = value; } }
    public List<short> scrollNoneNum { get { return ScrollNoneNum; } set { ScrollNoneNum = value; } }
    public List<short> scrollWindIINum { get { return ScrollWindIINum; } set { ScrollWindIINum = value; } }
    public List<short> scrollFireIINum { get { return ScrollFireIINum; } set { ScrollFireIINum = value; } }
    public List<short> scrollWaterIINum { get { return ScrollWaterIINum; } set { ScrollWaterIINum = value; } }
    public List<short> scrollGroundIINum { get { return ScrollGroundIINum; } set { ScrollGroundIINum = value; } }
    public List<short> scrollLightIINum { get { return ScrollLightIINum; } set { ScrollLightIINum = value; } }
    public List<short> scrollDarkIINum { get { return ScrollDarkIINum; } set { ScrollDarkIINum = value; } }

    public List<int> weaponSwordGold { get { return WeaponSwordGold; } set { WeaponSwordGold = value; } }
    public List<int> weaponAxeGold { get { return WeaponAxeGold; } set { WeaponAxeGold = value; } }
    public List<int> weaponSpearGold { get { return WeaponSpearGold; } set { WeaponSpearGold = value; } }
    public List<int> weaponHammerGold { get { return WeaponHammerGold; } set { WeaponHammerGold = value; } }
    public List<int> weaponBowGold { get { return WeaponBowGold; } set { WeaponBowGold = value; } }
    public List<int> weaponStaffGold { get { return WeaponStaffGold; } set { WeaponStaffGold = value; } }
    public List<int> subhandShieldGold { get { return SubhandShieldGold; } set { SubhandShieldGold = value; } }
    public List<int> subhandDorlachGold { get { return SubhandDorlachGold; } set { SubhandDorlachGold = value; } }
    public List<int> jewelryNeckGold { get { return JewelryNeckGold; } set { JewelryNeckGold = value; } }
    public List<int> jewelryFingerGold { get { return JewelryFingerGold; } set { JewelryFingerGold = value; } }
    public List<int> armorHeadHGold { get { return ArmorHeadHGold; } set { ArmorHeadHGold = value; } }
    public List<int> armorBodyHGold { get { return ArmorBodyHGold; } set { ArmorBodyHGold = value; } }
    public List<int> armorHandHGold { get { return ArmorHandHGold; } set { ArmorHandHGold = value; } }
    public List<int> armorBackHGold { get { return ArmorBackHGold; } set { ArmorBackHGold = value; } }
    public List<int> armorFootHGold { get { return ArmorFootHGold; } set { ArmorFootHGold = value; } }
    public List<int> armorHeadLGold { get { return ArmorHeadLGold; } set { ArmorHeadLGold = value; } }
    public List<int> armorBodyLGold { get { return ArmorBodyLGold; } set { ArmorBodyLGold = value; } }
    public List<int> armorHandLGold { get { return ArmorHandLGold; } set { ArmorHandLGold = value; } }
    public List<int> armorBackLGold { get { return ArmorBackLGold; } set { ArmorBackLGold = value; } }
    public List<int> armorFootLGold { get { return ArmorFootLGold; } set { ArmorFootLGold = value; } }
    public List<int> scrollWindIGold { get { return ScrollWindIGold; } set { ScrollWindIGold = value; } }
    public List<int> scrollFireIGold { get { return ScrollFireIGold; } set { ScrollFireIGold = value; } }
    public List<int> scrollWaterIGold { get { return ScrollWaterIGold; } set { ScrollWaterIGold = value; } }
    public List<int> scrollGroundIGold { get { return ScrollGroundIGold; } set { ScrollGroundIGold = value; } }
    public List<int> scrollLightIGold { get { return ScrollLightIGold; } set { ScrollLightIGold = value; } }
    public List<int> scrollDarkIGold { get { return ScrollDarkIGold; } set { ScrollDarkIGold = value; } }
    public List<int> scrollNoneGold { get { return ScrollNoneGold; } set { ScrollNoneGold = value; } }
    public List<int> scrollWindIIGold { get { return ScrollWindIIGold; } set { ScrollWindIIGold = value; } }
    public List<int> scrollFireIIGold { get { return ScrollFireIIGold; } set { ScrollFireIIGold = value; } }
    public List<int> scrollWaterIIGold { get { return ScrollWaterIIGold; } set { ScrollWaterIIGold = value; } }
    public List<int> scrollGroundIIGold { get { return ScrollGroundIIGold; } set { ScrollGroundIIGold = value; } }
    public List<int> scrollLightIIGold { get { return ScrollLightIIGold; } set { ScrollLightIIGold = value; } }
    public List<int> scrollDarkIIGold { get { return ScrollDarkIIGold; } set { ScrollDarkIIGold = value; } }
}

//顾客来访记录实例
public class CustomerRecordObject
{
    private List<short> ComeNum;
    private List<short> GoldPoorNum;
    private List<short> GoldNormalNum;
    private List<short> GoldRichNum;
    private List<short> GoldVeryRichNum;
    private List<short> GoToShopNum;
    private List<short> GoToShopWeaponNum;
    private List<short> GoToShopArmorNum;
    private List<short> GoToShopJewelryNum;
    private List<short> GoToShopScrollNum;
    private List<short> BuyNum;
    private List<short> BuyWeaponNum;
    private List<short> BuyArmorNum;
    private List<short> BuyJewelryNum;
    private List<short> BuyScrollNum;
    private List<short> BackNum;
    private List<short> Satisfaction;
    public CustomerRecordObject(List<short> comeNum, List<short> goldPoorNum, List<short> goldNormalNum, List<short> goldRichNum, List<short> goldVeryRichNum,
         List<short> goToShopNum, List<short> goToShopWeaponNum, List<short> goToShopArmorNum, List<short> goToShopJewelryNum, List<short> goToShopScrollNum,
         List<short> buyNum, List<short> buyWeaponNum, List<short> buyArmorNum, List<short> buyJewelryNum, List<short> buyScrollNum, List<short> backNum, List<short> satisfaction)
    {
        this.ComeNum = comeNum;
        this.GoldPoorNum = goldPoorNum;
        this.GoldNormalNum = goldNormalNum;
        this.GoldRichNum = goldRichNum;
        this.GoldVeryRichNum = goldVeryRichNum;
        this.GoToShopNum = goToShopNum;
        this.GoToShopWeaponNum = goToShopWeaponNum;
        this.GoToShopArmorNum = goToShopArmorNum;
        this.GoToShopJewelryNum = goToShopJewelryNum;
        this.GoToShopScrollNum = goToShopScrollNum;
        this.BuyNum = buyNum;
        this.BuyWeaponNum = buyWeaponNum;
        this.BuyArmorNum = buyArmorNum;
        this.BuyJewelryNum = buyJewelryNum;
        this.BuyScrollNum = buyScrollNum;
        this.BackNum = backNum;
        this.Satisfaction = satisfaction;
    }
   public List<short> comeNum { get { return ComeNum; } set { ComeNum = value; } }
    public List<short> goldPoorNum { get { return GoldPoorNum; } set { GoldPoorNum = value; } }
    public List<short> goldNormalNum { get { return GoldNormalNum; } set { GoldNormalNum = value; } }
    public List<short> goldRichNum { get { return GoldRichNum; } set { GoldRichNum = value; } }
    public List<short> goldVeryRichNum { get { return GoldVeryRichNum; } set { GoldVeryRichNum = value; } }
    public List<short> goToShopNum { get { return GoToShopNum; } set { GoToShopNum = value; } }
    public List<short> goToShopWeaponNum { get { return GoToShopWeaponNum; } set { GoToShopWeaponNum = value; } }
    public List<short> goToShopArmorNum { get { return GoToShopArmorNum; } set { GoToShopArmorNum = value; } }
    public List<short> goToShopJewelryNum { get { return GoToShopJewelryNum; } set { GoToShopJewelryNum = value; } }
    public List<short> goToShopScrollNum { get { return GoToShopScrollNum; } set { GoToShopScrollNum = value; } }
    public List<short> buyNum { get { return BuyNum; } set { BuyNum = value; } }
    public List<short> buyWeaponNum { get { return BuyWeaponNum; } set { BuyWeaponNum = value; } }
    public List<short> buyArmorNum { get { return BuyArmorNum; } set { BuyArmorNum = value; } }
    public List<short> buyJewelryNum { get { return BuyJewelryNum; } set { BuyJewelryNum = value; } }
    public List<short> buyScrollNum { get { return BuyScrollNum; } set { BuyScrollNum = value; } }
    public List<short> backNum { get { return BackNum; } set { BackNum = value; } }
    public List<short> satisfaction { get { return Satisfaction; } set { Satisfaction = value; } }
}

//顾客实例
public class CustomerObject
{
    private int ID;
    private string Name;
    private short HeroType;
    private string Pic;
    private int Gold;
    private short DistrictID;//访问的地区
    private ShopType ShopType;//目标店铺的类型
    private int BuildingID;//根据地区和类型选定的店铺建筑ID列表
    private BucketList BucketList;
    private CustomerStage Stage;
    private byte Layer;
    private short Satisfaction;
    public CustomerObject(int id, string name, short heroType, string pic, int gold, short districtID, ShopType shopType, int buildingID, BucketList bucketList, CustomerStage stage, byte layer, short satisfaction
        )
    {
        this.ID = id;
        this.Name = name;
        this.HeroType = heroType;
        this.Pic = pic;
        this.Gold = gold;
        this.DistrictID = districtID;
        this.ShopType = shopType;
        this.BuildingID = buildingID;
        this.BucketList = bucketList;
        this.Stage = stage;
        this.Layer = layer;
        this.Satisfaction = satisfaction;
    }
    public int id { get { return ID; } }
    public string name { get { return Name; } }
    public short heroType { get { return HeroType; } }
    public string pic { get { return Pic; } }
    public int gold { get { return Gold; } set { Gold = value; } }
    public short districtID { get { return DistrictID; } }
    public ShopType shopType { get { return ShopType; } }
    public int buildingID { get { return BuildingID; } set { BuildingID = value; } }
    public BucketList bucketList { get { return BucketList; } }
    public CustomerStage stage { get { return Stage; } set { Stage = value; } }
    public byte layer { get { return Layer; } set { Layer = value; } }
    public short satisfaction { get { return Satisfaction; } set { Satisfaction = value; } }
}

//愿望单
public class BucketList
{
    private ItemTypeBig TypeBig;
    private ItemTypeSmall TypeSmall;
    private short PrototypeID;//原型ID
    private short Num;
    private byte Rank;
    public BucketList(ItemTypeBig typeBig, ItemTypeSmall typeSmall, short prototypeID, short num, byte rank)
    {
        this.TypeBig = typeBig;
        this.TypeSmall = typeSmall;
        this.PrototypeID = prototypeID;
        this.Num = num;
        this.Rank = rank;
    }
    public ItemTypeBig typeBig { get { return TypeBig; } }
    public ItemTypeSmall typeSmall { get { return TypeSmall; } }
    public short prototypeID { get { return PrototypeID; } }
    public short num { get { return Num; } }
    public byte rank { get { return Rank; } }
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
    public byte InputWind;
    public byte InputFire;
    public byte InputWater;
    public byte InputGround;
    public byte InputLight;
    public byte InputDark;
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
    public short InputWind;
    public short InputFire;
    public short InputWater;
    public short InputGround;
    public short InputLight;
    public short InputDark;
    public short OutputCereal;
    public short OutputVegetable;
    public short OutputFruit;
    public short OutputMeat;
    public short OutputFish;
    public short OutputBeer;
    public short OutputWine;
    public short OutputWood;
    public short OutputStone;
    public short OutputMetal;
    public short OutputLeather;
    public short OutputCloth;
    public short OutputTwine;
    public short OutputBone;
    public short OutputWind;
    public short OutputFire;
    public short OutputWater;
    public short OutputGround;
    public short OutputLight;
    public short OutputDark;
}

public class ExecuteEventObject
{
    private ExecuteEventType Type;
    private int StartTime;
    private int EndTime;
    private List<List<int>> Value;
    public ExecuteEventObject( ExecuteEventType type, int startTime, int endTime, List<List<int>> value)
    {
        this.Type = type;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Value = value;
    }
    public ExecuteEventType type { get { return Type; } }
    public int startTime { get { return StartTime; } }
    public int endTime { get { return EndTime; } }
    public List<List<int>> value { get { return Value; } }

}

