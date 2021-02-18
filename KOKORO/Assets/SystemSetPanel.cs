using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SystemSetPanel : BasePanel
{
    public static SystemSetPanel Instance;
    GameControlInPlay gci;
    GameControl gc;
    public Slider volumeMusicSlider;
    public Slider volumeSoundSlider;
    public Button closeBtn;

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



    public override void OnShow()
    {
        volumeMusicSlider.value = gc.volumeMusic;
        volumeSoundSlider.value = gc.volumeSound;
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetAsLastSibling();

        isShow = true;

        gci.tempTimeSpeed = gc.timeFlowSpeed;
        gci.TimePause();
    }



    public override void OnHide()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isShow = false;

        if (gci.tempTimeSpeed == 1)
        {
            gci.TimePlay();
        }
        else if (gci.tempTimeSpeed == 2)
        {
            gci.TimeFast();
        }
    }
}
