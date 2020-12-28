using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureTeamBlock : MonoBehaviour
{
    public Text titleText;

    public Text dungeon_nameText;
    public Text dungeon_progressText;
    public GameObject dungeon_bgListGo;
    public GameObject dungeon_heroListGo;//TODO:可能要移除
    public GameObject dungeon_fgListGo;

    public List<RectTransform> dungeon_sceneBgRt;
    public List<RectTransform> dungeon_sceneFgRt;

    public List<GameObject> dungeon_side0Go;
    public List<GameObject> dungeon_side1Go;

    public Button dungeon_selectBtn;

    public List<Image> hero_picImage;
    public List<Text> hero_nameText;
    public List<Text> hero_hpmpText;
    public List<Button> hero_setBtn;

    public Text contentText;

    public Button retreatBtn;
    public Button detailBtn;
    public Button startBtn;



}
