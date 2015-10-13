using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class UIScale : MonoBehaviour
{
    //持续的时间
    public float Duration = 0.4f;

    //是不是还在播放
    public bool isPlaying;

    Tweener scale;

    //UI刚刚进入的时候要播放缩放
    public void SetActive(bool value)
    {
        if (value)
        {
            //先将原panel缩小
            RectTransform rectTrans = this.GetComponent<RectTransform>();
            rectTrans.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            //激活
            this.gameObject.SetActive(true);

            //线性放大
            scale = rectTrans.DOScale(new Vector3(1f, 1f, 1f), Duration);
            scale.SetEase(Ease.Linear);
        }
        else
        {
            //将原panel线性缩小
            RectTransform rectTrans = this.GetComponent<RectTransform>();
            scale = rectTrans.DOScale(new Vector3(0.4f, 0.4f, 0.4f), Duration / 2f);
            scale.SetEase(Ease.Linear);
            scale.onKill = OnKill;
        }
    }

    void OnKill()
    {
        //激活
        this.gameObject.SetActive(false);
    }


}
