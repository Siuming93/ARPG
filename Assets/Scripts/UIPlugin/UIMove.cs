using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class UIMove : MonoBehaviour
{
    public float Target;
    public float Duration;
    public Ease Ease;
    public int Loops;
    public bool Active;

    public void Start()
    {
        SetActive(Active);
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            //1.激活,自动移动
            this.gameObject.SetActive(true);
            this.transform.DOMoveX(Target, Duration).SetEase(Ease).SetLoops(Loops);
        }
        else
        {
            //关闭,反向移动
            //暂时不做操作
            //this.gameObject.SetActive(false);
        }
    }
}
