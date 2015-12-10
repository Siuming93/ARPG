using System.Collections;
using System.Reflection.Emit;
using System.Security.Cryptography;
using Assets.Scripts.Presenter.Manager;
using Assets.Scripts.Presenter.Manager.Charcter;
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
        public GameObject BloodSplatEffectPerfab;
        public float DisapearTime;
        public Vector3 BooldEffectVector3;

        private Animator _animator;
        private HpandNameUi _hpandNameUi;
        private AudioSource _audioSource;
        private bool _isDying = false;

        private static int count = 0;

        private void Awake()
        {
            EnemyState.OnTakeDamageEvent += OnTakeDamage;
            EnemyState.OnInfoChangeEvent += OnUpdateShowInfo;
            HpandNameUiParenTransform = GameObject.Find("HpandNamePanel").transform;

            _hpandNameUi =
                ((GameObject) Instantiate(HpandNameUiPerfab)).GetComponent<HpandNameUi>();
            _hpandNameUi.transform.parent = HpandNameUiParenTransform;
            _hpandNameUi.Fellow = transform;
            _hpandNameUi.Name = EnemyState.Name;

            _animator = transform.GetComponentInChildren<Animator>();
            _audioSource = transform.GetComponent<AudioSource>();
        }

        private void Start()
        {
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
            _hpandNameUi.HpPercent = EnemyState.GetHpPercet();
        }

        private void OnTakeDamage(GameObject source, string trigger)
        {
            if (_isDying)
                return;

            //1.播放动画
            _animator.SetTrigger(trigger);
            //2.击退
            transform.position += (transform.position - source.transform.position).normalized;
            //3.播放音效,要限制播放数,不超过四个
            if (count < 1)
            {
                _audioSource.Play();
                count++;
            }
            //4.放血
            var effect = Instantiate(BloodSplatEffectPerfab, transform.position, transform.rotation) as GameObject;
            effect.transform.parent = transform;
            effect.transform.localPosition += BooldEffectVector3;

            //若死亡,要挪到底下去
            if (trigger == "Death")
            {
                _isDying = true;
                StartCoroutine(Dead());
            }
        }

        private void LateUpdate()
        {
            count = 0;
        }

        private IEnumerator Dead()
        {
            float timer = 0;
            _hpandNameUi.DestroySelf();

            while (timer < DisapearTime)
            {
                //取消刚体
                if (rigidbody != null)
                    Destroy(rigidbody);
                timer += Time.deltaTime;
                transform.position -= 3*transform.up*Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}