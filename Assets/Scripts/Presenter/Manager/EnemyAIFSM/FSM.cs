using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.EnemyAIFSM
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    public class FSM : MonoBehaviour
    {
        /// <summary>
        /// FSM中所有的状态列表
        /// </summary>
        protected readonly List<FSMStateBase> fsmStates = new List<FSMStateBase>();

        /// <summary>
        /// 当前状态编号
        /// </summary>
        public FSMStateId CurFsmStateId { get; private set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public FSMStateBase CurFsmState { get; protected set; }

        /// <summary>
        /// 玩家的Transform组件
        /// </summary>
        protected Transform PlayerTransform;

        /// <summary>
        /// 下一个巡逻点或者玩家的位置,取决于当前的状态
        /// </summary>
        protected Vector3 desPos;

        /// <summary>
        /// 巡逻点数组
        /// </summary>
        protected GameObject[] PointList;

        protected virtual void Initialize()
        {
        }

        protected virtual void FSMUpdate()
        {
        }

        public virtual void FSMFixedUpdate()
        {
        }

        public void PerformTransition(Transition transition)
        {
            //根据当前状态类,以trans为参数调用它的GetOutputState方法,确定转移后的新状态的编号
            var fsmId = CurFsmState.GetNextStateId(transition);


            for (int i = 0; i < fsmStates.Count; i++)
            {
                if (fsmStates[i].StateId == fsmId)
                {
                    CurFsmState = fsmStates[i];
                    break;
                }
            }
        }
    }

    public enum Transition
    {
        SawPlayer = 0,
        ReachPlayer,
        LostPlayer,
        NoHealth
    }

    public enum FSMStateId
    {
        Patrolling = 0,
        Chasing,
        Attacking,
        Dead
    }
}