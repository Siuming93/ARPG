using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.View.Skill;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    private Dictionary<int, Skill> _skills = new Dictionary<int, Skill>();

    public readonly Dictionary<int, Skill> PlayerSkills = new Dictionary<int, Skill>();
    public Skill CurSkill;

    private List<int> SkillToExcuteList = new List<int>();

    public bool IsCurSkillExcute
    {
        get
        {
            if (CurSkill != null)
                return CurSkill.IsExcute;

            return false;
        }
    }

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

    public float GetSkillCdPercent(int id)
    {
        Skill skill = null;
        if (PlayerSkills.TryGetValue(id, out skill))
        {
            return skill.CDTimePercent;
        }

        return 0f;
    }

    private void Update()
    {
        if (CurSkill != null) CurSkill.Update();
    }


    private void Awake()
    {
        Instance = this;
        InitAllSkill();
    }

    private void InitAllSkill()
    {
        var path = "Assets/Resources/SkillData";
        var parentDirectory = new DirectoryInfo(path);
        var player = GameObject.FindGameObjectWithTag(Tags.Player);

        var childDirectories = parentDirectory.GetDirectories();
        for (int i = 0; i < childDirectories.Length; i++)
        {
            var skill = Resources.Load<Skill>("SkillData/" + childDirectories[i].Name + "/Skill");
            _skills.Add(int.Parse(childDirectories[i].Name), skill);
            skill.Init(player);
            PlayerSkills[int.Parse(childDirectories[i].Name)] = skill;
        }
    }
}