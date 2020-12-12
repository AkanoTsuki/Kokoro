using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameControlInStartMenu : MonoBehaviour
{
    GameControl gc;
    Button toNewGameButton;
    Button toContinueButton;
    GameObject ConfirmBoxGo;
    int flag = 0; //0是否检测到存档

    void Awake()
    {
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();

        toNewGameButton = GameObject.Find("Canvas/Button_New").GetComponent<Button>();
        toContinueButton = GameObject.Find("Canvas/Button_Continue").GetComponent<Button>();
        ConfirmBoxGo = GameObject.Find("Canvas/ConfirmBox");

    }
    void Start()
    {  
        
        SetButtonState();
    }

 

    public void ToNewGame()
    {
        //SoundManager._instance.PlayingSound("ButtonClick1");
        if (!gc.CheckSaveFile())//没检测到存档
        { SceneManager.LoadScene("A2_NewGame"); }
        else//检测到存档
        {
            ConfirmBoxGo.transform.localPosition = Vector3.zero;
            ConfirmBoxGo.transform.GetChild(0).GetComponent<Text>().text = "程序检测到已存在游戏记录，如开始新游戏将会覆盖已有数据，确定要重新开始游戏吗？";
            toNewGameButton.interactable = false;
            toContinueButton.interactable = false;
            flag = 0;
        }
    }

    public void ToContinue()
    {
        gc.Load();
        SceneManager.LoadScene("A3_Play");
        

    }

    void SetButtonState()
    {
        if (gc.CheckSaveFile())//检测到存档文件
        {
            toNewGameButton.interactable = true;
            toContinueButton.interactable = true;
        }
        else
        {
            toNewGameButton.interactable = true;
            toContinueButton.interactable = false;
            
        }
    }


    public void Confirm()
    {
        gc.Delete();
        SceneManager.LoadScene("A2_NewGame");
    }
    public void Cancel()
    {
        SetButtonState();
        ConfirmBoxGo.transform.localPosition = new Vector3(0, 5000f, 0);
    }


}
