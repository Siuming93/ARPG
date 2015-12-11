using UnityEngine;
using System.Collections;
using Assets.Scripts.Presenter.Manager;

public class EnterTranscript : MonoBehaviour
{
    public string TranscirptName;

    private void OnTriggerEnter()
    {
        GameManager.Instance.LoadScene(TranscirptName);
    }
}