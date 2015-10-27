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

    void Start()
    {
        nav = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        if (nav.enabled || rigidbody.velocity.magnitude > sensitive)
            animator.SetBool("IsMove", true);
        else
            animator.SetBool("IsMove", false);
    }
}
