using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.EnemyAIFSM
{
    internal class DeadState : FSMStateBase
    {
        public override FSMStateId StateId
        {
            get { return FSMStateId.Dead; }
        }

        public override void Reason(Transform player, Transform npc)
        {
            throw new NotImplementedException();
        }

        public override void Act(Transform player, Transform npc)
        {
            throw new NotImplementedException();
        }
    }
}