using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    class ParticleAction:ActionBase
    {
        public Transform ParenTransform;
        public GameObject EffectGameObject;
        public override ActionType ActionType
        {
            get { throw new NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
            throw new NotImplementedException();
        }
    }
}
