using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float Speed;
   

    private Animator animator;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }


    void Update()
    {
        //1.获得移动方向
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 ve = new Vector3(-v, 0f, h) * Speed;

        if (ve.magnitude > 0f)
        {
            rigidbody.velocity = ve;
            transform.rotation = Quaternion.LookRotation(new Vector3(-v, 0f, h));
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);
        }


    }


}
