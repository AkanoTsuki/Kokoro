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

    bool isPlay = false;
    bool isLoop = false;
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
        Debug.Log(charaName + " Stop()");
        isPlay = false;
    }

    public void SetCharaFrames(string name)
    {
        charaName = name;

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

        //Debug.Log(walk_LeftFrames[0].texture.width + " " + walk_LeftFrames[0].texture.height);
        //Debug.Log(attackFrames[0].texture.width + " " + attackFrames[0].texture.height);
    }

    public void SetCharaFramesSimple(string name)//怪物用
    {
        charaName = name;

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

    public void SetAnim(AnimStatus animStatus)
    {

        switch (animStatus)
        {
            case AnimStatus.WalkLeft:
                needFrames[0] = walk_LeftFrames[0];
                needFrames[1] = walk_LeftFrames[2];
                needFrames[2] = walk_LeftFrames[1];
              //  gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(needFrames[0].texture.width / (3*2f), needFrames[0].texture.height/2f);
                isLoop = true;
                break;
            case AnimStatus.WalkRight:
                needFrames[0] = walk_RightFrames[0];
                needFrames[1] = walk_RightFrames[2];
                needFrames[2] = walk_RightFrames[1];
               // gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(needFrames[0].texture.width / (3 * 2f), needFrames[0].texture.height / 2f);
                isLoop = true;
                break;

            case AnimStatus.Idle:
                needFrames[0] = idleFrames[0];
                needFrames[1] = idleFrames[2];
                needFrames[2] = idleFrames[1];
              //  gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(72, 72);
                isLoop = true;
                break;
            case AnimStatus.Attack:
                needFrames[0] = attackFrames[0];
                needFrames[1] = attackFrames[2];
                needFrames[2] = attackFrames[1];
                //   gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(72, 72);
                if (GetComponent<RectTransform>().anchoredPosition.x < 0)
                {
                    transform.DOShakePosition(1, Vector2.right * 10, 1, 0, true);
                }
                else
                {
                    transform.DOShakePosition(1, Vector2.left * 10, 1, 0, true);
                }

                isLoop = false;
                break;
            case AnimStatus.Bow:
                needFrames[0] = bowFrames[0];
                needFrames[1] = bowFrames[2];
                needFrames[2] = bowFrames[1];
             //   gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(72, 72);
                isLoop = false;
                break;
            case AnimStatus.Magic:
                needFrames[0] = magicFrames[0];
                needFrames[1] = magicFrames[2];
                needFrames[2] = magicFrames[1];
              //  gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(72, 72);
                isLoop = false;
                break;
            case AnimStatus.Hit:
                needFrames[0] = hitFrames[0];
                needFrames[1] = hitFrames[2];
                needFrames[2] = hitFrames[1];
                //  gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(72, 72);
                transform.DOShakePosition(0.5f, 5, 5, 50, true);
                transform.GetComponent<Image>().DOColor(Color.red, 0.3f);
                transform.GetComponent<Image>().DOColor(Color.white, 0.3f).SetDelay(0.3f);
                isLoop = false;
                break;
            case AnimStatus.Death:
                needFrames[0] = deathFrames[0];
                needFrames[1] = deathFrames[2];
                needFrames[2] = deathFrames[1];
               // gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(72, 72);
                isLoop = true;
                break;


        }
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(needFrames[0].texture.width / (3 * 2f), needFrames[0].texture.height / 2f);

        currentIndex = 0;
       // Debug.Log("开始行走动画第一帧");
        //gameObject.GetComponent<Image>().sprite = needFrames[currentIndex];
     
        //currentIndex++;
        Play();
    }

    


}
