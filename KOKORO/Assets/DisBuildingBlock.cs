using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DisBuildingBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!DistrictMapPanel.Instance.isChoose)
        {
            GetComponent<Outline>().enabled = true;
        }
     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Outline>().enabled = false;
    }
}
