using Assets.Scripts.Presenter.Manager.Charcter;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Transcript
{
    public class ComboUiManager : MonoBehaviour
    {
        public Text Text;
        public float ShowTime;

        public float duration;

        private float timer;
        private int value;
        private int oldValue;

        private void Start()
        {
            EnemyManager.Instance.OnComboAddEvent += AddCombo;
            gameObject.SetActive(false);
        }

        public void AddCombo()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            //1.重新计时
            timer = 0f;
            //2.增加值
            value++;
        }

        private void LateUpdate()
        {
            if (oldValue != value)
            {
                //1.更新数字
                Text.text = value.ToString();
                //2.shake
                transform.DOShakePosition(duration, new Vector3(3f, 3f, 0f)).SetLoops(1);
                Text.transform.DOShakeScale(duration, new Vector3(1f, 2f, 0f)).SetLoops(1);
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