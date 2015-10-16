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
    public bool IsActive;

    public void Start()
    {
        if (IsActive)
            Move();
    }

    /// <summary>
    /// 根据默认值移动
    /// </summary>
    public void Move()
    {
        this.transform.DOMoveX(Target, Duration).SetEase(Ease).SetLoops(Loops);
    }

    /// <summary>
    /// 根据所给参数移动
    /// </summary>
    /// <param name="target"></param>
    /// <param name="duration"></param>
    public void Move(Vector3 target, float duration)
    {
        this.transform.DOMove(target, duration);
    }

    /// <summary>
    /// 激活后移动或者移动后关闭
    /// </summary>
    /// <param name="active"></param>
    public void MoveAndSetActive(bool active)
    {
        if (active)
        {
            //1.激活,自动移动
            this.gameObject.SetActive(true);
            this.transform.DOMoveX(Target, Duration).SetEase(Ease).SetLoops(Loops);
        }
        else
        {
            //2.关闭,移动完成后关闭
            this.transform.DOMoveX(Target, Duration).SetEase(Ease).SetLoops(Loops).onKill = SetActiveFalse;
        }
    }

    /// <summary>
    /// 将当前gamaObject的Active设置为false
    /// </summary>
    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }


    
}
