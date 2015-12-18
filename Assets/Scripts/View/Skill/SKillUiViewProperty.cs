using Assets.Scripts.Presenter.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Skill
{
    /// <summary>
    /// 挂在GameObject的Button上,来连接BUtton与技能
    /// </summary>
    public class SKillUiViewProperty : MonoBehaviour
    {
        //技能信息
        public string Name;
        public int Id;
        public string Description;
        public string ImageName;
        //组件信息
        public Image MaskChild;
        public Button Button;

        private void Update()
        {
            //解决冷却问题
            MaskChild.fillAmount = SkillManager.Instance.GetSkillCdPercent(Id);
            if (MaskChild.fillAmount > 0)
            {
                Button.enabled = false;
            }
            else
            {
                Button.enabled = true;
            }
        }
    }
}