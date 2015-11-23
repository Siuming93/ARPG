using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class UIMove : MonoBehaviour
{
    public float Target;
    public float Duration = 0.4f;
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

    public void MoveOut(Direction direction, float duration = 0.4f)
    {
        //1.设定目标
        Vector3 target;
        Duration = duration;
        switch (direction)
        {
            case Direction.UptoCenter:
                target = new Vector3(transform.position.x, Screen.height*1.5f, transform.position.z);
                break;
            case Direction.BottomtoCenter:
                target = new Vector3(transform.position.x, -Screen.height*0.5f, transform.position.z);
                break;
            case Direction.LefttoCenter:
                target = new Vector3(-Screen.width*0.5f, transform.position.y, transform.position.z);
                break;
            case Direction.RighttoCenter:
                target = new Vector3(Screen.width*1.5f, transform.position.y, transform.position.z);
                break;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }
        //2.移动完成后关闭
        this.transform.DOMove(target, Duration).SetEase(Ease).SetLoops(Loops).onKill = SetActiveFalse;
    }

    public void MoveIn(Direction direction, float duration = 0.4f)
    {
        //1.挪到初始位置
        Duration = duration;
        switch (direction)
        {
            case Direction.UptoCenter:
                transform.position = new Vector3(transform.position.x, Screen.height*1.5f, transform.position.z);
                break;
            case Direction.BottomtoCenter:
                transform.position = new Vector3(transform.position.x, -Screen.height*0.5f, transform.position.z);
                break;
            case Direction.LefttoCenter:
                transform.position = new Vector3(-Screen.width*0.5f, transform.position.y, transform.position.z);
                break;
            case Direction.RighttoCenter:
                transform.position = new Vector3(Screen.width*1.5f, transform.position.y, transform.position.z);
                break;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }
        //2.激活,自动移动
        this.gameObject.SetActive(true);
        this.transform.DOMove(new Vector3(Screen.width*0.5f, Screen.height*0.5f, transform.position.z), Duration)
            .SetEase(Ease)
            .SetLoops(Loops);
    }
}

public enum Direction : byte
{
    UptoCenter,
    BottomtoCenter,
    LefttoCenter,
    RighttoCenter
}