using Assets.Scripts.Presenter.Manager;
using Assets.Scripts.UIPlugin;
using UnityEngine;

namespace Assets.Scripts.View.Transcript
{
    /// <summary>
    /// 显示通关信息
    /// </summary>
    public class PassTranscriptUiView : MonoBehaviour
    {
        public UIScale PassTranscriptUiScale;

        private void Start()
        {
            GameManager.Instance.OnPassTranscriptEvent += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            PassTranscriptUiScale.ScaleIn();
        }
    }
}