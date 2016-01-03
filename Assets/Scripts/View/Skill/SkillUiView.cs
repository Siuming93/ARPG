using Assets.Scripts.Presenter.Manager;
using Assets.Scripts.Presenter.Manager.Charcter;
using UnityEngine;

namespace Assets.Scripts.View.Skill
{
    internal class SkillUiView : MonoBehaviour
    {
        public SkillManager PlayerSkillManager;

        public void OnClick(SKillUiViewProperty sKillViewProperty)
        {
            PlayerSkillManager.ExcuteSkill(sKillViewProperty.Id);
        }
    }
}