using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Skill.Action
{
    /// <summary>
    /// 动画行为
    /// </summary>
    public class AnimationAction : ActionBase
    {
        /// <summary>
        /// 触发的触发器名称
        /// </summary>
        public string TriggerName;

        /// <summary>
        /// 角色所带的动画控制器
        /// </summary>
        private Animator _animator;

        public override ActionType ActionType
        {
            get { throw new NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
            _animator = playerGameObject.GetComponentInChildren<Animator>();
        }


        protected override void Play()
        {
            //触发相应触发器
            _animator.SetTrigger(TriggerName);

            Finish();
        }
    }
}