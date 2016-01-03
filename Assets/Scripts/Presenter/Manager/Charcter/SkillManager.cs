using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UIPlugin;
using Assets.Scripts.View.Skill;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.Charcter
{
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        //不同平台下StreamingAssets的路径是不同的，这里需要注意一下。
        public static readonly string PathURL =
#if UNITY_ANDROID
            "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
		Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
            "file://" + Application.dataPath + "/StreamingAssets/";
#else
        string.Empty;
#endif

        /// <summary>
        /// 所有技能的字典
        /// </summary>
        private static Dictionary<int, Skill> _skills = new Dictionary<int, Skill>();

        /// <summary>
        /// 当前角色所拥有的技能
        /// </summary>
        public List<int> PlayerSkills = new List<int>();

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
            if (PlayerSkills.Contains(id) && _skills.TryGetValue(id, out CurSkill))
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
            if (PlayerSkills.Contains(id) && _skills.TryGetValue(id, out CurSkill))
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
        }

        private void Start()
        {
            if (_skills.Count == 0)
            {
                //初始化所有技能
                StartCoroutine(InitAllSkill());
                //初始化玩家技能
                InitPlayerSkill();
            }
        }

        /// <summary>
        /// 根据序列化初始化当前的所有技能
        /// </summary>
        private IEnumerator InitAllSkill()
        {
            WWW bundle = null;

            bundle = new WWW(PathURL + "name.assetbundle");
            MessageUiManger.Instance.Print("Path:" + PathURL + "name.assetbundle");
            MessageUiManger.Instance.Print("Bundle.error:" + bundle.error);
            MessageUiManger.Instance.Print("bundle.assetBundle" + bundle.assetBundle);
            yield return bundle;

            try
            {
                var player = GameObject.FindGameObjectWithTag(Tags.Player);
                var skill = bundle.assetBundle.LoadAll(typeof (Skill));
                foreach (var o in skill)
                {
                    var s = o as Skill;
                    MessageUiManger.Instance.Print(s.ToString());
                    _skills.Add(s.Id, s);
                    s.Init(player);
                }
            }
            catch (Exception e)
            {
                MessageUiManger.Instance.Print(e.Message);
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
    }
}