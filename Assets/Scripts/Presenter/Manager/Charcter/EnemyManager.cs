﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.View.Skill.Action;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.Charcter
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }

        public List<EnemyAutoBorn> EnemyBornPointList = new List<EnemyAutoBorn>();
        public float Distance;
        public GameObject Player;

        private List<EnemyState> EnemyList = new List<EnemyState>();

        private void Awake()
        {
            Instance = this;
        }

        public void AddEnemy(EnemyState enemyState)
        {
            EnemyList.Add(enemyState);
            enemyState.OnDeadEvent += EnemyDead;
        }

        public void TakeDamanage(Transform transform, int value, AttackDirection attackDirection)
        {
            //找到所有要受到攻击的怪物v
            var enemyToTakeDamage = new List<EnemyState>();
            for (var i = 0; i < EnemyList.Count; i++)
            {
                var enemystate = EnemyList[i];
                //在距离内
                if (Vector3.Distance(transform.position, enemystate.transform.position) < Distance)
                {
                    //在方向内
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

        public void EnemyDead(EnemyState enemyState)
        {
            EnemyList.Remove(enemyState);
        }

        public event OnComboAddEvent OnComboAddEvent;

        public void RemoveEnemyBornPoint(EnemyAutoBorn enemyAutoBorn)
        {
            EnemyBornPointList.Remove(enemyAutoBorn);

            if (EnemyBornPointList.Count == 0)
            {
                GameManger.Instance.PassTranscript("1");
            }
        }
    }
}