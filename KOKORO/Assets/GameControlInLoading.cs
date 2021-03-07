using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControlInLoading : MonoBehaviour
{ 
    //进度条
    public Slider progressSlider;

    //进度条进度显示文字  
    public Text ProgressSliderText;

    public Transform HandleAnim;

    //当前加载进度  
    private int nowProcess;

    //异步资源
    private AsyncOperation async;

    //异步加载的场景名称
    static public string sceneName = "";

   

    void Start()
    {

        //屏幕常亮
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //调用加载函数
        //sceneName = "1.1";
        ShowProgressBar();

        HandleAnim.GetComponent<LoopEffect>().PlayRoleWalkRight("chara"+Random.Range(1,8)+"_" + Random.Range(1, 9));
    }

    void Update()
    {

        if (async == null) return;

        int toProcess;

        // async.progress 你正在读取的场景的进度值  0---0.9      
        // 如果当前的进度小于0.9，说明它还没有加载完成，就说明进度条还需要移动      
        // 如果，场景的数据加载完毕，async.progress 的值就会等于0.9    
        if (async.progress < 0.9f)
        {
            toProcess = (int)async.progress * 100;
        }
        else
        {
            toProcess = 100;
        }

        // 如果滑动条的当前进度，小于，当前加载场景的方法返回的进度     
        if (nowProcess < toProcess)
        {
            nowProcess++;
        }

        progressSlider.value = nowProcess / 100f;
        //设置progressText进度显示  
        ProgressSliderText.text ="加载中...("+ progressSlider.value * 100 + "%)";
        // 设置为true的时候，如果场景数据加载完毕，就可以自动跳转场景     
        if (nowProcess == 100)
        {
            HandleAnim.GetComponent<LoopEffect>().Stop();
            async.allowSceneActivation = true;
        }

    }
    //异步加载scene  

    IEnumerator LoadScene()
    {

        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        yield return async;
    }

    //外部调用的加载的方法  

    public void ShowProgressBar()
    {

        progressSlider.value = 0f;

        //设置progressText进度显示  
        ProgressSliderText.text = "0%";

        //场景名不为空时 , 开启协程异步加载资源
        if (sceneName != "") StartCoroutine(LoadScene());

    }
}
