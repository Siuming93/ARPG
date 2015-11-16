using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageManger : MonoBehaviour
{
    public Text MessageText;

    public static MessageManger Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SetMessage(string message, float timer = 2f)
    {
        MessageText.text = message;
        StartCoroutine(ClearMessage(timer));
    }

    private IEnumerator ClearMessage(float timer)
    {
        yield return new WaitForSeconds(timer);

        MessageText.text = "";
    }
}