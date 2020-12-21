﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InteractiveLabel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public LabelType labelType;
    public int index;

    //英雄装备专用变量
    public EquipPart equipPart;
    public int heroID;

    public Button btn;
    GameControl gc;
    void Start()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
        if (labelType == LabelType.BuildingInBuild)
        {
            btn.onClick.AddListener(delegate () { gc.CreateBuildEvent((short)index); });
        }
        else if (labelType == LabelType.Item)
        {
            btn.onClick.AddListener(delegate ()
            {
                ItemListAndInfoPanel.Instance.nowItemID = index;
                ItemListAndInfoPanel.Instance.UpdateInfo(gc.itemDic[index]);
            });

            // btn.onClick.AddListener(delegate () { BuildingPanel.Instance.OnShow(gc.buildingDic[index], 686, -88, -45); });
        }
        else if (labelType == LabelType.HeroInSelect)
        {
            btn.onClick.AddListener(delegate ()
            {
                HeroSelectPanel.Instance.nowSelectedHeroID = index;
                HeroSelectPanel.Instance.UpdateDesInfo();
            });
        }
        else if (labelType == LabelType.HeroInSelectToCheck)
        {

            btn.onClick.AddListener(delegate ()
            {
                HeroSelectPanel.Instance.nowSelectedHeroID = index;
                HeroSelectPanel.Instance.UpdateDesInfo();
                HeroPanel.Instance.nowSelectedHeroID = index;
                HeroPanel.Instance.OnShow(gc.heroDic[index], HeroPanel.Instance.nowEquipState, HeroPanel.Instance.nowSkillState,  (int)(HeroSelectPanel.Instance.GetComponent<RectTransform>().anchoredPosition.x + HeroSelectPanel.Instance.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)(HeroSelectPanel.Instance.GetComponent<RectTransform>().anchoredPosition.y), 5000);

            });
        }
        else if (labelType == LabelType.ItemToSet)
        {
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (labelType == LabelType.NewGameHero)
        {
            GameControlInNewGame gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();
            HeroPanel.Instance.OnShow(gci.temp_HeroList[index], false, false, 374, -32, -374);
        }
        else if (labelType == LabelType.NewGameLeader)
        {
            GameControlInNewGame gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInNewGame>();
            HeroPanel.Instance.OnShow(gci.temp_Leader, false, false, 374, -32, -234);
        }
        else if (labelType == LabelType.Item)
        {
            ItemListAndInfoPanel.Instance.UpdateInfo(gc.itemDic[index]);
        }
        else if (labelType == LabelType.HeroInSelect)
        {
            HeroPanel.Instance.OnShow(gc.heroDic[index], false, false, (int)(BuildingPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + BuildingPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing),
               (int)BuildingPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y, 5000);
        }
        else if (labelType == LabelType.HeroInSelectToCheck)
        {
            HeroPanel.Instance.OnShow(gc.heroDic[index], false, false, (int)(HeroSelectPanel.Instance.GetComponent<RectTransform>().anchoredPosition.x + HeroSelectPanel.Instance.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)(HeroSelectPanel.Instance.GetComponent<RectTransform>().anchoredPosition.y), 5000);
        }
        else if (labelType == LabelType.EquipmentLook)
        {
            if(index!=-1)
            {
                ItemListAndInfoPanel.Instance.OnShow(index, (int)(HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.x + HeroPanel.Instance.gameObject.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing),
     (int)HeroPanel.Instance.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            }
  
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
        }
        else if (labelType == LabelType.HeroInSelect)
        {
            HeroPanel.Instance.OnHide();
        }
        else if (labelType == LabelType.HeroInSelectToCheck)
        {
            if (HeroPanel.Instance.nowSelectedHeroID == -1)
            {
                HeroPanel.Instance.OnHide();
                return;
            }

            HeroPanel.Instance.OnShow(gc.heroDic[HeroPanel.Instance.nowSelectedHeroID], HeroPanel.Instance.nowEquipState, HeroPanel.Instance.nowSkillState, (int)(HeroSelectPanel.Instance.GetComponent<RectTransform>().anchoredPosition.x + HeroSelectPanel.Instance.GetComponent<RectTransform>().sizeDelta.x + GameControl.spacing), (int)(HeroSelectPanel.Instance.GetComponent<RectTransform>().anchoredPosition.y), 5000);

        }
        else if (labelType == LabelType.EquipmentLook)
        {
            ItemListAndInfoPanel.Instance.OnHide();
        }
    }
}
