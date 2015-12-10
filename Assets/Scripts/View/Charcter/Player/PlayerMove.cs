using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float Speed;
    private Animator _animator;

    private void Start()
    {
        _animator = transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (SkillManager.Instance.IsCurSkillExcute ||
            !_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
            !_animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            return;
        //1.获得移动方向
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 ve = new Vector3(-v, 0f, h)*Speed;

        if (ve.magnitude > 0f)
        {
            rigidbody.velocity = ve;
            transform.rotation = Quaternion.LookRotation(new Vector3(-v, 0f, h));
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}