using UnityEngine;
using System.Collections;
using Assets.Scripts.Presenter.Manager;

public class TranscriptUiManager : MonoBehaviour
{
    public void OnPassTranscriptButtonEnter(string name)
    {
        GameManager.Instance.LoadScene(name);
    }
}