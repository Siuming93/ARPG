using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.View.Charcter.Player;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.EnemyAIFSM
{
    /// <summary>
    /// 追逐状态
    /// </summary>
    internal class ChaseState : FSMStateBase
    {
        public ChaseState(Transform[] wp)
        {
            //设置转向与移动速度
            CurRotSpeed = 6.0f;
            CurSpeed = 160.0f;
        }

        public override FSMStateId StateId
        {
            get { return FSMStateId.Chasing; }
        }

        public override void Reason(Transform player, Transform npc)
        {
            //1.检测与玩家距离
            var distance = Vector3.Distance(npc.position, player.position);

            //2.小于攻击距离,转换到攻击状态
            if (distance <= AttackDistance)
            {
                npc.GetComponent<EnemyAiController>().TransitionState(Transition.ReachPlayer);
            }

            //3.如果与玩家距离超出追逐距离,回去巡逻
            else if (distance >= chaseDistance)
            {
                npc.GetComponent<EnemyAiController>().TransitionState(Transition.LostPlayer);
            }
        }

        public override void Act(Transform player, Transform npc)
        {
            //将玩家位置设为目标位置
            DestVector3 = player.position;

            //转向目标点
            var targetRotation = Quaternion.LookRotation(DestVector3 - npc.position);
            npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime*CurRotSpeed);

            //向前移动
            var cc = npc.GetComponent<CharacterController>();
            cc.SimpleMove(npc.transform.forward*Time.deltaTime*CurSpeed);

            //播放动画
            var animation = npc.GetComponent<AnimationView>();
            animation.PlayAnimation("IsMove");
        }
    }
}