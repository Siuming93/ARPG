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
        public List<ActionBase> Actions = new List<ActionBase>();

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
            for (var i = 0; i < Actions.Count; i++)
            {
                Actions[i].Excute();
            }
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