using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class AnimatiorControlByTraveller : MonoBehaviour
{
    private float speed = 10.0f;
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
    bool isLoop = false;
    public string charaName = "chara1_1";

    public int travellerID=-1;

    public List<int> pathPointList = new List<int>();
    public int nowPointIndex = 0;
    // Start is called before the first frame update

    RectTransform rt;
    Vector2 targetPos;
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //Debug.Log("SetAnim animStatus=" + animStatus + " charaName=" + charaName);

        switch (animStatus)
        {
            case AnimStatus.WalkUp:
                needFrames[0] = walk_UpFrames[0];
                needFrames[1] = walk_UpFrames[2];
                needFrames[2] = walk_UpFrames[1];
                isLoop = true;
                break;
            case AnimStatus.WalkLeft:
                needFrames[0] = walk_LeftFrames[0];
                needFrames[1] = walk_LeftFrames[2];
                needFrames[2] = walk_LeftFrames[1];
                isLoop = true;
                break;
            case AnimStatus.WalkRight:
                needFrames[0] = walk_RightFrames[0];
                needFrames[1] = walk_RightFrames[2];
                needFrames[2] = walk_RightFrames[1];
                isLoop = true;
                break;
            case AnimStatus.WalkDown:
                needFrames[0] = walk_DownFrames[0];
                needFrames[1] = walk_DownFrames[2];
                needFrames[2] = walk_DownFrames[1];
                isLoop = true;
                break;
            case AnimStatus.Idle:
                needFrames[0] = idleFrames[0];
                needFrames[1] = idleFrames[2];
                needFrames[2] = idleFrames[1];
                isLoop = true;
                break;

        }
        gameObject.GetComponent<Image>().sprite = needFrames[1];
        currentIndex = 0;
        Play();

    }

    public void SetFaceTo(Vector2 startPos,Vector2 targetPos)
    {
        if (startPos.y >= targetPos.y)
        {
            SetAnim(AnimStatus.WalkDown);
        }
        else if (startPos.y < targetPos.y)
        {
            SetAnim(AnimStatus.WalkUp);
        }
        else if (startPos.x >= targetPos.x)
        {
            SetAnim(AnimStatus.WalkLeft);
        }
        else if (startPos.x < targetPos.x)
        {
            SetAnim(AnimStatus.WalkRight);
        }

    }

    public void Move()
    {
        if (Vector2.Distance(rt.anchoredPosition, targetPos) > 0.1f)
        {
            if (rt.anchoredPosition.y >= targetPos.y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else if (rt.anchoredPosition.y < targetPos.y)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else if (rt.anchoredPosition.x >= targetPos.x)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if (rt.anchoredPosition.x < targetPos.x)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
        else
        {
            rt.anchoredPosition = targetPos;
            if (nowPointIndex == pathPointList.Count-1)
            {
                //到达
            }
            else
            {
                nowPointIndex++;
            }
     
        }

    }
    public void SetTargetPos()
    {
        targetPos = new Vector2(DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].X, DataManager.mAreaPathPointDict[pathPointList[nowPointIndex]].Y);
    }


}
