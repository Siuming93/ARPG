using UnityEngine;
using System.Collections;
using System.Reflection.Emit;
using Assets.Scripts.Presenter.Manager;

public class PassTranscriptUiPanel : MonoBehaviour
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