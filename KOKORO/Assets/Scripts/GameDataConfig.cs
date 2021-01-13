using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class DataManager
{ 

    [System.Serializable]
    public class TestArray
    {
        
        public List<HeroPrototype> Hero;
        //public List<HeroPrototype> Hero;
        public List<ItemPrototype> Item;
        public List<SkillPrototype> Skill;
        public List<DistrictPrototype> District;
        public List<DistrictGridPrototype> DistrictGrid;
        public List<BuildingPrototype> Building;
        public List<DungeonPrototype> Dungeon;
        public List<MonsterPrototype> Monster;
        public List<LemmaPrototype> Lemma;
        public List<ProduceEquipPrototype> ProduceEquip;
        public List<ProduceResourcePrototype> ProduceResource;
    }

    

    public static Dictionary<int, HeroPrototype> mHeroDict = new Dictionary<int, HeroPrototype>();
    public static Dictionary<Attribute, CreateHeroRank> cCreateHeroRankDict = new Dictionary<Attribute, CreateHeroRank>();

    public static Dictionary<int, ItemPrototype> mItemDict = new Dictionary<int, ItemPrototype>();
    public static Dictionary<int, SkillPrototype> mSkillDict = new Dictionary<int, SkillPrototype>();
    //public static Dictionary<int, HeroPrototype> mHeroDict = new Dictionary<int, HeroPrototype>();

    public static Dictionary<int, DistrictPrototype> mDistrictDict = new Dictionary<int, DistrictPrototype>();
    public static Dictionary<int, DistrictGridPrototype> mDistrictGridDict = new Dictionary<int, DistrictGridPrototype>();
    public static Dictionary<int, BuildingPrototype> mBuildingDict = new Dictionary<int, BuildingPrototype>();
    public static Dictionary<int, DungeonPrototype> mDungeonDict = new Dictionary<int, DungeonPrototype>();
    public static Dictionary<int, MonsterPrototype> mMonsterDict = new Dictionary<int, MonsterPrototype>();
    public static Dictionary<int, LemmaPrototype> mLemmaDict = new Dictionary<int, LemmaPrototype>();
    public static Dictionary<int, ProduceEquipPrototype> mProduceEquipDict = new Dictionary<int, ProduceEquipPrototype>();
    public static Dictionary<int, ProduceResourcePrototype> mProduceResourceDict = new Dictionary<int, ProduceResourcePrototype>();

    public static string[] mNameMan = { "亚伦", "亚伯", "亚伯拉罕", "亚当", "艾德里安", "艾登", "艾丹", "阿尔瓦", "亚历克斯", "亚历山大", "艾伦", "艾伯特", "阿尔弗雷德", "安德鲁", "安迪", "安格斯", "安东尼", "阿波罗", "阿诺德", "亚瑟", "奥古斯特", "奥斯汀", "本", "本杰明", "伯特", "本森", "比尔", "比利", "布莱克", "鲍伯", "鲍比", "布拉德", "布兰登", "布兰特", "布伦特", "布赖恩", "布朗", "布鲁斯", "迦勒", "卡梅伦", "卡尔", "卡洛斯", "凯里", "卡斯帕", "塞西", "查尔斯", "采尼", "克里斯", "克里斯蒂安", "克里斯多夫", "克拉克", "柯利弗", "科迪", "科尔", "科林", "科兹莫", "丹尼尔", "丹尼", "达尔文", "大卫", "丹尼斯", "德里克", "狄克", "唐纳德", "道格拉斯", "杜克", "迪伦", "埃迪", "埃德加", "爱迪生", "艾德蒙", "爱德华", "艾德文", "以利亚", "艾略特", "埃尔维斯", "埃里克", "伊桑", "埃文", "福特", "弗兰克思", "弗兰克", "富兰克林", "弗瑞德", "加百利", "加比", "加菲尔德", "加里", "加文", "杰弗里", "乔治", "基诺", "格林", "格林顿", "汉克", "哈帝", "哈里森", "哈利", "海顿", "亨利", "希尔顿", "雨果", "汉克", "霍华德", "亨利", "伊恩", "伊格纳茨", "伊凡", "艾萨克", "以赛亚", "艾塞亚", "杰克", "杰克逊", "雅各布", "詹姆士", "詹森", "杰伊", "杰弗瑞", "杰罗姆", "杰瑞", "杰西", "吉姆", "吉米", "乔", "约翰", "约翰尼", "乔尼", "乔纳森", "乔丹", "约瑟夫", "约书亚", "贾斯汀", "凯斯", "肯", "肯尼迪", "肯尼斯", "肯尼", "凯文", "凯尔", "兰斯", "拉里", "劳伦特", "劳伦斯", "利安德尔", "李", "雷欧", "雷纳德", "利奥波特", "莱斯利", "劳伦", "劳瑞", "劳瑞恩", "路易斯", "卢克", "路加", "马库斯", "马西", "马克", "马科斯", "马尔斯", "马歇尔", "马丁", "马文", "梅森", "马修", "马克斯", "迈克尔", "米奇", "麦克", "纳撒尼尔", "尼尔", "尼尔森", "尼古拉斯", "尼克", "诺亚", "诺曼", "奥利弗", "奥斯卡", "欧文", "帕特里克", "派翠克", "保罗", "彼得", "菲利普", "菲比", "昆廷", "兰德尔", "伦道夫", "兰迪", "雷", "列得", "雷克斯", "理查德", "里奇", "赖利", "瑞利", "罗伯特", "罗宾", "罗宾逊", "鲁宾逊", "洛克", "罗杰", "罗纳德", "罗文", "罗伊", "赖安", "萨姆", "山姆", "萨米", "塞缪尔", "斯考特", "肖恩", "西德尼", "西蒙", "所罗门", "斯帕克", "斯宾塞", "斯派克", "斯坦利", "史蒂夫", "史蒂文", "斯图尔特", "斯图亚特", "特伦斯", "特里", "泰德", "托马斯", "提姆", "蒂莫西", "托德", "汤米", "汤姆", "托马斯", "托尼", "泰勒", "奥特曼", "尤利塞斯", "范", "弗恩", "弗农", "维克多", "文森特", "华纳", "沃伦", "韦恩", "卫斯理", "威廉", "威利", "维利", "扎克", "圣扎迦利" };
    public static string[] mNameWoman = { "阿比盖尔", "艾比", "艾达", "阿德莱德", "艾德琳", "亚历桑德拉", "艾丽莎", "艾米", "亚历克西斯", "爱丽丝", "艾丽西娅", "艾琳娜", "艾莉森", "艾莉莎", "爱丽丝娅", "阿曼达", "艾美", "安伯", "阿纳斯塔西娅", "安德莉亚", "安琪", "安吉拉", "安吉莉亚", "安吉莉娜", "安", "安娜", "安妮", "安尼塔", "艾莉尔", "阿普里尔", "艾许莉", "阿什利", "艾希礼", "欧蕊", "阿维娃", "笆笆拉", "芭比", "贝亚特", "比阿特丽斯", "贝基", "贝拉", "贝斯", "贝蒂", "布兰奇", "邦妮", "布伦达", "布莱安娜", "布兰妮", "布列塔尼", "卡米尔", "莰蒂丝", "坎蒂", "卡瑞娜", "卡门", "凯罗尔", "卡罗琳", "凯丽", "凯莉", "卡桑德拉", "凯西", "凯瑟琳", "凯茜", "切尔西", "沙琳", "夏洛特", "切莉", "雪莉尔", "克洛伊", "克莉丝", "克里斯蒂娜", "克里斯汀", "克里斯蒂", "辛迪", "克莱尔", "克劳迪娅", "克莱门特", "克劳瑞丝", "康妮", "康斯坦斯", "科拉", "科瑞恩", "科瑞斯特尔", "戴茜", "达芙妮", "达茜", "戴夫", "黛比", "黛博拉", "黛布拉", "黛米", "黛安娜", "德洛丽丝", "堂娜", "多拉", "桃瑞丝", "伊迪丝", "伊迪萨", "伊莱恩", "埃莉诺", "伊丽莎白", "埃拉", "爱伦", "艾莉", "艾米瑞达", "艾米丽", "艾玛", "伊妮德", "埃尔莎", "埃莉卡", "爱斯特尔", "爱丝特", "尤杜拉", "伊娃", "伊芙", "伊夫林", "芬妮", "费怡", "菲奥纳", "福罗拉", "弗罗伦丝", "弗郎西丝", "弗雷德里卡", "弗里达", "吉娜", "吉莉安", "格拉蒂丝", "格罗瑞娅", "格瑞丝", "格瑞塔", "格温多琳", "汉娜", "海莉", "赫柏", "海伦娜", "海伦", "汉纳", "海蒂", "希拉里", "希拉蕊", "希拉莉", "英格丽德", "伊莎贝拉", "爱沙拉", "艾琳", "艾丽丝", "艾维", "杰奎琳", "詹米", "简", "珍妮特", "贾斯敏", "姬恩", "珍娜", "詹妮弗", "詹妮", "杰西卡", "杰西", "姬尔", "琼", "乔安娜", "乔斯林", "乔莉埃特", "约瑟芬", "乔茜", "乔伊", "乔伊斯", "朱迪丝", "朱蒂", "朱莉娅", "朱莉安娜", "朱莉", "朱恩", "凯琳", "卡瑞达", "凯瑟琳", "凯特", "凯西", "卡蒂", "卡特里娜", "凯", "凯拉", "凯莉", "凯尔西", "特里娜", "基蒂", "莱瑞拉", "蕾西", "劳拉", "罗兰", "劳伦", "莉娜", "莉迪娅", "莉莲", "莉莉", "琳达", "琳赛", "丽莎", "莉兹", "洛拉", "罗琳", "路易莎", "路易丝", "露西娅", "露茜", "露西妮", "露露", "莉迪娅", "莉蒂亚", "林恩", "梅布尔", "玛佩尔", "马德琳", "玛姬", "玛米", "曼达", "曼迪", "玛格丽特", "玛丽亚", "玛里琳", "玛丽莲", "玛丽琳", "玛莎", "梅维丝", "玛丽", "玛蒂尔达", "莫琳", "梅维丝", "玛克辛", "梅", "梅米", "梅甘", "梅琳达", "梅利莎", "美洛蒂", "默西迪丝", "梅瑞狄斯", "米娅", "米歇尔", "米莉", "米兰达", "米里亚姆", "米娅", "茉莉", "莫尼卡", "摩尔根", "摩根", "南茜", "娜塔莉", "娜塔莎", "妮可", "尼基塔", "尼娜", "娜拉", "诺拉", "诺玛", "尼迪亚", "奥克塔维亚", "奥琳娜", "奥利维亚", "奥菲莉娅", "奥帕", "帕梅拉", "帕特丽夏", "芭迪", "保拉", "波琳", "珀尔", "帕姬", "菲洛米娜", "菲比", "菲丽丝", "波莉", "普里西拉", "昆蒂娜", "雷切尔", "丽贝卡", "瑞加娜", "丽塔", "罗丝", "洛克萨妮", "露丝", "萨布丽娜", "萨莉", "桑德拉", "萨曼莎", "萨米", "桑迪", "莎拉", "萨瓦纳", "萨瓦娜", "斯佳丽", "斯嘉丽", "塞尔玛", "塞琳娜", "塞丽娜", "莎伦", "雪莉", "斯莱瑞", "西尔维亚", "索尼亚", "索菲娅", "丝塔茜", "丝特拉", "斯蒂芬妮", "苏", "萨妮", "苏珊", "塔玛拉", "谭雅坦尼娅", "塔莎", "特莉萨", "苔丝", "蒂凡妮", "蒂娜", "棠雅", "东妮亚", "特蕾西", "厄休拉", "温妮莎", "维纳斯", "维拉", "维姬", "维多利亚", "维尔莉特", "维吉妮亚", "维达", "薇薇安", "旺达", "温蒂", "惠特尼", "韦恩", "温妮", "尤兰达", "伊薇特", "伊温妮", "塞尔达", "佐伊", "卓拉" };
    //初始化方法
    public static void InitCreateHeroRankDict()
    {
        cCreateHeroRankDict.Add(Attribute.Hp, new CreateHeroRank(new int[] { 120, 100, 80, 60, 40 }, new int[] { 150, 120, 100, 80, 60 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.Mp, new CreateHeroRank(new int[] { 120, 100, 80, 60, 40 }, new int[] { 150, 120, 100, 80, 60 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));

        cCreateHeroRankDict.Add(Attribute.AtkMax, new CreateHeroRank(new int[] { 13, 11, 8, 5, 3 }, new int[] { 15, 13, 11, 8, 5 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.MAtkMax, new CreateHeroRank(new int[] { 13, 11, 8, 5, 3 }, new int[] { 15, 13, 11, 8, 5 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));

        cCreateHeroRankDict.Add(Attribute.Def, new CreateHeroRank(new int[] { 8, 6, 4, 2, 0 }, new int[] { 10, 8, 6, 4, 2 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.MDef, new CreateHeroRank(new int[] { 8, 6, 4, 2, 0 }, new int[] { 10, 8, 6, 4, 2 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));

        cCreateHeroRankDict.Add(Attribute.Hit, new CreateHeroRank(new int[] { 17, 15, 13, 11, 9 }, new int[] { 20, 17, 15, 13, 11 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.Dod, new CreateHeroRank(new int[] { 17, 15, 13, 11, 9 }, new int[] { 20, 17, 15, 13, 11 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.CriR, new CreateHeroRank(new int[] { 17, 15, 13, 11, 9 }, new int[] { 20, 17, 15, 13, 11 }, new byte[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));


        cCreateHeroRankDict.Add(Attribute.WorkPlanting, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50,30,20,10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10,20,40,24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkFeeding, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkFishing, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkHunting, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkFelling, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkQuarrying, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMining, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkBuild, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMakeWeapon, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMakeArmor, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMakeJewelry, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMakeScroll, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkSundry, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new byte[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));

    }

    public static void Init()
    {
        Clear();
        string jsonTest = ((TextAsset)Resources.Load("GameDataConfig")).text;
        TestArray jsonObject = JsonUtility.FromJson<TestArray>(jsonTest);
        if (jsonObject == null)
        {
            Debug.LogError("ExampleData data null");
        }


        if (jsonObject.Hero == null) { Debug.LogError("Hero data null"); }
        foreach (HeroPrototype item in jsonObject.Hero) { mHeroDict[item.ID] = item; }

        if (jsonObject.Item == null){Debug.LogError("Item data null");}
        foreach (ItemPrototype item in jsonObject.Item){ mItemDict[item.ID] = item;}

        if (jsonObject.Skill == null) { Debug.LogError("Skill data null"); }
        foreach (SkillPrototype item in jsonObject.Skill) { mSkillDict[item.ID] = item; }

        if (jsonObject.District == null) { Debug.LogError("District data null"); }
        foreach (DistrictPrototype item in jsonObject.District) { mDistrictDict[item.ID] = item; }

        if (jsonObject.DistrictGrid == null) { Debug.LogError("DistrictGrid data null"); }
        foreach (DistrictGridPrototype item in jsonObject.DistrictGrid) { mDistrictGridDict[item.ID] = item; }

        if (jsonObject.Building == null) { Debug.LogError("Building data null"); }
        foreach (BuildingPrototype item in jsonObject.Building) { mBuildingDict[item.ID] = item; }

        if (jsonObject.Dungeon == null) { Debug.LogError("Dungeon data null"); }
        foreach (DungeonPrototype item in jsonObject.Dungeon) { mDungeonDict[item.ID] = item; }

        if (jsonObject.Monster == null) { Debug.LogError("Monster data null"); }
        foreach (MonsterPrototype item in jsonObject.Monster) { mMonsterDict[item.ID] = item; }

        if (jsonObject.Lemma == null) { Debug.LogError("Lemma data null"); }
        foreach (LemmaPrototype item in jsonObject.Lemma) { mLemmaDict[item.ID] = item; }

        if (jsonObject.ProduceEquip == null) { Debug.LogError("ProduceEquip data null"); }
        foreach (ProduceEquipPrototype item in jsonObject.ProduceEquip) { mProduceEquipDict[item.ID] = item; }
        
        if (jsonObject.ProduceResource == null) { Debug.LogError("ProduceResource data null"); }
        foreach (ProduceResourcePrototype item in jsonObject.ProduceResource) { mProduceResourceDict[item.ID] = item; }


    }
    public static void Clear()
    {
        mItemDict.Clear();
    }
}

public class GameDataConfig : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        DataManager.Init();
        DataManager.InitCreateHeroRankDict();

        //Debug.Log(DataManager.cCreateHeroTypeDict[1].Name);
        //Debug.Log(DataManager.mBuildingDict[5].Name);
        //Debug.Log(DataManager.mMonsterDict[1].Name);
       Debug.Log(DataManager.mDistrictDict.Count);
        //Debug.Log(DataManager.mDungeonDict[0].FixEvent[0]);
    }

}
