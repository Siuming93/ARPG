using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Skill.Action
{
    public abstract class ActionBase : ScriptableObject
    {
        public float WaitTime;
        public abstract ActionType ActionType { get; }


        protected float _timer = 0f;
        public bool IsExcute { get; private set; }
        public abstract void Init(GameObject playerGameObject);

        public virtual void Update()
        {
            if (!IsExcute)
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
            IsExcute = true;
        }

        protected abstract void Play();

        public virtual void Finish()
        {
            IsExcute = false;
            _timer = 0f;
        }
    }

    public enum ActionType : byte
    {
        Attack,
        Buffer
    }
}