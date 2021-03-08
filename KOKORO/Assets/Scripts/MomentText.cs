using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MomentText : MonoBehaviour
{
    bool isPlay = false;
    float targetY = 0f;

    public List<GameObject> pool;
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
                pool.Add(gameObject);
            }
        }
     
    }

    public void Play(string content, Vector2 location)
    {
        GetComponent<Text>().text  = content;
        GetComponent<RectTransform>().anchoredPosition = location;
        targetY = location.y + 50f;

        //Debug.Log("GetComponent<RectTransform>().anchoredPosition=" + GetComponent<RectTransform>().anchoredPosition);
        isPlay = true;
    }

    public void PlayJump(string content, Vector2 location)
    {
        GetComponent<Text>().text = content;
        GetComponent<RectTransform>().anchoredPosition = location;
        GetComponent<RectTransform>().localScale = Vector2.one;
        //GetComponent<Text>().color = Color.white;

        int fx = Random.Range(0, 2) == 0 ? -1 : 1;
        int firstXOffest = Random.Range(-20 * fx, 20 * fx);
        float secondXOffest = firstXOffest * 1.5f;

        //Tweener tweener = transform.DOMove(Vector3.zero,1f);
        //GetComponent<Text>().DOFade(0f, 1f);
        //Sequence s = GetComponent<Text>().DOFade(0f, 1f);
        Sequence s = DOTween.Sequence();
    
        s.Append(GetComponent<RectTransform>().DOJumpAnchorPos(location+new Vector2(firstXOffest, 0), Random.Range(40,50), 1, Random.Range(1.1f, 1.2f)));
       
        s.Append(GetComponent<RectTransform>().DOJumpAnchorPos(location+new Vector2(secondXOffest, Random.Range(0, 6)), 5, 1, 0.3f));
        s.AppendCallback(SetFree);
        //s.Insert(0f, GetComponent<Text>().DOFade(0f, s.Duration()));
        s.Insert(0.2f, GetComponent<RectTransform>().DOScale(Vector2.one*1.3f, 0.5f));
        s.Insert(0.8f, GetComponent<RectTransform>().DOScale(Vector2.one, 0.3f));


    }

    void SetFree()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5000);
        pool.Add(gameObject);
    }
}
