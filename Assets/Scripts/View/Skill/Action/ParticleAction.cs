using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    /// <summary>
    /// 粒子特效
    /// </summary>
    internal class ParticleAction : ActionBase
    {
        /// <summary>
        /// 粒子特效的父类
        /// </summary>
        public Transform ParenTransform;

        /// <summary>
        /// 粒子预制
        /// </summary>
        public GameObject EffectGameObject;

        public override ActionType ActionType
        {
            get { throw new NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
            throw new NotImplementedException();
        }

        protected override void Play()
        {
            throw new NotImplementedException();
        }
    }
}