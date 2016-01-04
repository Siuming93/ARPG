using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Assets.Scripts.Presenter.Manager;
using Assets.Scripts.Presenter.Manager.Charcter;
using Assets.Scripts.View.Skill.Action;
using UnityEngine;

namespace Assets.Scripts.View.Skill
{
    /// <summary>
    /// 技能
    /// </summary>
    public class Skill : ScriptableObject, ICloneable
    {
        /// <summary>
        /// 技能ID
        /// </summary>
        public int Id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 冷却时间
        /// </summary>
        public float CDTime;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 带有的行为
        /// </summary>
        public List<ActionBase> Actions = new List<ActionBase>();

        /// <summary>
        /// 动画播放器
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// 是否有行为在执行
        /// </summary>
        public bool IsExcute
        {
            get
            {
                for (var i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].IsExcute)
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 冷却百分比
        /// </summary>
        public float CDTimePercent
        {
            get { return CDTime == 0f ? 0f : 1 - timer/CDTime; }
        }

        /// <summary>
        /// 计时器
        /// </summary>
        private float timer = 0;

        /// <summary>
        /// 初始化所有行为
        /// </summary>
        /// <param name="player"></param>
        public void Init(GameObject player)
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                Actions[i].Init(player);
            }

            _animator = player.GetComponentInChildren<Animator>();
        }

        public void Update()
        {
            for (var i = 0; i < Actions.Count; i++)
            {
                Actions[i].Update();
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        public void Excute()
        {
            GameManager.Instance.StartCoroutine(CalculateExcute());
            GameManager.Instance.StartCoroutine(CalculateCDTime());
        }

        /// <summary>
        /// 延时执行
        /// </summary>
        /// <returns></returns>
        private IEnumerator CalculateExcute()
        {
            bool hasExcuteAllAction = false;
            while (true)
            {
                for (var i = 0; i < Actions.Count; i++)
                {
                    //动画行为立刻执行
                    if (Actions[i].GetType() == typeof (AnimationAction))
                    {
                        Actions[i].Excute();
                        yield return null;
                    }
                    //非动画行为只能在转换的时候开始执行
                    else if (_animator.IsInTransition(0) && Actions[i].GetType() != typeof (AnimationAction))
                    {
                        Actions[i].Excute();
                        hasExcuteAllAction = true;
                    }
                }

                if (hasExcuteAllAction)
                    break;
            }
        }

        /// <summary>
        /// 计算  CD时间
        /// </summary>
        /// <returns></returns>
        private IEnumerator CalculateCDTime()
        {
            timer = 0;

            while (timer < CDTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            timer = CDTime;
        }

        /// <summary>
        /// 强制结束
        /// </summary>
        public void Finish()
        {
            for (var i = 0; i < Actions.Count; i++)
            {
                Actions[i].Finish();
            }
        }

        /// <summary>
        /// 克隆技能
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var newObject = Activator.CreateInstance(this.GetType());

            var filds = newObject.GetType().GetFields();
            var thisFilds = this.GetType().GetFields();

            //克隆fild
            for (var i = 0; i < thisFilds.Length; i++)
            {
                var fi = thisFilds[i];
                filds[i].SetValue(newObject, fi.GetValue(this));
            }

            var list = newObject.GetType().GetField("Actions").GetValue(newObject) as List<ActionBase>;
            //克隆list
            for (var i = 0; i < Actions.Count; i++)
            {
                list[i] = Actions[i].Clone() as ActionBase;
            }


            return newObject;
        }
    }
}