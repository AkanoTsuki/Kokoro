using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SystemSetPanel : BasePanel
{
    public static SystemSetPanel Instance;

    GameControlInPlay gci;
    GameControl gc;

    #region 【UI控件】
    public Slider volumeMusicSlider;
    public Slider volumeSoundSlider;
    public Button closeBtn;
    #endregion

    void Awake()
    {
        Instance = this;
        gc = GameObject.Find("GameManager").GetComponent<GameControl>();
        gci = GameObject.Find("GameManagerInScene").GetComponent<GameControlInPlay>();
    }

    void Start()
    {
        volumeMusicSlider.onValueChanged.AddListener((value) =>
        {
            gc.SetVolumeMusic((byte)value);
        });
        volumeSoundSlider.onValueChanged.AddListener((value) =>
        {
            gc.SetVolumeSound((byte)value);
        });
        closeBtn.onClick.AddListener(delegate () { OnHide(); });
    }

    //主面板显示
    public override void OnShow()
    {
        volumeMusicSlider.value = gc.volumeMusic;
        volumeSoundSlider.value = gc.volumeSound;
    
        gci.tempTimeSpeed = gc.timeFlowSpeed;
        gci.TimePause();

        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetAsLastSibling();
        isShow = true;
    }

    //主面板关闭
    public override void OnHide()
    {
        if (gci.tempTimeSpeed == 1)
        {
            gci.TimePlay();
        }
        else if (gci.tempTimeSpeed == 2)
        {
            gci.TimeFast();
        }

        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isShow = false;
    }
}
