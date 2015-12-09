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
            timer = 0;
            for (var i = 0; i < Actions.Count; i++)
            {
                Actions[i].Excute();
            }

            SkillManager.Instance.StartCoroutine(CalculateCDTime());
        }

        private IEnumerator CalculateCDTime()
        {
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