/*用来显示攻击计数的combo数*/

using Assets.Scripts.Presenter.Manager.Charcter;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Transcript
{
    /// <summary>
    /// 显示连击信息
    /// </summary>
    public class ComboUiView : MonoBehaviour
    {
        //显示的UIText组件
        public Text Text;
        //显示的事件,即中断计时
        public float ShowTime;
        //波动范围
        public float Duration;

        //显示时间计时器
        private float timer;
        //当前值
        private int value;
        //之前值
        private int oldValue;

        private void Start()
        {
            //在敌人管理器中注册,并被调用
            EnemyManager.Instance.OnComboAddEvent += AddCombo;
            //平时不显示
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 增加一下combo
        /// </summary>
        public void AddCombo()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            //1.重新计时
            timer = 0f;
            //2.增加值
            value++;
        }

        /// <summary>
        /// 更新combo数据显示
        /// </summary>
        private void LateUpdate()
        {
            if (oldValue != value)
            {
                //1.更新数字
                Text.text = value.ToString();
                //2.shake
                transform.DOShakePosition(Duration, new Vector3(3f, 3f, 0f)).SetLoops(1);
                Text.transform.DOShakeScale(Duration, new Vector3(1f, 2f, 0f)).SetLoops(1);
            }

            //3.计时,到了就消失
            timer += Time.deltaTime;
            if (timer > ShowTime)
            {
                value = 0;
                gameObject.SetActive(false);
            }

            oldValue = value;
        }
    }
}