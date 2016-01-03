using Assets.Scripts.Presenter.Manager.Charcter;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    /// <summary>
    /// 攻击行为
    /// </summary>
    public class AttackAction : ActionBase
    {
        /// <summary>
        /// 伤害的数值
        /// </summary>
        public int Value;

        /// <summary>
        /// 攻击的方向
        /// </summary>
        public AttackDirection AttackDirection;

        /// <summary>
        /// 攻击起始点
        /// </summary>
        private Transform _playerTransform;

        public override ActionType ActionType
        {
            get { throw new System.NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
            _playerTransform = playerGameObject.transform;
        }

        protected override void Play()
        {
            //1.传递位置和伤害值以及范围
            EnemyManager.Instance.Attack(_playerTransform, Value, AttackDirection);

            Finish();
        }
    }

    public enum AttackDirection : byte
    {
        Forward,
        Around
    }
}