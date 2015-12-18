using Assets.Scripts.UIPlugin;
using UnityEngine;

namespace Assets.Scripts.View.MainMenu
{
    /// <summary>
    /// 底部按钮组
    /// </summary>
    public class BottomBarView : MonoBehaviour
    {
        public UIScale TaskUiScale;
        public UIScale KnapscakUiScale;


        private void Start()
        {
        }

        public void OnTaskButtonClick()
        {
            TaskUiScale.SetActive(!TaskUiScale.gameObject.activeSelf);
        }

        public void OnKnapscakButtonClick()
        {
            KnapscakUiScale.SetActive(!KnapscakUiScale.gameObject.activeSelf);
        }
    }
}