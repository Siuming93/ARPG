using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Presenter.Manager;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    public class BattleEffectAction : ActionBase
    {
        public GameObject EffectPrefabGameObject;
        public string Tag;
        public Vector3 Position;
        public Quaternion Rotaion;
        private Transform _playerTransform;

        public override ActionType ActionType
        {
            get { throw new NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
            _playerTransform = playerGameObject.transform;
        }

        protected override void Play()
        {
            var effect = Instantiate(EffectPrefabGameObject) as GameObject;
            var Transform = GameObject.FindGameObjectWithTag(Tag).transform;
            effect.transform.parent = Transform;
            effect.transform.localPosition = Position;
            effect.transform.localRotation = Rotaion;

            Finish();
        }
    }

    public enum TargetType
    {
        Enemy,
        Player
    }
}