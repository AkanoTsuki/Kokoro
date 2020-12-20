using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum AnimStatus
{
    Idle,
    WalkLeft,
    WalkRight,
    Attack,
    Bow,
    Magic,
    Hit,
    Dead
}
public class AnimatiorControl : MonoBehaviour
{
    private float fps = 5.0f;
    private float time = 0;
    private int currentIndex = 0;
    Sprite[] actionFrames;
    Sprite[] walkFrames;
    Sprite[] needFrames = new Sprite[3];

    bool isPlay = false;
    bool isLoop = false;
    public string charaName = "chara1_01";

    void Start()
    {
       // nowFrame = gameObject.GetComponent<Image>().sprite;
        actionFrames = Resources.LoadAll<Sprite>("Image/RolePic/"+ charaName + "/action");
        walkFrames = Resources.LoadAll<Sprite>("Image/RolePic/"+ charaName + "/walk");
        Debug.Log("Image/RolePic/" + charaName + "/walk");
        Debug.Log(actionFrames.Length);
        SetAnim(AnimStatus.Idle);
        gameObject.GetComponent<Image>().sprite = needFrames[currentIndex];

        Play();
        Debug.Log(isLoop);
    }


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
                if (currentIndex > 2)
                {
                    if (isLoop)
                    {
                        currentIndex = 0;
                    }
                    else
                    {

                        SetAnim(AnimStatus.Idle);
                        currentIndex = 0;
                    }

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetAnim(AnimStatus.WalkRight);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
   
            SetAnim(AnimStatus.Attack);
        }
        //  PlayAnim("chara1_01", AnimStatus.Idel);
    }

    public void Play()
    {
        isPlay = true;
    }
    public void Stop()
    {
        isPlay = false;
    }
    void SetAnim(AnimStatus animStatus)
    {
       
        byte Index = 0;
        switch (animStatus)
        {
            case AnimStatus.WalkLeft: Index = 1; isLoop = true; break;
            case AnimStatus.WalkRight: Index = 2; isLoop = true; break;
            case AnimStatus.Idle: Index = 2; isLoop = true; break;
            case AnimStatus.Attack:  Index = 1; isLoop = false; break;
            case AnimStatus.Bow:  Index = 7; isLoop = true; break;
            case AnimStatus.Magic:  Index = 13; isLoop = true; break;
            case AnimStatus.Hit:  Index = 14; isLoop = true; break;
            case AnimStatus.Dead:  Index = 15; isLoop = true; break;
        }
        switch (animStatus)
        {
            case AnimStatus.WalkLeft:
            case AnimStatus.WalkRight:
                needFrames[0] = walkFrames[Index * 3 + 0];
                needFrames[1] = walkFrames[Index * 3 + 2];
                needFrames[2] = walkFrames[Index * 3 + 1];
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(39, 54);

                break;
            case AnimStatus.Idle: 
            case AnimStatus.Attack:
            case AnimStatus.Bow:
            case AnimStatus.Magic:
            case AnimStatus.Hit: 
            case AnimStatus.Dead:
                needFrames[0] = actionFrames[Index * 3 + 0];
                needFrames[1] = actionFrames[Index * 3 + 2];
                needFrames[2] = actionFrames[Index * 3 + 1];
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(72, 72);
                break;
        }


        currentIndex = 0;
       
        // Play(needFrames,true);
        Play();
    }

    


}
