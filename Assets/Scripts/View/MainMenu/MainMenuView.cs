using Assets.Scripts.UIPlugin;
using UnityEngine;

namespace Assets.Scripts.View.MainMenu
{
    /// <summary>
    /// UI总管理器
    /// </summary>
    public class MainMenuView : MonoBehaviour
    {
        public UIScale PlayerStatusScale;

        /// <summary>
        /// 显示人物信息
        /// </summary>
        public void OnPlayerStatusShowButtonClick()
        {
            //1.信息面板处于激活状态,则返回
            if (PlayerStatusScale.gameObject.activeSelf)
                return;

            //2.未激活,则激活
            PlayerStatusScale.SetActive(true);
        }

        /// <summary>
        /// 隐藏人物信息
        /// </summary>
        public void OnPlayerStatusCloseButtonClick()
        {
            PlayerStatusScale.SetActive(false);
        }
    }
}