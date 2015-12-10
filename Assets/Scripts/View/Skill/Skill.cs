using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.View.Skill.Action;
using UnityEngine;

namespace Assets.Scripts.View.Skill
{
    public class Skill : ScriptableObject
    {
        public int Id;
        public string Name;
        public float CDTime;
        public string Description;
        public List<ActionBase> Actions = new List<ActionBase>();

        private Animator animator;

        public bool IsExcute
        {
            get
            {
                for (var i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].IsExcute)
                        return true;
                }

                return false;
            }
        }

        public float CDTimePercent
        {
            get { return CDTime == 0f ? 0f : 1 - timer/CDTime; }
        }


        private float timer = 0;

        public void Init(GameObject player)
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                Actions[i].Init(player);
            }

            animator = player.GetComponentInChildren<Animator>();
        }

        public void Update()
        {
            for (var i = 0; i < Actions.Count; i++)
            {
                Actions[i].Update();
            }
        }

        public void Excute()
        {
            SkillManager.Instance.StartCoroutine(CalculateExcute());

            SkillManager.Instance.StartCoroutine(CalculateCDTime());
        }

        private IEnumerator CalculateExcute()
        {
            bool hasExcuteAllAction = false;
            while (true)
            {
                for (var i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].GetType() == typeof (AnimationAction))
                    {
                        Actions[i].Excute();
                        yield return null;
                    }
                    else if (animator.IsInTransition(0) && Actions[i].GetType() != typeof (AnimationAction))
                    {
                        Actions[i].Excute();
                        hasExcuteAllAction = true;
                    }
                }

                if (hasExcuteAllAction)
                    break;
            }
        }

        private IEnumerator CalculateCDTime()
        {
            timer = 0;

            while (timer < CDTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            timer = CDTime;
        }

        public void Finish()
        {
            for (var i = 0; i < Actions.Count; i++)
            {
                Actions[i].Finish();
            }
        }
    }
}