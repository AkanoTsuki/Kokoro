using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomentText : MonoBehaviour
{
    bool isPlay = false;
    float targetY = 0f;


    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            GetComponent<RectTransform>().anchoredPosition += 20f * Vector2.up * Time.deltaTime;
            if (GetComponent<RectTransform>().anchoredPosition.y >= targetY)
            {
               
                isPlay = false;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                AdventureMainPanel.Instance.numPool.Add(gameObject);
            }
        }
     
    }

    public void Play(string content, Vector2 location)
    {
        GetComponent<Text>().text  = content;
        GetComponent<RectTransform>().anchoredPosition = location;
        targetY = location.y + 30f;

        //Debug.Log("GetComponent<RectTransform>().anchoredPosition=" + GetComponent<RectTransform>().anchoredPosition);
        isPlay = true;

     
    }
}
