using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureTeamBlock : MonoBehaviour
{
    public Text titleText;

    public Text dungeon_nameText;
    public Text dungeon_progressText;

    public List<Image> dungeon_elementImage;

    public GameObject dungeon_bgListGo;
    public GameObject dungeon_fgListGo;
    public GameObject dungeon_effectLayerGo;
    public GameObject dungeon_numLayerGo;
    public GameObject dungeon_talkLayerGo;

    public List<RectTransform> dungeon_sceneBgRt;
    public List<RectTransform> dungeon_sceneFgRt;

    public RectTransform dungeon_campRt;

    public List<GameObject> dungeon_side0Go;
    public List<GameObject> dungeon_side1Go;
    public List<RectTransform> dungeon_side0HpRt;
    public List<RectTransform> dungeon_side0MpRt;
    public List<RectTransform> dungeon_side1HpRt;
    public List<RectTransform> dungeon_side1MpRt;
    public List<RectTransform> dungeon_side0ApBgRt;
    public List<Text> dungeon_side0ApText;
    public List<Image> dungeon_side0ApImage;
    public List<RectTransform> dungeon_side1ApBgRt;
    public List<Text> dungeon_side1ApText;
    public List<Image> dungeon_side1ApImage;
    public List<Image> dungeon_side0SharpnessImage;
    public List<Image> dungeon_side0SharpnessBgImage;
    public List<GameObject> dungeon_side0HaloGo;

    public List<GameObject> dungeon_side0BuffsGo;
    public List<GameObject> dungeon_side1BuffsGo;

    public RectTransform dungeon_progressNowBarRt;
    public RectTransform dungeon_progressNowFlagRt;

    public RectTransform dungeon_destinationRt;
    public Image dungeon_destinationImage;
    //public Button dungeon_selectBtn;

    public RectTransform damageDataRt;
    public List<RectTransform> damageData_heroRt;
    public List<Image> damageData_picImage;
    public List<Text> damageData_valueText;

    public List<Image> hero_picImage;
    public List<Text> hero_nameText;
    public List<Text> hero_hpmpText;
   // public List<Button> hero_setBtn;

    public Text contentText;

    public RectTransform getsRt;
    public Text gets_contentText;
    public Button gets_confrimBtn;

    public Button retreatBtn;
    public Button detailBtn;
    public Button startBtn;

    public RectTransform freeRt;

}
