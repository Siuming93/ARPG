using Assets.Scripts.Presenter.Manager;
using UnityEngine;

namespace Assets.Scripts.Model.Charcter
{
    /// <summary>
    /// 敌人状态
    /// </summary>
    public class EnemyState : MonoBehaviour
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 最大血量
        /// </summary>
        public int MaxHp;

        /// <summary>
        /// 当前血量
        /// </summary>
        private int _curHp;

        private void Start()
        {
            _curHp = MaxHp;
            //初始时即进行广播,更改血条状态
            OnInfoChangeEvent();
        }

        /// <summary>
        /// 承受伤害
        /// </summary>
        /// <param name="sourceGameObject"></param>
        /// <param name="value"></param>
        /// <param name="trigger">攻击触发的动画特效</param>
        public void TakeDamage(GameObject sourceGameObject, int value, string trigger)
        {
            _curHp -= value;
            //血量小于0 确认死亡
            if (_curHp < 0)
            {
                trigger = "Death";
                OnDeadEvent(this);
            }
            else

                trigger = "BeHit";

            OnInfoChangeEvent();
            OnTakeDamageEvent(sourceGameObject, trigger);
        }

        /// <summary>
        /// 获得血量的百分比
        /// </summary>
        /// <returns>血量百分比</returns>
        public float GetHpPercet()
        {
            return (float) _curHp/MaxHp;
        }

        /// <summary>
        /// 当受到伤害时
        /// </summary>
        public event OnTakeDamageEvent OnTakeDamageEvent;

        /// <summary>
        /// 当信息改变时
        /// </summary>
        public event OnInfoChangeEvent OnInfoChangeEvent;

        /// <summary>
        /// 当敌人应该死亡时
        /// </summary>
        public event OnDeadEvent OnDeadEvent;
    }
}