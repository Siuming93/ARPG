using Assets.Scripts.Presenter.Manager.Charcter;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    public class AttackAction : ActionBase
    {
        public int Value;
        public AttackDirection AttackDirection;
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
            EnemyManager.Instance.TakeDamanage(_playerTransform, Value, AttackDirection);

            Finish();
        }
    }

    public enum AttackDirection : byte
    {
        Forward,
        Around
    }
}