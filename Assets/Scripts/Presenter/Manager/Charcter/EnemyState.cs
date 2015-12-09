using UnityEngine;
using System.Collections;
using Assets.Scripts.Presenter.Manager;
using Assets.Scripts.Presenter.Manager.Charcter;
using Assets.Scripts.View.Skill.Action;

public class EnemyState : MonoBehaviour
{
    private Animator _animator;
    public string Name;
    public int MaxHp;
    private int _curHp;

    private void Start()
    {
        _animator = transform.GetComponentInChildren<Animator>();
        _curHp = MaxHp;
        OnInfoChangeEvent();
    }


    public void TakeDamage(GameObject sourceGameObject, int value, string trigger)
    {
        _curHp -= value;
        if (_curHp < 0)
        {
            trigger = "Death";
            EnemyManager.Instance.EnemyDead(this);
        }
        else

            trigger = "BeHit";

        OnInfoChangeEvent();
        OnTakeDamageEvent(sourceGameObject, trigger);
    }

    public float GetHpPercet()
    {
        return (float) _curHp/MaxHp;
    }

    public event OnTakeDamageEvent OnTakeDamageEvent;

    public event OnInfoChangeEvent OnInfoChangeEvent;
}