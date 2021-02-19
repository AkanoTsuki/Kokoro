using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MomentEffect : MonoBehaviour
{
    private float fps = 10.0f;
    private float time = 0;
    private int currentIndex = 0;
    bool isPlay = false;
    Sprite[] needFrames;

    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            if (time >= 1.0f / fps)
            {

                gameObject.GetComponent<Image>().sprite = needFrames[currentIndex];
                //Debug.Log(gameObject.GetComponent<Image>().sprite);
                currentIndex++;
                time = 0;
                if (currentIndex >= needFrames.Length)
                {

                    isPlay = false;
                    GetComponent<RectTransform>().anchoredPosition=new Vector2(0, 5000);
                    AdventureMainPanel.Instance.effectPool.Add(gameObject);
                }
            }
        }
    }

    public void Play(string name,Vector2 location)
    {
        currentIndex = 0;
        GetComponent<RectTransform>().anchoredPosition = location;
        needFrames = Resources.LoadAll<Sprite>("Image/Effect/" + name);
        isPlay = true;
    }
}
