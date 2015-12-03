using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    public  class BattleEffectAction : ActionBase
    {
        public GameObject EffectPrefabGameObject;
        private string _tag;
        private Transform _playerTransform;

        public override ActionType ActionType
        {
            get { throw new NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
            _tag = playerGameObject.tag;
            _playerTransform = playerGameObject.transform;
        }

        protected override void Play()
        {
            Instantiate(EffectPrefabGameObject, _playerTransform.position, _playerTransform.rotation);
        }
    }

    public enum TargetType
    {
        Enemy,
        Player
    }
}