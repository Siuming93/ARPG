using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Presenter.Manager;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    /// <summary>
    /// 特效行为
    /// </summary>
    public class BattleEffectAction : ActionBase
    {
        /// <summary>
        /// 特效球
        /// </summary>
        public GameObject EffectPrefabGameObject;

        /// <summary>
        /// 所绑定的Tag
        /// </summary>
        public string Tag;

        /// <summary>
        /// 相对位置
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// 相对旋转
        /// </summary>
        public Quaternion Rotaion;

        public override ActionType ActionType
        {
            get { throw new NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
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