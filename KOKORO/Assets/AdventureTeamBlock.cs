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
    public GameObject dungeon_heroListGo;
    public GameObject dungeon_fgListGo;

    public List<Image> hero_picImage;
    public List<Text> hero_nameText;
    public List<Text> hero_hpmpText;
    public List<Button> hero_setBtn;

    public Text contentText;

    public Button retreatBtn;
    public Button detailBtn;
    public Button startBtn;



}
