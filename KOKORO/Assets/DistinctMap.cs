using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistinctMap : MonoBehaviour
{
    public Canvas canvas;//画布
    private RectTransform rectTransform;//坐标
    // Start is called before the first frame update
    GameObject go;
    bool isChoose = false;

    Vector2 pos;
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = this.transform as RectTransform; //也可以写成this.GetComponent<RectTransform>(),但是不建议；

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, canvas.worldCamera, out pos))
            {
                //rectTransform.anchoredPosition = pos;
                Debug.Log(pos);
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            go = Instantiate(Resources.Load("Prefab/UIBlock/Block_DisBuilding")) as GameObject;
            go.transform.SetParent(transform);
            isChoose = true;
        }

        if (isChoose)
        {
           
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, canvas.worldCamera, out pos))
            {
               
                pos = new Vector2(((int)pos.x / 32) * 32f,((int)pos.y / 32) * 32f);
                Debug.Log(pos);
                go.GetComponent<RectTransform>().anchoredPosition = pos;

                
            }
        }
    }
}
