using Assets.Scripts.Presenter.Manager;
using UnityEngine;

namespace Assets.Scripts.View.Charcter.Player
{
    /// <summary>
    /// 角色移动
    /// </summary>
    public class PlayerMoveView : MonoBehaviour
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float Speed;

        private Animator _animator;
        private float v, h;

        private void Start()
        {
            _animator = transform.GetComponentInChildren<Animator>();
            Joystick.Instance.OnJoystickMovement += GetMoveAxis;
            Joystick.Instance.OnEndJoystickMovement += EndJoyMove;
        }

        private void GetMoveAxis(Joystick joystick, Vector2 axis)
        {
            h = axis.x;
            v = axis.y;
        }

        private void EndJoyMove(Joystick joystick)
        {
            h = 0;
            v = 0;
        }

        private void Update()
        {
            //若当前不是处于移动后者Idle状态,则不移动
            if (SkillManager.Instance != null && SkillManager.Instance.IsCurSkillExcute ||
                !_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
                !_animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                return;

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
}