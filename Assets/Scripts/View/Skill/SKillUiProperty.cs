using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Skill
{
    public class SKillUiProperty : MonoBehaviour
    {
        public string Name;
        public int Id;
        public string Description;
        public string ImageName;

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