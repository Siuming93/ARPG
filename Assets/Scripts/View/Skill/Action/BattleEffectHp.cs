using System;
using System.Collections.Generic;
using Assets.Scripts.View.Player;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    class BattleEffectHp:MonoBehaviour
    {
        public int Value;
        public TargetType TargetType;

        private List<PlayerState> _stateList = new List<PlayerState>(); 
        void OnTrigger(Collider other)
        {
            switch (TargetType)
            {
                case TargetType.Enemy:
                    if (other.collider.tag == Tags.Enemy)
                    {
                       other.collider.gameObject.GetComponent<PlayerState>().TakeDamage();
                    }
                    break;
                case TargetType.Player:
                    if (other.collider.tag == Tags.Player)
                    {
                        other.collider.gameObject.GetComponent<PlayerState>().TakeDamage();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
