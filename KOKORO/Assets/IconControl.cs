using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class IconControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AreaMapIconType areaMapIconType;
    public int id;

    void Start()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        if (areaMapIconType == AreaMapIconType.District)
        {
            GetComponent<Button>().onClick.AddListener(delegate ()
            {

                if (AreaMapPanel.Instance.districtInfoBlockID != id)
                {
                    float x = Screen.width / 2f - AreaMapPanel.Instance.districtGo[id].GetComponent<RectTransform>().anchoredPosition.x;
                    float y = -AreaMapPanel.Instance.districtGo[id].GetComponent<RectTransform>().anchoredPosition.y - Screen.height / 2f;
                    AreaMapPanel.Instance.transform.DOComplete();
                    AreaMapPanel.Instance.transform.DOLocalMove(new Vector2(x - Screen.width / 2f, y + Screen.height / 2f), 0.5f);
                }

                AreaMapPanel.Instance.ShowDistrictInfoBlock(id, (int)(AreaMapPanel.Instance.districtGo[id].GetComponent<RectTransform>().anchoredPosition.x + 60f), (int)(AreaMapPanel.Instance.districtGo[id].GetComponent<RectTransform>().anchoredPosition.y));
            });
        }
        else
        {
            GetComponent<Button>().onClick.AddListener(delegate () 
            {
                if (AreaMapPanel.Instance.dungeonInfoBlockID != id)
                {
                    float x = Screen.width / 2f - AreaMapPanel.Instance.dungeonGo[id].GetComponent<RectTransform>().anchoredPosition.x;
                    float y = -AreaMapPanel.Instance.dungeonGo[id].GetComponent<RectTransform>().anchoredPosition.y - Screen.height / 2f;
                    AreaMapPanel.Instance.transform.DOComplete();
                    AreaMapPanel.Instance.transform.DOLocalMove(new Vector2(x - Screen.width / 2f, y + Screen.height / 2f), 0.5f);
                }
                AreaMapPanel.Instance.ShowDungeonInfoBlock(id, (int)(AreaMapPanel.Instance.dungeonGo[id].GetComponent<RectTransform>().anchoredPosition.x + 20f), (int)(AreaMapPanel.Instance.dungeonGo[id].GetComponent<RectTransform>().anchoredPosition.y));
            });

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioControl.Instance.PlaySound("system_cursor-02");
        if (areaMapIconType == AreaMapIconType.District)
        {
            if (AreaMapPanel.Instance.districtInfoBlockID != id)
            {
                AreaMapPanel.Instance.districtGo[id].transform.localScale = new Vector2(1.2f, 1.2f);
                AreaMapPanel.Instance.districtNameGo[id].transform.localScale = new Vector2(1.2f, 1.2f);
            }
     
        }
        else if (areaMapIconType == AreaMapIconType.Dungeon)
        {
            if (AreaMapPanel.Instance.dungeonInfoBlockID != id)
            {
                AreaMapPanel.Instance.dungeonGo[id].transform.localScale = new Vector2(1.2f, 1.2f);
                AreaMapPanel.Instance.dungeonNameGo[id].transform.localScale = new Vector2(1.2f, 1.2f);
            }
      
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (areaMapIconType == AreaMapIconType.District)
        {
            AreaMapPanel.Instance.districtGo[id].transform.localScale = Vector2.one;
            AreaMapPanel.Instance.districtNameGo[id].transform.localScale = Vector2.one;
        }
        else if (areaMapIconType == AreaMapIconType.Dungeon)
        {
            AreaMapPanel.Instance.dungeonGo[id].transform.localScale = Vector2.one;
            AreaMapPanel.Instance.dungeonNameGo[id].transform.localScale = Vector2.one;
        }
    }
}
