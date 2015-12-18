using System;
using System.Collections.Generic;
using Assets.Scripts.Model.Charcter;
using Assets.Scripts.View;
using Assets.Scripts.View.Skill.Action;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.Charcter
{
    /// <summary>
    /// 场景中所有敌人的管理器
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static EnemyManager Instance { get; private set; }

        /// <summary>
        /// 生成点
        /// </summary>
        public List<EnemyAutoBornView> EnemyBornPointList = new List<EnemyAutoBornView>();

        /// <summary>
        /// 暂定的攻击距离
        /// </summary>
        public float Distance;

        /// <summary>
        /// 玩家
        /// </summary>
        public GameObject Player;

        /// <summary>
        /// 所有敌人列表
        /// </summary>
        private List<EnemyState> _enemyList = new List<EnemyState>();

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 生成了敌人
        /// </summary>
        /// <param name="enemyState"></param>
        public void AddEnemy(EnemyState enemyState)
        {
            _enemyList.Add(enemyState);
            enemyState.OnDeadEvent += EnemyDead;
        }

        /// <summary>
        /// 寻找所有可以被攻击的敌人,并进行攻击逻辑判断
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value"></param>
        /// <param name="attackDirection"></param>
        public void TakeDamanage(Transform transform, int value, AttackDirection attackDirection)
        {
            //找到所有要受到攻击的怪物v
            var enemyToTakeDamage = new List<EnemyState>();
            for (var i = 0; i < _enemyList.Count; i++)
            {
                var enemystate = _enemyList[i];
                //在距离内
                if (Vector3.Distance(transform.position, enemystate.transform.position) < Distance)
                {
                    //判断攻击方向
                    switch (attackDirection)
                    {
                        case AttackDirection.Forward:
                            var angleCos = Vector3.Dot(transform.forward,
                                (enemystate.transform.position - transform.position).normalized);
                            if (angleCos > 0.5f)
                                enemyToTakeDamage.Add(enemystate);
                            break;
                        case AttackDirection.Around:
                            enemyToTakeDamage.Add(enemystate);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("attackDirection", attackDirection, null);
                    }
                }
            }
            //受到伤害
            for (var i = 0; i < enemyToTakeDamage.Count; i++)
            {
                enemyToTakeDamage[i].TakeDamage(Player, 10, "BeHit");
                if (OnComboAddEvent != null) OnComboAddEvent();
            }
        }

        /// <summary>
        /// 敌人死亡
        /// </summary>
        /// <param name="enemyState"></param>
        public void EnemyDead(EnemyState enemyState)
        {
            _enemyList.Remove(enemyState);
        }

        /// <summary>
        /// 连击增加
        /// </summary>
        public event OnComboAddEvent OnComboAddEvent;

        /// <summary>
        /// 将清空的怪物生成点移除
        /// </summary>
        /// <param name="enemyAutoBornView"></param>
        public void RemoveEnemyBornPoint(EnemyAutoBornView enemyAutoBornView)
        {
            EnemyBornPointList.Remove(enemyAutoBornView);

            if (EnemyBornPointList.Count == 0)
            {
                GameManager.Instance.PassTranscript("1");
            }
        }
    }
}