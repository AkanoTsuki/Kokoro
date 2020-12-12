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
    void Start()
    {
        GameControl gc = GameObject.Find("GameManager").GetComponent<GameControl>();
        if (labelType == LabelType.BuildingInBuild)
        {
            btn.onClick.AddListener(delegate () { gc.Build(index); });
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
    }
}
