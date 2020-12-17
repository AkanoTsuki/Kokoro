using UnityEngine;
using System.Collections;
using DG.Tweening;

//所有面板的公共基类
public class BasePanel : MonoBehaviour
{
   // protected CanvasGroup canvasGroup;//对该页面CanvasGroup组件的引用

    public bool isShow = false;

    CanvasGroup canvasGroup;
    void Start()
    {

      // canvasGroup = this.GetComponent<CanvasGroup>();
     //  Debug.Log("this=" + this + " canvasGroup=" + canvasGroup);
    }

    public virtual void OnInit()
    {
        SetLocalPosition(0, 5000);
    }

    /// <summary>
    /// 页面进入显示，可交互
    /// </summary>
    public virtual void OnShow() { }



    /// <summary>
    /// 页面暂停（弹出了其他页面），不可交互
    /// </summary>
    public virtual void OnPause() { }

    /// <summary>
    /// 页面继续显示（其他页面关闭），可交互
    /// </summary>
    public virtual void OnResume() { }

    /// <summary>
    /// 本页面被关闭（移除），不再显示在界面上
    /// </summary>
    public virtual void OnHide() { }

    public virtual void SetLocalPosition(int x, int y) 
    { 
        this.transform.localPosition = new Vector3(x, y, 0);
      
    }
    public virtual void SetAnchoredPosition(int x, int y)
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

    }
    public virtual void SetSibling(int index)
    {
        this.transform.SetSiblingIndex(index);
    }
    public virtual int GetSibling()
    {
        return this.transform.GetSiblingIndex();
    }

    /// <summary>
    /// 页面逐渐显示 showInCenter：是否固定显示在中间
    /// </summary>
    public void ShowByGradually(bool showInCenter)
    {
        if (showInCenter)
            this.transform.localPosition = new Vector3(0, 0, 0);


        canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = true;//使该页面可交互
        canvasGroup.DOFade(1, 0.4f);//使该页面渐渐显示,1是Alpha值
    }

    /// <summary>
    /// 页面立即显示 showInCenter：是否固定显示在中间
    /// </summary>
    public void ShowByImmediately(bool showInCenter)
    {
        if (showInCenter)
            this.transform.localPosition = new Vector3(0, 0, 0);

        canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;//使该页面可交互
    }

    /// <summary>
    /// 页面逐渐隐藏
    /// </summary>
    public void HideByGradually()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;//使该页面不可交互
        canvasGroup.DOFade(0, 0.4f);//使该页面渐渐隐藏，0是Alpha值
    }

    /// <summary>
    /// 页面立即隐藏
    /// </summary>
    public void HideByImmediately()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;//使该页面不可交互
        canvasGroup.alpha = 0;
    }

}