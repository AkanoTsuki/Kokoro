using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoopEffect : MonoBehaviour
{
    private float fps = 15.0f;
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

    public void Play(string name,int posY,float scale)
    {
        currentIndex = 0;
        //GetComponent<RectTransform>().anchoredPosition = location;
        needFrames = Resources.LoadAll<Sprite>("Image/Effect/" + name);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, posY);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(needFrames[0].texture.width / (float)needFrames.Length* scale, needFrames[0].texture.height * scale);

        isPlay = true;
    }
    public void PlayRoleWalkRight(string name)
    {
        currentIndex = 0;
        //GetComponent<RectTransform>().anchoredPosition = location;
        needFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + name+ "/Walk_Right");
        //gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(needFrames[0].texture.width / (float)needFrames.Length , needFrames[0].texture.height );

        isPlay = true;
    }

    public void Stop()
    {
        isPlay = false;
    }
}
