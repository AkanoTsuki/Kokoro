using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
//除了控制动画，顺带控制鼠标焦点事件
public class AnimatiorControlByNPC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float fps = 10.0f;
    private float time = 0;
    private int currentIndex = 0;

    private float timeCX = 0;

    Sprite[] idleFrames;
    Sprite[] walk_DownFrames;
    Sprite[] walk_LeftFrames;
    Sprite[] walk_RightFrames;
    Sprite[] walk_UpFrames;

    Sprite[] needFrames = new Sprite[3];

    public bool isShow = false;
    bool isPlay = false;
  //  bool isLoop = false;
    bool isNeedStop = false;
    public string charaName = "chara1_1";
    public int customerID = -1;


    // Update is called once per frame
    void Update()
    {
        if (isPlay&& isShow)
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
            if (isNeedStop)
            {
                timeCX -= Time.deltaTime;
                if (timeCX <= 0)
                {
                    isNeedStop = false;
                    isPlay = false;
                }
            }
           
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
        //Debug.Log("SetAnim animStatus=" + animStatus + " charaName=" + charaName);

        switch (animStatus)
        {
           case AnimStatus.WalkUp:
                needFrames[0] = walk_UpFrames[0];
                needFrames[1] = walk_UpFrames[2];
                needFrames[2] = walk_UpFrames[1];
               // isLoop = true;
                break;
            case AnimStatus.WalkLeft:
                needFrames[0] = walk_LeftFrames[0];
                needFrames[1] = walk_LeftFrames[2];
                needFrames[2] = walk_LeftFrames[1];
               // isLoop = true;
                break;
            case AnimStatus.WalkRight:
                needFrames[0] = walk_RightFrames[0];
                needFrames[1] = walk_RightFrames[2];
                needFrames[2] = walk_RightFrames[1];
               // isLoop = true;
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
               // isLoop = true;
                break;

        }
        gameObject.GetComponent<Image>().sprite = needFrames[1];
        currentIndex = 0;
        Play();

    }

    public void SetAnim(AnimStatus animStatus,float time)
    {
        //Debug.Log("SetAnim animStatus=" + animStatus + " charaName=" + charaName);

        switch (animStatus)
        {
            case AnimStatus.WalkUp:
                needFrames[0] = walk_UpFrames[0];
                needFrames[1] = walk_UpFrames[2];
                needFrames[2] = walk_UpFrames[1];
           
                break;
            case AnimStatus.WalkLeft:
                needFrames[0] = walk_LeftFrames[0];
                needFrames[1] = walk_LeftFrames[2];
                needFrames[2] = walk_LeftFrames[1];
       
                break;
            case AnimStatus.WalkRight:
                needFrames[0] = walk_RightFrames[0];
                needFrames[1] = walk_RightFrames[2];
                needFrames[2] = walk_RightFrames[1];
         
                break;
            case AnimStatus.WalkDown:
                needFrames[0] = walk_DownFrames[0];
                needFrames[1] = walk_DownFrames[2];
                needFrames[2] = walk_DownFrames[1];
             
                break;
            case AnimStatus.Idle:
                needFrames[0] = idleFrames[0];
                needFrames[1] = idleFrames[2];
                needFrames[2] = idleFrames[1];
           
                break;

        }
        gameObject.GetComponent<Image>().sprite = needFrames[1];
        currentIndex = 0;
        isNeedStop = true;
        timeCX = time;
        Play();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DistrictMapPanel.Instance.ShowCustomerInfo(customerID, GetComponent<RectTransform>().anchoredPosition);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DistrictMapPanel.Instance.HideCustomerInfo();
    }
}
