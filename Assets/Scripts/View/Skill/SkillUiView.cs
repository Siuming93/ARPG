using Assets.Scripts.Presenter.Manager;
using UnityEngine;

namespace Assets.Scripts.View.Skill
{
    internal class SkillUiView : MonoBehaviour
    {
        public void OnClick(SKillUiViewProperty sKillViewProperty)
        {
            SkillManager.Instance.ExcuteSkill(sKillViewProperty.Id);
        }
    }
}