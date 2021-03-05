using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoopEffect : MonoBehaviour
{
    private float fps = 10.0f;
    private float time = 0;
    private int currentIndex = 0;
    bool isPlay = false;
    Sprite[] needFrames;


    
    // Update is called once per frame
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
                    currentIndex = 0;
                }
            }
        }
    }

    public void Play(string name)
    {
        currentIndex = 0;
        //GetComponent<RectTransform>().anchoredPosition = location;
        needFrames = Resources.LoadAll<Sprite>("Image/Effect/" + name);
        isPlay = true;
    }
    public void Stop()
    {
        isPlay = false;
    }
}
