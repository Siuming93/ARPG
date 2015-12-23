using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.View.Charcter.Player;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.EnemyAIFSM
{
    internal class AttackState : FSMStateBase
    {
        public override FSMStateId StateId
        {
            get { return FSMStateId.Attacking; }
        }

        public override void Reason(Transform player, Transform npc)
        {
            //1.检测与玩家距离
            var distance = Vector3.Distance(npc.position, player.position);

            //2.小于攻击距离,转换到攻击状态
            if (distance > AttackDistance)
            {
                npc.GetComponent<EnemyAiController>().TransitionState(Transition.LostPlayer);
            }
        }

        public override void Act(Transform player, Transform npc)
        {
            //播放动画
            var animation = npc.GetComponent<AnimationView>();
            animation.PlayAnimation("Attack");
        }
    }
}