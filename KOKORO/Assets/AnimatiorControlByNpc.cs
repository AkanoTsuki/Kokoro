using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimatiorControlByNpc : MonoBehaviour
{
    private float speed = 30.0f;
    private float fps = 8.0f;
    private float time = 0;
    private int currentIndex = 0;
    Sprite[] setFrames;
    Sprite[] needFrames = new Sprite[3];
    bool isPlay = false;
    public string charaName = "chara1_1";
    //bool isLoop = false;

    short needTimes = 0;//次数-1为loop

    List<string> randomAnimNameList=new List<string>();
    List<byte> randomAnimRateList=new List<byte>();
    List<short> randomAnimTimesList=new List<short>();

    public bool isHero = false;

     bool canRandom = true;


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

                    if (needTimes != -1)
                    {
                        needTimes--;
                        if (needTimes == 0)
                        {
                            if (isHero)
                            {
                                SetAnim("Pic", -1);
                            }
                            else
                            {
                                SetAnim("Idle", -1);
                            }
                        }
                    }
                    else
                    {
                        if (canRandom&&Random.Range(0, 100) < 10)
                        {
                            GetRandomAnim();
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

    public void SetCharaFrames(string name,int width,int height,bool canRandom)
    {
        charaName = name;
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        randomAnimNameList.Clear();
        randomAnimRateList.Clear();
        randomAnimTimesList.Clear();
        this.canRandom = canRandom;
        //GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
    }

    public void SetRandomAnim(List<string> name, List<byte> rate, List<short> times)
    {
        randomAnimNameList = name;
        randomAnimRateList = rate;
        randomAnimTimesList = times;
    }
    void GetRandomAnim()
    {
        int ran = Random.Range(0, 100);
        int count = 0;
        for (int i = 0; i < randomAnimNameList.Count; i++)
        {
            count += randomAnimRateList[i];
            if (ran < count)
            {
                SetAnim(randomAnimNameList[i], randomAnimTimesList[i]);
                return;
            }
        }

        if (isHero)
        {
            SetAnim("Pic", -1);
        }
        else
        {
            SetAnim("Idle", -1);
        }
    }


    public void SetAnim(string animStatus, short times)
    {
        //Debug.Log("SetAnim() animStatus=" + animStatus);
        if (animStatus != "Idle")
        {
            setFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/" + animStatus);
            needFrames = new Sprite[setFrames.Length];
        }
        else//默认
        {
            setFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/SayYes");
            //Debug.Log("SetAnim() setFrames.Length=" + setFrames.Length);
            if (setFrames.Length == 0)//用于动物
            {
                int ran = Random.Range(0, 4);
                switch (ran)
                {
                    case 0: setFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Up"); break;
                    case 1: setFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Left"); break;
                    case 2: setFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Right"); break;
                    case 3: setFrames = Resources.LoadAll<Sprite>("Image/RolePic/" + charaName + "/Walk_Down"); break;
                }
                //Debug.Log("SetAnim() setFrames.Length=" + setFrames.Length);
            }
           
            needFrames = new Sprite[1];
            //Debug.Log("SetAnim() needFrames.Length=" + needFrames.Length);
        }


        switch (animStatus)
        {
            case "Idle":
                fps = 1.0f;
                needFrames[0] = setFrames[1];
                needTimes = times;
                gameObject.GetComponent<Image>().sprite = needFrames[0];
                currentIndex = 0;
                Play();
                break;
            case "Beat1":
            case "Beat2":
                fps = 8.0f;
                for (int i = 0; i < needFrames.Length; i++)
                {
                    needFrames[i] = setFrames[i];
                }
                needTimes = times;
                gameObject.GetComponent<Image>().sprite = needFrames[1];
                currentIndex = 0;
                Play();
                break;
        
            case "Pic":
            case "Walk_Up":
            case "Walk_Left":
            case "Walk_Right":
            case "Walk_Down":
                fps = 1.0f;
                needFrames[0] = setFrames[0];
                //isLoop = true;
                gameObject.GetComponent<Image>().sprite = needFrames[0];
                //Debug.Log("gameObject.GetComponent<Image>().sprite=" + gameObject.GetComponent<Image>().sprite);
                currentIndex = 0;
                Play();
                break;
          
            default:
                //Debug.Log("charaName=" + charaName + " animStatus=" + animStatus + " needFrames.Length=" + needFrames.Length + " setFrames.Length=" + setFrames.Length);

                fps = 8.0f;
                for (int i = 0; i < needFrames.Length; i++)
                {
                    needFrames[i] = setFrames[i];
                }
                needTimes = times;
                gameObject.GetComponent<Image>().sprite = needFrames[1];
                currentIndex = 0;
                Play();
                break;
        }
      



    }
}
