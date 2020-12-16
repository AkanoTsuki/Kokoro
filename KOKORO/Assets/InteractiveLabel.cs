using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InteractiveLabel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public LabelType labelType;
    public int index;
    public Button btn;
    GameControl gc;
    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
        if (labelType == LabelType.BuildingInBuild)
        {
            btn.onClick.AddListener(delegate () { gc.Build((short)index); });
        }
        else if (labelType == LabelType.BuildingInDistrictMain)
        {
            btn.onClick.AddListener(delegate () { BuildingPanel.Instance.OnShow(gc.buildingDic[index],  686,-88,-45); });
        }
        else if (labelType == LabelType.Item)
        {
            btn.onClick.AddListener(delegate () {
                ItemListAndInfoPanel.Instance.nowItemID = index;
                ItemListAndInfoPanel.Instance.UpdateInfo(gc.itemDic[index]);
            });
 
           // btn.onClick.AddListener(delegate () { BuildingPanel.Instance.OnShow(gc.buildingDic[index], 686, -88, -45); });
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (labelType == LabelType.NewGameHero)
        {
            GameControlInNewGame gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();
            HeroPanel.Instance.OnShow(gci.temp_HeroList[index], 374, -32,-374);
        }
        else if (labelType == LabelType.NewGameLeader)
        {
            GameControlInNewGame gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();
            HeroPanel.Instance.OnShow(gci.temp_Leader, 374, -32, -234);
        }
        else if (labelType == LabelType.Item)
        {
            ItemListAndInfoPanel.Instance.UpdateInfo(gc.itemDic[index]);
            // btn.onClick.AddListener(delegate () { BuildingPanel.Instance.OnShow(gc.buildingDic[index], 686, -88, -45); });
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (labelType == LabelType.NewGameHero)
        {

            HeroPanel.Instance.OnHide();
        }
        else if (labelType == LabelType.NewGameLeader)
        {

            HeroPanel.Instance.OnHide();
        }
        else if (labelType == LabelType.Item)
        {
            if (ItemListAndInfoPanel.Instance.nowItemID == -1)
            {
                return;
            }
            if (!gc.itemDic.ContainsKey(ItemListAndInfoPanel.Instance.nowItemID))
            {
                return;
            }

            ItemListAndInfoPanel.Instance.UpdateInfo(gc.itemDic[ItemListAndInfoPanel.Instance.nowItemID]);
            // btn.onClick.AddListener(delegate () { BuildingPanel.Instance.OnShow(gc.buildingDic[index], 686, -88, -45); });
        }
    }
}
