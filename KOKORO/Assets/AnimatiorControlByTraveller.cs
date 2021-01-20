﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class AnimatiorControlByTraveller : MonoBehaviour
{
    GameControl gc;

    private float speed = 30.0f;
    private float fps = 10.0f;
    private float time = 0;
    private int currentIndex = 0;
    Sprite[] idleFrames;
    Sprite[] walk_DownFrames;
    Sprite[] walk_LeftFrames;
    Sprite[] walk_RightFrames;
    Sprite[] walk_UpFrames;

    Sprite[] needFrames = new Sprite[3];

    bool isPlay = false;
    public string charaName = "chara1_1";

    public int travellerID=-1;

    public List<int> pathPointList = new List<int>();
    public int nowPointIndex = 0;
    // Start is called before the first frame update

    RectTransform rt;
    Vector2 targetPos;
    private void Awake()
    {
        rt =transform.GetComponent<RectTransform>();
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;

            if (time >= 1.0f / fps)
            {
                gameObject.GetComponent<Image>().sprite = needFrames[currentIndex];
                currentIndex++;
                time = 0;
                if (currentIndex > needFrames.Length - 1)
                {
                    currentIndex = 0;

                }
            }
            Move();
        }
    }
    public void Play()
    {
        isPlay = true;
    }
    public void Stop()
    {
        isPlay = false;
    }


    public void SetCharaFrames(string name)
    {
        charaName = name;
        needFrames = new Sprite[3];

        idleFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Idle");

        walk_DownFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Down");
        walk_LeftFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        walk_RightFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Right");
        walk_UpFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Up");

        //Debug.Log(" walk_RightFrames.Length=" + walk_RightFrames.Length);
    }
    public void SetAnim(AnimStatus animStatus)
    {
      //  Debug.Log("SetAnim animStatus=" + animStatus + " charaName=" + charaName);

        switch (animStatus)
        {
            case AnimStatus.WalkUp:
                needFrames[0] = walk_UpFrames[0];
                needFrames[1] = walk_UpFrames[2];
                needFrames[2] = walk_UpFrames[1];
             //   isLoop = true;
                break;
            case AnimStatus.WalkLeft:
                needFrames[0] = walk_LeftFrames[0];
                needFrames[1] = walk_LeftFrames[2];
                needFrames[2] = walk_LeftFrames[1];
              //  isLoop = true;
                break;
            case AnimStatus.WalkRight:
                needFrames[0] = walk_RightFrames[0];
                needFrames[1] = walk_RightFrames[2];
                needFrames[2] = walk_RightFrames[1];
             //   isLoop = true;
                break;
            case AnimStatus.WalkDown:
                needFrames[0] = walk_DownFrames[0];
                needFrames[1] = walk_DownFrames[2];
                needFrames[2] = walk_DownFrames[1];
              //  isLoop = true;
                break;
            case AnimStatus.Idle:
                needFrames[0] = idleFrames[0];
                needFrames[1] = idleFrames[2];
                needFrames[2] = idleFrames[1];
              //  isLoop = true;
                break;

        }
        gameObject.GetComponent<Image>().sprite = needFrames[1];
        currentIndex = 0;
        Play();

    }

    public void SetFaceTo(Vector2 startPos,Vector2 targetPos)
    {
        if (System.Math.Abs(startPos.y - targetPos.y) >= System.Math.Abs(startPos.x - targetPos.x))
        {
            if (startPos.y >= targetPos.y)
            {
                SetAnim(AnimStatus.WalkDown);
            }
            else if (startPos.y < targetPos.y)
            {
                SetAnim(AnimStatus.WalkUp);
            }
        }
        else
        {
            if (startPos.x >= targetPos.x)
            {
                SetAnim(AnimStatus.WalkLeft);
            }
            else if (startPos.x < targetPos.x)
            {
                SetAnim(AnimStatus.WalkRight);
            }
        }
    }

    public void StartMove()
    {
        // nowPointIndex = 1;
        rt.anchoredPosition= new Vector2(DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].X, DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].Y);
    
        nowPointIndex++;
        gc.travellerDic[travellerID].x = rt.anchoredPosition.x;
        gc.travellerDic[travellerID].y = rt.anchoredPosition.y;
        gc.travellerDic[travellerID].nowPointIndex = nowPointIndex;
        targetPos = new Vector2(DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].X, DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].Y);
        SetFaceTo(rt.anchoredPosition, targetPos);
        Play();
    }

    public void ContinueMove()
    {
        rt.anchoredPosition = new Vector2(gc.travellerDic[travellerID].x,  gc.travellerDic[travellerID].y);
        targetPos = new Vector2(DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].X, DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].Y);
        SetFaceTo(rt.anchoredPosition, targetPos);
        Play();
    }

    public void Move()
    {
        if (Vector2.Distance(rt.anchoredPosition, targetPos) > 1f)
        {
            if (rt.anchoredPosition.y >= targetPos.y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
             if (rt.anchoredPosition.y < targetPos.y)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
             if (rt.anchoredPosition.x >= targetPos.x)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
             if (rt.anchoredPosition.x < targetPos.x)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            gc.travellerDic[travellerID].x = rt.anchoredPosition.x;
            gc.travellerDic[travellerID].y = rt.anchoredPosition.y;
        }
        else
        {
            rt.anchoredPosition = targetPos;
            if (nowPointIndex == pathPointList.Count-1)
            {
                Stop();
                gameObject.GetComponent<Image>().sprite = walk_DownFrames[2];
                //MessagePanel.Instance.AddMessage("到达了");

                gameObject.transform.localScale = Vector2.zero;
                AreaMapPanel.Instance.travellerGoPool.Add(gameObject);
                gc.travellerDic.Remove(travellerID);
            }
            else
            {
                nowPointIndex++;
                gc.travellerDic[travellerID].nowPointIndex = nowPointIndex;
                //  Debug.Log("nowPointIndex=" + nowPointIndex);
                targetPos = new Vector2(DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].X, DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].Y);
              //  Debug.Log("nowPointIndex=" + nowPointIndex+ " targetPos="+ targetPos);
                SetFaceTo(rt.anchoredPosition, targetPos);
                gc.travellerDic[travellerID].x = rt.anchoredPosition.x;
                gc.travellerDic[travellerID].y = rt.anchoredPosition.y;
            }    
        }
     
    }
    public void SetTargetPos()
    {
        targetPos = new Vector2(DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].X, DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].Y);
    }


}
