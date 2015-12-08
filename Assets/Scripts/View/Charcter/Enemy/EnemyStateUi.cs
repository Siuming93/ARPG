using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Charcter.Enemy
{
    /// <summary>
    /// 负责动画的播放,血量的显示
    /// </summary>
    public class EnemyStateUi : MonoBehaviour
    {
        //绑定一个p层的状态
        public EnemyState EnemyState;
        public Transform HpandNameUiParenTransform;
        public GameObject HpandNameUiPerfab;

        private Animator _animator;
        private HpandNameUi _hpandNameUi;

        private void Start()
        {
            _hpandNameUi =
                ((GameObject) Instantiate(HpandNameUiPerfab)).GetComponent<HpandNameUi>();
            _hpandNameUi.transform.parent = HpandNameUiParenTransform;
            _hpandNameUi.Fellow = transform;
            _animator = transform.GetComponentInChildren<Animator>();

            EnemyState.OnTakeDamageEvent += OnTakeDamage;
        }


        private void Init(EnemyState state)
        {
            EnemyState = state;
            OnUpdateShowInfo();
        }


        private void PlayAnimation()
        {
            //把所有关于动画的控制挪到这里来
        }

        private void OnUpdateShowInfo()
        {
            _hpandNameUi.HpPercent = 0.5f;
        }

        private void OnTakeDamage()
        {
            //1.播放动画
            _animator.SetTrigger("BeHit");
            //2.击退
            var damageSource = GameObject.FindGameObjectWithTag(Tags.Player).transform;
            transform.position += damageSource.forward;
        }
    }
}