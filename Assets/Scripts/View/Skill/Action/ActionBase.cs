using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Skill.Action
{
    public abstract class ActionBase : ScriptableObject
    {
        public float WaitTime;
        public abstract ActionType ActionType { get; }

        private float _timer = 0f;
        private bool _excute = false;

        public abstract void Init(GameObject playerGameObject);

        public virtual void Update()
        {
            if (!_excute)
                return;

            if (_timer <= WaitTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                Play();
            }
        }

        public virtual void Excute()
        {
            _excute = true;
        }

        protected virtual void Play()
        {
            Finish();
        }

        public virtual void Finish()
        {
            _excute = false;
            _timer = 0f;
        }
    }

    public enum ActionType : byte
    {
        Attack,
        Buffer
    }
}