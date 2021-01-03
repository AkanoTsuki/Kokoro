using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomentTalk : MonoBehaviour
{
    bool isPlay = false;
    private float time = 0;
    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {

            time += Time.deltaTime;
           
            if (time >= 2f )
            {
                isPlay = false;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
                AdventureMainPanel.Instance.talkPool.Add(gameObject);
            }
        }
    }
    public void Show(string content,byte side, Vector2 location)
    {
        switch (content)
        {
            case "icon_talk_exclamation":
            case "icon_talk_question":
            case "icon_talk_ellipsis":
            case "icon_talk_sad":
            case "icon_talk_happy":
            case "icon_talk_love":
                GetComponent<Image>().sprite= Resources.Load("Image/Other/" + content+"_"+ side, typeof(Sprite)) as Sprite;
                GetComponent<RectTransform>().sizeDelta = new Vector2(32f, 32f);
                transform.GetChild(0).GetComponent<Text>().text = "";
                break;
            default:
                GetComponent<Image>().sprite = Resources.Load("Image/Other/icon_talk_empty_" + side, typeof(Sprite)) as Sprite;
                transform.GetChild(0).GetComponent<Text>().text = content;
                //Debug.Log(transform.GetChild(0).GetComponent<RectTransform>().sizeDelta);
                //Debug.Log(transform.GetChild(0).GetComponent<Text>().preferredWidth);
                GetComponent<RectTransform>().sizeDelta = new Vector2(transform.GetChild(0).GetComponent<Text>().preferredWidth + 19f, 32f);
                GetComponent<RectTransform>().anchoredPosition = location;
                break;
        }

        time = 0;
        isPlay = true;


    }
}
