using UnityEngine;

namespace Assets.Scripts.Model.Charcter
{
    /// <summary>
    /// 角色信息,是Enemy和玩家信息的父类
    /// </summary>
    public abstract class CharctorInfo : MonoBehaviour
    {
        /// <summary>
        /// 血量
        /// </summary>
        public int Hp { get; set; }

        /// <summary>
        /// 攻击力
        /// </summary>
        public int Damager { get; set; }

        /// <summary>
        /// 接受伤害
        /// </summary>
        /// <param name="value"></param>
        public abstract void TakeDamage(int value);
    }
}