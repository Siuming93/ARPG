using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Skill.Action
{
    public class AnimationAction : ActionBase
    {
        public string TriggerName;

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
            _animator.SetTrigger(TriggerName);

            Finish();
        }
    }
}