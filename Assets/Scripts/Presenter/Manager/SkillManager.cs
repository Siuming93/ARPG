using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.UIPlugin;
using Assets.Scripts.View.Skill;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static SkillManager Instance { get; private set; }

        /// <summary>
        /// 所有技能的字典
        /// </summary>
        private Dictionary<int, Skill> _skills = new Dictionary<int, Skill>();

        /// <summary>
        /// 当前角色所拥有的技能
        /// </summary>
        public Dictionary<int, Skill> PlayerSkills = new Dictionary<int, Skill>();

        /// <summary>
        /// 当前正在执行的技能
        /// </summary>
        public Skill CurSkill;

        /// <summary>
        /// 等待执行的技能列表
        /// </summary>
        private List<int> SkillToExcuteList = new List<int>();

        /// <summary>
        /// 当前技能是否正在执行
        /// </summary>
        public bool IsCurSkillExcute
        {
            get
            {
                if (CurSkill != null)
                    return CurSkill.IsExcute;

                return false;
            }
        }

        /// <summary>
        /// 执行技能
        /// </summary>
        /// <param name="id">需要执行的技能ID</param>
        public void ExcuteSkill(int id)
        {
            if (CurSkill != null && CurSkill.IsExcute)
            {
                SkillToExcuteList.Add(id);
                return;
            }
            if (PlayerSkills.TryGetValue(id, out CurSkill))
            {
                CurSkill.Excute();
            }
        }

        /// <summary>
        /// 得到技能的冷却数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public float GetSkillCdPercent(int id)
        {
            Skill skill = null;
            if (PlayerSkills.TryGetValue(id, out skill))
            {
                return skill.CDTimePercent;
            }

            return 0f;
        }

        /// <summary>
        /// Update当前正在执行技能的Update
        /// </summary>
        private void Update()
        {
            if (CurSkill != null) CurSkill.Update();
        }


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            //初始化所有技能
            StartCoroutine(InitAllSkill());
            //初始化玩家技能
            InitPlayerSkill();
        }

        /// <summary>
        /// 根据序列化初始化当前的所有技能
        /// </summary>
        private IEnumerator InitAllSkill()
        {
            var bundle = new WWW("file://" + Application.dataPath + "/name.unity3D");
            yield return bundle;

            var player = GameObject.FindGameObjectWithTag(Tags.Player);
            var skill = bundle.assetBundle.LoadAll(typeof (Skill));
            foreach (var o in skill)
            {
                var s = o as Skill;
                print(s);
                PlayerSkills.Add(s.Id, s);
                s.Init(player);
            }
        }

        /// <summary>
        /// 连接服务器获得当前角色的所有技能
        /// </summary>
        private void InitPlayerSkill()
        {
            //TODO
            //PlayerSkills = _skills;
        }

        private void OnGUI()
        {
            GUILayout.Label(_skills.Count.ToString());
        }
    }
}