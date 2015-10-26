using UnityEngine;
using System.Collections;

/// <summary>
/// 控制角色动画
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    public float sensitive;
    private Animator animator;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        print(rigidbody.velocity);
        if (rigidbody.velocity.magnitude > sensitive)
            animator.SetBool("IsMove", true);
        else
            animator.SetBool("IsMove", false);
    }
}
