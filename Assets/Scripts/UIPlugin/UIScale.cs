using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class UIScale : MonoBehaviour
{
    //持续的时间
    public float Duration = 0.4f;
    public Vector3 Target;

    /// <summary>
    /// 将scale设定为目标值
    /// </summary>
    /// <param name="target"></param>
    /// <param name="duration"></param>
    public void Scale(Vector3 target, float duration)
    {
        transform.DOScale(target, duration);
    }

    /// <summary>
    /// 将scale设定为已经设定过的目标值
    /// </summary>
    public void Scale()
    {
        transform.DOScale(Target, Duration);
    }

    /// <summary>
    /// UI缩放激活或者关闭
    /// </summary>
    /// <param name="value"></param>
    public void SetActive(bool value)
    {
        Tweener scale;
        if (value)
        {
            //1.先将原panel缩小
            RectTransform rectTrans = this.GetComponent<RectTransform>();
            rectTrans.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            //2.激活
            this.gameObject.SetActive(true);
            //3.线性放大
            scale = rectTrans.DOScale(new Vector3(1f, 1f, 1f), Duration);
            scale.SetEase(Ease.Linear);
        }
        else
        {
            //1.将原panel线性缩小
            RectTransform rectTrans = this.GetComponent<RectTransform>();
            scale = rectTrans.DOScale(new Vector3(0.4f, 0.4f, 0.4f), Duration/2f);
            scale.SetEase(Ease.Linear);
            //2.结束的关闭
            scale.onKill = SetActiveFalse;
        }
    }

    public void ScaleIn()
    {
        //1.先将原panel缩小
        RectTransform rectTrans = this.GetComponent<RectTransform>();
        rectTrans.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        //2.激活
        this.gameObject.SetActive(true);
        //3.线性放大
        rectTrans.DOScale(new Vector3(1f, 1f, 1f), Duration).SetEase(Ease.Linear);
    }

    public void ScaleOut()
    {
        //1.将原panel线性缩小 
        //2.结束的关闭
        this.GetComponent<RectTransform>()
            .DOScale(new Vector3(0.4f, 0.4f, 0.4f), Duration/2f)
            .SetEase(Ease.Linear)
            .onKill = SetActiveFalse;
    }

    //将gamaObject的Active设置为false;
    private void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}