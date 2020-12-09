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
        public List<ItemPrototype> Item;
        public List<CreateHeroType> HeroType;
    }

    public static Dictionary<string, ItemPrototype> mItemDict = new Dictionary<string, ItemPrototype>();

    public static Dictionary<string, CreateHeroType> cCreateHeroTypeDict = new Dictionary<string, CreateHeroType>();
    
    
    public static Dictionary<Attribute, CreateHeroRank> cCreateHeroRankDict = new Dictionary<Attribute, CreateHeroRank>();

    public static void InitCreateHeroTypeDict()
    {
       //cCreateHeroTypeDict.Add("Warrior-0",new CreateHeroType());
        
    }
    public static void InitCreateHeroRankDict()
    {
        cCreateHeroRankDict.Add(Attribute.Hp, new CreateHeroRank(new int[] { 120, 100, 80, 60, 40 }, new int[] { 150, 120, 100, 80, 60 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.Mp, new CreateHeroRank(new int[] { 120, 100, 80, 60, 40 }, new int[] { 150, 120, 100, 80, 60 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));

        cCreateHeroRankDict.Add(Attribute.AtkMax, new CreateHeroRank(new int[] { 13, 11, 8, 5, 3 }, new int[] { 15, 13, 11, 8, 5 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.MAtkMax, new CreateHeroRank(new int[] { 13, 11, 8, 5, 3 }, new int[] { 15, 13, 11, 8, 5 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));

        cCreateHeroRankDict.Add(Attribute.Def, new CreateHeroRank(new int[] { 8, 6, 4, 2, 0 }, new int[] { 10, 8, 6, 4, 2 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.MDef, new CreateHeroRank(new int[] { 8, 6, 4, 2, 0 }, new int[] { 10, 8, 6, 4, 2 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));

        cCreateHeroRankDict.Add(Attribute.Hit, new CreateHeroRank(new int[] { 17, 15, 13, 11, 9 }, new int[] { 20, 17, 15, 13, 11 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.Dod, new CreateHeroRank(new int[] { 17, 15, 13, 11, 9 }, new int[] { 20, 17, 15, 13, 11 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));
        cCreateHeroRankDict.Add(Attribute.CriR, new CreateHeroRank(new int[] { 17, 15, 13, 11, 9 }, new int[] { 20, 17, 15, 13, 11 }, new int[,] { { 5, 15, 25, 30, 25 }, { 10, 20, 25, 25, 20 }, { 15, 25, 35, 15, 10 } }));


        cCreateHeroRankDict.Add(Attribute.WorkPlanting, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50,30,20,10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10,20,40,24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkFeeding, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkFishing, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkHunting, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkFelling, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkQuarrying, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMining, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkBuild, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMakeWeapon, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMakeArmor, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkMakeJewelry, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));
        cCreateHeroRankDict.Add(Attribute.WorkSundry, new CreateHeroRank(new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[] { 150, 120, 90, 70, 50, 30, 20, 10 }, new int[,] { { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 }, { 0, 0, 1, 5, 10, 20, 40, 24 } }));

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
        if (jsonObject.Item == null)
        {
            Debug.LogError("Item data null");
        }
        Debug.Log(jsonObject.ToString());

        foreach (ItemPrototype item in jsonObject.Item)
        {
            mItemDict[item.PrototypeID] = item;
        }

        if (jsonObject.HeroType == null)
        {
            Debug.LogError("HeroType data null");
        }
        else
        {
            foreach (CreateHeroType item in jsonObject.HeroType)
            {
                cCreateHeroTypeDict[item.PrototypeID] = item;
            }
        }
        

    }
    public static void Clear()
    {
        mItemDict.Clear();
    }
}

public class GameDataConfig : MonoBehaviour
{



    // Start is called before the first frame update
    void Awake()
    {
        
        DataManager.Init();
        DataManager.InitCreateHeroRankDict();
        //Debug.Log(DataManager.mItemDict["1"].Name);
        //Debug.Log(DataManager.cCreateHeroTypeDict["1"].Name);
        //Debug.Log(DataManager.cCreateHeroTypeDict["1"].AtkMax);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
