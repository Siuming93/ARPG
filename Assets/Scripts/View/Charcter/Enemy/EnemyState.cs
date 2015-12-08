using UnityEngine;
using System.Collections;
using Assets.Scripts.Presenter.Manager.Charcter;
using Assets.Scripts.View.Skill.Action;

public class EnemyState : MonoBehaviour
{
    private PlayerAnimator _animator;

    private void Awake()
    {
        _animator = transform.GetComponentInChildren<PlayerAnimator>();
    }


    public void PlayAnimation(string trigger)
    {
        print("OnPlayAnimation" + trigger);
        _animator.PlayAnimation(trigger);
    }
}