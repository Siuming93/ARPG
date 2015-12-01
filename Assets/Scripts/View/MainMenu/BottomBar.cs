using UnityEngine;

namespace Assets.Scripts.View.MainMenu
{
    public class BottomBar : MonoBehaviour
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