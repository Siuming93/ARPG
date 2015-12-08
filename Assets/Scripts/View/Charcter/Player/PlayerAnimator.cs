using UnityEngine;
using System.Collections;

/// <summary>
/// 控制角色动画
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    public float sensitive;

    private NavMeshAgent nav;
    private Animator animator;
    private Rigidbody rigidbody;

    public void PlayAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    private void Start()
    {
        nav = transform.GetComponentInParent<NavMeshAgent>();
        rigidbody = transform.GetComponentInParent<Rigidbody>();
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (nav.enabled || rigidbody.velocity.magnitude > sensitive)
            animator.SetBool("IsMove", true);
        else
            animator.SetBool("IsMove", false);
    }
}