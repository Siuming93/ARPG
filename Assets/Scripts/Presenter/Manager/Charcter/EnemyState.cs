using UnityEngine;
using System.Collections;
using Assets.Scripts.Presenter.Manager;
using Assets.Scripts.Presenter.Manager.Charcter;
using Assets.Scripts.View.Skill.Action;

public class EnemyState : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = transform.GetComponentInChildren<Animator>();
    }


    public void TakeDamage(string trigger)
    {
        OnTakeDamageEvent();
    }

    public event OnTakeDamageEvent OnTakeDamageEvent;
}