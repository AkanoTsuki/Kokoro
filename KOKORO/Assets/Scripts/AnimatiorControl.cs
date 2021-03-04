using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimatiorControl : MonoBehaviour
{
    private float fps = 10.0f;
    private float time = 0;
    private int currentIndex = 0;

    Sprite[] attackFrames;
    Sprite[] bowFrames;
    Sprite[] deathFrames;
    Sprite[] hitFrames;
    Sprite[] idleFrames;
    Sprite[] magicFrames;
    Sprite[] walk_DownFrames;
    Sprite[] walk_LeftFrames;
    Sprite[] walk_RightFrames;
    Sprite[] walk_UpFrames;



    Sprite[] needFrames = new Sprite[3];
    Sprite[] setFrames;

    bool isPlay = false;
    bool isLoop = false;
    bool isFighting = false;
    short times = -1;
    public string charaName = "chara1_1";



    void Start()
    {
        //SetAnim(AnimStatus.Idle);
        //gameObject.GetComponent<Image>().sprite = needFrames[currentIndex];

        //Play();
       
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
                if (currentIndex > needFrames.Length-1)
                {
                    currentIndex = 0;
                    if (times > 0)
                    {
                        times--;
                    }
                    else
                    {
                        if (!isLoop)
                        {
                            if (isFighting)
                            {
                                SetAnim(AnimStatus.Idle);
                            }
                            else
                            {
                                SetAnim(AnimStatus.Front);
                            }
                        }
                    }
                    
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
        attackFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Attack");
        bowFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Bow");
        deathFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Death");
        hitFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Hit");
        idleFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Idle");
        magicFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Magic");
        walk_DownFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Down");
        walk_LeftFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        walk_RightFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Right");
        walk_UpFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Up");

        //Debug.Log(" walk_RightFrames.Length=" + walk_RightFrames.Length);
    }

    public void SetCharaFramesSimple(string name)//怪物用
    {
        charaName = name;
        needFrames = new Sprite[3];
        attackFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        bowFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        deathFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        hitFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        idleFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        magicFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        walk_DownFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        walk_LeftFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        walk_RightFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
        walk_UpFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left");
    }

    public void SetChestFrames(byte type)
    {
        needFrames = new Sprite[4];
        idleFrames = Resources.LoadAll<Sprite>("Image/Chest/chests" + type );
    }
    public void SetSpringFrames(string type)
    {
        needFrames = new Sprite[1];
      
        idleFrames = Resources.LoadAll<Sprite>("Image/Other/spring" + type);

    }

    public void SetAnim(AnimStatus animStatus)
    {
        //Debug.Log("SetAnim animStatus=" + animStatus + " charaName=" + charaName);

        switch (animStatus)
        {
            case AnimStatus.WalkLeft:
                needFrames[0] = walk_LeftFrames[0];
                needFrames[1] = walk_LeftFrames[2];
                needFrames[2] = walk_LeftFrames[1];

              
                isLoop = true;
                isFighting = false;
                times = -1;
                break;
            case AnimStatus.WalkRight:
                needFrames[0] = walk_RightFrames[0];
                needFrames[1] = walk_RightFrames[2];
                needFrames[2] = walk_RightFrames[1];
                gameObject.GetComponent<Image>().sprite = needFrames[0];
                isLoop = true;
                isFighting = false;
                times = -1;
                break;
            case AnimStatus.Idle:
                needFrames[0] = idleFrames[0];
                needFrames[1] = idleFrames[2];
                needFrames[2] = idleFrames[1];
                isLoop = true;
                isFighting = true;
                times = -1;
                break;
            case AnimStatus.Attack:
                needFrames[0] = attackFrames[0];
                needFrames[1] = attackFrames[2];
                needFrames[2] = attackFrames[1];
                if (GetComponent<RectTransform>().anchoredPosition.x < 0)
                {
                    transform.DOShakePosition(1, Vector2.right * 10, 1, 0, true);
                }
                else
                {
                    transform.DOShakePosition(1, Vector2.left * 10, 1, 0, true);
                }

                isLoop = false;
                isFighting = true;
                times = -1;
                break;
            case AnimStatus.AttackLoop:
                needFrames[0] = attackFrames[0];
                needFrames[1] = attackFrames[2];
                needFrames[2] = attackFrames[1];
                isLoop = true;
                isFighting = true;
                times = -1;
                break;
            case AnimStatus.Bow:
                needFrames[0] = bowFrames[0];
                needFrames[1] = bowFrames[2];
                needFrames[2] = bowFrames[1];
                isLoop = false;
                isFighting = true;
                times = -1;
                break;
            case AnimStatus.Magic:
                needFrames[0] = magicFrames[0];
                needFrames[1] = magicFrames[2];
                needFrames[2] = magicFrames[1];
                isLoop = false;
                isFighting = true;
                times = -1;
                break;
            case AnimStatus.Hit:
                needFrames[0] = hitFrames[0];
                needFrames[1] = hitFrames[2];
                needFrames[2] = hitFrames[1];
                transform.DOShakePosition(0.5f, 5, 5, 50, true);
                transform.GetComponent<Image>().DOColor(Color.red, 0.3f);
                transform.GetComponent<Image>().DOColor(Color.white, 0.3f).SetDelay(0.3f);
                isLoop = false;
                isFighting = true;
                times = -1;
                break;
            case AnimStatus.Death:
                needFrames[0] = deathFrames[0];
                needFrames[1] = deathFrames[2];
                needFrames[2] = deathFrames[1];
              
                gameObject.GetComponent<Image>().sprite = needFrames[0];
                isLoop = true;
                isFighting = true;
                times = -1;
                break;
            case AnimStatus.Front:
                needFrames[0] = walk_DownFrames[1];
                gameObject.GetComponent<Image>().sprite = needFrames[0];
                isLoop = true;
                isFighting = false;
                times = -1;
                break;
            case AnimStatus.ChestOpen:
                needFrames[0] = idleFrames[0];
                needFrames[1] = idleFrames[1];
                needFrames[2] = idleFrames[2];
                needFrames[3] = idleFrames[3];
                gameObject.GetComponent<Image>().sprite = needFrames[0];
                isLoop = true;
                isFighting = false;
                times = -1;
                break;
            case AnimStatus.SpringAppear:
                needFrames[0] = idleFrames[0];
                gameObject.GetComponent<Image>().sprite = needFrames[0];
                gameObject.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
                gameObject.GetComponent<Image>().DOFade(1f, 0.5f);
                gameObject.GetComponent<Image>().DOFade(0f, 0.5f).SetDelay(2f);
                isLoop = true;
                isFighting = false;
                times = -1;
                break;
            case AnimStatus.Laugh:
            case AnimStatus.Surprise:
            case AnimStatus.SayYes:
            case AnimStatus.SayNo:
                setFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/"+ animStatus.ToString());
                if (setFrames.Length > 0)
                {
                    needFrames = new Sprite[setFrames.Length];
                    needFrames[0] = setFrames[0];
                    needFrames[1] = setFrames[1];
                    needFrames[2] = setFrames[2];
                    gameObject.GetComponent<Image>().sprite = needFrames[0];
                    isLoop = false;
                    isFighting = false;
                    times =(short) Random.Range(2,4);
                    fps =Random.Range( 4f,4.5f);
                }
                else
                {
                    return;
                }
                break;

        }
        // Debug.Log(needFrames[0].texture.width + " " + needFrames[0].texture.height);
        if (animStatus == AnimStatus.ChestOpen )
        {
           // Debug.Log("needFrames[0].texture.width=" + needFrames[0].texture.width + " needFrames[0].texture.height=" + needFrames[0].texture.height);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(needFrames[0].texture.width , needFrames[0].texture.height / 4f);
          //  Debug.Log("gameObject.GetComponent<RectTransform>().sizeDelta=" + gameObject.GetComponent<RectTransform>().sizeDelta);
            fps = 2.5f;
        }
        else if (animStatus == AnimStatus.SpringAppear)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(needFrames[0].texture.width , needFrames[0].texture.height );
        }
        else
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(needFrames[0].texture.width / (3 * 2f), needFrames[0].texture.height / 2f);
            fps = 10.0f;
        }

        if (animStatus != AnimStatus.Front&& animStatus != AnimStatus.Death && animStatus != AnimStatus.SpringAppear)
        {
            currentIndex = 0;
            Play();
        }
        else
        {
            Stop();
        }

    }


    public void SetAnim(string animStatus, short times)
    {
        
    }

}
