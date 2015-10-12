using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class UIMoveAuto : MonoBehaviour
{
    public float Distance;
    public float Duration;


    public void Start()
    {
        //自动循环播放
        Image image = this.GetComponent<Image>();
        Tweener mover = image.rectTransform.DOMoveX(Distance, Duration);
        mover.SetEase(Ease.Linear);
        mover.SetLoops(int.MaxValue, LoopType.Restart);
    }
}
