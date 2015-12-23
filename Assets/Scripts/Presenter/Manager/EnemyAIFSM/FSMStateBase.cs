using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.EnemyAIFSM
{
    /// <summary>
    /// 状态机基类
    /// </summary>
    public abstract class FSMStateBase
    {
        /// <summary>
        /// 字典,记录转换状态信息
        /// </summary>
        protected Dictionary<Transition, FSMStateId> map = new Dictionary<Transition, FSMStateId>();

        /// <summary>
        /// 状态编号
        /// </summary>
        public abstract FSMStateId StateId { get; }

        /// <summary>
        /// 目标位置
        /// </summary>
        protected Vector3 DestVector3;

        /// <summary>
        /// 巡逻点数组
        /// </summary>
        protected Transform[] WayPointTransforms;

        /// <summary>
        /// 转向速度
        /// </summary>
        protected float CurRotSpeed;

        /// <summary>
        /// 移动速度
        /// </summary>
        protected float CurSpeed;

        /// <summary>
        /// 切换追逐的距离
        /// </summary>
        protected float chaseDistance = 40.0f;

        /// <summary>
        /// 切换攻击的距离
        /// </summary>
        protected float AttackDistance = 4.0f;

        /// <summary>
        /// 到达巡逻地点的误差距离
        /// </summary>
        protected float ArriveDistance = 3.0f;


        public void AddTransition(Transition transition, FSMStateId fsmStateId)
        {
            map[transition] = fsmStateId;
        }

        /// <summary>
        /// 确定手否需要转换以及如何转换
        /// </summary>
        /// <param name="player"></param>
        /// <param name="npc"></param>
        public abstract void Reason(Transform player, Transform npc);

        /// <summary>
        /// 定义角色行为
        /// </summary>
        /// <param name="player"></param>
        /// <param name="npc"></param>
        public abstract void Act(Transform player, Transform npc);

        public FSMStateId GetNextStateId(Transition trans)
        {
            return map[trans];
        }
    }
}