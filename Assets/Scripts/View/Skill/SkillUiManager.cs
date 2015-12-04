using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    internal class SkillUiManager : MonoBehaviour
    {
        public void OnClick(SKillUiProperty sKillProperty)
        {
            SkillManager.Instance.ExcuteSkill(sKillProperty.Id);
        }
    }
}